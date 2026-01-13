
Imports System.Drawing

Public Class Frm_Last_Fras
    Private mDs As DataSet
    Private mEmp as DTOEmp = BLL.BLLApp.Emp
    Private mDsYeas As DataSet
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        CliGuid
        Pdf
        Ico
        Yea
        Id
        Fch
        Cli
        Clx
        EurBase
        EurLiq
        Fpg
        Cfp
    End Enum

    Private Enum ColsMonth
        Mes
        Abr
        Count
        Eur
    End Enum

    Private Sub Frm_Last_Fras_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetYeas()
        SetMonths()
        Refresca()
        EnableYeaButtons()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        Dim SQL As String = "SELECT Fra.Guid, Fra.CliGuid, (CASE WHEN CCA.Hash IS NULL THEN 0 ELSE 1 END) AS PDF, FRA.YEA,FRA.FRA,FRA.FCH,FRA.CLI,CLX.CLX,FRA.EURBASE,FRA.EURLIQ,FRA.FPG,FRA.CFP " _
        & "FROM fra INNER JOIN " _
        & "CLX ON FRA.EMP=CLX.EMP AND FRA.CLI=CLX.CLI LEFT OUTER JOIN " _
        & "CCA ON CCA.GUID = FRA.CCAGUID " _
        & "WHERE FRA.EMP=" & mEmp.Id & " AND " _
        & "FRA.YEA=" & CurrentYea() & " AND " _
        & "MONTH(FRA.FCH)=" & CurrentMonth() & " " _
        & "ORDER BY FRA.fra DESC"

        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix icono Pdf
        Dim oCol As DataColumn = oTb.Columns.Add("PDFICO", System.Type.GetType("System.Byte[]"))
        oCol.SetOrdinal(Cols.Ico)


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

            With .Columns(Cols.Guid)
                .Visible = False
            End With
            With .Columns(Cols.CliGuid)
                .Visible = False
            End With
            With .Columns(Cols.Pdf)
                .Visible = False
            End With
            With .Columns(Cols.Ico)
                .HeaderText = ""
                .Width = 16
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Yea)
                .Visible = False
            End With
            With .Columns(Cols.Id)
                .HeaderText = "Factura"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 45
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 65
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Cli)
                .Visible = False
            End With
            With .Columns(Cols.Clx)
                .HeaderText = "client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.EurBase)
                .HeaderText = "Base"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.EurLiq)
                .HeaderText = "Liquid"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 70
                .DefaultCellStyle.Format = "#,##0.00;-#,##0.00;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fpg)
                .HeaderText = "Forma de pago"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Cfp)
                .Visible = False
            End With
        End With
    End Sub


    Private Sub SetYeas()
        Dim SQL As String = "SELECT YEA FROM Fra " _
        & "WHERE EMP=" & mEmp.Id & " " _
        & "GROUP BY YEA " _
        & "ORDER BY YEA DESC"

        mDsYeas = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = mDsYeas.Tables(0)
        Dim oRow As DataRow
        With ToolStripComboBoxYea
            .BeginUpdate()
            For Each oRow In oTb.Rows
                .Items.Add(oRow("YEA"))
            Next
            .EndUpdate()
            If oTb.Rows.Count > 0 Then .SelectedIndex = 0
        End With
    End Sub

    Private Sub SetMonths()
        Dim SQL As String = "SELECT MONTH(FCH) AS MES, COUNT(FRA) AS FRAS, SUM(EURBASE) AS EUR FROM FRA WHERE " _
        & "EMP=" & App.Current.Emp.Id & " AND " _
        & "YEA=" & CurrentYea() & " " _
        & "GROUP BY MONTH(FCH) " _
        & "ORDER BY MONTH(FCH) DESC"
        Dim oDs As DataSet = maxisrvr.GetDataset(SQL, maxisrvr.Databases.Maxi)
        Dim oTb As DataTable = oDs.Tables(0)

        'afegeix Columna abreviació mes
        Dim oCol As DataColumn = oTb.Columns.Add("ABR", System.Type.GetType("System.String"))
        oCol.SetOrdinal(ColsMonth.Abr)

        With DataGridViewMonths
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With
            .DataSource = oTb
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = False
            .AllowUserToResizeRows = False
            With .Columns(ColsMonth.Mes)
                .Visible = False
            End With
            With .Columns(ColsMonth.Abr)
                .HeaderText = "mes"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 35
            End With
            With .Columns(ColsMonth.Count)
                .HeaderText = "factures"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .Width = 50
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(ColsMonth.Eur)
                .HeaderText = "Bases"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .DefaultCellStyle.Format = "#,##0.00 €;-#,##0.00 €;"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .CurrentCell = DataGridViewMonths(ColsMonth.Abr, 0)
        End With

    End Sub

    Private Function CurrentYea() As Integer
        Dim iYea As Integer = ToolStripComboBoxYea.SelectedItem
        Return iYea
    End Function

    Private Function CurrentMonth() As Integer
        Dim iCol As Integer = ColsMonth.Mes
        Dim iRow As Integer = DataGridViewMonths.CurrentRow.Index
        Dim iMonth As Integer = DataGridViewMonths.Item(iCol, iRow).Value
        Return iMonth
    End Function


    Private Function CurrentFras() As Fras
        Dim oFras As New Fras

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim IntYea As Integer = CurrentYea()
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oCliGuid As Guid = oRow.Cells(Cols.CliGuid).Value
                Dim oCli As New Client(oCliGuid)
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oFra = New Fra(oGuid)
                oFra.Yea = IntYea
                oFra.Client = oCli
                oFra.Id = oRow.Cells(Cols.Id).Value
                oFra.Fch = oRow.Cells(Cols.Fch).Value
                oFras.Add(oFra)
            Next
            oFras.Sort()
        Else
            'Dim oFra As Fra = CurrentFra()
            'If oFra IsNot Nothing Then
            'oFras.Add(CurrentFra)
            'End If
        End If
        Return oFras
    End Function


    Private Sub EnableYeaButtons()
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
        AnyanteriorToolStripButton.Enabled = (Idx < iYeas - 1)
        AnysegüentToolStripButton.Enabled = (Idx > 0)
    End Sub

    Private Sub AnyanteriorToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnyanteriorToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Dim iYeas As Integer = mDsYeas.Tables(0).Rows.Count
        Idx = Idx + 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub AnysegüentToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AnysegüentToolStripButton.Click
        Dim Idx As Integer = ToolStripComboBoxYea.SelectedIndex
        Idx = Idx - 1
        ToolStripComboBoxYea.SelectedIndex = Idx
        EnableYeaButtons()
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oFras As Fras = CurrentFras()

        If oFras.Count > 0 Then
            Dim oMenu_Fra As New Menu_Fra(oFras)
            AddHandler oMenu_Fra.Progress, AddressOf onProgress
            AddHandler oMenu_Fra.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Fra.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub onProgress(sender As Object, e As ProgressEventArgs)
        Select Case e.Codi
            Case ProgressEventArgs.Codis.Start
                With ProgressBar1
                    .Maximum = e.MaxCount
                    .Value = 0
                    .Visible = True
                End With
                LabelProgressCaption.Text = ""
                LabelProgressCaption.Visible = True
                Application.DoEvents()
            Case ProgressEventArgs.Codis.Stop
                ProgressBar1.Visible = False
                LabelProgressCaption.Visible = False
                Application.DoEvents()
            Case ProgressEventArgs.Codis.Increment
                ProgressBar1.Increment(1)
                LabelProgressCaption.Text = e.Caption
                Application.DoEvents()
        End Select
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        mAllowEvents = False
        Dim i As Integer = DataGridView1.CurrentRow.Index
        Dim j As Integer = DataGridView1.CurrentCell.ColumnIndex
        Dim iFirstRow As Integer = DataGridView1.FirstDisplayedScrollingRowIndex()
        Refresca()
        If DataGridView1.Rows.Count > 0 Then
            DataGridView1.FirstDisplayedScrollingRowIndex() = iFirstRow
            mAllowEvents = True

            If i > DataGridView1.Rows.Count - 1 Then
                DataGridView1.CurrentCell = DataGridView1.Rows(DataGridView1.Rows.Count - 1).Cells(j)
            Else
                DataGridView1.CurrentCell = DataGridView1.Rows(i).Cells(j)
            End If
        End If
        SetContextMenu()
    End Sub

    Private Sub DataGridView1_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Ico
                Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                If oRow.Cells(Cols.Pdf).Value = 1 Then
                    e.Value = My.Resources.pdf
                Else
                    e.Value = My.Resources.empty
                End If
        End Select
    End Sub


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        ShowFra()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            ShowFra()
            e.Handled = True
        End If
    End Sub

    Private Sub ShowFra()
        Dim oFras As Fras = CurrentFras()
        If oFras.Count = 1 Then
            Dim oFrm As New Frm_Fra(oFras(0))
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            With oFrm
                .Show()
            End With

        End If
    End Sub


    Private Sub DataGridView1_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim DblEur As Decimal = oRow.Cells(Cols.EurBase).Value
        Select Case DblEur
            Case 0
                oRow.DefaultCellStyle.BackColor = Color.Red
            Case Is < 0
                PaintGradientRowBackGround(e, Color.LightSalmon)
            Case Else
                Dim oCod As Client.Cfps = CType(oRow.Cells(Cols.Cfp).Value, Client.Cfps)
                Select Case oCod
                    Case Contact.Cfps.Contado
                        PaintGradientRowBackGround(e, Color.LightBlue)
                    Case Contact.Cfps.Reposicion_fondos
                        PaintGradientRowBackGround(e, Color.LemonChiffon)
                    Case Contact.Cfps.A_negociar
                        PaintGradientRowBackGround(e, Color.LightGray)
                    Case Contact.Cfps.Giro_sin_domiciliar
                        PaintGradientRowBackGround(e, Color.LightCyan)
                    Case Contact.Cfps.Transferencia
                        PaintGradientRowBackGround(e, Color.LightGreen)
                    Case Else
                        oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
                End Select
        End Select
    End Sub

    Private Sub PaintGradientRowBackGround(ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs, ByVal oColor As System.Drawing.Color)

        ' Do not automatically paint the focus rectangle.
        e.PaintParts = e.PaintParts And Not DataGridViewPaintParts.Focus


        ' Determine whether the cell should be painted with the 
        ' custom selection background.
        Dim oBgColor As System.Drawing.Color = Color.WhiteSmoke

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
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.SelectionChanged
        SetContextMenu()
    End Sub

    Private Sub ToolStripComboBoxYea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ToolStripComboBoxYea.SelectedIndexChanged
        If mAllowEvents Then
            EnableYeaButtons()
            SetMonths()
            Refresca()
        End If
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Refresca()
    End Sub

    Private Sub DataGridViewMonths_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DataGridViewMonths.CellFormatting
        Select Case e.ColumnIndex
            Case ColsMonth.Abr
                Dim oRow As DataGridViewRow = DataGridViewMonths.Rows(e.RowIndex)
                Dim iMes As Integer = oRow.Cells(ColsMonth.Mes).Value
                e.Value = BLL.BLLSession.Current.Lang.MesAbr(iMes)
        End Select
    End Sub

    Private Sub DataGridViewMonths_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridViewMonths.SelectionChanged
        If mAllowEvents Then
            EnableYeaButtons()
            Refresca()
        End If
    End Sub
End Class