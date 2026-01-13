Imports Excel = Microsoft.Office.Interop.Excel
Imports OfficeOpenXml
Imports System.Globalization

Public Class ExcelHelper

    Private Shared _App As Excel.Application


    Shared Function ExcelApp(exs As List(Of Exception)) As Excel.Application
        If _App Is Nothing Then
            Try
                _App = New Excel.Application
            Catch ex As System.Exception
                exs.Add(ex)
            End Try
        End If
        Return _App
    End Function

    Shared Sub Quit()
        If _App IsNot Nothing Then
            _App.Quit()
            _App = Nothing
        End If
    End Sub

    Shared Function ExcelSheetFromFilename(sFilename As String, ByRef oBook As ExcelHelper.Book, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EPPlusHelper.ExcelSheetFromFilename(sFilename, oBook, exs)
        Return retval
    End Function

    Shared Function ExcelSheetFromFilename(sFilename As String, ByRef oSheet As ExcelHelper.Sheet, exs As List(Of Exception)) As Boolean
        Dim oBook As New ExcelHelper.Book
        Dim retval As Boolean = ExcelSheetFromFilename(sFilename, oBook, exs)
        oSheet = oBook.Sheets.First
        oSheet.Filename = sFilename
        Return retval
    End Function

    Shared Function Stream(oBook As ExcelHelper.Book, ByRef oStream As Byte(), Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean = EPPlusHelper.Stream(oBook, oStream, exs)
        Return retval
    End Function

    Shared Function Stream(oSheet As ExcelHelper.Sheet, ByRef oStream As Byte(), Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim oBook As New ExcelHelper.Book
        oBook.Sheets.Add(oSheet)
        oBook.Filename = oSheet.Filename
        Dim retval As Boolean = Stream(oBook, oStream, exs)
        Return retval
    End Function

    Shared Function Save(oBook As ExcelHelper.Book, Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean = EPPlusHelper.Save(oBook, exs)
        Return retval
    End Function

    Shared Function Save(oSheet As ExcelHelper.Sheet, Optional exs As List(Of Exception) = Nothing) As Boolean
        Dim oBook As New ExcelHelper.Book
        oBook.Sheets.Add(oSheet)
        oBook.Filename = oSheet.Filename
        Dim retval As Boolean = Save(oBook, exs)
        Return retval
    End Function


    Private Shared Function Load(WorkSheet As ExcelWorksheet, oSheet As ExcelHelper.Sheet, Optional exs As List(Of Exception) = Nothing) As Boolean
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
                    Case ExcelHelper.Sheet.NumberFormats.m3trimmed
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00 \m3;-#,##0.00 \m3;#"
                        WorkSheet.Column(col + 1).Width = 50
                    Case ExcelHelper.Sheet.NumberFormats.m3
                        WorkSheet.Column(col + 1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right
                        WorkSheet.Column(col + 1).Style.Numberformat.Format = "#,##0.00000 \m3;-#,##0.00000 \m3;#"
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

    '=============================================================

    Public Shared Function GetExcelFileFromStream(ByVal oByte() As Byte, ByVal sFilename As String, exs As List(Of Exception)) As String
        If sFilename = "" Then
            sFilename = System.Guid.NewGuid.ToString & ".xlsx"
            Dim sTmpFolder As String = IOHelper.TmpFolder & "\"
            sFilename = System.IO.Path.Combine(sTmpFolder, sFilename)
        End If

        IOHelper.SaveStream(oByte, exs, sFilename)
        Return sFilename
    End Function

    Shared Function GetImgFromExcelFirstPage(ByVal oStream As Byte(), ByRef iCols As Integer, ByRef iRows As Integer, iImageWidth As Integer, iImageHeight As Integer, exs As List(Of Exception)) As System.Drawing.Image
        Dim retval As System.Drawing.Image = Nothing
        Dim sExcelName As String = System.Guid.NewGuid.ToString & ".xlsx"
        Dim sTmpFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\"
        Dim sExcelPath As String = System.IO.Path.Combine(sTmpFolder, sExcelName)

        If IOHelper.SaveStream(oStream, exs, sExcelPath) Then
            retval = GetImgFromExcelFirstPage(sExcelPath, iCols, iRows, iImageWidth, iImageHeight, exs)
        End If

        Return retval
    End Function

    Shared Function GetImgFromExcelFirstPage(ByVal sExcelPath As String, ByRef iCols As Integer, ByRef iRows As Integer, iImageWidth As Integer, iImageHeight As Integer, exs As List(Of Exception)) As Image
        Dim retval As Image = Nothing
        Dim oApp = ExcelHelper.ExcelApp(exs)
        If exs.Count = 0 Then
            Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sExcelPath)
            Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

            iRows = oSheet.Cells(65536, 1).end(Excel.XlDirection.xlUp).row
            iCols = oSheet.Cells(1, 255).End(Excel.XlDirection.xlToLeft).column

            Dim sTmpFolder As String = IOHelper.TmpFolder & "\"
            Dim sPdfName As String = System.IO.Path.ChangeExtension(sExcelPath, ".pdf")
            Dim sPdfPath As String = System.IO.Path.Combine(sTmpFolder, sPdfName)


            oWb.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypePDF, sPdfPath)
            oWb.Close(Excel.XlSaveAction.xlDoNotSaveChanges)
            oApp.Quit()
            oApp = Nothing
            oWb = Nothing

            Dim oByteArray As Byte() = Nothing
            If FileSystemHelper.GetStreamFromFile(sPdfPath, oByteArray, exs) Then
                Dim oPdf = GhostScriptHelper.Rasterize(oByteArray, iImageWidth, iImageHeight, exs)
                retval = oPdf.Thumbnail
            End If
        End If
        Return retval
    End Function

    Shared Function GetExcelThumbnail(ByVal oStream As Byte(), exs As List(Of Exception)) As System.Drawing.Image
        Dim sExcelFilename As Object = ExcelHelper.GetExcelFileFromStream(oStream, "", exs)
        Dim sXpsFilename As String = ExcelHelper.GetXpsFileNameFromExcelFileName(exs, sExcelFilename)
        Dim oXPSHelper As XPSHelper = XPSHelper.FromFilename(sXpsFilename)
        Dim oImg As System.Drawing.Image = oXPSHelper.GenerateThumbnail()
        Return oImg
    End Function

    Public Shared Function GetXpsFileNameFromExcelFileName(exs As List(Of Exception), ByVal sExcelFilename As Object, Optional ByVal sXpsFilename As String = "") As String
        Dim oApp = ExcelHelper.ExcelApp(exs)
        If exs.Count = 0 Then
            Dim workbook As Excel.Workbook = oApp.Workbooks.Open(sExcelFilename)

            If sXpsFilename = "" Then sXpsFilename = sExcelFilename & ".xps"
            workbook.ExportAsFixedFormat(Excel.XlFixedFormatType.xlTypeXPS, sXpsFilename, From:=1, [To]:=1)
        End If

        Return sXpsFilename
    End Function

    Shared Function GetSheetNames(exs As List(Of Exception), sFilename As String) As List(Of String)
        Dim retval As New List(Of String)
        Dim oApp = ExcelHelper.ExcelApp(exs)
        If exs.Count = 0 Then
            Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sFilename)
            For Each oSheet As Excel.Worksheet In oWb.Worksheets
                retval.Add(oSheet.Name)
            Next
        End If
        Return retval
    End Function


    Shared Function GetColumnNames(exs As List(Of Exception), sFilename As String, Optional sSheetName As String = "") As List(Of String)
        Dim retval As New List(Of String)
        Dim oApp = ExcelHelper.ExcelApp(exs)
        If exs.Count = 0 Then
            Dim oWb As Excel.Workbook = oApp.Workbooks.Open(sFilename)

            Dim oSheet As Excel.Worksheet = Nothing
            If sSheetName = "" Then
                oSheet = oWb.ActiveSheet
            Else
                oSheet = oWb.Sheets(sSheetName)
            End If

            Dim LastCol As Integer = oSheet.Cells(1, oSheet.Columns.Count).End(Excel.XlDirection.xlToLeft).Column
            For iCol As Integer = 1 To LastCol
                retval.Add(oSheet.Cells(1, iCol).value)
            Next

        End If
        Return retval

    End Function



    Public Class Book
        Public Property Sheets As List(Of Sheet)
        Property Filename As String

        Public Enum UrlCods
            NotSet
            CustomerDeliveries
        End Enum

        Public Sub New(Optional sFilename As String = "")
            _Sheets = New List(Of Sheet)
            _Filename = sFilename
        End Sub
    End Class

    Public Class Sheet
        Public Property Name As String
        Public Property Filename As String
        Public Property Rows As List(Of Row)
        Public Property Columns As List(Of Column)
        Public Property DisplayTotals As Boolean

        Public Property ColumnHeadersOnFirstRow As Boolean

        Private _CultureInfo As Globalization.CultureInfo

        Public Enum NumberFormats
            NotSet
            PlainText
            [DDMMYY]
            [Integer]
            [Decimal2Digits]
            Euro
            W50
            Percent
            Kg
            m3trimmed
            m3
            mm
        End Enum

        Public Property CultureInfo As CultureInfo
            Get
                Return _Cultureinfo
            End Get
            Set(value As CultureInfo)
                _CultureInfo = value
                For Each oRow In _Rows
                    oRow.CultureInfo = _CultureInfo
                Next
            End Set
        End Property

        Public Sub New(Optional sName As String = "", Optional sFilename As String = "")
            MyBase.New()
            _Rows = New List(Of Row)
            _Columns = New List(Of Column)
            _Name = sName
            _Filename = sFilename
            _CultureInfo = CultureInfo.GetCultureInfo("es-ES")
        End Sub

        Shared Function Factory(ByVal oDs As DataSet) As Sheet
            Dim retval As New Sheet
            Dim oTb As System.Data.DataTable = oDs.Tables(0)

            Dim i As Integer = 1
            Dim j As Integer

            For j = 0 To oTb.Columns.Count - 1
                Try
                    retval.AddColumn(oTb.Columns(j).Caption)
                Catch ex As Exception
                End Try
            Next

            For Each oDataRow As DataRow In oTb.Rows
                Dim oRow As Row = retval.AddRow
                For j = 0 To oTb.Columns.Count - 1
                    Try
                        oRow.AddCell(oDataRow(j))
                    Catch ex As Exception
                    End Try
                Next
            Next

            Return retval
        End Function

        Public Function AddRow(Optional oColor As System.Drawing.Color = Nothing) As Row
            Dim retval As New Row(Me)
            retval.Color = oColor
            retval.CultureInfo = _CultureInfo
            Me.Rows.Add(retval)
            Return retval
        End Function

        Public Function AddColumn(Optional sHeader As String = "", Optional oNumberFormat As NumberFormats = NumberFormats.NotSet) As Column
            Dim oColumn As New Column(sHeader, oNumberFormat)
            _Columns.Add(oColumn)
            Return oColumn
        End Function

        Public Function AddRowWithCells(ByVal ParamArray CellTexts() As String) As Row
            Dim retval = AddRow()
            For Each sCellText As String In CellTexts
                retval.AddCell(sCellText)
            Next
            Return retval
        End Function

        Public Function MatchHeaderCaptions(ParamArray Captions() As String) As Boolean
            Dim retval As Boolean
            If _Rows.Count > 0 Then
                Dim oFirstRow = _Rows(0)
                If Captions.Count <= oFirstRow.Cells.Count Then
                    retval = True
                    For i = 0 To Captions.Count - 1
                        If oFirstRow.GetString(i).Trim <> Captions(i).Trim Then
                            retval = False
                            Exit For
                        End If
                    Next
                End If
            End If
            Return retval
        End Function

        Public Sub TrimCols(maxCols As Integer)
            For Each oRow In _Rows
                For Col = oRow.Cells.Count - 1 To maxCols - 1 Step -1
                    oRow.Cells.RemoveAt(Col)
                Next
            Next
        End Sub

    End Class


    Public Class Column
        Public Property Header As String
        Public Property NumberFormat As Sheet.NumberFormats

        Public Property ForeColor As Color
        Public Property BackColor As Color

        Public Sub New(sHeader As String, oNumberFormat As Sheet.NumberFormats)
            MyBase.New()
            _Header = sHeader
            _NumberFormat = oNumberFormat
        End Sub
    End Class


    Public Class Row
        Public Property Sheet As Sheet
        Public Property Cells As List(Of Cell)
        Public Property Color As System.Drawing.Color
        Property BackColor As Color
        Public Property CultureInfo As CultureInfo

        Public Sub New(oSheet As Sheet)
            MyBase.New()
            _Sheet = oSheet
            _Cells = New List(Of Cell)
        End Sub

        Public Function AddCell(Optional sContent As Object = Nothing, Optional sUrl As String = "", Optional oNumberFormat As Sheet.NumberFormats = Sheet.NumberFormats.NotSet) As Cell
            Dim oCell As New Cell(sContent, sUrl, oNumberFormat)
            _Cells.Add(oCell)
            Return oCell
        End Function

        Public Function AddCell(DtFch As Date) As Cell
            Dim oCell As New Cell
            If DtFch <> Nothing Then
                oCell = New Cell(DtFch, , Sheet.NumberFormats.DDMMYY)
            End If
            _Cells.Add(oCell)
            Return oCell
        End Function

        Public Function AddFormula(sFormulaR1C1 As String) As Cell
            Dim oCell As New Cell
            oCell.FormulaR1C1 = sFormulaR1C1
            _Cells.Add(oCell)
            Return oCell
        End Function

        Public Function GetString(CellIdx As Integer) As String
            Dim retval As String = ""
            If _Cells.Count > CellIdx Then
                retval = _Cells(CellIdx).Content
            End If
            Return retval
        End Function

        Public Function GetInt(CellIdx As Integer) As Integer
            Dim retval As Integer = 0
            Dim src = GetString(CellIdx)
            If IsNumeric(src) Then retval = Decimal.Parse(src, _CultureInfo)
            Return retval
        End Function

        Public Function GetDecimal(CellIdx As Integer) As Decimal
            Dim retval As Decimal = 0
            Dim src = GetString(CellIdx)
            If IsNumeric(src) Then
                retval = Decimal.Parse(src, _CultureInfo)
            End If
            Return retval
        End Function

        Public Function GetFchSpain(CellIdx As Integer) As Date
            Dim src = GetString(CellIdx)
            Dim retval As Date = TimeHelper.ParseFchSpain(src)
            Return retval.Date
        End Function
    End Class


    Public Class Cell
        Property Url As String
        Property Content As Object
        Property FormulaR1C1 As String
        Property Indent As Integer
        Property Alignment As Alignments
        Property CellStyle As CellStyles

        Property ForeColor As Color
        Property BackColor As Color


        Private _NumberFormat As Sheet.NumberFormats

        Public Enum Alignments
            NotSet
            Left
            Center
            Right
        End Enum

        Public Enum CellStyles
            NotSet
            Bold
            Italic
            Total
        End Enum

        Public Sub New(Optional oContent As Object = Nothing, Optional sUrl As String = "", Optional oNumberFormat As Sheet.NumberFormats = Sheet.NumberFormats.NotSet)
            MyBase.New()
            _Content = oContent
            If sUrl > "" Then _Url = sUrl
        End Sub

        Public Function IsEmpty() As Boolean
            Dim retval As Boolean
            If _Content Is Nothing Then
                retval = True
            ElseIf TypeOf (_Content) Is String Then
                retval = _Content.ToString.Trim = ""
            ElseIf TypeOf (_Content) Is Double Then
                retval = _Content = 0
            End If
            Return retval
        End Function

        Public Function IsNotEmpty() As Boolean
            Return Not IsEmpty()
        End Function
    End Class


    Public Class Cells
        Inherits List(Of Cell)
    End Class


    Public Class ValidationResult
        Property Row As Integer
        Property Text As String

        Property cod As Cods

        Public Enum Cods
            success
            fail
        End Enum

        Shared Function factory(row As Integer, cod As Cods, text As String) As ValidationResult
            Dim retval As New ValidationResult
            With retval
                .Row = row
                .cod = cod
                .Text = text
            End With
            Return retval
        End Function
    End Class
End Class
