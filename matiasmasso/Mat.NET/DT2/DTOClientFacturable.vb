Public Class DTOClientFacturable
    Property Customer As DTOCustomer
    Property Invoices As List(Of DTOInvoice)
    Property AlbaransPerFacturar As List(Of DTODelivery)
    Property Facturable As Boolean


    Public Sub New(Optional ByVal oCustomer As DTOCustomer = Nothing)
        MyBase.New
        _Customer = oCustomer
        _Invoices = New List(Of DTOInvoice)
        _AlbaransPerFacturar = New List(Of DTODelivery)
    End Sub

    Public Function Total() As DTOAmt
        Dim DcEur As Decimal = _Invoices.SelectMany(Function(x) x.Deliveries).Sum(Function(y) y.Import.Eur)
        Dim retval = DTOAmt.Factory(DcEur)
        Return retval
    End Function
End Class
