Public Class Xl_BritaxTarget
    Inherits _Xl_ReadOnlyDatagridview

    Private _Bookfras As List(Of DTOBookFra)
    Private _PurchaseOrders As List(Of DTOPurchaseOrder)
    Private _PendingOrders As List(Of DTOPurchaseOrder)
    Private _DefaultValue As DTOTemplate
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Year As Integer
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        mes
        orders
        fras
        pendingOrders
    End Enum

    Public Shadows Sub Load(year As Integer, bookFras As List(Of DTOBookFra),
                            Orders As List(Of DTOPurchaseOrder),
                            PendingOrders As List(Of DTOPurchaseOrder))
        _Bookfras = bookFras
        _PurchaseOrders = Orders
        _PendingOrders = PendingOrders
        _Year = year

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        _ControlItems = New ControlItems

        Dim oControlItem As New ControlItem(0, _PurchaseOrders, _Bookfras, _PendingOrders)
        _ControlItems.Add(oControlItem)

        For mes As Integer = 1 To 12
            Dim month As Integer = mes
            Dim orders = FEB2.Britax.TargetMonthOrders(_PurchaseOrders, _Year, month)
            Dim fras = _Bookfras.Where(Function(x) x.Cca.Fch.Month = month).ToList
            Dim pendingOrders = FEB2.Britax.TargetMonthOrders(_PendingOrders, _Year, month)
            oControlItem = New ControlItem(mes, orders, fras, pendingOrders)

            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub




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
        With MyBase.Columns(Cols.mes)
            .HeaderText = "Mes"
            .DataPropertyName = "mes"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.orders)
            .HeaderText = "Comandes"
            .DataPropertyName = "Orders"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.fras)
            .HeaderText = "Factures"
            .DataPropertyName = "Fras"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.pendingOrders)
            .HeaderText = "Order book"
            .DataPropertyName = "PendingOrders"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
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
            oContextMenu.Items.Add("factures", Nothing, AddressOf Do_Fras)
            oContextMenu.Items.Add("order book", Nothing, AddressOf Do_PendingOrders)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Fras()
        Dim oControlitem As ControlItem = CurrentControlItem()
        Dim oFras = oControlitem.SourceFras
        If oFras IsNot Nothing Then
            Dim oFrm As New Frm_BookFras(oFras)
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_PendingOrders()
        Dim oBritax = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Roemer)
        Dim oControlitem As ControlItem = CurrentControlItem()
        Dim oOrders = oControlitem.SourcePendingOrders
        If oOrders IsNot Nothing Then
            Dim items = oOrders.SelectMany(Function(x) x.Items).ToList
            Dim oFrm As New Frm_ProveidorPncs(items)
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Integer
        Property SourceOrders As List(Of DTOPurchaseOrder)
        Property SourceFras As List(Of DTOBookFra)
        Property SourcePendingOrders As List(Of DTOPurchaseOrder)

        Property Mes As String
        Property Orders As Decimal
        Property Fras As Decimal
        Property PendingOrders As Decimal

        Public Sub New(mes As Integer, orders As List(Of DTOPurchaseOrder), fras As List(Of DTOBookFra), pendingOrders As List(Of DTOPurchaseOrder))
            MyBase.New()
            _Source = mes
            _SourceOrders = orders
            _SourceFras = fras
            _SourcePendingOrders = pendingOrders

            _Mes = IIf(mes = 0, "totals", Current.Session.Lang.Mes(mes))
            _Orders = orders.Sum(Function(x) x.SumaDeImports.Eur)
            _Fras = fras.Sum(Function(x) x.BaseDevengada.Eur)
            _PendingOrders = pendingOrders.Sum(Function(x) x.Items.Sum(Function(y) y.Pending * y.Price.Eur * (100 - y.Dto) / 100))
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


