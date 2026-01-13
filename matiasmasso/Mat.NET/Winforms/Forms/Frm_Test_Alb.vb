Public Class Frm_Test_Alb
    Private _Delivery As DTODelivery

    Private Sub Frm_Test_Alb_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oCustomer As DTOCustomer = BLLCustomer.Find(New Guid("FCF3F90E-4CE5-4445-8E13-C50354FC56FE"))
        _Delivery = BLLDelivery.Factory(oCustomer, BLLSession.Current.User)
        Xl_DeliveryItems1.Load(_Delivery)
        SetTotals()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim oDelivery As DTODelivery = BLLDelivery.FromNum(19426)
        Dim oProveidor = BLLProveidor.WellKnown(DTOProveidor.WellKnown.Roemer)
        _Delivery = BLLDelivery.Factory(oProveidor, BLLSession.Current.User)

        BLLDelivery.Load(_Delivery)
        Xl_DeliveryItems1.Load(_Delivery)
    End Sub

    Private Sub Xl_DeliveryItems1_AfterUpdate(sender As Object, e As MatEventArgs) 
        SetTotals()
    End Sub

    Private Sub SetTotals()
        Dim items As List(Of DTODeliveryItem) = Xl_DeliveryItems1.Items
        Dim oBaseImponible = BLLDeliveryItems.BaseImponible(items)
        Dim oPaymentTerms As DTOPaymentTerms
        Select Case _Delivery.Cod
            Case DTOPurchaseOrder.Codis.Proveidor
                oPaymentTerms = _Delivery.Proveidor.PaymentTerms
            Case Else
                oPaymentTerms = _Delivery.Customer.PaymentTerms
        End Select
        Label1.Text = String.Format("{0} {1}", BLLDelivery.TotalsText(items, _Delivery.Contact, _Delivery.Fch), BLLPaymentTerms.Text(oPaymentTerms, _Delivery.Contact.Lang))
    End Sub
End Class