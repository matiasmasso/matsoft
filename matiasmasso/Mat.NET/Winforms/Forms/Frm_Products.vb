Public Class Frm_Products

    Private _Catalog As DTOProductCatalog
    Private _Product As DTOProduct
    Private _SelMode As SelModes
    Private _ProductBrandMode As DTO.Defaults.SelectionModes
    Private _ProductCategoryMode As DTO.Defaults.SelectionModes
    Private _CategorySortOrder As DTOProductCategory.SortOrders
    Private _ProductSkuMode As DTO.Defaults.SelectionModes
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)

    Public Enum SelModes
        Browse
        SelectProduct
        SelectProductBrand
        SelectProductCategory
        SelectProductSku
        SelectZip
    End Enum

    Public Sub New(Optional oProduct As DTOProduct = Nothing, Optional oSelectMode As SelModes = SelModes.Browse, Optional oCatalog As DTOProductCatalog = Nothing)
        MyBase.New()
        Me.InitializeComponent()
        _Product = oProduct
        _Catalog = oCatalog
        SetSelectionModes(oSelectMode)
    End Sub

    Private Async Sub Frm_Products_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If Await RefrescaProductBrands(exs) Then
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Public ReadOnly Property Product As DTOProduct
        Get
            Return _Product
        End Get
    End Property

    Private Async Sub RefrescaProductBrands(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        If Not Await RefrescaProductBrands(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function RefrescaProductBrands(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oLang As DTOLang = Current.Session.Lang
        Dim oProductBrands As List(Of DTOProductBrand)
        If _Catalog Is Nothing Then
            oProductBrands = Await FEBL.ProductBrands.All(exs, Current.Session.Emp, True)
        Else
            oProductBrands = _Catalog.Brands
        End If
        If exs.Count = 0 Then
            Dim oDefaultBrand = FEBL.Product.Brand(exs, _Product)
            If exs.Count = 0 Then
                Xl_ProductBrands1.Load(oProductBrands, _ProductBrandMode, , oDefaultBrand)
                Await RefrescaProductCategories(exs)
            End If
        End If
        Return exs.Count = 0
    End Function


    Private Async Sub RefrescaProductCategories(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        If Not Await RefrescaProductCategories(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Function RefrescaProductCategories(exs As List(Of Exception)) As Task(Of Boolean)
        Dim oProductBrand As DTOProductBrand = Xl_ProductBrands1.Value
        If oProductBrand Is Nothing Then
            Xl_ProductCategories1.Clear()
            Xl_ProductSkus1.Clear()
        Else
            Dim oProductCategories As List(Of DTOProductCategory)
            If _Catalog Is Nothing Then
                oProductCategories = Await FEBL.ProductCategories.All(exs, oProductBrand, IncludeObsolets:=True)
            Else
                oProductCategories = _Catalog.Categories.Where(Function(x) x.Brand.Equals(oProductBrand)).ToList
            End If
            If exs.Count = 0 Then
                Dim oDefaultCategory As DTOProductCategory = DTOProduct.Category(_Product)
                Xl_ProductCategories1.load(oProductCategories, _ProductCategoryMode, , oDefaultCategory, _CategorySortOrder)
                Await RefrescaProductSkus()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
        Return exs.Count = 0
    End Function

    Private Async Sub RefrescaProductSkus(sender As Object, e As MatEventArgs)
        Await RefrescaProductSkus()
    End Sub

    Private Async Function RefrescaProductSkus() As Task
        Dim exs As New List(Of Exception)
        Dim oProductCategory As DTOProductCategory = Xl_ProductCategories1.Value

        Select Case _SelMode
            Case SelModes.Browse
                Xl_ProductSkus1.Visible = False
                Xl_ProductStocks1.Visible = True

                If oProductCategory Is Nothing Then
                    Xl_ProductStocks1.Clear()
                Else
                    Dim oStocks = Await FEBL.Stocks.All(exs, GlobalVariables.Emp.Mgz, oProductCategory)
                    If exs.Count = 0 Then
                        Xl_ProductStocks1.Load(oStocks)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            Case Else
                Xl_ProductSkus1.Visible = True
                Xl_ProductStocks1.Visible = False

                If oProductCategory Is Nothing Then
                    Xl_ProductSkus1.Clear()
                Else

                    Dim oProductSkus As List(Of DTOProductSku)
                    If _Catalog Is Nothing Or Xl_ProductSkus1.DisplayObsolets Then
                        oProductSkus = Await FEBL.ProductSkus.All(exs, oProductCategory, GlobalVariables.Emp.Mgz, True)
                    Else
                        oProductSkus = _Catalog.Skus.Where(Function(x) x.Category.Equals(oProductCategory)).ToList
                    End If

                    If exs.Count = 0 Then
                        Dim oDefaultProduct As DTOProductSku = Nothing
                        If TypeOf _Product Is DTOProductSku Then
                            oDefaultProduct = _Product
                        End If
                        Xl_ProductSkus1.Load(oProductSkus, _ProductSkuMode, , oDefaultProduct)
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
        End Select
    End Function



    Public ReadOnly Property Value As DTOProduct
        Get
            Dim retval As DTOProduct = Nothing
            If Xl_ProductSkus1.Value IsNot Nothing Then
                retval = Xl_ProductSkus1.Value
            ElseIf Xl_ProductCategories1.Value IsNot Nothing Then
                retval = Xl_ProductCategories1.Value
            ElseIf Xl_ProductBrands1.Value IsNot Nothing Then
                retval = Xl_ProductBrands1.Value
            End If
            Return retval
        End Get
    End Property

    Private Sub Xl_onItemSelected(sender As Object, e As MatEventArgs) Handles _
        Xl_ProductBrands1.OnItemSelected, _
         Xl_ProductCategories1.OnItemSelected, _
          Xl_ProductSkus1.OnItemSelected

        RaiseEvent onItemSelected(Me, e)
        _Product = e.Argument
        Me.Close()
    End Sub

    Private Sub Xl_ProductBrands1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.RequestToAddNew
        Dim oProductBrand = DTOProductBrand.Factory(Current.Session.Emp)
        Dim oFrm As New Frm_ProductBrand(oProductBrand)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductBrands
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductBrands1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.RequestToRefresh
        Dim exs As New List(Of Exception)
        _Product = Me.Value
        If Not Await RefrescaProductBrands(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        Dim exs As New List(Of Exception)
        If _AllowEvents Then
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
        Dim oFrm As New Frm_ProductCategory(oProductCategory)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductCategories
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductCategories1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToRefresh
        Dim exs As New List(Of Exception)
        _Product = Me.Value
        If Not Await RefrescaProductCategories(exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Xl_ProductCategories1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.ValueChanged
        If _AllowEvents Then
            Await RefrescaProductSkus()
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub Xl_ProductSkus1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductSkus1.RequestToAddNew
        Dim oProductCategory As DTOProductCategory = Xl_ProductCategories1.Value
        Dim oProductSku = DTOProductSku.Factory(Current.Session.User, oProductCategory)
        Dim oFrm As New Frm_ProductSku(oProductSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductSkus
        oFrm.Show()
    End Sub

    Private Async Sub Xl_ProductSkus1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductSkus1.RequestToRefresh
        _Product = Me.Value
        Await RefrescaProductSkus()
    End Sub


    Private Sub Xl_ProductSkus1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductSkus1.ValueChanged
        If _AllowEvents Then
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

 

    Private Sub SetSelectionModes(oSelMode As SelModes)
        _SelMode = oSelMode
        Select Case _SelMode
            Case SelModes.SelectProductBrand
                _ProductBrandMode = DTO.Defaults.SelectionModes.Selection
            Case SelModes.SelectProductCategory
                _ProductCategoryMode = DTO.Defaults.SelectionModes.Selection
            Case SelModes.SelectProductSku
                _ProductSkuMode = DTO.Defaults.SelectionModes.Selection
            Case SelModes.SelectProduct
                _ProductBrandMode = DTO.Defaults.SelectionModes.Selection
                _ProductCategoryMode = DTO.Defaults.SelectionModes.Selection
                _ProductSkuMode = DTO.Defaults.SelectionModes.Selection
        End Select
    End Sub


End Class