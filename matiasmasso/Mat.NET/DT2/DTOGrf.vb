Public Class DTOGrf
    Property FchFrom As Date
    Property FchTo As Date
    Property DateInterval As DateInterval
    Property Items As List(Of DTOGrfItem)

End Class

Public Class DTOGrfItem
    Property Fch As Date
    Property Value As Decimal
End Class

Public Class DTOGrfMesValue

    Property Product As DTOProduct
    Property Color As Color
    Property Mesos As List(Of DTOYearMonth)
    Property Resto As Decimal

    Public Enum Codis
        Comandes
        Albarans
    End Enum

    Function Sum() As Decimal
        Dim retval As Decimal = _Mesos.Sum(Function(x) x.eur)
        Return retval
    End Function


    Public Sub New(oProduct As DTOProduct, ByVal oYearMonths As List(Of DTOYearMonth))
        _Product = oProduct
        _Mesos = oYearMonths
    End Sub

End Class