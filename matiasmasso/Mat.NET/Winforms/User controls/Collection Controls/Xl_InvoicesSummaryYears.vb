Public Class Xl_InvoicesSummaryYears
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOYearMonth)
    Private items As List(Of DTOYearMonth)

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        year
        Amt
    End Enum

    Public Shadows Sub Load(values As List(Of DTOYearMonth)) 'List(Of KeyValuePair(Of DTOYearMonth, DTOAmt)))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        Refresca()
    End Sub

    Public ReadOnly Property Year As Integer
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Integer = oControlItem.Source.year
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False


        Dim query = _Values.GroupBy(Function(g) New With {Key g.year}).
            Select(Function(group) New With {
            .year = group.Key.year,
            .Eur = group.Sum(Function(y) y.Eur)}).ToList()

            _ControlItems = New ControlItems
            For Each oItem In query
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            Next

            MyBase.DataSource = _ControlItems
            If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.Rows(0).Cells(Cols.year)
        End If


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
        With MyBase.Columns(Cols.year)
            .HeaderText = "Any"
            .DataPropertyName = "Year"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Eur"
            .Width = 90
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

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source.year))
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Object

        Property Year As Integer
        Property Eur As Decimal

        Public Sub New(value As Object)
            MyBase.New()
            _Source = value
            With value
                _Year = .Year
                _Eur = .eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class



