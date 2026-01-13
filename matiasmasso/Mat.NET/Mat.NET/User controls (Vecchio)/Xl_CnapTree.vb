Public Class Xl_CnapTree
    Private _Cnaps As List(Of DTOCnap)
    Private _SelectionMode As bll.dEFAULTS.SelectionModes
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event SelectedItemChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(oCnaps As List(Of DTOCnap), Optional oDefaultCnap As DTOCnap = Nothing, Optional oSelectionMode As bll.dEFAULTS.SelectionModes = BLL.Defaults.SelectionModes.Browse)
        _Cnaps = oCnaps
        _SelectionMode = oSelectionMode
        For Each oCnap As DTOCnap In _Cnaps
            TreeView1.Nodes.Add(SetNode(oCnap))
        Next
        If oDefaultCnap IsNot Nothing Then SelectCnap(oDefaultCnap)
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Cnap As DTOCnap
        Get
            Return CurrentCnap()
        End Get
    End Property

    Private Function SetNode(ByVal oCnap As DTOCnap) As MaxiSrvr.TreeNodeObj
        Dim oNode As New MaxiSrvr.TreeNodeObj(BLL_Cnap.FullNom(oCnap, BLL_Cnap.NomCodis.Short, BLL.BLLSession.Current), oCnap)
        For Each oChild As DTOCnap In oCnap.Children
            oNode.Nodes.Add(SetNode(oChild))
        Next
        Return oNode
    End Function


    Private Sub TreeView1_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterSelect
        If _AllowEvents Then
            Dim oCnap As DTOCnap = CurrentCnap()
            If oCnap IsNot Nothing Then
                RaiseEvent SelectedItemChanged(Me, New MatEventArgs(CurrentCnap))
                SetContextMenu()
            End If
        End If
    End Sub


    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        If _SelectionMode = BLL.Defaults.SelectionModes.Selection Then
            RaiseEvent onItemSelected(Me, New MatEventArgs(CurrentCnap))
        End If
    End Sub

    Private Sub onCnapUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
        TreeView1.Nodes.Clear()
        _AllowEvents = False
        RaiseEvent RequestToRefresh(Me, EventArgs.Empty)
        SelectCnap(e.Argument)
    End Sub

    Private Sub SelectCnap(ByVal oCnap As DTOCnap)
        CnapLoader.Load(oCnap)
        Dim oRootNodes As MaxiSrvr.TreeNodeObjs = MaxiSrvr.TreeNodeObjs.FromTreeView(TreeView1)
        'Dim oNodes() As TreeNode = TreeView1.Nodes.Find(oCnap.Nom, True)
        'Dim oSelectedNode As MaxiSrvr.TreeNodeObj = Nothing
        'For Each oNode As TreeNodeObj In oNodes
        ' If CType(oNode.Obj, Cnap).Guid.Equals(oCnap.Guid) Then
        ' TreeView1.SelectedNode = oNode
        ' TreeView1.SelectedNode.EnsureVisible()
        ' End If
        ' Next
        'Dim oSelectedNode As MaxiSrvr.TreeNodeObj = TreeView1.Nodes.Find(Function(x) CType(CType(x, TreeNodeObj).Obj, Cnap).Guid.Equals(oCnap.Guid))
        Dim oSelectedNode As MaxiSrvr.TreeNodeObj = GetCnapNode(oRootNodes, oCnap)
        TreeView1.SelectedNode = oSelectedNode
        TreeView1.SelectedNode.EnsureVisible()
        '_AllowEvents = True
    End Sub

    Private Function GetCnapNode(ByVal oNodes As MaxiSrvr.TreeNodeObjs, ByVal oCnap As DTOCnap) As MaxiSrvr.TreeNodeObj
        Dim retval As maxisrvr.TreeNodeObj = Nothing
        For Each oNode As maxisrvr.TreeNodeObj In oNodes
            If oCnap.Guid.Equals(DirectCast(oNode.Obj, DTOCnap).Guid) Then
                retval = oNode
                Exit For
            Else
                retval = GetCnapNode(oNode.Children, oCnap)
                If retval IsNot Nothing Then Exit For
            End If
        Next
        Return retval
    End Function

    Private Sub onCnapDelete(ByVal sender As Object, ByVal e As System.EventArgs)
        TreeView1.Nodes.Clear()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)

        Dim oParent = DirectCast(sender, DTOCnap).Parent
        SelectCnap(oParent)
    End Sub

    Private Function CurrentNode() As MaxiSrvr.TreeNodeObj
        Dim oNode As maxisrvr.TreeNodeObj = TreeView1.SelectedNode
        Return oNode
    End Function

    Private Function CurrentCnap() As DTOCnap
        Dim retval As DTOCnap = Nothing
        Dim oNode As TreeNodeObj = CurrentNode()
        If oNode IsNot Nothing Then
            retval = oNode.Obj
        End If
        Return retval
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip

        Dim oCnap As DTOCnap = CurrentCnap()
        If oCnap IsNot Nothing Then
            Dim oMenu_CNap As New Menu_Cnap(oCnap)
            oContextMenu.Items.AddRange(oMenu_CNap.Range)
            oContextMenu.Items.Add("-")
        End If

        oContextMenu.Items.Add(New ToolStripMenuItem("afegir", Nothing, AddressOf Do_AddNew))
        TreeView1.ContextMenuStrip = oContextMenu

    End Sub


    Private Sub Do_AddNew()
        Dim oParentCnap As DTOCnap = CurrentCnap()

        Dim oCnap As New DTOCnap
        oCnap.Id = oParentCnap.Id & "X"
        oCnap.NomShort_ESP = "(nueva categoria)"
        oCnap.NomShort_CAT = "(nova categoria)"
        oCnap.NomShort_ENG = "(new category)"
        Dim oFrm As New Frm_Cnap(oCnap)
        AddHandler oFrm.AfterUpdate, AddressOf onCnapUpdate
        oFrm.Show()
    End Sub



End Class
