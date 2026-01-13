Public Class Xl_PgcClassTree
    Inherits TreeView

    Private _Lang As DTOLang
    Private _Values As List(Of DTOPgcClass)
    Private _NodeList As List(Of TreeNode)

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToAddRoot(sender As Object, e As MatEventArgs)
    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private _AllowEvents As Boolean

    Public Shadows Sub Load(values As List(Of DTOPgcClass), oLang As DTOLang, Optional oSelectedClass As DTOPgcClass = Nothing)
        _Values = values
        _Lang = oLang
        _NodeList = New List(Of TreeNode)

        MyBase.AllowDrop = True

        If oSelectedClass Is Nothing Then
            If MyBase.SelectedNode IsNot Nothing Then
                oSelectedClass = MyBase.SelectedNode.Tag
            End If
        End If

        MyBase.Nodes.Clear()
        For Each oValue As DTOPgcClass In _Values
            Dim oTreeNode As TreeNode = GetNode(oValue)
            MyBase.Nodes.Add(oTreeNode)
            _NodeList.Add(oTreeNode)
        Next

        If oSelectedClass IsNot Nothing Then
            Me.SelectedValue = oSelectedClass
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Values As List(Of DTOPgcClass)
        Get
            Return _Values
        End Get
    End Property

    Public ReadOnly Property SelectedClass As DTOPgcClass
        Get
            Dim retval As DTOPgcClass = Nothing
            Dim oNode As TreeNode = MyBase.SelectedNode
            If oNode IsNot Nothing Then
                retval = oNode.Tag
            End If
            Return retval
        End Get
    End Property

    Public WriteOnly Property SelectedValue() As DTOPgcClass
        Set(value As DTOPgcClass)
            Dim oNode As TreeNode = _NodeList.Find(Function(x) DirectCast(x.Tag, DTOPgcClass).Guid.Equals(value.Guid))
            If oNode IsNot Nothing Then
                MyBase.SelectedNode = oNode
            End If
        End Set
    End Property

    Private Function GetNode(oValue As DTOPgcClass) As TreeNode
        Dim retval As New TreeNode(oValue.Nom.Tradueix(_Lang))
        retval.Tag = oValue
        If oValue.Children IsNot Nothing Then
            For Each Item As DTOPgcClass In oValue.Children
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
            Dim oClass_Menu As New Menu_PgcClass(oNode.Tag)
            AddHandler oClass_Menu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oClass_Menu.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("afegir arrel", Nothing, AddressOf Do_AddRoot)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(sender As Object, e As MatEventArgs)
        RaiseEvent RequestToRefresh(Me, e)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_AddRoot()
        RaiseEvent RequestToAddRoot(Me, MatEventArgs.Empty)
    End Sub


    Private Sub Xl_PgcClasss_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles Me.AfterSelect
        SetContextMenu()
        RaiseEvent ValueChanged(Me, New MatEventArgs(e.Node.Tag))
    End Sub

#Region "DragDrop"
    Private Async Sub Xl_PgcClasss_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        If (e.Data.GetDataPresent(GetType(TreeNode))) Then
            Await DragDropClass(e)
        ElseIf (e.Data.GetDataPresent(GetType(List(Of DTOPgcCta)))) Then
            DragDropCta(e)
        End If
    End Sub

    Private Async Function DragDropClass(e As DragEventArgs) As Task
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
                If TypeOf targetNode.Tag Is DTOPgcClass Or TypeOf targetNode.Tag Is DTOPgcClass Then
                    Dim exs As New List(Of Exception)
                    Dim draggedSrc As DTOPgcClass = draggedNode.Tag
                    If FEB2.PgcClass.Load(draggedSrc, exs) Then
                        draggedSrc.Parent = targetNode.Tag
                        If Await FEB2.PgcClass.Update(draggedSrc, exs) Then
                            RaiseEvent RequestToRefresh(Me, New MatEventArgs(draggedSrc))
                        Else
                            UIHelper.WarnError(exs)
                        End If
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
    End Function
    Private Async Sub DragDropCta(e As DragEventArgs)
        Dim exs As New List(Of Exception)
        ' Retrieve the client coordinates of the drop location.
        Dim targetPoint As Point = MyBase.PointToClient(New Point(e.X, e.Y))

        ' Retrieve the node at the drop location.
        Dim targetNode As TreeNode = MyBase.GetNodeAt(targetPoint)

        ' Retrieve the account that was dragged.
        Dim draggedCtas As List(Of DTOPgcCta) = e.Data.GetData(GetType(List(Of DTOPgcCta)))
        Dim oLang As DTOLang = Current.Session.Lang

        If targetNode IsNot Nothing Then
            If draggedCtas IsNot Nothing Then
                If TypeOf targetNode.Tag Is DTOPgcClass Then
                    For Each oCta As DTOPgcCta In draggedCtas
                        If FEB2.PgcCta.Load(oCta, exs) Then
                            oCta.PgcClass = targetNode.Tag
                            If Not Await FEB2.PgcCta.Update(oCta, exs) Then
                                UIHelper.WarnError(exs, DTOPgcCta.FullNom(oCta, oLang))
                            End If
                        Else
                            UIHelper.WarnError(exs)
                        End If
                    Next
                    MyBase.SelectedNode = targetNode
                    RaiseEvent RequestToRefresh(Me, New MatEventArgs(targetNode.Tag))
                    _AllowEvents = True
                End If
            End If
        End If
    End Sub

    Private Sub Xl_PgcClasss_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter
        _AllowEvents = False
        If e.Data.GetDataPresent(GetType(List(Of DTOPgcCta))) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
        MyBase.Focus()
    End Sub

    Private Sub Xl_PgcClasss_ItemDrag(sender As Object, e As ItemDragEventArgs) Handles Me.ItemDrag
        _AllowEvents = False
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    Private Sub Xl_PgcClasss_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragOver

        'As the mouse moves over nodes, provide feedback to 
        'the user by highlighting the node that is the 
        'current drop target
        Dim pt As Point = DirectCast(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        Dim targetNode As TreeNode = MyBase.GetNodeAt(pt)

        'See if the targetNode is currently selected, 
        'if so no need to validate again
        If Not (MyBase.SelectedNode Is targetNode) Then
            If e.Data.GetDataPresent(GetType(TreeNode)) Then
                'Select the    node currently under the cursor
                If targetNode Is Nothing Then
                    e.Effect = DragDropEffects.None
                Else
                    If TypeOf (targetNode.Tag) Is DTOPgcClass Then
                        Dim oDraggedClass As DTOPgcClass = e.Data.GetData(GetType(TreeNode)).tag
                        If oDraggedClass Is Nothing Then
                            MyBase.SelectedNode = targetNode
                            e.Effect = DragDropEffects.Move
                        Else
                            If oDraggedClass.Guid.Equals(targetNode.Tag.guid) Then
                                e.Effect = DragDropEffects.None
                            Else
                                MyBase.SelectedNode = targetNode
                                e.Effect = DragDropEffects.Move
                            End If
                        End If
                    End If

                    'Check that the selected node is not the dropNode and
                    'also that it is not a child of the dropNode and 
                    'therefore an invalid target
                    'Dim dropNode As TreeNode =
                    'DirectCast(e.Data.GetData("System.Windows.Forms.TreeNode"),
                    'TreeNode)

                    'Do Until targetNode Is Nothing
                    'If targetNode Is dropNode Then
                    'e.Effect = DragDropEffects.None
                    'Exit Sub
                    'End If
                    '   targetNode = targetNode.Parent
                    '  Loop


                End If
            ElseIf e.Data.GetDataPresent(GetType(List(Of DTOPgcCta)))
                Dim oCtas As List(Of DTOPgcCta) = e.Data.GetData(GetType(List(Of DTOPgcCta)))
                If oCtas.Count > 0 Then
                    'Select the    node currently under the cursor
                    If targetNode Is Nothing Then
                        e.Effect = DragDropEffects.None
                    Else
                        If TypeOf (targetNode.Tag) Is DTOPgcClass Then
                            Dim oFirstCta As DTOPgcCta = oCtas.First
                            Dim oDraggedClass As DTOPgcClass = oFirstCta.PgcClass
                            If oDraggedClass Is Nothing Then
                                MyBase.SelectedNode = targetNode
                                e.Effect = DragDropEffects.Move
                            Else
                                If oDraggedClass.Guid.Equals(targetNode.Tag.guid) Then
                                    e.Effect = DragDropEffects.None
                                Else
                                    MyBase.SelectedNode = targetNode
                                    e.Effect = DragDropEffects.Move
                                End If
                            End If
                        End If

                    End If
                End If
            End If




        End If

    End Sub


#End Region
End Class


