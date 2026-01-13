Public Class Xl_BaseGuidCodNoms

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOBaseGuidCodNom)
    Private _DefaultValue As DTOBaseGuidCodNom
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOBaseGuidCodNom), Optional oDefaultValue As DTOBaseGuidCodNom = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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

    Public ReadOnly Property Values As List(Of DTOBaseGuidCodNom)
        Get
            Dim retval As New List(Of DTOBaseGuidCodNom)
            For Each oControlitem In _ControlItems
                retval.Add(oControlitem.Source)
            Next
            Return retval
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOBaseGuidCodNom) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOBaseGuidCodNom In oFilteredValues
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

    Private Function FilteredValues() As List(Of DTOBaseGuidCodNom)
        Dim retval As List(Of DTOBaseGuidCodNom)
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

    Public ReadOnly Property Value As DTOBaseGuidCodNom
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBaseGuidCodNom = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowBaseGuidCodNom.DefaultCellStyle.BackColor = Color.Transparent

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

    Private Function SelectedItems() As List(Of DTOBaseGuidCodNom)
        Dim retval As New List(Of DTOBaseGuidCodNom)
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
            Dim oSelectedValue = oControlItem.Source
            Select Case oSelectedValue.Cod
                Case DTOBaseGuidCodNom.Cods.Vehicle
                    Dim value As New DTOVehicle(oSelectedValue.Guid)
                    Dim oMenu As New Menu_Vehicle(value)
                    AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu.Range)
                Case DTOBaseGuidCodNom.Cods.ProductBrand
                    Dim value As New DTOProductBrand(oSelectedValue.Guid)
                    Dim oMenu As New Menu_ProductBrand(value)
                    AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu.Range)
                Case DTOBaseGuidCodNom.Cods.ProductCategory
                    Dim value As New DTOProductCategory(oSelectedValue.Guid)
                    Dim oMenu As New Menu_ProductCategory(value)
                    AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu.Range)
                Case DTOBaseGuidCodNom.Cods.ProductSku
                    Dim value As New DTOProductSku(oSelectedValue.Guid)
                    Dim oMenu As New Menu_ProductSku(value)
                    AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu.Range)
            End Select
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add("retirar", Nothing, AddressOf Do_Remove)
        End If
        oContextMenu.Items.Add("afegir producte", Nothing, AddressOf Do_AddNew)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Remove()
        Dim oControlItem As ControlItem = CurrentControlItem()
        _ControlItems.Remove(oControlItem)
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOBaseGuidCodNom = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Select Case oSelectedValue.Cod
                        Case DTOBaseGuidCodNom.Cods.Vehicle
                            Dim oVehicle As New DTOVehicle(oSelectedValue.Guid)
                            Dim oFrm As New Frm_Vehicle(oVehicle)
                            oFrm.Show()
                        Case DTOBaseGuidCodNom.Cods.ProductBrand
                            Dim oBrand As New DTOProductBrand(oSelectedValue.Guid)
                            Dim oFrm As New Frm_Tpa(oBrand)
                            oFrm.Show()
                        Case DTOBaseGuidCodNom.Cods.ProductCategory
                            Dim oCategory As New DTOProductCategory(oSelectedValue.Guid)
                            Dim oFrm As New Frm_Stp(oCategory, DTO.Defaults.SelectionModes.Browse)
                            oFrm.Show()
                        Case DTOBaseGuidCodNom.Cods.ProductSku
                            Dim oSku As New DTOProductSku(oSelectedValue.Guid)
                            Dim oFrm As New Frm_Art(oSku)
                            oFrm.Show()
                    End Select
                Case DTO.Defaults.SelectionModes.Selection
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
        Property Source As DTOBaseGuidCodNom

        Property Nom As String

        Public Sub New(value As DTOBaseGuidCodNom)
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
