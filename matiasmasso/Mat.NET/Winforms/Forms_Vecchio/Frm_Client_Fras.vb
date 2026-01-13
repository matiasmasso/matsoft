

Public Class Frm_Client_Fras
    Private mDsYeas As DataSet
    Private mClient As Client
    Private mAllowEvents As Boolean

    Private Enum Cols
        Guid
        Yea
        Id
        Fch
        EurBase
        EurLiq
        Fpg
        Cfp
    End Enum


    Public WriteOnly Property Client() As Client
        Set(ByVal value As Client)
            mClient = value
            Me.Text = "FACTURES DE " & mClient.Clx
            SetYeas()
            Refresca()
            EnableYeaButtons()
        End Set
    End Property

    Private Sub Refresca()
        LoadGrid()
        SetContextMenu()
    End Sub

    Private Sub LoadGrid()
        mAllowEvents = False

        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT Fra.Guid, FRA.YEA,FRA.FRA,FRA.FCH,FRA.EURBASE,FRA.EURLIQ,FRA.FPG,FRA.CFP ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("INNER JOIN CliGral ON FRA.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE CliGral.Emp=" & BLLApp.Emp.Id & " ")
        sb.AppendLine("AND FRA.YEA=" & CurrentYea() & " ")
        sb.AppendLine("AND FRA.CliGuid='" & mClient.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY FRA.fra DESC")

        Dim SQL As String = sb.ToString
        Dim exs As New List(Of Exception)
        Dim oDs As DataSet = DAL.SQLHelper.GetDataset(SQL, exs)
        Dim oTb As DataTable = oDs.Tables(0)
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

        mAllowEvents = True
    End Sub


    Private Sub SetYeas()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Year(Fra.Fch) AS Yea ")
        sb.AppendLine("FROM Fra ")
        sb.AppendLine("WHERE Fra.CliGuid = '" & mClient.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY Yea DESC ")
        Dim SQL As String = sb.ToString

        mDsYeas = MaxiSrvr.GetDataset(SQL, MaxiSrvr.Databases.Maxi)
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


    Private Function CurrentYea() As Integer
        Dim iYea As Integer = ToolStripComboBoxYea.SelectedItem
        Return iYea
    End Function

    Private Function CurrentFra() As Fra
        Dim oFra As Fra = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim iId As Integer = DataGridView1.CurrentRow.Cells(Cols.Id).Value
            oFra = Fra.FromNum(BLL.BLLApp.Emp, CurrentYea, iId)
        End If
        Return oFra
    End Function

    Private Function CurrentInvoice() As DTOInvoice
        Dim retval As DTOInvoice = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            Dim oGuid As Guid = DataGridView1.CurrentRow.Cells(Cols.Guid).Value
            retval = New DTOInvoice(oGuid)
        End If
        Return retval
    End Function

    Private Function CurrentInvoices() As List(Of DTOInvoice)
        Dim retval As New List(Of DTOInvoice)

        If DataGridView1.SelectedRows.Count > 0 Then
            Dim iYea As Integer = CurrentYea()
            Dim oRow As DataGridViewRow
            For Each oRow In DataGridView1.SelectedRows
                Dim oGuid As Guid = oRow.Cells(Cols.Guid).Value
                Dim oInvoice As New DTOInvoice(oGuid)
                retval.Add(oInvoice)
            Next
            'oFras.Sort()
        Else
            Dim oInvoice As DTOInvoice = CurrentInvoice()

            If oInvoice IsNot Nothing Then
                retval.Add(CurrentInvoice)
            End If
        End If
        Return retval
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
        Dim oInvoices As List(Of DTOInvoice) = CurrentInvoices()

        If oInvoices.Count > 0 Then
            Dim oMenu_Fra As New Menu_Fra(oInvoices)
            AddHandler oMenu_Fra.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Fra.Range)
        End If

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim i As Integer
        Dim j As Integer
        Dim iFirstRow As Integer
        Dim iFirstVisibleCell As Integer = Cols.Fch
        Dim oGrid As DataGridView = DataGridView1

        If oGrid.Rows.Count > 0 Then
            i = oGrid.CurrentRow.Index
            j = oGrid.CurrentCell.ColumnIndex
            iFirstRow = oGrid.FirstDisplayedScrollingRowIndex()
        End If

        Refresca()

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


    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        'ShowFra()
        ShowInvoice()
    End Sub

    Private Sub DataGridView1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Enter Then
            'ShowFra()
            ShowInvoice()
            e.Handled = True
        End If
    End Sub

    Private Sub ShowFra()
        Dim oFra As Fra = CurrentFra()
        Dim oFrm As New Frm_Fra(oFra)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
    End Sub

    Private Sub ShowInvoice()
        Dim oInvoice As DTOInvoice = CurrentInvoice()
        Dim oFrm As New Frm_Invoice(oInvoice)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Show()
        End With
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
                    Case Contact.Cfps.Transferencia, Contact.Cfps.TransferenciaAnticipada
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
            Refresca()
        End If
    End Sub

    Private Sub ToolStripButtonRefresca_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButtonRefresca.Click
        Refresca()
    End Sub

End Class