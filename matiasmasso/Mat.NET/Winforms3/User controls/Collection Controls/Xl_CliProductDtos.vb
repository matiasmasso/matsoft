Public Class Xl_CliProductDtos

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOCliProductDto)
    Private _DefaultValue As DTOCliProductDto
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToRemove(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Dto
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCliProductDto), Optional oDefaultValue As DTOCliProductDto = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Public ReadOnly Property Values As List(Of DTOCliProductDto)
        Get
            Dim retval As New List(Of DTOCliProductDto)
            For Each oControlItem As ControlItem In _ControlItems
                Dim item As DTOCliProductDto = oControlItem.Source
                item.Dto = oControlItem.Dto
                retval.Add(item)
            Next
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOCliProductDto) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOCliProductDto In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        MyBase.Sort(MyBase.Columns(Cols.Nom), System.ComponentModel.ListSortDirection.Ascending)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOCliProductDto)
        Dim retval As List(Of DTOCliProductDto) = _Values
        Return retval
    End Function


    Public ReadOnly Property Value As DTOCliProductDto
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCliProductDto = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowCliProductDto.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
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

    Private Function SelectedItems() As List(Of DTOCliProductDto)
        Dim retval As New List(Of DTOCliProductDto)
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

            Dim oMenu As New Menu_Product(SelectedItems.First.Product)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("retirar", Nothing, AddressOf Do_Remove)
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Remove()
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oDto As DTOCliProductDto = oControlItem.Source
        RaiseEvent RequestToRemove(Me, New MatEventArgs(oDto))
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOCliProductDto = CurrentControlItem.Source
            'Select Case _SelectionMode
            ' Case DTO.Defaults.SelectionModes.Browse
            ' Dim oFrm As New Frm_CliProductDto(oSelectedValue)
            ' AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            ' oFrm.Show()
            ' Case DTO.Defaults.SelectionModes.Selection
            ' RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
            ' End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_CliProductDtos_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles Me.CellValueChanged
        If _AllowEvents Then
            RaiseEvent afterupdate(Me, MatEventArgs.Empty)
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOCliProductDto

        Property Nom As String
        Property Dto As Decimal

        Public Sub New(value As DTOCliProductDto)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Product.FullNom()
                _Dto = .Dto
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


