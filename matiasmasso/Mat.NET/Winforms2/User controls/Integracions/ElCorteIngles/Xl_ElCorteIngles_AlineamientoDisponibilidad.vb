Public Class Xl_ElCorteIngles_AlineamientoDisponibilidad
    Inherits DataGridView

    Private _Values As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item)
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToImportExcel(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)


    Private Enum Cols
        EciRef
        Dept
        Id
        PrvRef
        Ean
        Brand
        Category
        Sku
        Stock
        Descatalogat
        Price
    End Enum

    Public Shadows Sub Load(values As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item))
        _Values = values

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub


    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Function FilteredValues() As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item)
        If String.IsNullOrEmpty(_Filter) Then
            Return _Values
        Else
            Return _Values.Where(Function(x) x.Matches(_Filter)).ToList()
        End If
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

    Public ReadOnly Property Value As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item = oControlItem.Source
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
        With MyBase.Columns(Cols.EciRef)
            .HeaderText = "Ref Eci"
            .DataPropertyName = "EciRef"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dept)
            .HeaderText = "Dept"
            .DataPropertyName = "Dept"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Ref M+O"
            .DataPropertyName = "Id"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.PrvRef)
            .HeaderText = "Ref fabricant"
            .DataPropertyName = "PrvRef"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ean)
            .HeaderText = "Ean"
            .DataPropertyName = "Ean"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Brand)
            .HeaderText = "Marca comercial"
            .DataPropertyName = "BrandNom"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Category)
            .HeaderText = "Categoría"
            .DataPropertyName = "CategoryNom"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "SkuNom"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        '-------------------------- exclusive for files Alineamiento de Disponibilidad
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
        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.Descatalogat), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Price)
            .HeaderText = "Preu"
            .DataPropertyName = "Price"
            .Width = 70
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

    Private Function SelectedItems() As List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item)
        Dim retval As New List(Of DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item)
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
            'Dim oCustomerProduct = SelectedItems.First.CustomerProduct
            'Dim oMenu_CustomerProduct As New Menu_CustomerProduct(SelectedItems.First.CustomerProduct)
            'AddHandler oMenu_CustomerProduct.AfterUpdate, AddressOf RefreshRequest
            'oContextMenu.Items.AddRange(oMenu_CustomerProduct.Range)
            'Dim oMenuSku As New Menu_ProductSku(SelectedItems.First.CustomerProduct().Sku)
            'Dim item As ToolStripMenuItem = oContextMenu.Items.Add("Article")
            'oContextMenu.Items.Add(item)
            'Item.DropDownItems.AddRange(oMenuSku.Range)
            'oContextMenu.Items.Add("-")
        End If
        'oContextMenu.Items.Add("excel", Nothing, AddressOf Do_Excel)
        'oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        'oContextMenu.Items.Add("importar Excel ean/ref", Nothing, AddressOf Do_Import)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Import()
        RaiseEvent RequestToImportExcel(Me, MatEventArgs.Empty)
    End Sub


    Private Sub Do_Excel()
        'Dim oSheet = FEB.CustomerProducts.Excel(_Values)
        'Dim exs As New List(Of Exception)
        'If Not UIHelper.ShowExcel(oSheet, exs) Then
        'UIHelper.WarnError(exs)
        'End If
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item = CurrentControlItem.Source
            'Dim oFrm As New Frm_CustomerProduct(oSelectedValue.CustomerProduct())
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

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.Descatalogat
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim item = oControlItem.Source
                If item.Descatalogado = "S" Then e.Value = My.Resources.aspa Else e.Value = My.Resources.vb
        End Select
    End Sub

    Protected Class ControlItem
        Property Source As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item

        Property BrandNom As String
        Property CategoryNom As String
        Property SkuNom As String
        Property Id As String
        Property EciRef As String
        Property PrvRef As String
        Property Ean As String
        Property Dept As String
        Property Stock As Integer
        Property Price As Decimal

        Public Sub New(value As DTO.Integracions.ElCorteIngles.AlineamientoDisponibilidad.Item)
            MyBase.New()
            _Source = value
            Dim oSku = FEB.GlobalVariables.Cache(Current.Session.Emp).FindSku(DTOEan.Factory(value.Ean))
            With value
                If oSku IsNot Nothing Then

                    _BrandNom = DTOProductSku.BrandNom(oSku)
                    _CategoryNom = DTOProductSku.CategoryNom(oSku)
                    _SkuNom = DTOProductSku.SkuNom(oSku)
                    If oSku.Id <> 0 Then _Id = oSku.Id
                    _PrvRef = oSku.RefProveidor
                    _Ean = DTOEan.eanValue(oSku.Ean13)
                End If
                _Stock = .Stock
                _Price = .Price
                _Dept = .Uneco
                _EciRef = .RefEci
                '_EciRef = .Ref
                'If .Dept IsNot Nothing Then
                ' _Dept = .Dept.Nom
                ' End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class



