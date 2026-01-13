Public Class Xl_WinMenuTree
    Inherits TreeView

    Private _User As DTOUser
    Private _Values As List(Of DTOWinMenuItem)
    Private _DefaultItem As DTOBaseGuid
    Private _DefaultNode As TreeNode

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(items As List(Of DTOWinMenuItem), oDefaultItem As DTOBaseGuid)
        _Values = items
        _DefaultItem = oDefaultItem
        If Not MyBase.AllowDrop Then SetProperties()

        MyBase.Nodes.Clear()
        Dim rootFolders = items.Where(Function(x) x.Parent Is Nothing).ToList
        For Each item In rootFolders
            Try
                Dim oTreeNode = GetNode(item)
                MyBase.Nodes.Add(oTreeNode)

            Catch ex As Exception
                Stop
            End Try
        Next

        If _DefaultNode Is Nothing Then
            MyBase.SelectedNode = MyBase.Nodes(0)
        Else
            MyBase.SelectedNode = _DefaultNode
            If MyBase.SelectedNode Is Nothing And MyBase.Nodes.Count > 0 Then
                MyBase.SelectedNode = MyBase.Nodes(0)
            End If
        End If

        MyBase.SelectedNode.Expand()

    End Sub

    Private Sub SetProperties()
        MyBase.AllowDrop = True

        _User = Current.Session.User

        Dim oImageList As New ImageList
        oImageList.Images.Add("Closed", My.Resources.Folder_16closed)
        oImageList.Images.Add("Open", My.Resources.Folder_16open)
        MyBase.ImageList = oImageList
    End Sub

    Private Function GetNode(oSrc As DTOWinMenuItem) As TreeNode
        Dim retval As New TreeNode
        With retval
            .Text = oSrc.LangText.Tradueix(_User.Lang)
            .Tag = oSrc
            .Name = oSrc.Guid.ToString
            .ImageKey = "Closed"
            .SelectedImageKey = "Open"
            If _DefaultItem IsNot Nothing AndAlso oSrc.Guid.Equals(_DefaultItem.Guid) Then
                _DefaultNode = retval
                .Expand()
            End If
        End With

        'Dim oChildren As List(Of DTOWinMenuItem) = oSrc.Children.FindAll(Function(x) x.Cod = DTOWinMenuItem.Cods.Folder)
        Dim oChildren As List(Of DTOWinMenuItem) = _Values.Where(Function(x) x.Parent IsNot Nothing AndAlso x.Parent.Guid.Equals(oSrc.Guid) AndAlso x.Cod = DTOWinMenuItem.Cods.Folder).ToList
        For Each item As DTOWinMenuItem In oChildren
            retval.Nodes.Add(GetNode(item))
        Next

        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oMenu As New ContextMenuStrip

        Dim oNode As TreeNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            If _User.Rol.Id = DTORol.Ids.SuperUser Then
                Dim oWinMenuItem As DTOWinMenuItem = oNode.Tag
                If oWinMenuItem IsNot Nothing Then
                    Dim oMenu_WinMenuItem As New Menu_WinMenuItem(oWinMenuItem)
                    AddHandler oMenu_WinMenuItem.AfterUpdate, AddressOf refreshrequest
                    AddHandler oMenu_WinMenuItem.AfterDelete, AddressOf AfterDelete
                    oMenu.Items.AddRange(oMenu_WinMenuItem.Range)
                End If
                oMenu.Items.Add("-")
                oMenu.Items.Add(New ToolStripMenuItem("afegir", Nothing, AddressOf AddNewWinMenuItem))
            End If
            oMenu.Items.Add(New ToolStripMenuItem("refresca", Nothing, AddressOf refreshrequest))
        End If
        MyBase.ContextMenuStrip = oMenu
    End Sub

    Private Sub AfterUpdate(sender As Object, e As MatEventArgs)
        Dim oBaseGuid As DTOBaseGuid = e.Argument
        SaveSetting(DTOSession.Settings.Last_Menu_Selection, oBaseGuid.Guid.ToString())
        refreshrequest()
    End Sub

    Private Sub AfterDelete(sender As Object, e As MatEventArgs)
        Dim oDeletedItem As DTOWinMenuItem = e.Argument
        If oDeletedItem.Parent IsNot Nothing Then
            SaveSetting(DTOSession.Settings.Last_Menu_Selection, oDeletedItem.Parent.Guid.ToString())
        End If
        refreshrequest()
    End Sub

    Private Sub refreshrequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub AddNewWinMenuItem(sender As Object, e As EventArgs)
        Dim oParent As DTOWinMenuItem = MyBase.SelectedNode.Tag
        Dim oWinMenuItem = DTOWinMenuItem.Factory(oParent)
        Dim oFrm As New Frm_WinMenuItem(oWinMenuItem)
        AddHandler oFrm.AfterUpdate, AddressOf AfterUpdate
        oFrm.Show()
    End Sub

    Private Async Sub Xl_WinMenuTree_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        If (e.Data.GetDataPresent(GetType(TreeNode))) Then
            ' Retrieve the client coordinates of the drop location.
            Dim targetPoint As Point = MyBase.PointToClient(New Point(e.X, e.Y))

            ' Retrieve the node at the drop location.
            Dim targetNode As TreeNode = MyBase.GetNodeAt(targetPoint)

            ' Retrieve the node that was dragged.
            Dim draggedNode As TreeNode = e.Data.GetData(GetType(TreeNode))

            ' Confirm that the node at the drop location is not 
            ' the dragged node and that target node isn't null
            ' (for example if you drag outside the control)
            If targetNode IsNot Nothing Then
                If Not draggedNode.Equals(targetNode) Then
                    If TypeOf targetNode.Tag Is DTOWinMenuItem Then
                        Dim oUser As DTOUser = Current.Session.User
                        If oUser.Rol.IsSuperAdmin Then
                            Dim rc As MsgBoxResult = MsgBox("canviem la carpeta de ubicació?", MsgBoxStyle.OkCancel)
                            If rc = MsgBoxResult.Ok Then
                                Dim exs As New List(Of Exception)
                                Dim draggedSrc As DTOWinMenuItem = draggedNode.Tag
                                If FEB2.WinmenuItem.Load(draggedSrc, exs) Then
                                    draggedSrc.Parent = targetNode.Tag
                                    If Await FEB2.WinmenuItem.Update(draggedSrc, exs) Then
                                        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
                                    Else
                                        UIHelper.WarnError(exs)
                                    End If
                                Else
                                    UIHelper.WarnError(exs)
                                End If
                            End If
                        End If


                    End If
                End If
                ' Remove the node from its current 
                ' location and add it to the node at the drop location.
                'draggedNode.Remove()
                'targetNode.Nodes.Add(draggedNode)

                ' Expand the node at the location 
                ' to show the dropped node.
                'targetNode.Expand()


            End If
        End If
    End Sub

    Private Sub Xl_WinMenuTree_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Private Sub Xl_WinMenuTree_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles Me.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub


    Private Sub Xl_WinMenuTree_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            SetContextMenu()
        Else
            SaveSetting(DTOSession.Settings.Last_Menu_Selection, e.Node.Name)
            Dim item As DTOWinMenuItem = e.Node.Tag
            RaiseEvent ValueChanged(Me, New MatEventArgs(item))
        End If
        MyBase.Refresh()
    End Sub
End Class
