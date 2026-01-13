Public Class Xl_EdiversaOrdersSkus

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOEdiversaOrder)
    Private _Stocks As List(Of DTOProductSku)
    Private _DefaultValue As DTOEdiversaOrder
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Qty
        Brand
        Category
        Sku
        Stock
        Pn2
        Pn1
    End Enum

    Public Shadows Sub Load(values As List(Of DTOEdiversaOrder), oStocks As List(Of DTOProductSku))
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        _Values = values
        _Stocks = oStocks
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oSkus As New List(Of DTOProductSku)
        Dim oQtys As New List(Of Integer)
        If _Values IsNot Nothing Then

            For Each oOrder As DTOEdiversaOrder In _Values
                For Each oLine As DTOEdiversaOrderItem In oOrder.Items.FindAll(Function(x) x.Sku IsNot Nothing)
                    Dim oSku As DTOProductSku = oSkus.Find(Function(x) x.Equals(oLine.Sku))
                    If oSku Is Nothing Then
                        oSkus.Add(oLine.Sku)
                        oQtys.Add(oLine.Qty)
                    Else
                        Dim idx As Integer = oSkus.IndexOf(oSku)
                        oQtys(idx) += oLine.Qty
                    End If
                Next
            Next

            For Each oSku As DTOProductSku In oSkus
                Dim oStock As DTOProductSku = _Stocks.Find(Function(x) x.Equals(oSku))
                If oStock IsNot Nothing Then
                    With oSku
                        .Stock = oStock.Stock
                        .Clients = oStock.Clients
                        .Proveidors = oStock.Proveidors
                    End With
                End If
            Next

            Dim UnSortedControlItems As New List(Of ControlItem)
            For i As Integer = 0 To oSkus.Count - 1
                Dim oControlItem As New ControlItem(oSkus(i), oQtys(i))
                UnSortedControlItems.Add(oControlItem)
            Next

            Dim SortedControlItems As List(Of ControlItem) = UnSortedControlItems.OrderBy(Function(x) x.Sku).OrderBy(Function(x) x.Category).OrderBy(Function(x) x.Brand).ToList
            _ControlItems = New ControlItems
            For Each item As ControlItem In SortedControlItems
                _ControlItems.Add(item)
            Next

            MyBase.DataSource = _ControlItems
            If _ControlItems.Count > 0 Then
                MyBase.CurrentCell = MyBase.FirstDisplayedCell
            End If

            If _DefaultValue IsNot Nothing Then
                Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
                Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
                If rowIdx >= 0 Then
                    MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Sku)
                End If
            End If

            SetContextMenu()

        End If
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOProductSku
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductSku = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.DataSource = _ControlItems
        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = True


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Quantitat"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Brand)
            .HeaderText = "Marca"
            .DataPropertyName = "Brand"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Category)
            .HeaderText = "Categoria"
            .DataPropertyName = "Category"
            .Width = 100
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Stock)
            .HeaderText = "Stock"
            .DataPropertyName = "Stock"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn2)
            .HeaderText = "Clients"
            .DataPropertyName = "Pn2"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn1)
            .HeaderText = "Proveidors"
            .DataPropertyName = "Pn1"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
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
            Dim oMenu_art As New Menu_ProductSku(SelectedControlItems.First.Source)
            AddHandler oMenu_art.AfterUpdate, AddressOf RefreshRequest
            Dim oMenuItem As New ToolStripMenuItem("Producte...")
            oMenuItem.DropDownItems.AddRange(oMenu_art.Range)
            oContextMenu.Items.Add(oMenuItem)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("Excel", Nothing, AddressOf Do_Excel)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_Excel()
        Dim oLang = Current.Session.Lang()
        Dim oDomain = DTOWebDomain.Factory(oLang, True)
        Dim oExcel As New MatHelper.Excel.Sheet
        For Each oControlItem As ControlItem In _ControlItems
            Dim oRow = oExcel.addRow()
            Dim oSku As DTOProductSku = oControlItem.Source
            oRow.addCell(oControlItem.Brand)
            oRow.addCell(oControlItem.Category)
            oRow.addCell(oSku.NomLlarg.Tradueix(oLang), oSku.GetUrl(oLang))
            oRow.addCell(oControlItem.Qty)
            oRow.addCell(oControlItem.Stock - oControlItem.Pn2)
            oRow.addCell(oControlItem.Pn1)
        Next
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oExcel, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductSku = SelectedControlItems.First.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_Art(oSelectedValue)
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

    Private Sub Xl_EdiversaOrdersSkus_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles Me.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Stock
                Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                e.CellStyle.BackColor = DTOProductSku.BackColor(oControlItem.Stock, oControlItem.Qty + oControlItem.Pn2)
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTOProductSku

        Property Brand As String
        Property Category As String
        Property Sku As String
        Property Qty As Integer
        Property Stock As Integer
        Property Pn2 As Integer
        Property Pn1 As Integer

        Public Sub New(value As DTOProductSku, iQty As Integer)
            MyBase.New()
            _Source = value
            With value
                _Qty = iQty
                _Brand = value.category.brand.nom.Tradueix(Current.Session.Lang)
                _Category = value.category.nom.Tradueix(Current.Session.Lang)
                _Sku = value.nom.Tradueix(Current.Session.Lang)
                Stock = value.Stock
                Pn2 = value.Clients
                Pn1 = value.Proveidors
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

