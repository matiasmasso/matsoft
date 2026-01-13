Public Class Xl_Cods
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOCod)
    Private _DefaultValue As DTOCod
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Id
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCod), Optional oDefaultValue As DTOCod = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
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
        Dim oFilteredValues As List(Of DTOCod) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOCod In oFilteredValues
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
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Nom)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOCod)
        Dim retval As List(Of DTOCod)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOCod
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCod = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowCod.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = False
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Id"
            .DataPropertyName = "Id"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 50
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
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

    Private Function SelectedItems() As List(Of DTOCod)
        Dim retval As New List(Of DTOCod)
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
            Dim oMenu_Cod As New Menu_Cod(SelectedItems.First)
            AddHandler oMenu_Cod.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Cod.Range)
            oContextMenu.Items.Add("-")
        End If

        If _ControlItems.Count > 0 Then
            Dim goUp = oContextMenu.Items.Add("pujar", Nothing, AddressOf Do_GoUp)
            goUp.Enabled = Not oControlItem.Equals(_ControlItems.First)
            Dim goDown = oContextMenu.Items.Add("baixar", Nothing, AddressOf Do_GoDown)
            goDown.Enabled = Not oControlItem.Equals(_ControlItems.Last)
        End If

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Async Sub Do_GoUp()
        Dim exs As New List(Of Exception)
        Dim idx = _ControlItems.IndexOf(CurrentControlItem())
        Dim tmp = _ControlItems(idx)
        _ControlItems(idx) = _ControlItems(idx - 1)
        _ControlItems(idx - 1) = tmp
        Dim oCods As New DTOCod.Collection
        oCods.AddRange(_ControlItems.Select(Function(x) x.Source))
        If Not Await FEB.Cods.Sort(exs, oCods) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_GoDown()
        Dim exs As New List(Of Exception)
        Dim idx = _ControlItems.IndexOf(CurrentControlItem())
        Dim tmp = _ControlItems(idx)
        _ControlItems(idx) = _ControlItems(idx + 1)
        _ControlItems(idx + 1) = tmp
        Dim oCods = _ControlItems.Select(Function(x) x.Source).ToList()
        If Not Await FEB.Cods.Sort(exs, oCods) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOCod = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.browse
                    Dim oFrm As New Frm_Cod(oSelectedValue)
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


    Protected Class ControlItem
        Property Source As DTOCod

        Property Id As Integer
        Property Nom As String

        Public Sub New(value As DTOCod)
            MyBase.New()
            _Source = value
            With value
                _Id = .Id
                _Nom = .Nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

