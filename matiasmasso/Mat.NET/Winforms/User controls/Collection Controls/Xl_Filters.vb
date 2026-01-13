Public Class Xl_Filters
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOFilter)
    Private _DefaultValue As DTOFilter
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Esp
        Cat
        Eng
        Por
    End Enum

    Public Shadows Sub Load(values As List(Of DTOFilter), Optional oDefaultValue As DTOFilter = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
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
        Dim oFilteredValues As List(Of DTOFilter) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOFilter In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Esp)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOFilter)
        Dim retval As List(Of DTOFilter)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.langText.Esp.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOFilter
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOFilter = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowFilter.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Esp)
            .HeaderText = "Espanyol"
            .DataPropertyName = "Esp"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cat)
            .HeaderText = "Català"
            .DataPropertyName = "Cat"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eng)
            .HeaderText = "Anglès"
            .DataPropertyName = "Eng"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Por)
            .HeaderText = "Portuguès"
            .DataPropertyName = "Por"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOFilter)
        Dim retval As New List(Of DTOFilter)
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
            Dim oMenu_Filter As New Menu_Filter(SelectedItems.First)
            AddHandler oMenu_Filter.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Filter.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim iRowIdx As Integer = MyBase.CurrentRow.Index

        Dim oMenuUp = New ToolStripMenuItem("pujar", Nothing, AddressOf do_Up)
        oMenuUp.Enabled = (iRowIdx > 0)
        oContextMenu.Items.Add(oMenuUp)

        Dim oMenuDown = New ToolStripMenuItem("baixar", Nothing, AddressOf do_Down)
        oMenuDown.Enabled = (iRowIdx < _ControlItems.Count - 1)
        oContextMenu.Items.Add(oMenuDown)

        oContextMenu.Items.Add("-")
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOFilter = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.browse
                    Dim oFrm As New Frm_Filter(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.selection
                    RaiseEvent OnItemSelected(Me, New MatEventArgs(Me.Value))
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Async Sub do_Up()
        Dim i = MyBase.CurrentRow.Index
        Await switch(_ControlItems(i).Source, _ControlItems(i - 1).Source)
    End Sub

    Private Async Sub do_Down()
        Dim i = MyBase.CurrentRow.Index
        Await switch(_ControlItems(i).Source, _ControlItems(i + 1).Source)
    End Sub

    Private Async Function switch(oFilter1 As DTOFilter, oFilter2 As DTOFilter) As Task
        Dim exs As New List(Of Exception)
        Dim idx1 As Integer = oFilter1.ord
        Dim idx2 As Integer = oFilter2.ord
        oFilter1.ord = idx2
        oFilter2.ord = idx1
        Dim oFilters = {oFilter1, oFilter2}.ToList
        MyBase.ToggleProgressBarRequest(Me, New MatEventArgs(True))
        If Await FEB2.Filters.Update(exs, oFilters) Then
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function



    Protected Class ControlItem
        Property Source As DTOFilter

        Property Esp As String
        Property Cat As String
        Property Eng As String
        Property Por As String

        Public Sub New(value As DTOFilter)
            MyBase.New()
            _Source = value
            With value.langText
                _Esp = .Esp
                _Cat = .Cat
                _Eng = .Eng
                _Por = .Por
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


