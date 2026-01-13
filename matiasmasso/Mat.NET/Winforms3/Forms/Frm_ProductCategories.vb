Public Class Frm_ProductCategories

    Private _Brands As List(Of DTOProductBrand)
    Private _IncludeObsoletos As Boolean
    Private _DefaultProduct As DTOProduct
    Private _SelectionMode As DTOProduct.SelectionModes
    Private _IncludeNullValue As Boolean
    Private _AllowEvents = True

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oSelectionMode As DTOProduct.SelectionModes, Optional oDefaultProduct As DTOProduct = Nothing, Optional IncludeObsoletos As Boolean = False, Optional oCatalog As List(Of DTOProductBrand) = Nothing)
        MyBase.New
        InitializeComponent()
        _Brands = oCatalog
        _SelectionMode = oSelectionMode
        _IncludeObsoletos = IncludeObsoletos
        _DefaultProduct = oDefaultProduct
    End Sub

    Private Async Sub Frm_ProductSkus_Load(sender As Object, e As EventArgs) Handles Me.Load
        If _Brands Is Nothing Then
            Await reloadBrands()
        Else
            ProgressBar1.Visible = False
            refrescaBrands(DefaultBrand)
        End If
    End Sub

    Private Async Function reloadBrands() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        _Brands = Await FEB.ProductCategories.CompactTree(exs, GlobalVariables.Emp, Current.Session.Lang)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            refrescaBrands(DefaultBrand)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub refrescaBrands(Optional oDefaultBrand As DTOProductBrand = Nothing)
        _AllowEvents = False
        Xl_ProductBrands1.Load(FilteredBrands(), _SelectionMode, _IncludeNullValue, oDefaultBrand)
        refrescaCategories(DefaultCategory)
        _AllowEvents = True
    End Sub

    Private Sub refrescaCategories(Optional oDefaultCategory As DTOProductCategory = Nothing)
        _AllowEvents = False
        Dim values = CurrentBrand().Categories
        Xl_ProductCategories1.load(values, _SelectionMode, _IncludeNullValue, oDefaultCategory)
        _AllowEvents = True
    End Sub

    Private Function CurrentBrand() As DTOProductBrand
        Return Xl_ProductBrands1.Value
    End Function

    Private Function CurrentCategory() As DTOProductCategory
        Return Xl_ProductCategories1.Value
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
        If TypeOf _DefaultProduct Is DTOProductCategory Then
            retval = _DefaultProduct
        ElseIf TypeOf _DefaultProduct Is DTOProductSku Then
            retval = DirectCast(_DefaultProduct, DTOProductSku).Category
        End If
        Return retval
    End Function

    Private Function DefaultSku() As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        If TypeOf _DefaultProduct Is DTOProductSku Then
            retval = _DefaultProduct
        End If
        Return retval
    End Function

    Private Sub Xl_Product_OnItemSelected(sender As Object, e As MatEventArgs) Handles _
        Xl_ProductBrands1.OnItemSelected,
         Xl_ProductCategories1.OnItemSelected

        RaiseEvent OnItemSelected(Me, e)
        Me.Close()
    End Sub



    Private Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        If _AllowEvents Then
            refrescaCategories()
        End If
    End Sub


    Private Async Sub InclouObsoletsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles _
            InclouObsoletsToolStripMenuItem.Click,
             Xl_ProductBrands1.RequestToToggleObsoletos,
              Xl_ProductCategories1.RequestToToggleObsoletos

        _IncludeObsoletos = Not _IncludeObsoletos
        _DefaultProduct = CurrentCategory()
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