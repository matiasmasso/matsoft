Imports System.Reflection
Imports System.Runtime.CompilerServices

Public Class Xl_EciPlatformDeliveries
    Inherits Xl_CheckedTreeView

    Private _AllowEvents As Boolean
    Private _PreviousSelection As List(Of DTOPurchaseOrder)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event onOrderSelected(sender As Object, e As MatEventArgs)


    Public Shadows Sub Load(oGroups As List(Of DTOECITransmGroup), oDeptOrders As List(Of DTOPurchaseOrder))
        Dim oGroupNode As TreeNode = Nothing
        Dim oCentroNode As TreeNode = Nothing
        Dim oAssignedOrders As New List(Of DTOPurchaseOrder)
        _AllowEvents = False

        MyBase.Nodes.Clear()

        For Each oGroup In oGroups
            oGroupNode = GroupNode(oGroup)
            'oGroupNode.Expand()

            Dim oGroupCenters = GroupCenters(oGroup, oDeptOrders)
            For Each oCentro In oGroupCenters
                oCentroNode = CentroNode(oCentro)

                Dim oCentroOrders = CentroOrders(oGroup, oCentro, oDeptOrders)
                For Each oOrder In oCentroOrders
                    Dim oOrderGuid = oOrder.Guid
                    If Not oAssignedOrders.Any(Function(x) x.Guid.Equals(oOrderGuid)) Then
                        Dim oOrderNode = OrderNode(oOrder)
                        oCentroNode.Nodes.Add(oOrderNode)
                        oAssignedOrders.Add(oOrder)
                    End If
                Next

                If oCentroNode.Nodes.Count > 0 Then
                    oGroupNode.Nodes.Add(oCentroNode)
                End If
            Next

            If oGroupNode.Nodes.Count > 0 Then
                MyBase.Nodes.Add(oGroupNode)
                'oGroupNode.Checked = True
            End If
        Next

        Dim oNoGroup As New DTOECITransmGroup(Guid.Empty)
        Dim oMissingGroupOrders = oDeptOrders.Except(oAssignedOrders)
        If oMissingGroupOrders.Count > 0 Then
            oGroupNode = New TreeNode("(sense grup)")
            oGroupNode.Tag = oNoGroup
            'oGroupNode.Checked = True
            MyBase.Nodes.Add(oGroupNode)

            For Each oCentro In oMissingGroupOrders.GroupBy(Function(x) x.customer.Guid).Select(Function(y) y.First.customer)
                oCentroNode = New TreeNode(oCentro.ref)
                oCentroNode.Tag = oCentro
                'oCentroNode.Checked = True
                oGroupNode.Nodes.Add(oCentroNode)

                For Each oOrder In oMissingGroupOrders.Where(Function(x) x.customer.Equals(oCentro))
                    oCentroNode.Nodes.Add(OrderNode(oOrder))
                Next
            Next
            'oGroupNode.Expand()
        End If

        For Each oNodeGroup In MyBase.Nodes
            oNodeGroup.checked = True
            For Each oCentroNode In oNodeGroup.nodes
                oCentroNode.Checked = True
                For Each oOrderNode In oCentroNode.Nodes
                    oOrderNode.checked = True
                Next
            Next
        Next

        _PreviousSelection = CheckedOrders()
        _AllowEvents = True
    End Sub

    Public Shadows Sub Load(oDeliveries As List(Of DTODelivery))
        _AllowEvents = False
        Dim oDisabledColor = Color.FromArgb(255, 80, 80, 80)
        For Each oGroupNode As TreeNode In MyBase.Nodes
            For Each oCentroNode As TreeNode In oGroupNode.Nodes
                For Each oOrderNode As TreeNode In oCentroNode.Nodes
                    If oDeliveries.Any(Function(x) matches(oOrderNode.Tag, x)) Then
                        oOrderNode.ForeColor = Color.Black
                        oCentroNode.ForeColor = Color.Black
                        oGroupNode.ForeColor = Color.Black
                    Else
                        oOrderNode.ForeColor = oDisabledColor
                        oCentroNode.ForeColor = oDisabledColor
                        oGroupNode.ForeColor = oDisabledColor
                    End If
                Next
            Next
        Next
        _AllowEvents = True
    End Sub

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



    Public Function CheckedOrders() As List(Of DTOPurchaseOrder)
        Dim retval = New List(Of DTOPurchaseOrder)
        For Each oGroupNode As TreeNode In MyBase.Nodes
            Debug.Print(oGroupNode.Text)
            For Each oCentroNode As TreeNode In oGroupNode.Nodes
                For Each oNode In oCentroNode.Nodes
                    If oNode.checked Then
                        'Dim oorder As DTOPurchaseOrder = oNode.tag
                        'If oorder.concept.StartsWith("62046601") Then Stop
                        retval.Add(oNode.tag)
                    End If
                Next
            Next
        Next
        Return retval
    End Function


    Private Function GroupCenters(oGroup As DTOECITransmGroup, oDeptOrders As List(Of DTOPurchaseOrder)) As List(Of DTOCustomer)
        Dim retval = oDeptOrders.Where(Function(x) DTOECITransmGroup.Belongs(oGroup, x)).
            GroupBy(Function(y) y.customer.Guid).
            Select(Function(z) z.First.customer).ToList

        retval = retval.OrderBy(Function(x) x.ref).ToList
        Return retval
    End Function

    Private Function CentroOrders(oGroup As DTOECITransmGroup, oCentro As DTOCustomer, oOrders As List(Of DTOPurchaseOrder)) As List(Of DTOPurchaseOrder)
        'If oOrders.Where(Function(x) x.concept.StartsWith("62046601")).ToList.Count > 1 Then Stop
        Dim oGroupOrders = oOrders.Where(Function(x) DTOECITransmGroup.Belongs(oGroup, x)).ToList
        Dim retval = oGroupOrders.Where(Function(x) x.customer.Equals(oCentro)).ToList()
        Return retval
    End Function

    Private Function GroupNode(oGroup As DTOECITransmGroup) As TreeNode
        Dim retval As New TreeNode(oGroup.Nom)
        retval.Tag = oGroup
        Return retval
    End Function

    Private Function CentroNode(oCentro As DTOCustomer) As TreeNode
        Dim retval As New TreeNode(oCentro.ref)
        retval.Tag = oCentro
        Return retval
    End Function

    Private Function OrderNode(oOrder As DTOPurchaseOrder) As TreeNode
        Dim sCaption = orderCaption(oOrder)
        Dim retval As New TreeNode(sCaption)
        retval.Tag = oOrder
        retval.StateImageIndex = -1
        Return retval
    End Function

    Private Function DeliveryNode(oDelivery As DTODelivery) As TreeNode
        Dim oOrder = oDelivery.items.First.purchaseOrderItem.purchaseOrder
        Dim sCaption = orderCaption(oOrder)
        Dim retval As New TreeNode(sCaption)
        retval.Tag = oDelivery
        Return retval
    End Function

    Private Function orderCaption(oOrder As DTOPurchaseOrder) As String
        Dim retval = oOrder.concept
        Dim trimIdx = retval.IndexOf("/prov.")
        If trimIdx > 0 Then retval = retval.Substring(0, trimIdx - 1)
        Return retval
    End Function


    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oNode As TreeNode = MyBase.SelectedNode

        If oNode IsNot Nothing Then
            If oNode.Tag IsNot Nothing Then
                If TypeOf oNode.Tag Is DTOECITransmGroup Then
                    Dim oGroup As DTOECITransmGroup = oNode.Tag
                    Dim oMenu_EciTransmGroup As New Menu_EciTransmGroup(oGroup)
                    oContextMenu.Items.AddRange(oMenu_EciTransmGroup.Range)
                    oContextMenu.Items.Add("-")
                    oContextMenu.Items.Add("desel·lecciona-ho tot", Nothing, AddressOf SelectNone)
                ElseIf TypeOf oNode.Tag Is DTOCustomer Then
                    Dim oCustomer As DTOCustomer = oNode.Tag
                    Dim oMenu_Contact As New Menu_Contact(oCustomer)
                    oContextMenu.Items.AddRange(oMenu_Contact.Range)
                ElseIf TypeOf oNode.Tag Is DTOPurchaseOrder Then
                    Dim oOrder As DTOPurchaseOrder = oNode.Tag
                    Dim oMenu_Order As New Menu_PurchaseOrder(oOrder)
                    oContextMenu.Items.AddRange(oMenu_Order.Range)
                End If
            End If
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SelectNone()
        _AllowEvents = False
        For Each oNode As TreeNode In MyBase.Nodes
            oNode.Checked = False
        Next
        _AllowEvents = True
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub


    Protected Overrides Sub OnAfterCheck(ByVal e As System.Windows.Forms.TreeViewEventArgs)
        If _AllowEvents Then
            _AllowEvents = False
            MyBase.OnAfterCheck(e)
            Application.DoEvents()

            If TypeOf e.Node.Tag Is DTOECITransmGroup Then
                If e.Node.Checked = False Then
                    For Each oNode As TreeNode In e.Node.Nodes
                        'oNode.Checked = False
                    Next
                End If
            End If

            Dim exs As New List(Of Exception)
            Dim oOrders = CheckedOrders()
            Dim sPreviousSelectionJson = JsonHelper.Serialize(_PreviousSelection, exs)
            Dim scurrentSelectionJson = JsonHelper.Serialize(oOrders, exs)
            If sPreviousSelectionJson <> scurrentSelectionJson Then
                _PreviousSelection = oOrders
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            End If

            _AllowEvents = True
        End If
    End Sub


    Private Sub Xl_EciPlatformDeliveries_NodeSelectedChanged(sender As Object, e As MatEventArgs) Handles Me.NodeSelectedChanged
        If _AllowEvents Then
            SetContextMenu()
            If TypeOf e.Argument Is DTOPurchaseOrder Then
                RaiseEvent onOrderSelected(Me, e)
            End If
        End If
    End Sub
End Class
