Public Class DTOProductSkuExtended
    Inherits DTOProductSku

    Public Property UnitsInStock As Integer
    Public Property UnitsInClients As Integer
    Public Property UnitsInPot As Integer
    Public Property UnitsInProveidor As Integer
    Public Property UnitsInPrevisio As Integer
    Public Property Confirmed As Integer

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
