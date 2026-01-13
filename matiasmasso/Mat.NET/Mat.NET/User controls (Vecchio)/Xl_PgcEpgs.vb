Public Class Xl_PgcEpgs
    Inherits TreeView

    Private _Lang as DTOLang
    Private _Values As List(Of DTOPgcEpgBase)
    Private _SelectedEpg As DTOPgcEpgBase = Nothing
    Private _NodeList As List(Of TreeNode)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Private _AllowEvents As Boolean

    Public Shadows Sub Load(values As List(Of DTOPgcEpgBase), oLang as DTOLang, Optional oSelectedEpg As DTOPgcEpgBase = Nothing)
        _Values = values
        _Lang = oLang
        _NodeList = New List(Of TreeNode)

        If oSelectedEpg Is Nothing Then
            If MyBase.SelectedNode IsNot Nothing Then
                oSelectedEpg = MyBase.SelectedNode.Tag
            End If
        End If

        MyBase.Nodes.Clear()
        For Each oValue As DTOPgcEpgBase In _Values
            Dim oTreeNode As TreeNode = GetNode(oValue)
            MyBase.Nodes.Add(oTreeNode)
            _NodeList.Add(oTreeNode)
        Next

        If oSelectedEpg IsNot Nothing Then
            Me.SelectedValue = oSelectedEpg
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Values As List(Of DTOPgcEpgBase)
        Get
            Return _Values
        End Get
    End Property

    Public WriteOnly Property SelectedValue() As DTOPgcEpgBase
        Set(value As DTOPgcEpgBase)
            Dim oNode As TreeNode = _NodeList.Find(Function(x) CType(x.Tag, DTOPgcEpgBase).Guid.Equals(value.Guid))
            If oNode IsNot Nothing Then
                MyBase.SelectedNode = oNode
            End If
        End Set
    End Property

    Private Function GetNode(oValue As DTOPgcEpgBase) As TreeNode
        Dim retval As New TreeNode(BLL_PgcEpgBase.FullNom(oValue, _Lang))
        retval.Tag = oValue
        If oValue.Children IsNot Nothing Then
            For Each Item As DTOPgcEpgBase In oValue.Children
                Dim oTreeNode As TreeNode = GetNode(Item)
                retval.Nodes.Add(oTreeNode)
                _NodeList.Add(oTreeNode)
            Next
        End If

        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            Dim oEpg_Menu As New Menu_PgcEpg(oNode.Tag)
            AddHandler oEpg_Menu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oEpg_Menu.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Sub Do_AddNew()
        Dim oEpg As DTOPgcEpgBase = Nothing
        Dim oParentNode As TreeNode = MyBase.SelectedNode
        Dim oParent As DTOPgcEpgBase = oParentNode.Tag
        Dim iOrdinal As Integer
        If oParent.Children.Count > 0 Then
            Dim oLastChild As DTOPgcEpgBase = oParent.Children.Last
            iOrdinal = oLastChild.Ordinal + 1
        End If

        If MyBase.SelectedNode Is Nothing Then
            oEpg = New DTOPgcEpg0
            Dim oFrm As New Frm_PgcEpg0(oEpg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (oParent) Is DTOPgcEpg0 Then
            oEpg = BLL_PgcEpg1.NewEpg(oParent, iOrdinal)
            Dim oFrm As New Frm_PgcEpg1(oEpg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (oParent) Is DTOPgcEpg1 Then
            oEpg = BLL_PgcEpg2.NewEpg(oParent, iOrdinal)
            Dim oFrm As New Frm_PgcEpg2(oEpg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (oParent) Is DTOPgcEpg2 Then
            oEpg = BLL_PgcEpg3.NewEpg(oParent, iOrdinal)
            Dim oFrm As New Frm_PgcEpg3(oEpg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (oParent) Is DTOPgcEpg3 Then
            oEpg = New DTOPgcCta
            oEpg.Parent = oParent
            'oFrm = New Frm_PgcCta(oEpg)
        End If


    End Sub

    Private Sub Xl_Values_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles Me.NodeMouseClick
        SetContextMenu()
    End Sub
End Class

