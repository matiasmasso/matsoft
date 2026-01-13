Public Class Csb 'FEBL

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCsb)
        Return Await Api.Fetch(Of DTOCsb)(exs, "Csb", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCsb As DTOCsb, exs As List(Of Exception)) As Boolean
        If Not oCsb.IsLoaded And Not oCsb.IsNew Then
            Dim pCsb = Api.FetchSync(Of DTOCsb)(exs, "Csb", oCsb.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCsb)(pCsb, oCsb, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function SaveVto(exs As List(Of Exception), oCsb As DTOCsb, oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Csb/SaveVto", oCsb.Guid.ToString, oUser.Guid.ToString())
    End Function


    Shared Async Function RevertVto(oCca As DTOCca, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Csb/RevertVto", oCca.Guid.ToString())
    End Function

    Shared Async Function Reclama(exs As List(Of Exception), oUser As DTOUser, oCsb As DTOCsb) As Task(Of DTOCca)
        Return Await Api.Fetch(Of DTOCca)(exs, "Csb/Reclama", oUser.Guid.ToString, oCsb.Guid.ToString())
    End Function

    Shared Async Function RetrocedeixReclamacio(oUser As DTOUser, oCsb As DTOCsb, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Csb/RetrocedeixReclamacio", oUser.Guid.ToString, oCsb.Guid.ToString())
    End Function
End Class

Public Class Csbs

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oBanc As DTOBanc, year As Integer) As Task(Of List(Of DTOCsb))
        Dim retval = Await Api.Fetch(Of List(Of DTOCsb))(exs, "Csbs", oEmp.Id, oBanc.Guid.ToString, year)

        'restore banc if it comes empty from Api
        If retval IsNot Nothing Then
            For Each oCsb In retval
                If oCsb.Csa IsNot Nothing Then
                    If oCsb.Csa.banc Is Nothing Then
                        oCsb.Csa.banc = oBanc
                    End If
                End If
            Next
        End If
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oCustomer As DTOContact) As Task(Of List(Of DTOCsb))
        Return Await Api.Fetch(Of List(Of DTOCsb))(exs, "Csbs/FromCustomer", oEmp.Id, oCustomer.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oIban As DTOIban) As Task(Of List(Of DTOCsb))
        Return Await Api.Fetch(Of List(Of DTOCsb))(exs, "Csbs/FromIban", oIban.Guid.ToString())
    End Function

    Shared Async Function CsbResults(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOCsbResult))
        Dim retval = Await Api.Fetch(Of List(Of DTOCsbResult))(exs, "Csbs/CsbResults", oEmp.Id)
        Return retval
    End Function



    Shared Async Function PendentsDeGirar(exs As List(Of Exception), oEmp As DTOEmp, oCountry As DTOCountry, sepa As Boolean) As Task(Of List(Of DTOCsb))
        Dim oCountryGuid = Guid.Empty
        If oCountry IsNot Nothing Then oCountryGuid = oCountry.Guid
        Return Await Api.Fetch(Of List(Of DTOCsb))(exs, "Csbs/PendentsDeGirar", oEmp.Id, oCountryGuid.ToString, If(sepa, 1, 0))
    End Function

    Shared Async Function PendentsDeVto(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date) As Task(Of List(Of DTOCsb))
        Dim oCountryGuid = Guid.Empty
        Return Await Api.Fetch(Of List(Of DTOCsb))(exs, "Csbs/PendentsDeVto", oEmp.Id, DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Async Function mailingLogs(exs As List(Of Exception), oEmp As DTOEmp, year As Integer) As Task(Of List(Of DTOCsb))
        Return Await Api.Fetch(Of List(Of DTOCsb))(exs, "Csbs/mailinglogs", oEmp.Id, year)
    End Function



    Shared Async Function IndexImpagats(exs As List(Of Exception), oEmp As DTOEmp, oCustomer As DTOContact, Optional ByVal iDesdeElsDarrersMesos As Integer = 6) As Task(Of Decimal)
        Dim fchFrom As Date = DateTime.Today.AddMonths(-iDesdeElsDarrersMesos)
        Dim oCsbs = Await FEB2.Csbs.All(exs, oEmp, oCustomer)
        Dim oActiveCsbs = oCsbs.
            Where(Function(x) x.Result <> DTOCsb.Results.Reclamat And x.Vto >= fchFrom).
            ToList
        Dim DcTots = oActiveCsbs.Sum(Function(y) y.Amt.Eur)
        Dim DcImpagats = oActiveCsbs.Where(Function(x) x.Result = DTOCsb.Results.Impagat).Sum(Function(y) y.Amt.Eur)

        Dim retval As Decimal
        If DcTots <> 0 Then
            retval = 100 * DcImpagats / DcTots
        End If
        Return retval
    End Function


    Shared Async Function ExcelPendentsDeVto(oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)
        Dim retval As ExcelHelper.Sheet = Nothing
        Dim FchTo As Date = oExercici.LastDayOrToday
        Dim items = Await FEB2.Csbs.PendentsDeVto(exs, oExercici.Emp, FchTo)
        If exs.Count = 0 Then
            Dim sFilename As String = String.Format("{0}.{1} Efectes en circulació.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
            retval = New ExcelHelper.Sheet(oExercici.Year, sFilename)

            With retval
                .AddColumn("banc", ExcelHelper.Sheet.NumberFormats.PlainText)
                .AddColumn("efecte", ExcelHelper.Sheet.NumberFormats.PlainText)
                .AddColumn("venciment", ExcelHelper.Sheet.NumberFormats.DDMMYY)
                .AddColumn("import", ExcelHelper.Sheet.NumberFormats.Euro)
                .AddColumn("lliurat", ExcelHelper.Sheet.NumberFormats.PlainText)
            End With

            For Each item As DTOCsb In items
                Dim oRow As ExcelHelper.Row = retval.AddRow
                With item
                    oRow.AddCell(.Csa.Banc.Abr)
                    oRow.AddCell(item.FormattedId())
                    oRow.AddCell(.Vto)
                    oRow.AddCellAmt(.Amt)
                    oRow.AddCell(.Contact.Nom)
                End With
            Next
        End If
        Return retval
    End Function
End Class

