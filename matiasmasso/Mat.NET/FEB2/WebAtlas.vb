Public Class WebAtlas
    Inherits _FeblBase

    Shared Async Function Update(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "webatlas/update", oEmp.Id)
    End Function

    Shared Async Function Distributors(oProveidor As DTOProveidor, exs As List(Of Exception)) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "webatlas/proveidor", oProveidor.Guid.ToString, "distributors")
    End Function

    Shared Async Function Distributors(exs As List(Of Exception), Optional oProduct As DTOProduct = Nothing, Optional oProvinciaOrZona As DTOArea = Nothing, Optional oLocation As DTOLocation = Nothing, Optional oProveidor As DTOProveidor = Nothing, Optional lang As DTOLang = Nothing, Optional includeItems As Boolean = False, Optional LatestPdcFchFrom As Date = Nothing) As Task(Of List(Of DTOProductDistributor))
        If lang Is Nothing Then lang = DTOApp.current.lang
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "webatlas/distribuidors", OpcionalGuid(oProduct), OpcionalGuid(oProvinciaOrZona), OpcionalGuid(oLocation), OpcionalGuid(oProveidor), lang.Tag, OpcionalBool(includeItems), FormatFch(LatestPdcFchFrom))
    End Function

    Shared Function DistributorsSync(exs As List(Of Exception), Optional oProduct As DTOProduct = Nothing, Optional oProvinciaOrZona As DTOArea = Nothing, Optional oLocation As DTOLocation = Nothing, Optional oProveidor As DTOProveidor = Nothing, Optional lang As DTOLang = Nothing, Optional includeItems As Boolean = False, Optional LatestPdcFchFrom As Date = Nothing) As List(Of DTOProductDistributor)
        If lang Is Nothing Then lang = DTOApp.current.lang
        Return Api.FetchSync(Of List(Of DTOProductDistributor))(exs, "webatlas/distribuidors", OpcionalGuid(oProduct), OpcionalGuid(oProvinciaOrZona), OpcionalGuid(oLocation), OpcionalGuid(oProveidor), lang.Tag, OpcionalBool(includeItems), FormatFch(LatestPdcFchFrom))
    End Function


    Shared Async Function Excel(oProveidor As DTOProveidor, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)
        Dim retval As ExcelHelper.Sheet = Nothing
        Dim items = Await FEB2.WebAtlas.Distributors(oProveidor, exs)
        If exs.Count = 0 Then
            Dim sTitle As String = String.Format("M+O {0} {1} {2:00}", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day)
            Dim sFilename As String = String.Format("M+O Store Locator {0:yyyyMMdd}.xlsx", DateTime.Today)
            retval = New ExcelHelper.Sheet(sTitle, sFilename)
            With retval
                .AddColumn("Store Name")
                .AddColumn("Address")
                .AddColumn("City")
                .AddColumn("Postcode")
                .AddColumn("Region")
                .AddColumn("Country")
            End With

            For Each item In items
                Dim oRow As ExcelHelper.Row = retval.AddRow
                oRow.AddCell(item.Nom)
                oRow.AddCell(item.Adr)
                oRow.AddCell(item.Location.Nom)
                oRow.AddCell(item.ZipCod)
                oRow.AddCell(item.Zona.Nom)
                oRow.AddCell(item.Country.Nom)
            Next

        End If

        Return retval
    End Function

End Class
