Public Class Extracte
    Inherits _FeblBase

    Shared Async Function Years(exs As List(Of Exception), oEmp As DTOEmp, Optional oCta As DTOPgcCta = Nothing, Optional oContact As DTOContact = Nothing) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "extracte/years", oEmp.Id, OpcionalGuid(oCta), OpcionalGuid(oContact))
    End Function

    Shared Async Function Ctas(exs As List(Of Exception), oExercici As DTOExercici, Optional oContact As DTOContact = Nothing) As Task(Of List(Of DTOPgcCta))
        Return Await Api.Fetch(Of List(Of DTOPgcCta))(exs, "extracte/ctas", oExercici.Emp.Id, oExercici.Year, OpcionalGuid(oContact))
    End Function

    Shared Async Function Ccbs(exs As List(Of Exception), oExtracte As DTOExtracte) As Task(Of List(Of DTOCcb))
        Return Await Api.Fetch(Of List(Of DTOCcb))(exs, "extracte/ccbs", oExtracte.Exercici.Emp.Id, oExtracte.Exercici.Year, OpcionalGuid(oExtracte.Cta), OpcionalGuid(oExtracte.Contact))
    End Function

    Shared Function CcbsSync(exs As List(Of Exception), oExtracte As DTOExtracte) As List(Of DTOCcb)
        Return Api.FetchSync(Of List(Of DTOCcb))(exs, "extracte/ccbs", oExtracte.Exercici.Emp.Id, oExtracte.Exercici.Year, OpcionalGuid(oExtracte.Cta), OpcionalGuid(oExtracte.Contact))
    End Function

    Shared Function Excel(oCcbs As List(Of DTOCcb), oLang As DTOLang) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet()

        If oCcbs.Count > 0 Then
            retval.Name = oCcbs.First.Cta.Id
            retval.Filename = DTOExtracte.Filename(oCcbs, oLang)

            With retval
                .AddColumn("Registre", ExcelHelper.Sheet.NumberFormats.PlainText)
                .AddColumn("Data", ExcelHelper.Sheet.NumberFormats.DDMMYY)
                .AddColumn("Concepte", ExcelHelper.Sheet.NumberFormats.PlainText)
                .AddColumn("Deure", ExcelHelper.Sheet.NumberFormats.Euro)
                .AddColumn("Haver", ExcelHelper.Sheet.NumberFormats.Euro)
                .AddColumn("Saldo", ExcelHelper.Sheet.NumberFormats.Euro)
            End With


            Dim oRow As ExcelHelper.Row
            If oCcbs.Count > 0 Then
                retval.AddRow()

                Dim oCta As DTOPgcCta = oCcbs.First.Cta

                Dim Saldo As Decimal = 0
                For Each item As DTOCcb In oCcbs
                    Dim sUrl As String = ""
                    If item.Cca.DocFile IsNot Nothing Then
                        sUrl = FEB2.DocFile.DownloadUrl(item.Cca.DocFile, True)
                    End If

                    oRow = retval.AddRow
                    Dim oDeb = DTOCcb.Debit(item)
                    Dim oHab = DTOCcb.Credit(item)

                    oRow.AddCell(item.Cca.Id, sUrl)
                    oRow.AddCell(item.Cca.Fch)
                    oRow.AddCell(item.Cca.Concept, sUrl)

                    If oDeb Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCellAmt(oDeb)
                    End If
                    If oHab Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCellAmt(oHab)
                    End If


                    Dim cellSaldo As ExcelHelper.Cell = oRow.AddCell
                    If oCta.Act = DTOPgcCta.Acts.Deutora Then
                        cellSaldo.FormulaR1C1 = "R[-1]C+RC[-2]-RC[-1]"
                    Else
                        cellSaldo.FormulaR1C1 = "R[-1]C+RC[-1]-RC[-2]"
                    End If
                Next
            End If
        End If

        Return retval
    End Function

End Class
