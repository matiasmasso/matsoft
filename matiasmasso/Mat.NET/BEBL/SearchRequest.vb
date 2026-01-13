Public Class SearchRequest

    Shared Function Load(ByRef value As DTOSearchRequest, exs As List(Of Exception)) As Boolean
        With value
            If .User IsNot Nothing Then
                Select Case .User.Rol.Id
                    Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                        .Contact = BEBL.User.GetRep(.User)
                End Select
            End If

            If Log(value, exs) Then
                .Results = Results(value, exs)
            End If
        End With
        Return (exs.Count = 0)
    End Function

    Shared Function Log(oRequest As DTOSearchRequest, exs As List(Of Exception)) As Boolean
        Try
            If oRequest.User IsNot Nothing Then
                If Not oRequest.User.Rol.IsStaff Then
                    SearchRequestLoader.Log(oRequest)
                End If
            End If
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return exs.Count = 0
    End Function

    Shared Function Results(oSearchRequest As DTOSearchRequest, exs As List(Of Exception)) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)
        If oSearchRequest.User IsNot Nothing Then
            retval.AddRange(FromGeoPro(oSearchRequest))
            retval.AddRange(FromContactPro(oSearchRequest))
        End If
        retval.AddRange(FromNoticiaKeywords(oSearchRequest))
        retval.AddRange(FromBlogPosts(oSearchRequest))
        retval.AddRange(FromGeo(oSearchRequest))
        retval.AddRange(FromCnap(oSearchRequest))
        retval.AddRange(FromProducts(oSearchRequest))
        Return retval
    End Function

    Shared Function FromGeo(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As List(Of DTOSearchResult) = SearchResultsLoader.FromGeo(oSearchRequest)
        For Each item In retval
            item.Url = UrlHelper.Factory(True, DTOProductBrand.UrlSegment(item.BaseGuid), System.Web.HttpUtility.UrlEncode(oSearchRequest.SearchKey))
            'item.Url = BLLProductBrand.Url(item.BaseGuid) & "/" & System.Web.HttpUtility.UrlEncode(oSearchRequest.SearchKey)
        Next
        Return retval
    End Function

    Shared Function FromGeoPro(oSearchRequest As DTOSearchRequest) As List(Of DTO.DTOSearchResult)
        Dim retval As List(Of DTOSearchResult) = SearchResultsLoader.FromGeoPro(oSearchRequest)
        For Each item In retval
            item.Url = UrlHelper.Factory(True, "area/areacustomers", item.BaseGuid.Guid.ToString)
            'retval = Defaults.UrlFromSegments(AbsoluteUrl, "area", "areacustomers", oArea.Guid.ToString)
            'item.Url = BLLArea.UrlCustomers(item.BaseGuid)
        Next
        Return retval
    End Function
    Shared Function FromContactPro(oSearchRequest As DTOSearchRequest) As List(Of DTO.DTOSearchResult)
        Dim retval As List(Of DTOSearchResult) = SearchResultsLoader.FromContactPro(oSearchRequest)
        For Each item In retval
            item.Url = UrlHelper.Factory(True, "contacto", item.BaseGuid.Guid.ToString)
            'item.Url = BLLContact.Url(item.BaseGuid)
        Next
        Return retval
    End Function

    Shared Function FromProducts(oSearchRequest As DTOSearchRequest) As List(Of DTO.DTOSearchResult)
        Dim retval As List(Of DTOSearchResult) = SearchResultsLoader.FromProducts(oSearchRequest)
        For Each item In retval
            Select Case item.Cod
                Case DTOSearchResult.Cods.Brand
                    item.Url = UrlHelper.Factory(True, DTOProductBrand.UrlSegment(item.BaseGuid))
                Case DTOSearchResult.Cods.Category
                    item.Url = UrlHelper.Factory(True, DTOProductBrand.UrlSegment(item.BaseGuid), DTOProductCategory.UrlSegment(item.BaseGuid))
                Case DTOSearchResult.Cods.Sku
                    item.Url = UrlHelper.Factory(True, DTOProductBrand.UrlSegment(item.BaseGuid), DTOProductCategory.UrlSegment(item.BaseGuid), DTOProductSku.UrlSegment(item.BaseGuid))
            End Select
        Next
        Return retval
    End Function

    Shared Function FromNoticiaKeywords(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As List(Of DTOSearchResult) = SearchResultsLoader.FromNoticiaKeywords(oSearchRequest)
        For Each item In retval
            With item
                '.Url = BLLNoticia.UrlFriendly(item.BaseGuid)
            End With
        Next
        Return retval
    End Function


    Shared Function FromBlogPosts(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim oPosts As List(Of DTOBlogPost) = WpPostsLoader.FromTag(oSearchRequest.SearchKey)
        Dim retval As New List(Of DTOSearchResult)
        For Each oBlogPost As DTOBlogPost In oPosts
            Dim oItem As New DTOSearchResult
            With oItem
                .Caption = String.Format("{0:dd/MM/yy} {1}", oBlogPost.fch, oBlogPost.Title)
                .Url = oBlogPost.VirtualPath
                .Cod = DTOSearchResult.Cods.BlogPost
            End With
            retval.Add(oItem)
        Next
        Return retval
    End Function

    Shared Function FromCnap(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)

        Dim oProducts = CnapsLoader.All(oSearchRequest.SearchKey)
        If oProducts IsNot Nothing Then
            For Each oProduct In oProducts
                Dim oItem As New DTOSearchResult
                With oItem
                    .Caption = oProduct.FullNom(oSearchRequest.Lang)
                    .Url = UrlHelper.Factory(True, "product", oProduct.Guid.ToString)
                End With
                retval.Add(oItem)
            Next
        End If
        Return retval
    End Function


End Class


