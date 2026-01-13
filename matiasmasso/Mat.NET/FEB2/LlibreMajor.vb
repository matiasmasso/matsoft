Public Class LlibreMajor


    Shared Function Url(DtFch As Date, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.LlibreMajor, DtFch.ToOADate)
    End Function

    Shared Function ExcelUrl(oExercici As DTOExercici, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.LlibreMajor, oExercici.Guid.ToString())
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oExercici As DTOExercici, oLang As DTOLang) As Task(Of Byte())
        Return Await Api.FetchBinary(exs, "Ccas/LlibreMajor/Excel", oExercici.Emp.Id, oExercici.Year, oLang.Tag)
    End Function


    Shared Async Function Excel(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of ExcelHelper.Sheet) 'DEPRECATED
        Dim retval As New ExcelHelper.Sheet
        Dim oLang As DTOLang = DTOLang.CAT

        Dim DcSaldo As Decimal
        Dim sCtaId As String = ""
        Dim sCtaNom As String = ""

        retval.AddColumn(oLang.Tradueix("Cuenta", "Compte", "Account"), ExcelHelper.Sheet.NumberFormats.PlainText)
        retval.AddColumn(oLang.Tradueix("Descripcion", "Descripció", "Description"), ExcelHelper.Sheet.NumberFormats.PlainText)
        retval.AddColumn(oLang.Tradueix("Asiento", "Assentament", "Log"), ExcelHelper.Sheet.NumberFormats.Integer)
        retval.AddColumn(oLang.Tradueix("Fecha", "Data", "Date"), ExcelHelper.Sheet.NumberFormats.DDMMYY)
        retval.AddColumn(oLang.Tradueix("Concepto", "Concepte", "Concept"), ExcelHelper.Sheet.NumberFormats.PlainText)
        retval.AddColumn(oLang.Tradueix("Debe", "Deure", "Debits"), ExcelHelper.Sheet.NumberFormats.Euro)
        retval.AddColumn(oLang.Tradueix("Haber", "Haver", "Credits"), ExcelHelper.Sheet.NumberFormats.Euro)
        retval.AddColumn(oLang.Tradueix("Saldo", "Saldo", "Balance"), ExcelHelper.Sheet.NumberFormats.Euro)

        Dim oCcd As New DTOCcd
        oCcd.Exercici = oExercici
        oCcd.Cta = New DTOPgcCta
        Dim items = Await FEB2.Ccbs.LlibreMajor(exs, oExercici)
        If exs.Count = 0 Then
            For Each item As DTOCcb In items
                If oCcd.Unequals(item) Then
                    oCcd = DTOCcd.Factory(oExercici, item.cta, item.contact)
                    DcSaldo = 0
                    sCtaId = DTOPgcCta.FormatAccountId(item.cta, item.contact)
                    sCtaNom = DTOPgcCta.FormatAccountDsc(item.cta, item.contact, oLang)
                End If
                DcSaldo += If(oCcd.Cta.act = item.dh, item.amt.Eur, -item.amt.Eur)

                Dim oRow As ExcelHelper.Row = retval.AddRow()
                oRow.AddCell(sCtaId)
                oRow.AddCell(sCtaNom)
                oRow.AddCell(item.cca.id)
                oRow.AddCell(item.cca.fch)
                oRow.AddCell(item.cca.concept)
                oRow.addCell(If(item.dh = DTOCcb.DhEnum.debe, item.amt.Eur, 0))
                oRow.addCell(If(item.dh = DTOCcb.DhEnum.haber, item.amt.Eur, 0))
                oRow.AddCell(DcSaldo)

            Next

        End If
        Return retval
    End Function

End Class
