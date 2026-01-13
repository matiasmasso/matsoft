Public Class DTOBaseQuota
    Property Source As Object
    Property Base As DTOAmt
    Property Quota As DTOAmt
    Property Tipus As Decimal

    Public Sub New(oBase As DTOAmt, Optional DcTipus As Decimal = 0, Optional oQuota As DTOAmt = Nothing)
        MyBase.New
        _Base = oBase
        If DcTipus <> 0 Then
            _Tipus = DcTipus
            If oQuota Is Nothing Then
                _Quota = oBase.Percent(DcTipus)
            Else
                _Quota = oQuota
            End If
        End If
    End Sub

    Public Sub CalcTipus()
        If _Base Is Nothing Then
            _Tipus = 0
        ElseIf _base.Eur = 0 Then
            _Tipus = 0
        Else
            _Tipus = 100 * Math.Round(_Quota.Eur / _Base.Eur, _Base.Cur.Decimals, MidpointRounding.AwayFromZero)
        End If
    End Sub
End Class
