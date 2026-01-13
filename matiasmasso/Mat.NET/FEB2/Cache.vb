Imports System.Threading

Public Class Cache


    'refresh cache from server if current cache is outdated
    Public Shared Async Function Fetch(exs As List(Of Exception), oUser As DTOUser) As Task(Of Models.ClientCache)
        Dim retval = Await Fetch(exs, oUser.Emp)
        Return retval
    End Function
    Public Shared Async Function Fetch(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of Models.ClientCache)
        Dim retval = GlobalVariables.Cache(oEmp)
        Try
            Dim oCacheResponse = Await CheckCacheFromServer(exs, retval)
            Dim needsToUpdate = oCacheResponse IsNot Nothing AndAlso oCacheResponse.LastUpdates.Count > 0
            If needsToUpdate Then UpdateTables(retval, oCacheResponse)
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Private Shared Async Function CheckCacheFromServer(exs As List(Of Exception), oCurrentCache As Models.ClientCache) As Task(Of Models.ServerCache)
        Dim oCacheRequest = oCurrentCache.Request() 'build a request with last dates each client table was updated
        Dim retval = Await Api.PostRequest(Of Models.ServerCache, Models.ServerCache)(oCacheRequest, exs, Nothing, "cache/checkForClientUpdates")
        Return retval
    End Function

    'from website global.asax; must be sync
    Public Shared Function FetchSync(exs As List(Of Exception), oEmp As DTOEmp) As Models.ClientCache
        Dim retval = GlobalVariables.Cache(oEmp)
        Try
            Dim oCacheRequest = retval.Request() 'build a request with last dates each client table was updated
            Dim oCacheResponse = Api.PostRequestSync(Of Models.ServerCache, Models.ServerCache)(oCacheRequest, exs, Nothing, "cache/checkForClientUpdates")
            Dim needsToUpdate = oCacheResponse IsNot Nothing AndAlso oCacheResponse.LastUpdates.Count > 0
            If needsToUpdate Then UpdateTables(retval, oCacheResponse)
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Private Shared Sub UpdateTables(ByRef oCurrentCache As Models.ClientCache, oCacheResponse As Models.ServerCache)
        Dim hasToRefreshTree As Boolean
        oCurrentCache.IsLoading = True
        'the response returns only the updated tables with their new dates if any
        For Each tableToUpdate In oCacheResponse.LastUpdates
            Select Case tableToUpdate.Table
                Case Models.ServerCache.Tables.Brands
                    Dim oMinified As List(Of Dictionary(Of String, Object)) = oCacheResponse.Brands
                    oCurrentCache.Brands = oMinified.Select(Function(x) Models.Min.ProductBrand.Expand(x)).ToList()
                    hasToRefreshTree = True
                Case Models.ServerCache.Tables.Depts
                    Dim oMinified As List(Of Dictionary(Of String, Object)) = oCacheResponse.Depts
                    oCurrentCache.Depts = oMinified.Select(Function(x) Models.Min.ProductDept.Expand(x)).ToList()
                    hasToRefreshTree = True
                Case Models.ServerCache.Tables.Categories
                    Dim oMinified As List(Of Dictionary(Of String, Object)) = oCacheResponse.Categories
                    oCurrentCache.Categories = oMinified.Select(Function(x) Models.Min.ProductCategory.Expand(x)).ToList()
                    hasToRefreshTree = True
                Case Models.ServerCache.Tables.Skus
                    Dim oMinified As List(Of Dictionary(Of String, Object)) = oCacheResponse.Skus
                    oCurrentCache.Skus = oMinified.Select(Function(x) Models.Min.ProductSku.Expand(x)).ToList()
                    hasToRefreshTree = True
                Case Models.ServerCache.Tables.SkuBundles
                    Dim oMinified As List(Of Dictionary(Of String, Object)) = oCacheResponse.SkuBundles
                    oCurrentCache.SkuBundles = oMinified.Select(Function(x) Models.Min.SkuBundle.Expand(x)).ToList()
                    hasToRefreshTree = True
                Case Models.ServerCache.Tables.Stocks
                    Dim oMinified As List(Of Dictionary(Of String, Object)) = oCacheResponse.Stocks
                    oCurrentCache.Stocks = oMinified.Select(Function(x) Models.Min.SkuStock.Expand(x)).ToList()
                Case Models.ServerCache.Tables.RetailPrices
                    oCurrentCache.RetailPrices = oCacheResponse.RetailPrices
                Case Models.ServerCache.Tables.SkuPrevisions
                    oCurrentCache.SkuPrevisions = oCacheResponse.SkuPrevisions
                Case Models.ServerCache.Tables.ProductUrls
                    oCurrentCache.ProductUrls = oCacheResponse.ProductUrls
                Case Models.ServerCache.Tables.ProductPlugins
                    oCurrentCache.ProductPlugins = DTOProductPlugin.ExpandPlugins(oCacheResponse.ProductPlugins)
            End Select
            oCurrentCache.UpdateTable(tableToUpdate)
        Next

        If hasToRefreshTree Then
            'merge catalog objects to build the tree
            Dim worker As New Thread(New ThreadStart(AddressOf RefreshCatalogTree))
            worker.Start()
        Else
            oCurrentCache.FinishedLoading()
        End If
    End Sub

    Private Shared Sub RefreshCatalogTree()

        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        Dim oCache = GlobalVariables.Cache(oEmp)


        'Build the tree starting with Product Brands at the top level
        For Each oBrand In oCache.Brands
            oBrand.Emp = oEmp
            oBrand.Categories = New List(Of DTOProductCategory)
            oBrand.Depts = New DTODept.Collection
        Next

        For Each oDept In oCache.Depts
            oDept.Brand = oCache.Brands.FirstOrDefault(Function(x) x.Guid.Equals(oDept.Brand.Guid))
            oDept.Brand.Depts.Add(oDept)
        Next

        For Each oCategory In oCache.Categories
            oCategory.Brand = oCache.Brands.FirstOrDefault(Function(x) x.Guid.Equals(oCategory.Brand.Guid))
            oCategory.Brand.Categories.Add(oCategory)
            oCategory.Skus = New List(Of DTOProductSku)
        Next

        For Each oSku In oCache.Skus
            oSku.Category = oCache.Categories.FirstOrDefault(Function(x) x.Guid.Equals(oSku.Category.Guid))
            If oSku.Category IsNot Nothing Then

                oSku.Category.Skus.Add(oSku)
                If oCache.SkuBundles.Any(Function(x) x.Guid.Equals(oSku.Guid)) Then
                    Dim oSkuBundle As DTOProductSku = oCache.SkuBundles.FirstOrDefault(Function(x) x.Guid.Equals(oSku.Guid))
                    Dim DcRrpp = oSkuBundle.BundleSkus.Sum(Function(x) x.Qty * x.Rrpp)
                    If oCache.RetailPrices.ContainsKey(oSkuBundle.Guid) Then oCache.RetailPrices.Remove(oSkuBundle.Guid)
                    oCache.RetailPrices.Add(oSkuBundle.Guid, DcRrpp.ToString("F", System.Globalization.CultureInfo.InvariantCulture))

                    oSku.IsBundle = True
                    oSku.BundleSkus = New List(Of DTOSkuBundle)
                    For Each oBundleSku In oSkuBundle.BundleSkus
                        oBundleSku.Sku = oCache.FindSku(oBundleSku.Sku.Guid)
                        oSku.BundleSkus.Add(oBundleSku)
                    Next
                End If
            End If
        Next

        oCache.FinishedLoading()

        SetProductUrls(oCache)
    End Sub

    Private Shared Sub SetProductUrls(oCache As Models.ClientCache)
        Dim oCanonicalUrlSegments As List(Of DTOLangText) = CanonicalUrlSegments(oCache)

        For Each oBrand In oCache.Brands
            Dim brandSegment = oCanonicalUrlSegments.FirstOrDefault(Function(x) x.Guid.Equals(oBrand.Guid))
            oBrand.UrlCanonicas = DTOUrl.Factory(brandSegment)
            For Each oDept In oBrand.Depts
                Dim deptSegment = oCanonicalUrlSegments.FirstOrDefault(Function(x) x.Guid.Equals(oDept.Guid))
                oDept.UrlCanonicas = DTOUrl.Factory(brandSegment, deptSegment)
            Next
            For Each oCategory In oBrand.Categories
                Dim categorySegment = oCanonicalUrlSegments.FirstOrDefault(Function(x) x.Guid.Equals(oCategory.Guid))
                oCategory.UrlCanonicas = DTOUrl.Factory(brandSegment, categorySegment)
                For Each oSku In oCategory.Skus
                    Dim skuSegment = oCanonicalUrlSegments.FirstOrDefault(Function(x) x.Guid.Equals(oSku.Guid))
                    oSku.UrlCanonicas = DTOUrl.Factory(brandSegment, categorySegment, skuSegment)
                Next
            Next
        Next
    End Sub

    Private Shared Function CanonicalUrlSegments(oCache As Models.ClientCache) As List(Of DTOLangText)
        'Organize canonical url segments
        Dim retval As New List(Of DTOLangText)
        Dim oLangText As New DTOLangText
        For Each langUrl In oCache.ProductUrls
            If Not Models.Min.ProductUrl.ProductGuid(langUrl).Equals(oLangText.Guid) Then
                Dim oGuid = Models.Min.ProductUrl.ProductGuid(langUrl)
                oLangText = New DTOLangText(oGuid, DTOLangText.Srcs.ProductUrl)
                retval.Add(oLangText)
            End If
            oLangText.SetText(Models.Min.ProductUrl.lang(langUrl), Models.Min.ProductUrl.Segment(langUrl))
        Next
        Return retval
    End Function


End Class
