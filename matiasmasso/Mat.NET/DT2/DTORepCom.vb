Public Class DTORepCom
    Property Rep As DTORep
    Property Com As Decimal

    Property RepCustom As Boolean 'Si es True vol dir que el representant o la comisió han estat assignats manualment, i els processos de validació no l'haurien de sobreescriure

    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(oRep As DTORep, DcComisioPercent As Decimal)
        MyBase.New()
        _Rep = oRep
        _Com = DcComisioPercent
    End Sub

    Public Sub Clear()
        _Rep = Nothing
        _Com = 0
        _RepCustom = True
    End Sub

    Shared Shadows Function Equals(O1 As DTORepCom, O2 As DTORepCom) As Boolean
        Dim retval As Boolean
        If O1 Is Nothing Then
            If O2 Is Nothing Then retval = True
        Else
            If O2 IsNot Nothing Then
                If O1.Rep Is Nothing Then
                    If O2.Rep Is Nothing Then retval = True
                Else
                    retval = O1.Rep.Guid.Equals(O2.Rep.Guid) And O1.Com = O2.Com
                End If
            End If
        End If
        Return retval
    End Function

End Class
