Imports Newtonsoft.Json.Linq

Public Class Product
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOProduct)
        Dim retval As DTOProduct = Nothing
        Dim value = Await Api.Fetch(Of JObject)(exs, "Product", oGuid.ToString())
        Dim jsonString = JsonHelper.Serialize(value, exs)
        Dim oSourceCod As DTOProduct.SourceCods = CInt(value("SourceCod"))
        Select Case oSourceCod
            Case DTOProduct.SourceCods.Brand
                retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTOProductBrand)(jsonString)
            Case DTOProduct.SourceCods.Category
                retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTOProductCategory)(jsonString)
            Case DTOProduct.SourceCods.Sku
                retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTOProductSku)(jsonString)
        End Select
        Return retval
    End Function


    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid) As DTOProduct
        Dim retval As DTOProduct = Nothing
        Dim value = Api.FetchSync(Of JObject)(exs, "Product", oGuid.ToString())
        Dim jsonString = JsonHelper.Serialize(value, exs)
        Dim oSourceCod As DTOProduct.SourceCods = CInt(value("SourceCod"))
        Select Case oSourceCod
            Case DTOProduct.SourceCods.Brand
                retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTOProductBrand)(jsonString)
            Case DTOProduct.SourceCods.Category
                retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTOProductCategory)(jsonString)
            Case DTOProduct.SourceCods.Sku
                retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DTOProductSku)(jsonString)
        End Select
        Return retval
    End Function

    Shared Function Load(ByRef src As DTOProduct, exs As List(Of Exception)) As Boolean
        If TypeOf src Is DTOProductBrand Then
            Dim oBrand As DTOProductBrand = src
            src = ProductBrand.FindSync(src.Guid, exs)
        ElseIf TypeOf src Is DTOProductCategory Then
            Dim oCategory As DTOProductCategory = DirectCast(src, DTOProductCategory)
            src = ProductCategory.FindSync(src.Guid, exs)
        ElseIf TypeOf src Is DTOProductSku Then
            Dim oSku As DTOProductSku = DirectCast(src, DTOProductSku)
            src = ProductSku.FindSync(exs, src.Guid)
        ElseIf src.sourceCod = DTOProduct.SourceCods.Brand Then
            src = ProductBrand.FindSync(src.Guid, exs)
        ElseIf src.sourceCod = DTOProduct.SourceCods.Category Then
            src = ProductCategory.FindSync(src.Guid, exs)
        ElseIf src.sourceCod = DTOProduct.SourceCods.Sku Then
            src = ProductSku.FindSync(exs, src.Guid)
        ElseIf src.sourceCod = DTOProduct.SourceCods.Dept Then
            src = Dept.FindSync(exs, src.Guid)
        End If
        Return exs.Count = 0
    End Function

    Shared Function Brand(exs As List(Of Exception), src As DTOProduct) As DTOProductBrand
        'Return Await Api.Fetch(Of DTOProductBrand)(exs, "Product/Brand", oProduct.Guid.ToString())
        Dim retval As DTOProductBrand = Nothing
        If src IsNot Nothing Then
            Try
                If TypeOf src Is DTOProductBrand Then
                    retval = src
                ElseIf TypeOf src Is DTOProductCategory Then
                    Dim oCategory As DTOProductCategory = src
                    If oCategory.Brand IsNot Nothing Then
                        retval = oCategory.Brand
                    Else
                        If ProductCategory.Load(oCategory, exs) Then
                            retval = oCategory.Brand
                        End If
                    End If
                ElseIf TypeOf src Is DTOProductSku Then
                    Dim oSku As DTOProductSku = src
                    If oSku.Category IsNot Nothing Then
                        Dim oCategory As DTOProductCategory = oSku.Category
                        If oCategory.Brand IsNot Nothing Then
                            retval = oCategory.Brand
                        Else
                            If ProductCategory.Load(oCategory, exs) Then
                                retval = oCategory.Brand
                            End If
                        End If
                    Else
                        If ProductSku.Load(oSku, exs) Then
                            retval = oSku.Category.Brand
                        End If
                    End If
                Else
                    src = src.ToDerivedClass
                    Select Case src.sourceCod
                        Case DTOProduct.SourceCods.Brand
                            Return src
                        Case DTOProduct.SourceCods.Category
                            Dim oCategory As DTOProductCategory = src
                            If ProductCategory.Load(oCategory, exs) Then
                                retval = oCategory.Brand
                            End If
                        Case DTOProduct.SourceCods.Sku
                            Dim oSku As DTOProductSku = src
                            If ProductSku.Load(oSku, exs) Then
                                retval = oSku.Category.Brand
                            End If
                    End Select
                End If
            Catch ex As Exception
                exs.Add(ex)
            End Try
        End If
        Return retval
    End Function


    Shared Function BrandSync(oProduct As DTOProduct) As DTOProductBrand
        Dim exs As New List(Of Exception)
        Return Api.FetchSync(Of DTOProductBrand)(exs, "Product/Brand", oProduct.Guid.ToString())
    End Function

    Shared Function Excerpt(oProduct As DTOProduct, oLang As DTOLang) As String
        Dim retval As String = ""
        retval = oProduct.Excerpt.Tradueix(oLang)
        If retval = "" Then
            Dim sContent = oProduct.Content.Tradueix(oLang)
            If sContent.Length > 0 Then
                retval = Excerpt(sContent, 250)
            End If
        End If
        Return retval
    End Function

    Shared Async Function FraccionarTemporalment(exs As List(Of Exception), oProduct As DTOProduct, oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "product/FraccionarTemporalment", oProduct.Guid.ToString, oUser.Guid.ToString())
    End Function

    Shared Async Function AllowUserToFraccionarInnerPack(exs As List(Of Exception), oProduct As DTOProduct, oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "productSku/AllowUserToFraccionarInnerPack", oProduct.Guid.ToString, oUser.Guid.ToString())
    End Function


    Shared Function Accordion(oProduct As DTOProduct, oLang As DTOLang) As List(Of DTOMenuItem)
        Dim retval As New List(Of DTOMenuItem)
        Select Case oProduct.sourceCod
            Case DTOProduct.SourceCods.Brand
                retval.Add(New DTOMenuItem(oLang.Tradueix("Puntos de venta", "Punts de venda", "Sale points", "Pontos de venda"), DTOProduct.Tabs.distribuidores))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Galería de imágenes", "Galeria d'imatges", "Image gallery", "Galeria de Imagens"), DTOProduct.Tabs.galeria))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Descargas", "Descàrregues", "Downloads", "Descargas"), DTOProduct.Tabs.descargas))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Vídeos", "Vídeos", "Movies", "Vídeos"), DTOProduct.Tabs.videos))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Artículos relacionados", "Articles relacionats", "Posts on this subject", "Artigos relacionados"), DTOProduct.Tabs.bloggerposts))
            Case DTOProduct.SourceCods.Dept
                'retval.Add(New DTOMenuItem(oLang.Tradueix("Accesorios", "Accesoris", "Accessories"), DTOProduct.Tabs.accesorios)) (cal relacionar el dept amb les categories -Una View?-)
                retval.Add(New DTOMenuItem(oLang.Tradueix("Puntos de venta", "Punts de venda", "Sale points", "Pontos de venda"), DTOProduct.Tabs.distribuidores))
                'retval.Add(New DTOMenuItem(oLang.Tradueix("Galería de imágenes", "Galería de imatges", "Image gallery", "Galeria"), DTOProduct.Tabs.galeria))
                'retval.Add(New DTOMenuItem(oLang.Tradueix("Descargas", "Descàrregues", "Downloads", "Descargas"), DTOProduct.Tabs.descargas))
                'retval.Add(New DTOMenuItem(oLang.Tradueix("Vídeos", "Vídeos", "Movies", "Vídeos"), DTOProduct.Tabs.videos))
                'retval.Add(New DTOMenuItem(oLang.Tradueix("Artículos relacionados", "Articles relacionats", "Posts on this subject", "Artigos relacionados"), DTOProduct.Tabs.bloggerposts))
            Case DTOProduct.SourceCods.Category
                retval.Add(New DTOMenuItem(oLang.Tradueix("Colección", "Col·lecció", "Designs", "Coleçao"), DTOProduct.Tabs.coleccion))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Accesorios", "Accesoris", "Accessories"), DTOProduct.Tabs.accesorios))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Puntos de venta", "Punts de venda", "Sale points", "Pontos de venda"), DTOProduct.Tabs.distribuidores))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Galería de imágenes", "Galeria d'imatges", "Image gallery", "Galeria de Imagens"), DTOProduct.Tabs.galeria))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Descargas", "Descàrregues", "Downloads", "Descargas"), DTOProduct.Tabs.descargas))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Vídeos", "Vídeos", "Movies", "Vídeos"), DTOProduct.Tabs.videos))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Artículos relacionados", "Articles relacionats", "Posts on this subject", "Artigos relacionados"), DTOProduct.Tabs.bloggerposts))
            Case DTOProduct.SourceCods.Sku
                Dim oSku As DTOProductSku = oProduct
                If oSku.Hereda Then
                    retval.Add(New DTOMenuItem(oLang.Tradueix("Descripción", "Descripció", "Description", "Descrição"), DTOProduct.Tabs.descripcion))
                End If
                retval.Add(New DTOMenuItem(oLang.Tradueix("Accesorios", "Accesoris", "Accessories", "Accessórios"), DTOProduct.Tabs.accesorios))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Puntos de venta", "Punts de venda", "Sale points", "Pontos de venda"), DTOProduct.Tabs.distribuidores))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Descargas", "Descàrregues", "Downloads", "Descargas"), DTOProduct.Tabs.descargas))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Vídeos", "Vídeos", "Movies", "Vídeos"), DTOProduct.Tabs.videos))
                retval.Add(New DTOMenuItem(oLang.Tradueix("Artículos relacionados", "Articles relacionats", "Posts on this subject", "Artigos relacionados"), DTOProduct.Tabs.bloggerposts))
        End Select
        Return retval
    End Function

    Shared Function FacebookWidget(domain As DTOWebDomain, Optional oProduct As DTOProduct = Nothing) As DTOrrssWidget
        Dim retval As New DTOrrssWidget
        retval.Titular = "matiasmasso.sa"
        'Initialize the JavaScript SDK using this app:
        retval.widgetId = "1436241433262925" 'M+O

        If oProduct IsNot Nothing Then
            If DTOProduct.Brand(oProduct).Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Romer)) Then
                Select Case domain.Id
                    Case DTOWebDomain.Ids.matiasmasso_pt
                        retval.Titular = "BritaxPT"
                        retval.widgetId = "489736407757151"
                    Case Else
                        'retval.Titular = "britax.romer.espana"
                        retval.Titular = "BritaxES"
                        retval.widgetId = "489736407757151"
                End Select
            ElseIf DTOProduct.Brand(oProduct).Equals(DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.FourMoms)) Then
                Select Case domain.Id
                    Case DTOWebDomain.Ids.matiasmasso_pt
                        retval.Titular = "pages/4moms-España/488554607914396"
                        retval.widgetId = "489736407757151"
                    Case Else
                        retval.Titular = "pages/4moms-España/488554607914396"
                        retval.widgetId = "489736407757151"
                End Select
            End If
        End If

        retval.Url = "https://www.facebook.com/" & retval.Titular
        Return retval
    End Function

    Shared Function Url_Deprecated(oProduct As DTOProduct, Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim oDomain = DTOWebDomain.Factory(, AbsoluteUrl)
        Dim retval = oProduct.GetUrl(oDomain.DefaultLang, oTab, AbsoluteUrl)
        Return retval
    End Function

    Shared Function UrlThumbnail(oProduct As DTOProduct, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""

        If TypeOf oProduct Is DTOProductBrand Then
            retval = ProductBrand.LogoUrl(oProduct, AbsoluteUrl)
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            retval = ProductCategory.ThumbnailUrl(oProduct, AbsoluteUrl)
        ElseIf TypeOf oProduct Is DTOProductSku Then
            retval = DirectCast(oProduct, DTOProductSku).thumbnailUrl()
        End If

        Return retval
    End Function

    Shared Function ShowAtlas(src As DTOProduct) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean
        Dim oBrand As DTOProductBrand = Product.Brand(exs, src)
        If oBrand IsNot Nothing Then
            ProductBrand.Load(oBrand, exs)
            retval = oBrand.ShowAtlas
        End If
        Return retval
    End Function

    Shared Function BrandCodDist(oProduct As DTOProduct) As DTOProductBrand.CodDists
        Dim exs As New List(Of Exception)
        Dim retval As DTOProductBrand.CodDists
        Dim oBrand = Product.Brand(exs, oProduct)
        If oBrand IsNot Nothing Then
            ProductBrand.Load(oBrand, exs)
            retval = oBrand.CodDist
        End If
        Return retval
    End Function

    Shared Function Content(oProduct As DTOProduct, oLang As DTOLang, Optional AllowInheritance As Boolean = True) As String
        Dim exs As New List(Of Exception)
        Dim retval As String = ""
        If TypeOf oProduct Is DTOProductBrand Then
            Dim oBrand As DTOProductBrand = oProduct
            retval = oBrand.Excerpt.Tradueix(oLang)
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            Dim oCategory As DTOProductCategory = oProduct
            retval = oCategory.Content.Tradueix(oLang)
        ElseIf TypeOf oProduct Is DTOProductSku Then
            Dim oSku As DTOProductSku = oProduct
            If AllowInheritance And oSku.Hereda Then
                Dim oProductCategory As DTOProductCategory = oSku.Category
                ProductCategory.Load(oProductCategory, exs)
                retval = oProductCategory.Content.Tradueix(oLang)
            Else
                retval = oSku.Content.Tradueix(oLang)
            End If
        End If
        Return retval
    End Function

    Shared Function Description(oProduct As DTOProduct, oLang As DTOLang, Optional AllowInheritance As Boolean = True) As String 'TO DEPRECATE
        Dim exs As New List(Of Exception)
        Dim retval As String = ""
        If TypeOf oProduct Is DTOProductBrand Then
            Dim oBrand As DTOProductBrand = oProduct
            retval = oBrand.Excerpt.Tradueix(oLang)
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            Dim oCategory As DTOProductCategory = oProduct
            retval = oCategory.Content.Tradueix(oLang)
        ElseIf TypeOf oProduct Is DTOProductSku Then
            Dim oSku As DTOProductSku = oProduct
            If AllowInheritance And oSku.Hereda Then
                Dim oProductCategory As DTOProductCategory = oSku.Category
                ProductCategory.Load(oProductCategory, exs)
                retval = oProductCategory.Content.Tradueix(oLang)
            Else
                retval = oSku.Content.Tradueix(oLang)
            End If
        End If
        Return retval
    End Function


    Shared Function Excerpt(oProduct As DTOProduct, ByVal oLang As DTOLang, Optional ByVal MaxLen As Integer = 0, Optional BlAppendEllipsis As Boolean = True) As String
        Dim retval As String = ""
        Select Case oProduct.sourceCod
            Case DTOProduct.SourceCods.Brand
                Dim oBrand As DTOProductBrand = oProduct
                retval = oBrand.Excerpt.Tradueix(oLang)
            Case DTOProduct.SourceCods.Category
                Dim oCategory As DTOProductCategory = oProduct
                retval = oCategory.Excerpt.Tradueix(oLang)
            Case DTOProduct.SourceCods.Sku
                Dim oSku As DTOProductSku = oProduct
                retval = oSku.Excerpt.Tradueix(oLang)
        End Select
        If retval = "" Then
            Dim content = Product.Content(oProduct, oLang, True)
            retval = Excerpt(content, MaxLen, BlAppendEllipsis)
        End If
        Return retval
    End Function

    Shared Function Excerpt(sLongText As String, Optional ByVal MaxLen As Integer = 0, Optional BlAppendEllipsis As Boolean = True) As String
        If sLongText > "" Then
            If sLongText.IndexOf("<more/>") >= 0 Then
                sLongText = sLongText.Substring(0, sLongText.IndexOf("<more/>"))
            Else
                If sLongText > "" Then
                    If MaxLen > 0 Then
                        Dim ellipsis As String = IIf(BlAppendEllipsis, "...", "")
                        If sLongText.Length > MaxLen - ellipsis.Length Then
                            Dim iLastBlank As Integer = sLongText.Substring(0, MaxLen).LastIndexOf(" ")
                            If iLastBlank > 0 Then
                                sLongText = sLongText.Substring(0, iLastBlank) & ellipsis
                            Else
                                sLongText = sLongText.Substring(0, MaxLen - ellipsis.Length) & ellipsis
                            End If
                        End If
                    End If
                End If
            End If
        End If
        Return sLongText
    End Function


    Shared Function Html(oProduct As DTOProduct, oLang As DTOLang) As String
        Dim retval As String = Product.Content(oProduct, oLang)
        If retval > "" Then
            retval = retval.Replace(vbCrLf, "<br/>")
        End If
        Return retval
    End Function

    Shared Function TwitterWidget(oUser As DTOUser, oProduct As DTOProduct) As DTOrrssWidget
        Dim exs As New List(Of Exception)
        Dim retval As New DTOrrssWidget
        retval.Titular = "matiasmasso"
        retval.widgetId = "400568373211185152"

        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
            Case DTORol.Ids.rep, DTORol.Ids.comercial

        End Select

        If oProduct IsNot Nothing Then
            Dim oBrand = Product.Brand(exs, oProduct)
            Select Case oBrand.Guid.ToString.ToUpper
                Case "D4C2BC59-046D-42D3-86E3-BDCA91FB473F" 'romer
                    retval.Titular = "romer_es"
                    retval.widgetId = "467050866231361537"
                Case "D56CE172-3C98-48E0-A378-8718BE8622F7" 'britax
                    retval.Titular = "romer_es"
                    retval.widgetId = "467050866231361537"
                Case "B1A0FB03-0C18-4607-9091-DF5A6A635BB0" 'inglesina
                    retval.Titular = "inglesinaBebe"
                    retval.widgetId = "467056444156542976"
            End Select
        End If
        retval.Url = "https://twitter.com/" & retval.Titular
        Return retval
    End Function

    Shared Function InstagramWidget(oProduct As DTOProduct) As DTOrrssWidget
        Dim exs As New List(Of Exception)
        Dim retval As DTOrrssWidget = Nothing

        If oProduct IsNot Nothing Then
            Dim oBrand = Product.Brand(exs, oProduct)
            Select Case oBrand.Guid.ToString.ToUpper
                Case "67058F90-1FD6-4AE6-82ED-78447779B358" '4moms
                    retval = New DTOrrssWidget()
                    retval.Titular = "@4moms_hq"
                    retval.Url = "https://snapwidget.com/in/?u=NG1vbXNfaHF8aW58NzV8MnwzfHxub3w1fG5vbmV8b25TdGFydHx5ZXM=&v=19614"
            End Select
        End If
        Return retval
    End Function


    Shared Function showBlog(oProduct As DTOProduct) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = True
        Dim oBrand = Product.Brand(exs, oProduct)
        If oBrand IsNot Nothing Then
            Select Case oBrand.Guid.ToString
                Case "b1a0fb03-0c18-4607-9091-df5a6a635bb0" 'inglesina
                    retval = False
            End Select
        End If
        Return retval
    End Function

    Shared Function PillNavbarItem(oProduct As DTOProduct, oPillTab As DTOProduct.Tabs, oActiveTab As DTOProduct.Tabs, oLang As DTOLang, Esp As String, Optional Cat As String = "", Optional Eng As String = "", Optional Por As String = "") As DTONavbarItem
        Dim sTitle As String = oLang.Tradueix(Esp, Cat, Eng, Por)
        Dim active As Boolean = (oPillTab = oActiveTab)
        Dim sUrl As String = oProduct.GetUrl(oLang, oPillTab)
        Dim retval = DTONavbarItem.Factory(sTitle, sUrl, active)
        Return retval
    End Function

    Shared Function Pills(oProduct As DTOProduct, oLang As DTOLang, oActiveTab As DTOProduct.Tabs) As DTONavbar
        Dim exs As New List(Of Exception)
        Dim oNavbar = DTONavbar.Factory(DTONavbar.Formats.horizontal)
        Select Case oProduct.sourceCod
            Case DTOProduct.SourceCods.Brand
                oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.general, oActiveTab, oLang, "productos", "productes", "products", "produtos"))
                If Product.Brand(exs, oProduct).ShowAtlas Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.distribuidores, oActiveTab, oLang, "puntos de venta", "punts de venda", "sale points", "pontos de venda"))
                End If
                If MediaResources.ExistsFromProductOrChildrenSync(exs, oProduct) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.galeria, oActiveTab, oLang, "galería", "galeria", "gallery", "galeria"))
                End If
                If ProductDownloads.ExistsFromProductOrParentSync(oProduct, False, True) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.descargas, oActiveTab, oLang, "descargas", "descarregues", "downloads", "descargas"))
                End If
                If YouTubeMovies.ExistFromProductSync(exs, oProduct.Guid, oLang) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.videos, oActiveTab, oLang, "Vídeos", "Vídeos", "Vídeos", "vídeos"))
                End If
            Case DTOProduct.SourceCods.Dept
                oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.general, oActiveTab, oLang, "características", "característiques", "features", "características"))
                'oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.coleccion, oActiveTab, oLang, "colección", "colecció", "collection", "coleção"))
                'If ProductAccessoriesLoader.Exist(oProduct) Then
                'oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.accesorios, oActiveTab, oLang, "accesorios", "accessoris", "accessories", "accessórios"))
                'End If
                'If Brand(oProduct).ShowAtlas Then
                oNavbar.Items.Add(Product.PillNavbarItem(DirectCast(oProduct, DTODept).Brand, DTOProduct.Tabs.distribuidores, oActiveTab, oLang, "puntos de venta", "punts de venda", "sale points", "pontos de venda"))
                'End If
                'If MediaResources.ExistsFromProductOrChildrenSync(exs,oProduct) Then
                'oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.galeria, oActiveTab, oLang, "galería", "galeria", "gallery", "galeria"))
                'End If
                'If ProductDownloads.ExistsFromProductOrParentSync(oProduct, False, True) Then
                'oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.descargas, oActiveTab, oLang, "descargas", "descarregues", "downloads", "descargas"))
                'End If
                'If YouTubeMoviesLoader.ExistFromProduct(oProduct.Guid) Then
                'oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.videos, oActiveTab, oLang, "Vídeos", "Vídeos", "Vídeos", "vídeos"))
                'End If
            Case DTOProduct.SourceCods.Category
                oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.general, oActiveTab, oLang, "características", "característiques", "features", "características"))
                oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.coleccion, oActiveTab, oLang, "colección", "colecció", "collection", "coleção"))
                If Product.RelatedsExistSync(exs, DTOProduct.Relateds.Accessories, oProduct) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.accesorios, oActiveTab, oLang, "accesorios", "accessoris", "accessories", "accessórios"))
                End If
                If Product.Brand(exs, oProduct).ShowAtlas Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.distribuidores, oActiveTab, oLang, "puntos de venta", "punts de venda", "sale points", "pontos de venda"))
                End If
                If MediaResources.ExistsFromProductOrChildrenSync(exs, oProduct) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.galeria, oActiveTab, oLang, "galería", "galeria", "gallery", "galeria"))
                End If
                If Downloads.FromProductOrParentSync(exs, oProduct, False, True, oLang).Count > 0 Then
                    'If ProductDownloads.ExistsFromProductOrParentSync(oProduct, False, True) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.descargas, oActiveTab, oLang, "descargas", "descarregues", "downloads", "descargas"))
                End If
                If YouTubeMovies.ExistFromProductSync(exs, oProduct.Guid, oLang) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.videos, oActiveTab, oLang, "Vídeos", "Vídeos", "Vídeos", "vídeos"))
                End If
            Case DTOProduct.SourceCods.Sku
                oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.general, oActiveTab, oLang, "características", "característiques", "features", "características"))
                If Product.RelatedsExistSync(exs, DTOProduct.Relateds.Accessories, oProduct) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.accesorios, oActiveTab, oLang, "accesorios", "accessoris", "accessories", "accessórios"))
                End If
                If Product.Brand(exs, oProduct).ShowAtlas Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.distribuidores, oActiveTab, oLang, "puntos de venta", "punts de venda", "sale points", "pontos de venda"))
                End If
                If MediaResources.ExistsFromProductOrChildrenSync(exs, oProduct) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.galeria, oActiveTab, oLang, "galería", "galeria", "gallery", "galeria"))
                End If
                If ProductDownloads.ExistsFromProductOrParentSync(oProduct, False, True) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.descargas, oActiveTab, oLang, "descargas", "descarregues", "downloads", "descargas"))
                End If
                If YouTubeMovies.ExistFromProductSync(exs, oProduct.Guid, oLang) Then
                    oNavbar.Items.Add(Product.PillNavbarItem(oProduct, DTOProduct.Tabs.videos, oActiveTab, oLang, "Vídeos", "Vídeos", "Vídeos", "vídeos"))
                End If
        End Select
        Return oNavbar
    End Function

    Shared Function RelatedsExistSync(exs As List(Of Exception), cod As DTOProduct.Relateds, oProduct As DTOProduct) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "Product/relateds/Exist", cod, oProduct.Guid.ToString())
    End Function

    Shared Async Function Relateds(exs As List(Of Exception), cod As DTOProduct.Relateds, oTargetProduct As DTOProduct, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsoletos As Boolean = False, Optional AllowInheritance As Boolean = True) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "Product/relateds", cod, oTargetProduct.Guid.ToString, OpcionalGuid(oMgz), OpcionalBool(IncludeObsoletos), OpcionalBool(AllowInheritance))
    End Function

    Shared Function RelatedsSync(exs As List(Of Exception), cod As DTOProduct.Relateds, oTargetProduct As DTOProduct, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsoletos As Boolean = False, Optional AllowInheritance As Boolean = True) As List(Of DTOProductSku)
        Return Api.FetchSync(Of List(Of DTOProductSku))(exs, "Product/relateds", cod, oTargetProduct.Guid.ToString, OpcionalGuid(oMgz), OpcionalBool(IncludeObsoletos), OpcionalBool(AllowInheritance))
    End Function

    Shared Async Function UpdateRelateds(exs As List(Of Exception), cod As DTOProduct.Relateds, oTargetProduct As DTOProduct, values As List(Of DTOProductSku)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTOProductSku))(values, exs, "Product/Relateds", cod, oTargetProduct.Guid.ToString())
    End Function

End Class

Public Class Products

    Shared Async Function FromNom(exs As List(Of Exception), oBrand As DTOProductBrand, nom As String) As Task(Of List(Of DTOProduct))
        Return Await Api.Execute(Of String, List(Of DTOProduct))(nom, exs, "Products/FromNom", oBrand.Guid.ToString())
    End Function

    Shared Async Function ForSiteMap(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of DTOProductBrand.Collection)
        Dim retval = Await Api.Fetch(Of DTOProductBrand.Collection)(exs, "Products/ForSiteMap", oEmp.Id)
        Return retval
    End Function

    Shared Async Function FromCnap(exs As List(Of Exception), cnapKey As String) As Task(Of List(Of DTOProduct))
        Return Await Api.Execute(Of String, List(Of DTOProduct))(cnapKey, exs, "Products/FromCnap")
    End Function


End Class
