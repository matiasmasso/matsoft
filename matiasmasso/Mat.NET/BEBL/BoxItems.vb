Public Class BoxItems
    Shared Function BrandVideos(oLang As DTOLang) As List(Of DTOBoxItem) 'surt a www.matiasmasso.es/videos
        Dim retval As List(Of DTOBoxItem) = BoxItemsLoader.BrandVideos(oLang)
        For Each oBoxItem As DTOBoxItem In retval
            With oBoxItem
                Dim oBrand As DTOProductBrand = .Tag
                .ImageUrl = BEBL.ProductBrand.LogoUrl(oBrand, True)
                .NavigateUrl = oLang.Domain().Url(oBrand.urlSegment(DTOProduct.Tabs.videos, oLang))
            End With
        Next
        Return retval
    End Function

    Shared Function FromEBooks(oEmp As DTOEmp) As List(Of DTOBoxItem)
        Dim retval As New List(Of DTOBoxItem)
        For Each oBrand As DTOProductBrand In DAL.ProductBrandsLoader.All(oEmp, False)
            'For Each oBrand As ProductBrand In DAL.ProductBrandsLoader.All(oUser)
            If oBrand.EnabledxPro Then
                Dim oItem As New DTOBoxItem
                With oItem
                    .ImageUrl = BEBL.ProductBrand.LogoUrl(oBrand, True)
                    .NavigateUrl = BEBL.ProductBrand.EPubUrl(oBrand, True)
                End With
                retval.Add(oItem)
            End If
        Next
        Return retval
    End Function


End Class
