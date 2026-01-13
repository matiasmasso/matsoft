Public Class DTOConsumerBasket
    Inherits DTOBaseGuid

    Property Site As Sites
    Property Fch As DateTime
    Property User As DTOUser
    Property Items As List(Of DTOConsumerBasketItem)

    Public Enum Sites
        NotSet
        MMO
        Britax
        Inglesina
        Thorley
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub
End Class

Public Class DTOConsumerBasketItem
    Property Qty As Integer
    Property Sku As DTOProductSku
    Property Price As DTOAmt
End Class
