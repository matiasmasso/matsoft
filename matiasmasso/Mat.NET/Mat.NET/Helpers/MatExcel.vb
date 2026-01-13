
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

    Shared Function RepClis(ByVal oRep As Rep) As Excel.Application
        Dim oApp As New Excel.Application
        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        With oSheet
            .Cells.Font.Size = 9
        End With
        Dim oTb As DataTable = oRep.ClientsDataset.Tables(0)
        Dim oRow As DataRow
        Dim i As Integer
        Dim j As Integer
        For Each oRow In oTb.Rows
            i = i + 1
            'SALTA J=0 QUE ES CLINUM en dataset i Columna prohibida al Excel
            For j = 1 To oTb.Columns.Count - 1
                oSheet.Cells(i, j) = oRow(j)
            Next
        Next
        Return oApp
    End Function

    Shared Function GetExcelFromDataset(ByVal oDs As DataSet) As Excel.Application
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        With oSheet
            .Cells.Font.Size = 9
        End With
        Dim oTb As System.Data.DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        Dim i As Integer = 1
        Dim j As Integer

        For j = 0 To oTb.Columns.Count - 1
            Try
                oSheet.Cells(i, j + 1) = oTb.Columns(j).Caption
            Catch ex As Exception
            End Try
        Next

        For Each oRow In oTb.Rows
            i = i + 1
            For j = 0 To oTb.Columns.Count - 1
                Try
                    oSheet.Cells(i, j + 1) = oRow(j)
                Catch ex As Exception
                End Try
            Next
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Return oApp
    End Function


    Shared Function GetExcelFromDataGridView(ByVal oGrid As Windows.Forms.DataGridView, Optional ByRef oProgressBar As ProgressBar = Nothing) As Excel.Application
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        With oSheet
            .Cells.Font.Size = 9
        End With
        Dim oRow As Windows.Forms.DataGridViewRow
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
            Windows.Forms.Application.DoEvents()
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
                Windows.Forms.Application.DoEvents()
            End If
        Next

        If oProgressBar IsNot Nothing Then
            oProgressBar.Visible = False
            Windows.Forms.Application.DoEvents()
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

    Shared Sub ImportArtsFromExcel()
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "excel 2007(*.xlsx)|*.xlsx|excel 97-2003(*.xls)|*.xls|All files (*.*)|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim oApp As Excel.Application = GetExcel()
                Dim oWb As Excel.Workbook = oApp.Workbooks.Open(.FileName)
                Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
                Dim iRow As Integer = 1
                Dim oArt As Art = Nothing
                Dim oEmp As New DTOEmp(1)
                Dim exs As New List(Of exception)
                'TPA,STP,CTG,ORD,MYD,EAN.REF,PRVNOM,ARD
                Do
                    iRow += 1
                    If Not IsNumeric(oSheet.Cells(iRow, 1).value2) Then
                        If oSheet.Cells(iRow, 1).value2 = "" Then Exit Do
                    End If
                    Dim oTpa As Tpa = Tpa.FromNum(oEmp, oSheet.Cells(iRow, 1).value2)
                    Dim oStp As New Stp(oTpa, oSheet.Cells(iRow, 2).value2)
                    oArt = New Art(oStp)
                    With oArt
                        .NomCurt = oSheet.Cells(iRow, 4).value2
                        .Nom_ESP = oSheet.Cells(iRow, 5).value2
                        .Cbar = oSheet.Cells(iRow, 6).value2
                        .NomPrv = oSheet.Cells(iRow, 8).value2
                        .Keys = New ArrayList
                        For Each sKey As String In MaxiSrvr.GetArrayListFromSplitCharSeparatedString(oSheet.Cells(iRow, 9).value2)
                            .Keys.Add(sKey)
                        Next
                        .Hereda = True
                        .Dimensions.Hereda = True
                        .Update(exs)
                        oSheet.Cells(iRow, 10).value2 = .Id
                    End With
                Loop
                oWb.Close()

            End If
        End With
    End Sub

    Shared Function FillRoemerTemplate(sFilename As String, exs As list(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oApp As Excel.Application = GetExcel()
        oApp.Visible = True
        Dim oWb As Excel.Workbook = Nothing

        Try
            oWb = oApp.Workbooks.Open(sFilename)
            Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

            'Dim oDefaultStyle As Excel.Style = oWb.Styles.Add("DefaultStyle")
            'oDefaultStyle.Font.Name = "Arial"
            'oDefaultStyle.Font.Size = 10

            'Dim oWarningStyle As Excel.Style = oWb.Styles.Add("WarningStyle")
            'oWarningStyle.Font.Name = "Arial"
            'oWarningStyle.Font.Size = 10
            'oWarningStyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)


            Dim oRange As Excel.Range = Nothing
            Dim iColRef As Integer = 6
            Dim iColFirstMonthQty As Integer = 9
            Dim iFirstRow As Integer = 3
            Dim iRow As Integer = iFirstRow
            Dim oRoemer As Proveidor = New Roemer
            Do
                Dim oItems As New ForecastItems
                Dim sRefProveidor As String = oSheet.Cells(iRow, iColRef).value2
                If sRefProveidor = "" Then Exit Do
                Dim SQL As String = "SELECT Yea,Mes,Qty FROM LastForecast WHERE Proveidor=@Proveidor AND REF=@Ref ORDER BY Yea,Mes"
                Dim oDrd As SqlClient.SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi, "@Proveidor", oRoemer.Guid.ToString, "@Ref", sRefProveidor)
                Do While oDrd.Read
                    oItems.Add(CInt(oDrd("Yea")), CInt(oDrd("Mes")), CInt(oDrd("Qty")))
                Loop
                oDrd.Close()

                If oItems.Count = 0 Then
                    oRange = oSheet.Range(oSheet.Cells(iRow, iColRef - 2), oSheet.Cells(iRow, iColRef))
                    'oRange.Style = "WarningStyle"
                Else
                    For i As Integer = 1 To 12
                        oRange = oSheet.Cells(iRow, iColFirstMonthQty + (2 * (i - 1)))
                        oRange.Value2 = oItems.GetQty(i)
                    Next
                End If

                iRow += 1
            Loop
            retval = True

        Catch ex As Exception
            exs.Add(ex)

        Finally
            If oWb IsNot Nothing Then
                oWb.Close()
            End If
        End Try

        Return retval
    End Function

    Shared Function GetTrpZonComparativa(ByVal oZona As Zona) As Excel.Application
        Dim oEmp As DTOEmp = BLL.BLLApp.Emp

        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        With oSheet
            .Cells.Font.Size = 9
        End With


        Dim SQL As String = "SELECT TRPZON.TrpGuid, TRPZON.TrpZon " _
        & "FROM TRPZON INNER JOIN " _
        & "TRP ON TRPZON.TrpGuid = TRP.Guid " _
        & "WHERE TRPZON.Emp =" & oEmp.Id & " AND " _
        & "TRPZON.Zona= '" & oZona.Guid.ToString & "' AND " _
        & "TRP.activat = 1 " _
        & "ORDER BY TRP.ABR"

        Dim oDrd As SqlDataReader = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        Dim oTrpZons As New TrpZons
        Do While oDrd.Read
            Dim oTrp As New Transportista(CType(oDrd("TrpGuid"), Guid))
            oTrpZons.Add(New TrpZon(oTrp, oDrd("TrpZon")))
        Loop
        oDrd.Close()

        Dim i As Integer = 1
        Dim j As Integer
        Dim iFirstTrpCol As Integer = 3
        Dim DcM3 As Decimal

        For j = 0 To oTrpZons.Count - 1
            Try
                oSheet.Cells(i, j + iFirstTrpCol) = oTrpZons(j).Transportista.Nom_o_NomComercial
            Catch ex As Exception
            End Try
        Next

        SQL = "SELECT ROUND(M3, 2) AS M3, COUNT(alb) AS FREQ " _
        & "FROM Alb " _
        & "WHERE Emp =" & oEmp.Id & " AND " _
        & "FCH > DATEADD(m, - 3, GETDATE()) AND " _
        & "M3 >= 0 " _
        & "GROUP BY ROUND(M3, 2) " _
        & "ORDER BY M3"

        oDrd = MaxiSrvr.GetDataReader(SQL, MaxiSrvr.Databases.Maxi)
        Dim BestCol As Integer
        Dim DcMinCost As Decimal
        Dim oCost As MaxiSrvr.Amt
        Do While oDrd.Read
            i = i + 1
            DcM3 = oDrd("M3")
            oSheet.Cells(i, 1) = DcM3
            oSheet.Cells(i, 2) = oDrd("FREQ")
            DcMinCost = Decimal.MaxValue
            BestCol = 0
            For j = 0 To oTrpZons.Count - 1
                oCost = oTrpZons(j).Cost(DcM3)
                If oCost IsNot Nothing Then
                    oSheet.Cells(i, j + iFirstTrpCol) = oCost.Eur
                    If (oCost.Eur < DcMinCost) Then
                        DcMinCost = oCost.Eur
                        BestCol = j + iFirstTrpCol
                    End If
                End If
            Next
            If BestCol > 0 Then
                Dim oBestRange As Excel.Range = oSheet.Cells(i, BestCol)
                oBestRange.Font.Bold = True
            End If
        Loop
        oDrd.Close()

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Return oApp

    End Function



    Shared Function FromExcelSheet(oExcelSheet As DTOExcelSheet) As Excel.Application
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        AddExcelStyles(oWb)

        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        Dim oSheetRange As Excel.Range = oSheet.UsedRange
        oSheetRange.Style = "BaseStyle"

        Dim iRow As Integer = 1
        Dim iCol As Integer

        For Each oColumn As DTOExcelColumn In oExcelSheet.Columns
            iCol = oExcelSheet.Columns.IndexOf(oColumn) + 1

            Dim oColumnRange As Excel.Range = oSheet.Columns(iCol)
            oColumnRange.EntireColumn.AutoFit()
            If oColumn.Style <> DTOExcelSheet.Styles.NotSet Then
                oColumnRange.Style = oWb.Styles(oColumn.Style.ToString)
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
                Dim oRange As Excel.Range = oSheet.Cells(iRow, iCol)
                If oCell.Content IsNot Nothing Then
                    If oCell.Url > "" Then
                        oSheet.Hyperlinks.Add(oRange, oCell.Url, , , oCell.Content.ToString)
                    Else
                        oRange.Value = oCell.Content
                    End If
                End If
            Next
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Return oApp
    End Function

    Shared Sub AddExcelStyles(oWb As Excel.Workbook)
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
        With oWb.Styles.Add(Name:=DTOExcelSheet.Styles.DDMMYY.ToString)
            .NumberFormatLocal = "dd/mm/aa"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        End With

    End Sub


    Shared Function Extracte(oCcbs As Ccbs) As Excel.Application
        Dim oApp As New Excel.Application()
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet
        oSheet.Cells.Font.Size = 9
        With oWb.Styles.Add(Name:="Extracte")
            .NumberFormatLocal = "#.##0,00;#,##0.00;#"
            .HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        End With

        oSheet.Columns("D:F").Style = "Extracte"

        Dim oCells As New ArrayList
        oCells.Add("registre")
        oCells.Add("data")
        oCells.Add("concepte")
        oCells.Add("deure")
        oCells.Add("haver")
        oCells.Add("saldo")
        AddRow(oSheet, oCells)

        For Each oCcb As Ccb In oCcbs
            oCells = New ArrayList
            oCells.Add(oCcb.Cca.Id.ToString)
            oCells.Add(oCcb.Cca.fch.ToShortDateString)

            If oCcb.Cca.DocExists Then
                Dim oCell As New DTOExcelCell(oCcb.Cca.Txt.ToString, BLL.BLLDocFile.DownloadUrl(oCcb.Cca.DocFile, True))
                oCells.Add(oCell)
            Else
                oCells.Add(oCcb.Cca.Txt.ToString)
            End If

            If oCcb.Dh = DTOCcb.DhEnum.Debe Then
                oCells.Add(Math.Abs(oCcb.Amt.Eur))
                oCells.Add(0)
            Else
                oCells.Add(0)
                oCells.Add(Math.Abs(oCcb.Amt.Eur))
            End If
            AddRow(oSheet, oCells)
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        Return oApp
    End Function
End Class


