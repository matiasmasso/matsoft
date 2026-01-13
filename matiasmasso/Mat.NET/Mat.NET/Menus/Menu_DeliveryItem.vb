Public Class Menu_DeliveryItem
    Inherits Menu_Base

    Private _DeliveryItem As DTODeliveryItem

    Public Sub New(ByVal oDeliveryItem As DTODeliveryItem)
        MyBase.New()
        _DeliveryItem = oDeliveryItem
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Sku(), _
        MenuItem_Pdc(), _
        MenuItem_Alb(), _
        MenuItem_Fra()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Sku() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Article"
        AddHandler oMenuItem.Click, AddressOf Do_Sku
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comanda"
        AddHandler oMenuItem.Click, AddressOf Do_Pdc
        Return oMenuItem
    End Function

    Private Function MenuItem_Alb() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Albarà"
        AddHandler oMenuItem.Click, AddressOf Do_Alb
        Return oMenuItem
    End Function

    Private Function MenuItem_Fra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Factura"
        If _DeliveryItem.Delivery Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = _DeliveryItem.Delivery.Invoice IsNot Nothing
        End If
        AddHandler oMenuItem.Click, AddressOf Do_Fra
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Sku(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSku As DTOProductSku = _DeliveryItem.PurchaseOrderItem.Sku
        Dim oFrm As New Frm_ProductSku(oSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Pdc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oPurchaseOrder As DTOPurchaseOrder = _DeliveryItem.PurchaseOrderItem.PurchaseOrder
        Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Alb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oAlb As New Alb(_DeliveryItem.Delivery.Guid)
        Dim oFrm As New Frm_AlbNew2(oAlb)
        AddHandler oFrm.AfterUpdate, AddressOf MyBase.RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Fra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oInvoice As DTOInvoice = _DeliveryItem.Delivery.Invoice
        Dim oFrm As New frm_Invoice(oInvoice)
        oFrm.show()
    End Sub



End Class

