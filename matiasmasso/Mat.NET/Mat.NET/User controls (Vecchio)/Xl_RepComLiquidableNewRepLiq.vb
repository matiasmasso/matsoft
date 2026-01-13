Public Class Xl_RepComLiquidableNewRepLiq
    Private _RepComsLiquidables As List(Of DTORepComLiquidable)
    Const MINDAYSFROMVTO As Integer = 20

    Private _ControlItems As NewRepLiqControlItems
    Private _CancelRequest As Boolean
    Private _ShowNoLiquidables As Boolean
    Private _AllowEvents As Boolean

    Private Enum Cols
        Check
        RepAbr
        Fra
        Fch
        Base
        Comisio
        Client
        Obs
    End Enum

    Private Sub Xl_RepComLiquidableNewRepLiq_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim DtFch As Date = LastFinDeMes()
        DateTimePicker1.Value = DtFch
        SetProperties()
    End Sub

    Public ReadOnly Property RepLiqs(exs As List(Of Exception)) As List(Of DTORepLiq)
        Get
            Dim DtFch As Date = DateTimePicker1.Value

            'filtra les partides per les que estiguin chequejades i corresponguin a un rep chequejat
            Dim oReps As List(Of DTORep) = Xl_RepLiqNew_RepsSplit1.Reps
            Dim oRepComsLiquidables As New List(Of DTORepComLiquidable)
            For Each oControlItem As NewRepLiqControlItem In _ControlItems
                If oControlItem.Checked Then
                    Dim oRepComLiquidable As DTORepComLiquidable = oControlItem.Source
                    For Each oRep As DTORep In oReps
                        If oRep.Equals(oRepComLiquidable.Rep) Then
                            oRepComsLiquidables.Add(oControlItem.Source)
                        End If
                    Next
                End If
            Next

            'genera les liquidacions a partir de les partides seleccionades
            Dim retval As List(Of DTORepLiq) = BLL_RepLiqs.NewfromRepComsLiquidables(oRepComsLiquidables, DtFch, exs)
            Return retval
        End Get
    End Property



    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        refresca()
    End Sub

    Private Sub refresca()
        _RepComsLiquidables = BLL.BLLRepComLiquidables.Pendents()
        _ControlItems = New NewRepLiqControlItems
        _CancelRequest = False
        PanelBottomBar.Visible = True
        ButtonStart.Enabled = False

        LoadPendents()
        DataGridView1.DataSource = _ControlItems
        SetContextMenu()

        Xl_RepLiqNew_RepsSplit1.NewRepLiqControlItems = _ControlItems
        _AllowEvents = True
    End Sub

    Private Sub LoadPendents()
        PanelBottomBar.Visible = True
        Application.DoEvents()
        With ProgressBar1
            .Value = 0
            .Maximum = _RepComsLiquidables.Count
        End With

        _ControlItems = New NewRepLiqControlItems
        For Each oRepComLiquidable As DTORepComLiquidable In _RepComsLiquidables
            ProgressBar1.Increment(1)
            Application.DoEvents()

            If oRepComLiquidable.Comisio.Eur > 0 Then
                Dim oCodi As NewRepLiqControlItem.Codis
                Dim DtFchRepLiq As Date = DateTimePicker1.Value
                Dim BlLiquidable As Boolean = Liquidable(oRepComLiquidable.Fra, DtFchRepLiq, oCodi)

                Dim oItem As New NewRepLiqControlItem(oRepComLiquidable, BlLiquidable, oCodi)
                _ControlItems.Add(oItem)

                LabelCount.Text = String.Format("{0}/{1} {2}%", _ControlItems.Count, _RepComsLiquidables.Count, Math.Round(_ControlItems.Count / _RepComsLiquidables.Count * 100, 0))
                If _CancelRequest Then Exit For
            End If
        Next

        PanelBottomBar.Visible = False
    End Sub


    Private Function Liquidable(ByVal oFra As DTOInvoice, ByVal DtLiq As Date, ByRef oCodi As NewRepLiqControlItem.Codis) As Boolean
        Dim retval As Boolean
        If BLL.BLLCustomer.IsImpagat(oFra.Customer) Then
            oCodi = NewRepLiqControlItem.Codis.Impagat
        ElseIf BLL.BLLCustomer.IsInsolvent(oFra.Customer) Then
            oCodi = NewRepLiqControlItem.Codis.Insolvent
        Else
            Dim oCfp As DTOCustomer.FormasDePagament = oFra.Cfp
            Select Case oCfp
                Case DTOCustomer.FormasDePagament.Comptat
                    retval = True
                    For Each oAlb As DTODelivery In BLL.BLLDeliveries.all(oFra)
                        Select Case oAlb.CashCod
                            Case DTO.DTOCustomer.CashCodes.Reembols
                                If oAlb.FchCobroReembolso = Date.MinValue Then
                                    retval = False
                                    oCodi = NewRepLiqControlItem.Codis.CashPending
                                    Exit For
                                End If
                            Case DTO.DTOCustomer.CashCodes.TransferenciaPrevia, DTO.DTOCustomer.CashCodes.Visa
                                If oAlb.PortsCod = DTO.DTOCustomer.PortsCodes.Altres Then
                                    retval = False
                                    oCodi = NewRepLiqControlItem.Codis.CashPending
                                    Exit For
                                End If
                            Case DTO.DTOCustomer.CashCodes.credit
                        End Select
                    Next oAlb
                    If retval Then oCodi = NewRepLiqControlItem.Codis.CashOk

                Case DTOCustomer.FormasDePagament.DomiciliacioBancaria, DTOCustomer.FormasDePagament.Xerocopia
                    If oFra.ExistPnds Then
                        retval = False
                    Else
                        Dim DaysFromVto As Integer = DtLiq.Subtract(oFra.Vto).Days
                        retval = (DaysFromVto > MINDAYSFROMVTO)
                    End If
                    oCodi = IIf(retval, NewRepLiqControlItem.Codis.BancOk, NewRepLiqControlItem.Codis.BancPending)

                Case DTOCustomer.FormasDePagament.aNegociar
                    oCodi = NewRepLiqControlItem.Codis.NoBancPnd
                Case Else
                    Liquidable = Not oFra.ExistPnds
                    oCodi = IIf(retval, NewRepLiqControlItem.Codis.NoBancNoPnd, NewRepLiqControlItem.Codis.NoBancPnd)
            End Select
        End If

        Return retval

    End Function

    Private Sub SetProperties()
        With DataGridView1
            With .RowTemplate
                '.Height = DataGridView1.Font.Height * 1.35
                .DefaultCellStyle.BackColor = Color.Transparent
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()
            .Columns.Add(New DataGridViewCheckBoxColumn)
            For i As Integer = Cols.RepAbr To Cols.Obs
                .Columns.Add(New DataGridViewTextBoxColumn)
            Next

            '.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ReadOnly = True
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False

            With .Columns(Cols.Check)
                .HeaderText = ""
                .DataPropertyName = "Checked"
                .Width = 20
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.RepAbr)
                .ReadOnly = True
                .HeaderText = "Rep"
                .DataPropertyName = "RepAbr"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            End With
            With .Columns(Cols.Fra)
                .ReadOnly = True
                .HeaderText = "Factura"
                .DataPropertyName = "Fra"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            With .Columns(Cols.Fch)
                .ReadOnly = True
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            With .Columns(Cols.Base)
                .ReadOnly = True
                .HeaderText = "Base"
                .DataPropertyName = "Base"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Comisio)
                .ReadOnly = True
                .HeaderText = "Comisio"
                .DataPropertyName = "Comisio"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            With .Columns(Cols.Client)
                .ReadOnly = True
                .HeaderText = "Client"
                .DataPropertyName = "Client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
            With .Columns(Cols.Obs)
                .ReadOnly = True
                .HeaderText = "Observacions"
                .DataPropertyName = "Obs"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
    End Sub


    Private Function SelectedItems() As NewRepLiqControlItems
        Dim retval As New NewRepLiqControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As NewRepLiqControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem)
        Return retval
    End Function

    Private Function SelectedRepComsLiquidables() As List(Of DTORepComLiquidable)
        Dim retval As New List(Of DTORepComLiquidable)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As NewRepLiqControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem.Source)
        Return retval
    End Function

    Private Function CurrentItem() As NewRepLiqControlItem
        Dim retval As NewRepLiqControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As NewRepLiqControlItem = CurrentItem()
        Dim oRepComLiquidable As DTORepComLiquidable = oControlItem.Source

        If oControlItem IsNot Nothing Then
            Dim oMenu_RepComLiquidable As New Menu_RepComLiquidable(SelectedRepComsLiquidables)
            oContextMenu.Items.AddRange(oMenu_RepComLiquidable.Range)
        End If

        Dim oMenuItem As New ToolStripMenuItem("No Liquidable", Nothing, AddressOf Descarta)
        oMenuItem.Checked = Not oRepComLiquidable.Liquidable
        oMenuItem.CheckOnClick = True
        oContextMenu.Items.Add(oMenuItem)

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Protected Sub Descarta(sender As Object, e As EventArgs)
        Dim oMenuItem As ToolStripMenuItem = sender

        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        Dim oControlItem As NewRepLiqControlItem = oRow.DataBoundItem
        oControlItem.Checked = False
        oControlItem.Descarta = oMenuItem.Checked
        DataGridView1.Refresh()

        Xl_RepLiqNew_RepsSplit1.NewRepLiqControlItems = _ControlItems
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As NewRepLiqControlItem = oRow.DataBoundItem
        If oItem.Descarta Then
            PaintGradientRowBackGround(e, Color.DarkGray)
        Else
            Select Case oItem.Codi
                Case NewRepLiqControlItem.Codis.Impagat
                    PaintGradientRowBackGround(e, Color.LightSalmon)
                Case NewRepLiqControlItem.Codis.Insolvent
                    PaintGradientRowBackGround(e, Color.FromArgb(255, 153, 51))
                Case NewRepLiqControlItem.Codis.CashOk
                    PaintGradientRowBackGround(e, Color.LightBlue)
                Case NewRepLiqControlItem.Codis.CashPending
                    PaintGradientRowBackGround(e, Color.SkyBlue)
                Case NewRepLiqControlItem.Codis.BancOk
                    PaintGradientRowBackGround(e, Color.LightGreen)
                Case NewRepLiqControlItem.Codis.BancPending
                    PaintGradientRowBackGround(e, Color.White)
                Case NewRepLiqControlItem.Codis.NoBancPnd
                    PaintGradientRowBackGround(e, Color.LightGray)
                Case NewRepLiqControlItem.Codis.NoBancNoPnd
                    PaintGradientRowBackGround(e, Color.HotPink)
                Case Else
                    oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
            End Select
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
        Try
            e.Graphics.FillRectangle(backbrush, rowBounds)
        Finally
            backbrush.Dispose()
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.ColumnIndex = Cols.Check Then
            Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim oControlItem As NewRepLiqControlItem = oRow.DataBoundItem
            oControlItem.Checked = Not oControlItem.Checked
            Xl_RepLiqNew_RepsSplit1.NewRepLiqControlItems = _ControlItems
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs)
        _CancelRequest = True
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Sub ButtonResetRepComsLiquidables_Click(sender As Object, e As EventArgs)
        Me.Cursor = Cursors.WaitCursor
        Dim exs as New List(Of exception)
        If RepComsLiquidables.ResetRepComNoLiquidadas( exs) Then
            Me.Cursor = Cursors.Default
            MsgBox("partides pendents de liquidar a representants regenerades", MsgBoxStyle.Information)
        Else
            Me.Cursor = Cursors.Default
            MsgBox("error al regenerar partides pendents de liquidar a representants:" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
        End If
    End Sub


    Private Sub ButtonCancel_Click_1(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        _CancelRequest = True
    End Sub

    Private Function LastFinDeMes() As Date
        Dim Fch As Date = Today
        Dim FirstDayCurrentMonth As Date = Fch.AddDays(-Fch.Day + 1)
        If Fch.Day > 20 Then
            Fch = FirstDayCurrentMonth.AddMonths(1).AddDays(-1)
        Else
            Fch = FirstDayCurrentMonth.AddDays(-1)
        End If
        Return Fch
    End Function


    Private Sub Xl_RepLiqNew_RepsSplit1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_RepLiqNew_RepsSplit1.AfterUpdate
        Dim oReps As List(Of DTORep) = Xl_RepLiqNew_RepsSplit1.Reps
        DataGridView1.CurrentCell = Nothing

        For Each oRow As DataGridViewRow In DataGridView1.Rows
            Dim oControlItem As NewRepLiqControlItem = oRow.DataBoundItem
            Dim oRepComLiquidable As DTORepComLiquidable = oControlItem.Source
            oRow.Visible = oReps.Exists(Function(x) x.Guid.Equals(oRepComLiquidable.Rep.Guid))
        Next

    End Sub

    Private Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        DataGridView1.DataSource = Nothing
        ButtonStart.Enabled = True
    End Sub
End Class
