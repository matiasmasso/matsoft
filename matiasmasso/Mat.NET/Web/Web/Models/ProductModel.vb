Public Class ProductModel
    Property Title As String
    Property Text As String
    Property Excerpt As String

    Property NavViewModel As NavViewModel

    Property Product As DTOProduct
    Property Tag As String

    Property SideMenu As ProductMenuModel
    Property ImageUrl As String

    Property BrandLogoUrl As String

    Property Retail As DTOAmt

    Property GalleryMode As GalleryModes

    Property Filters As List(Of DTOFilter)

    Property CheckedFilterItems As List(Of DTOFilter.Item)
    Property Items As ImageBoxViewModel.Collection

    Public Enum GalleryModes
        NotSet
        Categories
        Depts
    End Enum


    Public Sub New()
        MyBase.New
        _Items = New ImageBoxViewModel.Collection
        _Filters = New DTOFilter.Collection
        _CheckedFilterItems = New DTOFilter.Item.Collection
    End Sub

    Shared Function Factory(oBrand As DTOProductBrand, oLang As DTOLang, oTab As DTOProduct.Tabs) As ProductModel
        Dim retval As New ProductModel
        With retval
            .Product = oBrand
            .BrandLogoUrl = oBrand.LogoUrl()
            .Title = oBrand.Nom.tradueix(ContextHelper.Lang())
            .Tag = oBrand.Guid.ToString
            .NavViewModel = NavViewModel.Factory(oBrand, oTab, oLang)
        End With
        Return retval
    End Function

    Shared Function Factory(oDept As DTODept, oLang As DTOLang, oTab As DTOProduct.Tabs) As ProductModel
        Dim retval As New ProductModel
        With retval
            .Product = oDept
            .BrandLogoUrl = oDept.Brand.LogoUrl()
            .Title = oDept.Nom.tradueix(ContextHelper.Lang())
            .Tag = oDept.Guid.ToString
            .NavViewModel = NavViewModel.Factory(oDept, oTab, oLang)
        End With
        Return retval
    End Function

    Shared Function ProductFilters(oAllFilters As List(Of DTOFilter), oProductFilterItems As List(Of DTOFilter.Item)) As DTOFilter.Collection
        Dim retval As New DTOFilter.Collection
        For Each oFilter In oAllFilters
            For Each item In oFilter.Items
                Dim itemGuid = item.Guid
                If oProductFilterItems.Any(Function(x) x.Guid.Equals(itemGuid)) Then
                    If Not retval.Any(Function(x) x.Guid.Equals(oFilter.Guid)) Then
                        Dim oClon As New DTOFilter(oFilter.Guid)
                        oClon.LangText = oFilter.LangText
                        retval.Add(oClon)
                    End If
                    retval.Last().Items.Add(item)
                End If
            Next
        Next
        Return retval
    End Function

    Public Function BrandNom() As String
        Return DTOProduct.Brand(_Product).Nom.tradueix(ContextHelper.Lang)
    End Function

    Public Function BrandUrl() As String
        Return DTOProduct.Brand(_Product).getUrl()
    End Function


    Public Function CategoryNom(oLang As DTOLang) As String
        Return DTOProduct.Category(_Product).Nom.tradueix(oLang)
    End Function

    Public Function CategoryUrl() As String
        Return DTOProduct.Category(_Product).getUrl()
    End Function

    Public Function SkuNom(oLang As DTOLang) As String
        Return DTOProduct.Sku(_Product).Nom.tradueix(oLang)
    End Function

    Public Function TextHasImages() As Boolean
        Return _Text.Contains("<img ")
    End Function

    Public Function FacebookImgUrl() As String
        Return DTOFacebook.FbImg(_Text)
    End Function
End Class
