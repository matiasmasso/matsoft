
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Balance
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mTb(2, 2) As DataTable
    Private Grids(2, 2) As DataGridView
    Private mFontEpg As Font
    Private mRowTot(2, 2) As DataRow
    Private mRowRes(2) As DataRow
    Private mAllowEvents As Boolean

    Private Enum Cols
        Bal
        Act
        Epg
        Cta
        Dsc
        Eur
        Sum
        Lin
    End Enum

    Private Enum XlsCols
        NotUsed
        Dsc
        Eur
        Sum
        Gap
    End Enum

    Private Enum Lins
        Epg
        Cta
        Sum
    End Enum

    Private Sub Frm_Balance_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mFontEpg = New Font(Me.Font, FontStyle.Bold)
        Dim sFch As String = GetSetting("MAT.NET", "Accounts", "FchLastBalance")
        Dim DtFch As Date
        If IsDate(sFch) Then
            DtFch = CDate(sFch)
        Else
            DtFch = Today
            SaveSetting("MAT.NET", "Accounts", "FchLastBalance", DtFch.ToShortDateString)
        End If

        'DtFch = IIf(IsDate(sFch), sFch, Today)
        DateTimePicker1.Value = DtFch
        Grids(PgcGrup.BalCods.balanç, PgcCta.Acts.deutora) = DataGridView1
        Grids(PgcGrup.BalCods.balanç, PgcCta.Acts.creditora) = DataGridView2
        Grids(PgcGrup.BalCods.explotacio, PgcCta.Acts.deutora) = DataGridView3
        Grids(PgcGrup.BalCods.explotacio, PgcCta.Acts.creditora) = DataGridView4
        refresca()
        DescuadresToolStripButton.Enabled = Not Cuadra()
        mAllowEvents = True
    End Sub

    Private Sub refresca()
        LoadGrid(PgcGrup.BalCods.balanç, PgcCta.Acts.deutora)
        LoadGrid(PgcGrup.BalCods.balanç, PgcCta.Acts.creditora)
        LoadGrid(PgcGrup.BalCods.explotacio, PgcCta.Acts.deutora)
        LoadGrid(PgcGrup.BalCods.explotacio, PgcCta.Acts.creditora)

        mRowTot(PgcGrup.BalCods.balanç, PgcCta.Acts.deutora)(Cols.Dsc) = "Total ACTIU"
        mRowTot(PgcGrup.BalCods.balanç, PgcCta.Acts.creditora)(Cols.Dsc) = "Total PASSIU"
        mRowTot(PgcGrup.BalCods.explotacio, PgcCta.Acts.deutora)(Cols.Dsc) = "Total DESPESES"
        mRowTot(PgcGrup.BalCods.explotacio, PgcCta.Acts.creditora)(Cols.Dsc) = "Total INGRESSOS"

    End Sub

    Private Sub LoadGrid(ByVal oBal As PgcGrup.BalCods, ByVal oAct As PgcCta.Acts)
        mTb(oBal, oAct) = CreateTable()
        Dim oDs As DataSet = GetDataset(DateTimePicker1.Value, oBal, oAct)
        Dim oTb1 As DataTable = oDs.Tables(0)
        Dim oRow1 As DataRow
        Dim oRow2 As DataRow
        Dim oEpgRow As DataRow = Nothing
        Dim EpgId As Integer
        Dim DblEur As Decimal

        mRowTot(oBal, oAct) = mTb(oBal, oAct).NewRow
        mRowTot(oBal, oAct)(Cols.Sum) = 0
        mRowTot(oBal, oAct)(Cols.Lin) = Lins.Sum

        For Each oRow1 In oTb1.Rows
            If oRow1(Cols.Epg) <> EpgId Then
                EpgId = oRow1(Cols.Epg)
                oEpgRow = mTb(oBal, oAct).NewRow
                oEpgRow(Cols.Lin) = Lins.Epg
                oEpgRow(Cols.Epg) = EpgId
                oEpgRow(Cols.Dsc) = New PgcGrup(PgcPlan.FromYear(DateTimePicker1.Value.Year), EpgId.ToString).Nom
                oEpgRow(Cols.Sum) = 0
                mTb(oBal, oAct).Rows.Add(oEpgRow)
            End If
            DblEur = oRow1(Cols.Eur)
            oRow2 = mTb(oBal, oAct).NewRow
            oRow2(Cols.Lin) = Lins.Cta
            oRow2(Cols.Bal) = oRow1(Cols.Bal)
            oRow2(Cols.Act) = oRow1(Cols.Act)
            oRow2(Cols.Epg) = oRow1(Cols.Epg)
            oRow2(Cols.Cta) = oRow1(Cols.Cta)
            oRow2(Cols.Dsc) = "        " & CStr(oRow1(Cols.Cta)).PadRight(6) & oRow1(Cols.Dsc)
            oRow2(Cols.Eur) = DblEur
            mTb(oBal, oAct).Rows.Add(oRow2)
            oEpgRow(Cols.Sum) += DblEur
            mRowTot(oBal, oAct)(Cols.Sum) += DblEur
        Next

        If oBal = PgcGrup.BalCods.balanç And oAct = PgcCta.Acts.creditora Then
            Dim DblActiu As Decimal = mRowTot(PgcGrup.BalCods.balanç, PgcCta.Acts.deutora)(Cols.Sum)
            Dim DblPassiu As Decimal = mRowTot(PgcGrup.BalCods.balanç, PgcCta.Acts.creditora)(Cols.Sum)
            mRowRes(oBal) = mTb(oBal, oAct).NewRow
            mRowRes(oBal)(Cols.Lin) = Lins.Cta
            mRowRes(oBal)(Cols.Dsc) = "        RESULTATS"
            mRowRes(oBal)(Cols.Sum) = DblActiu - DblPassiu
            mTb(oBal, oAct).Rows.Add(mRowRes(oBal))
            mRowTot(PgcGrup.BalCods.balanç, PgcCta.Acts.deutora)(Cols.Sum) = DblActiu
        End If

        mTb(oBal, oAct).Rows.Add(mRowTot(oBal, oAct))

        If oBal = PgcGrup.BalCods.explotacio And oAct = PgcCta.Acts.creditora Then
            Dim DblDespeses As Decimal = mRowTot(PgcGrup.BalCods.explotacio, PgcCta.Acts.deutora)(Cols.Sum)
            Dim DblIngressos As Decimal = mRowTot(PgcGrup.BalCods.explotacio, PgcCta.Acts.creditora)(Cols.Sum)
            mRowRes(oBal) = mTb(oBal, oAct).NewRow
            mRowRes(oBal)(Cols.Lin) = Lins.Cta
            mRowRes(oBal)(Cols.Dsc) = "        RESULTATS"
            mRowRes(oBal)(Cols.Sum) = DblIngressos - DblDespeses
            mTb(oBal, oAct).Rows.Add(mRowRes(oBal))
        End If

        With Grids(oBal, oAct)
            With .RowTemplate
                .Height = Grids(oBal, oAct).Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = mTb(oBal, oAct)
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = False
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(Cols.Bal)
                .Visible = False
            End With
            With .Columns(Cols.Act)
                .Visible = False
            End With
            With .Columns(Cols.Epg)
                .Visible = False
            End With
            With .Columns(Cols.Cta)
                .Visible = False
            End With
            With .Columns(Cols.Dsc)
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Eur)
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Sum)
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Lin)
                .Visible = False
            End With
        End With
    End Sub

    Private Function GetDataset(ByVal DtFch As Date, ByVal oBal As PgcGrup.BalCods, ByVal oAct As PgcCta.Acts) As DataSet
        Dim SQL As String = "SELECT PGCGRUP.Bal, PGCCTA.act, SUBSTRING(CCB.CTA, 1, 1), CCB.cta, " _
        & "PGCCTA.Esp AS NOM, " _
        & "SUM(CASE WHEN CCB.DH = PGCCTA.act THEN EUR ELSE - EUR END) AS EUR " _
        & "FROM CCB INNER JOIN " _
        & "PGCCTA ON CCB.PgcPlan = PGCCta.PgcPlan AND CCB.cta LIKE PGCCTA.Id INNER JOIN " _
        & "PGCGRUP ON PGCGRUP.PgcPlan = PGCCta.PgcPlan AND PGCGRUP.Id LIKE SUBSTRING(PGCCTA.Id, 1, 1) " _
        & "WHERE CCB.Emp =" & mEmp.Id & " AND " _
        & "PGCGRUP.Bal =" & oBal & " AND " _
        & "PGCCTA.Act =" & oAct & " AND " _
        & "CCB.yea =" & DtFch.Year & " AND " _
        & "CCB.FCH<='" & Format(DtFch, "yyyyMMdd") & "' " _
        & "GROUP BY PGCGRUP.Bal, PGCCTA.act, CCB.cta, PGCCTA.Esp " _
        & "HAVING SUM(CASE WHEN CCB.DH = PGCCTA.act THEN EUR ELSE - EUR END)<>0 " _
        & "ORDER BY CCB.cta"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Return oDs
    End Function

    Private Function CreateTable() As DataTable
        Dim oTb As New DataTable()
        oTb.Columns.Add("BAL", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("ACT", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("EPG", System.Type.GetType("System.Int32"))
        oTb.Columns.Add("CTA", System.Type.GetType("System.String"))
        oTb.Columns.Add("DSC", System.Type.GetType("System.String"))
        oTb.Columns.Add("EUR", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("SUM", System.Type.GetType("System.Decimal"))
        oTb.Columns.Add("COD", System.Type.GetType("System.Int32"))
        Return oTb
    End Function



    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles _
    DataGridView1.RowPrePaint, _
     DataGridView2.RowPrePaint, _
      DataGridView3.RowPrePaint, _
       DataGridView4.RowPrePaint

        Dim oGrid As DataGridView = CType(sender, DataGridView)
        Dim oRow As DataGridViewRow = oGrid.Rows(e.RowIndex)
        Dim oCod As Lins = CType(oRow.Cells(Cols.Lin).Value, Lins)
        Select Case oCod
            Case Lins.Epg
                oRow.DefaultCellStyle.BackColor = Color.LightYellow
                oRow.DefaultCellStyle.Font = mFontEpg
            Case Lins.Sum
                oRow.DefaultCellStyle.BackColor = Color.LightBlue
                oRow.DefaultCellStyle.Font = mFontEpg
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End Select
    End Sub


    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If mAllowEvents Then
            SaveSetting("MAT.NET", "Accounts", "FchLastBalance", DateTimePicker1.Value)
        End If
    End Sub


    Private Sub ExcelToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ExcelToolStripButton.Click
        Dim oldCI As System.Globalization.CultureInfo = _
    System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim DtFch As Date = DateTimePicker1.Value
        Dim SQL As String = "SELECT E.Esp, B.cta, C.Esp AS CTANOM, " _
        & "SUM(CASE WHEN B.DH = C.ACT THEN EUR ELSE - EUR END) AS EUR " _
        & "FROM CCB AS B LEFT OUTER JOIN " _
        & "PGCCTA AS C ON B.PgcPlan = C.PgcPlan AND B.cta LIKE C.Id LEFT OUTER JOIN " _
        & "PGCEPGCTAS AS X ON B.PgcPlan = X.PgcPLan AND B.cta LIKE X.Cta + '%' LEFT OUTER JOIN " _
        & "PGCEPG AS E ON X.Epg = E.Id " _
        & "WHERE B.Emp =" & mEmp.Id & " AND " _
        & "B.yea =" & dtfch.Year & " AND " _
        & "B.fch <= '" & Format(DtFCH, "yyyyMMdd") & "' " _
        & "GROUP BY E.SortKey, E.Esp, B.cta, C.Esp " _
        & "ORDER BY E.SortKey, B.cta"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oApp As Excel.Application = MatExcel.GetExcelFromDataset(oDs)
        Dim oSheet As Excel.Worksheet = oApp.Workbooks(1).ActiveSheet
        Dim oCols As Excel.Range = oSheet.Columns("K:P")
        oCols.NumberFormat = "#.##0,00;-#.##0,0;"

        oApp.Visible = True

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

        oApp.Visible = True

    End Sub

    Private Function BalFormula(ByVal j As Integer) As String
        Dim ColBal1 As Integer = 2
        Dim ColDeb As Integer = 10
        Dim ColHab As Integer = 11
        Dim ColBal1Offset As Integer = j - ColBal1
        Dim ColDebOffset As Integer = j - ColDeb
        Dim ColHabOffset As Integer = j - ColHab

        Dim ColXDebCur As Integer = 14
        Dim ColXHabCur As Integer = 15
        Dim ColXDebOld As Integer = 16
        Dim ColXHabOld As Integer = 17

        Dim sFormula As String = ""
        Select Case j
            Case ColXDebCur
                sFormula = "=SI(FC(-" & ColBal1Offset & ")=1;FC(-" & ColDebOffset & ")-FC(-" & ColHabOffset & ");0)"
            Case ColXHabCur
                sFormula = "=SI(FC(-" & ColBal1Offset & ")=2;FC(-" & ColHabOffset & ")-FC(-" & ColDebOffset & ");0)"
            Case ColXDebOld
                sFormula = "=SI(FC(-" & ColBal1Offset & ")=1;FC(-" & ColDebOffset & ")-FC(-" & ColHabOffset & ");0)"
            Case ColXHabOld
                sFormula = "=SI(FC(-" & ColBal1Offset & ")=2;FC(-" & ColHabOffset & ")-FC(-" & ColDebOffset & ");0)"

        End Select
        Return sFormula
    End Function

    Private Sub DescuadresToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DescuadresToolStripButton.Click
        Dim iYea As Integer = DateTimePicker1.Value.Year
        root.ShowCcaDescuadres(iYea)
    End Sub

    Private Function Cuadra() As Boolean
        Dim iYea As Integer = DateTimePicker1.Value.Year
        Dim SQL As String = "SELECT SUM(CASE WHEN CCB.DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) - SUM(CASE WHEN CCB.DH = 2 THEN CCB.EUR ELSE - CCB.EUR END) AS DIF " _
        & "FROM CCB " _
        & "WHERE CCB.EMP =" & mEmp.Id & " And CCB.YEA =" & iYea
        Cuadra = True
        Dim oDrd As SqlClient.SqlDataReader = maxisrvr.GetDataReader(SQL, maxisrvr.Databases.Maxi)
        If oDrd.Read Then
            Cuadra = oDrd("DIF") = 0
        End If
        oDrd.Close()
    End Function

    Private Sub BuscarToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BuscarToolStripButton.Click
        Dim iYea As Integer = DateTimePicker1.Value.Year
        root.ShowCcaSearch(iYea)
    End Sub

    Private Sub PdfToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PdfToolStripButton.Click
        Dim DtFch As Date = DateTimePicker1.Value
        Dim oBal As New Balance(mEmp, Balance.DocCods.FullBook, DtFch, BLL.BLLApp.Lang)
        root.ShowPdf(oBal.Pdf)
        Exit Sub

    End Sub

    Private Sub RefrescaToolStripButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefrescaToolStripButton.Click
        refresca()
    End Sub

    Private Enum ColsRes
        mes
        vendes
        compres
        result
    End Enum

    Private Sub LoadResults()
        Dim SQL As String = "SELECT  CAST(MONTH(fch) AS VARCHAR) AS MES, " _
        & "SUM(CASE WHEN CTA LIKE '7%' THEN (CASE WHEN CCB.DH = 2 THEN EUR ELSE - EUR END) ELSE 0 END) AS HAB, " _
        & "SUM(CASE WHEN CTA LIKE '6%' THEN (CASE WHEN CCB.DH = 1 THEN EUR ELSE - EUR END) ELSE 0 END) AS DEB, " _
        & "SUM(CASE WHEN CTA LIKE '7%' THEN (CASE WHEN CCB.DH = 2 THEN EUR ELSE - EUR END) ELSE 0 END) - SUM(CASE WHEN CTA LIKE '6%' THEN (CASE WHEN CCB.DH = 1 THEN EUR ELSE - EUR END) ELSE 0 END) AS RESULT " _
        & "FROM CCB " _
        & "WHERE Emp =" & mEmp.Id & " AND " _
        & "yea =" & DateTimePicker1.Value.Year & " AND " _
        & "(cta BETWEEN '6' AND '8') " _
        & "GROUP BY MONTH(fch) " _
        & "ORDER BY MONTH(fch)"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        With DataGridViewResults
            With .RowTemplate
                .Height = DataGridViewResults.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            With .Columns(ColsRes.mes)
                .HeaderText = "MES"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsRes.vendes)
                .HeaderText = "INGRESOS"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsRes.compres)
                .HeaderText = "DESPESES"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsRes.result)
                .HeaderText = "RESULTATS"
                .Width = 100
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With

        Dim oRow As DataRow
        Dim iMaxPositiu As Decimal = 0
        Dim iMaxNegatiu As Decimal = 0
        For Each oRow In oTb.Rows
            If oRow(ColsRes.vendes) > iMaxPositiu Then iMaxPositiu = oRow(ColsRes.vendes)
            If (oRow(ColsRes.vendes) - oRow(ColsRes.compres)) < iMaxNegatiu Then iMaxNegatiu = (oRow(ColsRes.vendes) - oRow(ColsRes.compres))
        Next

        Dim iItmWidth As Integer = 30
        Dim iWidth As Integer = 12 * iItmWidth
        Dim iEpgHeight As Integer = 15
        Dim iBottomHeight As Integer = 0
        Dim iHeight As Integer = PictureBoxResults.Height - iEpgHeight - iBottomHeight
        Dim SngFactor As Decimal = iHeight / (iMaxPositiu - iMaxNegatiu)

        Dim oImg As New System.Drawing.Bitmap(iWidth, PictureBoxResults.Height)
        Dim oGr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(oImg)
        Dim X1 As Integer
        Dim X2 As Integer = 0
        Dim Yingr As Integer
        Dim Ydesp As Integer


        Dim oFont As New Font("Arial", 8, FontStyle.Regular)
        Dim oLang As New DTOLang("CAT")
        Dim BlPen As Boolean
        Dim oBrush As Brush = Nothing
        Dim oBrushBlue As Brush = Brushes.Blue
        Dim oBrushSalm As Brush = Brushes.LightSalmon
        Dim s As String
        Dim oEpgRc As Rectangle
        For Each oRow In oTb.Rows
            X1 = X2 + 1
            X2 = X1 + iItmWidth - 1
            BlPen = Not BlPen
            oBrush = IIf(BlPen, Brushes.WhiteSmoke, Brushes.White)
            oEpgRc = New Rectangle(X1, 0, X2 - X1, iEpgHeight)
            s = oLang.MesAbr(oRow(ColsRes.mes))
            oGr.FillRectangle(oBrush, X1, 0, X2 - X1, iEpgHeight)
            oGr.DrawString(s, oFont, Brushes.Black, X1, 0)

            BlPen = Not BlPen
            oBrush = IIf(BlPen, Brushes.WhiteSmoke, Brushes.White)
            Yingr = iEpgHeight + iHeight - oRow(ColsRes.vendes) * SngFactor
            oGr.FillRectangle(oBrush, X1, iEpgHeight + 1, X2 - X1, Yingr - 1)
            oGr.FillRectangle(oBrushBlue, X1, Yingr, X2 - X1, iEpgHeight + iHeight - Yingr)

            Ydesp = iEpgHeight + iHeight - oRow(ColsRes.compres) * SngFactor
            oGr.FillRectangle(oBrushSalm, X1 + 2, Ydesp, X2 - X1, iEpgHeight + iHeight - Ydesp)

            BlPen = Not BlPen
            oBrush = IIf(BlPen, Brushes.WhiteSmoke, Brushes.White)
            's = Format(CDbl(oRow("QTY")), "#,##0")
            'oGr.FillRectangle(oBrush, X1, iEpgHeight + iHeight + 1, X2 - X1, iBottomHeight)
            'oGr.DrawString(s, oFont, Brushes.Black, X1, iEpgHeight + iHeight + 1)
        Next

        PictureBoxResults.Image = oImg

    End Sub


    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedTab.Text
            Case TabPageResults.Text
                Static Loaded_results As Boolean
                If Not Loaded_results Then
                    'Loaded_results = True
                    LoadResults()
                End If
            Case TabPage2.Text
                DataGridView3.Columns(Cols.Bal).Visible = False
                DataGridView4.Columns(Cols.Bal).Visible = False
        End Select
    End Sub

    Private Sub DataGridView_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    DataGridView1.DoubleClick, _
     DataGridView2.DoubleClick, _
      DataGridView3.DoubleClick, _
       DataGridView4.DoubleClick

        Dim oGrid As DataGridView = CType(sender, DataGridView)
        Dim oTb As DataTable = oGrid.DataSource()
        Dim oRow As DataGridViewRow = oGrid.CurrentRow
        If Not IsDBNull(oRow.Cells(Cols.Cta).Value) Then
            Select Case CInt(oRow.Cells(Cols.Lin).Value)
                Case Lins.Cta
                    Dim iYea As Integer = DateTimePicker1.Value.Year
                    Dim oPlan As PgcPlan = PgcPlan.FromYear(iYea)
                    Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(oPlan, oRow.Cells(Cols.Cta).Value.ToString.Trim)
                    Dim oCce As New Cce(mEmp, oCta, iYea)
                    root.ShowCceCcds(oCce, CDate("1/1/" & iYea), DateTimePicker1.Value)
            End Select
        End If
    End Sub

    Private Sub DataGridViewResults_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewResults.CellFormatting
        Select Case e.ColumnIndex
            Case ColsRes.mes
                Dim iMes As Integer = CInt(e.Value)
                Dim oLang As DTOLang = App.Current.emp.WinUsr.Lang
                Dim sMes As String = oLang.MesAbr(iMes)
                e.Value = sMes
        End Select
    End Sub

    Private Sub ToolStripButtonSumesySaldos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonSumesySaldos.Click
        root.ShowBalSumasYSaldos(DateTimePicker1.Value)
    End Sub

    Private Sub DataGridViewResults_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewResults.DoubleClick
        Dim oRow As DataGridViewRow = DataGridViewResults.CurrentRow
        If oRow IsNot Nothing Then
            Dim iMes As Integer = DataGridViewResults.CurrentRow.Cells(ColsRes.mes).Value
            Dim DtFch As Date = DateTimePicker1.Value
            Dim iYea As Integer = DtFch.Year
            root.ShowDiariResumMensual(iYea, iMes)
        End If
    End Sub
End Class