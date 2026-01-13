Public Class Xl_InvRpts
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTO.Models.InvrptModel

    Private _Mode As DTO.Models.InvrptModel.Modes
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        Qty
        Amt
    End Enum

    Public Shadows Sub Load(value As DTO.Models.InvrptModel, Optional mode As DTO.Models.InvrptModel.Modes = DTO.Models.InvrptModel.Modes.Catalog)
        _Value = value
        _Mode = mode

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim items = _Value

        _ControlItems = New ControlItems
        Dim oTotalItem As New ControlItem(Nothing, ControlItem.LinCods.Total, "Total", 0, _Value.Items.Sum(Function(x) x.Qty * x.Retail), 0, ControlItem.ExpandModes.Expanded)
        _ControlItems.Add(oTotalItem)
        MyBase.DataSource = _ControlItems
        MyBase.CurrentCell = MyBase.Rows(0).Cells(Cols.Nom)

        Dim oControlItems = IIf(_Mode = Models.InvrptModel.Modes.Catalog, BrandItems(), CustomerItems())
        For Each oControlItem In oControlItems
            _ControlItems.Add(oControlItem)
        Next

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function BrandItems() As ControlItems
        Dim retval As New ControlItems
        Dim oCustomer As DTOGuidNom.Compact = CurrentParent(ControlItem.LinCods.Customer)
        Dim level = CurrentLevel() + 1
        For Each brand In _Value.Catalog.Brands.OrderBy(Function(x) x.Nom).ToList()
            Dim items = _Value.Items.Where(Function(a) brand.Categories.SelectMany(Function(b) b.Skus).Any(Function(c) c.Guid = a.ProductGuid)).ToList()
            If oCustomer IsNot Nothing Then items = items.Where(Function(x) x.CustomerGuid = oCustomer.Guid).ToList()
            If items.Count > 0 Then
                Dim oControlItem As New ControlItem(brand, ControlItem.LinCods.Brand, brand.Nom, 0, items.Sum(Function(x) x.Qty * x.Retail), level, ControlItem.ExpandModes.Collapsed)
                retval.Add(oControlItem)
            End If
        Next
        Return retval
    End Function

    Private Function CategoryItems() As ControlItems
        Dim retval As New ControlItems
        Dim oBrand As Models.CatalogModel.Brand = CurrentParent(ControlItem.LinCods.Brand)
        Dim oCustomer As DTOGuidNom.Compact = CurrentParent(ControlItem.LinCods.Customer)
        Dim level = CurrentLevel() + 1
        For Each category In oBrand.Categories.OrderBy(Function(x) x.Nom).ToList()
            Dim items = _Value.Items.Where(Function(a) category.Skus.Any(Function(b) b.Guid.Equals(a.ProductGuid))).ToList()
            If oCustomer IsNot Nothing Then items = items.Where(Function(x) x.CustomerGuid = oCustomer.Guid).ToList()
            If items.Count > 0 Then
                Dim oControlItem As New ControlItem(category, ControlItem.LinCods.Category, category.Nom, items.Sum(Function(x) x.Qty), items.Sum(Function(x) x.Qty * x.Retail), level, ControlItem.ExpandModes.Collapsed)
                retval.Add(oControlItem)
            End If
        Next
        MyBase.Columns(Cols.Qty).Visible = True
        Return retval
    End Function

    Private Function SkuItems() As ControlItems
        Dim retval As New ControlItems
        Dim oCategory As Models.CatalogModel.Category = CurrentParent(ControlItem.LinCods.Category)
        Dim oCustomer As DTOGuidNom.Compact = CurrentParent(ControlItem.LinCods.Customer)
        Dim oExpandNode As ControlItem.ExpandModes = IIf(oCustomer Is Nothing, ControlItem.ExpandModes.Collapsed, ControlItem.ExpandModes.None)
        Dim level = CurrentLevel() + 1
        For Each sku In oCategory.Skus.OrderBy(Function(x) x.Nom).ToList()
            Dim items = _Value.Items.Where(Function(a) sku.Guid = a.ProductGuid).ToList()
            If oCustomer IsNot Nothing Then items = items.Where(Function(x) x.CustomerGuid = oCustomer.Guid).ToList()
            If items.Count > 0 Then
                Dim oControlItem As New ControlItem(sku, ControlItem.LinCods.Sku, sku.Nom, items.Sum(Function(x) x.Qty), items.Sum(Function(x) x.Qty * x.Retail), level, oExpandNode)
                retval.Add(oControlItem)
            End If
        Next
        MyBase.Columns(Cols.Qty).Visible = True
        Return retval
    End Function

    Private Function CustomerItems() As ControlItems
        Dim retval As New ControlItems
        Dim oSku As Models.CatalogModel.Sku = CurrentParent(ControlItem.LinCods.Sku)
        Dim oExpandNode As ControlItem.ExpandModes = IIf(oSku Is Nothing, ControlItem.ExpandModes.Collapsed, ControlItem.ExpandModes.None)
        Dim level = CurrentLevel() + 1
        For Each customer In _Value.Customers
            Dim items = _Value.Items.Where(Function(x) x.CustomerGuid.Equals(customer.Guid)).ToList()
            If oSku IsNot Nothing Then items = items.Where(Function(x) x.ProductGuid = oSku.Guid).ToList()
            If items.Count > 0 Then
                Dim oControlItem As New ControlItem(customer, ControlItem.LinCods.Customer, customer.Nom, items.Sum(Function(x) x.Qty), items.Sum(Function(x) x.Qty * x.Retail), level, oExpandNode)
                retval.Add(oControlItem)
            End If
        Next
        MyBase.Columns(Cols.Qty).Visible = True
        Return retval
    End Function


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.CellSelect
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
        With MyBase.Columns(Cols.Qty)
            .HeaderText = "Unitats"
            .DataPropertyName = "Qty"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Amt)
            .HeaderText = "Import"
            .DataPropertyName = "Volume"
            .Width = 100
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
            Select Case oControlItem.LinCod
                Case ControlItem.LinCods.Brand
                    Dim oBrand As New DTOProductBrand(oControlItem.Tag.Guid)
                    Dim oMenu_Brand = New Menu_ProductBrand(oBrand)
                    oContextMenu.Items.AddRange(oMenu_Brand.Range)
                Case ControlItem.LinCods.Category
                    Dim oCategory As New DTOProductCategory(oControlItem.Tag.Guid)
                    Dim oMenu_Category = New Menu_ProductCategory(oCategory)
                    oContextMenu.Items.AddRange(oMenu_Category.Range)
                Case ControlItem.LinCods.Sku
                    If CurrentParent(ControlItem.LinCods.Customer) IsNot Nothing Then
                        oContextMenu.Items.Add("justificant", Nothing, AddressOf ShowFile)
                        oContextMenu.Items.Add("-")
                    End If

                    Dim oSku As New DTOProductSku(oControlItem.Tag.Guid)
                    Dim oMenu_Sku = New Menu_ProductSku(oSku)
                    oContextMenu.Items.AddRange(oMenu_Sku.Range)
                Case ControlItem.LinCods.Customer
                    If CurrentParent(ControlItem.LinCods.Sku) IsNot Nothing Then
                        oContextMenu.Items.Add("justificant", Nothing, AddressOf ShowFile)
                        oContextMenu.Items.Add("-")
                    End If

                    Dim oContact As New DTOContact(oControlItem.Tag.Guid)
                    Dim oMenu_Contact = New Menu_Contact(oContact, Nothing)
                    oContextMenu.Items.AddRange(oMenu_Contact.Range)
            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Async Sub ShowFile(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        Dim oCustomer As DTOGuidNom.Compact = CurrentParent(ControlItem.LinCods.Customer)
        Dim oSku As DTOGuidNom.Compact = CurrentParent(ControlItem.LinCods.Sku)
        Dim item = _Value.Items.FirstOrDefault(Function(x) x.CustomerGuid = oCustomer.Guid And x.ProductGuid = oSku.Guid)
        Dim src = Await FEB.InvRpt.Raport(exs, oCustomer.Guid, oSku.Guid, item.Fch)
        If exs.Count = 0 Then
            MsgBox(src, MsgBoxStyle.Information)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Select Case oCurrentControlItem.ExpandMode
                Case ControlItem.ExpandModes.Expanded
                    oCurrentControlItem.ExpandMode = ControlItem.ExpandModes.Collapsed
                    Dim idx = _ControlItems.IndexOf(oCurrentControlItem) + 1
                    Do While _ControlItems.Count > idx
                        If _ControlItems(idx).Level <= oCurrentControlItem.Level Then Exit Do
                        _ControlItems.RemoveAt(idx)
                    Loop
                Case ControlItem.ExpandModes.Collapsed
                    oCurrentControlItem.ExpandMode = ControlItem.ExpandModes.Expanded
                    Select Case oCurrentControlItem.LinCod
                        Case ControlItem.LinCods.Brand
                            InsertControlItems(CategoryItems())
                        Case ControlItem.LinCods.Category
                            InsertControlItems(SkuItems())
                        Case ControlItem.LinCods.Sku
                            InsertControlItems(CustomerItems())
                        Case ControlItem.LinCods.Customer
                            InsertControlItems(BrandItems())
                    End Select
                Case Else
            End Select
        End If
    End Sub

    Private Function CurrentLevel() As Integer
        Dim retval As Integer = 0
        Dim oControlitem = CurrentControlItem()
        If oControlitem IsNot Nothing Then
            retval = oControlitem.Level
        End If
        Return retval
    End Function

    Private Function CurrentParent(linCod As ControlItem.LinCods) As Object
        Dim retval As Object = Nothing
        Dim idx = _ControlItems.IndexOf(CurrentControlItem)
        For i As Integer = idx To 0 Step -1
            If _ControlItems(i).LinCod = linCod Then
                retval = _ControlItems(i).Tag
                Exit For
            End If
        Next
        Return retval
    End Function

    Private Sub InsertControlItems(oControlItems As ControlItems)
        Dim idx = _ControlItems.IndexOf(CurrentControlItem)
        For i As Integer = oControlItems.Count - 1 To 0 Step -1
            _ControlItems.Insert(idx + 1, oControlItems(i))
        Next
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Tag))
            SetContextMenu()
        End If
    End Sub

    Private Sub Xl_InvRpts_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles Me.CellPainting
        If e.RowIndex >= 0 And e.ColumnIndex = Cols.Nom Then
            Dim oRow = MyBase.Rows(e.RowIndex)
            Dim oControlitem As ControlItem = oRow.DataBoundItem
            Dim level = oControlitem.Level
            Dim padding = 10 * level
            e.CellStyle.Padding = New Padding(padding + 9, 0, 0, 0)
            Dim oClipRectangle As New Rectangle(e.ClipBounds.X + padding, e.ClipBounds.Y, e.ClipBounds.Width - padding, e.ClipBounds.Height)
            e.Paint(oClipRectangle, DataGridViewPaintParts.All)
            If oControlitem.ExpandMode <> ControlItem.ExpandModes.None Then
                Dim ico = oControlitem.Ico()
                e.Graphics.DrawImage(oControlitem.Ico(), e.CellBounds.Left + padding, e.CellBounds.Top + 1, ico.Width, ico.Height)
            End If
            e.Handled = True
        End If
    End Sub

    Protected Class ControlItem
        Property Tag As Object
        Property LinCod As LinCods

        Property Nom As String
        Property Qty As Integer
        Property Volume As Decimal
        Property Level As Integer
        Property ExpandMode As ExpandModes

        Public Enum ExpandModes
            None
            Expanded
            Collapsed
        End Enum

        Public Enum LinCods
            Total
            Brand
            Category
            Sku
            Customer
        End Enum

        Public Sub New(tag As Object, linCod As LinCods, nom As String, qty As Integer, volume As Decimal, level As Integer, oExpandMode As ExpandModes)
            MyBase.New()
            _Tag = tag
            _LinCod = linCod
            _Level = level
            _ExpandMode = oExpandMode
            Dim indentation = New String(" ", 3 + 3 * (_Level + 1))
            _Nom = nom ' String.Format("{0}{1}", indentation, nom)
            _Qty = qty
            _Volume = volume
        End Sub

        Private Function symbol() As String
            Dim retval As String = ""
            Select Case ExpandMode
                Case ExpandModes.Expanded
                    retval = "-"
                Case ExpandModes.Collapsed
                    retval = "+"
            End Select
            Return retval
        End Function
        Public Function Ico() As Image
            Dim retval As Image = Nothing
            Select Case ExpandMode
                Case ExpandModes.Expanded
                    retval = My.Resources.Expanded9
                Case ExpandModes.Collapsed
                    retval = My.Resources.Collapsed9
            End Select
            Return retval
        End Function


    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


