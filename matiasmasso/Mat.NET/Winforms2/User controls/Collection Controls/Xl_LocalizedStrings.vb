Public Class Xl_LocalizedStrings

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOLocalizedString)
    Private _locales As List(Of String)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        key
        value
    End Enum

    Public Shadows Sub Load(values As List(Of DTOLocalizedString), locales As List(Of String))
        _Values = values
        _locales = locales

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOLocalizedString) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOLocalizedString In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOLocalizedString)
        Dim retval As List(Of DTOLocalizedString)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.key.ToLower.Contains(_Filter.ToLower))
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Public ReadOnly Property Value As DTOLocalizedString
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOLocalizedString = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowLocalizedString.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.key)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        For Each locale In _locales
            With MyBase.Columns(Cols.key)
                .HeaderText = locale
                .DataPropertyName = locale
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With
        Next
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

    Private Function SelectedItems() As List(Of DTOLocalizedString)
        Dim retval As New List(Of DTOLocalizedString)
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

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            oContextMenu.Items.Add("zoom", Nothing, AddressOf Do_Zoom)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub do_Zoom()
        Dim CellIdx = MyBase.CurrentCell.ColumnIndex
        If CellIdx <> Cols.key Then
            Dim oControlItem = CurrentControlItem()
            Dim oLocalizedString As DTOLocalizedString = oControlItem.Source
            Dim locale = MyBase.Columns(MyBase.CurrentCell.ColumnIndex).HeaderText
            Dim item = oLocalizedString.items.FirstOrDefault(Function(x) x.locale = locale)
            Dim oFrm As New Frm_LocalizedString(oLocalizedString, locale)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.key
            Case Else
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oLocalizedString As DTOLocalizedString = oControlItem.Source
                Dim locale = MyBase.Columns(e.ColumnIndex).HeaderText
                Dim item = oLocalizedString.items.FirstOrDefault(Function(x) x.locale = locale)
                If item Is Nothing Then
                    e.Value = ""
                Else
                    e.Value = item.value
                End If
        End Select
    End Sub

    Private Sub Xl_LocalizedStrings_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellDoubleClick
        Select Case e.ColumnIndex
            Case Cols.key
            Case Else
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oLocalizedString As DTOLocalizedString = oControlItem.Source
                Dim locale = MyBase.Columns(e.ColumnIndex).HeaderText
                Dim item = oLocalizedString.items.FirstOrDefault(Function(x) x.locale = locale)
                Dim oFrm As New Frm_LocalizedString(oLocalizedString, locale)
                AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                oFrm.Show()
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOLocalizedString

        Property Key As String

        Public Sub New(value As DTOLocalizedString)
            MyBase.New()
            _Source = value
            With value
                _Key = .key
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

