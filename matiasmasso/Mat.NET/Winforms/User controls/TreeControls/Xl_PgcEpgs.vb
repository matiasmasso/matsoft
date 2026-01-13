Public Class Xl_PgcClassTree
    Inherits TreeView

    Private _Lang As DTOLang
    Private _Values As List(Of DTOPgcEpgBase)
    Private _NodeList As List(Of TreeNode)

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private _AllowEvents As Boolean

    Public Shadows Sub Load(values As List(Of DTOPgcEpgBase), oLang As DTOLang, Optional oSelectedEpg As DTOPgcEpgBase = Nothing)
        _Values = values
        _Lang = oLang
        _NodeList = New List(Of TreeNode)

        MyBase.AllowDrop = True

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

    Public ReadOnly Property SelectedEpg As DTOPgcEpgBase
        Get
            Dim retval As DTOPgcEpgBase = Nothing
            Dim oNode As TreeNode = MyBase.SelectedNode
            If oNode IsNot Nothing Then
                retval = oNode.Tag
            End If
            Return retval
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


    Private Sub Xl_PgcClassTree_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Me.AfterSelect
        SetContextMenu()
        RaiseEvent ValueChanged(Me, New MatEventArgs(e.Node.Tag))
    End Sub

#Region "DragDrop"
    Private Sub Xl_PgcClassTree_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        If (e.Data.GetDataPresent(GetType(TreeNode))) Then
            DragDropEpg(e)
        ElseIf (e.Data.GetDataPresent(GetType(List(Of DTOPgcCta)))) Then
            DragDropCta(e)
        End If
    End Sub

    Private Sub DragDropEpg(e As DragEventArgs)
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
                If TypeOf targetNode.Tag Is DTOPgcEpg0 Or TypeOf targetNode.Tag Is DTOPgcEpg1 Then
                    Dim exs As New List(Of Exception)
                    Dim draggedSrc As DTOPgcEpgBase = draggedNode.Tag
                    BLL.BLLPgcEpgBase.Load(draggedSrc)
                    draggedSrc.Parent = targetNode.Tag
                    If BLL.BLLPgcEpgBase.Update(draggedSrc, exs) Then
                        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
                    Else
                        UIHelper.WarnError(exs)
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
    End Sub
    Private Sub DragDropCta(e As DragEventArgs)
        ' Retrieve the client coordinates of the drop location.
        Dim targetPoint As Point = MyBase.PointToClient(New Point(e.X, e.Y))

        ' Retrieve the node at the drop location.
        Dim targetNode As TreeNode = MyBase.GetNodeAt(targetPoint)

        ' Retrieve the account that was dragged.
        Dim draggedCtas As List(Of DTOPgcCta) = e.Data.GetData(GetType(List(Of DTOPgcCta)))
        Dim oLang As DTOLang = BLLSession.Current.Lang

        If targetNode IsNot Nothing Then
            If draggedCtas IsNot Nothing Then
                If TypeOf targetNode.Tag Is DTOPgcEpg2 Then
                    Dim exs As New List(Of Exception)
                    For Each oCta As DTOPgcCta In draggedCtas
                        BLL.BLLPgcEpgBase.Load(oCta)
                        oCta.Epg = targetNode.Tag
                        If Not BLL.BLLPgcEpgBase.Update(oCta, exs) Then
                            UIHelper.WarnError(exs, BLLPgcCta.FullNom(oCta, oLang))
                        End If

                    Next
                    MyBase.SelectedNode = targetNode
                    RaiseEvent RequestToRefresh(Me, New MatEventArgs(targetNode.Tag))
                    _AllowEvents = True
                End If
            End If
        End If
    End Sub

    Private Sub Xl_PgcClassTree_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        _AllowEvents = False
        If e.Data.GetDataPresent(GetType(List(Of DTOPgcCta))) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
        MyBase.Focus()
    End Sub

    Private Sub Xl_PgcClassTree_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles Me.ItemDrag
        _AllowEvents = False
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub Xl_PgcClassTree_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver

        'As the mouse moves over nodes, provide feedback to 
        'the user by highlighting the node that is the 
        'current drop target
        Dim pt As Point =
            CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        Dim targetNode As TreeNode = MyBase.GetNodeAt(pt)

        'See if the targetNode is currently selected, 
        'if so no need to validate again
        If Not (MyBase.SelectedNode Is targetNode) Then
            Dim oCtas As List(Of DTOPgcCta) = e.Data.GetData(GetType(List(Of DTOPgcCta)))
            If oCtas.Count > 0 Then
                'Select the    node currently under the cursor
                If targetNode Is Nothing Then
                    e.Effect = DragDropEffects.None
                Else
                    If TypeOf (targetNode.Tag) Is DTOPgcEpg2 Then
                        Dim oFirstCta As DTOPgcCta = oCtas.First
                        Dim oDraggedEpg As DTOPgcEpg2 = oFirstCta.Epg
                        If oDraggedEpg Is Nothing Then
                            MyBase.SelectedNode = targetNode
                            e.Effect = DragDropEffects.Move
                        Else
                            If oDraggedEpg.Guid.Equals(targetNode.Tag.guid) Then
                                e.Effect = DragDropEffects.None
                            Else
                                MyBase.SelectedNode = targetNode
                                e.Effect = DragDropEffects.Move
                            End If
                        End If
                    End If

                End If
            End If

            'Check that the selected node is not the dropNode and
            'also that it is not a child of the dropNode and 
            'therefore an invalid target
            'Dim dropNode As TreeNode =
            'CType(e.Data.GetData("System.Windows.Forms.TreeNode"),
            'TreeNode)

            'Do Until targetNode Is Nothing
            'If targetNode Is dropNode Then
            'e.Effect = DragDropEffects.None
            'Exit Sub
            'End If
            '   targetNode = targetNode.Parent
            '  Loop
        End If

    End Sub


#End Region
End Class

