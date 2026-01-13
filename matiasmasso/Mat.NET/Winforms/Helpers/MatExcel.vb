
Imports System.Data.SqlClient
Imports Excel = Microsoft.Office.Interop.Excel

Public Class MatExcel
    Private mExcel As Excel.Application
    Private mApp As Excel.Application
    Private mWb As Excel.Workbook
    Private mSheet As Excel.Worksheet
    Private mCultureInfoOriginal As System.Globalization.CultureInfo

    Public Sub New()
        MyBase.New()
        mApp = GetExcel()
        mApp.UserControl = True
        mCultureInfoOriginal = System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")

        mWb = mApp.Workbooks.Add()
        mSheet = mWb.ActiveSheet
        With mSheet
            .Cells.Font.Size = 9
        End With
    End Sub

    Shared Function Read(sFileName As String) As DTOExcelBook
        Dim retval As New DTOExcelBook
        Dim oExcel As Excel.Application = GetExcel()
        Dim w As Excel.Workbook = oExcel.Workbooks.Open(sFileName)
        For i As Integer = 1 To w.Sheets.Count
            Dim oSheet As New DTOExcelSheet
            retval.Sheets.Add(oSheet)

            Dim sheet As Excel.Worksheet = w.Sheets(i)
            Dim r As Excel.Range = sheet.UsedRange
            Dim iCellsCount As Integer = r.Cells.Count
            Dim iRowsCount As Integer = r.Rows.Count

            ' Load all cells into 2d array.
            Dim array(,) As Object = r.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)

            ' Scan the cells.
            If array IsNot Nothing Then

                ' Get bounds of the array.
                Dim bound0 As Integer = array.GetUpperBound(0)
                Dim bound1 As Integer = array.GetUpperBound(1)

                ' Loop over all elements.
                For j As Integer = 1 To bound0
                    Dim oRow As New DTOExcelRow(oSheet)
                    oSheet.Rows.Add(oRow)

                    For x As Integer = 1 To bound1
                        Dim oCell As New DTOExcelCell(array(j, x))
                        oRow.Cells.Add(oCell)
                    Next
                Next
            End If
        Next

        w.Close()
        Return retval
    End Function

    Shared Function Read2(sFileName As String) As DTOExcelBook
        Dim retval As New DTOExcelBook
        Dim oExcel As Excel.Application = GetExcel()
        Dim w As Excel.Workbook = oExcel.Workbooks.Open(sFileName)
        For i As Integer = 1 To w.Sheets.Count
            Dim oSheet As New DTOExcelSheet
            retval.Sheets.Add(oSheet)

            Dim sheet As Excel.Worksheet = w.Sheets(i)
            Dim r As Excel.Range = sheet.UsedRange
            Dim iCellsCount As Integer = r.Cells.Count
            Dim iRowsCount As Integer = r.Rows.Count

            ' Load all cells into 2d array.
            Dim array(,) As Object = r.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)

            ' Scan the cells.
            If array IsNot Nothing Then

                ' Get bounds of the array.
                Dim bound0 As Integer = array.GetUpperBound(0)
                Dim bound1 As Integer = array.GetUpperBound(1)

                ' Loop over all elements.
                For j As Integer = 1 To bound0
                    Dim oRow As New DTOExcelRow(oSheet)
                    oSheet.Rows.Add(oRow)

                    For x As Integer = 1 To bound1
                        Dim oCell As New DTOExcelCell(array(j, x))
                        oRow.Cells.Add(oCell)
                    Next
                Next
            End If
        Next

        w.Close()
        Return retval
    End Function

    Public Sub AddRow(Optional oMatCellsArray As ArrayList = Nothing)
        Static RowIndex As Integer
        RowIndex += 1

        If oMatCellsArray IsNot Nothing Then
            Dim ColIndex As Integer = 0
            For Each oCell As Object In oMatCellsArray
                ColIndex += 1
                Dim oRange As Excel.Range = mSheet.Cells(RowIndex, ColIndex)

                If TypeOf (oCell) Is DTOExcelCell Then
                    If oCell.Content IsNot Nothing Then
                        If oCell.Url > "" Then
                            mSheet.Hyperlinks.Add(oRange, oCell.Url, , , oCell.Content.ToString)
                        Else
                            oRange.Value = oCell.Content
                        End If
                    End If
                Else
                    'admet cadenes o numerics
                    oRange.Value = oCell
                End If
            Next
        End If
    End Sub


    Shared Sub AddRow(oSheet As Excel.Worksheet, Optional oMatCellsArray As ArrayList = Nothing)
        Static RowIndex As Integer
        RowIndex += 1

        If oMatCellsArray IsNot Nothing Then
            Dim ColIndex As Integer = 0
            For Each oCell As Object In oMatCellsArray
                ColIndex += 1
                Dim oRange As Excel.Range = oSheet.Cells(RowIndex, ColIndex)

                If TypeOf (oCell) Is DTOExcelCell Then
                    If oCell.Content IsNot Nothing Then
                        If oCell.Url > "" Then
                            oSheet.Hyperlinks.Add(oRange, oCell.Url, , , oCell.Content.ToString)
                        Else
                            oRange.Value = oCell.Content
                        End If
                    End If
                Else
                    'admet cadenes o numerics
                    oRange.Value = oCell
                End If
            Next
        End If
    End Sub


    Public Function Application() As Excel.Application
        System.Threading.Thread.CurrentThread.CurrentCulture = mCultureInfoOriginal
        Return mApp
    End Function

    Shared Function GetExcel() As Excel.Application
        Try
            ' Getobject function called without the first argument returns a
            ' reference to an instance of the application. 
            ' If the application is not running, an error occurs.
            GetExcel = GetObject(, "Excel.Application")
        Catch ex As Exception
            GetExcel = New Excel.Application
        End Try
    End Function

    Shared Function GetExcelFromDataGridView(ByVal oGrid As System.Windows.Forms.DataGridView, Optional ByRef oProgressBar As ProgressBar = Nothing) As Excel.Application
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo =
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture =
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        With oSheet
            .Cells.Font.Size = 9
        End With
        Dim oRow As System.Windows.Forms.DataGridViewRow
        Dim i As Integer = 1
        Dim j As Integer

        Dim xColIdx As Integer
        Dim xCols() As Integer
        ReDim xCols(oGrid.Columns.Count)

        For j = 0 To oGrid.Columns.Count - 1
            Try
                If oGrid.Columns(j).Visible Then
                    xCols(j) = xColIdx
                    oSheet.Cells(i, xCols(j) + 1) = oGrid.Columns(j).HeaderText
                    xColIdx += 1
                End If
            Catch ex As Exception
            End Try
        Next

        If oProgressBar IsNot Nothing Then
            With oProgressBar
                .Maximum = oGrid.Rows.Count
                .Value = 0
                .Visible = True
            End With
            System.Windows.Forms.Application.DoEvents()
        End If

        Dim GridRowIndex As Integer = 0
        For Each oRow In oGrid.Rows
            i = i + 1
            For j = 0 To oGrid.Columns.Count - 1
                Try
                    If oGrid.Columns(j).Visible Then
                        oSheet.Cells(i, xCols(j) + 1) = oGrid.Rows(GridRowIndex).Cells(j).Value
                    End If
                Catch ex As Exception
                End Try
            Next
            GridRowIndex += 1
            If oProgressBar IsNot Nothing Then
                oProgressBar.Increment(1)
                System.Windows.Forms.Application.DoEvents()
            End If
        Next

        If oProgressBar IsNot Nothing Then
            oProgressBar.Visible = False
            System.Windows.Forms.Application.DoEvents()
        End If

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Return oApp
    End Function


    Shared Function CopyLinksToClipboard(ByVal oArrayText As ArrayList, ByVal oArrayDocFile As ArrayList) As Excel.Application
        Dim oApp As New Excel.Application
        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        Dim oRange As Excel.Range = Nothing
        Dim oDocFile As DTODocFile = Nothing
        Dim sUrl As String = ""
        Dim i As Integer

        For i = 1 To oArrayText.Count
            'oSheet.Cells(i, 2) = oarrayUrl(i - 1)
            oRange = oSheet.Cells(i, 1)
            oDocFile = oArrayDocFile(i - 1)
            If oDocFile IsNot Nothing Then
                sUrl = BLL.BLLDocFile.DownloadUrl(oDocFile, True)
                oSheet.Hyperlinks.Add(oRange, sUrl, , , oArrayText(i - 1))
            Else
                oRange.Value = oArrayText(i - 1)
            End If
        Next

        oRange = oSheet.Range(oSheet.Cells(1, 1), oSheet.Cells(i - 1, 1))
        oRange.Columns.AutoFit()
        'oRange.Copy()
        'oWb.Close(False)
        'oApp = Nothing
        Return oApp
    End Function



    Shared Function FromExcelBook(oExcelBook As DTOExcelBook) As Excel.Application
        Dim oApp As New Excel.Application()
        Dim oRange As Excel.Range = Nothing
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo =
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture =
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        AddExcelStyles(oWb)

        'oWb.Sheets.Delete()

        For Each oExcelSheet As DTOExcelSheet In oExcelBook.Sheets

            Dim oSheet As Excel.Worksheet = Nothing
            If oExcelSheet.Equals(oExcelBook.Sheets.First) Then
                oSheet = oWb.ActiveSheet
            Else
                oSheet = oWb.Sheets.Add()
            End If

            If oExcelSheet.Name > "" Then
                oSheet.Name = oExcelSheet.Name
            End If

            Dim oSheetRange As Excel.Range = oSheet.UsedRange
            oSheetRange.Style = "BaseStyle"

            Dim iRow As Integer = 1
            Dim iCol As Integer

            For Each oColumn As DTOExcelColumn In oExcelSheet.Columns
                iCol = oExcelSheet.Columns.IndexOf(oColumn) + 1

                Dim oColumnRange As Excel.Range = oSheet.Columns(iCol)
                oColumnRange.EntireColumn.AutoFit()
                If oColumn.Style <> DTOExcelSheet.Styles.NotSet Then
                    Try
                        oColumnRange.Style = oWb.Styles(oColumn.Style.ToString)
                    Catch ex As Exception
                        BLLWinBug.Log("Excel.Range.Styles('" & oColumn.Style.ToString & "') no existeix (MatExcel.FromExcelBook)")
                    End Try
                End If

                If oColumn.Header > "" Then
                    Dim oCellRange As Excel.Range = oSheet.Cells(iRow, iCol)
                    oCellRange.Value = oColumn.Header
                End If

            Next

            For Each oRow As DTOExcelRow In oExcelSheet.Rows
                iRow += 1
                iCol = 0
                For Each oCell As DTOExcelCell In oRow.Cells
                    iCol += 1
                    oRange = oSheet.Cells(iRow, iCol)
                    If oCell.Content IsNot Nothing Then
                        If oCell.Url > "" Then
                            oSheet.Hyperlinks.Add(oRange, oCell.Url, , , oCell.Content.ToString)
                        Else
                            oRange.Value = oCell.Content
                        End If
                    End If
                Next
            Next

            oSheet.Columns.AutoFit()
        Next


        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Return oApp
    End Function

    Shared Function FromExcelSheet(oExcelSheet As DTOExcelSheet) As Excel.Application
        Dim oBook As New DTOExcelBook
        oBook.Sheets.Add(oExcelSheet)
        Dim retval As Excel.Application = FromExcelBook(oBook)
        Return retval
    End Function

    Shared Sub AddExcelStyles(oWb As Excel.Workbook)
        'oWb.Styles.clear
        With oWb.Styles.Add(Name:="BaseStyle")
            .Font.Size = 9
        End With
        With oWb.Styles.Add(Name:=DTOExcelSheet.Styles.PlainText.ToString)
            .NumberFormatLocal = "@"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft
        End With
        With oWb.Styles.Add(Name:=DTOExcelSheet.Styles.Integer.ToString)
            .NumberFormatLocal = "#"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        End With
        With oWb.Styles.Add(Name:=DTOExcelSheet.Styles.Decimal2Digits.ToString)
            .NumberFormatLocal = "#.##0,00 ;[Rojo]-#.##0,00 ;#"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        End With
        With oWb.Styles.Add(Name:=DTOExcelSheet.Styles.Euro.ToString)
            .NumberFormatLocal = "#.##0,00 €;[Rojo]-#.##0,00 €;#"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        End With
        With oWb.Styles.Add(Name:=DTOExcelSheet.Styles.DDMMYY.ToString)
            .NumberFormatLocal = "dd/mm/aa"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        End With

    End Sub


End Class


