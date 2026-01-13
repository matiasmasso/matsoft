Public Class Xl_RepliqFras
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Fra
        Fch
        Base
        Comisio
        Client
    End Enum

    Public Shadows Sub Load(values As List(Of DTORepComLiquidable))
        _ControlItems = New ControlItems
        For Each oItem As DTORepComLiquidable In values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next
        LoadGrid()
    End Sub

    Public ReadOnly Property RepComsLiquidables As List(Of DTORepComLiquidable)
        Get
            Dim retval As New List(Of DTORepComLiquidable)
            For Each oControlItem As ControlItem In _ControlItems
                retval.Add(oControlItem.Source)
            Next
            Return retval
        End Get
    End Property

    Private Sub LoadGrid()
        With DataGridView1
            With .RowTemplate
                .Height = DataGridView1.Font.Height * 1.3
            End With

            .AutoGenerateColumns = False
            .Columns.Clear()

            .DataSource = _ControlItems
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .ColumnHeadersVisible = True
            .RowHeadersVisible = False
            .MultiSelect = True
            .AllowUserToResizeRows = False
            .AllowDrop = False
            .ReadOnly = True

            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fra)
                .HeaderText = "Factura"
                .DataPropertyName = "Fra"
                .Width = 60
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Fch)
                .HeaderText = "Data"
                .DataPropertyName = "Fch"
                .Width = 65
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Format = "dd/MM/yy"
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Base)
                .HeaderText = "Base"
                .DataPropertyName = "Base"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Comisio)
                .HeaderText = "Comisio"
                .DataPropertyName = "Comisio"
                .Width = 70
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
                .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            End With
            .Columns.Add(New DataGridViewTextBoxColumn)
            With .Columns(Cols.Client)
                .HeaderText = "Client"
                .DataPropertyName = "Client"
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        End With
        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function SelectedItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem)
        Return retval
    End Function

    Private Function SelectedRepComsLiquidables() As List(Of DTORepComLiquidable)
        Dim retval As New List(Of DTORepComLiquidable)
        For Each oRow As DataGridViewRow In DataGridView1.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentItem.Source)
        Return retval
    End Function

    Private Function CurrentItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = DataGridView1.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentItem()

        If oControlItem IsNot Nothing Then
            Dim oMenu_RepComLiquidable As New Menu_RepComLiquidable(SelectedRepComsLiquidables)
            oContextMenu.Items.AddRange(oMenu_RepComLiquidable.Range)
        End If

        oContextMenu.Items.Add(New ToolStripMenuItem("Retrocedir liquidació factura", My.Resources.aspa, AddressOf Do_RemoveFra))

        DataGridView1.ContextMenuStrip = oContextMenu
    End Sub

    Private Async Sub Do_RemoveFra()
        Dim oControlItem As ControlItem = CurrentItem()
        Dim oRepcomLiquidable As DTORepComLiquidable = oControlItem.Source
        oRepcomLiquidable.RepLiq = Nothing
        Dim exs As New List(Of Exception)
        If Await FEB2.RepComLiquidable.Update(oRepcomLiquidable, exs) Then
            _ControlItems.Remove(oControlItem)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.RepComsLiquidables))
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        Dim oRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
        Dim oItem As ControlItem = oRow.DataBoundItem
        'Select Case oItem.Updated
        '    Case True
        'oRow.DefaultCellStyle.BackColor = Color.LightGray
        '    Case Else
        'oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        'End Select
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub

    Public Sub Do_Excel()
        Dim oSheet = UIHelper.GetExcelFromDataGridView(DataGridView1)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Protected Class ControlItem
        Public Property Source As DTORepComLiquidable

        Public Property Checked As Boolean
        Public Property Fra As Integer
        Public Property Fch As Date
        Public Property Base As Decimal
        Public Property Comisio As Decimal
        Public Property Client As String
        Public Property Liquidable As Boolean

        Public Sub New(oRepComLiquidable As DTORepComLiquidable)
            MyBase.New()
            _Source = oRepComLiquidable
            With oRepComLiquidable
                _Checked = False
                _Fra = .Fra.Num
                _Fch = .Fra.fch
                _Base = .baseFras.eur
                _Comisio = .Comisio.Eur
                _Client = .Fra.Customer.FullNom
                _Liquidable = .Liquidable
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

