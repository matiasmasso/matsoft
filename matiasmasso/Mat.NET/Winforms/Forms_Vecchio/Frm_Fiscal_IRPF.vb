
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

    Private _User As DTOUser

    Private _CtaIrpfTreballadors As DTOPgcCta
    Private _CtaIrpfProfesionals As DTOPgcCta
    Private _CtaIrpfLloguers As DTOPgcCta
    Private _CtaIrpf As DTOPgcCta
    Private _CtaNominas As DTOPgcCta
    Private _CtaLloguers As DTOPgcCta
    Private _CtaBancs As DTOPgcCta

    Private mAllowEvents As Boolean

    Private Enum ColCta
        CodIcon
        Icon
        CtaGuid
        Cta
        Txt
        Clis
        Base
        Cuota
    End Enum

    Private Enum ColScta
        CodIcon
        Icon
        Contact
        Base
        Cuota
        Tipus
        Clx
    End Enum

    Private Enum ColCca
        Guid
        Cca
        Fch
        Base
        Cuota
        Tipus
        Txt
    End Enum

    Private Sub Frm_Fiscal_IRPF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _User = BLL.BLLSession.Current.User
        'BLLUser.Load(_User)

        LoadFch()

        Dim oExercici As DTOExercici = BLL.BLLExercici.FromYear(mFchDesde.Year)
        _CtaIrpfTreballadors = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.IrpfTreballadors, oExercici)
        _CtaIrpfProfesionals = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.IrpfProfessionals, oExercici)
        _CtaIrpfLloguers = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.IrpfLloguers, oExercici)
        _CtaNominas = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Nomina, oExercici)
        _CtaLloguers = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.lloguers, oExercici)
        _CtaBancs = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.bancs, oExercici)
        _CtaIrpf = BLL.BLLPgcCta.FromCod(DTOPgcPlan.Ctas.Irpf, oExercici)


        LoadBancs()
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
        Dim SQL As String = "SELECT 0 AS ICON " _
        & ", CUOTAS.CtaGuid, CtaQuota.id " _
        & ", (CASE WHEN CUOTAS.CtaGuid = '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfTreballadors).Guid.ToString & "' THEN 'TREBALLADORS' WHEN CUOTAS.CtaGuid ='" & _CtaIrpfProfesionals.Guid.ToString & "' THEN 'PROFESIONALS' WHEN CUOTAS.CtaGuid ='" & _CtaIrpfLloguers.Guid.ToString & "' THEN 'LLOGUERS' ELSE '??' END) AS TXT " _
        & ", COUNT(DISTINCT CUOTAS.ContactGuid) AS CLIS " _
        & ", SUM(CASE WHEN BASES.DH = 1 THEN BASES.EUR ELSE - BASES.EUR END) AS BASE " _
        & ", SUM(CASE WHEN CUOTAS.DH = 2 THEN CUOTAS.EUR ELSE - CUOTAS.EUR END) AS CUOTA " _
        & "FROM CCB AS CUOTAS " _
        & "INNER JOIN Cca ON Cuotas.CcaGuid = Cca.Guid " _
        & "INNER JOIN PgcCta AS CtaQuota ON CUOTAS.CtaGuid = CtaQuota.Guid " _
        & "INNER JOIN CCB AS BASES " _
        & "INNER JOIN PgcCta AS CtaBase ON BASES.CtaGuid = CtaBase.Guid ON CUOTAS.CcaGuid = BASES.CcaGuid " _
        & "AND ((CUOTAS.CtaGuid = '" & _CtaIrpfTreballadors.Guid.ToString & "' AND BASES.CtaGuid = '" & _CtaNominas.Guid.ToString & "') " _
        & "OR (CUOTAS.CtaGuid = '" & _CtaIrpfProfesionals.Guid.ToString & "' AND (CtaBase.Id LIKE '2%' OR CtaBase.Id LIKE '6%') AND NOT CtaBase.id LIKE '63%') " _
        & "OR (CUOTAS.CtaGuid = '" & _CtaIrpfLloguers.Guid.ToString & "' AND BASES.CtaGuid = '" & _CtaLloguers.Guid.ToString & "')) " _
        & "WHERE CUOTAS.Emp = " & CInt(BLL.BLLApp.Emp.Id) & " " _
        & "AND CUOTAS.fch BETWEEN '" & sFchDesde & "' AND '" & sFchHasta & "' " _
        & "AND Cca.Ccd<>" & DTOCca.CcdEnum.AperturaExercisi & " " _
        & "GROUP BY CUOTAS.CtaGuid, CtaQuota.id " _
        & "ORDER BY CtaQuota.id"
        mDsCtas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsCtas.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("ICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(ColCta.Icon)

        oTb.PrimaryKey = {oTb.Columns(ColCta.CtaGuid)}

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
            With .Columns(ColCta.CtaGuid)
                .Visible = False
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

    Private Function CurrentCta() As DTOPgcCta
        Dim oCta As DTOPgcCta = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCtas.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(ColCta.CtaGuid).Value
            oCta = New DTOPgcCta(oGuid)
            oCta.Id = oRow.Cells(ColCta.Cta).Value
        End If
        Return oCta
    End Function

    Private Function CurrentBanc() As DTOBanc
        Dim retval As DTOBanc = ComboBoxBanc.SelectedItem
        Return retval
    End Function
#End Region

#Region "SCtas"

    Private Sub LoadSCtas(Optional ByVal oCta As DTOPgcCta = Nothing)
        If oCta Is Nothing Then oCta = CurrentCta()
        Dim sFchHasta As String = Format(mFchHasta, "yyyyMMdd")
        Dim sFchDesde As String = Format(mFchDesde, "yyyyMMdd")
        Dim oPlan As PgcPlan = PgcPlan.FromYear(mFchHasta.Year)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT (CASE WHEN SDO.EUR=0 THEN 0 WHEN SDO.EUR IS NULL THEN 0 ELSE 1 END) AS ICON ")
        sb.AppendLine(", CUOTAS.ContactGuid ")
        sb.AppendLine(", SUM(CASE WHEN BASES.DH = 1 THEN BASES.EUR ELSE - BASES.EUR END) AS BASE ")
        sb.AppendLine(", SUM(CASE WHEN CUOTAS.DH = 2 THEN CUOTAS.EUR ELSE - CUOTAS.EUR END) AS CUOTA ")
        sb.AppendLine(", '0' as TIPUS ")
        sb.AppendLine(", CLX.CLX ")
        sb.AppendLine("FROM CCB AS CUOTAS ")
        sb.AppendLine("INNER JOIN CCB AS BASES ON CUOTAS.CcaGuid = BASES.CcaGuid ")
        sb.AppendLine("INNER JOIN PgcCta AS CtaBase ON BASES.CtaGuid = CtaBase.Guid AND ")
        sb.AppendLine("     ((CUOTAS.CtaGuid = '" & _CtaIrpfTreballadors.Guid.ToString & "' AND BASES.CtaGuid = '" & _CtaNominas.Guid.ToString & "') OR ")
        sb.AppendLine("     (CUOTAS.CtaGuid = '" & _CtaIrpfProfesionals.Guid.ToString & "' AND (CtaBase.Id LIKE '2%' OR CtaBase.Id LIKE '6%') AND NOT CtaBase.Id LIKE '63%') OR ")
        sb.AppendLine("     (CUOTAS.CtaGuid = '" & _CtaIrpfLloguers.Guid.ToString & "' AND BASES.CtaGuid = '" & _CtaLloguers.Guid.ToString & "')) ")
        sb.AppendLine("INNER JOIN CLX ON CUOTAS.ContactGuid=CLX.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT EMP, YEA, CtaGuid, ContactGuid, SUM(CASE WHEN DH=1 THEN EUR ELSE -EUR END) AS EUR ")
        sb.AppendLine("                 From CCB Where CCB.FCH <'" & sFchDesde & "' ")
        sb.AppendLine("                 GROUP BY EMP, YEA, CtaGuid, ContactGuid) SDO ON CUOTAS.EMP=SDO.EMP AND CUOTAS.YEA=SDO.YEA AND CUOTAS.CtaGuid = SDO.CtaGuid AND CUOTAS.ContactGuid = SDO.ContactGuid ")
        sb.AppendLine("WHERE CUOTAS.Emp = " & CInt(BLL.BLLApp.Emp.Id) & " ")
        sb.AppendLine("AND CUOTAS.CtaGuid = '" & oCta.Guid.ToString & "' ")
        sb.AppendLine("AND CUOTAS.fch BETWEEN '" & sFchDesde & "' AND '" & sFchHasta & "' ")
        sb.AppendLine("GROUP BY CUOTAS.ContactGuid , CLX.CLX, SDO.EUR ")
        sb.AppendLine("ORDER BY CLX.CLX")

        Dim SQL As String = sb.ToString
        mDsSctas = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)

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
            With .Columns(ColScta.Contact)
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

    Private Function CurrentCcd() As DTOCcd
        Dim retval As DTOCcd = Nothing
        Dim oRow As DataGridViewRow = DataGridViewSctas.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(ColScta.Contact).Value
            Dim oContact As New DTOContact(oGuid)
            retval = New DTOCcd(CurrentExercici, CurrentCta, CurrentContact)
        End If
        Return retval
    End Function

    Private Function CurrentExercici() As DTOExercici
        Dim DtFch As Date = DateTimePicker1.Value
        Dim retval As DTOExercici = BLL.BLLExercici.FromYear(DtFch.Year)
        Return retval
    End Function

    Private Function CurrentContact() As DTOContact
        Dim retval As DTOContact = Nothing
        Dim oRow As DataGridViewRow = DataGridViewSctas.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(ColScta.Contact).Value
            retval = New DTOContact(oGuid)
        End If
        Return retval
    End Function


    Private Sub SetMenuSctas()
        Dim oContextMenu As New ContextMenuStrip
        Dim oCcd As DTOCcd = CurrentCcd()

        If oCcd IsNot Nothing Then
            Dim oMenu_Ccd As New Menu_Ccd(oCcd)
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
        Dim oCcd As DTOCcd = CurrentCcd()
        Dim oPlan As PgcPlan = PgcPlan.FromYear(mFchHasta.Year)

        Dim sSQL As String = "SELECT Cuotas.CcaGuid, CUOTAS.CCA, " _
        & "CUOTAS.FCH, " _
        & "SUM(CASE WHEN BASES.DH = 1 THEN BASES.EUR ELSE - BASES.EUR END) AS BASE, " _
        & "SUM(CASE WHEN CUOTAS.DH = 2 THEN CUOTAS.EUR ELSE - CUOTAS.EUR END) AS CUOTA, " _
        & "'0' as TIPUS, " _
        & "CCA.TXT " _
        & "FROM CCB AS CUOTAS INNER JOIN " _
        & "CCB AS BASES ON CUOTAS.CcaGuid = BASES.CcaGuid AND " _
        & "((CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfTreballadors).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.Nomina).Id & "') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfProfessionals).Id & "' AND BASES.cta LIKE '6%' AND NOT BASES.cta LIKE '63%') OR " _
        & "(CUOTAS.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.IrpfLloguers).Id & "' AND BASES.cta LIKE '" & oPlan.Cta(DTOPgcPlan.Ctas.lloguers).Id & "')) inner join " _
        & "CCA ON CUOTAS.EMP=CCA.EMP AND CUOTAS.YEA=CCA.YEA AND CUOTAS.CCA=CCA.CCA " _
        & "WHERE CUOTAS.Emp = 1 AND " _
        & "CUOTAS.CtaGuid = '" & oCcd.Cta.Guid.ToString & "' AND " _
        & "CUOTAS.fch BETWEEN '" & sFchDesde & "' AND '" & sFchHasta & "' AND " _
        & "CUOTAS.ContactGuid ='" & oCcd.Contact.Guid.ToString & "' " _
        & "GROUP BY Cuotas.CcaGuid, CUOTAS.CCA, CUOTAS.FCH, CCA.TXT " _
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

            With .Columns(ColCca.Guid)
                .Visible = False
            End With
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
        Dim retval As Cca = Nothing
        Dim oRow As DataGridViewRow = DataGridViewCcas.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = oRow.Cells(ColCca.Guid).Value
            retval = New Cca(oGuid)
        End If
        Return retval
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

        Me.Text = "IRPF " & _User.Lang.MesAbr(mMes) & " " & mYea

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

            Me.Text = "IRPF " & _User.Lang.MesAbr(mMes) & " " & mYea

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
            LoadSCtas(_CtaIrpfTreballadors)
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
            LoadSCtas(_CtaIrpfProfesionals)
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
            LoadSCtas(_CtaIrpfLloguers)
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

        SaveSubcomptes(_CtaIrpfTreballadors)
        SaveSubcomptes(_CtaIrpfProfesionals)
        SaveSubcomptes(_CtaIrpfLloguers)

        SaveMod110()
        SaveMod115()

        MsgBox("Guardat a comptabilitat", MsgBoxStyle.Information, "MAT.NET")
        Me.Close()
    End Sub

    Private Sub SaveMod110()
        Dim oTb As DataTable = mDsCtas.Tables(0)
        Dim oRowTreb As DataRow = oTb.Rows.Find(_CtaIrpfTreballadors.Guid)
        Dim oAmtTreb As DTOAmt = BLLApp.GetAmt(CDec(oRowTreb(ColCta.Cuota)))
        Dim oRowProf As DataRow = oTb.Rows.Find(_CtaIrpfProfesionals.Guid)
        Dim oAmtProf As DTOAmt = BLLApp.GetAmt(CDec(oRowProf(ColCta.Cuota)))

        Dim oCca As DTOCca = BLL.BLLCca.Factory(mFchVto, _User, DTOCca.CcdEnum.IRPF, 100 * mYea + mMes)
        oCca.Concept = "Hisenda-Mod 111 " & Me.Text
        BLL.BLLCca.AddDebit(oCca, oAmtTreb, _CtaIrpfTreballadors)
        BLL.BLLCca.AddDebit(oCca, oAmtProf, _CtaIrpfProfesionals)
        Dim oCcbTot As DTOCcb = BLL.BLLCca.AddSaldo(oCca, _CtaIrpf)
        Dim oTot As DTOAmt = oCcbTot.Amt

        Dim exs As New List(Of Exception)
        If Not BLL.BLLCca.Update(oCca, exs) Then
            UIHelper.WarnError(exs, oCca.Concept)
        End If

        oCca = BLL.BLLCca.Factory(mFchVto, _User, DTOCca.CcdEnum.IRPF, 100 * mYea + mMes)
        oCca.Concept = CurrentBanc.Abr & "-Hisenda Mod 111 " & Me.Text
        BLL.BLLCca.AddDebit(oCca, oTot, _CtaIrpf)
        BLL.BLLCca.AddSaldo(oCca, _CtaBancs, CurrentBanc)

        exs = New List(Of Exception)
        If Not BLL.BLLCca.Update(oCca, exs) Then
            UIHelper.WarnError(exs, oCca.Concept)
        End If

    End Sub

    Private Sub SaveMod115()
        Dim oTb As DataTable = mDsCtas.Tables(0)
        Dim oRow As DataRow = oTb.Rows.Find(_CtaIrpfLloguers.Guid)
        Dim oAmt As DTOAmt = BLLApp.GetAmt(CDec(oRow(ColCta.Cuota)))

        Dim oCca As DTOCca = BLL.BLLCca.Factory(mFchVto, _User, DTOCca.CcdEnum.IRPF, 100 * mYea + mMes)
        oCca.Concept = CurrentBanc.Abr & "-HISENDA-Mod 115 " & Me.Text
        BLL.BLLCca.AddDebit(oCca, oAmt, _CtaIrpfLloguers)
        BLL.BLLCca.AddSaldo(oCca, _CtaBancs, CurrentBanc)

        Dim exs As New List(Of Exception)
        If Not BLL.BLLCca.Update(oCca, exs) Then
            UIHelper.WarnError(exs, "Model 115")
        End If
    End Sub

    Private Sub SaveSubcomptes(oCta As DTOPgcCta)
        LoadSCtas(oCta)

        Dim oCca As DTOCca = BLL.BLLCca.Factory(mFchHasta, _User, DTOCca.CcdEnum.IRPF, 100 * mYea + mMes)
        With oCca
            If oCta.Equals(_CtaIrpfTreballadors) Then
                .Concept = "HISENDA-IRPF treballadors " & Me.Text
            ElseIf oCta.Equals(_CtaIrpfProfesionals) Then
                .Concept = "HISENDA-IRPF professionals " & Me.Text
            ElseIf oCta.Equals(_CtaIrpfLloguers) Then
                .Concept = "HISENDA-Mod.115 lloguers " & Me.Text
            End If
        End With

        For Each oRow As DataRow In mDsSctas.Tables(0).Rows
            Dim oContact As New DTOContact(CType(oRow(ColScta.Contact), Guid))
            Dim oAmt As DTOAmt = BLLApp.GetAmt(CDec(oRow(ColScta.Cuota)))
            BLLCca.AddDebit(oCca, oAmt, oCta, oContact)
        Next

        BLLCca.AddSaldo(oCca, oCta)

        Dim exs As New List(Of Exception)
        If Not BLLCca.Update(oCca, exs) Then
            UIHelper.WarnError(exs, oCca.Concept)
        End If
    End Sub

    Private Sub ToolStripButtonFitxer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonFitxer.Click
        Dim oFitxer As New MaxiSrvr.MatFileAEAT111(mYea, mMes, BLLApp.Org.Nif, "Matías Massó", BLLContact.Tel(BLLApp.Org))

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