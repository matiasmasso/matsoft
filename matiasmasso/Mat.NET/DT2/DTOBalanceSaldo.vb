Public Class DTOBalanceSaldo
    Inherits DTOPgcCta
    Property CurrentDeb As Decimal
    Property CurrentHab As Decimal
    Property PreviousDeb As Decimal
    Property PreviousHab As Decimal
    Property Contact As DTOContact

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Function IsDeutor() As Boolean
        Return CurrentDeb > CurrentHab
    End Function

    Public Function IsCreditor() As Boolean
        Return CurrentHab > CurrentDeb
    End Function
End Class
