Public Class Xl_Inventory

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductSku)
    Private _Mode As Modes
    Private _Lang As DTOLang
    Private _DefaultValue As DTOProductSku
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Modes
        NotSet
        Brands
        Categories
        Skus
    End Enum

    Private Enum Cols
        id
        nom
        stk
        pmc
        amt
    End Enum

    Public Shadows Sub Load(oMode As Modes, values As List(Of DTOProductSku), Optional oDefaultValue As DTOProductSku = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        _Mode = oMode
        _Lang = Current.Session.Lang
        _Values = values
        _SelectionMode = oSelectionMode

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
        Dim oTotalItem As New ControlItem(_Mode, _Values, _Lang)
        _ControlItems.Add(oTotalItem)

        Dim query As Object = DataSource()
        For Each oItem In query
            Dim oControlItem As New ControlItem(_Mode, oItem)
            Select Case _Mode
                Case Modes.Brands, Modes.Categories
                    oTotalItem.Amt += oItem.amt
                Case Modes.Skus
                    Dim oSku As DTOProductSku = oItem
                    oTotalItem.Amt += oSku.Stock * oSku.Pmc
            End Select
            _ControlItems.Add(oControlItem)
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        If oCell Is Nothing Then
            If _ControlItems.Count > 0 Then
                MyBase.CurrentCell = MyBase.Rows(1).Cells(Cols.nom)
            End If
        Else
            UIHelper.SetDataGridviewCurrentCell(Me, oCell)
        End If

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Private Shadows Function DataSource() As Object
        Dim retval As Object = Nothing
        Select Case _Mode
            Case Modes.Brands
                retval = _Values.
                   GroupBy(Function(g) New With {Key g.Category.Brand.Guid, g.Category.Brand.Nom}).
                   Select(Function(group) New With {
                      .Guid = group.Key.Guid,
                      .Nom = group.Key.Nom,
                      .Amt = group.Sum(Function(a) a.Stock * a.Pmc)
                      })
            Case Modes.Categories
                retval = _Values.
                   GroupBy(Function(g) New With {Key g.Category.Guid, g.Category.Nom}).
                   Select(Function(group) New With {
                      .Guid = group.Key.Guid,
                      .Nom = group.Key.Nom,
                      .Amt = group.Sum(Function(a) a.Stock * a.Pmc)
                      })
            Case Modes.Skus
                retval = _Values

        End Select
        Return retval
    End Function
    Public ReadOnly Property Value As Object
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As Object = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowProductSku.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.id)
            .HeaderText = "Id"
            .DataPropertyName = "Id"
            .Width = 50
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Format = "#"
            .Visible = _Mode = Modes.Skus
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.nom)
            .HeaderText = "Nom"
            .DataPropertyName = "Nom"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.stk)
            .HeaderText = "Quant"
            .DataPropertyName = "Qty"
            .Width = 40
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.pmc)
            .HeaderText = "Preu"
            .DataPropertyName = "Pmc"
            .Width = 70
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###0.00 €;-#,###0.00 €;#"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.amt)
            .HeaderText = "Inventari"
            .DataPropertyName = "amt"
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
            Select Case _Mode
                Case Modes.Brands
                    Dim oBrand As DTOProductBrand = oControlItem.Source
                    If oBrand IsNot Nothing Then
                        Dim oMenu_Brand As New Menu_ProductBrand(oBrand)
                        oContextMenu.Items.AddRange(oMenu_Brand.Range)
                    End If
                Case Modes.Categories
                    Dim oCategory As DTOProductCategory = oControlItem.Source
                    If oCategory IsNot Nothing Then
                        Dim oMenu_Category As New Menu_ProductCategory(oCategory)
                        oContextMenu.Items.AddRange(oMenu_Category.Range)
                    End If
                Case Modes.Skus
                    Dim oSku As DTOProductSku = oControlItem.Source
                    If oSku IsNot Nothing Then
                        Dim oMenu_Sku As New Menu_ProductSku(oSku)
                        oContextMenu.Items.AddRange(oMenu_Sku.Range)
                    End If
            End Select

        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oControlItem As ControlItem = CurrentControlItem()
        If oControlItem IsNot Nothing Then
            Select Case _Mode
                Case Modes.Brands
                    Dim oBrand As DTOProductBrand = oControlItem.Source
                    Dim oFrm As New Frm_Tpa(oBrand)
                    oFrm.Show()
                Case Modes.Categories
                    Dim oCategory As DTOProductCategory = oControlItem.Source
                    Dim oFrm As New Frm_Stp(oCategory, DTO.Defaults.SelectionModes.Browse)
                    oFrm.Show()
                Case Modes.Skus
                    Dim oSku As DTOProductSku = oControlItem.Source
                    Dim oFrm As New Frm_Art(oSku)
                    oFrm.Show()
            End Select

        End If
    End Sub


    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            If CurrentControlItem() IsNot Nothing Then
                RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
                SetContextMenu()
            End If
        End If
    End Sub

    Private Sub Xl_InventoryDelta_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        If e.RowIndex = 0 Then
            Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
            oRow.DefaultCellStyle.BackColor = Color.LightBlue
        End If
    End Sub

    Protected Class ControlItem
        Property Source As DTOProduct

        Property Id As String
        Property Nom As String
        Property Qty As Integer
        Property Pmc As Decimal
        Property Amt As Decimal

        Public Sub New(oMode As Modes, item As Object)
            MyBase.New()
            Select Case oMode
                Case Modes.Brands
                    _Source = New DTOProductBrand(item.guid)
                    _Source.Nom = item.nom
                    _Nom = _Source.nom.Tradueix(Current.Session.Lang)
                    _Amt = item.amt
                Case Modes.Categories
                    _Source = New DTOProductCategory(item.guid)
                    _Source.Nom = item.nom
                    _Nom = _Source.nom.Tradueix(Current.Session.Lang)
                    _Amt = item.amt
                Case Modes.Skus
                    _Source = item
                    With DirectCast(_Source, DTOProductSku)
                        _Id = .Id
                        _Nom = .Nom.Tradueix(Current.Session.Lang)
                        _Qty = .Stock
                        _Pmc = .Pmc
                        _Amt = .Stock * .Pmc
                    End With
            End Select
        End Sub

        Public Sub New(oMode As Modes, values As List(Of DTOProductSku), oLang As DTOLang)
            MyBase.New()
            Select Case oMode
                Case Modes.Brands
                    _Nom = String.Format("total")
                Case Modes.Categories
                    If values.Count > 0 Then
                        Dim oBrand As DTOProductBrand = values.First.Category.Brand
                        _Nom = String.Format("total {0}", oBrand.Nom.Tradueix(Current.Session.Lang))
                    End If
                Case Modes.Skus
                    If values.Count > 0 Then
                        Dim oCategory As DTOProductCategory = values.First.Category
                        _Nom = String.Format("total {0} {1}", oCategory.Brand.Nom.Tradueix(Current.Session.Lang), oCategory.Nom.Tradueix(Current.Session.Lang))
                    End If
            End Select
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


