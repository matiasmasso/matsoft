Public Class Frm_Cataleg

    Private _Catalog As List(Of DTOProductBrand)
    Private _Product As DTOProduct
    Private _CategorySortOrder As DTOProductCategory.SortOrders
    Private _ShowObsoletos As Boolean
    Private _ObsoletosLoaded As Boolean
    Private _Status As Status
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Private Enum Status
        IsLoading
        IsLoadingBrands
        IsLoadingSkus
        IsLoaded
    End Enum



    Public Sub New(Optional oProduct As DTOProduct = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Status = Status.IsLoading
        _Product = oProduct
    End Sub

    Private Async Sub Frm_Products_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _CategorySortOrder = Await FEB.UserDefaults.GetInt(exs, Current.Session.User, DTOUserDefault.Cods.ProductCategoriesOrder)
        If exs.Count = 0 Then
            Xl_LookupMgz1.Mgz = GlobalVariables.Emp.Mgz
            Await ReloadBrands()
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Sub

    Public ReadOnly Property Product As DTOProduct
        Get
            Return _Product
        End Get
    End Property

    Private Async Sub ReloadBrands(sender As Object, e As MatEventArgs)
        Await ReloadBrands()
    End Sub

    Private Async Function ReloadBrands() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Status = Status.IsLoadingBrands
        _Catalog = Await FEB.ProductCategories.CompactTree(exs, GlobalVariables.Emp, Current.Session.Lang)

        If exs.Count = 0 Then
            Await refresca(exs)
            ProgressBar1.Visible = False
            If exs.Count > 0 Then
                UIHelper.WarnError(exs)
            End If
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Function refresca(exs As List(Of Exception)) As Task(Of Boolean)
        _AllowEvents = False
        If Await RefrescaProductBrands(exs) Then
            _AllowEvents = True
        End If
        Return exs.Count = 0
    End Function

    Private Async Function RefrescaProductBrands(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oLang As DTOLang = Current.Session.Lang
        Dim oDefaultBrand = DTOProduct.Brand(_Product)
        Dim oFilteredBrands = _Catalog
        'If Not _ShowObsoletos Then oFilteredBrands = oFilteredBrands.Where(Function(x) x.Obsoleto = False).ToList
        Xl_ProductBrands1.Load(oFilteredBrands, oDefaultValue:=oDefaultBrand, DisplayObsoletos:=_ShowObsoletos)
        Await RefrescaProductCategories(exs)
        Return exs.Count = 0
    End Function

    Private Async Function RefrescaProductCategories(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oProductBrand As DTOProductBrand = Xl_ProductBrands1.Value
        Dim oDefaultCategory As DTOProductCategory = DTOProduct.Category(_Product)
        Dim oFilteredCategories As New List(Of DTOProductCategory)
        If oProductBrand IsNot Nothing Then
            oFilteredCategories = oProductBrand.Categories
        End If

        'If Not _ShowObsoletos Then oFilteredCategories = oFilteredCategories.Where(Function(x) x.Obsoleto = False).ToList
        Xl_ProductCategories1.load(oFilteredCategories, oDefaultValue:=oDefaultCategory, oSortOrder:=_CategorySortOrder, DisplayObsoletos:=_ShowObsoletos) 'DisplayObsoletos per evitar el default value false
        Await RefrescaProductSkus(exs)
        Return exs.Count = 0
    End Function


    Private Async Sub RefrescaProductSkus(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Await RefrescaProductSkus(exs)
    End Sub

    Private Async Function RefrescaProductSkus(exs As List(Of Exception)) As Task(Of Boolean)
        _Status = Status.IsLoadingSkus
        Xl_ProductSkusExtended1.Load(New List(Of DTOProductSku)) 'aclara primer mentres baixa les dades
        Dim oProductCategory As DTOProductCategory = Xl_ProductCategories1.Value
        If oProductCategory IsNot Nothing Then
            ProgressBar1.Visible = True
            Dim oSkus = Await FEB.ProductSkus.All(exs, oProductCategory, Current.Session.Lang, Xl_LookupMgz1.Mgz, _ShowObsoletos)
            If exs.Count = 0 Then
                'The user may have selected another category while the Api is still delivering the previous one
                If oProductCategory.Equals(Xl_ProductCategories1.Value) Then
                    Xl_ProductSkusExtended1.Load(oSkus)
                    _Status = Status.IsLoaded
                    ProgressBar1.Visible = False
                End If
            End If
        End If
        Return exs.Count = 0
    End Function

    Public ReadOnly Property Value As DTOProduct
        Get
            Dim retval As DTOProduct = Nothing
            If Xl_ProductSkusExtended1.Value IsNot Nothing Then
                retval = Xl_ProductSkusExtended1.Value
            ElseIf Xl_ProductCategories1.Value IsNot Nothing Then
                retval = Xl_ProductCategories1.Value
            ElseIf Xl_ProductBrands1.Value IsNot Nothing Then
                retval = Xl_ProductBrands1.Value
            End If
            Return retval
        End Get
    End Property

    Private Sub Xl_onItemSelected(sender As Object, e As MatEventArgs) Handles _
        Xl_ProductBrands1.OnItemSelected,
         Xl_ProductCategories1.OnItemSelected,
          Xl_ProductSkusExtended1.onItemSelected

        RaiseEvent onItemSelected(Me, e)
        _Product = e.Argument
        Me.Close()
    End Sub

    Private Sub Xl_ProductBrands1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.RequestToAddNew
        Dim oProductBrand As DTOProductBrand = DTOProductBrand.Factory(Current.Session.Emp)
        Dim oFrm As New Frm_ProductBrand(oProductBrand)
        AddHandler oFrm.AfterUpdate, AddressOf ReloadBrands
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductBrands1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.RequestToRefresh
        Await ReloadBrands()
    End Sub

    Private Async Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Or _Status = Status.IsLoadingSkus Then
            If Await RefrescaProductCategories(exs) Then
                RaiseEvent ValueChanged(Me, e)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Xl_ProductCategories1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToAddNew
        Dim oProductBrand As DTOProductBrand = Xl_ProductBrands1.Value
        Dim oProductCategory = DTOProductCategory.Factory(oProductBrand)
        Dim oFrm As New Frm_ProductCategory(oProductCategory) 'Ojo, es carrega les url i les descripcions. Nomes val per noves S`tp
        AddHandler oFrm.AfterUpdate, AddressOf Xl_ProductCategories1_AfterUpdate
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductCategories1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToRefresh
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            _Product = Me.Value
            If Not Await RefrescaProductCategories(exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Async Sub Xl_ProductCategories1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.ValueChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Or _Status = Status.IsLoadingSkus Then
            If Await RefrescaProductSkus(exs) Then
                RaiseEvent ValueChanged(Me, e)
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub Xl_ProductSkusExtended1_RequestToAddNewSku(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtended1.RequestToAddNewSku
        Dim oProductCategory As DTOProductCategory = Xl_ProductCategories1.Value
        Dim oProductSku = DTOProductSku.Factory(Current.Session.User, oProductCategory)
        Dim oFrm As New Frm_ProductSku(oProductSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductSkus
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductSkusExtended1_RequestToAddNewBundle(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtended1.RequestToAddNewBundle
        Dim oProductCategory As DTOProductCategory = Xl_ProductCategories1.Value
        Dim oProductSku = DTOProductSku.Factory(Current.Session.User, oProductCategory)
        oProductSku.IsBundle = True
        Dim oFrm As New Frm_SkuBundle(oProductSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductSkus
        oFrm.Show()
    End Sub


    Private Async Sub Xl_ProductSkusExtended1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtended1.RequestToRefresh
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
            _Product = Me.Value
            If Not Await RefrescaProductSkus(exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub


    Private Sub Xl_ProductSkusExtended1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductSkusExtended1.ValueChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub IncentiusToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IncentiusToolStripMenuItem.Click
        Dim oFrm As New Frm_Incentius
        oFrm.Show()
    End Sub

    Private Async Sub RefrescaF5ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefrescaF5ToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        If Not Await refresca(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub


    Private Async Sub Xl_ProductBrands1_onShowObsoletsChanged(sender As Object, e As MatEventArgs) Handles _
            Xl_ProductBrands1.RequestToToggleObsoletos,
              Xl_ProductCategories1.RequestToToggleObsoletos,
               Xl_ProductSkusExtended1.RequestToToggleObsoletos

        If _AllowEvents Then
            _AllowEvents = False
            Dim exs As New List(Of Exception)
            _ShowObsoletos = Not _ShowObsoletos

            Xl_ProductBrands1.ShowObsolets = _ShowObsoletos
            Xl_ProductCategories1.ShowObsolets = _ShowObsoletos
            Xl_ProductSkusExtended1.ShowObsolets = _ShowObsoletos

            If Not Await refresca(exs) Then
                UIHelper.WarnError(exs)
            End If
            _AllowEvents = True
        End If
    End Sub

    Private Async Sub Xl_ProductCategories1_AfterUpdate() Handles Xl_ProductCategories1.RequestToRefresh
        Await ReloadBrands()
    End Sub


    Private Sub StocksRealsToolStripMenuItem_CheckedChanged(sender As Object, e As EventArgs) Handles StocksRealsToolStripMenuItem.CheckedChanged
        Xl_ProductSkusExtended1.ShowRealStocks = StocksRealsToolStripMenuItem.Checked
        Me.Text = IIf(StocksRealsToolStripMenuItem.Checked, "Catàleg  (Stock real)", "Catàleg ")
    End Sub

    Private Async Sub Xl_LookupMgz1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_LookupMgz1.AfterUpdate
        Dim exs As New List(Of Exception)
        If Not Await RefrescaProductSkus(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductSku1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_ProductSku1.AfterUpdate
        Dim exs As New List(Of Exception)

        _Product = e.Argument
        If _Product IsNot Nothing Then
            If _Product.obsoleto Then _ShowObsoletos = True
            Xl_ProductBrands1.ShowObsolets = _ShowObsoletos
            Xl_ProductCategories1.ShowObsolets = _ShowObsoletos
            Xl_ProductSkusExtended1.ShowObsolets = _ShowObsoletos

            If Not Await refresca(exs) Then
                UIHelper.WarnError(exs)
            End If
        End If
        _AllowEvents = True
    End Sub

    Private Sub ImportarExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportarExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Filter = "Documents Excel|*.xlsx;*.xls|tots els fitxers|*.*"
            If .ShowDialog = DialogResult.OK Then
                Dim oBook = MatHelper.Excel.ClosedXml.Read(exs, .FileName)
                If exs.Count = 0 Then
                    Dim oSheet = oBook.Sheets.FirstOrDefault()
                    Dim oFrm As New Frm_CatalogImport(oSheet)
                    oFrm.Show()
                Else
                    UIHelper.WarnError(exs)
                End If
            End If

        End With
    End Sub

    Private Sub DescatalogatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DescatalogatsToolStripMenuItem.Click
        Dim oFrm As New Frm_Descatalogats
        oFrm.Show()
    End Sub
End Class