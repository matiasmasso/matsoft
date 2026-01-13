Imports OfficeOpenXml

Public Class ExcelHelper
    'requires EPPlus by Jan Källman (NuGet)

    Shared Function ExcelSheetFromFilename(sFilename As String, ByRef oBook As ExcelHelper.Book, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        oBook = New ExcelHelper.Book
        oBook.Filename = sFilename
        Dim iRow As Integer
        Dim iCol As Integer
        Dim BottomRow As Integer
        Dim LastCol As Integer
        Try
            Dim oFileInfo As New IO.FileInfo(sFilename)
            Dim package As New ExcelPackage(oFileInfo)

            Dim workbook As ExcelWorkbook = package.Workbook
            For Each worksheet As ExcelWorksheet In workbook.Worksheets
                If worksheet.Dimension IsNot Nothing Then
                    Dim oSheet As New ExcelHelper.Sheet
                    oSheet.Name = worksheet.Name
                    oBook.Sheets.Add(oSheet)
                    BottomRow = worksheet.Dimension.[End].Row
                    LastCol = worksheet.Dimension.[End].Column
                    For iRow = worksheet.Dimension.Start.Row To BottomRow
                        ' If iRow = 88 Then Stop
                        Dim oRow As ExcelHelper.Row = oSheet.AddRow()
                        For iCol = worksheet.Dimension.Start.Column To LastCol
                            Dim cellValue As Object = worksheet.Cells(iRow, iCol).Value
                            oRow.AddCell(cellValue)
                        Next
                    Next
                End If
            Next
            retval = True

        Catch ex As Exception
            exs.Add(New Exception(String.Format("error al importar Excel en fila {0} columna {1}", iRow, iCol)))
            exs.Add(ex)
        End Try

        Return retval
    End Function

    Shared Function Stream(oSheet As ExcelHelper.Sheet, ByRef oStream As Byte(), Optional exs As List(Of Exception) = Nothing, Optional ShowProgress As ProgressBarHandler = Nothing) As Boolean
        Dim oBook As New ExcelHelper.Book
        oBook.Sheets.Add(oSheet)
        oBook.Filename = oSheet.Filename
        Dim retval As Boolean = Stream(oBook, oStream, exs, ShowProgress)
        Return retval
    End Function

    Shared Function Stream(oBook As ExcelHelper.Book, ByRef oStream As Byte(), Optional exs As List(Of Exception) = Nothing, Optional ShowProgress As ProgressBarHandler = Nothing) As Boolean
        Dim retval As Boolean
        Dim oMemoryStream As New IO.MemoryStream
        Try
            Using xlPackage As New ExcelPackage(oMemoryStream)
                For Each oSheet As ExcelHelper.Sheet In oBook.Sheets
                    Dim idx As Integer = oBook.Sheets.IndexOf(oSheet) + 1
                    If oSheet.Name = "" Then oSheet.Name = "Hoja " & idx
                    xlPackage.Workbook.Worksheets.Add(oSheet.Name)
                    Dim ws As ExcelWorksheet = xlPackage.Workbook.Worksheets(idx)
                    ws.Name = oSheet.Name
                    Load(ws, oSheet, exs, ShowProgress)
                Next
                xlPackage.Save()
            End Using

            oStream = oMemoryStream.ToArray
            retval = exs.Count = 0
        Catch ex As Exception
            If exs IsNot Nothing Then
                exs.Add(ex)
            End If
        End Try
        Return retval
    End Function

    Shared Function Save(oBook As ExcelHelper.Book, Optional exs As List(Of Exception) = Nothing, Optional ShowProgress As ProgressBarHandler = Nothing) As Boolean
        Dim retval As Boolean
        Dim newFile As IO.FileInfo = Nothing
        Try
            newFile = New IO.FileInfo(oBook.Filename)
            Using xlPackage As New ExcelPackage(newFile)
                For Each oSheet As ExcelHelper.Sheet In oBook.Sheets
                    xlPackage.Workbook.Worksheets.Add(oSheet.Name)
                    Dim idx As Integer = xlPackage.Workbook.Worksheets.Count
                    Dim ws As ExcelWorksheet = xlPackage.Workbook.Worksheets(idx)
                    Load(ws, oSheet, exs, ShowProgress)
                Next
                xlPackage.Save()
            End Using
            retval = True
        Catch ex As Exception
            If exs IsNot Nothing Then
                exs.Add(ex)
            End If
        End Try
        Return retval
    End Function

    Private Shared Function Load(WorkSheet As ExcelWorksheet, oSheet As ExcelHelper.Sheet, Optional exs As List(Of Exception) = Nothing, Optional ShowProgress As ProgressBarHandler = Nothing) As Boolean
        Dim retval As Boolean
        Dim CancelRequest As Boolean
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
                    Case ExcelHelper.Sheet.NumberFormats.Euro
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00 €;-#,##0.00 €;#"
                        WorkSheet.Column(col + 1).Width = 40
                    Case ExcelHelper.Sheet.NumberFormats.Decimal2Digits
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00;-#,##0.00;#"
                        WorkSheet.Column(col + 1).Width = 30
                    Case ExcelHelper.Sheet.NumberFormats.Integer
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0;-#,##0;#"
                        WorkSheet.Column(col + 1).Width = 30
                    Case ExcelHelper.Sheet.NumberFormats.DDMMYY
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "dd/MM/yy"
                        WorkSheet.Column(col + 1).Width = 50
                    Case ExcelHelper.Sheet.NumberFormats.W50
                        WorkSheet.Column(col + 1).Width = 50
                    Case ExcelHelper.Sheet.NumberFormats.Percent
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#\%;-#\%;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case ExcelHelper.Sheet.NumberFormats.m3
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00 \m3;-#,##0.00 \m3;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case ExcelHelper.Sheet.NumberFormats.Kg
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0 \K\g;-#,##0 \K\g;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case ExcelHelper.Sheet.NumberFormats.mm
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
                    .Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
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
                    .Fill.BackgroundColor.SetColor(System.Drawing.Color.AliceBlue)
                    .Border.Top.Style = Style.ExcelBorderStyle.Hair
                    .Border.Bottom.Style = Style.ExcelBorderStyle.Hair
                End With
                ' For col = 0 To oSheet.Rows(row).Cells.Count - 1
                For col = 0 To oSheet.Columns.Count - 1
                    Select Case oSheet.Columns(col).NumberFormat
                        Case ExcelHelper.Sheet.NumberFormats.Integer, ExcelHelper.Sheet.NumberFormats.Decimal2Digits, ExcelHelper.Sheet.NumberFormats.Euro, ExcelHelper.Sheet.NumberFormats.Kg, ExcelHelper.Sheet.NumberFormats.m3
                            Dim cell As ExcelRange = WorkSheet.Cells(startRow, col + 1)
                            cell.FormulaR1C1 = "SUM(R[1]C:R[" & oSheet.Rows.Count & "]C)"
                    End Select
                Next
            End If

            'files
            startRow += 1
            For SheetRow = 0 To oSheet.Rows.Count - 1

                If ShowProgress IsNot Nothing Then
                    ShowProgress(0, oSheet.Rows.Count, SheetRow, "Desant fitxer Excel", CancelRequest)
                End If

                excelRow = SheetRow + startRow
                For col = 0 To oSheet.Rows(SheetRow).Cells.Count - 1
                    Dim cell As ExcelRange = WorkSheet.Cells(excelRow, col + 1)
                    Dim src As ExcelHelper.Cell = oSheet.Rows(SheetRow).Cells(col)

                    If src.FormulaR1C1 > "" Then
                        cell.FormulaR1C1 = src.FormulaR1C1
                    Else
                        cell.Value = src.Content
                    End If

                    If oSheet.Rows(SheetRow).Color <> Nothing Then
                        Dim oColor = oSheet.Rows(SheetRow).Color
                        cell.Style.Font.Color.SetColor(oColor)
                    End If


                    Dim sUrl As String = src.Url
                    If sUrl > "" Then
                        If Convert.ToString(src.Content) > "" Then
                            cell.Hyperlink = New Uri(sUrl)
                            With cell.Style
                                .Font.UnderLine = True
                                .Font.Color.SetColor(Color.Blue)
                            End With
                        End If
                    End If

                    If src.Indent > 0 Then
                        cell.Style.Indent = 4 * src.Indent
                    End If

                    Select Case src.Alignment
                        Case ExcelHelper.Cell.Alignments.Left
                            cell.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Left
                        Case ExcelHelper.Cell.Alignments.Center
                            cell.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center
                        Case ExcelHelper.Cell.Alignments.Right
                            cell.Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                    End Select


                    Select Case src.CellStyle
                        Case ExcelHelper.Cell.CellStyles.Bold
                            cell.Style.Font.Bold = True
                        Case ExcelHelper.Cell.CellStyles.Italic
                            cell.Style.Font.Bold = True
                        Case ExcelHelper.Cell.CellStyles.Total
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
