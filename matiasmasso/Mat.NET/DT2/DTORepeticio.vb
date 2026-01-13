Public Class DTORepeticio
    Inherits DTOContact

    Property Qty As Integer
    Property Orders As Integer
    Property Eur As Decimal

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function All(items As List(Of DTORepeticio), iOrderCount As Integer) As List(Of DTORepeticio)
        Dim retval As List(Of DTORepeticio) = items.Where(Function(x) x.Orders = iOrderCount)
        Return retval
    End Function

End Class
