

Public Class Frm_ProductSkus
    Private _Brands As List(Of DTOProductBrand)
    Private _IncludeObsoletos As Boolean
    Private _DefaultProduct As DTOProduct
    Private _SelectionMode As DTOProduct.SelectionModes
    Private _IncludeNullValue As Boolean
    Private _CustomCatalog As List(Of DTOProductBrand)
    Private _AllowEvents = True

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oSelectionMode As DTOProduct.SelectionModes, Optional oDefaultProduct As DTOProduct = Nothing, Optional IncludeObsoletos As Boolean = False, Optional oCustomCatalog As List(Of DTOProductBrand) = Nothing)
        MyBase.New
        InitializeComponent()
        _SelectionMode = oSelectionMode
        _IncludeObsoletos = IncludeObsoletos
        _CustomCatalog = oCustomCatalog

    End Sub

    Private Async Sub Frm_ProductSkus_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await reloadBrands()
    End Sub

    Private Async Function reloadBrands() As Task
        Dim exs As New List(Of Exception)
        If _CustomCatalog Is Nothing Then
            ProgressBar1.Visible = True
            _Brands = Await FEB.ProductSkus.CompactTree(exs, GlobalVariables.Emp, Current.Session.Lang, _IncludeObsoletos)
            ProgressBar1.Visible = False
        Else
            _Brands = _CustomCatalog
            _IncludeObsoletos = True
            InclouObsoletsToolStripMenuItem.Enabled = False
        End If
        If exs.Count = 0 Then
            refrescaBrands(DefaultBrand)
        Else
            UIHelper.WarnError(exs)
        End If

    End Function

    Private Sub refrescaBrands(Optional oDefaultBrand As DTOProductBrand = Nothing)
        _AllowEvents = False
        Xl_ProductBrands1.Load(FilteredBrands(), _SelectionMode, _IncludeNullValue, oDefaultBrand, DisplayObsoletos:=_IncludeObsoletos)
        refrescaCategories(DefaultCategory)
        refrescaSkus(DefaultSku)
        _AllowEvents = True
    End Sub

    Private Sub refrescaCategories(Optional oDefaultCategory As DTOProductCategory = Nothing)
        _AllowEvents = False
        Dim values As New List(Of DTOProductCategory)
        If CurrentBrand() IsNot Nothing Then values = CurrentBrand.categories
        Xl_ProductCategories1.load(values, _SelectionMode, _IncludeNullValue, oDefaultCategory, DisplayObsoletos:=_IncludeObsoletos)
        refrescaSkus(DefaultSku)
        _AllowEvents = True
    End Sub

    Private Sub refrescaSkus(Optional oDefaultSku As DTOProductSku = Nothing)
        _AllowEvents = False
        Dim values As New List(Of DTOProductSku)
        If CurrentCategory() IsNot Nothing Then values = CurrentCategory.skus
        Xl_ProductSkus1.Load(values, _SelectionMode, _IncludeNullValue, oDefaultSku, DisplayObsoletos:=_IncludeObsoletos)
        _AllowEvents = True
    End Sub

    Private Function CurrentBrand() As DTOProductBrand
        Return Xl_ProductBrands1.Value
    End Function

    Private Function CurrentCategory() As DTOProductCategory
        Return Xl_ProductCategories1.Value
    End Function

    Private Function CurrentSku() As DTOProductSku
        Return Xl_ProductSkus1.Value
    End Function

    Private Function DefaultBrand() As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing
        If TypeOf _DefaultProduct Is DTOProductBrand Then
            retval = _DefaultProduct
        ElseIf TypeOf _DefaultProduct Is DTOProductCategory Then
            retval = DirectCast(_DefaultProduct, DTOProductCategory).Brand
        ElseIf TypeOf _DefaultProduct Is DTOProductSku Then
            retval = DirectCast(_DefaultProduct, DTOProductSku).Category.Brand
        End If
        Return retval
    End Function

    Private Function DefaultCategory() As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing
        If _DefaultProduct IsNot Nothing Then
            If TypeOf _DefaultProduct Is DTOProductCategory Then
                retval = _DefaultProduct
            ElseIf TypeOf _DefaultProduct Is DTOProductSku Then
                retval = DirectCast(_DefaultProduct, DTOProductSku).category
            End If
        End If
        Return retval
    End Function

    Private Function DefaultSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        If _DefaultProduct IsNot Nothing Then
            If TypeOf _DefaultProduct Is DTOProductSku Then
                retval = _DefaultProduct
            End If
        End If
        Return retval
    End Function

    Private Sub Xl_Product_OnItemSelected(sender As Object, e As MatEventArgs) Handles _
        Xl_ProductBrands1.OnItemSelected,
         Xl_ProductCategories1.OnItemSelected,
          Xl_ProductSkus1.onItemSelected

        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub



    Private Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        If _AllowEvents Then
            refrescaCategories()
            refrescaSkus()
        End If
    End Sub

    Private Sub Xl_ProductCategories1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.ValueChanged
        If _AllowEvents Then
            refrescaSkus()
        End If
    End Sub

    Private Async Sub InclouObsoletsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles _
            InclouObsoletsToolStripMenuItem.Click,
             Xl_ProductBrands1.RequestToToggleObsoletos,
              Xl_ProductCategories1.RequestToToggleObsoletos,
               Xl_ProductSkus1.RequestToToggleObsoletos

        _IncludeObsoletos = Not _IncludeObsoletos
        _DefaultProduct = CurrentSku()
        Await reloadBrands()
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        refrescaBrands()
    End Sub

    Private Function FilteredBrands() As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim searchkey As String = Xl_TextboxSearch1.Value.ToLower
        If searchkey.Length < 4 Then
            retval = _Brands
        Else

            For Each oBrand In _Brands
                Dim pBrand As DTOProductBrand = Nothing
                For Each oCategory In oBrand.Categories
                    Dim pCategory As DTOProductCategory = Nothing
                    For Each oSku In oCategory.Skus
                        If oSku.matches(searchkey) Then
                            If pCategory Is Nothing Then
                                If pBrand Is Nothing Then
                                    pBrand = oBrand.clon
                                    pBrand.Categories = New List(Of DTOProductCategory)
                                    retval.Add(pBrand)
                                End If
                                pCategory = oCategory.Clon
                                pCategory.Skus = New List(Of DTOProductSku)
                                pBrand.Categories.Add(pCategory)
                            End If
                            pCategory.Skus.Add(oSku)
                        End If
                    Next
                Next
            Next
        End If
        Return retval
    End Function

End Class