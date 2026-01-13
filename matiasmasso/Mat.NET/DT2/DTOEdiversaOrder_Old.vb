Public Class DTOEdiversaOrder_Old
    Inherits DTOBaseGuid

    Property CustomerGLN As DTOEan
    Property Customer As DTOCustomer
    Property Fch As Date
    Property FchMin As Date
    Property FchLast As Date
    Property OrderNum As String
    Property Obs As String

    Property Emisor As DTOEdiversaContact
    Property Comprador As DTOEdiversaContact
    Property Receptor As DTOEdiversaContact
    Property Destinatari As DTOEdiversaContact
    Property Cur As DTOCur
    Property Items As List(Of DTOEdiversaOrderItem_Old)

    Property Amt As DTOAmt

    Property Exceptions As List(Of DTOEdiversaException)

    Public Sub New()
        MyBase.New
        _Items = New List(Of DTOEdiversaOrderItem_Old)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTOEdiversaOrderItem_Old)
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub
End Class


Public Class DTOEdiversaOrderItem_Old
    Property Parent As DTOEdiversaOrder_Old
    Property Qty As Integer
    Property Sku As DTOProductSku
    Property Ean As DTOEan
    Property Price As DTOAmt
    Property Dto As Decimal
    Property RefClient As String
    Property RefProveidor As String
    Property Exceptions As List(Of DTOEdiversaException)

    Public Sub New(oParent As DTOEdiversaOrder_Old)
        MyBase.New
        _Parent = oParent
        _Exceptions = New List(Of DTOEdiversaException)
    End Sub

End Class



