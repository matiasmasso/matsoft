Public Class Xl_CheckedFilters
    Inherits Xl_CheckedTreeView
    Private _AllowEvents As Boolean
    Private _PreviousSelection As List(Of DTOPurchaseOrder)
    'Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onOrderSelected(sender As Object, e As MatEventArgs)
    Public Property isDirty As Boolean


    Public Shadows Sub Load(oAllFilters As List(Of DTOFilter), oCheckedItems As List(Of DTOFilter.Item))
        _AllowEvents = True

        MyBase.Nodes.Clear()
        For Each oFilter In oAllFilters
            Dim oFilterNode = FilterNode(oFilter)
            MyBase.Nodes.Add(oFilterNode)
            For Each oItem In oFilter.items
                Dim i = oFilterNode.Nodes.Add(ItemNode(oItem))
                oFilterNode.Nodes(i).Checked = False
                If oCheckedItems.Any(Function(x) x.Guid.Equals(oItem.Guid)) Then
                    oFilterNode.Nodes(i).Checked = True
                    oFilterNode.Expand()
                End If
            Next
        Next

        _AllowEvents = True
    End Sub

    Public Function SelectedValues() As List(Of DTOFilter.Item)
        Dim retval As New List(Of DTOFilter.Item)
        For Each oFilterNode In MyBase.Nodes
            For Each oNode As TreeNode In oFilterNode.nodes
                If oNode.Checked Then
                    retval.Add(oNode.Tag)
                End If
            Next
        Next
        Return retval
    End Function

    Private Function FilterNode(oFilter As DTOFilter) As TreeNode
        Dim caption = oFilter.langText.Tradueix(Current.Session.Lang)
        Dim retval As New TreeNode(caption)
        retval.Tag = oFilter
        Return retval
    End Function

    Private Function ItemNode(oItem As DTOFilter.Item) As TreeNode
        Dim caption = oItem.langText.Tradueix(Current.Session.Lang)
        Dim retval As New TreeNode(caption)
        retval.Tag = oItem
        Return retval
    End Function


    Private Function matches(oOrder As DTOPurchaseOrder, oDelivery As DTODelivery) As Boolean
        Dim retval As Boolean
        If oDelivery.items.Count > 0 Then
            Dim oFirstItem = oDelivery.items.First
            If oFirstItem.purchaseOrderItem IsNot Nothing Then
                Dim oDeliveryOrder = oFirstItem.purchaseOrderItem.purchaseOrder
                retval = oDeliveryOrder.Equals(oOrder)
            End If
        End If
        Return retval
    End Function


    Protected Overrides Sub OnAfterCheck(ByVal e As System.Windows.Forms.TreeViewEventArgs)
        If _AllowEvents Then
            _AllowEvents = False
            _IsDirty = True
            MyBase.OnAfterCheck(e)
            Application.DoEvents()
            MyBase.RaiseAfterUpdate()
            _AllowEvents = True
        End If
    End Sub

End Class

