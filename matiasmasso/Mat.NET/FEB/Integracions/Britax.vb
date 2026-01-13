Public Class Britax
    Shared Async Function StoreLocator(exs As List(Of Exception)) As Task(Of DTO.Britax.XML)
        Return Await Api.Fetch(Of DTO.Britax.XML)(exs, "britax/storelocator")
    End Function

    Shared Function ExcelTarget(year As Integer, bookFras As List(Of DTOBookFra),
                            Orders As List(Of DTOPurchaseOrder),
                            PendingOrders As List(Of DTOPurchaseOrder)) As MatHelper.Excel.Sheet

        Dim oLang As DTOLang = DTOLang.ENG

        Dim retval As New MatHelper.Excel.Sheet(year, "monthly targets")
        With retval
            .AddColumn("month", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("orders", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("invoices", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("order book", MatHelper.Excel.Cell.NumberFormats.Euro)
            .DisplayTotals = True
        End With

        For mes = 1 To 12
            Dim iMes As Integer = mes
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            With oRow
                .AddCell(oLang.Mes(iMes))
                .AddCell(TargetMonthOrders(Orders, year, iMes).Sum(Function(x) x.SumaDeImports.Eur))
                .AddCell(bookFras.Where(Function(x) x.Cca.Fch.Month = iMes).Sum(Function(x) x.BaseDevengada.Eur))
                .AddCell(TargetMonthOrders(PendingOrders, year, iMes).Sum(Function(x) x.Items.Sum(Function(y) y.Pending * y.Price.Eur * (100 - y.Dto) / 100)))
            End With
        Next

        Return retval
    End Function

    Shared Function TargetMonthOrders(Orders As List(Of DTOPurchaseOrder), year As Integer, mes As Integer) As List(Of DTOPurchaseOrder)
        Dim retval = Orders.Where(Function(x) x.FchDeliveryMin.Year = year And x.FchDeliveryMin.Month = mes).ToList
        Dim missingDeliveryDate = Orders.Where(Function(x) x.FchDeliveryMin = Nothing)
        If Orders.Any(Function(x) x.FchDeliveryMin = Nothing) Then
            Dim fchTo As New Date(year, mes, 15)
            Dim fchFrom As Date = fchTo.AddMonths(-1).AddDays(1)
            Dim missingFchOrders = Orders.Where(Function(x) x.FchDeliveryMin = Nothing And x.Fch <= fchTo And x.Fch > fchFrom).ToList
            retval.AddRange(missingFchOrders)
        End If
        Return retval
    End Function


End Class
