Public Class Xl_ProductSkus

    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOProductSku)
    Private _DefaultValue As DTOProductSku
    Private _DisplayMode As DisplayModes
    Private _SelectionMode As DTOProduct.SelectionModes

    Private _Filter As String
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean


    Public Event RequestToAddNew(sender As Object, e As MatEventArgs)
    Public Event RequestToToggleObsoletos(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Enum Cols
        Brand
        Category
        Sku
    End Enum

    Public Enum DisplayModes
        Sku
        BrandCategorySku
    End Enum

    Public Shadows Sub Load(values As List(Of DTOProductSku), Optional oSelectionMode As DTOProduct.SelectionModes = DTOProduct.SelectionModes.Browse, Optional IncludeNullValue As Boolean = False, Optional oDefaultValue As DTOProductSku = Nothing, Optional oDisplayMode As DisplayModes = DisplayModes.Sku, Optional DisplayObsoletos As Boolean = False)
        _Values = values
        _SelectionMode = oSelectionMode
        _DisplayMode = oDisplayMode
        _DefaultValue = oDefaultValue
        MyBase.DisplayObsolets = DisplayObsoletos

        Static PropertiesSet As Boolean
        If Not PropertiesSet Then
            SetProperties()
            PropertiesSet = True
        End If

        Refresca()
    End Sub

    Private Sub Refresca()
        _AllowEvents = False
        Dim oFilteredValues As List(Of DTOProductSku) = FilteredValues()
        _ControlItems = New ControlItems
        For Each oItem As DTOProductSku In oFilteredValues
            If MyBase.DisplayObsolets Or oItem.Obsoleto = False Then
                Dim oControlItem As New ControlItem(oItem)
                _ControlItems.Add(oControlItem)
            End If
        Next

        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public Sub Clear()
        _ControlItems = New ControlItems
    End Sub


    Private Function FilteredValues() As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku)
        If _Filter = "" Then
            retval = _Values
        Else
            retval = _Values.FindAll(Function(x) x.nom.Contains(_Filter.ToLower))
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

    Public ReadOnly Property Value As DTOProductSku
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOProductSku = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.DisplayObsolets = False

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
        With MyBase.Columns(Cols.Brand)
            .HeaderText = "Marca comercial"
            .DataPropertyName = "Brand"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = False 'per Frm_Products al presentar-ho junt amb xl_ per brands i categories
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Category)
            .HeaderText = "Categoría"
            .DataPropertyName = "Category"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            .Visible = False
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sku)
            .HeaderText = "Producte"
            .DataPropertyName = "Sku"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
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

    Private Function SelectedItems() As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
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
            Dim oMenu_ProductSku As New Menu_ProductSku(SelectedItems)
            AddHandler oMenu_ProductSku.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_ProductSku.Range)
            oContextMenu.Items.Add("-")
        End If

        Dim oMenuItemObsolets As New ToolStripMenuItem("inclou obsolets")
        oMenuItemObsolets.CheckOnClick = True
        AddHandler oMenuItemObsolets.CheckedChanged, AddressOf onMenuItemObsoletsCheckedChanged
        oContextMenu.Items.Add(oMenuItemObsolets)

        oContextMenu.Items.Add("afegir", Nothing, AddressOf Do_AddNew)
        AddMenuItemObsolets(oContextMenu)

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub onMenuItemObsoletsCheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent RequestToToggleObsoletos(Me, MatEventArgs.Empty)
    End Sub

    Private Sub Do_AddNew()
        RaiseEvent RequestToAddNew(Me, MatEventArgs.Empty)
    End Sub


    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOProductSku = CurrentControlItem.Source
            Select Case _SelectionMode
                Case DTOProduct.SelectionModes.SelectAny, DTOProduct.SelectionModes.SelectSku
                    RaiseEvent onItemSelected(Me, New MatEventArgs(Me.Value))
                Case Else
                    Dim oFrm As New Frm_ProductSku(oSelectedValue)
                    AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
                    oFrm.Show()
            End Select

        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, New MatEventArgs(CurrentControlItem.Source))
            SetContextMenu()
        End If
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles Me.RowPrePaint
        Dim oRow As DataGridViewRow = MyBase.Rows(e.RowIndex)
        Dim oControlItem As ControlItem = oRow.DataBoundItem
        Dim item As DTOProductSku = oControlItem.Source
        If item.Obsoleto Then
            oRow.DefaultCellStyle.BackColor = Color.LightGray
        Else
            oRow.DefaultCellStyle.BackColor = Color.White
        End If
    End Sub



#Region "DragDrop"

    Private mLastMouseDownRectangle As System.Drawing.Rectangle


    Private Sub DataGridView1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        Dim iInterval As Integer = 1
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim hit As DataGridView.HitTestInfo = sender.HitTest(e.X, e.Y)
            If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                mLastMouseDownRectangle = New Rectangle(e.X - iInterval, e.Y - iInterval, 2 * iInterval, 2 * iInterval)
            End If
        End If
    End Sub

    Private Sub DataGridView1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            If Not mLastMouseDownRectangle.Contains(e.X, e.Y) Then
                Dim hit As DataGridView.HitTestInfo = sender.HitTest(mLastMouseDownRectangle.X, mLastMouseDownRectangle.Y)
                If hit.RowIndex >= 0 And hit.ColumnIndex >= 0 Then
                    Dim oSkus As List(Of DTOProductSku) = SelectedItems()
                    sender.DoDragDrop(oSkus, DragDropEffects.Copy)
                End If
            End If
        End If
    End Sub

#End Region




    Protected Class ControlItem
        Property Source As DTOProductSku

        Property Brand As String
        Property Category As String
        Property Sku As String

        Public Sub New(value As DTOProductSku)
            MyBase.New()
            _Source = value
            With value
                _Brand = .category.brand.nom.Tradueix(Current.Session.Lang)
                _Category = .category.nom.Tradueix(Current.Session.Lang)
                _Sku = .nom.Tradueix(Current.Session.Lang)
            End With
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


