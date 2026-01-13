Imports Newtonsoft.Json.Linq

Public Class ProductSku

#Region "Crud"

    Shared Function Find(oGuid As Guid, Optional oMgz As DTOMgz = Nothing) As DTOProductSku
        Dim retval As DTOProductSku = ProductSkuLoader.Find(oGuid, oMgz)
        Return retval
    End Function

    Shared Function FromNom(oCategory As DTOProductCategory, sNom As String) As DTOProductSku
        Return ProductSkuLoader.FromNom(oCategory, sNom)
    End Function

    Shared Function FromEan(oEan As DTOEan) As DTOProductSku
        Dim retval As DTOProductSku = ProductSkuLoader.FromEan(oEan)
        Return retval
    End Function

    Shared Function Load(ByRef oSku As DTOProductSku, Optional oMgz As DTOMgz = Nothing) As Boolean
        Dim retval As Boolean = ProductSkuLoader.Load(oSku, oMgz)
        Return retval
    End Function

    Shared Function LoadFromCustomer(oGuid As Guid, oCustomer As DTOCustomer, Optional oMgz As DTOMgz = Nothing) As DTOProductSku
        Dim retval As DTOProductSku = ProductSkusLoader.LoadFromCustomer(oGuid, oCustomer, oMgz)
        Return retval
    End Function

    Shared Function FromId(oEmp As DTOEmp, Id As Integer) As DTOProductSku
        Dim retval As DTOProductSku = ProductSkuLoader.FromId(oEmp, Id)
        Return retval
    End Function

    Shared Function FromProveidor(oProveidor As DTOContact, sRef As String) As DTOProductSku
        Dim retval As DTOProductSku = ProductSkuLoader.FromProveidor(oProveidor, sRef)
        Return retval
    End Function

    Shared Function ImageMime(oGuid As Guid) As ImageMime
        Return ProductSkuLoader.ImageMime(oGuid)
    End Function

    Shared Function ThumbnailMime(oGuid As Guid) As ImageMime
        Return ProductSkuLoader.ThumbnailMime(oGuid)
    End Function

    Shared Function Update(oProductSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductSkuLoader.Update(oProductSku, exs)
        BEBL.ServerCache.ResetImage(Models.ImageCache.Cods.CategorySkuColorsSprite, oProductSku.Category.Guid)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Skus)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.SkuBundles)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductUrls)
        Return retval
    End Function

    Shared Function Delete(oProductSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ProductSkuLoader.Delete(oProductSku, exs)
        BEBL.ServerCache.ResetImage(Models.ImageCache.Cods.CategorySkuColorsSprite, oProductSku.Category.Guid)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.Skus)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.SkuBundles)
        BEBL.ServerCache.NotifyUpdate(Models.ServerCache.Tables.ProductUrls)
        Return retval
    End Function

    Shared Function SeBuscaEmailsForMailing(oSku As DTOProductSku) As List(Of DTOUser)
        Dim retval As List(Of DTOUser) = ProductSkuLoader.SeBuscaEmailsForMailing(oSku)
        Return retval
    End Function

    Shared Function EmailsFromPncs(oSku As DTOProductSku) As List(Of DTOUser)
        Dim retval As List(Of DTOUser) = ProductSkuLoader.EmailsFromPncs(oSku)
        Return retval
    End Function

    Shared Function BundleSkus(oBundle As DTOProductSku) As List(Of DTOSkuBundle)
        Return ProductSkuLoader.BundleSkus(oBundle)
    End Function



#End Region

    Shared Function SearchById(oCustomer As DTOCustomer, skuId As Integer, oMgz As DTOMgz) As DTOProductSku
        Dim retval As DTOProductSku = ProductSkuLoader.SearchById(oCustomer, skuId, oMgz)
        Dim oTarifaDtos As List(Of DTOCustomerTarifaDto) = BEBL.CustomerTarifaDtos.Active(oCustomer)
        retval.Price = BEBL.PriceListItemCustomer.GetCustomerCost(retval, oTarifaDtos)
        Return retval
    End Function

    Shared Function Price(oSku As DTOProductSku, oCustomer As DTOCustomer, Optional DtFch As Date = Nothing) As DTOAmt
        Dim retval As DTOAmt = BEBL.Customer.SkuPrice(oCustomer, oSku, DtFch)
        Return retval
    End Function

    Shared Function LastCost(oEmp As DTOEmp, oSku As DTOProductSku, Optional DtFch As Date = Nothing) As DTOAmt
        Dim retval As DTOAmt = ProductSkuLoader.LastCost(oSku, oEmp.Mgz, DtFch)
        Return retval
    End Function

    Shared Function LastProductionAvailableUnits(oSku As DTOProductSku) As Integer
        Dim retVal As Integer = ProductSkuLoader.LastProductionAvailableUnits(oSku)
        Return retVal
    End Function



End Class

Public Class ProductSkus

    Shared Function All(oEmp As DTOEmp) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.All(oEmp)
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer, Optional oMgz As DTOMgz = Nothing, Optional IncludeExcludedProducts As Boolean = False) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.All(oCustomer, oMgz, IncludeExcludedProducts)
        Return retval
    End Function

    Shared Function All(oCategory As DTOProductCategory, oLang As DTOLang, oMgz As DTOMgz, Optional IncludeObsolets As Boolean = False) As JArray
        Dim retval As JArray = ProductSkusLoader.All(oCategory, oLang, oMgz, IncludeObsolets)
        Return retval
    End Function

    Shared Function All_Deprecated(oCategory As DTOProductCategory, oMgz As DTOMgz, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.All_Deprecated(oCategory, oMgz, IncludeObsolets)
        Return retval
    End Function

    Shared Function Excerpts() As List(Of DTOLangText)
        Return ProductSkusLoader.Excerpts()
    End Function

    Shared Function Bundles(oCategory As DTOProductCategory, oMgz As DTOMgz, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.Bundles(oCategory, oMgz, IncludeObsolets)
        Return retval
    End Function

    Shared Function All(Proveidor As DTOContact) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.All(Proveidor)
        Return retval
    End Function

    Shared Function All(oCnap As DTOCnap, Optional IncludeObsoletos As Boolean = False) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.All(oCnap, IncludeObsoletos)
        Return retval
    End Function

    Shared Function AllWithEan() As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.AllWithEan()
        Return retval
    End Function

    Shared Function FromEanValues(eanValues As List(Of String), Optional oMgz As DTOMgz = Nothing) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        If eanValues.Count > 0 Then
            retval = ProductSkusLoader.FromEanValues(eanValues.ToHashSet(), oMgz)
        End If
        Return retval
    End Function

    Shared Function CompactTree(oEmp As DTOEmp, oLang As DTOLang, Optional IncludeObsoletos As Boolean = False) As DTOCatalog
        Return ProductSkusLoader.CompactTree(oEmp, oLang, IncludeObsoletos)
    End Function

    Shared Function CatalogBrands(oCustomer As DTOCustomer) As DTOCatalog
        Return ProductSkusLoader.CatalogBrands(oCustomer)
    End Function

    Shared Function CompactTree(oCustomer As DTOCustomer, oLang As DTOLang, oMgz As DTOMgz) As DTOCatalog
        Return ProductSkusLoader.CompactTree(oCustomer, oLang, oMgz)
    End Function

    Shared Function CompactTree(oUser As DTOUser, Optional includeObsolets As Boolean = False) As DTOCatalog
        Return ProductSkusLoader.CompactTree(oUser, includeObsolets)
    End Function

    Shared Function Relateds(oProduct As DTOProduct, oCod As DTOProduct.Relateds, oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.Relateds(oProduct, oMgz, oCod)
        Return retval
    End Function


    Shared Function Search(oEmp As DTOEmp, sSearchKey As String, Optional oMgz As DTOMgz = Nothing, Optional DtFch As Date = Nothing) As List(Of DTOProductSku)
        Dim retval As DTOProductSku.Collection = ProductSkusLoader.Search(oEmp, sSearchKey, oMgz, DtFch)
        Return retval
    End Function

    Shared Function SimpleSearch(oEmp As DTOEmp, oMgz As DTOMgz, searchkey As String) As DTOProductSku.Collection
        Return ProductSkusLoader.SimpleSearch(oEmp, oMgz, searchkey)
    End Function

    Shared Function Search(oEmp As DTOEmp, skuIds As List(Of Integer)) As DTOProductSku.Collection
        Dim retval As DTOProductSku.Collection = ProductSkusLoader.Search(oEmp, skuIds)
        Return retval
    End Function

    Shared Function SearchMultiple(oEmp As DTOEmp, oLang As DTOLang, sSearchKey As String) As List(Of DTOProductSku.Compact)
        Return ProductSkusLoader.Search(oEmp, oLang, sSearchKey)
    End Function


    Shared Function FromGuids(oGuids As List(Of Guid), Optional oMgz As DTOMgz = Nothing) As DTOProductSku.Collection
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.FromGuids(oGuids, oMgz)
        Return retval
    End Function

    Shared Function Search(oCustomer As DTOCustomer, oEans As List(Of DTOEan), Optional oMgz As DTOMgz = Nothing) As DTOProductSku.Collection
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.Search(oCustomer, oEans, oMgz)
        If Not oCustomer.IsConsumer Then retval = GetCustomerCost(oCustomer, retval)
        Return retval
    End Function

    Private Shared Function GetCustomerCost(oCustomer As DTOCustomer, oSkus As DTOProductSku.Collection) As DTOProductSku.Collection
        Dim retval As New DTOProductSku.Collection
        Dim oTarifaDtos As List(Of DTOCustomerTarifaDto) = BEBL.CustomerTarifaDtos.Active(oCustomer)
        For Each oSku In oSkus
            oSku.Price = BEBL.PriceListItemCustomer.GetCustomerCost(oSku, oTarifaDtos)
            retval.Add(oSku)
        Next
        Return retval
    End Function





    Shared Function GuidNoms(oCategory As DTOProductCategory, oCustomer As DTOCustomer, oMgz As DTOMgz, stockOnly As Boolean, Optional includeHidden As Boolean = False) As List(Of DTOGuidNom)
        Dim oSkus As List(Of DTOProductSku) = ProductSkusLoader.All(oCategory, oCustomer, oMgz, IncludeObsolets:=True, stockOnly:=stockOnly)
        If Not includeHidden Then
            oSkus = oSkus.Where(Function(x) DTOProductSku.isHidden(x) = False).ToList
        End If
        Dim retval As New List(Of DTOGuidNom)
        For Each item In oSkus
            Dim append As Boolean = Not item.NoPro '= item.Stock > item.Clients
            'If Not stockOnly Then append = True
            If item.obsoleto And item.Stock <= 0 Then append = False
            If item.NoPro Then append = False
            If append Then
                retval.Add(New DTOGuidNom(item.Guid, item.Nom.Esp))
            End If
        Next
        Return retval
    End Function

    Shared Function FromCustomerOrders(oContact As DTOContact, oCategory As DTOProductCategory) As List(Of DTOProductSku)
        Dim retval As List(Of DTOProductSku) = ProductSkusLoader.FromCustomerOrders(oContact, oCategory)
        Return retval
    End Function

    Shared Function ObsoletsCount(oEmp As DTOEmp, FromFch As Date) As Integer
        Return ProductSkusLoader.ObsoletsCount(oEmp, FromFch)
    End Function

    Shared Function Obsolets(oUser As DTOUser, oLang As DTOLang, FromFch As Date) As MatHelper.Excel.Sheet
        Dim retval As MatHelper.Excel.Sheet = ProductSkusLoader.Obsolets(oUser, oLang, FromFch)
        Return retval
    End Function

    Shared Function Descatalogats(oUser As DTOUser, ExcludeConfirmed As Boolean) As List(Of DTODescatalogat)
        Return ProductSkusLoader.Descatalogats(oUser, ExcludeConfirmed)
    End Function
    Shared Function ConfirmDescatalogats(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Return ProductSkusLoader.ConfirmDescatalogats(exs, oGuids)
    End Function


    Shared Function LastImageFch(oSkus As List(Of DTOProductSku)) As DateTime
        Dim retval As DateTime = ProductSkusLoader.LastImageFch(oSkus)
        Return retval
    End Function


    Shared Function Sprite(oGuids As List(Of Guid), itemWidth As Integer, itemHeight As Integer) As Byte()
        Dim oImages = ProductSkusLoader.Sprite(oGuids, itemWidth, itemHeight)
        Dim retval = LegacyHelper.SpriteHelper.Factory(oImages, itemWidth, itemHeight)
        Return retval
    End Function

    Shared Function Sprite(oCategory As DTOProductCategory, itemWidth As Integer, itemHeight As Integer) As Byte()
        Dim oImages = ProductSkusLoader.Sprite(oCategory)
        Dim retval = LegacyHelper.SpriteHelper.Factory(oImages, itemWidth, itemHeight)
        Return retval
    End Function


End Class
