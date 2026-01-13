Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductSkuController
    Inherits _BaseController


    <HttpGet>
    <Route("api/productSku/{sku}")>
    Public Function Find(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ProductSku.Find(sku)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/{sku}/{mgz}")>
    Public Function Find(sku As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim value = BEBL.ProductSku.Find(sku, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productsku/fromId/{skuId}/{customer}/{mgz}")>
    Public Function SearchById(skuId As Integer, customer As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCustomer As New DTOCustomer(customer)
            Dim oMgz = DTOBaseGuid.opcional(Of DTOMgz)(mgz)
            Dim value = BEBL.ProductSku.SearchById(oCustomer, skuId, oMgz)
            retval = Request.CreateResponse(Of DTOProductSku)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el producte")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productsku/{sku}/{customer}/{mgz}")>
    Public Function Search(sku As Guid, customer As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCustomer As New DTOCustomer(customer)
            Dim oMgz = DTOBaseGuid.opcional(Of DTOMgz)(mgz)
            Dim value = BEBL.ProductSku.LoadFromCustomer(sku, oCustomer, oMgz)
            retval = Request.CreateResponse(Of DTOProductSku)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el producte")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/productSku/fromNom/{category}")>
    Public Function FromNom(category As Guid, <FromBody> nom As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCategory As New DTOProductCategory(category)
            Dim value = BEBL.ProductSku.FromNom(oCategory, nom)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/fromId/{emp}/{id}")>
    Public Function FromId(emp As Integer, id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.ProductSku.FromId(oEmp, id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/fromEan/{ean}")>
    Public Function fromEan(ean As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEan = DTOEan.Factory(ean)
            Dim value = BEBL.ProductSku.FromEan(oEan)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ProductSku/image/{guid}")>
    Public Function GetImage(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.art)
            If oImageMime Is Nothing Then
                oImageMime = BEBL.ProductSku.ImageMime(guid)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.art, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del ProductSku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductSku/thumbnail/{guid}")>
    Public Function Thumbnail(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.art150)
            If oImageMime Is Nothing Then
                oImageMime = BEBL.ProductSku.ThumbnailMime(guid)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.art150, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la thumbnail del ProductSku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductSku/thumbnail/{guid}/{width}/{height?}")> 'TO DEPRECATE (encara deu sortir al IOS
    Public Function ThumbnailWithSizeToDeprecate(guid As Guid, width As Integer, Optional height As Integer = 0) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            If (width = 0 Or width = DTOProductSku.THUMBNAILWIDTH) Then
                'if requested size matches thumbnail then fetch thumbnail from cache or from database
                Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.art150)
                If oImageMime Is Nothing Then
                    oImageMime = BEBL.ProductSku.ThumbnailMime(guid)
                    GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.art150, oImageMime)
                End If
                retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
            Else
                'fetch full image from cache or from database and size it accordingly
                Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.art)
                If oImageMime Is Nothing Then
                    oImageMime = BEBL.ProductSku.ImageMime(guid)
                    GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.art, oImageMime)
                End If
                If height = 0 Then height = width * DTOProductSku.IMAGEHEIGHT / DTOProductSku.IMAGEWIDTH
                Dim oImage = LegacyHelper.ImageHelper.FromBytes(oImageMime.ByteArray)

                Dim value = ImageMime.Factory(LegacyHelper.ImageHelper.GetThumbnailToFill(oImage, width, height).Bytes)
                retval = MyBase.HttpImageMimeResponseMessage(value)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la image del ProductSku")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ProductSku")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOProductSku)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la ProductSku")
            Else
                value.Image = oHelper.GetImage("image")

                If BEBL.ProductSku.Update(value, exs) Then
                    GlobalVariables.CachedImages.Reset(value.Guid)
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.ProductSkuLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.ProductSkuLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/ProductSku/delete")>
    Public Function Delete(<FromBody> value As DTOProductSku) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductSku.Delete(value, exs) Then
                GlobalVariables.CachedImages.Reset(value.Guid)
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ProductSku")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ProductSku")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/productSku/fromProveidor/{proveidor}")>
    Public Function fromProveidor(proveidor As Guid, <FromBody> ref As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim value = BEBL.ProductSku.FromProveidor(oProveidor, ref)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/SeBuscaEmailsForMailing/{sku}")>
    Public Function SeBuscaEmailsForMailing(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim values = BEBL.ProductSku.SeBuscaEmailsForMailing(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/emailsFromPncs/{sku}")>
    Public Function emailsFromPncs(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim values = BEBL.ProductSku.EmailsFromPncs(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/price/{sku}/{customer}/{fch}")>
    Public Function price(sku As Guid, customer As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.ProductSku.Price(oSku, oCustomer, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sku")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/productSku/lastCost/{emp}/{sku}/{fch}")>
    Public Function LastCost(emp As DTOEmp.Ids, sku As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oSku As New DTOProductSku(sku)
            Dim value = BEBL.ProductSku.LastCost(oEmp, oSku, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el darrer cost de la Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/LastProductionAvailableUnits/{sku}")>
    Public Function LastProductionAvailableUnits(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim value = BEBL.ProductSku.LastProductionAvailableUnits(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les ultimes unitats disponibles")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/productSku/AllowUserToFraccionarInnerPack/{product}/{user}")>
    Public Function AllowUserToFraccionarInnerPack(product As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim oUser As New DTOUser(user)
            Dim value = BEBL.Product.AllowUserToFraccionarInnerPack(oProduct, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al fraccionar les unitats demanables")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSku/BundleSkus/{sku}")>
    Public Function BundleSkus(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim values = BEBL.ProductSku.BundleSkus(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els components del bundle")
        End Try
        Return retval
    End Function

End Class

Public Class ProductSkusController
    Inherits _BaseController

    <HttpGet>
    <Route("api/productSkus/fromEmp/{emp}")>
    Public Function FromEmp(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.ProductSkus.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els productes del cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSkus/fromCustomer/{customer}/{mgz}/{includeExcludedProducts}")>
    Public Function fromCustomer(customer As Guid, mgz As Guid, includeExcludedProducts As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.ProductSkus.All(oCustomer, oMgz, (includeExcludedProducts = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSkus/fromCategory/{category}/{lang}/{mgz}/{includeObsolets}")>
    Public Function fromCategory(category As Guid, lang As String, mgz As Guid, includeObsolets As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCategory As New DTOProductCategory(category)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.ProductSkus.All(oCategory, oLang, oMgz, (includeObsolets = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSkus/bundles/fromCategory/{category}/{mgz}/{includeObsolets}")>
    Public Function bundlesFromCategory(category As Guid, mgz As Guid, includeObsolets As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCategory As New DTOProductCategory(category)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.ProductSkus.Bundles(oCategory, oMgz, (includeObsolets = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els Bundle")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSkus/fromProveidor/{proveidor}")>
    Public Function fromProveidor(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.ProductSkus.All(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSkus/excerpts")>
    Public Function excerpts() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ProductSkus.Excerpts()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productSkus/fromCnap/{cnap}/{includeObsolets}")>
    Public Function fromCnap(cnap As Guid, includeObsolets As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCnap As New DTOCnap(cnap)
            Dim values = BEBL.ProductSkus.All(oCnap, (includeObsolets = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les Sku")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/GuidNoms/{category}/{customer}/{mgz}/{stockOnly}/{includeHidden}")>
    Public Function GuidNoms(category As Guid, customer As Guid, mgz As Guid, stockOnly As Integer, includeHidden As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCategory As New DTOProductCategory(category)
            Dim oCcx As New DTOCustomer(customer)
            Dim oMgz As New DTOMgz(mgz)
            Dim values = BEBL.ProductSkus.GuidNoms(oCategory, oCcx, oMgz, (stockOnly = 1), (includeHidden = 1))
            retval = Request.CreateResponse(Of List(Of DTOGuidNom))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar el producte")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/compactTree/FromEmp/{emp}/{lang}/{includeObsoletos}")>
    Public Function compactTreeFromEmp(emp As DTOEmp.Ids, lang As String, includeObsoletos As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.ProductSkus.CompactTree(oEmp, oLang, (includeObsoletos = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/CatalogBrands/FromCustomer/{customer}")>
    Public Function CatalogBrandsFromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.ProductSkus.CatalogBrands(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/compactTree/FromCustomer/{customer}")>
    Public Function compactTreeFromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = BEBL.Customer.Find(customer)
            oCustomer.emp = MyBase.GetEmp(oCustomer.emp.Id)  'recupera mgz
            Dim values = BEBL.ProductSkus.CompactTree(oCustomer, oCustomer.lang, oCustomer.emp.Mgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/compactTree/FromUser/{user}")> 'per iMat(versio 2019)
    Public Function compactTreeFromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.ProductSkus.CompactTree(oUser, includeObsolets:=False)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/compactTree/WithObsolets/FromUser/{user}")> 'per iMat(versio 2019)
    Public Function compactTreeWithObsoletsFromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.ProductSkus.CompactTree(oUser, includeObsolets:=True)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productskus/search/{emp}/{mgz}/{fch}")>
    Public Function Search(emp As DTOEmp.Ids, mgz As Guid, fch As Date, <FromBody> searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oMgz = DTOBaseGuid.opcional(Of DTOMgz)(mgz)
            'If oMgz Is Nothing Then oMgz = oEmp.Mgz
            Dim values = BEBL.ProductSkus.Search(oEmp, searchkey, oMgz, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar el producte")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productskus/SimpleSearch/{emp}/{mgz}")>
    Public Function Search(emp As DTOEmp.Ids, mgz As Guid, <FromBody> searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = New DTOEmp(emp)
            Dim oMgz = New DTOMgz(mgz)
            Dim values = BEBL.ProductSkus.SimpleSearch(oEmp, oMgz, searchkey)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar el producte")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/productskus/search/fromSkuIds/{emp}")>
    Public Function Search(emp As DTOEmp.Ids, <FromBody> skuIds As List(Of Integer)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = New DTOEmp(emp)
            Dim values = BEBL.ProductSkus.Search(oEmp, skuIds)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar els productes")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productskus/search2/{customer}/{emp}/{lang}")>
    Public Function Search2(customer As Guid, emp As Integer, lang As String, <FromBody> searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = GetEmp(emp)
            Dim oLang = DTOLang.Factory(lang)
            'Dim oSkuList = BEBL.ProductSkus.SearchMultiple(oEmp, oLang, searchkey)
            Dim oSkuList = BEBL.ProductSkus.Search(oEmp, searchkey)
            If oSkuList.Count = 1 Then
                Dim oCustomer As New DTOCustomer(customer)
                Dim oSku = BEBL.ProductSku.LoadFromCustomer(oSkuList.First.Guid, oCustomer, oEmp.Mgz)
                retval = Request.CreateResponse(HttpStatusCode.OK, oSku)
            Else
                retval = Request.CreateResponse(HttpStatusCode.OK, oSkuList)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar el producte")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/productskus/fromGuids/{mgz}")>
    Public Function FromGuids(mgz As Guid, <FromBody> oGuids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.ProductSkus.FromGuids(oGuids, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar els productes per Ean")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productskus/search/fromEans/{customer}/{mgz}")>
    Public Function FromEans(customer As Guid, mgz As Guid, <FromBody> oEans As List(Of DTOEan)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCustomer As New DTOCustomer(customer)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.ProductSkus.Search(oCustomer, oEans, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar els productes per Ean")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/productskus/sprite/{itemWidth}/{itemHeight}")>
    Public Function Sprite(itemWidth As Integer, itemHeight As Integer, <FromBody> guids As Guid()) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSprite = BEBL.ProductSkus.Sprite(guids.ToList, itemWidth, itemHeight)
            retval = MyBase.HttpImageResponseMessage(oSprite)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar el sprite")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/sprite/{category}/{itemWidth}")>
    Public Function Sprite(category As Guid, itemWidth As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCategory As New DTOProductCategory(category)
            Dim itemHeight = itemWidth * DTOProductSku.IMAGEHEIGHT / DTOProductSku.IMAGEWIDTH
            Dim oSprite As Byte() = BEBL.ProductSkus.Sprite(oCategory, itemWidth, itemHeight)
            Dim value = ImageMime.Factory(oSprite)
            retval = MyBase.HttpImageMimeResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar el sprite")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productskus/descatalogats/{user}/{ExcludeConfirmed}")>
    Public Function Descatalogats(user As Guid, ExcludeConfirmed As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim value = BEBL.ProductSkus.Descatalogats(oUser, ExcludeConfirmed = 1)
            retval = Request.CreateResponse(Of List(Of DTODescatalogat))(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar els descatalogats")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/productskus/descatalogats/confirm")>
    Public Function ConfirmDescatalogats(<FromBody> oGuids As List(Of Guid)) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            If BEBL.ProductSkus.ConfirmDescatalogats(exs, oGuids) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al reconfirmar els descatalogats")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al reconfirmar els descatalogats")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productskus/obsolets/{user}/{lang}")>
    Public Function Obsolets(user As Guid, lang As String, <FromBody> fromFch As DateTime) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.ProductSkus.Obsolets(oUser, oLang, fromFch)
            retval = Request.CreateResponse(Of MatHelper.Excel.Sheet)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar els obsolets")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/productskus/obsoletsCount/{emp}")>
    Public Function ObsoletsCount(emp As DTOEmp.Ids, <FromBody> fromFch As DateTime) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim value = BEBL.ProductSkus.ObsoletsCount(oEmp, fromFch)
            retval = Request.CreateResponse(Of Integer)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al contar els obsolets")
        End Try
        Return retval
    End Function


End Class
