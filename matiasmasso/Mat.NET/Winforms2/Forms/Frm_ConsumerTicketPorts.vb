Public Class Frm_ConsumerTicketPorts
    Private _value As DTOConsumerTicket
    Private _sku As DTOProductSku
    Private _PurchaseOrderItem As DTOPurchaseOrderItem
    Private _DeliveryItem As DTODeliveryItem
    Private _exportCod As DTOInvoice.ExportCods
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOConsumerTicket)
        MyBase.New
        InitializeComponent()
        _value = value
        _sku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.Transport)
    End Sub

    Private Async Sub Frm_ConsumerTicketPorts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        Dim oPurchaseOrder = Await FEB.PurchaseOrder.Find(_value.PurchaseOrder.Guid, exs)
        If exs.Count = 0 Then
            Dim oDelivery = Await FEB.Delivery.Find(_value.Delivery.Guid, exs)
            UIHelper.ToggleProggressBar(PanelButtons, False)
            If exs.Count = 0 Then
                Dim oPurchaseOrderItem = oPurchaseOrder.Items.FirstOrDefault(Function(x) x.Sku.Equals(_sku))
                If oPurchaseOrderItem IsNot Nothing Then
                    _PurchaseOrderItem = oPurchaseOrderItem
                    _DeliveryItem = oDelivery.Items.FirstOrDefault(Function(x) x.PurchaseOrderItem.Equals(oPurchaseOrderItem))
                End If
                _exportCod = oDelivery.ExportCod
                If _exportCod = DTOInvoice.ExportCods.nacional Then
                    CheckBoxVat.Checked = True
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs)
        End If
        _AllowEvents = True
    End Sub

    Private Sub Xl_AmountCurTot_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_AmountCurTot.AfterUpdate
        If _AllowEvents Then
            Dim oTot = Xl_AmountCurTot.Amt
            If _exportCod = DTOInvoice.ExportCods.nacional Then
                Dim DcVatType As Decimal = DTOTax.closest(DTOTax.Codis.iva_Standard, DTO.GlobalVariables.Today()).tipus
                CheckBoxVat.Text = String.Format("Iva {0}%", DcVatType)
                Dim factor = 1 + DcVatType / 100
                Dim base = oTot.DividedBy(factor)
                Xl_AmountCurBase.Amt = base
                Xl_AmountCurVat.Amt = oTot.Substract(base)
            Else
                Xl_AmountCurBase.Amt = oTot
            End If
        End If
        ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim exs As New List(Of Exception)
        Dim oBase = Xl_AmountCurBase.Amt
        UIHelper.ToggleProggressBar(PanelButtons, True)

        If _PurchaseOrderItem Is Nothing Then
            _PurchaseOrderItem = DTOPurchaseOrderItem.Factory(_value.PurchaseOrder, _sku, 1, oBase, 0)
            _value.PurchaseOrder.Items.Add(_PurchaseOrderItem)
        Else
            _PurchaseOrderItem.Price = oBase
        End If

        If _DeliveryItem Is Nothing Then
            _DeliveryItem = DTODeliveryItem.Factory(_PurchaseOrderItem, 1, oBase, 0)
            _value.Delivery.Items.Add(_DeliveryItem)
        Else
            _DeliveryItem.Price = oBase
        End If

        Await FEB.PurchaseOrder.Update(exs, _value.PurchaseOrder)
        If exs.Count = 0 Then
            Await FEB.Delivery.Update(exs, _value.Delivery)
            If exs.Count = 0 Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class