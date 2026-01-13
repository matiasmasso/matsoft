Public Class IVALiquidacio
    Shared Function Factory(oExercici As DTOExercici, iMonth As Integer) As DTOIVALiquidacio
        Dim retval = DTOIVALiquidacio.Factory(oExercici, iMonth)
        Dim oBookFras = BEBL.BookFras.All(DTOBookFra.Modes.All, oExercici, iMonth)
        Dim oInvoices = BEBL.Invoices.All(oExercici, iMonth)
        Dim oCcas = BEBL.Ccas.All(oExercici, OnlyIvaRelateds:=True)

        retval.cca = oCcas.FirstOrDefault(Function(x) x.Ccd = DTOCca.CcdEnum.IVA AndAlso DTOYearMonth.HasFch(retval.YearMonth, x.Fch))

        AddIvaRepercutit(retval, oInvoices, oCcas)
        AddIntraComunitari(retval, oBookFras, oCcas)
        AddRecarrecEquivalencia(retval, oInvoices, oCcas)
        AddSoportatNacional(retval, oBookFras, oCcas)
        AddImportacions(retval, oBookFras, oCcas)

        Return retval
    End Function



    Private Shared Sub AddSoportatNacional(ByRef oLiquidacio As DTOIVALiquidacio, oBookfras As List(Of DTOBookFra), oCcas As List(Of DTOCca))
        Dim oBaseQuotas = oBookfras.
            SelectMany(Function(x) x.IvaBaseQuotas).
            Where(Function(y) y.Tipus > 0).
            ToList


        Dim oBase = DTOAmt.Factory(oBaseQuotas.Sum(Function(x) x.baseImponible.Eur))
        Dim oQuota = DTOAmt.Factory(oBaseQuotas.Sum(Function(x) x.Quota.Eur))
        Dim oItem = DTOIVALiquidacio.Item.Factory(DTOIVALiquidacio.Item.Cods.SoportatNacional, oBase, , oQuota)
        oItem.Saldo = Saldo(oCcas, oLiquidacio.YearMonth, DTOPgcPlan.Ctas.IvaSoportatNacional)
        oLiquidacio.Items.Add(oItem)
    End Sub

    Private Shared Sub AddIvaRepercutit(ByRef oLiquidacio As DTOIVALiquidacio, oInvoices As List(Of DTOInvoice), oCcas As List(Of DTOCca))
        Dim oAllBaseQuotas As List(Of DTOTaxBaseQuota) = oInvoices.SelectMany(Function(x) x.IvaBaseQuotas).ToList

        Dim oBaseQuotas = oAllBaseQuotas.
            Where(Function(x) x.tax.codi = DTOTax.Codis.iva_Standard Or x.tax.codi = DTOTax.Codis.iva_Reduit Or x.tax.codi = DTOTax.Codis.iva_SuperReduit).
            GroupBy(Function(g) New With {Key g.tax.codi, Key g.tax.tipus}).
            Select(Function(group) New With {.tax = New DTOTax With {.codi = group.Key.codi, .tipus = group.Key.tipus}, .baseImponible = group.Sum(Function(x) x.baseImponible.Eur), .Quota = group.Sum(Function(y) y.quota.Eur)}).ToList

        For Each oBaseQuota In oBaseQuotas
            Dim oItem = DTOIVALiquidacio.Item.Factory(DTOIVALiquidacio.Item.Cods.Repercutit, DTOAmt.Factory(oBaseQuota.baseImponible), oBaseQuota.tax.tipus, DTOAmt.Factory(oBaseQuota.Quota))
            oItem.Saldo = Saldo(oCcas, oLiquidacio.YearMonth, DTOPgcPlan.Ctas.IvaRepercutitNacional)
            oLiquidacio.Items.Add(oItem)
        Next

    End Sub

    Private Shared Sub AddRecarrecEquivalencia(ByRef oLiquidacio As DTOIVALiquidacio, oInvoices As List(Of DTOInvoice), oCcas As List(Of DTOCca))
        Dim oAllBaseQuotas As List(Of DTOTaxBaseQuota) = oInvoices.SelectMany(Function(x) x.IvaBaseQuotas).ToList
        Dim oBaseQuotas = oAllBaseQuotas.
            Where(Function(x) x.tax.codi = DTOTax.Codis.recarrec_Equivalencia_Standard Or x.tax.codi = DTOTax.Codis.recarrec_Equivalencia_Reduit Or x.tax.codi = DTOTax.Codis.recarrec_Equivalencia_SuperReduit).
            GroupBy(Function(g) New With {Key g.tax.codi, Key g.tax.tipus}).
            Select(Function(group) New With {.tax = New DTOTax With {.codi = group.Key.codi, .tipus = group.Key.tipus}, .BaseImponible = group.Sum(Function(x) x.baseImponible.Eur), .Quota = group.Sum(Function(y) y.quota.Eur)}).ToList

        For Each oBaseQuota In oBaseQuotas
            Dim oItem = DTOIVALiquidacio.Item.Factory(DTOIVALiquidacio.Item.Cods.RecarrecEquivalencia, DTOAmt.Factory(oBaseQuota.BaseImponible), oBaseQuota.tax.tipus, DTOAmt.Factory(oBaseQuota.Quota))
            oItem.Saldo = Saldo(oCcas, oLiquidacio.YearMonth, DTOPgcPlan.Ctas.IvaRecarrecEquivalencia)
            oLiquidacio.Items.Add(oItem)
        Next
    End Sub

    Private Shared Sub AddIntraComunitari(ByRef oLiquidacio As DTOIVALiquidacio, oBookfras As List(Of DTOBookFra), oCcas As List(Of DTOCca))
        Dim oBaseQuotas = oBookfras.
            Where(Function(y) y.ClaveExenta = "E5").
            SelectMany(Function(x) x.IvaBaseQuotas).
            Where(Function(z) z.Tipus = 0).
            ToList

        Dim oTax As DTOTax = DTOTax.closest(DTOTax.Codis.iva_Standard, DTO.GlobalVariables.Today())
        Dim DcBaseIntraComunitaria = oBaseQuotas.Sum(Function(x) x.baseImponible.Eur)
        Dim DcQuotaIntraComunitaria = oBaseQuotas.Sum(Function(x) x.baseImponible.percent(oTax.tipus).Eur)
        Dim oItem = DTOIVALiquidacio.Item.Factory(DTOIVALiquidacio.Item.Cods.IntraComunitari, DTOAmt.Factory(DcBaseIntraComunitaria), oTax.Tipus, DTOAmt.Factory(DcQuotaIntraComunitaria))
        oItem.Saldo = Saldo(oCcas, oLiquidacio.YearMonth, DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari)
        oLiquidacio.Items.Add(oItem)
    End Sub

    Private Shared Sub AddImportacions(ByRef oLiquidacio As DTOIVALiquidacio, oBookfras As List(Of DTOBookFra), oCcas As List(Of DTOCca))
        Dim oBaseQuotas = oBookfras.
            Where(Function(y) y.ClaveExenta = "E2").
            SelectMany(Function(x) x.IvaBaseQuotas).
            Where(Function(z) z.Tipus = 0).
            ToList

        Dim oTax As DTOTax = DTOTax.Closest(DTOTax.Codis.Iva_Standard, oLiquidacio.Fch)
        Dim DcBaseImportacions As Decimal = oBaseQuotas.Sum(Function(x) x.baseImponible.Eur)
        Dim DcQuotaImportacions As Decimal = Math.Round(DcBaseImportacions * oTax.Tipus / 100, 2, MidpointRounding.AwayFromZero)
        Dim oItem = DTOIVALiquidacio.Item.Factory(DTOIVALiquidacio.Item.Cods.Importacions, DTOAmt.Factory(DcBaseImportacions), , DTOAmt.Factory(DcQuotaImportacions))
        oItem.Saldo = Saldo(oCcas, oLiquidacio.YearMonth, DTOPgcPlan.Ctas.IvaSoportatImportacio)
        oLiquidacio.Items.Add(oItem)
    End Sub


    Private Shared Function Saldo(oCcas As List(Of DTOCca), oYearMonth As DTOYearMonth, oCtaCod As DTOPgcPlan.Ctas) As DTOAmt
        Dim retval As Decimal
        Dim oCcbs As List(Of DTOCcb) = Ccbs(oCcas, oYearMonth, oCtaCod)

        If oCcbs.Count > 0 Then
            Dim oCta As DTOPgcCta = oCcbs.First.Cta
            Dim debe As Decimal = oCcbs.Where(Function(x) x.Dh = DTOCcb.DhEnum.Debe).Sum(Function(y) y.Amt.Eur)
            Dim haber As Decimal = oCcbs.Where(Function(x) x.Dh = DTOCcb.DhEnum.Haber).Sum(Function(y) y.Amt.Eur)
            If oCta.Act = DTOPgcCta.Acts.Deutora Then
                retval = debe - haber
            Else
                retval = haber - debe
            End If

            'treu la liquidació si n'hi ha
            Dim oCcb As DTOCcb = oCcbs.FirstOrDefault(Function(x) x.Cca.Ccd = DTOCca.CcdEnum.IVA AndAlso DTOYearMonth.HasFch(oYearMonth, x.Cca.Fch))
            If oCcb IsNot Nothing Then
                retval = retval + oCcb.Amt.Eur
            End If

        End If
        Return DTOAmt.Factory(retval)
    End Function

    Private Shared Function Ccbs(oCcas As List(Of DTOCca), oYearMonth As DTOYearMonth, oCtaCod As DTOPgcPlan.Ctas) As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb) = oCcas.
            Where(Function(x) x.Fch.Month <= oYearMonth.Month).
            SelectMany(Function(y) y.Items).
            Where(Function(z) z.Cta.Codi = oCtaCod).
            ToList
        Return retval
    End Function
End Class
