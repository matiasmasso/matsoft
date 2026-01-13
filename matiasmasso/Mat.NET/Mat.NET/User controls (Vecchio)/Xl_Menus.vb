Public Class Xl_Menus
    Inherits TreeView

    Private _Lang as DTOLang
    Private _App As DTOSession.AppTypes
    Private _Menus As List(Of DTOMenu)
    Private _SelectedMenu As DTOMenu = Nothing
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private _AllowEvents As Boolean

    Public Shadows Sub Load(oApp As DTOSession.AppTypes, oLang as DTOLang)
        _App = oapp
        _Menus = BLL.BLLMenus.FromApp(_App)
        _Lang = oLang

        _SelectedMenu = Nothing
        If MyBase.SelectedNode IsNot Nothing Then
            _SelectedMenu = MyBase.SelectedNode.Tag
        End If

        MyBase.Nodes.Clear()
        For Each oMenu As DTOMenu In _Menus
            MyBase.Nodes.Add(GetNode(oMenu))
        Next
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function GetNode(oMenu As DTOMenu) As TreeNode
        Dim retval As New TreeNode(BLL.BLLMenu.Nom(oMenu, _Lang))
        retval.Tag = oMenu
        For Each Item As DTOMenu In oMenu.Items
            retval.Nodes.Add(GetNode(Item))
        Next

        If _SelectedMenu IsNot Nothing Then
            If oMenu.Guid.Equals(_SelectedMenu.guid) Then
                retval.EnsureVisible()
            End If
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            Dim oMenu_Menu As New Menu_Menu(oNode.Tag)
            AddHandler oMenu_Menu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Menu.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Sub Do_AddNew()
        Dim oMenu As DTOMenu = BLL.BLLMenu.NewMenu(_App)
        If MyBase.SelectedNode IsNot Nothing Then
            Dim oParent As DTOMenu = MyBase.SelectedNode.Tag
            oMenu.Parent = oParent
            Dim iLastChild As Integer = oParent.Items.Max(Function(x) x.Ord)
            oMenu.Ord = iLastChild + 1
        End If
        Dim oFrm As New Frm_Menu(oMenu)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_Menus_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        SetContextMenu()
    End Sub
End Class
