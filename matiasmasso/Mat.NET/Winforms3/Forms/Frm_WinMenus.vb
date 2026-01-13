Public Class Frm_WinMenus
    Private _Lang As DTOLang = Current.Session.Lang
    Private _FlatNodes As New List(Of TreeNode)

    Private Async Sub Frm_WinMenus_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As MatEventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim items = Await FEB.WinMenuItems.All(exs, Current.Session.User)
        If exs.Count = 0 Then
            Dim oTreeItems = FEB.WinMenuItems.Tree(items)
            LoadNodes(TreeView1.Nodes, oTreeItems)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Function

    Private Sub LoadNodes(oNodes As TreeNodeCollection, items As List(Of DTOWinMenuItem))
        For Each item In items
            Dim oNode As New TreeNode(item.LangText.Tradueix(_Lang))
            oNode.Tag = item
            If item.Children IsNot Nothing Then
                LoadNodes(oNode.Nodes, item.Children)
            End If
            oNodes.Add(oNode)
            _FlatNodes.Add(oNode)
        Next
    End Sub

    Private Sub TreeView1_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseClick
        If e.Button = MouseButtons.Right Then
            setcontextmenu
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim item As DTOWinMenuItem = TreeView1.SelectedNode.Tag

        If item IsNot Nothing Then
            Select Case item.CustomTarget
                Case DTOWinMenuItem.CustomTargets.Bancs, DTOWinMenuItem.CustomTargets.Staff, DTOWinMenuItem.CustomTargets.Reps
                Case Else
                    Dim oMenu_WinMenuItem As New Menu_WinMenuItem(item)
                    AddHandler oMenu_WinMenuItem.AfterUpdate, AddressOf refreshRequest
                    oContextMenu.Items.AddRange(oMenu_WinMenuItem.Range)
                    oContextMenu.Items.Add("-")
            End Select
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu

    End Sub

    Private Sub Do_AddNew()
        Dim oWinMenuItem As DTOWinMenuItem = Nothing
        Dim oTargetNode As TreeNode = TreeView1.SelectedNode
        Dim oTargetItem As DTOWinMenuItem = Nothing
        If oTargetNode IsNot Nothing Then
            oTargetItem = oTargetNode.Tag
            If oTargetItem.Cod = DTOWinMenuItem.Cods.Folder Then
                oWinMenuItem = DTOWinMenuItem.Factory(oTargetItem)
            Else
                oWinMenuItem = DTOWinMenuItem.Factory(oTargetItem.Parent)
            End If
        End If
        Dim oFrm As New Frm_WinMenuItem(oWinMenuItem)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub refreshRequest(sender As Object, e As MatEventArgs)
        Dim item As DTOWinMenuItem = e.Argument
        Dim oNode As TreeNode = _FlatNodes.FirstOrDefault(Function(x) x.Tag.Equals(item))
        oNode.Text = item.LangText.Tradueix(_Lang)
    End Sub


#Region "DragDrop"
    Public Sub TreeView_ItemDrag(ByVal sender As Object,
                                  ByVal e As ItemDragEventArgs) _
                                  Handles TreeView1.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Public Sub TreeView_DragEnter(ByVal sender As Object,
                               ByVal e As DragEventArgs) _
                               Handles TreeView1.DragEnter
        e.Effect = DragDropEffects.Move
    End Sub

    Public Async Sub TreeView_DragDrop(ByVal sender As Object,
                              ByVal e As DragEventArgs) _
                              Handles TreeView1.DragDrop

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then
            Dim SourceNode As TreeNode = DirectCast(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
            Dim pt As Point = DirectCast(sender, TreeView).PointToClient(New Point(e.X, e.Y))
            Dim TargetNode As TreeNode = DirectCast(sender, TreeView).GetNodeAt(pt)

            Dim TargetItem As DTOWinMenuItem = TargetNode.Tag
            Dim SourceItem As DTOWinMenuItem = SourceNode.Tag

            Dim exs As New List(Of Exception)
            Select Case TargetItem.Cod
                Case DTOWinMenuItem.Cods.Folder
                    SourceItem.Parent = TargetNode.Tag

                    If Await FEB.WinmenuItem.Update(SourceItem, exs) Then
                        Dim oClonNode = SourceNode.Clone
                        TargetNode.Nodes.Add(oClonNode)
                        'Remove original node
                        SourceNode.Remove()
                        TargetNode.Expand()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTOWinMenuItem.Cods.Item
                    If SourceItem.Cod = DTOWinMenuItem.Cods.Item Then
                        Dim oFolder = SourceNode.Parent
                        Dim oClonNode = oFolder.Nodes.Insert(TargetNode.Index, SourceNode.Text)
                        oClonNode.Tag = SourceNode.Tag
                        SourceNode.Remove()

                        Dim items As New List(Of DTOWinMenuItem)
                        For Each node In oFolder.Nodes
                            items.Add(node.tag)
                        Next
                        If Await FEB.WinMenuItems.SaveOrder(items, exs) Then
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Else
                        UIHelper.WarnError("No es poden arrossegar carpetes dins dels iconos de programa")
                    End If
            End Select


        End If
    End Sub

    Private Sub TreeView1_DragOver(sender As Object, e As DragEventArgs) Handles TreeView1.DragOver
        Dim node = TreeView1.GetNodeAt(TreeView1.PointToClient(New Point(e.X, e.Y)))
        TreeView1.SelectedNode = node
    End Sub
#End Region

End Class