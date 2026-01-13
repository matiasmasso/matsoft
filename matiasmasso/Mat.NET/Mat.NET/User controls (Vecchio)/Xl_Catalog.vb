Public Class Xl_Catalog
    Private _Catalog As ProductCatalog
    Private _SelectedProduct As DTOProduct0
    Private _AllowEvents As Boolean

    Public Event OnItemSelected(sender As Object, e As MatEventArgs)
    Public Event ValueChanged(sender As Object, e As MatEventArgs)


    Public Shadows Sub Load(oCatalog As ProductCatalog, Optional oSelectedProduct As DTOProduct0 = Nothing)
        _Catalog = oCatalog
        _SelectedProduct = oSelectedProduct
        LoadBrands()
        LoadCategories()
        'LoadSkus()
        _AllowEvents = True
    End Sub

    Private Sub LoadBrands()
        Dim oBrands As List(Of ProductBrand) = _Catalog.Brands
        Xl_ProductBrands1.Load(oBrands, bll.dEFAULTS.SelectionModes.Selection)
    End Sub

    Private Sub LoadCategories()
        Dim oBrand As ProductBrand = CurrentBrand()
        If oBrand Is Nothing Then
            Xl_ProductCategories1.Load(New List(Of ProductCategory))
        Else
            Dim oCategories As List(Of ProductCategory) = oBrand.Categories
            Xl_ProductCategories1.Load(oCategories, bll.dEFAULTS.SelectionModes.Selection)
        End If
    End Sub

    Private Sub LoadSkus()
        Dim oCategory As ProductCategory = CurrentCategory
        If oCategory Is Nothing Then
            Xl_ProductSkus1.Load(New List(Of ProductSku))
        Else
            Dim oSkus As List(Of ProductSku) = oCategory.ProductSkus
            Xl_ProductSkus1.Load(oSkus, bll.dEFAULTS.SelectionModes.Selection)
        End If
    End Sub

    Private Function CurrentBrand() As ProductBrand
        Dim retval As ProductBrand = Xl_ProductBrands1.Value
        Return retval
    End Function

    Private Function CurrentCategory() As ProductCategory
        Dim retval As ProductCategory = Xl_ProductCategories1.Value
        Return retval
    End Function

    Private Function CurrentSku() As ProductSku
        Dim retval As ProductSku = Xl_ProductSkus1.Value
        Return retval
    End Function

    Private Sub Xl_ProductBrands1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.OnItemSelected
        If e.Argument Is Nothing Then
            RaiseEvent OnItemSelected(Me, MatEventArgs.Empty)
        Else
            Dim oBrand As ProductBrand = e.Argument
            Dim oProductbase As New DTOProduct0(oBrand)
            RaiseEvent OnItemSelected(Me, New MatEventArgs(oProductbase))
        End If
    End Sub

    Private Sub Xl_ProductCategories1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.onItemSelected
        Dim oCategory As ProductCategory = e.Argument
        Dim oProductbase As New DTOProduct0(oCategory)
        RaiseEvent OnItemSelected(Me, New MatEventArgs(oProductbase))
    End Sub

    Private Sub Xl_ProductSkus1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_ProductSkus1.onItemSelected
        Dim oSku As ProductSku = e.Argument
        Dim oProductbase As New DTOProduct0(oSku)
        RaiseEvent OnItemSelected(Me, New MatEventArgs(oProductbase))
    End Sub

    Private Sub Xl_ProductBrands1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductBrands1.ValueChanged
        If _AllowEvents Then
            Dim oBrand As ProductBrand = e.Argument
            If e.Argument IsNot Nothing Then
                _SelectedProduct = New DTOProduct0(oBrand)
            End If
            LoadCategories()
            LoadSkus()
            RaiseEvent ValueChanged(Me, e.Argument)
        End If
    End Sub

    Private Sub Xl_ProductCategories1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductCategories1.ValueChanged
        If _AllowEvents Then
            Dim oCategory As ProductCategory = e.Argument
            If e.Argument IsNot Nothing Then
                _SelectedProduct = New DTOProduct0(oCategory)
            End If
            LoadSkus()
            RaiseEvent ValueChanged(Me, e.Argument)
        End If
    End Sub

    Private Sub Xl_ProductSkus1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_ProductSkus1.ValueChanged
        If _AllowEvents Then
            Dim oSku As ProductSku = e.Argument
            _SelectedProduct = New DTOProduct0(oSku)
            RaiseEvent ValueChanged(Me, e.Argument)
        End If
    End Sub

End Class
