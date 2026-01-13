Public Class Xl_ContactPurchaseOrderItems
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrderItem)
    Private _DefaultValue As DTOPurchaseOrderItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _FilterProduct As DTOProduct
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Pdc
        Fch
        Qty
        Sku
        Pn2
        Preu
        Dto
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrderItem), Optional oDefaultValue As DTOPurchaseOrderItem = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
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
        Dim oFilteredValues As List(Of DTOPurchaseOrderItem) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOPurchaseOrderItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        If _FilterProduct Is Nothing Then
            retval = _Values
        Else
            If TypeOf _FilterProduct Is DTOProductBrand Then
                retval = _Values.FindAll(Function(x) x.Sku.Category.Brand.Equals(_FilterProduct))
            ElseIf TypeOf _FilterProduct Is DTOProductCategory Then
                retval = _Values.FindAll(Function(x) x.Sku.Category.Equals(_FilterProduct))
            ElseIf TypeOf _FilterProduct Is DTOProductSku Then
                retval = _Values.FindAll(Function(x) x.Sku.Equals(_FilterProduct))
            End If
        End If

        If _Filter > "" Then
            retval = retval.Where(Function(x) x.Sku.matches(_Filter)).ToList
        End If
        Return retval
    End Function


    Public Property ProductFilter As DTOProduct
        Get
            Return _FilterProduct
        End Get
        Set(value As DTOProduct)
            _FilterProduct = value
            If _Values IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearProductFilter()
        If _FilterProduct IsNot Nothing Then
            _FilterProduct = Nothing
            Refresca()
        End If
    End Sub

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

    Public ReadOnly Property Value As DTOPurchaseOrderItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPurchaseOrderItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

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
        With MyBase.Columns(Cols.Pdc)
            .HeaderText = "Comanda"
            .DataPropertyName = "Pdc"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Demanat"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn2)
            .HeaderText = "Pendent"
            .DataPropertyName = "Pn2"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Preu)
            .HeaderText = "Preu"
            .DataPropertyName = "Preu"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dte"
            .DataPropertyName = "Dto"
            .Width = 50
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

    Private Function SelectedItems() As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
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
            Dim oMenu_PurchaseOrderItem As New Menu_PurchaseOrderItem(SelectedItems.First)
            AddHandler oMenu_PurchaseOrderItem.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_PurchaseOrderItem.Range)
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
            Dim oFrm As New Frm_PurchaseOrder(oCurrentControlItem.Source.PurchaseOrder)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Private Sub Xl_ContactPurchaseOrderItems_RowPrePaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem.Pn2 > 0 Then
            oRow.DefaultCellStyle.BackColor = Color.GreenYellow
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOPurchaseOrderItem

        Property Pdc As Integer
        Property Fch As Date
        Property Qty As Integer
        Property Sku As String
        Property Pn2 As Integer
        Property Dto As Decimal
        Property Preu As Decimal


        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            With value
                _Pdc = .PurchaseOrder.Num
                _Fch = .PurchaseOrder.Fch
                _Qty = .qty
                _Sku = .sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Pn2 = .Pending
                _Dto = .Dto
                _Preu = .Price.Eur
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


