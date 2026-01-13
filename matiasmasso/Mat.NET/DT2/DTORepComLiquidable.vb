Public Class DTORepComLiquidable
    Inherits DTOBaseGuid

    Property rep As DTORep
    Property fra As DTOInvoice
    Property repLiq As DTORepLiq
    Property base As DTOAmt
    Property comisio As DTOAmt
    Property obs As String
    Property liquidable As Boolean

    Property items As List(Of DTODeliveryItem)

    Public Sub New()
        MyBase.New()
        _Liquidable = True
        _Items = New List(Of DTODeliveryItem)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTODeliveryItem)
    End Sub
End Class
