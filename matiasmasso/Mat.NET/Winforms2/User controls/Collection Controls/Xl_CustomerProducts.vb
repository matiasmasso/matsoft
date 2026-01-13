Public Class Xl_CustomerProducts

    Inherits DataGridView

    Private _Values As List(Of DTOCustomerProduct)
    Private _DefaultValue As DTOCustomerProduct
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _Modalitat As Modalitats
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToRefresh(sender As Object, e As MatEventArgs)
    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToImportExcel(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Modalitats
        PerCustomer
        PerSku
    End Enum

    Private Enum Cols
        Customer
        Brand
        Category
        Sku
        Ref
        Dun14
        Dsc
        Color
    End Enum

    Public Shadows Sub Load(values As List(Of DTOCustomerProduct), oModalitat As Modalitats, Optional oDefaultValue As DTOCustomerProduct = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Values = values
        _SelectionMode = oSelectionMode
        _Modalitat = oModalitat

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub


    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOCustomerProduct) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOCustomerProduct In oFilteredValues
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next

        MyBase.DataSource = _ControlItems
        If _ControlItems.Count > 0 Then
            MyBase.CurrentCell = MyBase.FirstDisplayedCell
        End If

        If _DefaultValue IsNot Nothing Then
            Dim oControlItem As ControlItem = _ControlItems.ToList.Find(Function(x) x.Source.Equals(_DefaultValue))
            Dim rowIdx As Integer = _ControlItems.IndexOf(oControlItem)
            If rowIdx >= 0 Then
                MyBase.CurrentCell = MyBase.Rows(rowIdx).Cells(Cols.Ref)
            End If
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Function FilteredValues() As List(Of DTOCustomerProduct)
        Dim retval As List(Of DTOCustomerProduct)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.ref.Contains(_Filter.ToLower) Or x.sku.nomLlarg.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOCustomerProduct
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOCustomerProduct = oControlItem.Source
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
        With MyBase.Columns(Cols.Customer)
            .HeaderText = "Client"
            .DataPropertyName = "Customer"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = _Modalitat = Modalitats.PerSku
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Brand)
            .HeaderText = "Marca comercial"
            .DataPropertyName = "Brand"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = _Modalitat = Modalitats.PerCustomer
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Category)
            .HeaderText = "Categoría"
            .DataPropertyName = "Category"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = _Modalitat = Modalitats.PerCustomer
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = _Modalitat = Modalitats.PerCustomer
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ref)
            .HeaderText = "Referencia"
            .DataPropertyName = "Ref"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dun14)
            .HeaderText = "DUN 14"
            .DataPropertyName = "Dun14"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Visible = _Values.Any(Function(x) x.DUN14.isNotEmpty())
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dsc)
            .HeaderText = "Descripció"
            .DataPropertyName = "Dsc"
            .MinimumWidth = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = _Values.Any(Function(x) x.Dsc.isNotEmpty())
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Color)
            .HeaderText = "Color"
            .DataPropertyName = "Color"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Visible = _Values.Any(Function(x) x.Color.isNotEmpty())
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

    Private Function SelectedItems() As List(Of DTOCustomerProduct)
        Dim retval As New List(Of DTOCustomerProduct)
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
            Dim oMenu_CustomerProduct As New Menu_CustomerProduct(SelectedItems.First)
            AddHandler oMenu_CustomerProduct.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_CustomerProduct.Range)
            Dim oMenuSku As New Menu_ProductSku(SelectedItems.First.Sku)
            Dim item As ToolStripMenuItem = oContextMenu.Items.Add("Article")
            oContextMenu.Items.Add(item)
            item.DropDownItems.AddRange(oMenuSku.Range)
            oContextMenu.Items.Add("-")
        End If
        oContextMenu.Items.Add("excel", Nothing, AddressOf Do_Excel)
        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        oContextMenu.Items.Add("importar Excel ean/ref", Nothing, AddressOf Do_Import)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Import()
        RaiseEvent RequestToImportExcel(Me, MatEventArgs.Empty)
    End Sub


    Private Sub Do_Excel()
        Dim oSheet = FEB.CustomerProducts.Excel(_Values)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOCustomerProduct = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTO.Defaults.SelectionModes.Browse
                    Dim oFrm As New Frm_CustomerProduct(oSelectedValue)
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

    Private Sub RefreshRequest()
        RaiseEvent RequestToRefresh(Me, MatEventArgs.Empty)
    End Sub

    Protected Class ControlItem
        Property Source As DTOCustomerProduct

        Property Customer As String
        Property Ref As String
        Property Brand As String
        Property Category As String
        Property Sku As String
        Property Dun14 As String
        Property Dsc As String
        Property Color As String

        Public Sub New(value As DTOCustomerProduct)
            MyBase.New()
            _Source = value
            With value
                _Customer = .Customer.FullNom
                _Ref = .Ref
                _Brand = .Sku.BrandNom()
                _Category = .Sku.CategoryNom()
                _Sku = .sku.nom.Tradueix(Current.Session.Lang)
                _Dun14 = .DUN14
                _Dsc = .Dsc
                _Color = .Color
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


