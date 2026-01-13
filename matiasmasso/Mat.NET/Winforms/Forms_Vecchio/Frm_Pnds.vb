

Public Class Frm_Pnds
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mRol As Rols = Rols.Deutor
    Private WithEvents mPnd As Pnd

    Private Enum Rols
        Deutor
        Creditor
    End Enum

    Private Enum Cols
        Id
        Warn
        WarnIco
        Vto
        Eur
        Plan
        CtaGuid
        Cta
        Fra
        Fch
        Clx
        Fpg
        CliGuid
    End Enum

    Private Sub Frm_Pnds_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadCfps()
    End Sub


    Private Sub LoadGrid()

        Dim sAD As String = IIf(CurrentRol() = Rols.Deutor, "D", "A")
        Dim SQL As String = "SELECT ID, '0' AS WARN, PND.vto, PND.eur, PND.PGCPLAN, PgcCta.Guid, PgcCta.Id, PND.fra, PND.fch, CLX.clx, PND.fpg, PND.ContactGuid " _
        & "FROM PND " _
        & "LEFT OUTER JOIN CLX ON PND.ContactGuid = CLX.Guid " _
        & "INNER JOIN PgcCta ON PND.CtaGuid = PgcCta.Guid " _
        & "WHERE  PND.EMP=" & mEmp.Id & " AND " _
        & "PND.cfp =" & ComboBoxCfp.SelectedValue & " AND " _
        & "PND.ad = '" & sAD & "' AND STATUS<" & Pnd.StatusCod.saldat & "  " _
        & "ORDER BY PND.vto, PND.cli, PND.Fch, PND.fra"
        mDs =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = mDs.Tables(0)

        Dim oCol As DataColumn = oTb.Columns.Add("warnICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.WarnIco)

        Dim DblSum As Decimal
        Dim oRow As DataRow
        Dim oCta As PgcCta
        Dim oCcd As Ccd
        Dim i As Integer
        Dim j As Integer
        Dim BlCuadra As Boolean
        For Each oRow In oTb.Rows
            If oRow(Cols.Warn) = 0 Then
                oCta = New PgcCta(CType(oRow(Cols.CtaGuid), Guid))
                oCcd = New Ccd(New Contact(CType(oRow(Cols.CliGuid), Guid)), Today.Year, oCta)
                BlCuadra = oCcd.Cuadra
                For j = i To oTb.Rows.Count - 1
                    If oTb.Rows(j)(Cols.CliGuid).Equals(oRow(Cols.CliGuid)) Then
                        If oTb.Rows(j)(Cols.CtaGuid).Equals(oRow(Cols.CtaGuid)) Then
                            oTb.Rows(j)(Cols.Warn) = IIf(BlCuadra, "0", "1")
                        End If
                    End If
                Next
            End If
            DblSum += oRow(Cols.Eur)
            i = i + 1
        Next
        LabelSum.Text = "total: " & Format(DblSum, "#,##0.00") & " EUR"
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
                .DefaultCellStyle.BackColor = Color.Transparent
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False

            With .Columns(Cols.Id)
                .Visible = False
            End With
            With .Columns(Cols.Warn)
                .Visible = False
            End With
            With .Columns(Cols.WarnIco)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Vto)
                .HeaderText = "venciment"
                .Width = 75
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Eur)
                .HeaderText = "import"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Plan)
                .Visible = False
            End With
            With .Columns(Cols.CtaGuid)
                .Visible = False
            End With
            With .Columns(Cols.Cta)
                .HeaderText = "cta"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Fra)
                .HeaderText = "factura"
                .Width = 40
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .Width = 70
                .HeaderText = "data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Clx)
                .HeaderText = IIf(CurrentRol() = Rols.Deutor, "deutor", "creditor")
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Fpg)
                .Width = 100
                .HeaderText = "observacions"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.CliGuid)
                .Visible = False
            End With

        End With
    End Sub

    Private Sub LoadCfps()
        Dim SQL As String = "SELECT PND.CFP, " _
        & "CFP.NOM " _
        & "FROM PND LEFT OUTER JOIN " _
        & "CFP ON PND.cfp = CFP.Id " _
        & "WHERE PND.EMP=" & mEmp.Id & " AND " _
        & "PND.ad LIKE '" & IIf(CurrentRol() = Rols.Deutor, "D", "A") & "' " _
        & "GROUP BY PND.cfp, CFP.Nom " _
        & "ORDER BY PND.cfp"

        Dim oDs As DataSet =  DAL.SQLHelper.GetDataset(SQL, New List(Of Exception))
        Dim oTb As DataTable = oDs.Tables(0)
        With ComboBoxCfp
            .DisplayMember = "NOM"
            .ValueMember = "CFP"
            .DataSource = oTb
            .SelectedValue = 2 'reposicio fons
        End With
    End Sub

    Private Sub ComboBoxCfp_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxCfp.SelectedIndexChanged
        LoadGrid()
    End Sub

    Private Function CurrentRol() As Rols
        If RadioButtonDeutors.Checked Then
            Return Rols.Deutor
        Else
            Return Rols.Creditor
        End If
    End Function

    Private Sub RadioButtonDeutors_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButtonDeutors.CheckedChanged
        LoadCfps()
    End Sub

    Private Function CurrentPnd() As Pnd
        Dim oPnd As Pnd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim LngId As Long = oRow.Cells(Cols.Id).Value
            oPnd = New Pnd(LngId)
        End If
        Return oPnd
    End Function

 
  
    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.WarnIco
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                Dim oCod As String = oRow.Cells(Cols.Warn).Value
                If oCod = "1" Then
                    e.Value = My.Resources.warn
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oPnd As Pnd = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            oPnd = New Pnd(oRow.Cells(Cols.Id).Value)
            Dim oMenu_Pnd As New Menu_Pnd(oPnd)
            AddHandler oMenu_Pnd.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Pnd.Range)
        End If
        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fra

        If DataGridView1.Rows.Count > 0 Then
            i = DataGridView1.CurrentRow.Index
            j = DataGridView1.CurrentCell.ColumnIndex
            iFirstRow = DataGridView1.FirstDisplayedScrollingRowIndex()
        End If

        LoadGrid()

        If DataGridView1.Rows.Count = 0 Then
        Else
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(iFirstVisibleCell)
            End If
        End If
    End Sub

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Dim oPnd As Pnd = CurrentPnd()
        Dim oFrm As New Frm_Contact_Pnd(oPnd.ToDTO)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)

        If oRow.Cells(Cols.Eur).Value <= 0 Then
            PaintGradientRowBackGround(e, Color.LightSalmon)
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke
        'If (e.State And DataGridViewElementStates.Selected) = _
        'DataGridViewElementStates.Selected Then
        'oBgColor = DataGridView1.DefaultCellStyle.SelectionBackColor
        'End If

        ' Calculate the bounds of the row.
        Dim rowBounds As New Rectangle( _
            0, e.RowBounds.Top, _
            Me.DataGridView1.Columns.GetColumnsWidth( _
            DataGridViewElementStates.Visible) - _
            Me.DataGridView1.HorizontalScrollingOffset + 1, _
            e.RowBounds.Height)

        ' Paint the custom selection background.
        Dim backbrush As New System.Drawing.Drawing2D.LinearGradientBrush( _
        rowBounds, _
        oColor, _
        oBgColor, _
        System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        'System.Drawing.Drawing2D.LinearGradientBrush(rowBounds, _
        'e.InheritedRowStyle.BackColor, _
        'oColor, _
        'System.Drawing.Drawing2D.LinearGradientMode.Horizontal)
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripButtonExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonExcel.Click
        Dim oSheet As DTOExcelSheet = BLLExcel.Sheet(mDs)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class