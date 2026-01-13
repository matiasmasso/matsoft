Public Class InvoiceUpdaterHelper

    Shared Async Function Update(exs As List(Of Exception), oInvoices As List(Of DTOInvoice), oUser As DTOUser, Progress As ProgressBarHandler) As Task(Of Boolean)
        If oInvoices.Count = 0 Then
            exs.Add(New Exception("No hi han factures per desar"))
        Else
            Dim iCount As Integer = oInvoices.Count
            For Each oInvoice As DTOInvoice In oInvoices
                Dim idx As Integer = oInvoices.IndexOf(oInvoice)
                If Await Update(exs, oInvoice, oUser) Then
                    Progress(0, iCount, idx, String.Format("desant fra.{0} de {1}", oInvoice.NumeroYSerie, oInvoice.customer.nom), CancelRequest:=False)
                Else
                    Progress(0, iCount, idx, String.Format("ERROR al desar la fra.{0} de {1}", oInvoice.NumeroYSerie, oInvoice.customer.nom), CancelRequest:=False)
                End If
            Next
        End If
        Return exs.Count = 0
    End Function


    Shared Async Function Update(exs As List(Of Exception), oInvoice As DTOInvoice, oUser As DTOUser) As Task(Of Boolean)
        'No el podem passar a Azure perque SetCca necessita GhostScript per el thumbnail de la factura
        Dim retval As Boolean
        Dim oTaxes As List(Of DTOTax) = DTOTax.Closest(oInvoice.fch)
        DTOInvoice.SetRegimenEspecialOTrascendencia(oInvoice)
        DTOInvoice.SetImports(oInvoice, oTaxes)
        If oInvoice.tipoFactura = "F1" And oInvoice.serie = DTOInvoice.Series.Rectificativa Then
            oInvoice.tipoFactura = "R1"
        End If
        oInvoice.cca = Await SetCca(exs, oInvoice, oUser)
        If exs.Count = 0 Then
            retval = Await FEB2.Invoice.Update(exs, oInvoice, oInvoice.cca.docFile)
        End If
        Return retval
    End Function

    Shared Async Function SetCca(exs As List(Of Exception), oInvoice As DTOInvoice, oUser As DTOUser) As Task(Of DTOCca)
        Dim retval As DTOCca = Nothing
        Dim oCtas = Await FEB2.PgcCtas.All(exs)
        Dim oPdf As New LegacyHelper.PdfAlb(oInvoice)
        Dim oDocFile As DTODocFile = Nothing
        Dim oCert = Await FEB2.Cert.Find(GlobalVariables.Emp.Org, exs)
        Dim oStream = oPdf.Stream(exs, oCert)
        If LegacyHelper.DocfileHelper.LoadFromStream(oStream, oDocFile, exs) Then
            retval = FEB2.Invoice.CcaBuilder(exs, oInvoice, oDocFile, oUser, oCtas)
        End If
        Return retval
    End Function

End Class
