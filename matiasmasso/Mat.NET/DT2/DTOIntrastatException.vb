Public Class DTOIntrastatException
    Property Codi As Codis
    Property Tag As Object

    Enum Codis
        NotSet
        CodiMercancia
        Weight
        Amount
    End Enum

    Public Sub New(oCodi As Codis, oTag As Object)
        _Codi = oCodi
        _Tag = oTag
    End Sub
End Class