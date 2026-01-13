Public Class Frm_ProductBrands

    Private _Brands As List(Of DTOProductBrand)
    Private _IncludeObsoletos As Boolean
    Private _DefaultProduct As DTOProduct
    Private _SelectionMode As DTOProduct.SelectionModes
    Private _IncludeNullValue As Boolean
    Private _AllowEvents = True

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(oSelectionMode As DTOProduct.SelectionModes, Optional oDefaultProduct As DTOProduct = Nothing, Optional IncludeObsoletos As Boolean = False)
        MyBase.New
        InitializeComponent()
        _SelectionMode = oSelectionMode
        _IncludeObsoletos = IncludeObsoletos
    End Sub

    Private Async Sub Frm_ProductSkus_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await reloadBrands()
    End Sub

    Private Async Function reloadBrands() As Task
        Dim exs As New List(Of Exception)
        _Brands = Await FEB2.ProductSkus.CompactTree(exs, GlobalVariables.Emp, Current.Session.Lang, _IncludeObsoletos)
        If exs.Count = 0 Then
            refrescaBrands(DefaultBrand)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub refrescaBrands(Optional oDefaultBrand As DTOProductBrand = Nothing)
        _AllowEvents = False
        Xl_ProductBrands1.Load(_Brands, _SelectionMode, _IncludeNullValue, oDefaultBrand)
        _AllowEvents = True
    End Sub

    Private Function CurrentBrand() As DTOProductBrand
        Return Xl_ProductBrands1.Value
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
        Xl_ProductBrands1.OnItemSelected

        RaiseEvent OnItemSelected(Me, e)
    End Sub


    Private Async Sub InclouObsoletsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles _
             Xl_ProductBrands1.RequestToToggleObsoletos

        _IncludeObsoletos = Not _IncludeObsoletos
        _DefaultProduct = CurrentBrand()
        Await reloadBrands()
    End Sub

End Class