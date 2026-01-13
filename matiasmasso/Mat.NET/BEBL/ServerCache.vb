Public Class ServerCache
    Private Shared _isBusy As Boolean
    Private Shared _caches As List(Of Models.ServerCache)

    Shared Sub Initialize(oEmps As List(Of DTOEmp))
        Dim oEmp = oEmps.FirstOrDefault(Function(x) x.Id = DTOEmp.Ids.MatiasMasso)
        Dim oCache = Models.ServerCache.Factory(oEmp)
        _caches = New List(Of Models.ServerCache)
        _caches.Add(oCache)

        'first of all sync server cache with database
        CheckForServerUpdates(oCache)

    End Sub

    Shared Function ForEmp(oId As DTOEmp.Ids) As Models.ServerCache
        Dim retval = _caches.FirstOrDefault(Function(x) x.Emp.Id = oId)
        Return retval
    End Function

    Shared Sub NotifyUpdate(table As Models.ServerCache.Tables)
        Dim oCache = ForEmp(DTOEmp.Ids.MatiasMasso)
        oCache.DirtyTables.Add(table)
    End Sub

    'Syncs server cache tables if needed
    Shared Sub CheckForServerUpdates(ByRef oServerCache As Models.ServerCache)
        'Clean dirty tables so any updates may be reflected in the meantime
        Dim oDirtyTables As New HashSet(Of Models.ServerCache.Tables)
        For Each oTable In oServerCache.DirtyTables
            oDirtyTables.Add(oTable)
        Next
        oServerCache.DirtyTables.Clear()

        'Include tables with empty LastUpdate
        For Each oLastUpdate In oServerCache.LastUpdates.Where(Function(x) x.Fch = DateTime.MinValue).ToList()
            oDirtyTables.Add(oLastUpdate.Table)
        Next

        For Each table In oDirtyTables
            Dim oLastUpdate = oServerCache.LastUpdates.FirstOrDefault(Function(x) x.Table = table)
            If oLastUpdate IsNot Nothing Then
                oLastUpdate.Fch = DTO.GlobalVariables.Now()
                Select Case table
                    Case Models.ServerCache.Tables.Brands
                        Dim oBrands = ProductBrandsLoader.All(oServerCache.Emp, IncludeObsolets:=True)
                        oServerCache.Brands = oBrands.Select(Function(x) Models.Min.ProductBrand.Factory(x)).ToList()
                    Case Models.ServerCache.Tables.Depts
                        Dim oDepts = DeptsLoader.Cache(oServerCache.Emp)
                        oServerCache.Depts = oDepts.Select(Function(x) Models.Min.ProductDept.Factory(x)).ToList()
                    Case Models.ServerCache.Tables.Categories
                        Dim oCategories = ProductCategoriesLoader.All(oServerCache.Emp)
                        oServerCache.Categories = oCategories.Select(Function(x) Models.Min.ProductCategory.Factory(x)).ToList()
                    Case Models.ServerCache.Tables.DeptCategories
                        oServerCache.DeptCategories = DeptsLoader.Categories(oServerCache.Emp)
                    Case Models.ServerCache.Tables.ProductSpares
                        oServerCache.ProductSpares = ProductSparesLoader.all()
                    Case Models.ServerCache.Tables.Skus
                        Dim oSkus = ProductSkusLoader.All(oServerCache.Emp)
                        oServerCache.Skus = oSkus.Select(Function(x) Models.Min.ProductSku.Factory(x)).ToList()
                    Case Models.ServerCache.Tables.SkuBundles
                        Dim oSkuBundles = ProductSkuBundlesLoader.All(oServerCache.Emp)
                        oServerCache.SkuBundles = oSkuBundles.Select(Function(x) Models.Min.SkuBundle.Factory(x)).ToList()
                    Case Models.ServerCache.Tables.Stocks
                        Dim oSkuStocks = CatalogLoader.SkuStocks(oServerCache.Emp.Mgz)
                        oServerCache.Stocks = oSkuStocks.Select(Function(x) Models.Min.SkuStock.Factory(x)).ToList()
                    Case Models.ServerCache.Tables.RetailPrices
                        oServerCache.RetailPrices = CatalogLoader.SkuPrices(oServerCache.Emp)
                    Case Models.ServerCache.Tables.SkuPrevisions
                        oServerCache.SkuPrevisions = CatalogLoader.SkuPrevisions(oServerCache.Emp)
                    Case Models.ServerCache.Tables.ProductUrls
                        oServerCache.ProductUrls = CatalogLoader.ProductUrls(oServerCache.Emp)
                    Case Models.ServerCache.Tables.ProductPlugins
                        oServerCache.ProductPlugins = ProductPluginsLoader.All()
                End Select
            End If
        Next
    End Sub


    'Loads fresh tables from server cache if newer
    Shared Function CheckForClientUpdates(ByRef oClientRequest As Models.ServerCache) As Models.ServerCache
        Dim retval As New Models.ServerCache()
        retval.Emp = oClientRequest.Emp

        'first of all sync server cache with database if needed
        Dim oServerCache = ForEmp(oClientRequest.Emp.Id)
        CheckForServerUpdates(oServerCache)

        'now we can compare client dates with refreshed server cache dates
        For Each oClientTable In oClientRequest.LastUpdates
            Dim oServerTable = oServerCache.LastUpdates.FirstOrDefault(Function(x) x.Table = oClientTable.Table)
            If oServerTable IsNot Nothing AndAlso oServerTable.Fch > oClientTable.Fch Then
                retval.LastUpdates.Add(oServerTable)
                Select Case oClientTable.Table
                    Case Models.ServerCache.Tables.Brands
                        retval.Brands = oServerCache.Brands
                    Case Models.ServerCache.Tables.Depts
                        retval.Depts = oServerCache.Depts
                    Case Models.ServerCache.Tables.Categories
                        retval.Categories = oServerCache.Categories
                    Case Models.ServerCache.Tables.DeptCategories
                        retval.DeptCategories = oServerCache.DeptCategories
                    Case Models.ServerCache.Tables.ProductSpares
                        retval.ProductSpares = oServerCache.ProductSpares
                    Case Models.ServerCache.Tables.Skus
                        retval.Skus = oServerCache.Skus
                    Case Models.ServerCache.Tables.SkuBundles
                        retval.SkuBundles = oServerCache.SkuBundles
                    Case Models.ServerCache.Tables.Stocks
                        retval.Stocks = oServerCache.Stocks
                    Case Models.ServerCache.Tables.RetailPrices
                        retval.RetailPrices = oServerCache.RetailPrices
                    Case Models.ServerCache.Tables.SkuPrevisions
                        retval.SkuPrevisions = oServerCache.SkuPrevisions
                    Case Models.ServerCache.Tables.ProductUrls
                        retval.ProductUrls = oServerCache.ProductUrls
                    Case Models.ServerCache.Tables.ProductPlugins
                        retval.ProductPlugins = oServerCache.ProductPlugins
                End Select
            End If
        Next
        Return retval
    End Function

    Shared Function Image(oCod As Models.ImageCache.Cods, oGuid As Guid) As Byte()
        Dim retval As Byte() = Nothing
        Dim oCache = ServerCache.ForEmp(DTOEmp.Ids.MatiasMasso)
        Dim item = oCache.Images.Find(oCod, oGuid)
        If item IsNot Nothing Then retval = item.Image
        Return retval
    End Function


    Shared Function AddImage(oCod As Models.ImageCache.Cods, oGuid As Guid, oImage As Byte()) As Models.ImageCache.Item
        Dim oCache = ServerCache.ForEmp(DTOEmp.Ids.MatiasMasso)
        Dim retval = oCache.Images.Add(oCod, oGuid, oImage)
        Return retval
    End Function

    Shared Sub ResetImage(oCod As Models.ImageCache.Cods, Optional oGuid As Nullable(Of Guid) = Nothing)
        Dim oCache = ServerCache.ForEmp(DTOEmp.Ids.MatiasMasso)
        If oGuid Is Nothing Then
            oCache.Images.Items.RemoveAll(Function(x) x.Cod = oCod)
        Else
            oCache.Images.Items.RemoveAll(Function(x) x.Cod = oCod And x.Guid.Equals(oGuid))
        End If
    End Sub

End Class
