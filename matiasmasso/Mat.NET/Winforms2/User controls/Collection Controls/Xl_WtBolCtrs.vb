Public Class Xl_WtBolCtrs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Values As List(Of DTOWtbolCtr)
    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Enum Modes
        Sites
        Products
    End Enum

    Private Enum Cols
        Fch
        Product
        Site
        Ip
    End Enum

    Public Shadows Sub Load(values As List(Of DTOWtbolCtr))
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
        _ControlItems = New ControlItems

        For Each oItem In _Values
            Dim oControlItem As New ControlItem(oItem)
            _ControlItems.Add(oControlItem)
        Next


        Dim oCell As DTODatagridviewCell = UIHelper.DataGridviewCurrentCell(Me)
        MyBase.DataSource = _ControlItems
        UIHelper.SetDataGridviewCurrentCell(Me, oCell)

        SetContextMenu()
        _AllowEvents = True
    End Sub

    Public ReadOnly Property Value As DTOBaseGuid
        Get
            Dim oControlItem As ControlItem = CurrentControlItem()
            Dim retval As DTOBaseGuid = oControlItem.Source
            Return retval
        End Get
    End Property

    Private Sub SetProperties()
        MyBase.RowTemplate.Height = MyBase.Font.Height * 1.3
        'MyBase.RowWtbolCtr.DefaultCellStyle.BackColor = Color.Transparent

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
        With MyBase.Columns(Cols.Fch)
            .HeaderText = "Data"
            .DataPropertyName = "Fch"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 90
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            .DefaultCellStyle.Format = "dd/MM/yy HH:mm:ss"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Product)
            .HeaderText = "Producte"
            .DataPropertyName = "Product"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Site)
            .HeaderText = "Site"
            .DataPropertyName = "Site"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Ip)
            .HeaderText = "Ip"
            .DataPropertyName = "Ip"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .Width = 60

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

    Private Function SelectedItems() As List(Of DTOBaseGuid)
        Dim retval As New List(Of DTOBaseGuid)
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
            Dim oMenuItemSite As New ToolStripMenuItem("Site...")
            oContextMenu.Items.Add(oMenuItemSite)
            Dim oMenu_WtbolSite As New Menu_WtbolSite(oControlItem.Source.Site)
            AddHandler oMenu_WtbolSite.AfterUpdate, AddressOf RefreshRequest
            oMenuItemSite.DropDownItems.AddRange(oMenu_WtbolSite.Range)


            Dim oMenuItemProduct As New ToolStripMenuItem("Producte...")
            oContextMenu.Items.Add(oMenuItemProduct)
            Dim oMenu_Product As New Menu_Product(DTOProduct.FromObject(oControlItem.Source.Product))
            AddHandler oMenu_Product.AfterUpdate, AddressOf RefreshRequest
            oContextMenu.Items.AddRange(oMenu_Product.Range)
            oContextMenu.Items.Add("-")
            oContextMenu.Items.Add(New ToolStripMenuItem("Navegar", Nothing, AddressOf Do_Browse))
        End If

        MyBase.ContextMenuStrip = oContextMenu
    End Sub

    Private Sub Do_Browse()
        Dim oProduct As DTOProduct = SelectedItems.First
        Dim url As String = oProduct.GetUrl(Current.Session.Lang, DTOProduct.Tabs.distribuidores, True)
        UIHelper.ShowHtml(url)
    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles MyBase.DoubleClick
        Dim oCurrentControlItem As ControlItem = CurrentControlItem()
        If oCurrentControlItem IsNot Nothing Then
            Dim oSelectedValue As DTOWtbolSite = CurrentControlItem.Source.Site
            Dim oFrm As New Frm_WtbolSite(oSelectedValue, Frm_WtbolSite.Tabs.Clicks)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles MyBase.SelectionChanged
        If _AllowEvents Then
            SetContextMenu()
        End If
    End Sub


    Protected Class ControlItem
        Property Source As DTOWtbolCtr

        Property Fch As Date
        Property Product As String
        Property Site As String
        Property Ip As String


        Public Sub New(item As DTOWtbolCtr)
            MyBase.New()
            _Source = item
            _Fch = item.Fch
            _Product = item.Product.Nom
            _Site = item.Site.Nom
            _Ip = item.Ip
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


