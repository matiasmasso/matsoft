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
        Dim retval As Decimal = _Mesos.Sum(Function(x) x.Eur)
        Return retval
    End Function


    Public Sub New(oProduct As DTOProduct, ByVal oYearMonths As List(Of DTOYearMonth))
        _Product = oProduct
        _Mesos = oYearMonths
    End Sub

End Class

