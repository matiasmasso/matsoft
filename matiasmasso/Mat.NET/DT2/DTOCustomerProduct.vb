Public Class DTOCustomerProduct
    Inherits DTOBaseGuid

    Property sku As DTOProductSku
    Property customer As DTOCustomer
    Property ref As String
    Property DUN14 As String
    Property dsc As String
    Property color As String
    Property fchFrom As Date
    Property fchTo As Date
    Property yearMonth As DTOYearMonth
    Property qty As Integer


    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Dun14OrDefault(oCustomerProduct As DTOCustomerProduct) As String
        Dim retval As String = ""
        If oCustomerProduct IsNot Nothing Then
            retval = oCustomerProduct.DUN14
        End If
        Return retval
    End Function
End Class
