Public Class DTOTax
    Inherits DTOBaseGuid

    Property Codi As DTOTax.Codis
    Property Fch As Date
    Property Tipus As Decimal

    Public Enum Codis
        Exempt
        Iva_Standard
        Iva_Reduit
        Iva_SuperReduit
        Recarrec_Equivalencia_Standard
        Recarrec_Equivalencia_Reduit
        Recarrec_Equivalencia_SuperReduit
        Irpf_Professionals_Standard
        Irpf_Professionals_Reduit
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub


    Shared Function Closest(Optional DtFch As Date = Nothing) As List(Of DTOTax)
        If DtFch = Nothing Then DtFch = Today
        Dim pastTaxes = DTOApp.Current.Taxes.Where(Function(x) x.Fch <= DtFch).OrderByDescending(Function(y) y.Fch)
        Dim retval = pastTaxes.GroupBy(Function(x) x.Codi).Select(Function(y) y.First).ToList
        Return retval
    End Function


    Shared Function Closest(oCodi As DTOTax.Codis, Optional DtFch As Date = Nothing) As DTOTax
        If DtFch = Nothing Then DtFch = Today
        Dim oTaxes = Closest(DtFch)
        Dim retval = oTaxes.FirstOrDefault(Function(x) x.Codi = oCodi)
        Return retval
    End Function

    Shared Function ClosestTipus(oCodi As DTOTax.Codis, Optional DtFch As Date = Nothing) As Decimal
        Dim retval As Decimal
        Dim oTax = DTOTax.Closest(oCodi, DtFch)
        If oTax IsNot Nothing Then
            retval = oTax.Tipus
        End If
        Return retval
    End Function

    Shared Function ReqForIvaCod(oIvaCod As DTOTax.Codis) As DTOTax.Codis
        Dim retval As DTOTax.Codis = DTOTax.Codis.Exempt
        Select Case oIvaCod
            Case DTOTax.Codis.Iva_Standard
                retval = DTOTax.Codis.Recarrec_Equivalencia_Standard
            Case DTOTax.Codis.Recarrec_Equivalencia_Reduit
                retval = DTOTax.Codis.Recarrec_Equivalencia_Reduit
            Case DTOTax.Codis.Recarrec_Equivalencia_SuperReduit
                retval = DTOTax.Codis.Recarrec_Equivalencia_SuperReduit
        End Select
        Return retval
    End Function


    Shared Function Nom(oCodi As DTOTax.Codis, Optional ByVal oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.Current.Lang
        Dim sRetVal As String = ""
        Select Case oCodi
            Case DTOTax.Codis.Iva_Standard
                sRetVal = oLang.Tradueix("IVA", "IVA", "VAT")
            Case DTOTax.Codis.Iva_Reduit
                sRetVal = oLang.Tradueix("IVA reducido", "IVA reduit", "short VAT")
            Case DTOTax.Codis.Iva_SuperReduit
                sRetVal = oLang.Tradueix("IVA super reducido", "IVA super reduit", "super short VAT")
            Case DTOTax.Codis.Recarrec_Equivalencia_Standard, DTOTax.Codis.Recarrec_Equivalencia_Reduit, DTOTax.Codis.Recarrec_Equivalencia_SuperReduit
                sRetVal = oLang.Tradueix("recargo de equivalencia", "recarrec d'equivalencia", "additional VAT")
            Case DTOTax.Codis.Irpf_Professionals_Standard, DTOTax.Codis.Irpf_Professionals_Reduit
                sRetVal = oLang.Tradueix("retención IRPF", "retenció IRPF", "IRPF income tax deduction")
        End Select
        Return sRetVal
    End Function

    Shared Function CtaCod(ByVal oTaxCod As DTOTax.Codis) As DTOPgcPlan.Ctas
        Dim retval As DTOPgcPlan.Ctas = DTOTax.Codis.Exempt
        Select Case oTaxCod
            Case DTOTax.Codis.Iva_Standard
                retval = DTOPgcPlan.Ctas.IvaRepercutitNacional
            Case DTOTax.Codis.Iva_Reduit
                retval = DTOPgcPlan.Ctas.IvaReduit
            Case DTOTax.Codis.Iva_SuperReduit
                retval = DTOPgcPlan.Ctas.IvaSuperReduit
            Case DTOTax.Codis.Recarrec_Equivalencia_Standard
                retval = DTOPgcPlan.Ctas.IvaRecarrecEquivalencia
            Case DTOTax.Codis.Recarrec_Equivalencia_Reduit
                retval = DTOPgcPlan.Ctas.IvaRecarrecReduit
            Case DTOTax.Codis.Recarrec_Equivalencia_SuperReduit
                retval = DTOPgcPlan.Ctas.IvaRecarrecSuperReduit
        End Select
        Return retval
    End Function

    Shared Function Quota(oBase As DTOAmt, oTax As DTOTax) As DTOAmt
        Dim retval As DTOAmt = oBase.Clone.Percent(oTax.Tipus)
        Return retval
    End Function
End Class
