Public Class Xl_Marges
    Inherits _Xl_ReadOnlyDatagridview

    Private _Value As Models.MarginsModel
    Private _Mode As Modes
    Private _Target As Object
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Alb
        Fch
        Qty
        Eur
        Dto
        Pmc
        Nom
        Sales
        Cost
        Bnf
        Mrg
    End Enum

    Public Enum Modes
        NotSet
        Brands
        Categories
        Skus
        Items
    End Enum

    Public ReadOnly Property Mode As Modes
        Get
            Return _Mode
        End Get
    End Property

    Public ReadOnly Property SelectedItem As Object
        Get
            Dim retval As Object = Nothing
            Dim oControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = CurrentControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Public Shadows Sub Load(value As Models.MarginsModel, mode As Modes, Optional target As Object = Nothing)
        _Value = value
        _Mode = mode
        _Target = target

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        Dim exs As New List(Of Exception)
        _AllowEvents = False

        _ControlItems = New ControlItems
        If _Value.Brands.Count > 0 Then
            Select Case _Mode
                Case Modes.Brands
                    Dim oTotals As New ControlItem(Nothing, String.Format("Total {0}", GlobalVariables.Emp.Org.Nom), _Value.Purchases, _Value.Sales)
                    _ControlItems.Add(oTotals)
                    For Each oItem In _Value.Brands
                        Dim oControlItem As New ControlItem(oItem, oItem.Nom, oItem.Purchases, oItem.Sales)
                        _ControlItems.Add(oControlItem)
                    Next
                Case Modes.Categories
                    Dim oBrand = CType(_Target, Models.MarginsModel.Brand)
                    Dim oTotals As New ControlItem(Nothing, String.Format("Total {0}", oBrand.Nom), oBrand.Purchases, oBrand.Sales)
                    _ControlItems.Add(oTotals)
                    For Each oItem In oBrand.Categories
                        Dim oControlItem As New ControlItem(oItem, oItem.Nom, oItem.Purchases, oItem.Sales)
                        _ControlItems.Add(oControlItem)
                    Next
                Case Modes.Skus
                    Dim oCategory = CType(_Target, Models.MarginsModel.Category)
                    Dim oTotals As New ControlItem(Nothing, String.Format("Total {0}", oCategory.Nom), oCategory.Purchases, oCategory.Sales)
                    _ControlItems.Add(oTotals)
                    For Each oItem In oCategory.Skus
                        Dim oControlItem As New ControlItem(oItem, oItem.Nom, oItem.Purchases, oItem.Sales)
                        _ControlItems.Add(oControlItem)
                    Next
                Case Modes.Items
                    Dim oSku = CType(_Target, Models.MarginsModel.Sku)
                    Dim oTotals As New ControlItem(Nothing, String.Format("Total {0}", oSku.Nom), Nothing, oSku.Qty(), oSku.CostAverage(), oSku.SalePriceAverage, oSku.DtoAverage)
                    _ControlItems.Add(oTotals)
                    For Each oItem In oSku.Items
                        Dim oControlItem As New ControlItem(oItem, oItem.Alb, oItem.Fch, oItem.Qty, oItem.Pmc, oItem.Eur, oItem.Dto)
                        _ControlItems.Add(oControlItem)
                    Next
            End Select
        End If

        MyBase.DataSource = _ControlItems
        If MyBase.Rows.Count > 1 Then
            Dim oSelectedRow As DataGridViewRow = MyBase.Rows(1) 'salta la de totals
            oSelectedRow.Frozen = True
            MyBase.CurrentCell = oSelectedRow.Cells(Cols.Mrg)
            SetContextMenu()
        End If

        _AllowEvents = True
    End Sub


    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowDeliveryItem.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Alb)
            .HeaderText = "Albarà"
            .DataPropertyName = "Alb"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            '.DefaultCellStyle.Format = "#"
            .Visible = _Mode = Modes.Items
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
            .Visible = _Mode = Modes.Items
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
            .Visible = _Mode = Modes.Items
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Eur)
            .HeaderText = "Preu"
            .DataPropertyName = "Eur"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .Visible = _Mode = Modes.Items
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Dto)
            .HeaderText = "Dto"
            .DataPropertyName = "Dto"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
            .Visible = _Mode = Modes.Items
        End With


        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Pmc)
            .HeaderText = "Cost mig"
            .DataPropertyName = "Pmc"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .Visible = _Mode = Modes.Items
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Nom)
            Select Case _Mode
                Case Modes.Brands
                    .HeaderText = "Marques comercials"
                Case Modes.Categories
                    .HeaderText = "Categories"
            End Select
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = _Mode <> Modes.Items
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sales)
            .HeaderText = "Vendes"
            .DataPropertyName = "Sales"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cost)
            .HeaderText = "Compres"
            .DataPropertyName = "Purchases"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Bnf)
            .HeaderText = "Benefici brut"
            .DataPropertyName = "GrossProfit"
            .Width = 90
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Mrg)
            .HeaderText = "Marge"
            .DataPropertyName = "Margin"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#\%;-#\%;#"
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
        Dim exs As New List(Of Exception)
        Dim oContextMenu As New ContextMenuStrip
        Dim oControlItem As ControlItem = CurrentControlItem()

        If oControlItem IsNot Nothing AndAlso oControlItem.Source IsNot Nothing Then
            Select Case _Mode
                Case Modes.Brands
                    Dim oBrand As New DTOProductBrand(oControlItem.Source.Guid)
                    If oBrand IsNot Nothing Then
                        Dim oMenu_Brand As New Menu_ProductBrand(oBrand)
                        oContextMenu.Items.AddRange(oMenu_Brand.Range)
                    End If
                Case Modes.Categories
                    Dim oCategory As New DTOProductCategory(oControlItem.Source.Guid)
                    If oCategory IsNot Nothing Then
                        Dim oMenu_Category As New Menu_ProductCategory(oCategory)
                        oContextMenu.Items.AddRange(oMenu_Category.Range)
                    End If
                Case Modes.Skus
                    Dim oSku As New DTOProductSku(oControlItem.Source.Guid)
                    If oSku IsNot Nothing Then
                        Dim oMenu_Sku As New Menu_ProductSku(oSku)
                        oContextMenu.Items.AddRange(oMenu_Sku.Range)
                    End If
                Case Modes.Items
                    Dim oItem As Models.MarginsModel.Item = oControlItem.Source
                    If oItem IsNot Nothing Then
                        Dim oDelivery As New DTODelivery(oItem.AlbGuid)
                        Dim oDeliveries = {oDelivery}.ToList()
                        Dim oMenu_Delivery As New Menu_Delivery(oDeliveries)
                        oContextMenu.Items.AddRange(oMenu_Delivery.Range)
                    End If
            End Select
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
            RaiseEvent ValueChanged(Me, New MatEventArgs(SelectedItem()))
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem

        If oControlItem.Source Is Nothing Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.WhiteSmoke
        End If
    End Sub


    Protected Class ControlItem
        Property Source As Object

        Property Nom As String

        Property Alb As String
        Property Fch As Date
        Property Qty As Integer
        Property Pmc As Decimal
        Property Eur As Decimal
        Property Dto As Decimal
        Property Purchases As Decimal
        Property Sales As Decimal
        Property GrossProfit As Decimal
        Property Margin As Decimal

        Public Sub New(oSource As Object, nom As String, purchases As Decimal, sales As Decimal)
            _Source = oSource
            _Nom = nom
            _Purchases = purchases
            _Sales = sales
            _GrossProfit = _Sales - _Purchases
            If _Purchases <> 0 Then
                _Margin = 100 * (_Sales / _Purchases - 1)
            End If
        End Sub

        Public Sub New(oSource As Models.MarginsModel.Item, alb As String, fch As Nullable(Of Date), qty As Integer, pmc As Decimal, eur As Decimal, dto As Decimal)
            _Source = oSource
            _Alb = alb
            If fch IsNot Nothing Then
                _Fch = fch
            End If
            _Qty = qty
            _Pmc = pmc
            _Eur = eur
            _Dto = dto
            _Purchases = _Qty * _Pmc
            _Sales = Math.Round(_Qty * _Eur * (100 - _Dto) / 100, 2)
            _GrossProfit = _Sales - _Purchases
            If _Purchases <> 0 Then
                _Margin = 100 * (_Sales / _Purchases - 1)
            End If
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class

