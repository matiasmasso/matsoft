Public Class Frm_Products

    Private _Product As DTOProduct
    Private _SelMode As SelModes
    Private _ProductBrandMode As BLL.Defaults.SelectionModes
    Private _ProductCategoryMode As BLL.Defaults.SelectionModes
    Private _ProductSkuMode As BLL.Defaults.SelectionModes
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

    Public Sub New(Optional oProduct As DTOProduct = Nothing, Optional oSelectMode As SelModes = SelModes.Browse)
        MyBase.New()
        Me.InitializeComponent()
        _Product = oProduct
        SetSelectionModes(oSelectMode)
    End Sub

    Private Sub Frm_Products_Load(sender As Object, e As EventArgs) Handles Me.Load
        RefrescaProductBrands()
        _AllowEvents = True
    End Sub

    Private Sub RefrescaProductBrands()
        Dim oLang As DTOLang = Session.User.Lang
        Dim oProductBrands As List(Of DTOProductBrand) = BLL.BLLProductBrands.All(BLL.BLLApp.Emp, True)
        Xl_ProductBrands1.Load(oProductBrands, _ProductBrandMode)
        RefrescaProductCategories()
    End Sub

    Private Sub RefrescaProductCategories()
        Dim oProductBrand As DTOProductBrand = Xl_ProductBrands1.Value
        If oProductBrand Is Nothing Then
            Xl_ProductCategories1.Clear()
            Xl_ProductSkus1.Clear()
        Else
            Dim oProductCategories As List(Of DTOProductCategory) = BLL.BLLProductCategories.All(oProductBrand, True)
            Xl_ProductCategories1.Load(oProductCategories, _ProductCategoryMode)
            RefrescaProductSkus()
        End If
    End Sub

    Private Sub RefrescaProductSkus()
        Dim oProductCategory As DTOProductCategory = Xl_ProductCategories1.Value

        Select Case _SelMode
            Case SelModes.Browse
                Xl_ProductSkus1.Visible = False
                Xl_ProductStocks1.Visible = True

                If oProductCategory Is Nothing Then
                    Xl_ProductStocks1.Clear()
                Else
                    Dim oStocks As List(Of DTO.DTOStock) = BLL.BLLStocks.All(oProductCategory)
                    Xl_ProductStocks1.Load(oStocks)
                End If
            Case Else
                Xl_ProductSkus1.Visible = True
                Xl_ProductStocks1.Visible = False

                If oProductCategory Is Nothing Then
                    Xl_ProductSkus1.Clear()
                Else
                    Dim oProductSkus As List(Of DTOProductSku) = BLL.BLLProductSkus.All(oProductCategory, True)
                    Xl_ProductSkus1.Load(oProductSkus, _ProductSkuMode)
                End If
        End Select
    End Sub



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
        Me.Close()
    End Sub

    Private Sub Xl_ProductBrands1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.RequestToAddNew
        Dim oProductBrand As DTOProductBrand = BLL.BLLProductBrand.NewProductBrand(BLL.BLLApp.Emp)
        Dim oFrm As New Frm_ProductBrand(oProductBrand)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductBrands
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductBrands1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.RequestToRefresh
        _Product = Me.Value
        RefrescaProductBrands()
    End Sub

    Private Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        If _AllowEvents Then
            RefrescaProductCategories()
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub Xl_ProductCategories1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToAddNew
        Dim oProductBrand As DTOProductBrand = Xl_ProductBrands1.Value
        Dim oProductCategory As DTOProductCategory = BLL.BLLProductCategory.NewFromBrand(oProductBrand)
        Dim oFrm As New Frm_ProductCategory(oProductCategory)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductCategories
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductCategories1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.RequestToRefresh
        _Product = Me.Value
        RefrescaProductCategories()
    End Sub

    Private Sub Xl_ProductCategories1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.ValueChanged
        If _AllowEvents Then
            RefrescaProductSkus()
            RaiseEvent ValueChanged(Me, e)
        End If
    End Sub

    Private Sub Xl_ProductSkus1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_ProductSkus1.RequestToAddNew
        Dim oProductCategory As DTOProductCategory = Xl_ProductCategories1.Value
        Dim oProductSku As DTOProductSku = BLL.BLLProductSku.NewFromCategory(oProductCategory)
        Dim oFrm As New Frm_ProductSku(oProductSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefrescaProductSkus
        oFrm.Show()
    End Sub

    Private Sub Xl_ProductSkus1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_ProductSkus1.RequestToRefresh
        _Product = Me.Value
        RefrescaProductSkus()
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
                _ProductBrandMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectProductCategory
                _ProductCategoryMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectProductSku
                _ProductSkuMode = BLL.Defaults.SelectionModes.Selection
            Case SelModes.SelectProduct
                _ProductBrandMode = BLL.Defaults.SelectionModes.Selection
                _ProductCategoryMode = BLL.Defaults.SelectionModes.Selection
                _ProductSkuMode = BLL.Defaults.SelectionModes.Selection
        End Select
    End Sub


End Class