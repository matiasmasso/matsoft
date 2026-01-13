Public Class Menu_PurchaseOrderItem

    Private _PurchaseOrderItems As List(Of DTOPurchaseOrderItem)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oPurchaseOrderItem As DTOPurchaseOrderItem)
        MyBase.New()
        _PurchaseOrderItems = New List(Of DTOPurchaseOrderItem)
        _PurchaseOrderItems.Add(oPurchaseOrderItem)
    End Sub

    Public Sub New(ByVal oPurchaseOrderItems As List(Of DTOPurchaseOrderItem))
        MyBase.New()
        _PurchaseOrderItems = oPurchaseOrderItems
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
                MenuItem_Order(),
                MenuItem_Preu(),
                MenuItem_ShowSku(),
                MenuItem_Customer(),
                MenuItem_Sortides()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================
    Private Function MenuItem_Order() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "comanda"
        If _PurchaseOrderItems.First.PurchaseOrder Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = True
            Dim oOrders As New List(Of DTOPurchaseOrder)
            oOrders.Add(_PurchaseOrderItems.First.PurchaseOrder)
            Dim oMenu As New Menu_Pdc(oOrders)
            oMenuItem.DropDownItems.AddRange(oMenu.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Preu() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "preu"
        If _PurchaseOrderItems.Count > 1 Then
            oMenuItem.Enabled = False
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Preu
        Return oMenuItem
    End Function

    Private Function MenuItem_ShowSku() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "article"
        If _PurchaseOrderItems.First.Sku Is Nothing Then
            oMenuItem.Enabled = False
        ElseIf _PurchaseOrderItems.Count > 1 Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = True
            Dim oProductSku As DTOProductSku = _PurchaseOrderItems.First.Sku
            Dim oMenu_ProductSku As New Menu_ProductSku(oProductSku)
            oMenuItem.DropDownItems.AddRange(oMenu_ProductSku.Range)
        End If
        Return oMenuItem
    End Function




    Private Function MenuItem_Customer() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "client"
        If _PurchaseOrderItems.First.PurchaseOrder Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = True
            AddHandler oMenuItem.MouseEnter, AddressOf CustomerDropdown
        End If
        Return oMenuItem
    End Function

    Private Sub CustomerDropdown(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender
        If oMenuItem.DropDownItems.Count = 0 Then
            Dim oMenu As New Menu_Contact(_PurchaseOrderItems.First.PurchaseOrder.Contact)
            oMenuItem.DropDownItems.AddRange(oMenu.Range)
        End If
    End Sub

    Private Function MenuItem_Sortides() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "sortides"
        AddHandler oMenuItem.Click, AddressOf Do_Sortides
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_PurchaseOrderItem(_PurchaseOrderItem)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_Preu(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_SkuPreuJustification(_PurchaseOrderItems.First)
        oFrm.Show()
    End Sub

    Private Sub Do_Sortides()
        If _PurchaseOrderItems.Count > 0 Then
            Dim oFrm As New Frm_PurchaseOrderItems_Sortides(_PurchaseOrderItems.First)
            oFrm.Show()
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class

