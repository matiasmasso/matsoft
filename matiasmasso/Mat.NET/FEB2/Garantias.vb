Public Class Garantias
    Inherits _FeblBase
    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, FchFrom As Date, FchTo As Date) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "Garantias", oEmp.Id, FormatFch(FchFrom), FormatFch(FchTo))
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oExercici As DTOExercici, Optional oLang As DTOLang = Nothing) As Task(Of ExcelHelper.Sheet)
        Dim FchFrom As Date = DTOExercici.FromYear(oExercici.Emp, oExercici.Year - 2).FirstFch
        Dim FchTo As Date = oExercici.LastDayOrToday
        Dim sFilename As String = String.Format("{0}.{1} Garanties des de {2}.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year, FchFrom.Year)
        Dim oDomain = DTOWebDomain.Factory(oLang)
        Dim retval As New ExcelHelper.Sheet(oExercici.Year, sFilename)

        With retval
            .addColumn("albará", ExcelHelper.Sheet.NumberFormats.Integer)
            .addColumn("data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .addColumn("client", ExcelHelper.Sheet.NumberFormats.PlainText)
            .addColumn("concepte", ExcelHelper.Sheet.NumberFormats.PlainText)
            .addColumn("producte", ExcelHelper.Sheet.NumberFormats.PlainText)
            .addColumn("unitats", ExcelHelper.Sheet.NumberFormats.Euro)
            .addColumn("cost", ExcelHelper.Sheet.NumberFormats.Euro)
            .addColumn("import", ExcelHelper.Sheet.NumberFormats.Euro)
        End With

        Dim CancelRequest As Boolean = False
        Dim items As List(Of DTODeliveryItem) = Await All(exs, oExercici.Emp, FchFrom, FchTo)
        For Each item As DTODeliveryItem In items
            Dim oRow As ExcelHelper.Row = retval.addRow
            With item
                oRow.addCell(.delivery.id, FEB2.Delivery.PdfUrl(.delivery, True))
                oRow.addCell(.delivery.fch)
                oRow.addCell(.delivery.customer.FullNom)
                oRow.addCell(.purchaseOrderItem.purchaseOrder.concept)
                oRow.addCell(.sku.NomLlarg.Esp, .sku.GetUrl(oDomain.DefaultLang,, true))
                oRow.addCell(.qty)
                oRow.addCell(.sku.cost.Eur)
                oRow.addCell(.qty * .sku.cost.Eur)
            End With
        Next
        Return retval
    End Function
End Class
