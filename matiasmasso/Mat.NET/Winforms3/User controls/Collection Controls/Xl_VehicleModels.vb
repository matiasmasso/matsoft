Public Class Xl_VehicleModels
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOVehicle.ModelClass)
    Private _DefaultValue As DTOVehicle.ModelClass
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOVehicle.ModelClass), Optional oDefaultValue As DTOVehicle.ModelClass = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _SelectionMode = oSelectionMode
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOVehicle.ModelClass) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOVehicle.ModelClass In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOVehicle.ModelClass)
        Dim retval As List(Of DTOVehicle.ModelClass)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Nom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOVehicle.ModelClass
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOVehicle.ModelClass = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowVehicleModel.DefaultCellStyle.BackColor = Color.Transparent

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

    Private Function SelectedItems() As List(Of DTOVehicle.ModelClass)
        Dim retval As New List(Of DTOVehicle.ModelClass)
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
            Dim oMenu_VehicleModel As New Menu_VehicleModel(SelectedItems.First)
            AddHandler oMenu_VehicleModel.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_VehicleModel.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOVehicle.ModelClass = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_VehicleModel(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
                Case DTO.Defaults.SelectionModes.Selection
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
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
        Property Source As DTOVehicle.ModelClass

        Property Nom As String

        Public Sub New(value As DTOVehicle.ModelClass)
            MyBase.New()
            _Source = value
            With value
                _Nom = .Nom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

