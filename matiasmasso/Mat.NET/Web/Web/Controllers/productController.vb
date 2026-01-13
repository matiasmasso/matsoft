Imports SixLabors.ImageSharp

Public Class ProductController
    Inherits _MatController

    <ValidateInput(False)> 'per permetre ampersand a la url dins el nom de la categoria o el producte
    Async Function Index(Brand As String, Catchall As String, Optional filters As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oLang = ContextHelper.Lang()
        ContextHelper.NavViewModel.ResetCustomMenu()

        Dim oDomain As DTOWebDomain = ContextHelper.Domain
        Dim oMainSegment = New DTOLangText("")
        Dim sFriendlySegment As String = ""
        If MyBase.UrlSplit(HttpContext.Request.Url, oMainSegment, ".html", oDomain, oLang, sFriendlySegment) Then
            ViewBag.Lang = oLang
            Dim oProductAndTab = Await FEB2.ProductUrl.Search(exs, GlobalVariables.Emp, sFriendlySegment)
            If exs.Count = 0 Then
                If oProductAndTab Is Nothing OrElse oProductAndTab.Product Is Nothing Then
                    retval = Await ErrorNotFoundResult()
                Else
                    retval = Await Product(oProductAndTab.Product.ToDerivedClass(), oProductAndTab.Tab, oLang, filters)
                End If
            Else
                retval = Await ErrorResult(exs)
            End If
        Else
            Return Await ErrorNotFoundResult()
        End If
        Return retval
    End Function

    Public Async Function ProductGuid(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oProduct As DTOProduct = Await FEB2.Product.Find(exs, guid)
        If exs.Count = 0 Then
            If oProduct Is Nothing Then
                retval = Await ErrorNotFoundResult()
            Else
                retval = Await Product(oProduct, DTOProduct.Tabs.general, ContextHelper.Lang)
            End If
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Private Async Function Product(oProduct As DTOProduct, oTab As DTOProduct.Tabs, oLang As DTOLang, Optional filters As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oModel As New ProductModel
        Select Case oProduct.sourceCod
            Case DTOProduct.SourceCods.Brand
                retval = Await BrandPage(oProduct.ToDerivedClass(), oTab, oLang)
            Case DTOProduct.SourceCods.Dept
                retval = Await DeptPage(oProduct.ToDerivedClass(), oTab, oLang, filters)
            Case DTOProduct.SourceCods.Category
                retval = CategoryPage(oProduct.ToDerivedClass(), oTab, oLang)
            Case DTOProduct.SourceCods.Sku
                Dim oSku As DTOProductSku = oProduct.ToDerivedClass()
                Select Case oTab
                    Case DTOProduct.Tabs.imagen
                        Dim oImg = Await FEB2.ProductSku.Image(exs, oSku)
                        Dim oMimeCod As MimeCods = LegacyHelper.ImageHelper.GuessMime(oImg)
                        Dim oImageFormat As System.Drawing.Imaging.ImageFormat = LegacyHelper.ImageHelper.GetImageFormat(oMimeCod)
                        Dim sContentType As String = MediaHelper.ContentType(oMimeCod)
                        MyBase.HttpContext.Response.Cache.SetMaxAge(New TimeSpan(24 * 360, 0, 0))

                        Dim oStream As New System.IO.MemoryStream
                        oImg.SaveAsJpeg(oStream)
                        oStream.Position = 0
                        retval = New FileStreamResult(oStream, sContentType) ' "image/jpeg")
                    Case Else
                        retval = SkuPage(oProduct.ToDerivedClass(), oTab, oLang)
                End Select
        End Select
        Return retval
    End Function

    Private Async Function BrandPage(oBrand As DTOProductBrand, oTab As DTOProduct.Tabs, oLang As DTOLang) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oModel As New ProductModel

        FEB2.ProductBrand.Load(oBrand, exs)

        oModel = ProductModel.Factory(oBrand, oLang, oTab)
        ContextHelper.NavViewModel.LoadCustomMenu(oLang, oBrand)
        Select Case oTab
            Case DTOProduct.Tabs.distribuidores
                ViewBag.Title = String.Format("{0} {1}", Mvc.ContextHelper.Tradueix("Distribuidores Oficiales", "Distribuidors Oficials", "Official Dealers"), oBrand.Nom.Tradueix(oLang))
                retval = Redirect("/StoreLocator/" & oBrand.Guid.ToString)
            Case DTOProduct.Tabs.galeria
                retval = View("Galeria", oBrand)
            Case DTOProduct.Tabs.descargas
                retval = View("Downloads", oBrand)
            Case DTOProduct.Tabs.videos
                retval = View("Videos", oBrand)
            Case DTOProduct.Tabs.bloggerposts
                retval = View("BloggerPosts", oBrand)
            Case Else
                ViewBag.Canonical = oBrand.UrlCanonicas()
                ViewBag.MetaDescription = oBrand.Excerpt.Tradueix(oLang)
                Dim oDepts = Await FEB2.Depts.All(exs, oBrand)
                If oDepts.Count = 0 Then
                    Dim oCategories = Await FEB2.ProductCategories.All(exs, oBrand)
                    If exs.Count = 0 Then
                        oModel.GalleryMode = ProductModel.GalleryModes.Categories
                        For Each oCategory In oCategories.Where(Function(x) x.Codi = DTOProductCategory.Codis.standard)
                            oModel.Items.Add(oCategory.ImageUrl, DTOProductCategory.IMAGEWIDTH, DTOProductCategory.IMAGEHEIGHT, oCategory.Nom.Tradueix(oLang), oCategory.UrlCanonicas.RelativeUrl(oLang), oCategory.Excerpt.Tradueix(oLang), oCategory.Guid.ToString)
                        Next
                        retval = View("ProductBrand", oModel)
                    Else
                        retval = Await MyBase.ErrorResult(exs)
                    End If
                Else
                    oModel.GalleryMode = ProductModel.GalleryModes.Depts
                    For Each oDept In oDepts
                        oModel.Items.Add(oDept.BannerUrl(), DTODept.IMAGEWIDTH, DTODept.IMAGEHEIGHT, oDept.Nom.Tradueix(oLang), oDept.UrlCanonicas.RelativeUrl(oLang), "", oDept.Guid.ToString)
                    Next
                    retval = View("ProductBrand", oModel)
                End If


                retval = View("ProductBrand", oModel)
        End Select

        ViewBag.Canonical = oBrand.UrlCanonicas
        If oTab <> DTOProduct.Tabs.general Then
            ViewBag.Canonical = oBrand.UrlCanonicas.WithSufix(oTab.ToString())
        End If

        Return retval
    End Function

    Private Async Function DeptPage(oDept As DTODept, oTab As DTOProduct.Tabs, oLang As DTOLang, Optional filters As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oModel As New ProductModel
        FEB2.Dept.Load(oDept, False, exs)
        Dim oCategories = oDept.Categories.Where(Function(x) x.Codi = DTOProductCategory.Codis.standard And x.EnabledxConsumer = True).ToList()
        'FEB2.ProductBrand.Load(oDept.Brand, exs)

        If exs.Count = 0 Then
            'Dim oDepts = Await FEB2.Depts.AllWithFilters(exs, oDept.Brand)
            'oDept = oDepts.FirstOrDefault(Function(x) x.Guid.Equals(oDept.Guid))
            oModel = ProductModel.Factory(oDept, oLang, oTab)

            Dim idx As Integer = 0
            For Each oCategory In oCategories
                idx += 1
                oModel.Items.Add(oCategory.ImageUrl(), DTODept.IMAGEWIDTH, DTODept.IMAGEHEIGHT, oCategory.Nom.Tradueix(oLang), oCategory.UrlCanonicas.RelativeUrl(oLang), oCategory.Excerpt.Tradueix(oLang), tag:=oCategory.Guid.ToString)
            Next

            If oDept.Brand.Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Romer)) Then

                If Not String.IsNullOrEmpty(filters) Then
                    Dim oCheckedFilterItems = DTOFilter.Item.Collection.Factory(filters)
                    oModel.CheckedFilterItems = oCheckedFilterItems
                End If

                Dim oFilters = Await FEB2.Filters.All(exs)
                Dim oProductFilterItems = DTOFilter.Item.Collection.Factory(oDept)
                oModel.Filters = oFilters.WithItems(oProductFilterItems)
                For Each oFilter In oModel.Filters
                    For Each item In oFilter.Items
                        Dim itemGuid As Guid = item.Guid
                        For Each oCategory In oDept.Categories
                            If oCategory.FilterItems.Any(Function(x) x.Guid.Equals(itemGuid)) Then
                                item.TargetGuids.Add(oCategory.Guid)
                            End If
                        Next
                    Next
                Next
            End If

            ContextHelper.NavViewModel.LoadCustomMenu(oDept, oModel.Filters, oModel.CheckedFilterItems)

            'ViewBag.Canonical = oDept.Urls()
            ViewBag.Title = oDept.FullNom(oLang)
            ViewBag.MetaDescription = oDept.Excerpt.Tradueix(oLang)
            retval = View("ProductDept", oModel)
        Else
            retval = Await MyBase.ErrorResult(exs)
        End If

        ViewBag.Canonical = oDept.UrlCanonicas()
        If oTab <> DTOProduct.Tabs.general Then
            ViewBag.Canonical = oDept.UrlCanonicas.WithSufix(oTab.ToString())
        End If

        Return retval
    End Function

    Private Function CategoryPage(oCategory As DTOProductCategory, oTab As DTOProduct.Tabs, oLang As DTOLang) As ActionResult
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oModel As New ProductModel

        oCategory.IsLoaded = False
        FEB2.ProductCategory.Load(oCategory, exs)
        oModel.Product = oCategory
        oModel.Excerpt = oCategory.Excerpt.Tradueix(oLang)
        oModel.Text = oCategory.Content.Html(oLang)
        oModel.Tag = oCategory.Guid.ToString
        oModel.Title = String.Format("{0} {1}", oCategory.Brand.Nom.Tradueix(oLang), oCategory.Nom.Tradueix(oLang))
        ContextHelper.NavViewModel.LoadCustomMenu(oCategory, oLang)
        ViewBag.Title = oModel.Title
        retval = TabResult(oCategory, oTab, oModel, oLang)
        If retval Is Nothing Then
            'ViewBag.Canonical = oCategory.Urls()
            ViewBag.Canonical = oCategory.UrlCanonicas
            ViewBag.MetaDescription = oCategory.Excerpt.Tradueix(oLang)
            retval = View("ProductCategory", oModel)
            retval = View("ProductCategory", oModel)
        End If

        'ViewBag.Canonical = oCategory.Urls()
        ViewBag.Canonical = oCategory.UrlCanonicas
        If oTab <> DTOProduct.Tabs.general Then
            ViewBag.Canonical = oCategory.UrlCanonicas.WithSufix(oTab.ToString())
        End If

        Return retval
    End Function

    Private Function SkuPage(oSku As DTOProductSku, oTab As DTOProduct.Tabs, oLang As DTOLang) As ActionResult
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oModel As New ProductModel

        oSku.IsLoaded = False
        FEB2.ProductSku.Load(oSku, exs)
        oModel.Product = oSku
        oModel.Title = String.Format("{0} {1} {2}", oSku.Category.Brand.Nom.Tradueix(oLang), oSku.Category.Nom.Tradueix(oLang), oSku.Nom.Tradueix(oLang))
        If oSku.Hereda Then
            FEB2.ProductCategory.Load(oSku.Category, exs)
            oModel.Text = oSku.Category.Content.Html(oLang)
        Else
            oModel.Text = oSku.Content.Html(oLang)
        End If
        oModel.ImageUrl = oSku.imageUrl()
        oModel.Retail = oSku.Rrpp
        oModel.Tag = oSku.Guid.ToString
        retval = TabResult(oSku, oTab, oModel, oLang)
        ContextHelper.NavViewModel.LoadCustomMenu(oSku, oLang)

        ViewBag.Canonical = oSku.UrlCanonicas
        If oTab <> DTOProduct.Tabs.general Then
            ViewBag.Canonical = oSku.UrlCanonicas.WithSufix(oTab.ToString())
        End If

        If retval Is Nothing Then
            If oSku.Hereda Then
                ViewBag.Canonical = oSku.Category.UrlCanonicas
                If oTab <> DTOProduct.Tabs.general Then
                    ViewBag.Canonical = oSku.Category.UrlCanonicas.WithSufix(oTab.ToString())
                End If
            End If
            retval = View("ProductSku", oModel)
        End If
        Return retval
    End Function


    Private Function TabResult(oProduct As DTOProduct, oTab As DTOProduct.Tabs, oModel As ProductModel, oLang As DTOLang) As ActionResult
        Dim retval As ActionResult = Nothing
        Select Case oTab
            Case DTOProduct.Tabs.coleccion
                ViewBag.Title = String.Format("{0} {1}", oLang.Tradueix("Colección", "Col·lecció", "Designs", "Coleçao"), oModel.Title)
                retval = View("ProductCollection", oProduct)
            Case DTOProduct.Tabs.distribuidores
                ViewBag.Title = String.Format("{0} {1}", Mvc.ContextHelper.Tradueix("Distribuidores Oficiales", "Distribuidors Oficials", "Official Dealers"), oModel.Title)
                retval = View("StoreLocator", oModel)
            Case DTOProduct.Tabs.galeria
                ViewBag.Title = String.Format("{0} {1}", Mvc.ContextHelper.Tradueix("Galería de imágenes", "Galeria d'imatges", "Image gallery", "Galeria de Imagens"), oModel.Title)
                retval = View("Galeria", oModel.Product)
            Case DTOProduct.Tabs.descargas
                ViewBag.Title = String.Format("{0} {1}", Mvc.ContextHelper.Tradueix("Descargas", "Descarregues", "Downloads", "Descargas"), oModel.Title)
                retval = View("Downloads", oModel.Product)
            Case DTOProduct.Tabs.accesorios
                ViewBag.Title = String.Format("{0} {1}", oLang.Tradueix("Accesorios", "Accessoris", "Accessories"), oModel.Title)
                retval = View("Accessories", oModel.Product)
            Case DTOProduct.Tabs.videos
                ViewBag.Title = String.Format("{0} {1}", Mvc.ContextHelper.Tradueix("Vídeos"), oModel.Title)
                retval = View("Videos", oModel.Product)
            Case DTOProduct.Tabs.bloggerposts
                ViewBag.Title = String.Format("{0} {1}", Mvc.ContextHelper.Tradueix("Publicaciones", "Publicacions", "Posts"), oModel.Title)
                retval = View("BloggerPosts", oModel.Product)
        End Select

        Return retval
    End Function

    Public Async Function Cnap(cnapKey As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oModel As List(Of DTOProduct) = Await FEB2.Products.FromCnap(exs, cnapKey)
        Return View(oModel)
    End Function

    Public Async Function Ref(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        If IsNumeric(id) Then
            Dim oSku = Await FEB2.ProductSku.FromId(exs, GlobalVariables.Emp, id)
            If oSku IsNot Nothing Then
                FEB2.Product.Load(oSku, exs)
                retval = New RedirectResult(oSku.GetUrl(Mvc.ContextHelper.Lang))
            End If
        End If
        Return retval
    End Function


    Async Function General(productGuid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        If GuidHelper.IsGuid(productGuid) Then
            'Dim oGuid As New Guid(productGuid)
            Dim oProduct = Await FEB2.Product.Find(exs, productGuid)
            Select Case oProduct.sourceCod
                Case DTOProduct.SourceCods.Brand
                    retval = PartialView("_Categories", oProduct)
                Case DTOProduct.SourceCods.Category
                    retval = PartialView("_Category", oProduct)
                Case DTOProduct.SourceCods.Sku
                    retval = PartialView("_SKU", oProduct)
            End Select
        End If
        Return retval
    End Function

    Async Function PartialCollection(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oCategory As New DTOProductCategory(guid)
        Dim oSkus = Await FEB2.ProductSkus.All(exs, oCategory, MyBase.Lang, Mvc.GlobalVariables.Emp.Mgz, False)
        Dim model = oSkus.FindAll(Function(x) x.NoWeb = False And DTOProductSku.isHidden(x) = False).ToList
        Return PartialView("_Collection", model)
    End Function

    Async Function PartialDownloads(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct As New DTOProduct(guid)
        Dim model = Await FEB2.Downloads.FromProductOrParent(exs, oProduct, False, True, ContextHelper.Lang())
        If exs.Count = 0 Then
            Return PartialView("_Downloads", model)
        Else
            Return PartialView("_Error", exs)
        End If
    End Function

    Async Function PartialVideos(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct As New DTOProduct(guid)
        Dim oUser = ContextHelper.GetUser
        Dim oLang = Mvc.ContextHelper.Lang
        Dim model = Await FEB2.YouTubeMovies.All(exs, oUser, oProduct, oLang)
        If exs.Count = 0 Then
            Return PartialView("_Videos", model)
        Else
            Return PartialView("_Error", exs)
        End If
    End Function

    Async Function PartialImgGallery(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct As New DTOProduct(guid)
        Dim items = Await FEB2.MediaResources.All(exs, oProduct)
        If exs.Count = 0 Then
            Dim filteredItems = items.Where(Function(x) x.Obsolet = False And (x.Lang Is Nothing OrElse x.Lang.Equals(Mvc.ContextHelper.Lang()))).ToList
            Dim model As New List(Of List(Of DTOMediaResource))
            Dim oSection As New List(Of DTOMediaResource)
            model.Add(oSection)
            For Each item In filteredItems
                If oSection.Count > 0 Then
                    If oSection.First.Cod <> item.Cod Then
                        oSection = New List(Of DTOMediaResource)
                        model.Add(oSection)
                    End If
                End If
                oSection.Add(item)
            Next
            Return PartialView("_Gallery", model)
        Else
            Return PartialView("_Error", exs)
        End If
    End Function

    Async Function PartialAccessories(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct As New DTOProduct(guid)
        Dim model = Await FEB2.Product.Relateds(exs, DTOProduct.Relateds.Accessories, oProduct)
        If exs.Count = 0 Then
            Return PartialView("_Accessories", model)
        Else
            Return PartialView("_Error", exs)
        End If
    End Function

    Async Function Accesorios(product As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, product)
        Return PartialView("_Accessories", oProduct)
    End Function

    Async Function PartialBloggerPosts(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct As New DTOProduct(guid)
        Dim model = Await FEB2.BloggerPosts.FromProductOrParent(oProduct, exs)
        If exs.Count = 0 Then
            Return PartialView("_BloggerPosts", model)
        Else
            Return PartialView("_Error", exs)
        End If
    End Function



    Async Function BloggerPosts(product As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, product)
        Return PartialView("_BloggerPosts", oProduct)
    End Function

    Async Function Description(product As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, product)
        FEB2.Product.Load(oProduct, exs)
        Return PartialView("_Description", oProduct)
    End Function





#Region "Videos"
    Async Function AllVideos() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim model As DTOYouTubeMovie.ProductModel = Await FEB2.YouTubeMovies.Model(exs, GlobalVariables.Emp, ContextHelper.Lang, ContextHelper.GetUser())
        If exs.Count = 0 Then
            ViewBag.Title = ContextHelper.Lang.Tradueix("Vídeos")
            ViewBag.Canonical = DTOUrl.Factory("Videos")
            ViewBag.MetaDescription = ContextHelper.Lang.Tradueix("Descubre todos los videos disponibles", "Descobreix tots els videos disponibles", "Check all available videos")
            retval = View("AllVideos", model)
        Else
            retval = Await ErrorResult(exs)
        End If

        Return retval
    End Function



#End Region

#Region "SalePoints"

    Function Distribuidores(product As Guid) As PartialViewResult
        Return PartialView("_SalePoints", product)
    End Function

    Async Function Paisos(productGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, productGuid)
        Dim oLang As DTOLang = ContextHelper.Lang()
        Dim oCountries = Await FEB2.StoreLocator.Countries(exs, oProduct, oLang)
        Dim retval As JsonResult = Json(oCountries, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function Zonas(productGuid As Guid, countryGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, productGuid)
        Dim oCountry As New DTOCountry(countryGuid)
        Dim oLang = ContextHelper.Lang()
        Dim oZonas = Await FEB2.StoreLocator.Zonas(exs, oProduct, oCountry, oLang)
        Dim retval As JsonResult = Json(oZonas, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function Locations(productGuid As Guid, zonaGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, productGuid)
        Dim oZona As New DTOArea(zonaGuid)
        Dim oLang = ContextHelper.Lang()
        Dim oLocations = Await FEB2.StoreLocator.Locations(exs, oProduct, oZona, oLang)
        Dim retval As JsonResult = Json(oLocations, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function StoreLocatorTest(product As Guid, location As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, product)
        If oProduct.sourceCod = DTOProduct.SourceCods.Dept Then
            'canvia a marca doncs no està preparat per filtrar per departament
            Dim oDept = Await FEB2.Dept.Find(oProduct.Guid, exs)
            oProduct = oDept.Brand
        End If
        Dim Model As New DTOProductPageQuery
        With Model
            .Product = oProduct
            If location <> Guid.Empty Then .Location = Await FEB2.Location.Find(location, exs)
            .Tab = DTOProduct.Tabs.distribuidores
        End With
        Return PartialView("_StoreLocatorTest")
    End Function


    Async Function StoreLocator(product As Guid, location As Guid) As Threading.Tasks.Task(Of PartialViewResult) 'JScript web mobile
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, product)
        If oProduct.sourceCod = DTOProduct.SourceCods.Dept Then
            'canvia a marca doncs no està preparat per filtrar per departament
            Dim oDept = Await FEB2.Dept.Find(oProduct.Guid, exs)
            oProduct = oDept.Brand
        End If
        Dim Model As New DTOProductPageQuery
        With Model
            .Product = oProduct
            .Location = Await FEB2.Location.Find(location, exs)
            .Tab = DTOProduct.Tabs.distribuidores
        End With

        Return PartialView("_StoreLocator", Model)
    End Function

    Async Function Distributors(productGuid As Guid, locationGuid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oLocation As New DTOLocation(locationGuid)
        Dim oProduct As New DTOProduct(productGuid)
        'Dim oLocation = Await FEB2.Location.Find(locationGuid, exs)
        'Dim oProduct = Await FEB2.Product.Find(exs, productGuid)
        Dim oDistributors = Await FEB2.StoreLocator.Distributors(exs, oProduct, oLocation, ContextHelper.Lang())
        oDistributors = DTOProductDistributor.PremiumOrSpareDistributors(oDistributors, oLocation)

        Return PartialView("_StoreLocatorOfflineList", oDistributors)
    End Function


    Async Function FromGeoLocation(product As Guid, latitud As String, longitud As String) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        Dim culture As IFormatProvider = Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim dcLatitud, dcLongitud As Decimal

        If Decimal.TryParse(latitud, Globalization.NumberStyles.Float, culture, dcLatitud) Then
            If Decimal.TryParse(longitud, Globalization.NumberStyles.Float, culture, dcLongitud) Then
                Dim oCoordenadas As New GeoHelper.Coordenadas(dcLatitud, dcLongitud)
                Dim oProduct = Await FEB2.Product.Find(exs, product)
                Dim Model As List(Of DTONeighbour) = Await FEB2.StoreLocator.NearestNeighbours(exs, oProduct, oCoordenadas, ContextHelper.GetLang)
                retval = PartialView("_StoreLocatorOfflineNearestNeighbours", Model)
            End If
        End If
        Return retval
    End Function

    Async Function FromLocation(product As Guid, location As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing

        Dim oProductArea As New DTOProductArea
        oProductArea.Product = Await FEB2.Product.Find(exs, product)
        oProductArea.Area = Await FEB2.Location.Find(location, exs)
        retval = PartialView("_StoreLocatorOffline", oProductArea)
        Return retval
    End Function

    Async Function BestZona(productGuid As Guid, countryGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)

        Dim oProduct = Await FEB2.Product.Find(exs, productGuid)
        Dim oCountry As New DTOCountry(countryGuid)
        Dim oDistributors = Await FEB2.StoreLocator.Distributors(exs, oProduct, oCountry, ContextHelper.Lang())
        Dim oZona = DTOProductDistributor.BestZona(oDistributors)
        Dim retval As JsonResult = Json(oZona, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function BestLocation(productGuid As Guid, zonaGuid As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, productGuid)
        Dim oZona = DTOArea.Factory(zonaGuid, DTOArea.Cods.Zona)
        Dim oDistributors = Await FEB2.StoreLocator.Distributors(exs, oProduct, oZona, ContextHelper.Lang())
        Dim oLocation = DTOProductDistributor.BestLocation(oDistributors)
        Dim retval As JsonResult = Json(oLocation, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

#End Region

    Function pageindexchanged(returnurl As String, guid As Guid, pageindex As Integer) As PartialViewResult
        '¿Ho crida algú?
        ViewBag.Guid = guid
        ViewBag.PageIndex = pageindex

        Dim retval As PartialViewResult = PartialView(returnurl)
        Return retval
    End Function

    Async Function Videos(product As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        ' ViewBag.Guid = New Guid(ProductGuid)
        Dim exs As New List(Of Exception)
        Dim oProduct = Await FEB2.Product.Find(exs, product)
        FEB2.Product.Load(oProduct, exs)
        Dim Model As DTOProduct = oProduct
        If TypeOf oProduct Is DTOProductSku AndAlso DirectCast(oProduct, DTOProductSku).Hereda Then
            Model = DirectCast(oProduct, DTOProductSku).Category
        End If
        Return PartialView("_Videos", Model)
        'Return PartialView("_Videos", New With {.productGuid = Model.Product.Guid.ToString, .pageIndex = 1, .pageItems = 4}))
    End Function

    Async Function videospageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim retval As PartialViewResult = Nothing
        Dim Model As New List(Of DTOYouTubeMovie)
        Dim exs As New List(Of Exception)
        Dim oMovies = Await FEB2.YouTubeMovies.FromProductGuid(guid, ContextHelper.Lang(), exs)
        If exs.Count = 0 Then
            Dim items = oMovies.Where(Function(x) x.Obsoleto = False).ToList

            Dim indexFrom As Integer = pageindex * pagesize
            For i As Integer = indexFrom To indexFrom + pagesize - 1
                If i >= items.Count Then Exit For
                Model.Add(items(i))
            Next
            retval = PartialView("_VideosPaginated", Model)
        Else
            If Debugger.IsAttached Then Stop
        End If
        Return retval
    End Function


End Class