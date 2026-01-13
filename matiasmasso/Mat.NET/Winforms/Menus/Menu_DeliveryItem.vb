Public Class Menu_DeliveryItem
    Inherits Menu_Base

    Private _DeliveryItem As DTODeliveryItem

    Public Sub New(ByVal oDeliveryItem As DTODeliveryItem)
        MyBase.New()
        _DeliveryItem = oDeliveryItem
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Sku())
        MyBase.AddMenuItem(MenuItem_Pdc())
        MyBase.AddMenuItem(MenuItem_Alb())
        MyBase.AddMenuItem(MenuItem_Fra())
    End Sub

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Sku() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Article"
        Dim oMenu As New Menu_ProductSku(_DeliveryItem.Sku)
        oMenuItem.DropDownItems.AddRange(oMenu.Range)
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

    Private Async Sub Do_Pdc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oPurchaseOrder As DTOPurchaseOrder = _DeliveryItem.PurchaseOrderItem.PurchaseOrder
        If oPurchaseOrder Is Nothing Then
            Dim oDelivery As DTODelivery = _DeliveryItem.Delivery
            If FEB2.Delivery.Load(oDelivery, exs) Then
                oPurchaseOrder = _DeliveryItem.PurchaseOrderItem.PurchaseOrder
            Else
                UIHelper.WarnError(exs)
            End If
        End If

        If FEB2.PurchaseOrder.Load(exs, oPurchaseOrder, GlobalVariables.Emp.Mgz) Then
            If oPurchaseOrder.Cod = DTOPurchaseOrder.Codis.NotSet Then FEB2.PurchaseOrder.Load(exs, oPurchaseOrder, GlobalVariables.Emp.Mgz)
            Select Case oPurchaseOrder.Cod
                Case DTOPurchaseOrder.Codis.Client
                    If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder(oPurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case DTOPurchaseOrder.Codis.Proveidor
                    If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oPurchaseOrder.Contact, DTOAlbBloqueig.Codis.PDC, exs) Then
                        Dim oFrm As New Frm_PurchaseOrder_Proveidor(oPurchaseOrder)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Else
                        UIHelper.WarnError(exs)
                    End If
                Case Else
                    MsgBox("nomes implementades les comandes de client o proveidor", MsgBoxStyle.Exclamation)
            End Select
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Async Sub Do_Alb(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDelivery = _DeliveryItem.delivery
        Dim exs As New List(Of Exception)
        If oDelivery.Contact Is Nothing Then FEB2.Delivery.Load(oDelivery, exs)
        If Await FEB2.AlbBloqueig.BloqueigStart(Current.Session.User, oDelivery.Contact, DTOAlbBloqueig.Codis.ALB, exs) Then
            Dim oFrm As New Frm_AlbNew2(oDelivery)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Fra(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oInvoice As DTOInvoice = _DeliveryItem.Delivery.Invoice
        Dim oFrm As New Frm_Invoice(oInvoice)
        oFrm.show()
    End Sub



End Class

