Public Class Xl_QualityDistribution
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOQualityDistribution)
    Private _DefaultValue As DTOQualityDistribution
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event requestForExcel(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Ord
        Qty
        Nom
    End Enum

    Public Shadows Sub Load(values As List(Of DTOQualityDistribution), Optional oDefaultValue As DTOQualityDistribution = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOQualityDistribution) = FilteredValues()
        _ControlItems = New ControlItems
        Dim idx As Integer = 0
        For Each oItem As DTOQualityDistribution In oFilteredValues
            idx += 1
            Dim oControlItem As New ControlItem(oItem, idx)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOQualityDistribution)
        Dim retval As List(Of DTOQualityDistribution)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.Customer.FullNom.ToLower.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOQualityDistribution
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOQualityDistribution = oControlItem.Source
            Return retval
        End Get
    End Property
    Public ReadOnly Property Values As List(Of DTOQualityDistribution)
        Get
            Return _Values
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowQualityDistribution.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Ord)
            .HeaderText = "Ordre"
            .DataPropertyName = "Ord"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.ForeColor = Color.Gray
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Productes"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Client"
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

    Public Function SelectedItems() As List(Of DTOQualityDistribution)
        Dim retval As New List(Of DTOQualityDistribution)
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
            'Dim oMenu_QualityDistribution As New Menu_QualityDistribution(SelectedItems.First)
            'AddHandler oMenu_QualityDistribution.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_QualityDistribution.Range)
            'oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", My.Resources.Excel_16, AddressOf DO_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub
    Private Sub Do_Excel()
        RaiseEvent requestForExcel(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOQualityDistribution = CurrentControlItem.Source
            'Dim oFrm As New Frm_QualityDistribution(oSelectedValue)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub



    Protected Class ControlItem
        Property Source As DTOQualityDistribution

        Property Ord As Integer
        Property Qty As Integer
        Property Nom As String

        Public Sub New(value As DTOQualityDistribution, idx As Integer)
            MyBase.New()
            _Source = value
            With value
                _Ord = idx
                _Qty = .Skus.Count
                _Nom = .Customer.FullNom
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


