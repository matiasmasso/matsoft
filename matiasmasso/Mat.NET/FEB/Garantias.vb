Public Class Garantias
    Inherits _FeblBase
    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, FchFrom As Date, FchTo As Date) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "Garantias", oEmp.Id, FormatFch(FchFrom), FormatFch(FchTo))
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oExercici As DTOExercici, Optional oLang As DTOLang = Nothing) As Task(Of MatHelper.Excel.Sheet)
        Dim FchFrom As Date = DTOExercici.FromYear(oExercici.Emp, oExercici.Year - 2).FirstFch
        Dim FchTo As Date = oExercici.LastDayOrToday
        Dim sFilename As String = String.Format("{0}.{1} Garanties des de {2}.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year, FchFrom.Year)
        Dim oDomain = DTOWebDomain.Factory(oLang)
        Dim retval As New MatHelper.Excel.Sheet(oExercici.Year, sFilename)

        With retval
            .addColumn("albará", MatHelper.Excel.Cell.NumberFormats.Integer)
            .addColumn("data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .addColumn("client", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .addColumn("concepte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .addColumn("producte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .addColumn("unitats", MatHelper.Excel.Cell.NumberFormats.Euro)
            .addColumn("cost", MatHelper.Excel.Cell.NumberFormats.Euro)
            .addColumn("import", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        Dim CancelRequest As Boolean = False
        Dim items As List(Of DTODeliveryItem) = Await All(exs, oExercici.Emp, FchFrom, FchTo)
        For Each item As DTODeliveryItem In items
            Dim oRow As MatHelper.Excel.Row = retval.addRow
            With item
                oRow.addCell(.delivery.id, Delivery.PdfUrl(.delivery, True))
                oRow.addCell(.delivery.fch)
                oRow.addCell(.delivery.customer.FullNom)
                oRow.addCell(.purchaseOrderItem.purchaseOrder.concept)
                oRow.AddCell(.Sku.RefYNomLlarg.Esp, .Sku.GetUrl(oDomain.DefaultLang,, True))
                oRow.addCell(.qty)
                oRow.addCell(.sku.cost.Eur)
                oRow.addCell(.qty * .sku.cost.Eur)
            End With
        Next
        Return retval
    End Function
End Class
