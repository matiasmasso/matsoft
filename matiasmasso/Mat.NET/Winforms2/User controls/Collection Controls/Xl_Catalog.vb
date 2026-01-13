Public Class Xl_Catalog
    Inherits Xl_ExpandableDataGridView

    Private _HideObsoleteBrands As Boolean
    Private _HideObsoleteSkus As Boolean
    Private _Cache As DTO.Models.ClientCache
    Private _Filter As String
    Private _ExpandedTags As List(Of DTOProduct)
    Private _KeepExpandedTags As Boolean = True
    Private _SelectionMode As DTOProduct.SelectionModes

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToShowObsolets(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)
    Public Event OnItemsSelected(sender As Object, e As MatEventArgs)


    Private Enum Cols
        Nom
        LastProductionIco
        Stk
        Pn2
        Pn3
        RRPP
        ImgIco
        Pn1
    End Enum

    Public Shadows Sub Load(ByRef oCache As Models.ClientCache, hideObsoleteSkus As Boolean, hideObsoleteBrands As Boolean, Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.Browse)
        _Cache = oCache
        _HideObsoleteSkus = hideObsoleteSkus
        _HideObsoleteBrands = hideObsoleteBrands
        _SelectionMode = oSelectionMode
        MyBase.MultiSelect = True

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Dim oSku = oCache.Skus.FirstOrDefault(Function(x) x.Id = 21336)
        Refresca()
    End Sub

    Public Sub Home()
        'close all expanded items to return to initial tree
        _KeepExpandedTags = False
        Refresca()
    End Sub

    Private Function SelectedItems() As List(Of DTOProduct)
        Dim retval As New List(Of DTOProduct)
        If MyBase.SelectedRows.Count > 0 Then
            For Each row As DataGridViewRow In MyBase.SelectedRows
                Dim oControlItem As ControlItem = row.DataBoundItem
                retval.Add(oControlItem.Tag)
            Next
        Else
            Dim oControlItem As ControlItem = MyBase.CurrentRow.DataBoundItem
            retval.Add(oControlItem.Tag)
        End If
        Return retval
    End Function

    Public Sub UpdateData()
        For Each oExpandableItem In ControlItems
            If oExpandableItem.LinCod = ControlItem.LinCods.Sku Then
                Dim oControlItem As ControlItem = oExpandableItem
                Dim oSku As DTOProductSku = oControlItem.Tag
                oControlItem.SkuStock = _Cache.SkuStock(oSku.Guid)
                oControlItem.Retail = _Cache.RetailPrice(oSku.Guid)
            End If
        Next
    End Sub

    Private Sub Refresca()
        MyBase.AllowEvents = False

        'Register previously expanded items so we can restore them after refreshing the data
        If _KeepExpandedTags Then
            BuildPreviousExpandedItems()
        End If

        'Populate first tree level
        MyBase.ControlItems = New ExpandableItems
        For Each oBrand In FilteredBrands()
            Dim oControlItem As New ControlItem(oBrand)
            MyBase.ControlItems.Add(oControlItem)
        Next

        'Expand previously expanded items, if any
        If _KeepExpandedTags Then
            RestoreExpandedItems()
        Else
            'someone requested to clear expanded items.
            'make sure new expanded items are kept on next refresh 
            _KeepExpandedTags = True
        End If

        MyBase.BindDataSource()

        'Expand level if it contains just one item
        ExpandSingleItemLevels()

        MyBase.ClearSelection()
        MyBase.AllowEvents = True
    End Sub

    Private Sub BuildPreviousExpandedItems()
        _ExpandedTags = New List(Of DTOProduct)
        If MyBase.ControlItems IsNot Nothing Then
            For Each oControlItem In MyBase.ControlItems.Where(Function(x) x.IsExpanded)
                _expandedTags.Add(oControlItem.Tag)
            Next
        End If
    End Sub

    Private Sub RestoreExpandedItems()
        Dim oControlItemsToExpand = MyBase.ControlItems.Where(Function(x) _ExpandedTags.Any(Function(y) y.Guid.Equals(x.Tag.Guid)) And x.IsCollapsed).ToList()
        For Each oControlItem In oControlItemsToExpand
            MyBase.SelectedItem = oControlItem
            MyBase.ExpandOrCollapse()
            'search again for expanded tags
            RestoreExpandedItems()
        Next
    End Sub

    Private Function FilteredBrands() As List(Of DTOProductBrand)
        Dim retval = FilteredCategories().Select(Function(x) x.Brand).
            GroupBy(Function(y) y.Guid).
            Select(Function(z) z.First).
            ToList()
        Dim oEmptyBrands = New List(Of DTOProductBrand)(_Cache.Brands.Where(Function(x) Not _Cache.Categories.Any(Function(y) y.Brand.Guid.Equals(x.Guid))))
        If Not String.IsNullOrEmpty(_Filter) Then
            oEmptyBrands = oEmptyBrands.Where(Function(x) x.Matches(_Filter)).ToList()
        End If
        If _HideObsoleteBrands Then retval.RemoveAll(Function(x) x.obsoleto)
        Return retval
    End Function

    Private Function FilteredCategories(Optional oBrand As DTOProductBrand = Nothing) As List(Of DTOProductCategory)
        Dim retval As List(Of DTOProductCategory)
        If oBrand Is Nothing Then
            retval = FilteredSkus().Select(Function(x) x.Category).
            GroupBy(Function(y) y.Guid).
            Select(Function(z) z.First).
            ToList()
            Dim oEmptyCategories = New List(Of DTOProductCategory)(_Cache.Categories.Where(Function(x) Not _Cache.Skus.Any(Function(y) y.Category.Guid.Equals(x.Guid))))
            If Not String.IsNullOrEmpty(_Filter) Then
                oEmptyCategories = oEmptyCategories.Where(Function(x) x.Matches(_Filter)).ToList()
            End If
            retval.AddRange(oEmptyCategories)
        Else
            retval = FilteredSkus().Select(Function(x) x.Category).
            Where(Function(a) a.Brand.Equals(oBrand)).
            GroupBy(Function(y) y.Guid).
            Select(Function(z) z.First).
            ToList()
            Dim oEmptyCategories = New List(Of DTOProductCategory)(_Cache.Categories.Where(Function(x) x.Brand.Guid.Equals(oBrand.Guid) And Not _Cache.Skus.Any(Function(y) y.Category.Guid.Equals(x.Guid))))
            retval.AddRange(oEmptyCategories)
        End If
        If _HideObsoleteSkus Then retval.RemoveAll(Function(x) x.obsoleto)
        Return retval
    End Function

    Private Function FilteredSkus(Optional oCategory As DTOProductCategory = Nothing) As List(Of DTOProductSku)
        Dim retval = DisplayableSkus()
        If Not String.IsNullOrEmpty(_Filter) Then
            retval = retval.Where(Function(x) x.Matches(_Filter)).ToList()
        End If
        If oCategory IsNot Nothing Then
            'retval = retval.Where(Function(x) x.Category IsNot Nothing AndAlso x.Category.Equals(oCategory)).ToList()
            retval = retval.Where(Function(x) x.Category.Equals(oCategory)).ToList()
        End If
        'If _HideObsoleteSkus Then retval.RemoveAll(Function(x) ShouldHilde(x))
        Return retval
    End Function

    Private Function DisplayableSkus() As List(Of DTOProductSku)
        Dim retval = New List(Of DTOProductSku)(_Cache.Skus) 'copies the elements into a new List
        If _HideObsoleteSkus Then
            retval.RemoveAll(Function(x) ShouldHide(x))
        End If
        If _HideObsoleteBrands Then
            retval.RemoveAll(Function(x) x.Category.Brand.obsoleto)
        End If
        Return retval
    End Function

    Private Function ShouldHide(oSku As DTOProductSku) As Boolean
        Dim retval As Boolean = False
        If oSku.ObsoletoConfirmed <> DateTime.MinValue Or oSku.Category.obsoleto Or oSku.Category.Brand.obsoleto Then
            Dim oSkuStock = _Cache.SkuStock(oSku.Guid)
            If oSkuStock Is Nothing Then
                retval = True
            Else
                If oSkuStock.Stock = 0 And oSkuStock.Clients = 0 And oSkuStock.Proveidors = 0 Then
                    retval = True
                End If
            End If
        End If
        Return retval
    End Function

    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If MyBase.AllowEvents Then
                Refresca()
            End If
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowTemplate.DefaultCellStyle.BackColor = Color.Transparent

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
            .DataPropertyName = "Caption"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomLeft
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.LastProductionIco), DataGridViewImageColumn)
            .HeaderText = ""
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.NullValue = Nothing
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Stk)
            .DataPropertyName = "stock"
            .HeaderText = "stock"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn2)
            .DataPropertyName = "clients"
            .HeaderText = "clients"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn3)
            .DataPropertyName = "pot"
            .HeaderText = "prog"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.RRPP)
            .HeaderText = "PVP"
            .DataPropertyName = "RRPP"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With
        MyBase.Columns.Add(New DataGridViewImageColumn)
        With DirectCast(MyBase.Columns(Cols.ImgIco), DataGridViewImageColumn)
            .HeaderText = ""
            .Width = 18
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.NullValue = Nothing
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pn1)
            .DataPropertyName = "proveidors"
            .HeaderText = "prov"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight
            .DefaultCellStyle.Format = "#"
            .DefaultCellStyle.SelectionBackColor = Color.Transparent
            .DefaultCellStyle.SelectionForeColor = Color.Black
        End With

    End Sub

    Private Function CurrentControlItem() As ControlItem
        Dim retval As ControlItem = Nothing
        Dim oRow As DataGridViewRow = MyBase.CurrentRow
        If oRow IsNot Nothing Then
            retval = oRow.DataBoundItem
        End If
        Return retval
    End Function

    Private Sub Xl_Catalog_RequestToInsertNestedItems(sender As Object, e As MatEventArgs) Handles Me.RequestToInsertNestedItems
        Dim includeObsolets As Boolean = e.Argument
        Select Case MyBase.SelectedItem.LinCod
            Case ControlItem.LinCods.Brand
                MyBase.InsertControlItems(CategoryItems(includeObsolets))
            Case ControlItem.LinCods.Category
                MyBase.InsertControlItems(SkuItems(includeObsolets))
        End Select
    End Sub

    Private Function CategoryItems(Optional includeObsolets As Boolean = False) As ExpandableItems
        Dim retval As New ExpandableItems
        Dim oBrand As DTOProductBrand = SelectedItem.Tag
        For Each category In FilteredCategories(oBrand)
            Dim oControlItem = New ControlItem(category)
            retval.Add(oControlItem)
        Next
        Return retval
    End Function

    Private Function SkuItems(Optional includeObsolets As Boolean = False) As ExpandableItems
        Dim retval As New ExpandableItems
        Dim oCategory As DTOProductCategory = SelectedItem.Tag
        For Each sku In FilteredSkus(oCategory)
            If sku.BundleSkus.Count > 0 Then
                Dim oControlItem As New ControlItem(sku, DTOProductSku.BundleCods.parent)
                retval.Add(oControlItem)
                For Each oBundleChild In sku.BundleSkus
                    oControlItem = New ControlItem(oBundleChild.Sku, DTOProductSku.BundleCods.child)
                    retval.Add(oControlItem)
                Next
            Else
                Dim oControlItem = New ControlItem(sku, DTOProductSku.BundleCods.none)
                retval.Add(oControlItem)
            End If
        Next
        Return retval
    End Function

    Private Sub SetContextMenu()
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = MyBase.SelectedItem

        If oControlItem IsNot Nothing Then
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Brand
                    Dim oMenu_Brand As New Menu_ProductBrand(CType(oControlItem.Tag, DTOProductBrand))
                    AddHandler oMenu_Brand.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Brand.Range)
                Case ControlItem.LinCods.Category
                    Dim oMenu_Category As New Menu_ProductCategory(CType(oControlItem.Tag, DTOProductCategory))
                    AddHandler oMenu_Category.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Category.Range)
                Case ControlItem.LinCods.Sku
                    Dim oMenu_Sku As New Menu_ProductSku(CType(oControlItem.Tag, DTOProductSku))
                    AddHandler oMenu_Sku.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Sku.Range)
                    oContextMenu.Items.Add("-")
                    oContextMenu.Items.Add("veure obsolets", Nothing, AddressOf Do_ShowSkuObsolets)
            End Select
        End If

        Dim oMenuItemSelect As ToolStripMenuItem = Nothing
        Select Case _SelectionMode
            Case DTOProduct.SelectionModes.Browse
            Case DTOProduct.SelectionModes.SelectAny
                oMenuItemSelect = oContextMenu.Items.Add("seleccionar", Nothing, AddressOf Do_Select)
                If SelectedItems.Count > 1 Then oMenuItemSelect.Enabled = False
            Case DTOProduct.SelectionModes.Selectbrand
                oMenuItemSelect = oContextMenu.Items.Add("seleccionar", Nothing, AddressOf Do_Select)
                If SelectedItem.LinCod <> ControlItem.LinCods.Brand Then oMenuItemSelect.Enabled = False
                If SelectedItems.Count > 1 Then oMenuItemSelect.Enabled = False
            Case DTOProduct.SelectionModes.SelectCategory
                oMenuItemSelect = oContextMenu.Items.Add("seleccionar", Nothing, AddressOf Do_Select)
                If SelectedItem.LinCod <> ControlItem.LinCods.Category Then oMenuItemSelect.Enabled = False
                If SelectedItems.Count > 1 Then oMenuItemSelect.Enabled = False
            Case DTOProduct.SelectionModes.SelectSku
                oMenuItemSelect = oContextMenu.Items.Add("seleccionar", Nothing, AddressOf Do_Select)
                If SelectedItem.LinCod <> ControlItem.LinCods.Sku Then oMenuItemSelect.Enabled = False
                If SelectedItems.Count > 1 Then oMenuItemSelect.Enabled = False
            Case DTOProduct.SelectionModes.SelectMany
                Dim msg = String.Format("seleccionar {0} productes", SelectedItems.Count)
                oMenuItemSelect = oContextMenu.Items.Add(msg, Nothing, AddressOf Do_Select)
            Case DTOProduct.SelectionModes.SelectBrands
                Dim msg = String.Format("seleccionar {0} marques", SelectedItems.Count)
                oMenuItemSelect = oContextMenu.Items.Add(msg, Nothing, AddressOf Do_Select)
                oMenuItemSelect.Enabled = SelectedItems.All(Function(x) x.SourceCod = DTOProduct.SourceCods.Brand)
            Case DTOProduct.SelectionModes.SelectCategories
                Dim msg = String.Format("seleccionar {0} categories", SelectedItems.Count)
                oMenuItemSelect = oContextMenu.Items.Add(msg, Nothing, AddressOf Do_Select)
                oMenuItemSelect.Enabled = SelectedItems.All(Function(x) x.SourceCod = DTOProduct.SourceCods.Category)
            Case DTOProduct.SelectionModes.SelectSkus
                Dim msg = String.Format("seleccionar {0} articles", SelectedItems.Count)
                oMenuItemSelect = oContextMenu.Items.Add(msg, Nothing, AddressOf Do_Select)
                oMenuItemSelect.Enabled = SelectedItems.All(Function(x) x.SourceCod = DTOProduct.SourceCods.Sku)
        End Select

        If oMenuItemSelect IsNot Nothing Then
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add(oMenuItemSelect)
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Select()
        Select Case _SelectionMode
            Case DTOProduct.SelectionModes.SelectAny, DTOProduct.SelectionModes.Selectbrand, DTOProduct.SelectionModes.SelectCategory, DTOProduct.SelectionModes.SelectSku
                Dim item = SelectedItem().Tag
                RaiseEvent OnItemSelected(Me, New MatEventArgs(item))
            Case DTOProduct.SelectionModes.SelectMany, DTOProduct.SelectionModes.SelectBrands, DTOProduct.SelectionModes.SelectCategories, DTOProduct.SelectionModes.SelectSkus
                Dim items = SelectedItems()
                RaiseEvent OnItemsSelected(Me, New MatEventArgs(items))
        End Select
    End Sub

    Private Sub Do_ShowSkuObsolets()
        RaiseEvent RequestToShowObsolets(Me, MatEventArgs.Empty)
        'Dim oParentItem = MyBase.ParentItem
        'If oParentItem IsNot Nothing Then
        '    MyBase.SelectedItem = oParentItem
        '    MyBase.ExpandOrCollapse() 'collapses nested levels
        '    MyBase.SelectedItem = oParentItem
        '    MyBase.ExpandOrCollapse(True) 'inserts skus including obsolets
        'End If
    End Sub




    Protected Shadows Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If MyBase.AllowEvents Then SetContextMenu()
    End Sub




    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Dim oRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        If oControlItem IsNot Nothing Then
            If oControlItem.LinCod = ControlItem.LinCods.Sku Then
                Dim oSku As DTOProductSku = oControlItem.Tag
                Dim oCell = oRow.Cells(e.ColumnIndex)
                oCell.ToolTipText = ""

                Select Case e.ColumnIndex
                    Case Cols.Nom
                        If oControlItem.BundleCod <> DTOProductSku.BundleCods.child Then
                            Dim oSkuStock = oControlItem.SkuStock
                            If oSkuStock Is Nothing Then
                                If oSku.obsoleto Then
                                    e.CellStyle.BackColor = Color.LightGray
                                Else
                                    e.CellStyle.BackColor = Color.LightSalmon
                                End If
                            Else
                                Select Case oControlItem.SkuStock.Stock
                                    Case <= 0
                                        If oSku.obsoleto Then
                                            e.CellStyle.BackColor = Color.LightGray
                                        Else
                                            e.CellStyle.BackColor = Color.LightSalmon
                                        End If
                                    Case <= oControlItem.SkuStock.Clients - oControlItem.SkuStock.ClientsAlPot - oControlItem.SkuStock.ClientsEnProgramacio
                                        e.CellStyle.BackColor = Color.Yellow
                                    Case Else
                                        e.CellStyle.BackColor = Color.LightGreen
                                End Select
                            End If
                        End If
                        Dim sb As New Text.StringBuilder
                        sb.AppendLine(oSku.NomLlarg.Esp)
                        If oSku.Ean13 IsNot Nothing Then
                            sb.AppendFormat("Ean: {0}", oSku.Ean13.Value)
                        End If
                        oCell.ToolTipText = sb.ToString


                    Case Cols.Pn1
                        If oControlItem.SkuStock Is Nothing OrElse oControlItem.SkuStock.Proveidors = 0 Then
                            e.CellStyle.BackColor = MyBase.DefaultCellStyle.BackColor
                        Else
                            e.Value = oControlItem.SkuStock.Proveidors '- oControlItem.SkuStock.ClientsBlockStock
                            Dim sb As New Text.StringBuilder
                            sb.AppendFormat("{0} unitats pendents de proveidor", oControlItem.SkuStock.Proveidors)
                            'If oControlItem.SkuStock.ClientsBlockStock <> 0 Then
                            '    If sb.Length > 0 Then sb.Append(" - ")
                            '    sb.AppendFormat("{0} de stock bloquejat", oControlItem.SkuStock.ClientsBlockStock)
                            'End If
                            If oControlItem.Previsio = 0 Then
                                e.CellStyle.BackColor = MyBase.DefaultCellStyle.BackColor
                            ElseIf oControlItem.Previsio > oControlItem.SkuStock.Clients - oControlItem.SkuStock.Stock Then
                                e.CellStyle.BackColor = Color.LightGreen
                                sb.AppendFormat(". Prevista la arribada de {0} unitats", oControlItem.Previsio)
                            ElseIf oControlItem.Previsio = oControlItem.SkuStock.Clients Then
                                e.CellStyle.BackColor = Color.Yellow
                                sb.AppendFormat(". Prevista la arribada de {0} unitats", oControlItem.Previsio)
                            Else
                                e.CellStyle.BackColor = Color.LightSalmon
                                sb.AppendFormat(". Prevista la arribada de {0} unitats", oControlItem.Previsio)
                            End If
                            oCell.ToolTipText = sb.ToString
                        End If


                    Case Cols.LastProductionIco
                        If oSku.obsoleto Then
                            If oSku.ObsoletoConfirmed <> Nothing Then
                                e.Value = My.Resources.del
                                oCell.ToolTipText = String.Format("Descatalogat el {0:dd/MM/yy}", oSku.ObsoletoConfirmed)
                            Else
                                e.Value = My.Resources.aspa
                                oCell.ToolTipText = String.Format("Obsolet pendent de confirmar per el magatzem", oSku.ObsoletoConfirmed)
                            End If
                        ElseIf oSku.LastProduction Then
                            e.Value = My.Resources.wrong
                            oCell.ToolTipText = String.Format("Últimes unitats en producció")
                        Else
                            Select Case oSku.Moq
                                Case 0, 1
                                    e.Value = My.Resources.empty
                                    oCell.ToolTipText = String.Format("Servei en caixes unitaries")
                                Case 2
                                    e.Value = My.Resources.dau2_17x17
                                    oCell.ToolTipText = String.Format("Servei en caixes de 2 unitats")
                                Case 3
                                    e.Value = My.Resources.dau3_17x17
                                    oCell.ToolTipText = String.Format("Servei en caixes de 3 unitats")
                                Case 4
                                    e.Value = My.Resources.dau4_17x17
                                    oCell.ToolTipText = String.Format("Servei en caixes de 4 unitats")
                                Case 5
                                    e.Value = My.Resources.dau5_17x17
                                    oCell.ToolTipText = String.Format("Servei en caixes de 5 unitats")
                                Case Else
                                    e.Value = My.Resources.dau6_17x17
                                    oCell.ToolTipText = String.Format("Servei en caixes de mes de 5 unitats")
                            End Select
                        End If
                    Case Cols.Stk
                        If oControlItem.SkuStock IsNot Nothing Then
                            e.Value = oControlItem.SkuStock.Stock - oControlItem.SkuStock.ClientsBlockStock
                            If oControlItem.SkuStock.ClientsBlockStock = 0 Then
                                oCell.ToolTipText = String.Format("{0} unitats en stock", oControlItem.SkuStock.Stock)
                            Else
                                oCell.ToolTipText = String.Format("{0} unitats en stock - {1} unitats bloquejades", oControlItem.SkuStock.Stock, oControlItem.SkuStock.ClientsBlockStock)
                            End If
                        End If
                    Case Cols.Pn2
                        If oControlItem.SkuStock IsNot Nothing Then
                            e.Value = oControlItem.SkuStock.Clients - oControlItem.SkuStock.ClientsAlPot - oControlItem.SkuStock.ClientsEnProgramacio - oControlItem.SkuStock.ClientsBlockStock
                            Dim sb As New Text.StringBuilder
                            sb.AppendFormat("{0} pendents de clients", oControlItem.SkuStock.Clients)
                            If oControlItem.SkuStock.ClientsAlPot <> 0 Then sb.AppendFormat(" - {0} al pot", oControlItem.SkuStock.ClientsAlPot)
                            If oControlItem.SkuStock.ClientsEnProgramacio <> 0 Then sb.AppendFormat(" - {0} en programació", oControlItem.SkuStock.ClientsEnProgramacio)
                            If oControlItem.SkuStock.ClientsBlockStock <> 0 Then sb.AppendFormat(" - {0} de stock bloquejat", oControlItem.SkuStock.ClientsBlockStock)
                            oCell.ToolTipText = sb.ToString
                        End If
                    Case Cols.Pn3
                        If oControlItem.SkuStock IsNot Nothing Then
                            e.Value = oControlItem.SkuStock.ClientsEnProgramacio + oControlItem.SkuStock.ClientsAlPot
                            Dim sb As New Text.StringBuilder
                            If oControlItem.SkuStock.ClientsEnProgramacio <> 0 Then sb.AppendFormat("{0} unitats en programació", oControlItem.SkuStock.ClientsEnProgramacio)
                            If oControlItem.SkuStock.ClientsAlPot <> 0 Then
                                If sb.Length > 0 Then sb.Append(" + ")
                                sb.AppendFormat("{0} unitats al pot", oControlItem.SkuStock.ClientsAlPot)
                            End If
                            oCell.ToolTipText = sb.ToString
                        End If
                    Case Cols.RRPP
                        If oControlItem.Retail <> 0 Then
                            e.Value = oControlItem.Retail
                            oCell.ToolTipText = String.Format("Preu recomanat de venda al public: {0:#,##0.00 €}", oControlItem.Retail)
                        End If
                    Case Cols.ImgIco
                        If oControlItem.BundleCod = DTOProductSku.BundleCods.child Then
                            e.Value = My.Resources.empty
                            oCell.ToolTipText = ""
                        Else
                            If oSku.ImageExists Then
                                e.Value = My.Resources.img_16
                                oCell.ToolTipText = "article amb imatge"
                            Else
                                e.Value = My.Resources.empty
                                oCell.ToolTipText = "falta pujar la imatge d'aquest article"
                            End If
                        End If

                End Select
            ElseIf oControlItem.LinCod = ControlItem.LinCods.Category And oControlItem.IsExpanded Then
                'insert column headers when expanding category
                If e.ColumnIndex > 0 AndAlso TypeOf MyBase.Columns(e.ColumnIndex) Is DataGridViewTextBoxColumn Then
                    e.Value = MyBase.Columns(e.ColumnIndex).HeaderText
                End If
            End If
        End If

        e.CellStyle.SelectionBackColor = e.CellStyle.BackColor
    End Sub


    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem

        Select Case oControlItem.LinCod
            Case ControlItem.LinCods.Brand
                Dim oBrand As DTOProductBrand = oControlItem.Tag
                If oBrand.obsoleto Then
                    oRow.DefaultCellStyle.BackColor = Color.LightGray
                End If
            Case ControlItem.LinCods.Category
                Dim oCategory As DTOProductCategory = oControlItem.Tag
                If oCategory.obsoleto Then
                    oRow.DefaultCellStyle.BackColor = Color.LightGray
                End If
            Case ControlItem.LinCods.Sku
                If oControlItem.BundleCod = DTOProductSku.BundleCods.child Then
                    oRow.DefaultCellStyle.ForeColor = Color.FromArgb(70, 70, 70)
                End If
        End Select
    End Sub


    Private Sub Xl_Catalog_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        Dim oLinCod = oControlItem.LinCod
        Select Case _SelectionMode
            Case DTOProduct.SelectionModes.SelectAny
                RaiseEvent OnItemSelected(Me, New MatEventArgs(oControlItem.Tag))
            Case DTOProduct.SelectionModes.Selectbrand
                If oLinCod = ControlItem.LinCods.Brand Then RaiseEvent OnItemSelected(Me, New MatEventArgs(oControlItem.Tag))
            Case DTOProduct.SelectionModes.SelectCategory
                If oLinCod = ControlItem.LinCods.Category Then RaiseEvent OnItemSelected(Me, New MatEventArgs(oControlItem.Tag))
            Case DTOProduct.SelectionModes.SelectSku
                If oLinCod = ControlItem.LinCods.Sku Then RaiseEvent OnItemSelected(Me, New MatEventArgs(oControlItem.Tag))
            Case DTOProduct.SelectionModes.SelectMany
                Dim oProducts As New List(Of DTOProduct)
                oProducts.Add(oControlItem.Tag)
                RaiseEvent OnItemsSelected(Me, New MatEventArgs(oProducts))
            Case DTOProduct.SelectionModes.SelectBrands
                Dim oProducts As New List(Of DTOProduct)
                oProducts.Add(oControlItem.Tag)
                If oLinCod = ControlItem.LinCods.Brand Then RaiseEvent OnItemsSelected(Me, New MatEventArgs(oProducts))
            Case DTOProduct.SelectionModes.SelectCategories
                Dim oProducts As New List(Of DTOProduct)
                oProducts.Add(oControlItem.Tag)
                If oLinCod = ControlItem.LinCods.Category Then RaiseEvent OnItemsSelected(Me, New MatEventArgs(oProducts))
            Case DTOProduct.SelectionModes.SelectSkus
                Dim oProducts As New List(Of DTOProduct)
                oProducts.Add(oControlItem.Tag)
                If oLinCod = ControlItem.LinCods.Sku Then RaiseEvent OnItemsSelected(Me, New MatEventArgs(oProducts))
            Case Else
                Select Case oLinCod
                    Case ControlItem.LinCods.Brand
                        Dim oFrm As New Frm_ProductBrand(oControlItem.Tag)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Case ControlItem.LinCods.Category
                        Dim oFrm As New Frm_ProductCategory(oControlItem.Tag)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                    Case ControlItem.LinCods.Sku
                        Dim oFrm As New Frm_ProductSku(oControlItem.Tag)
                        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                        oFrm.Show()
                End Select
        End Select
    End Sub

    Protected Class ControlItem
        Inherits ExpandableItem
        Public Property SkuStock As Models.SkuStock
        Public Property Previsio As Integer
        Public Property Retail As Decimal

        Public BundleCod As DTOProductSku.BundleCods

        Public Enum LinCods
            Brand
            Category
            Sku
        End Enum

        Public Sub New(brand As DTOProductBrand)
            MyBase.New(brand.Guid, LinCods.Brand, brand.Nom.Esp, 0, Statuses.Collapsed)
            MyBase.Tag = brand
            MyBase.CellSpan = CellSpanCods.SpanAll
        End Sub

        Public Sub New(category As DTOProductCategory)
            MyBase.New(category.Guid, LinCods.Category, category.Nom.Esp, 1, Statuses.Collapsed)
            MyBase.Tag = category
            MyBase.CellSpan = CellSpanCods.SpanIfCollapsed
        End Sub

        Public Sub New(sku As DTOProductSku, oBundleCod As DTOProductSku.BundleCods)
            MyBase.New(sku.Guid, LinCods.Sku, String.Format("{0:00000} {1}", sku.Id, sku.Nom.Esp), 2, Statuses.None)
            BundleCod = oBundleCod
            MyBase.Tag = sku
            Dim oCache = FEB.GlobalVariables.Cache(Current.Session.Emp)

            Select Case oBundleCod
                Case DTOProductSku.BundleCods.parent
                    SkuStock = ParentBundleStocks(sku, oCache)
                    Retail = oCache.RetailPrice(sku.Guid)
                Case DTOProductSku.BundleCods.child
                    MyBase.Level = 3
                    SkuStock = oCache.SkuStock(sku.Guid)
                    'Retail = oCache.RetailPrice(sku.Guid)
                    Previsio = oCache.SkuPrevisio(sku.Guid)
                Case DTOProductSku.BundleCods.none
                    SkuStock = oCache.SkuStock(sku.Guid)
                    Retail = oCache.RetailPrice(sku.Guid)
                    Previsio = oCache.SkuPrevisio(sku.Guid)
            End Select

        End Sub

        Private Function ParentBundleStocks(oSku As DTOProductSku, oCache As Models.ClientCache) As Models.SkuStock
            Dim retval As New Models.SkuStock

            Dim oSkuChildStocks = oSku.BundleSkus.
                Select(Function(x) oCache.SkuStock(x.Sku.Guid)).
                Where(Function(y) y IsNot Nothing).
                ToList
            If oSkuChildStocks.Count > 0 Then
                With retval
                    .Stock = oSkuChildStocks.Min(Function(x) x.Stock)
                    .Clients = oSkuChildStocks.Max(Function(x) x.Clients)
                    .ClientsAlPot = oSkuChildStocks.Max(Function(x) x.ClientsAlPot)
                    .ClientsBlockStock = oSkuChildStocks.Max(Function(x) x.ClientsBlockStock)
                    .ClientsEnProgramacio = oSkuChildStocks.Max(Function(x) x.ClientsEnProgramacio)
                    .Proveidors = oSkuChildStocks.Min(Function(x) x.Proveidors)
                End With
            End If

            Return retval
        End Function


    End Class



End Class


