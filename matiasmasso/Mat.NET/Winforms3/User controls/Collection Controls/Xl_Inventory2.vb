Public Class Xl_Inventory2

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductSku)
    Private _Mode As DTOProduct.SourceCods
    Private _Fch As Date
    Private _Deadline1 As Integer
    Private _Deadline2 As Integer

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Nom
        LastIn
        Stock
        Cost
        Days
        Inventory
        Obsolets
    End Enum


    Public Shadows Sub Load(values As List(Of DTOProductSku), Mode As DTOProduct.SourceCods, DtFch As Date, Deadline1 As Integer, Deadline2 As Integer)
        _Values = values
        _Mode = Mode
        _Fch = DtFch
        _Deadline1 = Deadline1
        _Deadline2 = Deadline2

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
        Dim oTotalsItem As New ControlItem(Nothing, "Totals", 0, 0)
        _ControlItems.Add(oTotalsItem)

        Select Case _Mode
            Case DTOProduct.SourceCods.Brand
                Dim oFilteredValues = _Values.GroupBy(Function(g) New With {Key g.Category.Brand, Key g.Category.Brand.Nom}).
                    Select(Function(group) New With {.tag = group.Key.Brand, .Nom = group.Key.Nom, .Inventory = group.Sum(Function(y) Inventory(y)), .Obsolets = group.Sum(Function(z) Obsolets(z))})
                For Each oItem In oFilteredValues
                    Dim oControlItem As New ControlItem(oItem.tag, oItem.Nom.Tradueix(Current.Session.Lang), oItem.Inventory, oItem.Obsolets)
                    oTotalsItem.Inventory += oItem.Inventory
                    oTotalsItem.Obsolets += oItem.Obsolets
                    _ControlItems.Add(oControlItem)
                Next
            Case DTOProduct.SourceCods.Category
                Dim oFilteredValues = _Values.GroupBy(Function(g) New With {Key g.Category, Key g.Category.Nom}).
                    Select(Function(group) New With {.tag = group.Key.Category, .Nom = group.Key.Nom, .Inventory = group.Sum(Function(y) Inventory(y)), .Obsolets = group.Sum(Function(z) Obsolets(z))})
                For Each oItem In oFilteredValues
                    Dim oControlItem As New ControlItem(oItem.tag, oItem.Nom.Tradueix(Current.Session.Lang), oItem.Inventory, oItem.Obsolets)
                    oTotalsItem.Inventory += oItem.Inventory
                    oTotalsItem.Obsolets += oItem.Obsolets
                    _ControlItems.Add(oControlItem)
                Next
            Case DTOProduct.SourceCods.Sku
                Dim oFilteredValues As IEnumerable(Of DTOProductSku) = Nothing
                oFilteredValues = _Values
                For Each oItem In oFilteredValues
                    Dim DcInventory = Inventory(oItem)
                    Dim DcObsolets = Obsolets(oItem)
                    Dim oControlItem As New ControlItem(oItem, oItem.nom.Tradueix(Current.Session.Lang), DcInventory, DcObsolets, _Fch)
                    oTotalsItem.Inventory += DcInventory
                    oTotalsItem.Obsolets += DcObsolets
                    _ControlItems.Add(oControlItem)
                Next
        End Select


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Function Inventory(oSku As DTOProductSku) As Decimal
        Dim retval As Decimal
        Select Case DateDiff(DateInterval.Day, oSku.LastPurchaseDate, _Fch)
            Case < _Deadline1
                retval = oSku.Stock * oSku.Pmc
            Case > _Deadline2
            Case Else
                retval = oSku.Stock * oSku.Pmc / 2
        End Select
        Return retval
    End Function

    Private Function Obsolets(oSku As DTOProductSku) As Decimal
        Dim retval As Decimal
        Select Case DateDiff(DateInterval.Day, oSku.LastPurchaseDate, _Fch)
            Case < _Deadline1
            Case > _Deadline2
                retval = oSku.Stock * oSku.Pmc
            Case Else
                retval = oSku.Stock * oSku.Pmc / 2
        End Select
        Return retval
    End Function

    Public ReadOnly Property Value As DTOProduct
        Get
            Dim retval As DTOProduct = Nothing
            Dim oControlItem As ControlItem = CurrentControlItem()
            If oControlItem IsNot Nothing Then
                retval = oControlItem.Source
            End If
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowProduct.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.LastIn)
            .HeaderText = "Ultima compra"
            .DataPropertyName = "LastIn"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy"
            .Visible = _Mode = DTOProduct.SourceCods.Sku
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
            .Visible = _Mode = DTOProduct.SourceCods.Sku
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Cost)
            .HeaderText = "Cost mig"
            .DataPropertyName = "Cost"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
            .Visible = _Mode = DTOProduct.SourceCods.Sku
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Days)
            .HeaderText = "Dies"
            .DataPropertyName = "Days"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
            .Visible = _Mode = DTOProduct.SourceCods.Sku
        End With

        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Inventory)
            .HeaderText = "Inventari"
            .DataPropertyName = "Inventory"
            .Width = 80
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Obsolets)
            .HeaderText = "Obsolets"
            .DataPropertyName = "Obsolets"
            .Width = 80
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

    Private Function SelectedItems() As List(Of DTOProduct)
        Dim retval As New List(Of DTOProduct)
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

        If oControlItem IsNot Nothing AndAlso oControlItem.Source IsNot Nothing Then
            Dim oMenu_Product As New Menu_Product(SelectedItems.First)
            AddHandler oMenu_Product.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Product.Range)
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
            Dim oSelectedValue As DTOProduct = CurrentControlItem.Source
            Dim oFrm As New Frm_Art(oSelectedValue)
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

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles MyBase.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem

        If oControlItem.Source Is Nothing Then
            oRow.DefaultCellStyle.BackColor = Color.LightBlue
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOProduct

        Property Nom As String
        Property LastIn As Date
        Property Stock As Integer
        Property Cost As Decimal
        Property Days As Integer
        Property Inventory As Decimal
        Property Obsolets As Decimal

        Public Sub New(tag As DTOProduct, nom As String, DcInventory As Decimal, DcObsolets As Decimal, Optional DtFch As Date = Nothing)
            MyBase.New()
            _Source = tag
            _Nom = nom
            _Inventory = DcInventory
            _Obsolets = DcObsolets

            If TypeOf tag Is DTOProductSku Then
                Dim oSku As DTOProductSku = tag
                _LastIn = oSku.LastPurchaseDate
                _Stock = oSku.Stock
                _Cost = oSku.Pmc
                _Days = DateDiff(DateInterval.Day, _LastIn, DtFch)
            End If
        End Sub


    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


