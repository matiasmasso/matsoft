Public Class Csa
#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOCsa
        Dim retval As DTOCsa = CsaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oCsa As DTOCsa, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CsaLoader.Update(oCsa, exs)
        Return retval
    End Function

    Shared Function Delete(oCsa As DTOCsa, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CsaLoader.Delete(oCsa, exs)
        Return retval
    End Function


#End Region
    Shared Function SaveRemesaCobrament(ByRef oCsa As DTOCsa, oUser As DTOUser, exs As List(Of Exception)) As DTOCsa
        If oCsa.Items.Count > 0 Then
            Dim DtVto As Date = oCsa.Items.First.Vto 'les remeses al cobro es fan de un sol venciment, i les despeses de la remesa es registren el dia d'aquest venciment
            Dim oCca As DTOCca = DTOCca.Factory(DtVto, oUser, DTOCca.CcdEnum.DespesesRemesa, oCsa.formattedId)

            Dim oBanc As DTOBanc = oCsa.Banc
            BancLoader.Load(oBanc)
            Dim oTarifaExpenses = DTOAmt.Factory(oBanc.ComisioGestioCobr)
            Dim oExpenses = oTarifaExpenses.Times(oCsa.items.Count)

            Dim oCtaDebit = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.despesesCobrament, oCca.Exercici)
            oCca.AddDebit(oExpenses, oCtaDebit, oBanc)

            If oTarifaExpenses.IsNotZero Then
                Dim DcIva As Decimal = DTOTax.Closest(DTOTax.Codis.Iva_Standard, oCca.Fch).Tipus
                Dim oIva As DTOAmt = oTarifaExpenses.Times(oCsa.Items.Count).Percent(DcIva)
                Dim oCtaCredit = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.IvaSoportatNacional, oCca.Exercici)
                oCca.AddDebit(oIva, oCtaCredit)
            End If

            Dim oCtaBanc = PgcCtaLoader.FromCod(DTOPgcPlan.Ctas.bancs, oCca.Exercici)
            oCca.AddSaldo(oCtaBanc, oCsa.Banc)

            oCsa.Despeses = oExpenses

            CsaLoader.SaveExpenses(oCsa, oCca, exs)
        End If

        Return oCsa
    End Function


End Class

Public Class Csas

    Shared Function Years(ByRef oEmp As DTOEmp) As List(Of Integer)
        Dim retval = CsasLoader.Years(oEmp:=oEmp)
        Return retval
    End Function

    Shared Function Years(ByRef oBanc As DTOBanc) As List(Of Integer)
        Dim retval = CsasLoader.Years(oBanc:=oBanc)
        Return retval
    End Function

    Shared Function All(iYear As Integer, Optional oEmp As DTOEmp = Nothing, Optional oBanc As DTOBanc = Nothing) As List(Of DTOCsa)
        Dim retval = CsasLoader.All(iYear, oEmp, oBanc)
        Return retval
    End Function

    Shared Function Update(oCsas As List(Of DTOCsa), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CsasLoader.Update(oCsas, exs)
        Return retval
    End Function

    Shared Function Delete(oCsas As List(Of DTOCsa), exs As List(Of Exception)) As Boolean
        For Each oCsa In oCsas
            CsaLoader.Delete(oCsa, exs)
        Next
        Return exs.Count = 0
    End Function


End Class
