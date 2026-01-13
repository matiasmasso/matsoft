Public Class DTOOnlineVendor
    Inherits DTOBaseGuid

    Property Nom As String
    Property Url As String
    Property LandingPage As String
    Property LogoUrl As String
    <JsonIgnore> Property Logo As Image

    Property Customer As DTOCustomer
    Property IsActive As Boolean

    Property Obs As String

    Property Stocks As List(Of Stock)

    Public Enum wellknowns
        None
        Glider
        Algatec
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Class Stock
        Property Sku As DTOProductSku
        Property Qty As Integer
        Property Price As Decimal
        Property Fch As DateTime
    End Class

    Shared Function wellknown(value As wellknowns) As DTOOnlineVendor
        Dim retval As DTOOnlineVendor = Nothing
        Select Case value
            Case wellknowns.Glider
                retval = New DTOOnlineVendor(New Guid("C9B33742-A405-4804-B4F0-7461E07F0C6C"))
            Case wellknowns.Algatec
                retval = New DTOOnlineVendor(New Guid("976C81F8-FB7C-4343-B802-A597E2301359"))
        End Select
        Return retval
    End Function
End Class
