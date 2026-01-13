Public Class DTOBancTerm
    Inherits DTOBaseGuid
    Property banc As DTOBanc
    Property target As Targets
    Property fch As Date
    Property indexatAlEuribor As Boolean
    Property diferencial As Decimal
    Property minimDespesa As Decimal
    Property euriborValue As Decimal


    Public Enum Targets
        notSet
        anticips
        gestioCobrament
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Cost(ByRef oCsb As DTOCsb, oTerm As DTOBancTerm, Optional DtFch As Date = Nothing) As DTOAmt
        If DtFch = Nothing Then DtFch = Today
        Dim iDays As Integer = DateDiff(DateInterval.Day, DtFch, oCsb.Vto)

        Dim DcTipus As Decimal = oTerm.diferencial
        If oTerm.indexatAlEuribor Then
            DcTipus += oTerm.euriborValue
        End If
        Dim DcCost As Decimal = oCsb.Amt.eur * (DcTipus / 100) * (iDays / 360)
        If DcCost < oTerm.minimDespesa Then
            DcCost = oTerm.minimDespesa
        End If

        Dim retval As DTOAmt = DTOAmt.factory(DcCost)
        Return retval
    End Function
End Class
