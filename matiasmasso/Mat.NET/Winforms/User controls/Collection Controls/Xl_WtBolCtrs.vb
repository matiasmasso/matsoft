Public Class Xl_WtBolCtrs
    Inherits _Xl_ReadOnlyDatagridview

    Private _Serps As List(Of DTOWtbolSerp)
    Private _Ctrs As List(Of DTOWtbolCtr)
    Private _Mode As Modes

    Private _ControlItems As ControlItems
    Private _AllowEvents As Boolean

    Public Event ValueChanged(sender As Object, e As MatEventArgs)
    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Enum Modes
        Sites
        Products
    End Enum

    Private Enum Cols
        Site
        Views
        Prints
        Sites
        Clicks
        CTR
    End Enum

    Public Shadows Sub Load(Serps As List(Of DTOWtbolSerp), Ctrs As List(Of DTOWtbolCtr), Optional mode As Modes = Modes.Sites)
        _Serps = Serps
        _Ctrs = Ctrs
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
        _ControlItems = New ControlItems

        Dim iViews = _Serps.Count
        Dim oBases As IEnumerable(Of DTOBaseGuid) = Nothing

        Select Case _Mode
            Case Modes.Sites
                oBases = _Serps.SelectMany(Function(x) x.Items).Select(Function(y) y.Site)
            Case Modes.Products
                oBases = _Serps.Select(Function(x) x.Product)
        End Select

        Dim oDistinctBases As New List(Of DTOBaseGuid)

        For Each oBase In oBases
            Dim oBaseGuid = oBase.Guid
            If Not oDistinctBases.Any(Function(x) x.Guid.Equals(oBaseGuid)) Then
                oDistinctBases.Add(oBase)
            End If
        Next

        Select Case _Mode
            Case Modes.Sites
                For Each oBase As DTOWtbolSite In oDistinctBases
                    Dim oControlItem As New ControlItem(oBase, iViews, _Serps, _Ctrs)
                    _ControlItems.Add(oControlItem)
                Next
            Case Modes.Products
                For Each oBase As DTOProduct In oDistinctBases
                    Dim oControlItem As New ControlItem(oBase, iViews, _Serps, _Ctrs)
                    _ControlItems.Add(oControlItem)
                Next
        End Select


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
        With MyBase.Columns(Cols.Site)
            .HeaderText = IIf(_Mode = Modes.Sites, "Comerç", "Producte")
            .DataPropertyName = "Site"
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Views)
            .HeaderText = "Pàg.vistes"
            .DataPropertyName = "Views"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Prints)
            .HeaderText = "Impressions"
            .DataPropertyName = "Prints"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Sites)
            .HeaderText = "Sites"
            .DataPropertyName = "Sites"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
            .Visible = _Mode = Modes.Products
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.Clicks)
            .HeaderText = "Clicks"
            .DataPropertyName = "Clicks"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "#,###"
        End With
        MyBase.Columns.Add(New DataGridViewTextBoxColumn)
        With MyBase.Columns(Cols.CTR)
            .HeaderText = "Ctr"
            .DataPropertyName = "Ctr"
            .Width = 60
            .AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            .DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
            .DefaultCellStyle.Format = "0.#\%;-0.#\%;#"
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

            Select Case _Mode
                Case Modes.Sites
                    Dim oMenu_WtbolSite As New Menu_WtbolSite(SelectedItems.First)
                    AddHandler oMenu_WtbolSite.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_WtbolSite.Range)
                Case Modes.Products
                    Dim oMenu_Product As New Menu_Product(SelectedItems.First)
                    AddHandler oMenu_Product.AfterUpdate, AddressOf RefreshRequest
                    oContextMenu.Items.AddRange(oMenu_Product.Range)
                    oContextMenu.Items.Add("-")
                    oContextMenu.Items.Add(New ToolStripMenuItem("Navegar", Nothing, AddressOf Do_Browse))
            End Select
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
            Dim oSelectedValue As DTOWtbolSite = CurrentControlItem.Source
            Dim oFrm As New Frm_WtbolSite(oSelectedValue, Frm_WtbolSite.Tabs.Clicks)
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


    Protected Class ControlItem
        Property Source As DTOBaseGuid

        Property Site As String
        Property Views As Integer
        Property Prints As Integer
        Property Sites As Integer
        Property Clicks As Integer
        Property Ctr As Decimal

        Public Sub New(oSite As DTOWtbolSite, iViews As Integer, oSerps As List(Of DTOWtbolSerp), oCtrs As List(Of DTOWtbolCtr))
            MyBase.New()
            _Source = oSite
            _Site = oSite.Nom
            _Views = iViews
            _Prints = oSerps.SelectMany(Function(x) x.Items).Where(Function(y) y.Site.Equals(oSite)).Count
            _Clicks = oCtrs.Where(Function(x) x.Site.Equals(oSite)).Count
            _Ctr = 100 * _Clicks / Prints
        End Sub

        Public Sub New(oProduct As DTOProduct, iViews As Integer, oSerps As List(Of DTOWtbolSerp), oCtrs As List(Of DTOWtbolCtr))
            MyBase.New()
            Dim oProductSerps = oSerps.Where(Function(x) x.Product.Equals(oProduct))
            _Source = oProduct
            _Site = oProduct.FullNom()
            _Views = iViews
            _Prints = oProductSerps.Count
            _Sites = oProductSerps.SelectMany(Function(x) x.Items).GroupBy(Function(y) y.Site.Guid).Count
            _Clicks = oCtrs.Where(Function(x) x.Product.Equals(oProduct)).Count

            _Ctr = 100 * _Clicks / Prints
        End Sub

    End Class

    Protected Class ControlItems
        Inherits SortableBindingList(Of ControlItem)
    End Class

End Class


