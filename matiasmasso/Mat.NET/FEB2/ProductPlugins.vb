Imports System.Text
Imports System.Text.RegularExpressions

Public Class ProductPlugin
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOProductPlugin)
        Return Await Api.Fetch(Of DTOProductPlugin)(exs, "ProductPlugin", oGuid.ToString())
    End Function

    Shared Async Function Sprite(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "ProductPlugin/sprite", oGuid.ToString())
    End Function

    Shared Function SpriteSync(exs As List(Of Exception), oGuid As Guid)As Byte() 
        Return Api.FetchImageSync(exs, "ProductPlugin/sprite", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oProductPlugin As DTOProductPlugin, Optional includeSprite As Boolean = True) As Boolean
        If Not oProductPlugin.IsLoaded And Not oProductPlugin.IsNew Then
            Dim pProductPlugin = Api.FetchSync(Of DTOProductPlugin)(exs, "ProductPlugin", oProductPlugin.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProductPlugin)(pProductPlugin, oProductPlugin, exs)
                For Each item In oProductPlugin.Items
                    item.Plugin = oProductPlugin
                Next
                If includeSprite Then
                    Dim oSprite = SpriteSync(exs, oProductPlugin.Guid)
                    For idx = 0 To oProductPlugin.Items.Count - 1
                        Dim item = oProductPlugin.Items(idx)
                        item.Thumbnail = LegacyHelper.SpriteHelper.Extract(oSprite, idx, oProductPlugin.Items.Count)
                    Next
                End If
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oProductPlugin As DTOProductPlugin) As Task(Of Boolean)
        Return Await Api.Update(Of DTOProductPlugin)(oProductPlugin, exs, "ProductPlugin")
        oProductPlugin.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oProductPlugin As DTOProductPlugin) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProductPlugin)(oProductPlugin, exs, "ProductPlugin")
    End Function

    Public Shared Function Html(oCache As Models.ClientCache, oPlugin As DTOProductPlugin, ByVal lang As DTOLang) As String
        Dim width As Integer = DTOProductSku.THUMBNAILWIDTH
        Dim height As Integer = DTOProductSku.THUMBNAILHEIGHT
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append("<!------------------------------------- -->")
        sb.Append("<div class='PluginWrapper'>")
        sb.Append("<div class='Plugin' data-pluginId='" & oPlugin.Guid.ToString() & "'>")
        sb.Append("    <a href='#' class='ChevronLeft'><i class='fa-solid fa-chevron-left'></i></a>")
        sb.Append("    <div>")

        For Each item In oPlugin.Items
            Dim oProduct = oCache.FindProduct(item.Product.Guid)
            If oProduct IsNot Nothing Then
                Dim href = oProduct.UrlCanonicas.RelativeUrl(lang)
                Dim text = item.LangNom.Tradueix(lang)
                Dim alt = text
                Dim src = oProduct.ThumbnailUrl()
                Dim s = String.Format("        <a href='{0}' title='{1}'><div><img src='{2}' width='{3}' height='{4}' alt='{1}'/></div><div>{5}</div></a>", href, alt, src, width, height, text)
                sb.Append(s)
            End If

            'Dim oSku = oCache.FindSku(item.Product.Guid)
            'If oSku IsNot Nothing Then
            'Dim href = oSku.UrlCanonicas.RelativeUrl(lang)
            'Dim text = item.LangNom.Tradueix(lang)
            'Dim alt = text
            'Dim src = oSku.thumbnailUrl()
            'Dim s = String.Format("        <a href='{0}' title='{1}'><div><img src='{2}' width='{3}' height='{4}' alt='{1}'/></div><div>{5}</div></a>", href, alt, src, width, height, text)
            'sb.Append(s)
            'End If
        Next

        sb.Append("    </div>")
        sb.Append("    <a href='#' class='ChevronRight'><i class='fa-solid fa-chevron-right'></i></a>")
        sb.Append("</div>")
        sb.Append("</div>")
        sb.Append("<!------------------------------------- -->")
        Dim retval As String = sb.ToString()
        Return retval
    End Function
End Class

Public Class ProductPlugins
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTOProductPlugin))
        Return Await Api.Fetch(Of List(Of DTOProductPlugin))(exs, "ProductPlugins", oProduct.Guid.ToString())
    End Function


    Public Shared Async Function Deploy(exs As List(Of Exception), ByVal src As DTOLangText) As Task(Of DTOLangText)
        Dim retval = src 'default value in case of error

        Dim oCache = Await FEB2.Cache.Fetch(exs, New DTOEmp(DTOEmp.Ids.MatiasMasso))
        If exs.Count = 0 Then
            retval.Esp = Deploy(oCache, src, DTOLang.ESP())
            retval.Cat = Deploy(oCache, src, DTOLang.CAT())
            retval.Eng = Deploy(oCache, src, DTOLang.ENG())
            retval.Por = Deploy(oCache, src, DTOLang.POR())
        End If
        Return retval
    End Function

    Public Shared Function Deploy(oCache As Models.ClientCache, ByVal oLangText As DTOLangText, ByVal lang As DTOLang) As String
        Dim src As String = oLangText.Text(lang)

        'jocker pattern to search for plugin markups on text
        Dim pattern = DTOProductPlugin.Snippet(GuidHelper.RegexPattern())

        Dim matches As MatchCollection = Regex.Matches(src, pattern)
        Dim input As StringBuilder = New StringBuilder(src)

        'replace plugin markups by their expanded html code
        For Each match As Match In matches
            Dim guid = New Guid(match.Groups("Guid").Value)
            If oCache.Categories.Any(Function(x) x.Guid.Equals(guid)) Then

                'build a new plugin with all active skus in category collection
                Dim oCategory As DTOProductCategory = oCache.Categories.First(Function(x) x.Guid.Equals(guid))
                Dim plugin = oCategory.PluginCollection(oCache)
                input = input.Replace(match.Value, ProductPlugin.Html(oCache, plugin, lang))
            Else

                'look for plugin on plugins collection
                Dim plugin = oCache.ProductPlugins.FirstOrDefault(Function(x) x.Guid.Equals(guid))
                If plugin IsNot Nothing Then
                    input = input.Replace(match.Value, ProductPlugin.Html(oCache, plugin, lang))
                End If
            End If
        Next

        Dim retval = input.ToString()
        Return retval
    End Function


End Class

