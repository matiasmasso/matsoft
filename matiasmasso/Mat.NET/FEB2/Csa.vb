Public Class Csa

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCsa)
        Dim retval = Await Api.Fetch(Of DTOCsa)(exs, "Csa", oGuid.ToString())
        For Each item In retval.Items
            item.Csa = retval
        Next
        Return retval
    End Function

    Shared Function Load(ByRef oCsa As DTOCsa, exs As List(Of Exception)) As Boolean
        If Not oCsa.IsLoaded And Not oCsa.IsNew Then
            Dim pCsa = Api.FetchSync(Of DTOCsa)(exs, "Csa", oCsa.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCsa)(pCsa, oCsa, exs)

                For Each item In oCsa.Items
                    item.Csa = oCsa
                Next
            End If
        End If


        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCsa As DTOCsa, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCsa)(oCsa, exs, "Csa")
        oCsa.IsNew = False
    End Function


    Shared Async Function Delete(oCsa As DTOCsa, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCsa)(oCsa, exs, "Csa")
    End Function


    Shared Async Function SaveRemesaCobrament(ByVal oCsa As DTOCsa, oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim pCsa = Await FEB2.Execute(Of DTOCsa, DTOCsa)(oCsa, exs, "Csas/SaveRemesaCobrament", oUser.Guid.ToString())
        If exs.Count = 0 Then
            DTOBaseGuid.CopyPropertyValues(Of DTOCsa)(pCsa, oCsa, exs)
            retval = True
        End If

        Return retval
    End Function

    Shared Async Function SaveDespesesRemesaCobrament(oCsa As DTOCsa, oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Csas/SaveDespesesCobrament", oCsa.Guid.ToString, oUser.Guid.ToString())
    End Function

    Shared Async Function LaCaixaRemesaExportacio(oCsa As DTOCsa, exs As List(Of Exception)) As Task(Of String)
        Return Await Api.Fetch(Of String)(exs, "Csa/LaCaixaRemesaExportacio", oCsa.Guid.ToString())
    End Function

    Shared Async Function BuildCca(exs As List(Of Exception), oCsa As DTOCsa, oUser As DTOUser) As Task(Of DTOCca)
        Dim oCca = DTOCca.Factory(oCsa.Fch, oUser, DTOCca.CcdEnum.RemesaEfectes, oCsa.formattedId)
        oCca.Concept = oCsa.Banc.AbrOrNom & "-remesa {0} d'anticips de crèdit"
        Dim oCtas = Await FEB2.PgcCtas.All(exs)
        Dim oCta = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.BancsEfectesDescomptats)
        If exs.Count = 0 Then
            For Each item In oCsa.items
                oCca.AddCredit(item.Amt, oCta, oCsa.banc, item.Pnd)
            Next
            oCca.AddSaldo(oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Bancs), oCsa.banc)
        End If
        Return oCca
    End Function
End Class

Public Class Csas
    Shared Async Function YearsAsync(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of Integer))
        Dim retval = Await Api.Fetch(Of List(Of Integer))(exs, "csas/emp/years", oEmp.Id)
        Return retval
    End Function

    Shared Async Function YearsAsync(oBanc As DTOBanc, exs As List(Of Exception)) As Task(Of List(Of Integer))
        Dim retval = Await Api.Fetch(Of List(Of Integer))(exs, "csas/banc/years", oBanc.Guid.ToString())
        Return retval
    End Function

    Shared Async Function AllAsync(iYear As Integer, oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOCsa))
        Dim retval = Await Api.Fetch(Of List(Of DTOCsa))(exs, "csas/emp", oEmp.Id, iYear)
        Return retval
    End Function

    Shared Async Function AllAsync(iYear As Integer, oBanc As DTOBanc, exs As List(Of Exception)) As Task(Of List(Of DTOCsa))
        Dim retval = Await Api.Fetch(Of List(Of DTOCsa))(exs, "csas/banc", oBanc.Guid.ToString, iYear)
        Return retval
    End Function

    Shared Async Function Update(oCsas As List(Of DTOCsa), exs As List(Of Exception)) As Task(Of List(Of Integer))
        Dim retval = Await Api.Update(Of List(Of DTOCsa), List(Of Integer))(oCsas, exs, "csas")
        Return retval
    End Function

    Shared Async Function Delete(oCsas As List(Of DTOCsa), exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval = Await Api.Delete(Of List(Of DTOCsa))(oCsas, exs, "csas")
        Return retval
    End Function

    Shared Function Distribueix(oCsbs As List(Of DTOCsb)) As List(Of DTOCsa)
        Dim retval As New List(Of DTOCsa)
        For Each oCsb In oCsbs

        Next
        Return retval
    End Function

    Shared Function Excel(oCsas As List(Of DTOCsa)) As ExcelHelper.Sheet
        Dim CancelRequest As Boolean

        Dim sCaption As String = "remeses de efectes"
        If oCsas.Count = 1 Then sCaption = "remesa " & oCsas.First.formattedId()

        Dim retval As New ExcelHelper.Sheet(sCaption, sCaption & ".xlsx")

        retval.AddColumn("remesa", ExcelHelper.Sheet.NumberFormats.PlainText)
        retval.AddColumn("data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
        retval.AddColumn("banc", ExcelHelper.Sheet.NumberFormats.PlainText)
        retval.AddColumn("efecte", ExcelHelper.Sheet.NumberFormats.Integer)
        retval.AddColumn("client", ExcelHelper.Sheet.NumberFormats.PlainText)
        retval.AddColumn("import", ExcelHelper.Sheet.NumberFormats.Euro)
        retval.AddColumn("venciment", ExcelHelper.Sheet.NumberFormats.DDMMYY)

        Dim idx As Integer
        For Each oCsa As DTOCsa In oCsas.OrderBy(Function(x) x.Id)
            For Each item As DTOCsb In oCsa.Items.OrderBy(Function(x) x.Id)
                Dim oRow As ExcelHelper.Row = retval.AddRow()
                With item
                    oRow.AddCell(oCsa.Id)
                    oRow.AddCell(oCsa.Fch)
                    oRow.AddCell(oCsa.Banc.AbrOrNom)
                    oRow.AddCell(item.Id)
                    oRow.AddCell(item.Contact.FullNom)
                    oRow.AddCellAmt(item.Amt)
                    oRow.AddCell(item.Vto)
                End With
                idx += 1
            Next
            If CancelRequest Then Exit For
        Next

        Return retval
    End Function
End Class
