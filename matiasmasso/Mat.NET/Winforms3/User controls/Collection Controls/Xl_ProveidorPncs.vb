Public Class Xl_ProveidorPncs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOPurchaseOrderItem)
    Private _Lang As DTOLang
    Private _DefaultValue As DTOPurchaseOrderItem
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _DisplayMode As DisplayModes
    Private _AllowEvents As Boolean

    Public Event RequestToExcel(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum DisplayModes
        Table
        Group
    End Enum

    Private Enum Cols
        Pdc
        Fch
        Sku
        Qty
        Eur
        Dto
        Etd
    End Enum

    Public Shadows Sub Load(values As List(Of DTOPurchaseOrderItem), oLang As DTOLang, Optional oDisplayMode As DisplayModes = DisplayModes.Table)
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _DisplayMode = oDisplayMode
        _Lang = oLang

        Select Case _DisplayMode
            Case DisplayModes.Table
                _Values = values.OrderByDescending(Function(x) x.purchaseOrder.fch).ToList
                MyBase.Columns(Cols.Pdc).Visible = True
                MyBase.Columns(Cols.Fch).Visible = True
                For Each oCol In MyBase.Columns
                    oCol.SortMode = DataGridViewColumnSortMode.Automatic
                Next
            Case DisplayModes.Group
                _Values = values.OrderBy(Function(x) x.purchaseOrder.num).OrderBy(Function(x) x.purchaseOrder.fch).ToList
                MyBase.Columns(Cols.Pdc).Visible = False
                MyBase.Columns(Cols.Fch).Visible = False
                For Each oCol In MyBase.Columns
                    oCol.SortMode = DataGridViewColumnSortMode.NotSortable
                Next
        End Select

        Refresca()
    End Sub

    Public ReadOnly Property Values() As List(Of DTOPurchaseOrderItem)
        Get
            Return _Values
        End Get
    End Property

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOPurchaseOrderItem) = FilteredValues()
        _ControlItems = New ControlItems
        Dim oOrder As New DTOPurchaseOrder
        Dim oControlItem As ControlItem = Nothing
        For Each oItem As DTOPurchaseOrderItem In oFilteredValues
            If _DisplayMode = DisplayModes.Group AndAlso oOrder.UnEquals(oItem.PurchaseOrder) Then
                oOrder = oItem.PurchaseOrder
                If _ControlItems.Count > 0 Then
                    oControlItem = New ControlItem()
                    _ControlItems.Add(oControlItem)
                End If
                oControlItem = New ControlItem(oItem.purchaseOrder, _Lang)
                _ControlItems.Add(oControlItem)
            End If
            oControlItem = New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOPurchaseOrderItem)
        Dim retval As List(Of DTOPurchaseOrderItem)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.sku.nom.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOPurchaseOrderItem
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOPurchaseOrderItem = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowPurchaseOrderItem.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Uds"
            .DataPropertyName = "Qty"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Preu"
            .DataPropertyName = "Eur"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#.#\%;-#.#\%;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Etd)
            .HeaderText = "ETD"
            .DataPropertyName = "ETD"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
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

    Private Function SelectedItems() As Object
        Dim retval As Object = Nothing
        Dim oControlItem As ControlItem = CurrentControlItem()
        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.Order
                retval = SelectedOrders()
            Case ControlItem.LinCods.Item
                retval = SelectedOrderItems()
        End Select

        Return retval
    End Function


    Private Function SelectedOrders() As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Order
                    retval.Add(oControlItem.Source)
            End Select
        Next

        If retval.Count = 0 Then retval.Add(CurrentControlItem.Source)
        Return retval
    End Function

    Private Function SelectedOrderItems() As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        For Each oRow As DataGridViewRow In MyBase.SelectedRows
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Item
                    retval.Add(oControlItem.Source)
            End Select
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
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Blank
                Case ControlItem.LinCods.Order
                    Dim oMenu_PurchaseOrder As New Menu_Pdc(SelectedOrders)
                    AddHandler oMenu_PurchaseOrder.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_PurchaseOrder.Range)
                    oContextMenu.Items.Add("-")
                Case ControlItem.LinCods.Item
                    Dim oMenu_PurchaseOrderItem As New Menu_PurchaseOrderItem(SelectedOrderItems)
                    AddHandler oMenu_PurchaseOrderItem.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_PurchaseOrderItem.Range)
                    oContextMenu.Items.Add("-")
            End Select
        End If
        oContextMenu.Items.Add("nova ETD", Nothing, AddressOf Do_EditETD)
        oContextMenu.Items.Add("Excel", My.Resources.Excel, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_EditETD()
        Dim oFrm As New Frm_Etd(SelectedItems)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Excel()
        Dim items As New List(Of DTOPurchaseOrderItem)
        RaiseEvent RequestToExcel(Me, New MatEventArgs(SelectedItems))
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOPurchaseOrderItem = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    'Dim oFrm As New Frm_PurchaseOrderItem(oSelectedValue)
                    'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    'oFrm.Show()
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

    Private Sub Xl_ProveidorPncs_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.Order
                oRow.DefaultCellStyle.BackColor = Color.LightBlue
        End Select
    End Sub

    Private Sub Xl_ProveidorPncs_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        If _DisplayMode = DisplayModes.Group AndAlso e.ColumnIndex = Cols.Sku Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            Dim oControlItem As ControlItem = oRow.DataBoundItem
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Item
                    oRow.Cells(Cols.Sku).Style.Padding = New Padding(30, 0, 0, 0)
            End Select
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOBaseGuid

        Property Pdc As Integer
        Property Fch As Date
        Property Sku As String
        Property Qty As Integer
        Property Eur As Decimal
        Property Dto As Decimal
        Property Etd As Nullable(Of Date)
        Property LinCod As LinCods


        Public Enum LinCods
            Blank
            Order
            Item
        End Enum

        Public Sub New()
            MyBase.New()
            _LinCod = LinCods.Blank
        End Sub

        Public Sub New(value As DTOPurchaseOrder, oLang As DTOLang)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Order
            With value
                _Pdc = .num
                _Fch = .fch
                _Sku = DTOPurchaseOrder.fullConcepte(value, oLang, True)
                _Eur = .items.Where(Function(y) y.pending > 0).Sum(Function(x) DTOAmt.import(x.qty, x.price, x.dto).Eur)
            End With
        End Sub

        Public Sub New(value As DTOPurchaseOrderItem)
            MyBase.New()
            _Source = value
            _LinCod = LinCods.Item
            With value
                _Pdc = .PurchaseOrder.Num
                _Fch = .PurchaseOrder.fch
                _Sku = .sku.nomLlarg.Tradueix(Current.Session.Lang)
                _Qty = .Pending
                _Eur = .Price.Eur
                _Dto = .Dto
                If .ETD <> Nothing Then
                    _Etd = .ETD
                End If
            End With


        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


