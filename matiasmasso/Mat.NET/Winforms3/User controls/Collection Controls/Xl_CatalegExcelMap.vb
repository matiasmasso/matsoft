Public Class Xl_CatalegExcelMap
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductSkuExcelMap)

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Col
        Field
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductSkuExcelMap))
        _Values = values
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Public ReadOnly Property values As List(Of DTOProductSkuExcelMap)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOProductSkuExcelMap) = _Values
        _ControlItems = New ControlItems
        For Each oItem As DTOProductSkuExcelMap In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOProductSkuExcelMap
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductSkuExcelMap = oControlItem.Source
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
        With MyBase.Columns(Cols.Col)
            .HeaderText = "Columna"
            .DataPropertyName = "ColHeader"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Field)
            .HeaderText = "Camp"
            .DataPropertyName = "Field"
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

    Private Function SelectedItems() As List(Of DTOProductSkuExcelMap)
        Dim retval As New List(Of DTOProductSkuExcelMap)
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


    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub
    Private Sub Do_Remove()
        _ControlItems.Remove(CurrentControlItem)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            setcontextmenu
        End If
    End Sub

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing Then
            'Dim oMenu_Template As New Menu_Template(SelectedItems.First)
            'AddHandler oMenu_Template.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_Template.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("retirar", Nothing, AddressOf Do_Remove)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Protected Class ControlItem
        Property Source As DTOProductSkuExcelMap

        Property ColHeader As String
        Property Field As String

        Public Sub New(value As DTOProductSkuExcelMap)
            MyBase.New()
            _Source = value
            With value
                _ColHeader = .colHeader
                _Field = .skuField.ToString.Replace("_", " ")
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

