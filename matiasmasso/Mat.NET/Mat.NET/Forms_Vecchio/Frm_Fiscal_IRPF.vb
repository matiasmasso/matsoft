
Imports Excel = Microsoft.Office.Interop.Excel

Public Class Frm_Fiscal_IRPF

    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mPlan As PgcPlan = PgcPlan.FromToday
    Private mDsCtas As DataSet
    Private mDsSctas As DataSet
    Private mDsCcas As DataSet
    Private mBancs As List(Of DTOBanc)

    Private mFchHasta As Date
    Private mFchDesde As Date
    Private mFchVto As Date
    Private mYea As Integer
    Private mMes As Integer

    Private mAllowEvents As Boolean

    Private Enum ColCta
        CodIcon
        Icon
        Cta
        Txt
        Clis
        Base
        Cuota
    End Enum

    Private Enum ColScta
        CodIcon
        Icon
        Cli
        Base
        Cuota
        Tipus
        Clx
    End Enum

    Private Enum ColCca
        Cca
        Fch
        Base
        Cuota
        Tipus
        Txt
    End Enum

    Private Sub Frm_Fiscal_IRPF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadBancs()
        LoadFch()
        LoadCtas()
        LoadSCtas()
        LoadCcas()
        mAllowEvents = True
    End Sub

#Region "Ctas"
    Private Sub LoadCtas()
        Dim sFchHasta As String = Format(mFchHasta, "yyyyMMdd")
        Dim sFchDesde As String = Format(mFchDesde, "yyyyMMdd")
        Dim oPlan As PgcPlan = PgcPlan.FromYear(mFchHasta.Year)
        Dim SQL As String = "SELECT 0 AS ICON, " _
        & "CUOTAS.CTA, " _
        & "(CASE WHEN CUOTAS.CTA LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfTreballadors).Id & "' THEN 'TREBALLADORS' WHEN CUOTAS.CTA LIKE '47512' THEN 'PROFESIONALS' WHEN CUOTAS.CTA LIKE '47513' THEN 'LLOGUERS' ELSE '??' END) AS TXT, " _
        & "COUNT(DISTINCT CUOTAS.cli) AS CLIS, " _
        & "SUM(CASE WHEN BASES.DH = 1 THEN BASES.EUR ELSE - BASES.EUR END) AS BASE, " _
        & "SUM(CASE WHEN CUOTAS.DH = 2 THEN CUOTAS.EUR ELSE - CUOTAS.EUR END) AS CUOTA " _
        & "FROM CCB AS CUOTAS INNER JOIN " _
        & "CCB AS BASES ON CUOTAS.Emp = BASES.Emp AND CUOTAS.yea = BASES.yea AND CUOTAS.Cca = BASES.Cca AND " _
        & "((CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfTreballadors).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.Nomina).Id & "') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfProfessionals).Id & "' AND BASES.cta LIKE '6%' AND NOT BASES.cta LIKE '63%') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfLloguers).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.lloguers).Id & "')) " _
        & "WHERE CUOTAS.Emp = 1 AND " _
        & "CUOTAS.fch BETWEEN '" & sFchDesde & "' AND '" & sFchHasta & "' " _
        & "GROUP BY CUOTAS.CTA " _
        & "ORDER BY CUOTAS.CTA"
        mDsCtas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsCtas.Tables(0)



        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColCta.Icon)

        With DataGridViewCtas
            With .RowTemplate
                .Height = DataGridViewCtas.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            If oTb.Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(ColCta.Txt)
            End If

            With .Columns(ColCta.CodIcon)
                .Visible = False
            End With
            With .Columns(ColCta.Icon)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColCta.Cta)
                .HeaderText = "compte"
                .Width = 50
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColCta.Txt)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColCta.Clis)
                .HeaderText = "perceptors"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColCta.Base)
                .HeaderText = "bases"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColCta.Cuota)
                .HeaderText = "cuotas"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
        End With
    End Sub

    Private Sub DataGridViewCtas_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewCtas.CellFormatting
        Select Case e.ColumnIndex
            Case ColCta.Icon
                Dim oRow As DataGridViewRow = DataGridViewCtas.Rows(e.RowIndex)
                Dim iCod As Integer = CInt(oRow.Cells(ColCta.CodIcon).Value)
                Select Case iCod
                    Case 1
                        e.Value = My.Resources.warn
                    Case Else
                        e.Value = My.Resources.empty
                End Select
        End Select
    End Sub


    Private Sub DataGridViewCtas_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewCtas.SelectionChanged
        If mAllowEvents Then
            LoadSCtas()
        End If
    End Sub

    Private Function CurrentCta() As PgcCta
        Dim oCta As PgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCtas.CurrentRow
        If oRow IsNot Nothing Then
            Dim sId As String = oRow.Cells(ColCta.Cta).Value
            oCta = MaxiSrvr.PgcCta.FromNum(mPlan, sId)
        End If
        Return oCta
    End Function
#End Region

#Region "SCtas"

    Private Sub LoadSCtas(Optional ByVal sCta As String = "")
        If sCta = "" Then sCta = CurrentCta.Id
        Dim sFchHasta As String = Format(mFchHasta, "yyyyMMdd")
        Dim sFchDesde As String = Format(mFchDesde, "yyyyMMdd")
        Dim oPlan As PgcPlan = PgcPlan.FromYear(mFchHasta.Year)

        Dim SQL As String = "SELECT (CASE WHEN SDO.EUR=0 THEN 0 WHEN SDO.EUR IS NULL THEN 0 ELSE 1 END) AS ICON, " _
        & "CUOTAS.CLI, " _
        & "SUM(CASE WHEN BASES.DH = 1 THEN BASES.EUR ELSE - BASES.EUR END) AS BASE, " _
        & "SUM(CASE WHEN CUOTAS.DH = 2 THEN CUOTAS.EUR ELSE - CUOTAS.EUR END) AS CUOTA, " _
        & "'0' as TIPUS, " _
        & "CLX.CLX " _
        & "FROM CCB AS CUOTAS INNER JOIN " _
        & "CCB AS BASES ON CUOTAS.Emp = BASES.Emp AND CUOTAS.yea = BASES.yea AND CUOTAS.Cca = BASES.Cca AND " _
        & "((CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfTreballadors).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.Nomina).Id & "') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfProfessionals).Id & "' AND BASES.cta LIKE '6%' AND NOT BASES.cta LIKE '63%') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfLloguers).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.lloguers).Id & "')) inner join " _
        & "CLX ON CUOTAS.EMP=CLX.EMP AND CUOTAS.CLI=CLX.CLI LEFT OUTER join " _
        & "(SELECT EMP,YEA,CTA,CLI,SUM(CASE WHEN DH=1 THEN EUR ELSE -EUR END) AS EUR FROM CCB WHERE CCB.FCH<'" & sFchDesde & "' GROUP BY EMP,YEA,CTA,CLI) " _
        & "SDO ON CUOTAS.EMP=SDO.EMP AND CUOTAS.YEA=SDO.YEA AND CUOTAS.CTA LIKE SDO.CTA AND CUOTAS.CLI=SDO.CLI " _
        & "WHERE CUOTAS.Emp = 1 AND " _
        & "CUOTAS.CTA LIKE '" & sCta & "' AND " _
        & "CUOTAS.fch BETWEEN '" & sFchDesde & "' AND '" & sFchHasta & "' " _
        & "GROUP BY CUOTAS.CLI, CLX.CLX, SDO.EUR " _
        & "ORDER BY CLX.CLX"
        mDsSctas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)

        Dim oTb As DataTable = mDsSctas.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColCta.Icon)

        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            oRow(ColScta.Tipus) = Math.Round(100 * CDbl(oRow(ColScta.Cuota) / oRow(ColScta.Base)), 0) & "%"
        Next

        With DataGridViewSctas
            With .RowTemplate
                .Height = DataGridViewSctas.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            If oTb.Rows.Count > 0 Then
                .CurrentCell = .Rows(0).Cells(ColScta.Clx)
            End If

            With .Columns(ColScta.CodIcon)
                .Visible = False
            End With
            With .Columns(ColScta.Icon)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(ColScta.Cli)
                .Visible = False
            End With
            With .Columns(ColScta.Base)
                .HeaderText = "bases"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColScta.Cuota)
                .HeaderText = "cuotas"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColScta.Tipus)
                .HeaderText = "tipus"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColScta.Clx)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub


    Private Sub DataGridViewSCtas_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewSctas.CellFormatting
        Select Case e.ColumnIndex
            Case ColScta.Icon
                Dim oRow As DataGridViewRow = DataGridViewSctas.Rows(e.RowIndex)
                Dim iCod As Integer = CInt(oRow.Cells(ColScta.CodIcon).Value)
                Select Case iCod
                    Case 1
                        e.Value = My.Resources.warn
                    Case Else
                        e.Value = My.Resources.empty
                End Select
        End Select
    End Sub


    Private Sub DataGridViewSCtas_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewSctas.SelectionChanged
        If mAllowEvents Then
            SetMenuSctas()
            LoadCcas()
        End If
    End Sub

    Private Function CurrentCcd() As Ccd
        Dim oCcd As Ccd = Nothing
        Dim oRow As DataGridViewRow = DataGridViewSctas.CurrentRow
        If oRow IsNot Nothing Then
            Dim IntId As Integer = oRow.Cells(ColScta.Cli).Value
            Dim oContact As Contact = MaxiSrvr.Contact.FromNum(mEmp, IntId)
            oCcd = New Ccd(oContact, mYea, CurrentCta)
        End If
        Return oCcd
    End Function


    Private Sub SetMenuSctas()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcd As Ccd = CurrentCcd()

        If oCcd IsNot Nothing Then
            Dim oMenu_Ccd As New Menu_Ccd(oCcd, mEmp)
            AddHandler oMenu_Ccd.AfterUpdate, AddressOf RefreshRequestSCta
            oContextMenu.Items.AddRange(oMenu_Ccd.Range)
        End If

        DataGridViewSctas.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequestSCta(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColScta.Clx
        Dim oGrid As DataGridView = DataGridViewSctas

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadSCtas()

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

#End Region

#Region "Ccas"

    Private Sub LoadCcas()
        Dim sFchHasta As String = Format(mFchHasta, "yyyyMMdd")
        Dim sFchDesde As String = Format(mFchDesde, "yyyyMMdd")
        Dim oCcd As Ccd = CurrentCcd()
        Dim oPlan As PgcPlan = PgcPlan.FromYear(mFchHasta.Year)

        Dim sSQL As String = "SELECT CUOTAS.CCA, " _
        & "CUOTAS.FCH, " _
        & "SUM(CASE WHEN BASES.DH = 1 THEN BASES.EUR ELSE - BASES.EUR END) AS BASE, " _
        & "SUM(CASE WHEN CUOTAS.DH = 2 THEN CUOTAS.EUR ELSE - CUOTAS.EUR END) AS CUOTA, " _
        & "'0' as TIPUS, " _
        & "CCA.TXT " _
        & "FROM CCB AS CUOTAS INNER JOIN " _
        & "CCB AS BASES ON CUOTAS.Emp = BASES.Emp AND CUOTAS.yea = BASES.yea AND CUOTAS.Cca = BASES.Cca AND " _
        & "((CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfTreballadors).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.Nomina).Id & "') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfProfessionals).Id & "' AND BASES.cta LIKE '6%' AND NOT BASES.cta LIKE '63%') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfLloguers).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.lloguers).Id & "')) inner join " _
        & "CCA ON CUOTAS.EMP=CCA.EMP AND CUOTAS.YEA=CCA.YEA AND CUOTAS.CCA=CCA.CCA " _
        & "WHERE CUOTAS.Emp = 1 AND " _
        & "CUOTAS.CTA LIKE '" & oCcd.Cta.Id & "' AND " _
        & "CUOTAS.fch BETWEEN '" & sFchDesde & "' AND '" & sFchHasta & "' AND " _
        & "CUOTAS.CLI=" & oCcd.Contact.Id.ToString & " " _
        & "GROUP BY CUOTAS.CCA, CUOTAS.FCH, CCA.TXT " _
        & "ORDER BY CUOTAS.FCH, CUOTAS.CCA"
        mDsCcas = MaxiSrvr.GetDataset(sSQL, MaxiSrvr.Databases.Maxi)

        Dim oTb As DataTable = mDsCcas.Tables(0)
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            oRow(ColCca.Tipus) = Math.Round(100 * CDbl(oRow(ColCca.Cuota) / oRow(ColCca.Base)), 0) & "%"
        Next

        With DataGridViewCcas
            With .RowTemplate
                .Height = DataGridViewCcas.Font.Height * 1.3
                '.DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(ColCca.Cca)
                .HeaderText = "registre"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#"
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColCca.Fch)
                .HeaderText = "data"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColCca.Base)
                .HeaderText = "bases"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColCca.Cuota)
                .HeaderText = "cuotas"
                .Width = 80
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColCca.Tipus)
                .HeaderText = "tipus"
                .Width = 30
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#\%;-#\%;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColCca.Txt)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub

    Private Function CurrentCca() As Cca
        Dim oCca As Cca = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCtas.CurrentRow
        If oRow IsNot Nothing Then
            Dim IntId As Integer = oRow.Cells(ColCca.Cca).Value
            oCca = MaxiSrvr.Cca.FromNum(mEmp, mYea, IntId)
        End If
        Return oCca
    End Function


    Private Sub SetMenuCcas()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCca As Cca = CurrentCca()

        If oCca IsNot Nothing Then
            Dim oMenu_Cca As New Menu_Cca(oCca)
            AddHandler oMenu_Cca.AfterUpdate, AddressOf RefreshRequestCca
            oContextMenu.Items.AddRange(oMenu_Cca.Range)
        End If

        DataGridViewCcas.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequestCca(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColCca.Txt
        Dim oGrid As DataGridView = DataGridViewCcas

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadCcas()

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

#End Region

    Private Sub LoadFch()
        Dim DtFch As Date = Today.AddMonths(-1)
        mYea = DtFch.Year
        mMes = DtFch.Month
        mFchDesde = New Date(mYea, mMes, 1)
        mFchHasta = New Date(mYea, mMes, Date.DaysInMonth(mYea, mMes))

        Select Case mMes
            Case 12
                mFchVto = "30/1/" & mYea + 1
            Case Else
                mFchVto = New Date(mYea, mMes + 1, 20)
        End Select

        Me.Text = "IRPF " & BLL.BLLSession.Current.Lang.MesAbr(mMes) & " " & mYea

        DateTimePicker1.Value = mFchDesde
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged
        If mAllowEvents Then

            Dim DtFch As Date = DateTimePicker1.Value
            mYea = DtFch.Year
            mMes = DtFch.Month
            mFchDesde = New Date(mYea, mMes, 1)
            mFchHasta = New Date(mYea, mMes, Date.DaysInMonth(mYea, mMes))

            Select Case mMes
                Case 12
                    mFchVto = "30/1/" & mYea + 1
                Case Else
                    mFchVto = New Date(mYea, mMes + 1, 20)
            End Select

            Me.Text = "IRPF " & BLL.BLLSession.Current.User.Lang.MesAbr(mMes) & " " & mYea

            LoadCtas()
            LoadSCtas()
            LoadCcas()
        End If

    End Sub

    Private Sub LoadBancs()
        mBancs = BLL.BLLBancs.All()
        With ComboBoxBanc
            .DisplayMember = "Abr"
            .ValueMember = "Id"
            .DataSource = mBancs
        End With
    End Sub

    Public Function MyExcel() As Excel.Application
        Dim oApp As Excel.Application = MatExcel.GetExcel
        oApp.UserControl = True
        Dim oldCI As System.Globalization.CultureInfo = _
            System.Threading.Thread.CurrentThread.CurrentCulture
        System.Threading.Thread.CurrentThread.CurrentCulture = _
            New System.Globalization.CultureInfo("en-US")

        Dim oWb As Excel.Workbook = oApp.Workbooks.Add()
        Dim oSheet As Excel.Worksheet = oWb.ActiveSheet

        Dim row As Long

        With oSheet
            .Columns(3).ColumnWidth = 45
            row = 1
            .Cells(row, 1) = "MATIAS MASSO, S.A. - " & Me.Text
            row = 3
            .Cells(row, 2) = "PERCEPTORS"
            .Cells(row, 4) = "BASE"
            .Cells(row, 5) = "CUOTA"
            .Range("D3:E3").HorizontalAlignment = Excel.Constants.xlRight

            row = row + 2
            Dim oRow As DataRow
            For Each oRow In mDsCtas.Tables(0).Rows
                .Cells(row, 2) = oRow(ColCta.Cta)
                .Cells(row, 3) = oRow(ColCta.Txt)
                .Cells(row, 4) = oRow(ColCta.Base)
                .Cells(row, 5) = oRow(ColCta.Cuota)
                row = row + 1
            Next

            row = row + 2
            LoadSCtas("47511")
            .Cells(row, 2) = "TREBALLADORS"
            .Cells(row, 4).Formula = "=SUM(R[1]C:R[" & mDsSctas.Tables(0).Rows.Count & "]C)"
            .Cells(row, 5).Formula = "=SUM(R[1]C:R[" & mDsSctas.Tables(0).Rows.Count & "]C)"

            row = row + 1
            For Each oRow In mDsSctas.Tables(0).Rows
                .Cells(row, 3) = oRow(ColScta.Clx)
                .Cells(row, 4) = oRow(ColScta.Base)
                .Cells(row, 5) = oRow(ColScta.Cuota)
                row = row + 1
            Next

            row = row + 2
            LoadSCtas("47512")
            .Cells(row, 2) = "PROFESSIONALS"
            .Cells(row, 4).Formula = "=SUM(R[1]C:R[" & mDsSctas.Tables(0).Rows.Count & "]C)"
            .Cells(row, 5).Formula = "=SUM(R[1]C:R[" & mDsSctas.Tables(0).Rows.Count & "]C)"
            row = row + 1
            For Each oRow In mDsSctas.Tables(0).Rows
                .Cells(row, 3) = oRow(ColScta.Clx)
                .Cells(row, 4) = oRow(ColScta.Base)
                .Cells(row, 5) = oRow(ColScta.Cuota)
                row = row + 1
            Next

            row = row + 2
            LoadSCtas("47513")
            .Cells(row, 2) = "LLOGUERS"
            .Cells(row, 4).Formula = "=SUM(R[1]C:R[" & mDsSctas.Tables(0).Rows.Count & "]C)"
            .Cells(row, 5).Formula = "=SUM(R[1]C:R[" & mDsSctas.Tables(0).Rows.Count & "]C)"
            row = row + 1
            For Each oRow In mDsSctas.Tables(0).Rows
                .Cells(row, 3) = oRow(ColScta.Clx)
                .Cells(row, 4) = oRow(ColScta.Base)
                .Cells(row, 5) = oRow(ColScta.Cuota)
                row = row + 1
            Next

            '.Range("D:E").NumberFormat = "#.##0,00"
            .Columns(4).NumberFormat = "#,##0.00 €"
            .Columns(5).NumberFormat = "#,##0.00 €"
        End With

        System.Threading.Thread.CurrentThread.CurrentCulture = oldCI

        Return oApp
    End Function

    Private Sub ToolStripButtonXls_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripButtonXls.Click
        MyExcel.Visible = True
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        Dim oTb As DataTable = mDsCtas.Tables(0)
        Dim oRow As DataRow
        Dim sCta As String

        Dim DblTreballadors As Decimal
        Dim DblProfessionals As Decimal
        Dim DblLloguers As Decimal

        For Each oRow In oTb.Rows
            sCta = oRow(ColCta.Cta)
            Select Case sCta
                Case "47511"
                    DblTreballadors = oRow(ColCta.Cuota)
                Case "47512"
                    DblProfessionals = oRow(ColCta.Cuota)
                Case "47513"
                    DblLloguers = oRow(ColCta.Cuota)
            End Select
            SaveSubcomptes(sCta)
        Next

        SaveMod110(DblTreballadors, DblProfessionals)
        SaveMod115(DblLloguers)
        MsgBox("Guardat a comptabilitat", MsgBoxStyle.Information, "MAT.NET")
        Me.Close()
    End Sub

    Private Sub SaveMod110(ByVal DblTreballadors As Decimal, ByVal DblProfessionals As Decimal)
        Dim oCtaTreb As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.IrpfTreballadors)
        Dim oCtaProf As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.IrpfProfessionals)
        Dim oCtaIrpf As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.Irpf)
        Dim oCtaBanc As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.bancs)
        Dim oContact As Contact = Nothing
        Dim DblIrpf As Decimal = DblTreballadors + DblProfessionals
        Dim oCca As new cca(BLL.BLLApp.emp)
        Dim oCcb As Ccb
        With oCca
            .fch = mFchHasta
            .Ccd = DTOCca.CcdEnum.IRPF
            .Cdn = 100 * mYea + mMes
            .Txt = "HISENDA-Mod 111 " & Me.Text

            oCcb = New Ccb(oCtaTreb, oContact, New maxisrvr.Amt(DblTreballadors), DTOCcb.DhEnum.Debe)
            .ccbs.Add(oCcb)
            oCcb = New Ccb(oCtaProf, oContact, New maxisrvr.Amt(DblProfessionals), DTOCcb.DhEnum.Debe)
            .ccbs.Add(oCcb)
            oCcb = New Ccb(oCtaIrpf, oContact, New maxisrvr.Amt(DblIrpf), DTOCcb.DhEnum.Haber)
            .ccbs.Add(oCcb)

            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With

        Dim oBanc As Banc = ComboBoxBanc.SelectedItem
        oCca = new cca(BLL.BLLApp.emp)
        With oCca
            .fch = mFchVto
            .Ccd = DTOCca.CcdEnum.IRPF
            .Cdn = 100 * mYea + mMes
            .Txt = oBanc.Abr & "-Mod 111 " & Me.Text

            oCcb = New Ccb(oCtaIrpf, oContact, New maxisrvr.Amt(DblIrpf), DTOCcb.DhEnum.Debe)
            .ccbs.Add(oCcb)
            oCcb = New Ccb(oCtaBanc, oBanc, New maxisrvr.Amt(DblIrpf), DTOCcb.DhEnum.Haber)
            .ccbs.Add(oCcb)

            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With

    End Sub

    Private Sub SaveMod115(ByVal DblLloguers As Decimal)
        Dim oCtaLlog As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.IrpfLloguers)
        Dim oCtaBanc As PgcCta = mPlan.Cta(DTOPgcPlan.ctas.bancs)
        Dim oBanc As Banc = ComboBoxBanc.SelectedItem
        Dim oContact As Contact = Nothing
        Dim oCcb As Ccb
        Dim oCca As new cca(BLL.BLLApp.emp)
        With oCca
            .fch = mFchVto
            .Ccd = DTOCca.CcdEnum.IRPF
            .Cdn = 100 * mYea + mMes
            .Txt = oBanc.Abr & "-Mod 115 " & Me.Text

            oCcb = New Ccb(oCtaLlog, oContact, New maxisrvr.Amt(DblLloguers), DTOCcb.DhEnum.Debe)
            .ccbs.Add(oCcb)
            oCcb = New Ccb(oCtaBanc, oBanc, New maxisrvr.Amt(DblLloguers), DTOCcb.DhEnum.Haber)
            .ccbs.Add(oCcb)

            Dim exs as New List(Of exception)
            If Not .Update( exs) Then
                MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
            End If
        End With
    End Sub

    Private Sub SaveSubcomptes(ByVal sCta As String)
        LoadSCtas(sCta)
        Dim oCta As PgcCta = MaxiSrvr.PgcCta.FromNum(mPlan, sCta)
        Dim oCca As new cca(BLL.BLLApp.emp)
        With oCca
            .fch = mFchHasta
            .Ccd = DTOCca.CcdEnum.IRPF
            .Cdn = 100 * mYea + mMes
            Select Case sCta
                Case "47511"
                    .Txt = "HISENDA-IRPF treballadors " & Me.Text
                Case "47512"
                    .Txt = "HISENDA-IRPF professionals " & Me.Text
                Case "47513"
                    .Txt = "HISENDA-Mod.115 lloguers " & Me.Text
            End Select
        End With
        Dim oCcb As Ccb
        Dim oContact As Contact
        Dim oAmt As maxisrvr.Amt
        Dim oSum As New maxisrvr.Amt
        Dim oRow As DataRow
        For Each oRow In mDsSctas.Tables(0).Rows
            oContact = MaxiSrvr.Contact.FromNum(mEmp, oRow(ColScta.Cli))
            oAmt = New MaxiSrvr.Amt(CDec(oRow(ColScta.Cuota)))
            oSum.Add(oAmt)
            oCcb = New Ccb(oCta, oContact, oAmt, DTOCcb.DhEnum.Debe)
            oCca.ccbs.Add(oCcb)
        Next

        oContact = Nothing
        oAmt = oSum
        oCcb = New Ccb(oCta, oContact, oAmt, DTOCcb.DhEnum.Haber)
        oCca.ccbs.Add(oCcb)

        Dim exs as New List(Of exception)
        If Not oCca.Update( exs) Then
            MsgBox("error" & vbCrLf & BLL.Defaults.ExsToMultiline(exs))
        End If
    End Sub

    Private Sub ToolStripButtonFitxer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFitxer.Click
        Dim oFitxer As New maxisrvr.MatFileAEAT111(mYea, mMes, MaxiSrvr.Emp.FromDTOEmp(memp).Org.NIF, "Matías Massó", MaxiSrvr.Emp.FromDTOEmp(memp).Org.Tel)

        Dim oTb As DataTable = mDsCtas.Tables(0)
        Dim DblQuot As Decimal = 0
        Dim oRow As DataRow
        For Each oRow In oTb.Rows
            Select Case oRow(ColCta.Cta).ToString
                Case "47511"
                    oFitxer.SetTreball(oRow(ColCta.Clis), oRow(ColCta.Base), oRow(ColCta.Cuota))
                Case "47512"
                    oFitxer.SetProfessionals(oRow(ColCta.Clis), oRow(ColCta.Base), oRow(ColCta.Cuota))
            End Select
        Next

        Dim oFrm As New Frm_FileAEAT111
        With oFrm
            .File = oFitxer
            .Show()
        End With
    End Sub


End Class