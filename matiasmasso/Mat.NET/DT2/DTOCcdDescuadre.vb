Public Class DTOCcdDescuadre
    Property Cta As DTOPgcCta
    Property Contact As DTOContact
    Property Sdo As Decimal
    Property Cash As Decimal
    Property Pnd As Decimal

    Public Enum Rols
        Clients
        Proveidors
    End Enum

    Public ReadOnly Property Diff As Decimal
        Get
            Return 0
        End Get
    End Property
End Class
