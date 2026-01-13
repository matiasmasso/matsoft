Public Class DTORatio
    Property Nom As String
    Property Dsc As String
    Property Value As Decimal

    Property Formato As Formatos

    Public Enum Formatos
        Eur
        Ratio
        Percent
    End Enum
End Class
