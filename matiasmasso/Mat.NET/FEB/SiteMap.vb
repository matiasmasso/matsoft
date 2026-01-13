Imports System.Xml
Public Class SiteMap

    Shared Async Function Load(exs As List(Of Exception), oCache As Models.ClientCache, oDomain As DTOWebDomain, sFileType As String) As Task(Of DTOSiteMap)
        Dim retval As DTOSiteMap = Nothing
        Dim oEmp = oCache.Emp
        Dim oLastNews As List(Of DTONoticia) = Nothing
        Dim oProducts = Await Products.ForSiteMap(exs, oEmp)
        Dim oType As DTOSiteMap.Types = DTOSiteMap.Types.NotSet
        Select Case sFileType.ToLower
            Case "index.xml"
                oLastNews = Await LastNews(exs, oEmp, oDomain)
                retval = MainIndex(oDomain, oLastNews)
            Case "product-brands.xml"
                retval = product_brands(exs, oEmp, oDomain, oProducts)
            Case "product-depts.xml"
                retval = product_depts(exs, oCache, oDomain, oProducts)
            Case "product-categories.xml"
                retval = product_categories(exs, oEmp, oDomain, oProducts)
            Case "product-skus.xml"
                retval = product_skus(exs, oEmp, oDomain, oProducts)
            Case "noticias.xml"
                retval = Await Noticias(exs, oEmp, oDomain)
            Case "blog.xml"
                retval = Await BlogPosts(exs, oEmp, oDomain)
            Case "lastnews.xml", "sitemap_lastnews.xml"
                If oLastNews Is Nothing Then oLastNews = Await LastNews(exs, oEmp, oDomain)
                If oLastNews IsNot Nothing Then
                    retval = LastNewsSiteMap(oLastNews, oDomain)
                End If
        End Select
        Return retval
    End Function


    Private Shared Function MainIndex(oDomain As DTOWebDomain, oLastNews As List(Of DTONoticia)) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Index, DTOSiteMap.Modes.Index)
        With retval
            .Urls.Add(oDomain.Url("sitemaps/product-brands.xml"))
            .Urls.Add(oDomain.Url("sitemaps/product-depts.xml"))
            .Urls.Add(oDomain.Url("sitemaps/product-categories.xml"))
            .Urls.Add(oDomain.Url("sitemaps/product-skus.xml"))
            .Urls.Add(oDomain.Url("sitemaps/noticias.xml"))
            .Urls.Add(oDomain.Url("sitemaps/blog.xml"))
            If oLastNews.Count > 0 Then
                .Urls.Add(oDomain.Url("sitemaps/lastnews.xml"))
            End If
        End With

        Return retval
    End Function

    Private Shared Function product_brands(exs As List(Of Exception), oEmp As DTOEmp, oDomain As DTOWebDomain, oProducts As DTOProductBrand.Collection) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Brands)
        For Each oBrand As DTOProductBrand In oProducts
            Dim sUrl As String = oBrand.UrlCanonicas.AbsoluteUrl(oDomain.DefaultLang) ' oBrand.GetUrl(oDomain)
            Dim item = retval.AddItem(sUrl, oBrand.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 0.9)
            For Each olang In DTOLang.Collection.All
                item.AddLangRef(olang, oBrand.UrlCanonicas.CanonicalUrl(olang))
            Next
            Dim oNoLang As DTOLang = Nothing
            item.AddLangRef(oNoLang, oBrand.UrlCanonicas.AbsoluteUrl(oNoLang))
        Next
        Return retval
    End Function

    Private Shared Function product_depts(exs As List(Of Exception), oCache As Models.ClientCache, oDomain As DTOWebDomain, oProducts As DTOProductBrand.Collection) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Depts)
        Dim oDepts = oCache.Depts.Where(Function(x) x.obsoleto = False).ToList()
        For Each oDept As DTODept In oDepts
            Dim sUrl = oDept.UrlCanonicas.AbsoluteUrl(DTOLang.ESP)
            Dim item = retval.AddItem(sUrl, oDept.UsrLog.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 0.9)
            For Each olang In DTOLang.Collection.All
                item.AddLangRef(olang, oDept.UrlCanonicas.CanonicalUrl(olang))
            Next
            Dim oNoLang As DTOLang = Nothing
            item.AddLangRef(oNoLang, oDept.UrlCanonicas.AbsoluteUrl(oNoLang))
        Next
        Return retval
    End Function

    Private Shared Function product_depts_Old(exs As List(Of Exception), oEmp As DTOEmp, oDomain As DTOWebDomain, oProducts As DTOProductBrand.Collection) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Depts)
        Dim oDepts = oProducts.SelectMany(Function(x) x.Depts).ToList()
        For Each oDept As DTODept In oDepts
            Dim sUrl As String = oDept.UrlCanonicas.AbsoluteUrl(oDomain.DefaultLang) ' oBrand.GetUrl(oDomain)
            Dim item = retval.AddItem(sUrl, oDept.UsrLog.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 0.9)
            For Each olang In DTOLang.Collection.All
                item.AddLangRef(olang, oDept.UrlCanonicas.CanonicalUrl(olang))
            Next
            Dim oNoLang As DTOLang = Nothing
            item.AddLangRef(oNoLang, oDept.UrlCanonicas.AbsoluteUrl(oNoLang))
        Next
        Return retval
    End Function

    Private Shared Function product_categories(exs As List(Of Exception), oEmp As DTOEmp, oDomain As DTOWebDomain, oProducts As DTOProductBrand.Collection) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Categories)
        Dim oCategories = oProducts.SelectMany(Function(x) x.Depts).SelectMany(Function(y) y.Categories).ToList()
        oCategories.AddRange(oProducts.SelectMany(Function(x) x.Categories))

        For Each oCategory In oCategories
            Dim sUrl As String = oCategory.UrlCanonicas.AbsoluteUrl(oDomain.DefaultLang) ' oBrand.GetUrl(oDomain)
            Dim s4momsUrl = Url4momsOrElse(sUrl)
            If s4momsUrl = sUrl Then
                Dim item = retval.AddItem(sUrl, oCategory.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 0.9)
                For Each olang In DTOLang.Collection.All
                    item.AddLangRef(olang, oCategory.UrlCanonicas.CanonicalUrl(olang))
                Next
                Dim oNoLang As DTOLang = Nothing
                item.AddLangRef(oNoLang, oCategory.UrlCanonicas.AbsoluteUrl(oNoLang))
            Else
                retval.AddItem(s4momsUrl, oCategory.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 0.9)
            End If
        Next
        Return retval
    End Function

    Private Shared Function Url4momsOrElse(src As String) As String
        Dim retval As String = src
        Dim snippetEs = "https://www.matiasmasso.es/4moms/"
        Dim snippetPt = "https://www.matiasmasso.pt/4moms/"
        Dim Es4moms = "https://www.4moms.es/"
        Dim Pt4moms = "https://www.4moms.pt/"
        If src.StartsWith(snippetEs) Then
            retval = src.Replace(snippetEs, Es4moms)
        ElseIf src.StartsWith(snippetPt) Then
            retval = src.Replace(snippetPt, Pt4moms)
        End If
        Return retval
    End Function

    Private Shared Function product_skus(exs As List(Of Exception), oEmp As DTOEmp, oDomain As DTOWebDomain, oProducts As DTOProductBrand.Collection) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Product_Skus)
        Dim oCategories = oProducts.SelectMany(Function(x) x.Depts).SelectMany(Function(y) y.Categories).ToList()
        oCategories.AddRange(oProducts.SelectMany(Function(x) x.Categories))
        Dim oSkus = oCategories.SelectMany(Function(x) x.Skus).ToList()
        Dim oLang = oDomain.DefaultLang
        Dim DtFch As Date = DTO.GlobalVariables.Now()

        For Each oSku As DTOProductSku In oSkus
            Dim sUrl As String = oSku.UrlCanonicas.AbsoluteUrl(oDomain.DefaultLang)

            Dim s4momsUrl = Url4momsOrElse(sUrl)
            If s4momsUrl = sUrl Then
                Dim item = retval.AddItem(sUrl, oSku.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 0.9)
                For Each oLang In DTOLang.Collection.All
                    item.AddLangRef(oLang, oSku.UrlCanonicas.CanonicalUrl(oLang))
                Next
                Dim oNoLang As DTOLang = Nothing
                item.AddLangRef(oNoLang, oSku.UrlCanonicas.AbsoluteUrl(oNoLang))
            Else
                retval.AddItem(s4momsUrl, oSku.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 0.9)
            End If

        Next

        Return retval
    End Function

    Private Shared Async Function Noticias(exs As List(Of Exception), oEmp As DTOEmp, oDomain As DTOWebDomain) As Task(Of DTOSiteMap)
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.Noticias)
        Dim oNoticias = Await FEB.Noticias.HeadersForSitemap(exs, oEmp)
        For Each oNoticia As DTONoticia In oNoticias
            If oNoticia.Visible And Not oNoticia.professional Then
                Dim matchLang As Boolean = False
                Select Case oDomain.Id
                    Case DTOWebDomain.Ids.matiasmasso_pt
                        matchLang = Not String.IsNullOrEmpty(oNoticia.Title.Por)
                    Case DTOWebDomain.Ids.matiasmasso_es
                        matchLang = Not String.IsNullOrEmpty(oNoticia.Title.Esp)
                End Select
                If matchLang Then
                    Dim sRoutingUrl As String = oNoticia.Url().CanonicalUrl(oDomain.DefaultLang).ToLower
                    Dim item = retval.AddItem(sRoutingUrl, oNoticia.UsrLog.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, oNoticia.priority / 10)
                    For Each oLang In oNoticia.Title.Langs
                        item.AddLangRef(oLang, oNoticia.Url().CanonicalUrl(oLang))
                    Next
                    item.AddDefaultRef(oNoticia.Url().AbsoluteUrl(oDomain.DefaultLang))
                End If
            End If
        Next

        Return retval
    End Function

    Private Shared Async Function LastNews(exs As List(Of Exception), oEmp As DTOEmp, oDomain As DTOWebDomain) As Task(Of List(Of DTONoticia))
        Dim retval As New List(Of DTONoticia)
        Dim oNoticias = Await FEB.Noticias.HeadersForSitemap(exs, oEmp)
        For Each oNoticia As DTONoticia In oNoticias
            If oNoticia.isVisibleOnNewsSitemap(oDomain) Then
                retval.Add(oNoticia)
            End If
        Next
        Return retval
    End Function

    Private Shared Function LastNewsSiteMap(oNoticias As List(Of DTONoticia), oDomain As DTOWebDomain) As DTOSiteMap
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.LastNews, DTOSiteMap.Modes.News)
        For Each oNoticia As DTONoticia In oNoticias
            retval.AddItems(oNoticia, oDomain)
        Next
        Return retval
    End Function

    Private Shared Async Function NoticiasxCategoria(exs As List(Of Exception), oDomain As DTOWebDomain) As Task(Of DTOSiteMap)
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.NoticiasxCategoria)

        Dim oCategorias = Await CategoriasDeNoticia.ForSiteMap(exs)
        For Each oCategoria As DTOCategoriaDeNoticia In oCategorias
            Dim sRoutingUrl As String = oCategoria.Url(oDomain)
            retval.AddItem(sRoutingUrl, oCategoria.FchLastEdited, DTOSiteMap.ChangeFreqs.monthly, 8 / 10)
        Next

        Return retval
    End Function

    Private Shared Async Function BlogPosts(exs As List(Of Exception), oEmp As DTOEmp, oDomain As DTOWebDomain) As Task(Of DTOSiteMap)
        Dim retval As New DTOSiteMap(DTOSiteMap.Types.BlogPosts)
        Dim oBlogPosts = Await FEB.BlogPosts.All(exs, oDomain.DefaultLang())
        For Each oBlogPost As DTOBlogPost In oBlogPosts
            If oBlogPost.Visible Then
                Dim sRoutingUrl As String = oBlogPost.Url().CanonicalUrl(oDomain.DefaultLang)
                Dim item = retval.AddItem(sRoutingUrl, oBlogPost.Fch, DTOSiteMap.ChangeFreqs.monthly, 1)
                For Each oLang In oBlogPost.Title.Langs
                    item.AddLangRef(oLang, oBlogPost.Url.CanonicalUrl(oLang))
                Next
                Dim oNoLang As DTOLang = Nothing
                item.AddLangRef(oNoLang, oBlogPost.Url.AbsoluteUrl(oNoLang))
            End If
        Next

        Return retval
    End Function


    Shared Function Stream(oSitemap As DTOSiteMap) As Byte()
        Dim sb As New System.Text.StringBuilder

        Dim oXmlWriter As XmlWriter = XmlWriter.Create(sb, GetXmlWriterSettings)
        With oXmlWriter
            .WriteStartDocument() ' Write the Xml declaration.

            Select Case oSitemap.Mode
                Case DTOSiteMap.Modes.Index
                    .WriteComment("XML Sitemap Index generated by MATIAS MASSO, S.A.") ' Write a comment.
                    .WriteStartElement("sitemapindex", "http://www.sitemaps.org/schemas/sitemap/0.9") ' Write the root element.
                    For Each sUrl As String In oSitemap.Urls
                        WriteUrl(sUrl, oXmlWriter)
                    Next
                Case DTOSiteMap.Modes.Web
                    .WriteComment("XML Sitemap generated by MATIAS MASSO, S.A.") ' Write a comment.
                    .WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9") ' Write the root element.
                    For Each oItem As DTOSiteMapItem In oSitemap.Items
                        WriteItem(oItem, oXmlWriter)
                    Next
                Case DTOSiteMap.Modes.News
                    .WriteComment("XML News Sitemap generated by MATIAS MASSO, S.A.") ' Write a comment.
                    .WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9") ' Write the root element.
                    .WriteAttributeString("xmlns", "news", Nothing, "http://www.google.com/schemas/sitemap-news/0.9")
                    For Each oItem As DTOSiteMapItem In oSitemap.Items
                        WriteNewsItem(oItem, oXmlWriter)
                    Next
            End Select
            If oSitemap.Type = DTOSiteMap.Types.Index Then
            Else
            End If

            .WriteEndDocument()
            .Close()
        End With

        'sb.Insert(0, "<?xml-stylesheet type='text/xsl' href='https://www.matiasmasso.es/sitemap.xsl'?>")
        Dim sText As String = sb.ToString
        sText = sText.Replace("utf-16", "utf-8")
        sText = sText.Replace("UTF-16", "utf-8")

        Dim encoding As New System.Text.UTF8Encoding()
        Dim retval As Byte() = encoding.GetBytes(sText)

        Return retval
    End Function

    Private Shared Sub WriteUrl(sUrl As String, ByRef oXmlWriter As XmlWriter)
        With oXmlWriter
            .WriteStartElement("sitemap")

            .WriteStartElement("loc")
            .WriteString(sUrl)
            .WriteEndElement()

            .WriteEndElement()
        End With
    End Sub

    Private Shared Sub WriteItem(item As DTOSiteMapItem, ByRef oXmlWriter As XmlWriter)
        With oXmlWriter
            .WriteStartElement("url")

            .WriteStartElement("loc")
            .WriteString(item.Loc)
            .WriteEndElement()

            If item.Lastmod <> DateTimeOffset.MinValue Then
                Dim dateOffset As DateTimeOffset = item.Lastmod
                If item.Lastmod = Nothing Then dateOffset = DateTimeOffset.MinValue
                Dim sLastmod As String = dateOffset.ToString("o") 'round trip date format
                .WriteStartElement("lastmod")
                .WriteString(sLastmod)
                .WriteEndElement()
            End If

            If item.Changefreq <> DTOSiteMap.ChangeFreqs.NotSet Then
                .WriteStartElement("changefreq")
                .WriteString(item.Changefreq.ToString())
                .WriteEndElement()
            End If

            If item.Priority <> 0 Then
                Dim sPriority As String = TextHelper.VbFormat(item.Priority, "0.00").Replace(",", ".")
                .WriteStartElement("priority")
                .WriteString(sPriority)
                .WriteEndElement()
            End If

            For Each oLangRef In item.LangRefs
                .WriteStartElement("xhtml", "link", "http://www.w3.org/1999/xhtml")
                .WriteAttributeString("rel", "alternate")
                If oLangRef.Lang Is Nothing Then
                    Dim espLangRef = item.LangRefs.FirstOrDefault(Function(x) x.Lang.Tag = "ESP")
                    .WriteAttributeString("hreflang", "x-default")
                    .WriteAttributeString("href", espLangRef.Url)
                Else
                    .WriteAttributeString("hreflang", oLangRef.Lang.ISO6391)
                    .WriteAttributeString("href", oLangRef.Url)
                End If
                .WriteEndElement()

            Next

            If item.Images IsNot Nothing Then
                For Each oImage As DTOSitemapItemImage In item.Images
                    WriteImage(oImage, oXmlWriter)
                Next
            End If

            .WriteEndElement()
        End With
    End Sub
    Private Shared Sub WriteNewsItem(item As DTOSiteMapItem, ByRef oXmlWriter As XmlWriter)
        Dim newsNamespace = "http://www.google.com/schemas/sitemap-news/0.9"
        With oXmlWriter
            .WriteStartElement("url")

            .WriteStartElement("loc")
            .WriteString(item.Loc)
            .WriteEndElement()

            .WriteStartElement("news", "news", newsNamespace)

            .WriteStartElement("news", "publication", newsNamespace)

            .WriteStartElement("news", "name", newsNamespace)
            .WriteString(item.PublicationName)
            .WriteEndElement()

            .WriteStartElement("news", "language", newsNamespace)
            .WriteString(item.PublicationLang)
            .WriteEndElement()

            .WriteEndElement() 'publication

            .WriteStartElement("news", "publication_date", newsNamespace)
            .WriteString(item.Lastmod.ToString("o"))
            .WriteEndElement()

            .WriteStartElement("news", "title", newsNamespace)
            .WriteString(item.Title)
            .WriteEndElement()


            .WriteEndElement() 'news
            .WriteEndElement() 'url

        End With
    End Sub

    Private Shared Sub WriteImage(oImage As DTOSitemapItemImage, ByRef oXmlWriter As XmlWriter)
        Dim sNameSpace As String = "http://www.google.com/schemas/sitemap-image/1.1"
        With oXmlWriter
            .WriteStartElement("image", "image", sNameSpace)

            .WriteStartElement("image", "loc", sNameSpace)
            .WriteString(oImage.loc)
            .WriteEndElement()

            If oImage.title > "" Then
                .WriteStartElement("image", "title", sNameSpace)
                .WriteString(oImage.title)
                .WriteEndElement()
            End If

            If oImage.caption > "" Then
                .WriteStartElement("image", "caption", sNameSpace)
                .WriteString(oImage.caption)
                .WriteEndElement()
            End If

            If oImage.license <> DTOSitemapItemImage.Licenses.None Then
                .WriteStartElement("image", "license", sNameSpace)
                Select Case oImage.license
                    Case DTOSitemapItemImage.Licenses.CCByNd
                        .WriteString("https://creativecommons.org/licenses/by-nd/3.0/")
                End Select
                .WriteEndElement()
            End If

            .WriteEndElement()
        End With
    End Sub

    Shared Sub Save(oSitemap As DTOSiteMap, sFilename As String)
        Dim encoding As New System.Text.UTF8Encoding()
        Dim sSrc As String = oSitemap.ToString
        Dim oArray As Byte() = encoding.GetBytes(sSrc)
        Dim exs As New List(Of Exception)
        FileSystemHelper.SaveStream(oArray, exs, sFilename)
    End Sub

    Shared Function GetXmlWriterSettings() As XmlWriterSettings
        Dim retval As New XmlWriterSettings()
        retval.Indent = True
        Return retval
    End Function
End Class


