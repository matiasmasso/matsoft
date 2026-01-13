Public Class DTORepCertRetencio
    Property Rep As DTORep
    Property Fch As Date
    Property RepLiqs As List(Of DTORepLiq)
    Property Url As String


    Shared Function Year(oCert As DTORepCertRetencio) As Integer
        Dim retval As Integer = oCert.Fch.Year
        Return retval
    End Function

    Shared Function Quarter(oCert As DTORepCertRetencio) As Integer
        Dim retval As Integer = TimeHelper.Quarter(oCert.Fch)
        Return retval
    End Function

    Shared Function Title(oCert As DTORepCertRetencio) As String
        Dim oLang As DTOLang = oCert.Rep.Lang
        Dim sText As String = oLang.Tradueix("Certificado Trimestral Decimal Retenciones", "Certificat Trimestral Decimal Retencions", "Quarterly Tax Certificate")
        Dim retval As String = String.Format("{0} {1}.T{2}", sText, Year(oCert), Quarter(oCert))
        Return retval
    End Function

    Shared Function BaseImponible(oCert As DTORepCertRetencio) As DTOAmt
        Dim DcEur As Decimal = oCert.RepLiqs.Sum(Function(x) x.BaseImponible.Eur)
        Dim retval As DTOAmt = DTOAmt.Factory(DcEur)
        Return retval
    End Function

    Shared Function IVA(oCert As DTORepCertRetencio) As DTOAmt
        Dim DcEur As Decimal = oCert.RepLiqs.Sum(Function(x) DTORepLiq.GetIVAAmt(x).Eur)
        Dim retval As DTOAmt = DTOAmt.Factory(DcEur)
        Return retval
    End Function

    Shared Function IRPF(oCert As DTORepCertRetencio) As DTOAmt
        Dim DcEur As Decimal = oCert.RepLiqs.Sum(Function(x) DTORepLiq.GetIRPFAmt(x).Eur)
        Dim retval As DTOAmt = DTOAmt.Factory(DcEur)
        Return retval
    End Function

    Shared Function Liquid(oCert As DTORepCertRetencio) As DTOAmt
        Dim DcEur As Decimal = oCert.RepLiqs.Sum(Function(x) DTORepLiq.GetLiquid(x).Eur)
        Dim retval As DTOAmt = DTOAmt.Factory(DcEur)
        Return retval
    End Function

    Function GetUrl() As String
        Return GetUrl(_Rep, _Fch)
    End Function

    Shared Function GetUrl(oRep As DTORep, DtFch As Date) As String
        Dim oJson As New MatJSonObject()
        oJson.AddValuePair("Guid", oRep.Guid.ToString())
        oJson.AddValuePair("Year", DtFch.Year)
        oJson.AddValuePair("Quarter", TimeHelper.Quarter(DtFch))
        Dim sBase64 As String = oJson.ToBase64

        Dim retval As String = DTOWebDomain.Default(True).Url("doc", DTODocFile.Cods.repcertretencio, sBase64)
        Return retval
    End Function


End Class
