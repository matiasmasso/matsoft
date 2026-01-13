Public Class DTOCliProductDto
    Inherits DTOBaseGuid

    Property Customer As DTOCustomer
    Property Product As DTOProduct
    Property Dto As Decimal

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class
