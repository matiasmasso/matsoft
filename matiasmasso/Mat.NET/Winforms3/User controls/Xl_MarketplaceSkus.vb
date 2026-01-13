Public Class Xl_MarketplaceSkus
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As DTOMarketPlace
    Private _Cache As Models.ClientCache
    Private _showDisabled As Boolean = False
    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event CheckChanged(sender As Object, e As MatEventArgs)
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Private Enum Cols
        Check
        Brand
        Category
        SkuNom
        Id
        Ean
        Pvp
        Stock
        IcoText
        IcoImg
    End Enum

    Public Shadows Sub Load(value As DTOMarketPlace, cache As Models.ClientCache)
        _Value = value
        _Cache = cache
        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Public Sub ShowDisabled(value As Boolean)
        _showDisabled = value
        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem In oFilteredValues
            Dim oControlItem As New ControlItem(oItem, _Cache)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function FilteredValues() As List(Of DTOMarketplaceSku)
        Dim retval As List(Of DTOMarketplaceSku)
        If _Filter = "" Then
            retval = _Value.Catalog
        Else
            retval = _Value.Catalog.FindAll(Function(x) x.Sku.Nom.ToLower.IndexOf(_Filter.ToLower) >= 0)
        End If
        If Not _showDisabled Then
            retval = retval.Where(Function(x) x.Enabled = True).ToList()
        End If
        Return retval
    End Function


    Public Property Filter As String
        Get
            Return _Filter
        End Get
        Set(value As String)
            _Filter = value
            If _Value IsNot Nothing Then Refresca()
        End Set
    End Property

    Public Sub ClearFilter()
        If _Filter > "" Then
            _Filter = ""
            Refresca()
        End If
    End Sub

    'Public ReadOnly Property Value As List(Of DTOMarketplaceSku)
    '    Get
    '        Dim oControlItem As ControlItem = CurrentControlItem()
    '        Dim retval = oControlItem.Source
    '        Return retval
    '    End Get
    'End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.35
        'MyBase.RowRol.DefaultCellStyle.BackColor = Color.Transparent

        MyBase.AutoGenerateColumns = False
        MyBase.Columns.Clear()

        MyBase.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        MyBase.ColumnHeadersVisible = True
        MyBase.RowHeadersVisible = False
        MyBase.MultiSelect = True
        MyBase.AllowUserToResizeRows = False
        MyBase.AllowDrop = False
        MyBase.ReadOnly = False

        MyBase.Columns.Add(New DataGridViewCheckBoxColumn(False))
        With DirectCast(MyBase.Columns(Cols.Check), DataGridViewCheckBoxColumn)
            .HeaderText = ""
            .DataPropertyName = "Checked"
            .Width = 20
            .DefaultCellStyle.SelectionBackColor = Color.White
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Brand)
            .HeaderText = "Marca"
            .DataPropertyName = "Marca"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Category)
            .HeaderText = "Categoria"
            .DataPropertyName = "Category"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 100
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.SkuNom)
            .HeaderText = "Producte"
            .DataPropertyName = "SkuNom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Id)
            .HeaderText = "Id"
            .DataPropertyName = "Id"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 80
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ean)
            .HeaderText = "Ean"
            .DataPropertyName = "Ean"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 80
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pvp)
            .HeaderText = "Pvp"
            .DataPropertyName = "Pvp"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .ReadOnly = True
            .DefaultCellStyle.Format = "#,##0.00 €;-#,###0.00 €;#"
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Stock)
            .HeaderText = "Stock"
            .DataPropertyName = "Stock"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,##0;-#,##0;#"
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.IcoText), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
        End With

        MyBase.Columns.Add(New DataGridViewImageColumn(False))
        With DirectCast(MyBase.Columns(Cols.IcoImg), DataGridViewImageColumn)
            .CellTemplate = New DataGridViewImageCellBlank(False)
            .HeaderText = ""
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 16
            .DefaultCellStyle.NullValue = Nothing
            .ReadOnly = True
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

    Private Function SelectedItems() As List(Of DTOMarketplaceSku)
        Dim retval As New List(Of DTOMarketplaceSku)
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
            Dim oMenu As New Menu_MarketplaceSku(SelectedItems)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            AddHandler oMenu.EnabledChanged, AddressOf OnEnabledChange
            oContextMenu.Items.AddRange(oMenu.Range)
            oContextMenu.Items.Add("-")
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub OnEnabledChange(sender As Object, e As MatEventArgs)
        Dim oChangedItems As List(Of DTOMarketplaceSku) = e.Argument
        For Each oItem In oChangedItems
            Dim oControlItem = _ControlItems.FirstOrDefault(Function(x) x.Source.Sku.Guid.Equals(oItem.Sku.Guid))
            If oControlItem IsNot Nothing Then
                oControlItem.Checked = oItem.Enabled
            End If
        Next
        MyBase.Refresh()
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOMarketplaceSku = CurrentControlItem.Source
            Dim oFrm As New Frm_MarketplaceSku(oSelectedValue)
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

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles MyBase.CellFormatting
        Select Case e.ColumnIndex
            Case Cols.IcoText
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem = oControlItem.Source
                If oItem.HasTxt Then
                    e.Value = My.Resources.text_16
                End If
            Case Cols.IcoImg
                Dim oRow As DataGridViewRow = DirectCast(sender, DataGridView).Rows(e.RowIndex)
                Dim oControlItem As ControlItem = oRow.DataBoundItem
                Dim oItem = oControlItem.Source
                If oItem.HasImg Then
                    e.Value = My.Resources.img_16
                End If
        End Select
    End Sub

#Region "Check"


    Private Async Sub DataGridView1_CellValueChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles MyBase.CellValueChanged
        If _AllowEvents Then
            Select Case e.ColumnIndex
                Case Cols.Check
                    Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
                    Dim oControlItem As ControlItem = oRow.DataBoundItem

                    Dim oCurrentCheckState As CheckState = oControlItem.Checked
                    Dim oNewCheckState As CheckState = IIf(oControlItem.Checked = CheckState.Checked, CheckState.Unchecked, CheckState.Checked)
                    Dim oArgs As New ItemCheckEventArgs(e.RowIndex, oNewCheckState, oCurrentCheckState)

                    Dim exs As New List(Of Exception)
                    Dim item = oControlItem.Source
                    item.Enabled = Not item.Enabled

                    Dim items As New List(Of DTOMarketplaceSku)
                    items.Add(item)
                    If Not Await FEB.MarketPlace.EnableSkus(exs, items, item.Enabled) Then
                        item.Enabled = Not item.Enabled
                        MyBase.Refresh()
                        UIHelper.WarnError(exs)
                    End If
                    oControlItem.Checked = item.Enabled

                    RaiseEvent AfterUpdate(sender, New MatEventArgs(CurrentControlItem.Source))
            End Select
        End If
    End Sub

    Private Sub DataGridView1_CurrentCellDirtyStateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.CurrentCellDirtyStateChanged
        'provoca CellValueChanged a cada clic sense sortir de la casella
        Select Case MyBase.CurrentCell.ColumnIndex
            Case Cols.Check
                MyBase.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End Select
    End Sub


#End Region



    Protected Class ControlItem
        Property Checked As Boolean

        Property Source As DTOMarketplaceSku
        Property Marca As String
        Property Category As String
        Property SkuNom As String
        Property Id As String
        Property Ean As String
        Property Pvp As Decimal
        Property Stock As Integer


        Public Sub New(value As DTOMarketplaceSku, cache As Models.ClientCache)
            MyBase.New()
            Dim lang = Current.Session.Lang
            _Source = value
            Dim oSku = cache.FindSku(value.Sku.Guid)
            Dim stk = cache.SkuStock(value.Sku.Guid)
            With value
                _Checked = .Enabled
                _SkuNom = oSku.NomLlarg.Tradueix(lang)
                _Id = .CustomId
                _Ean = DTOEan.eanValue(oSku.Ean13)
                _Marca = oSku.Category.Brand.Nom.Tradueix(lang)
                _Category = oSku.Category.Nom.Tradueix(lang)
                _Pvp = cache.RetailPrice(value.Sku.Guid)
                If stk IsNot Nothing Then
                    _Stock = stk.StockAvailable()
                End If
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


