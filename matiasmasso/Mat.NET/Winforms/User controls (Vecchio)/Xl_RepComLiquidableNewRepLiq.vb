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

    Private Function CheckedRepComsLiquidables() As List(Of DTORepComLiquidable)
        Dim retval As New List(Of DTORepComLiquidable)
        For Each oControlItem As NewRepLiqControlItem In _ControlItems
            If oControlItem.Checked Then
                retval.Add(oControlItem.Source)
            End If
        Next
        Return retval
    End Function

    Public Async Function RepLiqs(exs As List(Of Exception)) As Task(Of List(Of DTORepLiq))
        Dim DtFch As Date = DateTimePicker1.Value

        'filtra les partides per les que estiguin chequejades i corresponguin a un rep chequejat
        Dim oCheckedReps As List(Of DTORep) = Xl_RepLiqNew_RepsSplit1.Reps
        Dim oRepComsLiquidables As New List(Of DTORepComLiquidable)
        For Each item In CheckedRepComsLiquidables()
            If oCheckedReps.Any(Function(x) x.Guid.Equals(item.Rep.Guid)) Then
                item.Rep = oCheckedReps.First(Function(x) x.Guid.Equals(item.Rep.Guid))
                oRepComsLiquidables.Add(item)
            End If
        Next
        'Dim oRepComsLiquidables = CheckedRepComsLiquidables().Where(Function(x) oCheckedReps.Any(Function(y) y.Equals(x.Rep))).ToList

        'genera les liquidacions a partir de les partides seleccionades
        Dim retval As List(Of DTORepLiq) = Await FEB2.Repliqs.Factory(oRepComsLiquidables, DtFch, Current.Session.User, exs)
        Return retval
    End Function




    Private Sub ButtonStart_Click(sender As Object, e As EventArgs) Handles ButtonStart.Click
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim exs As New List(Of Exception)
        _RepComsLiquidables = Await FEB2.RepComLiquidables.PendentsDeLiquidar(exs)
        If exs.Count = 0 Then
            _ControlItems = New NewRepLiqControlItems
            _CancelRequest = False

            PanelBottomBar.Visible = True
            ButtonStart.Enabled = False

            Await LoadPendents()
            DataGridView1.DataSource = _ControlItems
            SetContextMenu()

            Xl_RepLiqNew_RepsSplit1.NewRepLiqControlItems = _ControlItems
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function LoadPendents() As Task
        PanelBottomBar.Visible = True
        Application.DoEvents()
        With ProgressBar1
            .Value = 0
            .Maximum = _RepComsLiquidables.Count
        End With

        Dim exs As New List(Of Exception)

        _ControlItems = New NewRepLiqControlItems
        For Each oRepComLiquidable As DTORepComLiquidable In _RepComsLiquidables
            ProgressBar1.Increment(1)
            Application.DoEvents()

            If oRepComLiquidable.Comisio.Eur > 0 Then
                Dim DtFchRepLiq As Date = DateTimePicker1.Value
                Dim oTuple As Tuple(Of NewRepLiqControlItem.Codis, Boolean) = Await Liquidable(exs, oRepComLiquidable.Fra, DtFchRepLiq)
                Dim oCodi As NewRepLiqControlItem.Codis = oTuple.Item1
                Dim BlLiquidable As Boolean = oTuple.Item2

                Dim oItem As New NewRepLiqControlItem(oRepComLiquidable, BlLiquidable, oCodi)
                _ControlItems.Add(oItem)

                LabelCount.Text = String.Format("{0}/{1} {2}%", _ControlItems.Count, _RepComsLiquidables.Count, Math.Round(_ControlItems.Count / _RepComsLiquidables.Count * 100, 0, MidpointRounding.AwayFromZero))
                If _CancelRequest Then Exit For
            End If
        Next

        If exs.Count > 0 Then UIHelper.WarnError(exs)
        PanelBottomBar.Visible = False
    End Function


    Private Async Function Liquidable(exs As List(Of Exception), ByVal oFra As DTOInvoice, ByVal DtLiq As Date) As Task(Of Tuple(Of NewRepLiqControlItem.Codis, Boolean))
        Dim retval As Boolean
        Dim oCodi As NewRepLiqControlItem.Codis
        If FEB2.Contact.IsImpagatSync(oFra.Customer) Then
            oCodi = NewRepLiqControlItem.Codis.Impagat
        ElseIf FEB2.Customer.IsInsolvent(oFra.Customer) Then
            oCodi = NewRepLiqControlItem.Codis.Insolvent
        Else
            Dim oCfp As DTOPaymentTerms.CodsFormaDePago = oFra.Cfp
            Select Case oCfp
                Case DTOPaymentTerms.CodsFormaDePago.Comptat
                    retval = True
                    Dim oAlbs = Await FEB2.Deliveries.All(exs, oFra)
                    If exs.Count = 0 Then
                        For Each oAlb As DTODelivery In oAlbs
                            Select Case oAlb.CashCod
                                Case DTOCustomer.CashCodes.Reembols
                                    If oAlb.FchCobroReembolso = Date.MinValue Then
                                        retval = False
                                        oCodi = NewRepLiqControlItem.Codis.CashPending
                                        Exit For
                                    End If
                                Case DTOCustomer.CashCodes.TransferenciaPrevia, DTOCustomer.CashCodes.Visa
                                    If oAlb.PortsCod = DTOCustomer.PortsCodes.Altres Then
                                        retval = False
                                        oCodi = NewRepLiqControlItem.Codis.CashPending
                                        Exit For
                                    End If
                                Case DTOCustomer.CashCodes.credit
                            End Select
                        Next oAlb

                        If retval Then oCodi = NewRepLiqControlItem.Codis.CashOk
                    Else
                        UIHelper.WarnError(exs)
                    End If

                Case DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria, DTOPaymentTerms.CodsFormaDePago.Xerocopia
                    If oFra.ExistPnds Then
                        retval = False
                    Else
                        Dim DaysFromVto As Integer = DtLiq.Subtract(oFra.Vto).Days
                        retval = (DaysFromVto > MINDAYSFROMVTO)
                    End If
                    oCodi = IIf(retval, NewRepLiqControlItem.Codis.BancOk, NewRepLiqControlItem.Codis.BancPending)

                Case DTOPaymentTerms.CodsFormaDePago.aNegociar
                    oCodi = NewRepLiqControlItem.Codis.NoBancPnd
                Case Else

                    retval = Not oFra.ExistPnds
                    oCodi = IIf(retval, NewRepLiqControlItem.Codis.NoBancNoPnd, NewRepLiqControlItem.Codis.NoBancPnd)
            End Select
        End If


        Return New Tuple(Of NewRepLiqControlItem.Codis, Boolean)(oCodi, retval)

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
        If oControlItem IsNot Nothing Then

            Dim oRepComLiquidable As DTORepComLiquidable = oControlItem.Source

            If oControlItem IsNot Nothing Then
                Dim oMenu_RepComLiquidable As New Menu_RepComLiquidable(SelectedRepComsLiquidables)
                oContextMenu.Items.AddRange(oMenu_RepComLiquidable.Range)

                Dim oMenuItem As New ToolStripMenuItem("No Liquidable", Nothing, AddressOf Descarta)
                oMenuItem.Checked = Not oRepComLiquidable.Liquidable
                oMenuItem.CheckOnClick = True
                oMenuItem.Tag = oRepComLiquidable
                AddHandler oMenuItem.Click, AddressOf onDescartaCheck
                oContextMenu.Items.Add(oMenuItem)
            End If
        End If


        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub onDescartaCheck(sender As Object, e As EventArgs)
        Dim oMenuitem As ToolStripMenuItem = sender
        Dim exs As New List(Of Exception)
        Dim oRepComLiquidable As DTORepComLiquidable = oMenuitem.Tag
        oRepComLiquidable.Liquidable = Not oMenuitem.Checked
        If Not Await FEB2.RepComLiquidable.Descarta(oRepComLiquidable, exs) Then
            UIHelper.WarnError(exs, "No s'ha pogut descartar")
        End If

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
