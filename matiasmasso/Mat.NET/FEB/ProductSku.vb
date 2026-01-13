Imports Newtonsoft.Json.Linq

Public Class ProductSku
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid, Optional oMgz As DTOMgz = Nothing) As Task(Of DTOProductSku)
        If oMgz Is Nothing Then
            Return Await Api.Fetch(Of DTOProductSku)(exs, "ProductSku", oGuid.ToString())
        Else
            Return Await Api.Fetch(Of DTOProductSku)(exs, "ProductSku", oGuid.ToString, oMgz.Guid.ToString())
        End If
    End Function
    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid, Optional oMgz As DTOMgz = Nothing) As DTOProductSku
        If oMgz Is Nothing Then
            Return Api.FetchSync(Of DTOProductSku)(exs, "ProductSku", oGuid.ToString())
        Else
            Return Api.FetchSync(Of DTOProductSku)(exs, "ProductSku", oGuid.ToString, oMgz.Guid.ToString())
        End If
    End Function

    Shared Async Function FromNom(exs As List(Of Exception), oCategory As DTOProductCategory, Nom As String) As Task(Of DTOProductSku)
        Return Await Api.Execute(Of String, DTOProductSku)(Nom, exs, "ProductSku/fromNom", oCategory.Guid.ToString())
    End Function

    Shared Async Function FromId(exs As List(Of Exception), Id As Integer, oCustomer As DTOCustomer, oMgz As DTOMgz) As Task(Of DTOProductSku)
        Return Await Api.Fetch(Of DTOProductSku)(exs, "ProductSku/fromId", Id, oCustomer.Guid.ToString, oMgz.Guid.ToString)
    End Function

    Shared Async Function FromId(exs As List(Of Exception), oEmp As DTOEmp, Id As Integer) As Task(Of DTOProductSku)
        Return Await Api.Fetch(Of DTOProductSku)(exs, "ProductSku/fromId", oEmp.Id, Id)
    End Function

    Shared Async Function LoadFromCustomer(exs As List(Of Exception), oSku As DTOProductSku, oContact As DTOContact, oMgz As DTOMgz) As Task(Of DTOProductSku)
        Return Await Api.Fetch(Of DTOProductSku)(exs, "ProductSku", oSku.Guid.ToString(), oContact.Guid.ToString(), oMgz.Guid.ToString())
    End Function

    Shared Async Function FromEan(exs As List(Of Exception), oEan As DTOEan) As Task(Of DTOProductSku)
        Return Await Api.Fetch(Of DTOProductSku)(exs, "ProductSku/fromEan", oEan.Value)
    End Function

    Shared Function FromNomSync(exs As List(Of Exception), oCategory As DTOProductCategory, Nom As String) As DTOProductSku
        Return Api.ExecuteSync(Of String, DTOProductSku)(Nom, exs, "ProductSku/fromNom", oCategory.Guid.ToString())
    End Function

    Shared Async Function Image(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "productSku/Image", oSku.Guid.ToString())
    End Function

    Shared Async Function Thumbnail(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "productSku/Thumbnail", oSku.Guid.ToString)
    End Function

    Shared Async Function Thumbnail(exs As List(Of Exception), oSku As DTOProductSku, width As Integer, height As Integer) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "productSku/Thumbnail", oSku.Guid.ToString, width, height)
    End Function

    Shared Function ThumbnailSync(exs As List(Of Exception), oSku As DTOProductSku, width As Integer, height As Integer) As Byte()
        Return Api.FetchImageSync(exs, "productSku/Thumbnail", oSku.Guid.ToString, width, height)
    End Function

    Shared Function Load(ByRef oProductSku As DTOProductSku, exs As List(Of Exception), Optional IncludeImage As Boolean = False, Optional oMgz As DTOMgz = Nothing) As Boolean
        If Not oProductSku.IsLoaded And Not oProductSku.IsNew Then
            Dim pProductSku = FindSync(exs, oProductSku.Guid, oMgz)
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProductSku)(pProductSku, oProductSku, exs)
            End If
        End If
        If IncludeImage And oProductSku.ImageExists Then
            oProductSku.Image = Api.FetchImageSync(exs, "productSku/Image", oProductSku.Guid.ToString())
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function ImageSDSync(exs As List(Of Exception), oSku As DTOProductSku) As Byte()
        Dim retval = Api.FetchImageBytesSync(exs, "productSku/Image", oSku.Guid.ToString())
        Return retval
    End Function


    Shared Async Function Update(value As DTOProductSku, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Image IsNot Nothing Then
                oMultipart.AddFileContent("Image", value.Image)
            End If
            retval = Await Api.Upload(oMultipart, exs, "ProductSku")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oProductSku As DTOProductSku, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProductSku)(oProductSku, exs, "ProductSku")
    End Function


    Shared Async Function FromProveidor(exs As List(Of Exception), oProveidor As DTOContact, sRef As String) As Task(Of DTOProductSku)
        Return Await Api.Execute(Of String, DTOProductSku)(sRef, exs, "productSku/fromProveidor", oProveidor.Guid.ToString())
    End Function


    Shared Async Function Cost(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of DTOPriceListItem_Supplier)
        Dim retval = Await PriceListItemSupplier.GetPreusDeCost(exs, oSku)
        Return retval
    End Function

    Shared Async Function CostNet(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of DTOAmt)
        Dim retval As DTOAmt = Nothing
        Dim item = Await PriceListItemSupplier.GetPreusDeCost(exs, oSku)
        If item IsNot Nothing Then
            Dim DcCost As Decimal = item.Price * (100 - item.Parent.Discount_OnInvoice) * (100 - item.Parent.Discount_OffInvoice) / 10000
            retval = DTOAmt.Factory(DcCost)
        End If
        Return retval
    End Function

    Shared Function Url(oSku As DTOProductSku, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        Dim oDomain = DTOWebDomain.Factory(, AbsoluteUrl)
        Dim oCategory As DTOProductCategory = oSku.Category
        If oCategory Is Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "sku", oSku.Guid.ToString())
        Else
            Dim oBrand As DTOProductBrand = oCategory.Brand
            If oBrand IsNot Nothing Then
                retval = oSku.GetUrl(oDomain.DefaultLang, oTab, AbsoluteUrl)
            End If
        End If

        If retval = "" Then
            retval = UrlHelper.Factory(AbsoluteUrl, "product", oSku.Guid.ToString())
        End If

        If oTab <> DTOProduct.Tabs.general Then
            retval = retval & "/" & oTab.ToString
        End If

        Return retval
    End Function


    Shared Function UrlImageZoom(oSKU As DTOProductSku, Optional BlAbsoluteUrl As Boolean = False) As String
        Dim retval As String = Url(oSKU, DTOProduct.Tabs.general, BlAbsoluteUrl) & "/imagen"
        Return retval
    End Function

    Shared Async Function SeBuscaEmailsForMailing(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of List(Of DTOUser))
        Return Await Api.Fetch(Of List(Of DTOUser))(exs, "productSku/SeBuscaEmailsForMailing", oSku.Guid.ToString())
    End Function

    Shared Async Function EmailsFromPncs(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of List(Of DTOUser))
        Return Await Api.Fetch(Of List(Of DTOUser))(exs, "productSku/EmailsFromPncs", oSku.Guid.ToString())
    End Function

    Shared Async Function Price(exs As List(Of Exception), oSku As DTOProductSku, oCustomer As DTOCustomer, Optional DtFch As Date = Nothing) As Task(Of DTOAmt)
        Return Await Customer.SkuPrice(exs, oCustomer, oSku, DtFch)
    End Function

    Shared Async Function LastCost(exs As List(Of Exception), oEmp As DTOEmp, oSku As DTOProductSku, Optional DtFch As Date = Nothing) As Task(Of DTOAmt)
        Return Await Api.Fetch(Of DTOAmt)(exs, "productSku/LastCost", oEmp.Id, oSku.Guid.ToString, FormatFch(DtFch))
    End Function

    Shared Async Function LastProductionAvailableUnits(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of Integer)
        Return Await Api.Fetch(Of Integer)(exs, "productSku/LastProductionAvailableUnits", oSku.Guid.ToString())
    End Function

    Shared Async Function IsAllowedOrderQty(exs As List(Of Exception), oSku As DTOProductSku, ByVal iQty As Integer, Optional oUser As DTOUser = Nothing) As Task(Of Boolean)
        Dim RetVal As Boolean = True
        Dim iMoq As Integer = DTOProductSku.Moq(oSku)
        If iQty Mod iMoq <> 0 Then
            If oUser Is Nothing Then
                RetVal = False
            Else
                RetVal = Await Product.AllowUserToFraccionarInnerPack(exs, oSku, oUser)
            End If
        End If
        Return RetVal
    End Function

    Shared Async Function Bundles(exs As List(Of Exception), oSku As DTOProductSku) As Task(Of List(Of DTOSkuBundle))
        Return Await Api.Fetch(Of List(Of DTOSkuBundle))(exs, "ProductSku/BundleSkus", oSku.Guid.ToString())
    End Function
End Class

Public Class ProductSkus
    Inherits _FeblBase

    Shared Async Function Minified(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOProductSku))
        Dim min = Await Api.Fetch(Of List(Of JObject))(exs, "ProductSkus/min", oEmp.Id)
        Dim retval = min.Select(Function(x) Models.Min.ProductSku.Expand(x)).ToList()
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductSkus/fromEmp", oEmp.Id)
    End Function

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer, Optional oMgz As DTOMgz = Nothing, Optional IncludeExcludedProducts As Boolean = False) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductSkus/fromCustomer", oCustomer.Guid.ToString, OpcionalGuid(oMgz), OpcionalBool(IncludeExcludedProducts))
    End Function


    Shared Async Function All(exs As List(Of Exception), oCategory As DTOProductCategory, oLang As DTOLang, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsolets As Boolean = False) As Task(Of List(Of DTOProductSku))
        Dim retval As New List(Of DTOProductSku)
        If oCategory.IsBundle Then
            retval = Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductSkus/bundles/fromCategory", oCategory.Guid.ToString, OpcionalGuid(oMgz), OpcionalBool(IncludeObsolets))
        Else
            retval = Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductSkus/fromCategory", oCategory.Guid.ToString, oLang.Tag, OpcionalGuid(oMgz), OpcionalBool(IncludeObsolets))
        End If
        If exs.Count = 0 Then
            For Each oSku In retval
                oSku.Category = oCategory
            Next

            'Dim tmp = retval.FirstOrDefault(Function(x) x.Id = 23328)
        End If
        Return retval
    End Function

    Shared Function AllSync(exs As List(Of Exception), oCategory As DTOProductCategory, oMgz As DTOMgz, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductSku)
        Dim retval = Api.FetchSync(Of List(Of DTOProductSku))(exs, "ProductSkus/fromCategory", oCategory.Guid.ToString, OpcionalGuid(oMgz), OpcionalBool(IncludeObsolets))
        For Each oSku In retval
            oSku.Category = oCategory
        Next
        Return retval
    End Function

    Shared Async Function Excerpts(exs As List(Of Exception), oLang As DTOLang) As Task(Of List(Of DTOLangText))
        Dim retval = Await Api.Fetch(Of List(Of DTOLangText))(exs, "productSkus/excerpts")
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oProveidor As DTOContact) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductSkus/fromProveidor", oProveidor.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oCnap As DTOCnap, Optional IncludeObsoletos As Boolean = False) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "ProductSkus/fromCnap", oCnap.Guid.ToString, OpcionalBool(IncludeObsoletos))
    End Function

    Shared Async Function GuidNoms(exs As List(Of Exception), oCategory As DTOProductCategory, oCustomer As DTOCustomer, oMgz As DTOMgz, stockOnly As Boolean, Optional includeHidden As Boolean = False) As Task(Of List(Of DTOGuidNom))
        Return Await Api.Fetch(Of List(Of DTOGuidNom))(exs, "ProductSkus/Guidnoms", oCategory.Guid.ToString, oCustomer.Guid.ToString, oMgz.Guid.ToString, OpcionalBool(stockOnly), OpcionalBool(includeHidden))
    End Function

    Shared Async Function CompactTree(exs As List(Of Exception), oEmp As DTOEmp, oLang As DTOLang, Optional IncludeObsolets As Boolean = False) As Task(Of List(Of DTOProductBrand))
        Dim oCatalog = Await Api.Fetch(Of DTOCatalog)(exs, "productskus/compactTree/fromEmp", oEmp.Id, oLang.Tag, OpcionalBool(IncludeObsolets))
        Dim retval = oCatalog.toProductBrands()
        RebuildCircularReferences(retval)
        Return retval
    End Function

    Shared Async Function CompactTree(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOProductBrand))
        Dim oCatalog = Await Api.Fetch(Of DTOCatalog)(exs, "productskus/compactTree/fromCustomer", oCustomer.Guid.ToString())
        Dim retval = oCatalog.toProductBrands()
        'RebuildCircularReferences(retval)
        Return retval
    End Function

    Shared Async Function CatalogBrands(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOProductBrand))
        Dim oCatalog = Await Api.Fetch(Of DTOCatalog)(exs, "productskus/CatalogBrands/fromCustomer", oCustomer.Guid.ToString())
        Dim retval = oCatalog.toProductBrands()
        'RebuildCircularReferences(retval)
        Return retval
    End Function

    Shared Sub RebuildCircularReferences(oBrands As List(Of DTOProductBrand))
        For Each oBrand In oBrands
            oBrand.IsNew = False
            For Each oCategory In oBrand.Categories
                oCategory.IsNew = False
                oCategory.Brand = oBrand
                For Each oSku In oCategory.Skus
                    oSku.IsNew = False
                    oSku.Category = oCategory
                Next
            Next
        Next
    End Sub

    Shared Async Function SimpleSearch(exs As List(Of Exception), searchKey As String, oEmp As DTOEmp) As Task(Of List(Of DTOProductSku))
        Dim retval = Await Api.Execute(Of String, List(Of DTOProductSku))(searchKey, exs, "ProductSkus/SimpleSearch", oEmp.Id, oEmp.Mgz.Guid.ToString())
        Return retval
    End Function

    Shared Async Function Search(exs As List(Of Exception), searchKey As String, oEmp As DTOEmp, Optional oMgz As DTOMgz = Nothing, Optional fch As Date = Nothing) As Task(Of List(Of DTOProductSku))
        Dim retval = Await Api.Execute(Of String, List(Of DTOProductSku))(searchKey, exs, "ProductSkus/Search", oEmp.Id, OpcionalGuid(oMgz), FormatFch(fch))
        Return retval
    End Function

    Shared Async Function Search2(exs As List(Of Exception), searchKey As String, oCustomer As DTOCustomer, oEmp As DTOEmp, oLang As DTOLang) As Task(Of List(Of DTOProductSku))
        Dim retval = Await Api.Execute(Of String, List(Of DTOProductSku))(searchKey, exs, "ProductSkus/Search2", oCustomer.Guid.ToString, oEmp.Id, oLang.Tag)
        'restore missing props from DTOProductSku.Compact to DTOProductSku
        If retval IsNot Nothing Then
            For Each oSku In retval
                oSku.IsNew = False
            Next
        End If
        Return retval
    End Function

    'In xl_PurchaseOrderItems to search serverless
    Shared Async Function Search3(exs As List(Of Exception), searchKey As String, oCustomer As DTOCustomer, oEmp As DTOEmp, oLang As DTOLang, oCache As Models.ClientCache) As Task(Of List(Of DTOProductSku))
        Dim retval = oCache.SkuSearch(searchKey)
        If retval.Count = 1 Then
            Dim oSku = Await ProductSku.LoadFromCustomer(exs, retval.First, oCustomer, oEmp.Mgz)
            retval = New List(Of DTOProductSku)
            retval.Add(oSku)
        End If
        Return retval
    End Function


    Shared Async Function FromGuids(exs As List(Of Exception), oGuids As List(Of Guid), oMgz As DTOMgz) As Task(Of List(Of DTOProductSku))
        Dim retval = Await Api.Execute(Of List(Of Guid), List(Of DTOProductSku))(oGuids, exs, "ProductSkus/FromGuids", oMgz.Guid.ToString)
        Return retval
    End Function

    Shared Async Function Search(exs As List(Of Exception), oEans As List(Of DTOEan), oCustomer As DTOBaseGuid, Optional oMgz As DTOMgz = Nothing) As Task(Of List(Of DTOProductSku))
        Dim retval = Await Api.Execute(Of List(Of DTOEan), List(Of DTOProductSku))(oEans, exs, "ProductSkus/Search/fromEans", oCustomer.Guid.ToString, OpcionalGuid(oMgz))
        Return retval
    End Function

    Shared Async Function SearchFromSkuIds(exs As List(Of Exception), oEmp As DTOEmp, skuIds As List(Of String)) As Task(Of List(Of DTOProductSku))
        Dim retval = Await Api.Execute(Of List(Of String), List(Of DTOProductSku))(skuIds, exs, "ProductSkus/Search/fromSkuIds", oEmp.Id)
        Return retval
    End Function

    Shared Function SearchSync(exs As List(Of Exception), searchKey As String, oEmp As DTOEmp, Optional oMgz As DTOMgz = Nothing, Optional fch As Date = Nothing) As List(Of DTOProductSku)
        Return Api.ExecuteSync(Of String, List(Of DTOProductSku))(searchKey, exs, "ProductSkus/Search", oEmp.Id, OpcionalGuid(oMgz), FormatFch(fch))
    End Function

    Shared Async Function Obsolets(exs As List(Of Exception), oUser As DTOUser, oLang As DTOLang, fchFrom As DateTime) As Task(Of MatHelper.Excel.Sheet)
        Dim retval = Await Api.Execute(Of String, MatHelper.Excel.Sheet)(fchFrom.ToString("o"), exs, "ProductSkus/Obsolets", oUser.Guid.ToString, oLang.Tag)
        Return retval
    End Function

    Shared Async Function Descatalogats(exs As List(Of Exception), oUser As DTOUser, ExcludeConfirmed As Boolean) As Task(Of List(Of DTODescatalogat))
        Dim retval = Await Api.Fetch(Of List(Of DTODescatalogat))(exs, "ProductSkus/Descatalogats", oUser.Guid.ToString, OpcionalBool(ExcludeConfirmed))
        Return retval
    End Function

    Shared Async Function ConfirmDescatalogats(exs As List(Of Exception), oGuids As List(Of Guid)) As Task(Of Boolean)
        Dim retval = Await Api.Execute(Of List(Of Guid), Boolean)(oGuids, exs, "ProductSkus/Descatalogats/Confirm")
        Return retval
    End Function

    Shared Function SpriteSync(oSkus As IEnumerable(Of DTOBaseGuid), itemWidth As Integer, itemHeight As Integer) As Byte() 
        Dim exs As New List(Of Exception)
        Dim oGuids = oSkus.Select(Function(x) x.Guid).ToList
        Dim retval As Byte() = Api.ExecuteSync(Of List(Of Guid), Byte())(oGuids, exs, "ProductSkus/Sprite", itemWidth, itemHeight)
        Return retval
    End Function

    Shared Async Function Sprite(exs As List(Of Exception), oSkus As IEnumerable(Of DTOBaseGuid), itemWidth As Integer, itemHeight As Integer) As Task(Of Byte())
        Dim oGuids = oSkus.Select(Function(x) x.Guid).ToList
        Dim retval = Await Api.downloadImage(Of List(Of Guid))(oGuids, exs, "ProductSkus/Sprite", itemWidth, itemHeight)
        Return retval
    End Function

    'Shared Async Function Sprite(exs As List(Of Exception), oCategory As DTOProductCategory, itemWidth As Integer) As Task(Of Byte())
    'Dim retval = Await Api.FetchImage(exs, "ProductSkus/Sprite", oCategory.Guid.ToString, itemWidth)
    'Return retval
    'End Function

End Class
