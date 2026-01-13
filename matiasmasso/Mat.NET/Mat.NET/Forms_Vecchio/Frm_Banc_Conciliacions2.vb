

Public Class Frm_Banc_Conciliacions2
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mCtaBancs As PgcCta = PgcPlan.FromToday.Cta(DTOPgcPlan.ctas.bancs)
    Private mShowAllQ43 As Boolean = False
    Private mAllowEvents As Boolean = False


    Private Enum ColsBancs
        Id
        Nom
        Fch
        Sdo
    End Enum

    Private Sub Frm_Banc_Conciliacions2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadBancs()
        SetContextMenuBancs()
    End Sub

    Private Sub LoadBancs()
        Cursor = Cursors.WaitCursor

        Dim SQL As String = "SELECT CliBnc.cli, CliBnc.Abr, C.LASTFCH, dbo.Saldo(CliBnc.emp, @CTA, CliBnc.cli, C.LASTFCH) AS SALDO " _
        & "FROM CliBnc LEFT OUTER JOIN " _
        & "(SELECT A.emp, A.banc, MAX(A.fchOperacio) AS LASTFCH " _
        & "FROM AEB43 AS A INNER JOIN " _
        & "(SELECT emp, banc, fchOperacio, MAX(Id) AS MAXID " _
        & "FROM AEB43 " _
        & "GROUP BY emp, banc, fchOperacio) AS B ON A.Id = B.MAXID AND A.emp = B.emp AND A.banc = B.banc " _
        & "WHERE (A.saldoPosterior = dbo.Saldo(A.emp, @CTA, A.banc, A.fchOperacio)) " _
        & "GROUP BY A.emp, A.banc) AS C ON CliBnc.emp = C.emp AND CliBnc.cli = C.banc " _
        & "WHERE CliBnc.emp = @EMP AND CliBnc.actiu = 1 AND CLIBNC.ISOPAIS LIKE 'ES' " _
        & "ORDER BY CliBnc.Abr"

        mAllowEvents = False
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@CTA", mCtaBancs.Id)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridViewBancs
            With .RowTemplate
                .Height = DataGridViewBancs.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(ColsBancs.Id)
                .Visible = False
            End With
            With .Columns(ColsBancs.Nom)
                .HeaderText = "banc"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsBancs.Fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsBancs.Sdo)
                .HeaderText = "saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With

        End With
        mAllowEvents = True
        Cursor = Cursors.Default
    End Sub

    Private Sub DataGridViewBancs_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewBancs.SelectionChanged
        If mAllowEvents Then
            SetContextMenuBancs()
            LoadQ43()
            SetContextMenuQ43()
            LoadCcb()
        End If
    End Sub

    Private Enum ColsQ43
        Id
        fch
        Text
        Deb
        Hab
        SdoAnt
        SdoPost
        Match
    End Enum

    Private Function CurrentBanc() As Banc
        Dim oRetVal As Banc = Nothing
        Dim oRow As DataGridViewRow = DataGridViewBancs.CurrentRow
        If oRow IsNot Nothing Then
            Dim BancId As Integer = oRow.Cells(ColsBancs.Id).Value
            oRetVal = MaxiSrvr.Banc.FromNum(mEmp, BancId)
        End If
        Return oRetVal
    End Function

    Private Function CurrentLastFch() As Date
        Dim DtFch As Date = Date.MinValue
        Dim oRow As DataGridViewRow = DataGridViewBancs.CurrentRow
        If oRow IsNot Nothing Then
            If Not IsDBNull(oRow.Cells(ColsBancs.Fch).Value) Then
                DtFch = oRow.Cells(ColsBancs.Fch).Value
            End If
        End If
        Return DtFch
    End Function

    Private Sub LoadQ43()
        Dim SQL As String = "SELECT AEB43.Id, AEB43.fchOperacio, AEB43.concepte, " _
        & "(CASE WHEN AEB43.eur>0 THEN AEB43.eur ELSE 0 END) AS DEBE, " _
        & "(CASE WHEN AEB43.eur<0 THEN -AEB43.eur ELSE 0 END) AS HABER, " _
        & "AEB43.saldoAnterior, AEB43.saldoPosterior, (CASE WHEN CCB.CCA IS NULL THEN 0 ELSE 1 END) AS MATCH " _
        & "FROM AEB43 LEFT OUTER JOIN " _
        & "CCB ON AEB43.emp = CCB.Emp AND YEAR(AEB43.fchOperacio) = CCB.yea AND CCB.cta LIKE @CTA AND AEB43.banc = CCB.cli AND AEB43.eur = (CASE WHEN DH = 1 THEN CCB.EUR ELSE - CCB.EUR END) AND CCB.fch > @FCH " _
        & "WHERE AEB43.emp =@EMP AND AEB43.banc =@BANC "
        If Not mShowAllQ43 Then
            SQL = SQL & " AND AEB43.fchOperacio > @FCH "
        End If
        SQL = SQL & "ORDER BY AEB43.fchOperacio, AEB43.Id"

        mAllowEvents = False
        Dim Dtfch As Date = CurrentLastFch()
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi, "@EMP", mEmp.Id, "@CTA", mCtaBancs.Id, "@BANC", CurrentBanc.Id, "@FCH", Dtfch)
        Dim oTb As DataTable = oDs.Tables(0)
        With DataGridViewQ43
            With .RowTemplate
                .Height = DataGridViewQ43.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = True
            .MultiSelect = False
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(ColsQ43.Id)
                .Visible = False
            End With
            With .Columns(ColsQ43.fch)
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(ColsQ43.Text)
                .HeaderText = "concepte"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(ColsQ43.Deb)
                .HeaderText = "cobros"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(ColsQ43.Hab)
                .HeaderText = "pagos"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(ColsQ43.SdoAnt)
                .HeaderText = "sdo.ant."
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.ForeColor = Color.Gray
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(ColsQ43.SdoPost)
                .HeaderText = "nou saldo"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 90
                .DefaultCellStyle.BackColor = Color.AliceBlue
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(ColsQ43.Match)
                .Visible = False
            End With
        End With
        mAllowEvents = True
        Cursor = Cursors.Default
    End Sub

    Private Sub DataGridViewQ43_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridViewQ43.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridViewQ43.Rows(e.RowIndex)
        Select Case oRow.Cells(ColsQ43.Match).Value
            Case 1
                oRow.DefaultCellStyle.BackColor = Color.LightGray
            Case Else
                oRow.DefaultCellStyle.BackColor = Color.LightSalmon
        End Select
    End Sub

    Private Sub LoadCcb()
        Dim DtFch As Date = CurrentLastFch()
        If DtFch = Date.MinValue Then DtFch = Today 'en cas que la data de ultim saldo quadrat estigui en blanc, treu tot l'extracte fins a data d'avui
        Dim oCcd As New Ccd(CurrentBanc, DtFch.Year, mCtaBancs)
        Xl_Ccd_Extracte1.ShadowToDate = DtFch
        Xl_Ccd_Extracte1.Ccd = oCcd
    End Sub

    Private Sub RefreshRequestQ43(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsQ43.Text
        Dim oGrid As DataGridView = DataGridViewQ43
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadQ43()

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

    Private Sub RefreshRequestCcb(ByVal sender As Object, ByVal e As System.EventArgs)
        LoadCcb()
    End Sub


    Private Sub RefreshRequestBancs(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = ColsBancs.Nom
        Dim oGrid As DataGridView = DataGridViewBancs
        Dim oRow As DataGridViewRow = oGrid.CurrentRow

        If oRow IsNot Nothing Then
            i = oRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        LoadBancs()

        If oGrid.Rows.Count = 0 Then
        Else
            oGrid.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > oGrid.Rows.Count - 1 Then
                oGrid.CurrentCell = oGrid.Rows(oGrid.Rows.Count - 1).Cells(j)
            Else
                oGrid.CurrentCell = oGrid.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If

        SetContextMenuBancs()
        LoadQ43()
        SetContextMenuQ43()
        LoadCcb()
    End Sub

    Private Sub SetContextMenuBancs()
        Dim oContextMenu As New ContextMenuStrip
        Dim oBanc As Banc = CurrentBanc()

        Dim oMenuItem As New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequestBancs)
        oContextMenu.Items.Add(oMenuItem)

        If oBanc IsNot Nothing Then
            Dim oMenu_Banc As New Menu_Banc(oBanc)
            AddHandler oMenu_Banc.AfterImportQ43, AddressOf RefreshRequestBancs
            oContextMenu.Items.AddRange(oMenu_Banc.Range)
        End If

        DataGridViewBancs.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub SetContextMenuQ43()
        Dim oContextMenu As New ContextMenuStrip

        Dim oMenuItem As New ToolStripMenuItem("refresca", My.Resources.refresca, AddressOf RefreshRequestQ43)
        oContextMenu.Items.Add(oMenuItem)
        oMenuItem = New ToolStripMenuItem("buscar", My.Resources.search_16, AddressOf Do_SearchCcaFromQ43)
        oContextMenu.Items.Add(oMenuItem)
        oMenuItem = New ToolStripMenuItem("crear assentament", My.Resources.NewDoc, AddressOf Do_NewCcaFromQ43)
        oContextMenu.Items.Add(oMenuItem)
        oMenuItem = New ToolStripMenuItem("mostrar registres anteriors", My.Resources.REDO, AddressOf Do_ShowAll43)
        oMenuItem.Checked = mShowAllQ43
        oContextMenu.Items.Add(oMenuItem)

        DataGridViewQ43.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_ShowAll43(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oMenuItem As ToolStripMenuItem = DirectCast(sender, ToolStripMenuItem)
        mShowAllQ43 = Not mShowAllQ43
        oMenuItem.Checked = mShowAllQ43
        RefreshRequestQ43(sender, e)
    End Sub

    Private Sub Do_SearchCcaFromQ43(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = DataGridViewQ43.CurrentRow
        If oRow IsNot Nothing Then
            Dim DcEur As Decimal = Math.Abs(oRow.Cells(ColsQ43.Deb).Value - oRow.Cells(ColsQ43.Hab).Value)
            Dim iYea As Integer = CDate(oRow.Cells(ColsQ43.fch).Value).Year
            Dim oFrm As New Frm_Cca_Search(iYea, DcEur)
            oFrm.Show()
        End If

    End Sub

    Private Sub Do_NewCcaFromQ43(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oRow As DataGridViewRow = DataGridViewQ43.CurrentRow
        If oRow IsNot Nothing Then
            Dim DcEur As Decimal = oRow.Cells(ColsQ43.Deb).Value - oRow.Cells(ColsQ43.Hab).Value
            Dim oDh As DTOCcb.DhEnum = IIf(DcEur > 0, DTOCcb.DhEnum.Debe, DTOCcb.DhEnum.Haber)
            Dim oCca As new cca(BLL.BLLApp.emp)
            With oCca
                .fch = oRow.Cells(ColsQ43.fch).Value
                .Txt = oRow.Cells(ColsQ43.Text).Value
                .ccbs.Add(New Ccb(mCtaBancs, CurrentBanc, New maxisrvr.Amt(Math.Abs(DcEur)), oDh))
            End With
            Dim oFrm As New Frm_Cca(oCca)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestQ43
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequestCcb
            oFrm.Show()
        End If

    End Sub

    Private Sub DataGridViewQ43_SelectionChanged(ByVal sender As Object, ByVal e As MatEventArgs) Handles DataGridViewQ43.SelectionChanged
        SetContextMenuQ43()
    End Sub

    Private Sub Xl_Ccd_Extracte1_AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs) Handles Xl_Ccd_Extracte1.AfterUpdate
        RefreshRequestQ43(sender, e)
    End Sub
End Class