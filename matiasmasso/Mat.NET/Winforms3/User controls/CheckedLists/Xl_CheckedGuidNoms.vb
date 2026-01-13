Public Class Xl_CheckedGuidNoms
    Inherits _Xl_ReadOnlyDatagridview
    Private _values As List(Of DTOGuidNom)
    Private _checkedValues As List(Of DTOGuidNom)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean
    Private _Caption As String

    Public Event CheckedChanged(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Chk
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOGuidNom), Optional checkedValues As List(Of DTOGuidNom) = Nothing)
        _values = values
        _checkedValues = checkedValues
        If _checkedValues Is Nothing Then _checkedValues = New List(Of DTOGuidNom)

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
        For Each value In _values
            Dim isChecked = _checkedValues.Contains(value)
            Dim oControlitem As New ControlItem(value, isChecked)
            _ControlItems.Add(oControlitem)
        Next

        MyBase.DataSource = _ControlItems
        MyBase.ClearSelection()

        _AllowEvents = True
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.35
        'MyBase.RowRol.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.RowHeadersVisible = _Caption > ""
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewCheckBoxColumn(False))
        With DirectCast(MyBase.Columns(Cols.Chk), DataGridViewCheckBoxColumn)
            .DataPropertyName = "Checked"
            .Width = 20
            .DefaultCellStyle.SelectionBackColor = Color.White
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = _Caption
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
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


    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Chk
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
                Dim oControlItem = CurrentControlItem()
                Dim item = CurrentControlItem.Source
                RaiseEvent CheckedChanged(Me, New MatEventArgs(item))
        End Select
    End Sub


    Public Function CheckedValues() As List(Of DTOGuidNom)
        Dim retval = _ControlItems.
            ToList.
            Where(Function(x) x.Checked).
            Select(Function(y) y.Source).
            ToList()
        Return retval
    End Function

    Private Sub Xl_CheckedGuidNoms_SelectionChanged(sender As Object, e As EventArgs) Handles Me.SelectionChanged
        Dim controlItem = CurrentControlItem()
        RaiseEvent ValueChanged(Me, New MatEventArgs(controlItem.Source))
    End Sub

    Public Function Csv() As DTOCsv
        Dim retval As New DTOCsv("fitxer de leads", "Leads.csv")
        For Each item As DTOGuidNom In Me.CheckedValues
            Dim oRow = retval.AddRow()
            oRow.AddCell(item.Guid.ToString)
            oRow.AddCell(item.Nom)
        Next
        Return retval
    End Function

    Public Function Excel() As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("fitxer de leads", "Leads.xlsx")
        For Each item As DTOGuidNom In Me.CheckedValues
            Dim oRow = retval.AddRow()
            oRow.AddCell(item.Guid.ToString)
            oRow.AddCell(item.Nom)
        Next
        Return retval
    End Function

    Protected Class ControlItem
        Property Source As DTOGuidNom
        Property Checked As Boolean
        Property Nom As String

        Public Sub New(value As DTOGuidNom, checked As Boolean)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
                _Checked = checked
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

