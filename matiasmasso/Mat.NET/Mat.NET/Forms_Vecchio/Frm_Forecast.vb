
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Forecast
    Private mEmp as DTOEmp
    Private mTpa As Tpa
    Private mStp As Stp
    Private mProveidor As Proveidor
    Private mYea As Integer = Today.Year

    Private mDsFcast As DataSet
    Private mDsPro As DataSet
    Private mMode As Modes
    Private mAllowEvents As Boolean
    Private mAllowEventsPro As Boolean
    Private mAllowEventsControl As Boolean

    Private mMaxRows(20) As Integer
    Private mMinRows(20) As Integer

    Private Enum RowsFcast
        SumFcast
        SumSales
        Fcast
        Sales
    End Enum

    Private Enum ColsFcast
        StpNom
        StpOrd
        ArtNom
        ArtId
        CodLin
        LastProduction
        Tot
        M1
    End Enum

    Private Enum ColsPro
        ArtId
        StpNom
        ArtNom
        Warn
        Optimitzat
        Pro
        Stk
        Pn1
        Pn2
        Pre
        M3
        M3X
        MinPack
        OutPack
    End Enum

    Private Enum ColsControl
        ArtId
        StpNom
        ArtNom
        M0Prev
        M0Tmp
        M0Sales
        M0Pct
        M1Prev
        M1Sales
        M1Pct
        M2Prev
        M2Sales
        M2Pct
    End Enum

    Private Enum Modes
        None
        Tpa
        Stp
        Cat
    End Enum

    Public WriteOnly Property Tpa() As Tpa
        Set(ByVal value As Tpa)
            mTpa = value
            mEmp = mTpa.emp
            mMode = Modes.Tpa
            Me.Text = "FORECAST " & mTpa.Nom
            LoadYeas()
            If ComboBoxYeas.Items.Count > 0 Then
                LoadGridFcast()
            End If
            mProveidor = mTpa.Proveidor
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property Stp() As Stp
        Set(ByVal value As Stp)
            mStp = value
            mEmp = mStp.Tpa.emp
            mMode = Modes.Stp
            Me.Text = "FORECAST " & mStp.Tpa.Nom & "/" & mStp.Nom
            LoadYeas()
            If ComboBoxYeas.Items.Count > 0 Then
                LoadGridFcast()
            End If
            mProveidor = mStp.Tpa.Proveidor
            mAllowEvents = True
        End Set
    End Property

    Public WriteOnly Property Yea() As Integer
        Set(ByVal value As Integer)
            mYea = value
        End Set
    End Property

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedTab.Text

            Case TabPagePro.Text
                Static Loaded_Pro As Boolean
                If Not Loaded_Pro Then
                    Loaded_Pro = True
                    DateTimePickerDeliver.Value = DateTimePickerDeliver.Value.AddDays(NumericUpDownDelivery.Value)
                    DateTimePickerNextOrder.Value = DateTimePickerDeliver.Value.AddDays(NumericUpDownNextOrder.Value)
                    LoadGridPro()
                    mAllowEventsPro = True
                End If

            Case TabPageControl.Text
                Static Loaded_Control As Boolean
                If Not Loaded_Control Then
                    Loaded_Control = True
                    'DateTimePickerDeliver.Value = DateTimePickerDeliver.Value.AddDays(NumericUpDownDelivery.Value)
                    LoadGridControl()
                    mAllowEventsControl = True
                End If
        End Select
    End Sub

#Region "ForeCast"

    Private Sub LoadYeas()
        With ComboBoxYeas
            .Items.Clear()
            .Items.Add(Today.Year - 1)
            .Items.Add(Today.Year)
            .Items.Add(Today.Year + 1)
            .Items.Add(Today.Year + 2)
            .SelectedIndex = 1
        End With
    End Sub

    Private Sub LoadGridFcast()
        Cursor = Cursors.WaitCursor

        Dim SQL As String = "SELECT STP.DSC,STP.ORD,ART.ORD,ART.ART, " _
        & RowsFcast.Fcast & " AS CODLIN, ART.LASTPRODUCTION " _
        & ",SUM(LASTFORECAST.qty) AS TOT "

        For i As Integer = 1 To 12
            SQL += ", SUM(CASE WHEN LASTFORECAST.MES =" & i.ToString & " THEN LASTFORECAST.QTY ELSE 0 END) AS M" & i.ToString & " "
        Next

        SQL += "FROM  ART INNER JOIN " _
        & "STP ON ART.Category = STP.Guid LEFT OUTER JOIN " _
        & "(SELECT F1.EMP,F1.ART,F1.YEA,F1.MES,F1.QTY FROM FORECAST AS F1 INNER JOIN " _
        & "(SELECT EMP,ART,YEA,MES,MAX(fch) AS LASTFCH FROM FORECAST GROUP BY EMP,ART,YEA,MES) AS F2 " _
        & "ON F1.EMP=F2.EMP AND F1.ART=F2.ART AND F1.YEA=F2.YEA AND F1.MES=F2.MES AND F1.fch=F2.LASTFCH) AS LASTFORECAST " _
        & "ON LASTFORECAST.Emp = ART.emp AND LASTFORECAST.art = ART.art AND LASTFORECAST.yea =" & CurrentYea() & " " _
        & "WHERE ART.emp =" & mEmp.Id & " "
        '& " and ART.NOWEB=0 "

        Select Case mMode
            Case Modes.Tpa
                SQL = SQL & "AND Stp.Brand='" & mTpa.Guid.ToString & "' "
            Case Modes.Stp
                SQL = SQL & "AND ART.Category='" & mStp.Guid.ToString & "' "
        End Select

        If CheckBoxHideLastInProduction.Checked Then
            SQL = SQL & "AND ART.LASTPRODUCTION=0 "
        End If

        SQL = SQL & " GROUP BY STP.ord, ART.art, STP.dsc, ART.ord, ART.OBSOLETO, ART.LASTPRODUCTION "
        If CheckBoxHideObsoletos.Checked Then
            SQL = SQL & "HAVING SUM(LASTFORECAST.qty)>0 or (ART.OBSOLETO=0 AND ART.LASTPRODUCTION=0) "
        End If


        If Not CheckBoxHideSales.Checked Then
            SQL = SQL & "UNION " _
           & "SELECT  STP.DSC,STP.ORD,ART.ORD,ART.ART, " _
           & RowsFcast.Sales & " AS CODLIN,0 as LASTPRODUCTION, " _
           & "SUM(PNC.qty) AS TOT "

            For i As Integer = 1 To 12
                SQL += ", SUM(CASE WHEN MONTH(PDC.FCH) =" & i.ToString & " THEN PNC.QTY ELSE 0 END) AS M" & i.ToString & " "
            Next

            SQL = SQL & "FROM  PNC INNER JOIN " _
            & "ART ON PNC.ArtGuid= ART.Guid INNER JOIN " _
            & "STP ON ART.Category = STP.Guid INNER JOIN " _
            & "PDC ON PNC.PdcGuid = PDC.Guid " _
            & "WHERE ART.emp =" & mEmp.Id & " AND " _
            & "PdC.yea =" & CurrentYea() & " AND " _
            & "PNC.Cod =" & DTOPurchaseOrder.Codis.client & " "

            Select Case mMode
                Case Modes.Tpa
                    SQL = SQL & "AND Stp.Brand='" & mTpa.Guid.ToString & "' "
                Case Modes.Stp
                    SQL = SQL & "AND Stp.Guid='" & mStp.Guid.ToString & "' " _

            End Select

            If CheckBoxHideLastInProduction.Checked Then
                SQL = SQL & "AND ART.LASTPRODUCTION=0 "
            End If

            SQL = SQL & "GROUP BY STP.ord, ART.art, STP.dsc, ART.ord "
        End If

        SQL = SQL & "ORDER BY STP.DSC,STP.ORD,ART.ORD,ART.ART,CODLIN"

        mDsFcast = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsFcast.Tables(0)

        Dim iCol As Integer
        Dim oSumRow As DataRow = oTb.NewRow
        oSumRow(ColsFcast.CodLin) = RowsFcast.SumFcast
        For iCol = ColsFcast.Tot To oTb.Columns.Count - 1
            oSumRow(iCol) = 0
        Next

        Dim oSum2Row As DataRow = Nothing
        If Not CheckBoxHideSales.Checked Then
            oSum2Row = oTb.NewRow
            oSum2Row(ColsFcast.CodLin) = RowsFcast.SumSales
            For iCol = ColsFcast.Tot To oTb.Columns.Count - 1
                oSum2Row(iCol) = 0
            Next
        End If


        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            For iCol = ColsFcast.Tot To oTb.Columns.Count - 1
                If Not IsDBNull(oRow(iCol)) Then
                    If oRow(ColsFcast.CodLin) = RowsFcast.Fcast Then
                        oSumRow(iCol) += oRow(iCol)
                    ElseIf oRow(ColsFcast.CodLin) = RowsFcast.Sales Then
                        oSum2Row(iCol) += oRow(iCol)
                    End If
                End If
            Next
        Next

        If Not CheckBoxHideSales.Checked Then
            oSum2Row(ColsFcast.ArtNom) = "totals venut"
            oTb.Rows.InsertAt(oSum2Row, 0)
        End If

        oSumRow(ColsFcast.ArtNom) = "totals previsions"
        oTb.Rows.InsertAt(oSumRow, 0)

        With DataGridViewFcast
            With .RowTemplate
                .Height = DataGridViewFcast.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .MultiSelect = True
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .BackgroundColor = Color.White

            If .Rows.Count > 0 Then
                With .Rows(0)
                    .DefaultCellStyle.BackColor = Color.WhiteSmoke
                End With
            End If
            With .Columns(ColsFcast.StpNom)
                .HeaderText = "categoría"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsFcast.StpOrd)
                .Visible = False
            End With
            With .Columns(ColsFcast.ArtNom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsFcast.ArtId)
                .Visible = False
            End With
            With .Columns(ColsFcast.CodLin)
                .Visible = False
            End With
            With .Columns(ColsFcast.LastProduction)
                .Visible = False
            End With

            With .Columns(ColsFcast.Tot)
                .HeaderText = "total"
                .Width = 50
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            Dim oLang As DTOLang = BLL.BLLSession.Current.Lang
            For i As Integer = ColsFcast.M1 To ColsFcast.M1 + 11
                With .Columns(i)
                    .HeaderText = oLang.MesAbr(i - ColsFcast.M1 + 1)
                    .Width = 35
                    .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                    .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                    .DefaultCellStyle.Format = "#,###"
                End With
            Next
        End With
        Cursor = Cursors.Default

        'setcolors
        If Not CheckBoxHideSales.Checked Then

            Dim oDgRowFcast As DataGridViewRow
            Dim oDgRowSales As DataGridViewRow
            Dim oGrid As DataGridView = DataGridViewFcast
            Dim iRow As Integer

            'reset indexes files minimes i maximes
            For iCol = 0 To 20
                mMaxRows(iCol) = -1
                mMinRows(iCol) = -1
            Next

            For iCol = ColsFcast.Tot To oGrid.Columns.Count - 1
                Dim DcMinRate As Decimal = 0
                Dim iMinRow As Integer = 0
                Dim DcMaxRate As Decimal = 0
                Dim iMaxRow As Integer = 0
                Dim DcRate As Decimal = 0
                For iRow = 2 To oGrid.Rows.Count - 2 Step 2
                    oDgRowFcast = oGrid.Rows(iRow)
                    oDgRowSales = oGrid.Rows(iRow + 1)
                    If oDgRowFcast.Cells(ColsFcast.ArtId).Value = oDgRowSales.Cells(ColsFcast.ArtId).Value Then
                        If Not IsDBNull(oDgRowFcast.Cells(iCol).Value) And Not IsDBNull(oDgRowSales.Cells(iCol).Value) Then
                            'evita si el mes está buit
                            If oDgRowFcast.Cells(iCol).Value <> 0 And oSum2Row(iCol) > 0 Then
                                Dim DcSales As Decimal = oDgRowSales.Cells(iCol).Value
                                If DcSales = 0 Then DcSales = 0.5 'dona-li algo per poder comparar diferencies de forecasts amb vendes zero
                                DcRate = (DcSales / oDgRowFcast.Cells(iCol).Value)
                                If DcRate > 1 Then
                                    If DcRate > 0 And DcRate > DcMaxRate Then
                                        DcMaxRate = DcRate
                                        iMaxRow = iRow
                                    End If
                                Else
                                    If DcMinRate = 0 Or DcRate < DcMinRate Then
                                        DcMinRate = DcRate
                                        iMinRow = iRow
                                    End If
                                End If
                            End If

                        End If
                    End If
                Next

                If DcMaxRate > 0 Then
                    mMaxRows(iCol) = iMaxRow
                End If
                If DcMinRate > 0 Then
                    mMinRows(iCol) = iMinRow
                End If
            Next

        End If

    End Sub

    Private Function CurrentYea()
        'Return 2006
        Return ComboBoxYeas.Text
    End Function

    Private Sub ComboBoxYeas_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If mAllowEvents Then
            LoadGridFcast()
        End If
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
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
        Dim oTb As System.Data.DataTable = mDsFcast.Tables(0)
        Dim oRow As DataRow
        Dim oCell As Excel.Range
        Dim i As Integer
        Dim j As Integer
        i = i + 1
        oSheet.Cells(i, ColsFcast.Tot) = "TOTALS"
        For j = 1 To 12
            oCell = oSheet.Cells(i, ColsFcast.M1 + j)
            oCell.Value = BLL.BLLApp.Lang.MesAbr(j)
            oCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight
        Next

        Dim iSumRow As Integer = i + 1
        For Each oRow In oTb.Rows
            i = i + 1
            'oCell = oSheet.Cells(iSumRow, ColsFcast.M1 + j)
            'oCell.FormulaR1C1 = sFormula
            'oCell.NumberFormat = "#,##0;-#,##0;#"
            ' If oRow(ColsFcast.LastProduction) Then
            'Dim oRowRange As Excel.Range = oSheet.Cells(i, 0).row
            'oRowRange.FormatConditions.Add(
            'End If
            For j = 0 To oTb.Columns.Count - 1
                oSheet.Cells(i, j + 1) = oRow(j)
            Next
        Next
        Dim iLastRow = i
        Dim sFormula As String = "=SUM(R[+1]C:R[+" & iLastRow - iSumRow & "]C)"
        For j = 1 To 12
            oCell = oSheet.Cells(iSumRow, ColsFcast.M1 + j)
            oCell.FormulaR1C1 = sFormula
            oCell.NumberFormat = "#,##0;-#,##0;#"
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        oApp.Visible = True
    End Sub


    Private Function CurrentArt() As Art
        Dim oArt As Art = Nothing
        Dim oRow As DataGridViewRow = DataGridViewFcast.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(ColsFcast.ArtId).Value) Then
                Dim ArtId As Integer = oRow.Cells(ColsFcast.ArtId).Value
                oArt = MaxiSrvr.Art.FromNum(mEmp, ArtId)
            End If
        End If
        Return oArt
    End Function

    Private Function CurrentArts() As Arts
        Dim oArts As New Arts
        If DataGridViewFcast.SelectedRows.Count = 0 Then
            oArts.Add(CurrentArt)
        Else
            For Each oRow As DataGridViewRow In DataGridViewFcast.SelectedRows
                If Not IsDBNull(oRow.Cells(ColsFcast.ArtId).Value) Then
                    Dim ArtId As Integer = oRow.Cells(ColsFcast.ArtId).Value
                    oArts.Add(MaxiSrvr.Art.FromNum(mEmp, ArtId))
                End If
            Next
        End If
        Return oArts
    End Function

    Private Sub DataGridViewFcast_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewFcast.CellFormatting
        If mMaxRows(e.ColumnIndex) = e.RowIndex Then
            e.CellStyle.BackColor = Color.FromArgb(204, 255, 153) 'verd palid
        ElseIf mMinRows(e.ColumnIndex) = e.RowIndex Then
            e.CellStyle.BackColor = Color.FromArgb(255, 204, 153) 'salmo palid
        End If
    End Sub

    Private Sub DataGridViewFcast_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewFcast.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCurrentCell As DataGridViewCell = DataGridViewFcast.CurrentCell
        If oCurrentCell IsNot Nothing Then
            If oCurrentCell.RowIndex > 0 Then
                Select Case oCurrentCell.ColumnIndex
                    Case ColsFcast.ArtNom
                        If Not IsDBNull(DataGridViewFcast.CurrentRow.Cells(ColsFcast.ArtId).Value) Then
                            Dim oArts As Arts = CurrentArts()
                            Dim oMenuArt As New Menu_Art(oArts)
                            AddHandler oMenuArt.AfterUpdate, AddressOf RefreshRequestFcast
                            oContextMenu.Items.AddRange(oMenuArt.Range)
                        End If
                    Case Is > ColsFcast.Tot
                        oContextMenu.Items.Add("Copiar", My.Resources.Copy, AddressOf CellCopy)
                        oContextMenu.Items.Add("Pegar", Nothing, AddressOf CellPaste)
                End Select
            End If
        End If

        DataGridViewFcast.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub ToolStripButtonFCastRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFCastRefresca.Click
        LoadGridFcast()
    End Sub

    Private Sub DataGridViewFcast_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewFcast.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridViewFcast.Rows(e.RowIndex)
        Dim oBackColor As Color = Color.WhiteSmoke
        If Not IsDBNull(oRow.Cells(ColsFcast.LastProduction).Value) Then
            Dim BlLastProduction As Boolean = oRow.Cells(ColsFcast.LastProduction).Value
            If BlLastProduction Then
                oBackColor = Color.LightSalmon
            End If
        End If
        oRow.DefaultCellStyle.BackColor = oBackColor
        Select Case CType(oRow.Cells(ColsFcast.CodLin).Value, RowsFcast)
            Case RowsFcast.Sales, RowsFcast.SumSales
                oRow.DefaultCellStyle.ForeColor = Color.Navy
        End Select
    End Sub

    Private Sub CellCopy(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub CellPaste(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim tblExcelData As New DataTable
        Dim iLoopCounter As Integer


        Dim objPresumablyExcel As IDataObject = Clipboard.GetDataObject()
        If (objPresumablyExcel.GetDataPresent(DataFormats.CommaSeparatedValue)) Then

            Dim srReadExcel As New System.IO.StreamReader(CType(objPresumablyExcel.GetData(DataFormats.CommaSeparatedValue), System.IO.Stream))

            'The next task would be to read this stream of data one line at a time. A loop is the order of the day.
            While (srReadExcel.Peek() > 0)
                Dim sFormattedData As String = srReadExcel.ReadLine()
                Dim arrSplitData As Array = sFormattedData.Split(";")
                'The number of Array items is equivalent to the number of columns of data copied from the Excel Sheet.
                If tblExcelData.Columns.Count <= 0 Then
                    For iLoopCounter = 0 To arrSplitData.GetUpperBound(0)
                        tblExcelData.Columns.Add()
                    Next
                End If

                Dim oRow As DataRow = tblExcelData.NewRow()
                For iLoopCounter = 0 To arrSplitData.GetUpperBound(0)
                    oRow(iLoopCounter) = arrSplitData.GetValue(iLoopCounter)
                Next
                tblExcelData.Rows.Add(oRow)
            End While
        End If

        Dim iRowMax As Integer = 0
        Dim iRowMin As Integer = Integer.MaxValue
        Dim iColMax As Integer = 0
        Dim iColMin As Integer = Integer.MaxValue

        Dim oCells As DataGridViewSelectedCellCollection = DataGridViewFcast.SelectedCells
        Dim oCell As DataGridViewCell
        For Each oCell In oCells
            If oCell.RowIndex < iRowMin Then iRowMin = oCell.RowIndex
            If oCell.RowIndex > iRowMax Then iRowMax = oCell.RowIndex
            If oCell.ColumnIndex < iColMin Then iColMin = oCell.ColumnIndex
            If oCell.ColumnIndex > iColMax Then iColMax = oCell.ColumnIndex
        Next

        Dim SQL As String
        Dim ArtId As Integer
        Dim iMes As Integer
        Dim iQty As Integer

        For iTbRow As Integer = 0 To tblExcelData.Rows.Count - 1
            For iTbCol As Integer = 0 To tblExcelData.Columns.Count - 1
                If iTbRow + iRowMin < DataGridViewFcast.Rows.Count Then
                    Dim oRow As DataGridViewRow = DataGridViewFcast.Rows(iTbRow + iRowMin)
                    Dim oCellArt As DataGridViewCell = oRow.Cells(ColsControl.ArtId)
                    'If oCellArt
                    ArtId = DataGridViewFcast.Rows(iTbRow + iRowMin).Cells(ColsFcast.ArtId).Value
                    iMes = (iColMin - ColsFcast.M1) + iTbCol + 1

                    If iMes > 0 And iMes < 13 Then
                        If IsNumeric(tblExcelData.Rows(iTbRow)(iTbCol)) Then
                            iQty = tblExcelData.Rows(iTbRow)(iTbCol)

                            SQL = "INSERT INTO FORECAST(EMP,ART,YEA,MES,QTY) VALUES (" _
                            & mEmp.Id & ", " _
                            & ArtId & ", " _
                            & CurrentYea() & ", " _
                            & iMes & ", " _
                            & iQty & ")"
                            MaxiSrvr.ExecuteNonQuery(SQL, MaxiSrvr.Databases.Maxi)
                        End If
                    End If
                End If
            Next
        Next

        RefreshRequestFcast(Nothing, New System.EventArgs)
    End Sub


    Private Sub RefreshRequestFcast(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsFcast.ArtNom
        Dim oGrid As DataGridView = DataGridViewFcast

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridFcast()

        If oGrid.Rows.Count = 0 Then
        Else
            Try

                oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow
            Catch ex As Exception

            End Try

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub


    Private Sub ComboBoxYeas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxYeas.SelectedIndexChanged
        LoadGridFcast()
    End Sub

#End Region

#Region "Pro"

    Private Sub LoadGridPro()
        Cursor = Cursors.WaitCursor
        Dim oMgz As DTOMgz = BLL.BLLApp.Mgz

        Dim SQL As String = "SELECT ART.art, STP.dsc, ART.ord, " _
        & "0 AS OPTIMITZAT,0 AS PRO, ArtStock.Stock AS Stk, ArtPn1.Qty as Pn1, ArtPn2NoPn3.Qty AS Pn2, 0 AS PRE, " _
        & "(CASE WHEN ART.HEREDADIMENSIONS=1 THEN STP.M3 ELSE ART.M3 END) AS M3, ART.M3 AS M3X, " _
        & "(CASE WHEN ART.HEREDADIMENSIONS=1 THEN STP.INNERPACK ELSE ART.INNERPACK END) AS INNERPACK, " _
        & "(CASE WHEN ART.HEREDADIMENSIONS=1 THEN STP.OUTERPACK ELSE ART.OUTERPACK END) AS OUTERPACK " _
        & "FROM  ART INNER JOIN " _
        & "STP ON Art.Category = STP.Guid LEFT OUTER JOIN " _
        & "ArtStock ON ART.Guid = ArtStock.ArtGuid AND ArtStock.MgzGuid='" & oMgz.Guid.ToString & "' LEFT OUTER JOIN " _
        & "ArtPn2NoPn3 ON Art.Guid=ArtPn2NoPn3.ArtGuid LEFT OUTER JOIN " _
        & "ArtPn1 ON Art.Guid=ArtPn1.ArtGuid " _
        & "WHERE ART.emp =" & mEmp.Id & "  AND " _
        & "ART.OBSOLETO=0 AND " _
        & "LASTPRODUCTION=0 "

        Select Case mMode
            Case Modes.Tpa
                SQL = SQL & "AND Stp.Brand='" & mTpa.Guid.ToString & "' "
            Case Modes.Stp
                SQL = SQL & "AND Stp.Guid='" & mStp.Guid.ToString & "' "
        End Select

        SQL = SQL & " ORDER BY STP.ord, ART.ord"

        mDsPro = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsPro.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("WARNICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColsPro.Warn)


        Dim DtFchPrevisio As DateTime = DateTimePickerDeliver.Value.AddDays(NumericUpDownNextOrder.Value)
        Dim oArt As Art
        Dim iOptimitzat As Integer
        Dim iPro As Integer
        Dim iPack As Integer
        Dim SngM3 As Decimal
        Dim SngM3X As Decimal
        Dim SngSumM3X As Decimal = 0
        For Each oRow As DataRow In oTb.Rows
            oArt = MaxiSrvr.Art.FromNum(mEmp, oRow(ColsPro.ArtId))
            If IsDBNull(oRow(ColsPro.Pn2)) Then oRow(ColsPro.Pn2) = 0
            If IsDBNull(oRow(ColsPro.Stk)) Then oRow(ColsPro.Stk) = 0
            If IsDBNull(oRow(ColsPro.Pn1)) Then oRow(ColsPro.Pn1) = 0

            'If oArt.Id = 19212 Then Stop
            Dim iForecast As Integer = oArt.Forecast(Today, DtFchPrevisio)
            oRow(ColsPro.Pre) = iForecast
            If CheckBoxSkipForecast.Checked Then
                iPro = oRow(ColsPro.Pn2) - oRow(ColsPro.Stk) - oRow(ColsPro.Pn1)
            Else
                iPro = iForecast + oRow(ColsPro.Pn2) - oRow(ColsPro.Stk) - oRow(ColsPro.Pn1)
            End If

            oRow(ColsPro.Pro) = iPro

            iOptimitzat = iPro

            If iPro > 0 Then
                iPack = oRow(ColsPro.MinPack)

                If iPack > 0 Then
                    If iOptimitzat Mod iPack <> 0 Then
                        iOptimitzat += (iOptimitzat Mod iPack)
                    End If
                End If

                Dim iPalet As Integer = oRow(ColsPro.OutPack)
                Dim iBox As Integer = oRow(ColsPro.MinPack)
                If iPalet > 0 Then
                    Dim iPaletitzat As Integer
                    If iOptimitzat Mod iPalet <> 0 Then
                        iPaletitzat = iOptimitzat + iPalet - (iOptimitzat Mod iPalet)
                        If iPaletitzat > iPro * 1.5 Then
                            'If oRow(ColsPro.Pn1) > (oRow(ColsPro.Pn2) - oRow(ColsPro.Stk)) Then
                            'End If
                        End If
                        iOptimitzat = iPaletitzat
                    End If
                ElseIf iBox > 0 Then
                    Dim iBoxed As Integer
                    If iOptimitzat Mod iBox <> 0 Then
                        iBoxed = iOptimitzat + iPalet - (iOptimitzat Mod iBox)
                        If iBoxed > iPro * 1.5 Then
                            'If oRow(ColsPro.Pn1) > (oRow(ColsPro.Pn2) - oRow(ColsPro.Stk)) Then
                            'End If
                        End If
                        iOptimitzat = iBoxed
                    End If
                End If

            End If

            oRow(ColsPro.Optimitzat) = iOptimitzat

            SngM3 = oRow(ColsPro.M3)
            SngM3X = iOptimitzat * SngM3
            If iOptimitzat > 0 Then
                oRow(ColsPro.M3) = SngM3
                oRow(ColsPro.M3X) = SngM3X
                SngSumM3X += SngM3X
            Else
                oRow(ColsPro.M3) = 0
                oRow(ColsPro.M3X) = 0
            End If
        Next


        With DataGridViewPro
            With .RowTemplate
                .Height = DataGridViewFcast.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .MultiSelect = False
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .ReadOnly = False
            .BackgroundColor = Color.White

            With .Columns(ColsPro.ArtId)
                .Visible = False
            End With
            With .Columns(ColsPro.StpNom)
                .HeaderText = "categoría"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .ReadOnly = True
            End With
            With .Columns(ColsPro.ArtNom)
                .HeaderText = "article"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .ReadOnly = True
            End With
            With .Columns(ColsPro.Warn)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .ReadOnly = True
            End With

            With .Columns(ColsPro.Optimitzat)
                .HeaderText = "comanda"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsPro.Pro)
                .HeaderText = "proposta"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.Stk)
                .HeaderText = "stocks"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.Pn1)
                .HeaderText = "proveidor"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.Pn2)
                .HeaderText = "clients"
                .Width = 50
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.Pre)
                .HeaderText = "forecast"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.M3)
                .HeaderText = "vol/u"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "0.000;-0.000;#"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.M3X)
                .HeaderText = "volum"
                .Width = 80
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#0.000 m3;-0.000 m3;#"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.MinPack)
                .HeaderText = "caixa"
                .Width = 36
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .ReadOnly = True
            End With
            With .Columns(ColsPro.OutPack)
                .HeaderText = "palet"
                .Width = 36
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .ReadOnly = True
            End With
        End With
        Cursor = Cursors.Default

        CalcM3()

        mAllowEventsPro = True
    End Sub



    Private Sub NumericUpDownDelivery_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownDelivery.ValueChanged
        If mAllowEventsPro Then
            mAllowEventsPro = False
            DateTimePickerDeliver.Value = Today.AddDays(NumericUpDownDelivery.Value)
            LoadGridPro()
            mAllowEventsPro = True
        End If
    End Sub

    Private Sub DateTimePickerDeliver_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerDeliver.ValueChanged
        If mAllowEventsPro Then
            mAllowEventsPro = False
            Dim iDays As Integer = DateTimePickerDeliver.Value.Subtract(Today).Days
            If iDays <> CInt(NumericUpDownDelivery.Value) Then
                If iDays < 0 Then iDays = 0
                NumericUpDownDelivery.Value = iDays
            End If
            DateTimePickerNextOrder.Value = DateTimePickerDeliver.Value.AddDays(NumericUpDownNextOrder.Value)
            LoadGridPro()
            mAllowEventsPro = True
        End If
    End Sub

    Private Sub NumericUpDownNextOrder_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDownNextOrder.ValueChanged
        If mAllowEventsPro Then
            mAllowEventsPro = False
            DateTimePickerNextOrder.Value = DateTimePickerDeliver.Value.AddDays(NumericUpDownNextOrder.Value)
            LoadGridPro()
            mAllowEventsPro = True
        End If
    End Sub


    Private Sub DateTimePickerNextOrder_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePickerNextOrder.ValueChanged
        If mAllowEventsPro Then
            mAllowEventsPro = False
            Dim iDays As Integer = DateTimePickerNextOrder.Value.Subtract(DateTimePickerDeliver.Value).Days
            If iDays <> CInt(NumericUpDownNextOrder.Value) Then
                If iDays < 0 Then iDays = 0
                NumericUpDownNextOrder.Value = iDays
            End If
            LoadGridPro()
            mAllowEventsPro = True
        End If
    End Sub

    Private Sub DataGridViewPro_CellBeginEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles DataGridViewPro.CellBeginEdit
        Select Case e.ColumnIndex
            Case ColsPro.Optimitzat
            Case Else
                e.Cancel = True
        End Select
    End Sub



    Private Sub DataGridViewPro_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewPro.CellFormatting
        Select Case e.ColumnIndex
            Case ColsPro.Warn
                e.Value = WarnIco(e.RowIndex)

            Case ColsPro.Pro, ColsPro.Optimitzat
                If IsNumeric(e.Value) Then
                    If e.Value > 0 Then
                        e.CellStyle.BackColor = Color.White
                        e.CellStyle.ForeColor = Color.Black
                    Else
                        e.CellStyle.BackColor = Color.LightGray
                        e.CellStyle.ForeColor = Color.DarkGray
                    End If
                Else
                    e.CellStyle.BackColor = Color.LightGray
                    e.CellStyle.ForeColor = Color.DarkGray
                End If
            Case ColsPro.M3, ColsPro.M3X
                Dim iPro As Integer = DataGridViewPro.Rows(e.RowIndex).Cells(ColsPro.Pro).Value
                If iPro > 0 Then
                    If e.Value <= 0 Then
                        e.CellStyle.BackColor = Color.LightSalmon
                    Else
                        e.CellStyle.BackColor = Color.White
                    End If
                    e.CellStyle.ForeColor = Color.Black
                Else
                    e.CellStyle.BackColor = Color.LightGray
                    e.CellStyle.ForeColor = Color.DarkGray
                End If
        End Select
    End Sub

    Private Function WarnIco(ByVal iRowIndex As Integer)
        Dim oImg As Image = My.Resources.empty
        Dim oRow As DataGridViewRow = DataGridViewPro.Rows(iRowIndex)
        Dim iFcast As Integer = oRow.Cells(ColsPro.Pre).Value
        Dim iIncrement As Integer = oRow.Cells(ColsPro.Optimitzat).Value - oRow.Cells(ColsPro.Pro).Value
        If iFcast = 0 Then
            If oRow.Cells(ColsPro.Optimitzat).Value > oRow.Cells(ColsPro.Pro).Value Then
                oImg = My.Resources.warn
            End If
        Else
            Dim iRate As Integer = iIncrement / iFcast
            If iRate > 1.2 Then
                oImg = My.Resources.warn
            End If
        End If
        Return oImg
    End Function

    Private Sub SetContextMenuPro()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCurrentCell As DataGridViewCell = DataGridViewPro.CurrentCell
        If oCurrentCell IsNot Nothing Then
            Dim oArt As Art = MaxiSrvr.Art.FromNum(mEmp, DataGridViewPro.CurrentRow.Cells(ColsPro.ArtId).Value)
            Dim oMenuArt As New Menu_Art(oArt)
            AddHandler oMenuArt.AfterUpdate, AddressOf RefreshRequestPro
            oContextMenu.Items.AddRange(oMenuArt.Range)
        End If

        DataGridViewPro.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub DataGridViewPro_CellStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellStateChangedEventArgs) Handles DataGridViewPro.CellStateChanged
        'Stop
        'Dim s As String = e.StateChanged.ToString
        'datagridviewelementstates.
        'e.Empty = Truetab

        Dim oGrid As DataGridView = CType(sender, DataGridView)
        Select Case oGrid.Columns(e.Cell.ColumnIndex).ReadOnly
            Case True
                Dim iNextEntryCol As Integer = -1
                Dim iNextEntryRow As Integer = e.Cell.RowIndex
                For i As Integer = e.Cell.ColumnIndex + 1 To oGrid.ColumnCount - 1
                    If oGrid.Columns(i).Visible And Not oGrid.Columns(i).ReadOnly Then
                        iNextEntryCol = i
                        Exit For
                    End If
                Next
                If iNextEntryCol = -1 Then
                    'If oGrid.Rows.Count > iNextEntryRow + 1 Then
                    iNextEntryRow += 1
                    For i As Integer = 0 To e.Cell.ColumnIndex
                        If oGrid.Columns(i).Visible And Not oGrid.Columns(i).ReadOnly Then
                            iNextEntryCol = i
                            Exit For
                        End If
                    Next
                    'End If
                End If
                If iNextEntryCol >= 0 Then
                    oGrid.CurrentCell = oGrid.Rows(iNextEntryRow).Cells(iNextEntryCol)
                    oGrid.CurrentCell.Selected = True
                End If
        End Select
    End Sub

    Private Sub DataGridViewPro_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewPro.CellValidated
        Select Case e.ColumnIndex
            Case ColsPro.Warn
                Dim oRow As DataGridViewRow = DataGridViewPro.Rows(e.RowIndex)
                oRow.Cells(e.ColumnIndex).Value = WarnIco(e.RowIndex)
            Case ColsPro.Optimitzat
                Dim oRow As DataGridViewRow = DataGridViewPro.Rows(e.RowIndex)
                oRow.Cells(ColsPro.M3X).Value = oRow.Cells(ColsPro.Optimitzat).Value * oRow.Cells(ColsPro.M3).Value
                CalcM3()
                'Dim oRow As DataGridViewRow = DataGridViewPro.Rows(e.RowIndex)
        End Select
    End Sub

    Private Sub CalcM3()
        Dim DcM3 As Decimal = 0
        For Each oRow As DataGridViewRow In DataGridViewPro.Rows
            'If Not IsDBNull(oRow.Cells(ColsPro.Optimitzat).Value) Then
            DcM3 += oRow.Cells(ColsPro.M3X).Value
            'End If
        Next
        NumericUpDownM3.Value = DcM3
    End Sub


    Private Sub DataGridViewPro_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewPro.DoubleClick
        Dim oArt As Art = MaxiSrvr.Art.FromNum(mEmp, DataGridViewPro.CurrentRow.Cells(ColsPro.ArtId).Value)
        Dim oFrm As New Frm_Art(oArt)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestPro
        oFrm.show()
    End Sub

    Private Sub DataGridViewPro_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles DataGridViewPro.EditingControlShowing

    End Sub

    Private Sub DataGridViewPro_EditModeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewPro.EditModeChanged

    End Sub

    Private Sub DataGridViewPro_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewPro.SelectionChanged
        SetContextMenuPro()
    End Sub

    Private Sub RefreshRequestPro(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsPro.ArtNom
        Dim oGrid As DataGridView = DataGridViewPro

        If oGrid.CurrentRow IsNot Nothing Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadGridPro()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub CheckBoxSkipForecast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxSkipForecast.CheckedChanged
        If mAllowEventsPro Then
            RefreshRequestPro(sender, e)
        End If
    End Sub

#End Region

#Region "Control"
    Private Sub LoadGridControl()
        Cursor = Cursors.WaitCursor

        Dim SQL As String = "SELECT ART.art, STP.dsc, ART.ord " _
        & ",0 AS M0PREV,0 AS M0TMP,0 AS M0SALES,0 AS M0PCT " _
        & ",0 AS M1PREV,0 AS M1SALES,0 AS M1PCT " _
        & ",0 AS M2PREV,0 AS M2SALES,0 AS M2PCT " _
        & "FROM  ART INNER JOIN " _
        & "Stp ON Stp.Guid = Art.Category " _
        & "WHERE ART.emp =" & mEmp.Id & " "

        Select Case mMode
            Case Modes.Tpa
                SQL = SQL & "AND Stp.Brand='" & mTpa.Guid.ToString & "' "
            Case Modes.Stp
                SQL = SQL & "AND ART.Category='" & mStp.Guid.ToString & "' "
        End Select

        SQL = SQL & " ORDER BY STP.ord, ART.ord"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim DtFch As Date = Today
        Dim oArt As Art
        Dim DtFromFch As Date
        Dim DtToFch As Date
        Dim iPrev As Integer
        Dim iTmp As Integer
        Dim iVenut As Integer
        For Each oRow As DataRow In oTb.Rows
            oArt = MaxiSrvr.Art.FromNum(mEmp, oRow(ColsControl.ArtId))
            DtFromFch = DtFch.AddDays(-DtFch.Day + 1)
            DtToFch = DtFch.AddDays(-DtFch.Day).AddMonths(1)
            iPrev = oArt.Forecast(DtFromFch, DtToFch)
            iTmp = oArt.Forecast(DtFromFch, DtFch)
            iVenut = oArt.Sales(DtFromFch, DtFch)
            oRow(ColsControl.M0Prev) = iPrev
            oRow(ColsControl.M0Tmp) = iTmp
            oRow(ColsControl.M0Sales) = iVenut
            If iTmp > 0 Then
                oRow(ColsControl.M0Pct) = (100 * (iVenut - iTmp) / iTmp)
            Else
                oRow(ColsControl.M0Pct) = 0
            End If

            DtFromFch = DtFch.AddDays(-DtFch.Day + 1).AddMonths(-1)
            DtToFch = DtFch.AddDays(-DtFch.Day).AddMonths(0)
            iPrev = oArt.Forecast(DtFromFch, DtToFch)
            iVenut = oArt.Sales(DtFromFch, DtFch)
            oRow(ColsControl.M1Prev) = iPrev
            oRow(ColsControl.M1Sales) = iVenut

            If iPrev > 0 Then
                oRow(ColsControl.M1Pct) = (100 * (iVenut - iPrev) / iPrev)
            Else
                oRow(ColsControl.M1Pct) = 0
            End If

            DtFromFch = DtFch.AddDays(-DtFch.Day + 1).AddMonths(-2)
            DtToFch = DtFch.AddDays(-DtFch.Day).AddMonths(-1)
            iPrev = oArt.Forecast(DtFromFch, DtToFch)
            iVenut = oArt.Sales(DtFromFch, DtFch)
            oRow(ColsControl.M2Prev) = iPrev
            oRow(ColsControl.M2Sales) = iVenut

            If iPrev > 0 Then
                oRow(ColsControl.M2Pct) = (100 * (iVenut - iPrev) / iPrev)
            Else
                oRow(ColsControl.M2Pct) = 0
            End If
        Next

        With DataGridViewControl
            With .RowTemplate
                .Height = DataGridViewFcast.Font.Height * 1.3
            End With
            .SelectionMode = DataGridViewSelectionMode.CellSelect
            .MultiSelect = False
            .DataSource = oTb

            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .AllowUserToResizeRows = False
            .BackgroundColor = Color.White

            With .Columns(ColsControl.ArtId)
                .Visible = False
            End With
            With .Columns(ColsControl.StpNom)
                .HeaderText = "categoría"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColsControl.ArtNom)
                .HeaderText = "article"
                .Width = 90
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With

            With .Columns(ColsControl.M0Prev)
                .HeaderText = "prev"
                .Width = 60
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsControl.M0Tmp)
                .HeaderText = "fins avui"
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsControl.M0Sales)
                .HeaderText = BLL.BLLSession.Current.Lang.MesAbr(DtFch.Month)
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsControl.M0Pct)
                .HeaderText = "dif"
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
            With .Columns(ColsControl.M1Prev)
                .HeaderText = "prev"
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsControl.M1Sales)
                .HeaderText = BLL.BLLSession.Current.Lang.MesAbr(DtFch.Month - 1)
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsControl.M1Pct)
                .HeaderText = "dif"
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
            With .Columns(ColsControl.M2Prev)
                .HeaderText = "prev"
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsControl.M2Sales)
                .HeaderText = BLL.BLLSession.Current.Lang.MesAbr(DtFch.Month - 2)
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
            End With
            With .Columns(ColsControl.M2Pct)
                .HeaderText = "dif"
                .Width = 40
                .DefaultCellStyle.BackColor = Color.WhiteSmoke
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#\%;-#\%;#"
            End With
        End With
        Cursor = Cursors.Default
        mAllowEventsPro = True
    End Sub

    Private Sub DataGridViewControl_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewControl.CellFormatting
        Select Case e.ColumnIndex
            Case ColsControl.M0Pct, ColsControl.M1Pct, ColsControl.M2Pct
                Select Case e.Value
                    Case Is >= 0
                        e.CellStyle.BackColor = Color.LightBlue
                    Case -20 - (-1)
                        e.CellStyle.BackColor = Color.LightYellow
                    Case Is < -20
                        e.CellStyle.BackColor = Color.LightSalmon
                End Select
        End Select
    End Sub

#End Region


    Private Sub ToolStripButtonProNewPdc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonProNewPdc.Click

        Dim oPdc As New Pdc(mEmp, DTOPurchaseOrder.Codis.proveidor)
        With oPdc
            .Client = New Client(mProveidor.Guid)
            .Fch = Today
            .Obs = mProveidor.Lang.Tradueix("plazo de entrega: ", "plaç d'entrega: ", "delivery time: ") & DateTimePickerDeliver.Value.ToShortDateString
            .Text = "regular"
            .Source = DTOPurchaseorder.Sources.eMail
            .Cur = mProveidor.DefaultCur
            .Mgz = BLL.BLLApp.Mgz
            .DeliveryCod = Alb.DeliveryCods.Mgz
            .EntregarEn = New Mgz(.Mgz.Guid)

            Dim oItm As LineItmPnc
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridViewPro.Rows
                If oRow.Cells(ColsPro.Optimitzat).Value > 0 Then
                    oItm = New LineItmPnc
                    With oItm
                        .Qty = oRow.Cells(ColsPro.Optimitzat).Value
                        .Pendent = .Qty
                        .Art = MaxiSrvr.Art.FromNum(mEmp, oRow.Cells(ColsPro.ArtId).Value)
                        .Preu = .Art.Cost
                        .dto = .Art.CostDto_OnInvoice
                    End With
                    .Itms.Add(oItm)
                End If
            Next
        End With

        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, New DTOContact(mProveidor.Guid), DTOAlbBloqueig.Codis.PDC, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oFrm As New Frm_Pdc_Proveidor(oPdc)
            oFrm.Show()
        End If
    End Sub

    Private Sub ToolStripButtonExcelPrevisioCompres_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcelPrevisioCompres.Click
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
        Dim oTb As System.Data.DataTable = mDsPro.Tables(0)
        Dim oRow As DataRow
        Dim i As Integer
        Dim j As Integer
        i = i + 1
        With oSheet
            .Cells(i, 1) = "CATEGORIA"
            .Cells(i, 2) = "PRODUCTE"
            .Cells(i, 3) = "STOCKS"
            .Cells(i, 4) = "PENDENT DE PROVEIDORS"
            .Cells(i, 5) = "PENDENT A CLIENTS"
            .Cells(i, 6) = "PREVISIO DE VENDES"
            .Cells(i, 7) = "PREVISIO DE COMPRES"
        End With

        For j = 3 To 7
            oSheet.Columns(j).NumberFormat = "#,##0;-#,##0;#"
        Next

        Dim sFormula As String = "=RC[-1]+RC[-2]-RC[-3]-RC[-4]"
        '=SI((F2+E2)>(D2+C2);F2+E2-D2-C2;0)
        Dim iSumRow As Integer = i + 1
        For Each oRow In oTb.Rows
            i = i + 1

            With oSheet
                .Cells(i, 1) = oRow(ColsPro.StpNom)
                .Cells(i, 2) = oRow(ColsPro.ArtNom)
                .Cells(i, 3) = oRow(ColsPro.Stk)
                .Cells(i, 4) = oRow(ColsPro.Pn1)
                .Cells(i, 5) = oRow(ColsPro.Pn2)
                .Cells(i, 6) = oRow(ColsPro.Pre)
                If oRow(ColsPro.Pro) >= 0 Then
                    .Cells(i, 7).FormulaR1C1 = sFormula
                Else
                    .Cells(i, 7) = 0
                End If
            End With
        Next

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI
        oApp.Visible = True
    End Sub

    Private Sub CheckBox_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
     CheckBoxHideSales.CheckedChanged, _
     CheckBoxHideLastInProduction.CheckedChanged, _
     CheckBoxHideSales.CheckedChanged
        If mAllowEvents Then
            RefreshRequestFcast(sender, e)
        End If
    End Sub



End Class
