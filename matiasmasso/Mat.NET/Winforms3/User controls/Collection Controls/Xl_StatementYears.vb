Public Class Xl_StatementYears
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of Integer)
    Private _DefaultValue As Integer
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        year
    End Enum

    Public Shadows Sub Load(values As List(Of Integer), Optional oDefaultValue As Integer = 0, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        _Values = values
        _SelectionMode = oSelectionMode
        _DefaultValue = oDefaultValue

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems
        For Each oItem As Integer In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue > 0 Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Year = _DefaultValue)
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.year)
            End If
        End If

        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As Integer
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Integer = oControlItem.Source
            Return retval
        End Get
    End Property

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
            .HeaderText = Current.Session.Tradueix("Ejercicio", "Exercici", "Exercise")
            .DataPropertyName = "Year"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "#"
        End With
    End Sub

    Private Function SelectedControlItems() As ControlItems
        Dim retval As New ControlItems
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem)
        Return retval
    End Function

    Private Function SelectedItems() As List(Of Integer)
        Dim retval As New List(Of Integer)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            retval.Add(oControlItem.Source)
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

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
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Integer

        Property Year As Integer

        Public Sub New(value As Integer)
            MyBase.New()
            _Source = value
            With value
                _Year = _Source
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

