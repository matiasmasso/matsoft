Public Class Xl_InvoicesSummaryMonths
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOYearMonth)
    Private items As List(Of DTOYearMonth)

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event RequestToGenerateIntrastat(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Month
        Amt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOYearMonth))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Public ReadOnly Property Month As Integer
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Integer = oControlItem.Source.Month
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False

        Dim iFirstNonEmptyRow As Integer = 11

        _ControlItems = New ControlItems
        For Each oItem In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
            If oItem.Eur <> 0 And iFirstNonEmptyRow = 11 Then
                iFirstNonEmptyRow = _Values.IndexOf(oItem)
            End If
        Next

        'Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        'UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        MyBase.CurrentCell = MyBase.Rows(iFirstNonEmptyRow).Cells(Cols.Month)

        SetContextMenu()
        _AllowEvents = True
    End Sub


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Month)
            .HeaderText = "Mes"
            .DataPropertyName = "Month"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
    End Sub

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            Dim oMenuItem As New ToolStripMenuItem("Nou Intrastat", Nothing, AddressOf Do_AddIntrastat)
            oContextMenu.Items.Add(oMenuItem)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddIntrastat()
        Dim oControlItem = CurrentControlItem()
        RaiseEvent RequestToGenerateIntrastat(Me, New MatEventArgs(oControlItem.Month))
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents And CurrentControlItem() IsNot Nothing Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source.Month))
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOYearMonth

        Property Month As String
        Property Eur As Decimal

        Public Sub New(value As DTOYearMonth)
            MyBase.New()
            _Source = value
            With value
                _Month = Current.Session.Lang.Mes(.Month)
                _Eur = .Eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class



