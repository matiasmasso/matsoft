Public Class DTOCustomerCataleg
    Property UTC As String
    Property Customer As String
    Property Items As List(Of DTOCustomerCatalegItem)

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New
        _UTC = TextHelper.VbFormat(DateTime.Now.ToUniversalTime, "u")
        _Customer = oCustomer.FullNom
        _Items = New List(Of DTOCustomerCatalegItem)
    End Sub
End Class
Public Class DTOCustomerCatalegItem
    Property SkuGuid As Guid
    Property SkuId As Integer
    Property Ref As String
    Property Ean As String
    Property Brand As String
    Property Category As String
    Property Name As String
    Property Cost As Decimal
    Property RRPP As Decimal
    Property Image As String

End Class
