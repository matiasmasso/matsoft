Public Class Xl_WebMenuItems
    Inherits TreeView

    Private _Lang As DTOLang

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(values As List(Of DTOWebMenuGroup), oLang As DTOLang)
        _Lang = oLang
        MyBase.Nodes.Clear()
        For Each item As DTOWebMenuGroup In values
            MyBase.Nodes.Add(GetNode(item))
        Next

    End Sub

    Private Function GetNode(value As DTOWebMenuGroup) As TreeNode
        Dim retval As New TreeNode
        With retval
            .Text = value.LangText.Tradueix(_Lang)
            .Tag = value
            .Name = value.Guid.ToString
        End With

        For Each item As DTOWebMenuItem In value.Items
            retval.Nodes.Add(GetNode(item))
        Next

        Return retval
    End Function

    Private Function GetNode(value As DTOWebMenuItem) As TreeNode
        Dim retval As New TreeNode
        With retval
            .Text = value.LangText.Tradueix(_Lang) & " (" & value.LangUrl.Tradueix(_Lang) & ")"
            .Tag = value
            .Name = value.Guid.ToString
        End With

        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oMenu As New ContextMenuStrip

        Dim oNode As TreeNode = MyBase.SelectedNode
        If oNode IsNot Nothing Then
            If TypeOf oNode.Tag Is DTOWebMenuGroup Then
                Dim oWebMenuGroup As DTOWebMenuGroup = oNode.Tag
                If oWebMenuGroup IsNot Nothing Then
                    Dim oMenu_WebMenuGroup As New Menu_WebMenuGroup(oWebMenuGroup)
                    AddHandler oMenu_WebMenuGroup.AfterUpdate, AddressOf RefreshRequest
                    oMenu.Items.AddRange(oMenu_WebMenuGroup.Range)
                End If
            ElseIf TypeOf oNode.Tag Is DTOWebMenuItem Then
                Dim oWebMenuItem As DTOWebMenuItem = oNode.Tag
                If oWebMenuItem IsNot Nothing Then
                    Dim oMenu_WebMenuItem As New Menu_WebMenuItem(oWebMenuItem)
                    AddHandler oMenu_WebMenuItem.AfterUpdate, AddressOf RefreshRequest
                    oMenu.Items.AddRange(oMenu_WebMenuItem.Range)
                End If
                oMenu.Items.Add("-")
                oMenu.Items.Add(New ToolStripMenuItem("afegir", Nothing, AddressOf AddNewWebMenuItem))
                oMenu.Items.Add(New ToolStripMenuItem("refresca", Nothing, AddressOf refreshrequest))
            End If
        End If
        MyBase.ContextMenuStrip = oMenu
    End Sub

    Private Sub AddNewWebMenuItem(sender As Object, e As EventArgs)
        Dim oParent As DTOBaseGuid = Nothing
        Dim oParentNode As TreeNode = MyBase.SelectedNode
        If TypeOf oParentNode.Tag Is DTOWebMenuGroup Then
            oParent = oParentNode.Tag
        ElseIf TypeOf oParentNode.Tag Is DTOWebMenuItem Then
            oParent = oParentNode.Parent.Tag
        End If

        Dim oWebMenuItem As New DTOWebMenuItem()
        With oWebMenuItem
            .Group = oParent
        End With
        Dim oFrm As New Frm_WebMenuItem(oWebMenuItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub TreeNode_NodeMouseDoubleClick() Handles MyBase.NodeMouseDoubleClick
        Dim oNode As TreeNode = MyBase.SelectedNode
        If TypeOf oNode.Tag Is DTOWebMenuGroup Then
            Dim oFrm As New Frm_WebMenuGroup(oNode.Tag)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf oNode.Tag Is DTOWebMenuItem Then
            Dim oFrm As New Frm_WebMenuItem(oNode.Tag)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If

    End Sub

    Private Sub Xl_WinMenuTree_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            SetContextMenu()
        End If
    End Sub
End Class
