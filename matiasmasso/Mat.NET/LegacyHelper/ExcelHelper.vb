
Imports OfficeOpenXml

Public Class ExcelHelper
    Inherits MatHelperStd.ExcelHelper

    Shared Function ExcelSheetFromFilename(exs As List(Of Exception), sFilename As String, ByRef oBook As MatHelperStd.ExcelHelper.Book, Optional hasHeaderRow As Boolean = False) As Boolean
        Dim retval As Boolean = EPPlusHelper.Read(exs, sFilename, oBook, hasHeaderRow)
        Return retval
    End Function

    Shared Function ExcelSheetFromFilename(exs As List(Of Exception), sFilename As String, ByRef oSheet As MatHelperStd.ExcelHelper.Sheet, Optional hasHeaderRow As Boolean = False) As Boolean
        Dim oBook As New MatHelperStd.ExcelHelper.Book
        Dim retval As Boolean = ExcelSheetFromFilename(exs, sFilename, oBook, hasHeaderRow)
        oSheet = oBook.Sheets.First
        oSheet.Filename = sFilename
        Return retval
    End Function

    Shared Function Stream(oBook As MatHelperStd.ExcelHelper.Book, ByRef oStream As Byte(), Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean = EPPlusHelper.Stream(oBook, oStream, exs)
        Return retval
    End Function

    Shared Function Stream(oSheet As MatHelperStd.ExcelHelper.Sheet, ByRef oStream As Byte(), Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim oBook As New MatHelperStd.ExcelHelper.Book
        oBook.Sheets.Add(oSheet)
        oBook.Filename = oSheet.Filename
        Dim retval As Boolean = Stream(oBook, oStream, exs)
        Return retval
    End Function

    Shared Function Save(oBook As MatHelperStd.ExcelHelper.Book, Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean = EPPlusHelper.Save(oBook, exs)
        Return retval
    End Function

    Shared Function Save(oSheet As MatHelperStd.ExcelHelper.Sheet, Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim oBook As New MatHelperStd.ExcelHelper.Book
        oBook.Sheets.Add(oSheet)
        oBook.Filename = oSheet.Filename
        Dim retval As Boolean = Save(oBook, exs)
        Return retval
    End Function


    Private Shared Function Load(WorkSheet As ExcelWorksheet, oSheet As MatHelperStd.ExcelHelper.Sheet, Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean
        Dim excelRow As Integer
        Dim col As Integer
        'Dim oCultureInfoOriginal As System.Globalization.CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture

        Try
            'Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

            With WorkSheet
                .Cells.Style.Font.Name = "Calibri"
                .Cells.Style.Font.Size = 11
            End With

            'format columnes
            For col = 0 To oSheet.Columns.Count - 1
                WorkSheet.Cells(1, col + 1).Value = oSheet.Columns(col).Header
                Select Case oSheet.Columns(col).NumberFormat
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00 €;-#,##0.00 €;#"
                        WorkSheet.Column(col + 1).Width = 40
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.Decimal2Digits
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00;-#,##0.00;#"
                        WorkSheet.Column(col + 1).Width = 30
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0;-#,##0;#"
                        WorkSheet.Column(col + 1).Width = 30
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "dd/MM/yy"
                        WorkSheet.Column(col + 1).Width = 50
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50
                        WorkSheet.Column(col + 1).Width = 50
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.Percent
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#\%;-#\%;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.m3D2
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00 \m3;-#,##0.00 \m3;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.m3
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00000 \m3;-#,##0.00000 \m3;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.Kg
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0 \K\g;-#,##0 \K\g;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.mm
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0 \m\m;-#,##0 \m\m;#"
                        WorkSheet.Column(col + 1).Width = 50
                End Select
            Next

            'format titols de columna
            Dim startRow As Integer
            Dim oRange As ExcelRange

            If oSheet.Columns.Count > 0 Then
                startRow += 1
                oRange = WorkSheet.Cells(startRow, 1, startRow, oSheet.Columns.Count)
                With oRange.Style
                    .Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                    '.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
                End With

                'texte titols de columna
                For col = 0 To oSheet.Columns.Count - 1
                    WorkSheet.Cells(startRow, col + 1).Value = oSheet.Columns(col).Header
                Next
            End If

            'fila totals
            If oSheet.DisplayTotals Then
                startRow += 1
                oRange = WorkSheet.Cells(startRow, 1, startRow, oSheet.Columns.Count)
                With oRange.Style
                    .Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                    '.Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue)
                    .Border.Top.Style = Style.ExcelBorderStyle.Hair
                    .Border.Bottom.Style = Style.ExcelBorderStyle.Hair
                End With
                ' For col = 0 To oSheet.Rows(row).Cells.Count - 1
                For col = 0 To oSheet.Columns.Count - 1
                    Select Case oSheet.Columns(col).NumberFormat
                        Case MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer, MatHelperStd.ExcelHelper.Sheet.NumberFormats.Decimal2Digits, MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro, MatHelperStd.ExcelHelper.Sheet.NumberFormats.Kg, MatHelperStd.ExcelHelper.Sheet.NumberFormats.m3
                            Dim cell As ExcelRange = WorkSheet.Cells(startRow, col + 1)
                            cell.FormulaR1C1 = "SUM(R[1]C:R[" & oSheet.Rows.Count & "]C)"
                    End Select
                Next
            End If

            'files
            startRow += 1
            For SheetRow = 0 To oSheet.Rows.Count - 1


                excelRow = SheetRow + startRow
                For col = 0 To oSheet.Rows(SheetRow).Cells.Count - 1
                    Dim cell As ExcelRange = WorkSheet.Cells(excelRow, col + 1)
                    Dim src As MatHelperStd.ExcelHelper.Cell = oSheet.Rows(SheetRow).Cells(col)

                    If src.FormulaR1C1 > "" Then
                        cell.FormulaR1C1 = src.FormulaR1C1
                    Else
                        cell.Value = src.Content
                    End If

                    'If oSheet.Rows(SheetRow).Color <> Nothing Then
                    'Dim oColor = oSheet.Rows(SheetRow).Color
                    'cell.Style.Font.Color.SetColor(oColor)
                    'End If


                    Dim sUrl As String = src.Url
                    If sUrl > "" Then
                        If Convert.ToString(src.Content) > "" Then
                            cell.Hyperlink = New Uri(sUrl)
                            With cell.Style
                                .Font.UnderLine = True
                                '.Font.Color.SetColor(Color.Blue)
                            End With
                        End If
                    End If

                    If src.Indent > 0 Then
                        cell.Style.Indent = 4 * src.Indent
                    End If

                    Select Case src.Alignment
                        Case MatHelperStd.ExcelHelper.Cell.Alignments.Left
                            cell.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                        Case MatHelperStd.ExcelHelper.Cell.Alignments.Center
                            cell.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        Case MatHelperStd.ExcelHelper.Cell.Alignments.Right
                            cell.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                    End Select


                    Select Case src.CellStyle
                        Case MatHelperStd.ExcelHelper.Cell.CellStyles.Bold
                            cell.Style.Font.Bold = True
                        Case MatHelperStd.ExcelHelper.Cell.CellStyles.Italic
                            cell.Style.Font.Bold = True
                        Case MatHelperStd.ExcelHelper.Cell.CellStyles.Total
                            cell.Style.Border.Top.Style = Style.ExcelBorderStyle.Thin
                            cell.Style.Border.Bottom.Style = Style.ExcelBorderStyle.Double
                    End Select
                Next
            Next

            WorkSheet.Cells.AutoFitColumns()

            'format full
            If oSheet.Columns.Count > 0 Then
                oRange = WorkSheet.Cells(startRow, 1, startRow + oSheet.Rows.Count - 1, oSheet.Columns.Count)
                With oRange.Style
                    .Border.Top.Style = Style.ExcelBorderStyle.Thin
                    .Border.Left.Style = Style.ExcelBorderStyle.Thin
                    .Border.Right.Style = Style.ExcelBorderStyle.Thin
                    .Border.Bottom.Style = Style.ExcelBorderStyle.Thin
                End With
            End If

        Catch ex As Exception
            If exs IsNot Nothing Then
                Dim sMsg As String = String.Format("EPPlus Error en fila {0} columna {1}: {2}", excelRow, col, ex.Message)
                exs.Add(New Exception(sMsg))
            End If
        Finally
            'Threading.Thread.CurrentThread.CurrentCulture = oCultureInfoOriginal
        End Try

        Return retval
    End Function

End Class
