Public Class DTOTaxBaseQuota
    Property Tax As DTOTax
    Property Base As DTOAmt

    Property Quota As DTOAmt

    Public Sub New(ByVal oTaxCodi As DTOTax.Codis, ByVal DcBase As Decimal, DcTipus As Decimal, Optional DcQuota As Decimal = 0)
        MyBase.New()
        _Tax = New DTOTax
        _Tax.Codi = oTaxCodi
        _Tax.Tipus = DcTipus
        _Base = DTOAmt.factory(DcBase)
        If DcQuota = 0 Then
            _Quota = _Base.Percent(_Tax.Tipus)
        Else
            _Quota = DTOAmt.factory(DcQuota)
        End If
    End Sub

    Public Sub New(ByVal oTax As DTOTax, ByVal oBase As DTOAmt)
        MyBase.New()
        _Tax = oTax
        _Base = oBase.Clone
        _Quota = _Base.Percent(_Tax.Tipus)
    End Sub

    Public Sub New()
        MyBase.New
        'needed for json serialization
    End Sub

    Public Sub AddBase(oBase As DTOAmt)
        _Base.Add(oBase)
        _Quota = _Base.Percent(_Tax.Tipus)
    End Sub
End Class
