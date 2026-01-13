

Public Class Frm_Last_Incidencies
    Private _Request As Models.IncidenciesModel.Request
    Private _Model As Models.IncidenciesModel
    Private _Customer As DTOGuidNom.Compact
    Private _Product As DTOGuidNom.Compact
    Private _AllowEvents As Boolean

    Public Sub New()
        MyBase.New()
        InitializeComponent()
        CheckBoxIncludeClosed.Checked = False
    End Sub

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New()
        InitializeComponent()
        _Customer = DTOGuidNom.Compact.Factory(oCustomer.Guid, oCustomer.FullNom)
        CheckBoxIncludeClosed.Checked = True
    End Sub

    Public Sub New(oProduct As DTOProduct)
        MyBase.New()
        InitializeComponent()
        _Product = DTOGuidNom.Compact.Factory(oProduct.Guid, oProduct.FullNom)
        CheckBoxIncludeClosed.Checked = True
    End Sub

    Private Async Sub Frm_Last_Incidencies_Load(sender As Object, e As EventArgs) Handles Me.Load
        Xl_Years1.LoadFrom()
        Await Reload()
        _AllowEvents = True
    End Sub

    Private Async Function Reload() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim year = If(CheckBoxYear.Checked, Xl_Years1.Value, 0)
        _Model = Await FEB.Incidencias.Model(exs, Current.Session.User, Not CheckBoxIncludeClosed.Checked, _Customer, _Product, year)
        If exs.Count = 0 Then
            LoadBrands()
            LoadCustomers()
            LoadAperturas()
            LoadTancaments()
            CheckBoxSrcProducte.Enabled = _Model.Items.Any(Function(x) x.Src = DTOIncidencia.Srcs.Producte)
            CheckBoxSrcTransport.Enabled = _Model.Items.Any(Function(x) x.Src = DTOIncidencia.Srcs.transport)
            refresca()
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Sub refresca()
        Dim oIncidencies = FilteredItems()
        Xl_Incidencies1.Load(oIncidencies, _Model.Customers, _Customer, _Model.Catalog)
        Cursor = Cursors.Default
    End Sub

    Private Function FilteredItems() As HashSet(Of Models.IncidenciesModel.Item)
        Dim retval = _Model.Items
        If _Customer Is Nothing AndAlso CurrentCustomer() IsNot Nothing Then
            retval = retval.Where(Function(x) x.CustomerGuid.Equals(CurrentCustomer().Guid)).ToHashSet()
        End If
        If CurrentApertura() IsNot Nothing Then
            retval = retval.Where(Function(x) x.CodApertura.Equals(CurrentApertura.Guid)).ToHashSet()
        End If
        If CurrentTancament() IsNot Nothing Then
            retval = retval.Where(Function(x) x.CodTancament.Equals(CurrentTancament.Guid)).ToHashSet()
        End If
        If CurrentSku() IsNot Nothing Then
            retval = retval.Where(Function(x) x.ProductGuid.Equals(CurrentSku.Guid)).ToHashSet()
        ElseIf CurrentCategory() IsNot Nothing Then
            retval = retval.Where(Function(x) x.ProductGuid.Equals(CurrentCategory.Guid) Or CurrentCategory.Skus.Any(Function(y) y.Guid.Equals(x.ProductGuid))).ToHashSet()
        ElseIf CurrentBrand() IsNot Nothing Then
            Dim oBrand = CurrentBrand()
            Dim oCategories = oBrand.Categories
            Dim oSkus = oBrand.Categories.SelectMany(Function(x) x.Skus)
            Dim oProductGuids As New List(Of Guid)
            oProductGuids.Add(oBrand.Guid)
            oProductGuids.AddRange(oCategories.Select(Function(x) x.Guid))
            oProductGuids.AddRange(oSkus.Select(Function(x) x.Guid))
            retval = retval.Where(Function(x) oProductGuids.Any(Function(y) y.Equals(x.ProductGuid))).ToHashSet()
        End If
        If Not CheckBoxIncludeClosed.Checked Then
            retval = retval.Where(Function(x) x.CodTancament = Nothing).ToHashSet()
        End If
        If Not CheckBoxSrcProducte.Checked Then
            retval = retval.Where(Function(x) x.Src <> DTOIncidencia.Srcs.Producte).ToHashSet()
        End If
        If Not CheckBoxSrcTransport.Checked Then
            retval = retval.Where(Function(x) x.Src <> DTOIncidencia.Srcs.transport).ToHashSet()
        End If
        If Not String.IsNullOrEmpty(Xl_TextboxSearch1.Value) Then
            retval = retval.Where(Function(x) x.Matches(Xl_TextboxSearch1.Value)).ToHashSet()
        End If
        Return retval
    End Function


    Private Function GetSrc() As DTOIncidencia.Srcs
        Dim retval As DTOIncidencia.Srcs = DTOIncidencia.Srcs.notSet
        If CheckBoxSrcProducte.Checked And Not CheckBoxSrcTransport.Checked Then retval = DTOIncidencia.Srcs.Producte
        If CheckBoxSrcTransport.Checked And Not CheckBoxSrcProducte.Checked Then retval = DTOIncidencia.Srcs.transport
        Return retval
    End Function



    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        ComboBoxApertura.SelectedIndexChanged,
          ComboBoxTancament.SelectedIndexChanged,
            CheckBoxSrcTransport.CheckedChanged,
              CheckBoxIncludeClosed.CheckedChanged,
                ComboBoxCustomer.SelectedIndexChanged,
                 CheckBoxSrcProducte.CheckedChanged,
                  CheckBoxSrcTransport.CheckedChanged

        If _AllowEvents Then
            refresca()
        End If
    End Sub

    Private Async Sub Xl_Incidencies1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Incidencies1.RequestToRefresh
        Await Reload()
    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Cursor = Cursors.WaitCursor
        Dim oSheet As MatHelper.Excel.Sheet = FEB.IncidenciaQuery.ExcelSheet(_Model, Application.CurrentCulture, Current.Session.Lang)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
        Cursor = Cursors.Default
    End Sub

    Private Async Sub ReposicionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReposicionsToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Cursor = Cursors.WaitCursor
        Dim items = Await FEB.Incidencias.Reposicions(exs, Current.Session.Emp, DTO.GlobalVariables.Today().Year - 1)
        If exs.Count = 0 Then
            For Each item As DTOIncidencia In items
                item.Url = FEB.UrlHelper.Factory(True, "incidencia", item.Guid.ToString())
            Next
            Dim oSheet = DTOIncidencia.ExcelReposicions(items)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
            Cursor = Cursors.Default
        Else
            Cursor = Cursors.Default
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub ToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem1.Click
        Await Reload()
    End Sub

#Region "Customer"

    Private Sub LoadCustomers()
        Dim caption = Current.Session.Tradueix("(todos los clientes)", "(tots els clients)", "(all customers)")
        Dim oDefault = DTOGuidNom.Compact.Factory(Guid.Empty, caption)
        Dim oCustomers = _Model.Customers.OrderBy(Function(x) x.Nom).ToList()
        oCustomers.Insert(0, oDefault)
        With ComboBoxCustomer
            .DisplayMember = "Nom"
            .DataSource = oCustomers
        End With
    End Sub

    Private Function CurrentCustomer() As DTOGuidNom.Compact
        Dim retval As DTOGuidNom.Compact = _Customer
        If ComboBoxCustomer.SelectedIndex > 0 Then
            retval = ComboBoxCustomer.SelectedItem
        End If
        Return retval
    End Function


#End Region

#Region "Apertures"
    Private Sub LoadAperturas()
        Dim caption = Current.Session.Tradueix("(todos los códigos de apertura)", "(tots els codis d'apertura)", "(all opening codes)")
        Dim oDefault = DTOGuidNom.Compact.Factory(Guid.Empty, caption)
        Dim oAperturas = _Model.CodisApertura.OrderBy(Function(x) x.Nom).ToList()
        oAperturas.Insert(0, oDefault)
        With ComboBoxApertura
            .DisplayMember = "Nom"
            .DataSource = oAperturas
        End With
    End Sub

    Private Function CurrentApertura() As DTOGuidNom.Compact
        Dim retval As DTOGuidNom.Compact = Nothing
        If ComboBoxApertura.SelectedIndex > 0 Then
            retval = ComboBoxApertura.SelectedItem
        End If
        Return retval
    End Function

#End Region

#Region "Tancaments"
    Private Sub LoadTancaments()
        Dim caption = Current.Session.Tradueix("(todos los códigos de cierre)", "(tots els codis de tancament)", "(all closing codes)")
        Dim oDefault = DTOGuidNom.Compact.Factory(Guid.Empty, caption)
        Dim oTancaments = _Model.CodisTancament.OrderBy(Function(x) x.Nom).ToList()
        oTancaments.Insert(0, oDefault)
        With ComboBoxTancament
            .DisplayMember = "Nom"
            .DataSource = oTancaments
        End With
        ComboBoxTancament.Enabled = CheckBoxIncludeClosed.Checked
    End Sub

    Private Function CurrentTancament() As DTOGuidNom.Compact
        Dim retval As DTOGuidNom.Compact = Nothing
        If ComboBoxTancament.SelectedIndex > 0 Then
            retval = ComboBoxTancament.SelectedItem
        End If
        Return retval
    End Function

#End Region



#Region "Products"
    Private Sub LoadBrands()
        Dim caption = Current.Session.Tradueix("(todas las marcas)", "(totes les marques)", "(all brands)")
        Dim oDefault = New Models.CatalogModel.Brand(Guid.Empty, caption)
        Dim oBrands = _Model.Catalog.Brands.OrderBy(Function(x) x.Nom).ToList()
        oBrands.Insert(0, oDefault)
        With ComboBoxBrand
            .DisplayMember = "Nom"
            .DataSource = oBrands
        End With
    End Sub

    Private Sub LoadCategories(oBrand As Models.CatalogModel.Brand)
        Dim caption = Current.Session.Tradueix("(todas las categorías)", "(totes les categories)", "(all categories)")
        Dim oDefault = New Models.CatalogModel.Category(Guid.Empty, caption)
        Dim oCategories = oBrand.Categories.OrderBy(Function(x) x.Nom).ToList()
        oCategories.Insert(0, oDefault)
        With ComboBoxCategory
            .DisplayMember = "Nom"
            .DataSource = oCategories
        End With
    End Sub

    Private Sub LoadSkus(oCategory As Models.CatalogModel.Category)
        Dim caption = Current.Session.Tradueix("(todas los productos)", "(tots els productes)", "(all products)")
        Dim oDefault = New Models.CatalogModel.Sku(Guid.Empty, caption)
        Dim oSkus = oCategory.Skus.OrderBy(Function(x) x.Nom).ToList()
        oSkus.Insert(0, oDefault)
        With ComboBoxSku
            .DisplayMember = "Nom"
            .DataSource = oSkus
        End With
    End Sub

    Private Function CurrentBrand() As Models.CatalogModel.Brand
        Dim retval As Models.CatalogModel.Brand = Nothing
        If ComboBoxBrand.SelectedIndex > 0 Then
            retval = ComboBoxBrand.SelectedItem
        End If
        Return retval
    End Function

    Private Function CurrentCategory() As Models.CatalogModel.Category
        Dim retval As Models.CatalogModel.Category = Nothing
        If CurrentBrand() IsNot Nothing AndAlso ComboBoxCategory.SelectedIndex > 0 Then
            retval = ComboBoxCategory.SelectedItem
        End If
        Return retval
    End Function

    Private Function CurrentSku() As Models.CatalogModel.Sku
        Dim retval As Models.CatalogModel.Sku = Nothing
        If CurrentCategory() IsNot Nothing AndAlso ComboBoxSku.SelectedIndex > 0 Then
            retval = ComboBoxSku.SelectedItem
        End If
        Return retval
    End Function

    Private Sub ComboBoxBrand_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxBrand.SelectedIndexChanged
        If _AllowEvents Then
            Dim oBrand = CurrentBrand()
            If oBrand Is Nothing Then
                ComboBoxCategory.Visible = False
            Else
                ComboBoxCategory.Visible = True
                LoadCategories(oBrand)
            End If
            ComboBoxSku.Visible = False
            refresca()
        End If
    End Sub

    Private Sub ComboBoxCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCategory.SelectedIndexChanged
        If _AllowEvents Then
            Dim oCategory = CurrentCategory()
            If oCategory Is Nothing Then
                ComboBoxSku.Visible = False
            Else
                ComboBoxSku.Visible = True
                LoadSkus(oCategory)
            End If
            refresca()
        End If
    End Sub

    Private Sub ComboBoxSku_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSku.SelectedIndexChanged
        If _AllowEvents Then
            refresca()
        End If
    End Sub




#End Region

    Private Async Sub CheckBoxIncludeClosed_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxIncludeClosed.CheckedChanged
        If _AllowEvents Then
            If CheckBoxIncludeClosed.Checked Then
                If _Model.Items.All(Function(x) x.FchClose = Nothing) Then
                    _AllowEvents = False
                    CheckBoxYear.Checked = True
                    Xl_Years1.Enabled = True
                    Await Reload()
                    _AllowEvents = True
                Else
                    refresca()
                End If
            Else
                refresca()
            End If
        End If
    End Sub

    Private Async Sub CheckBoxYear_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxYear.CheckedChanged
        If _AllowEvents Then
            Xl_Years1.Enabled = CheckBoxYear.Checked
            _AllowEvents = False
            Await Reload()
            _AllowEvents = True
        End If

    End Sub

    Private Async Sub Xl_Years1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Years1.AfterUpdate
        _AllowEvents = False
        Await Reload()
        _AllowEvents = True
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        refresca()
    End Sub
End Class