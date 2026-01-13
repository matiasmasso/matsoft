Public Class WebPortadaBrand
    Inherits _FeblBase

    Shared Async Function Find(oBrand As DTOProductBrand, exs As List(Of Exception)) As Task(Of DTOWebPortadaBrand)
        Return Await Api.Fetch(Of DTOWebPortadaBrand)(exs, "WebPortadaBrand", oBrand.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWebPortadaBrand As DTOWebPortadaBrand) As Boolean
        If Not oWebPortadaBrand.IsLoaded And Not oWebPortadaBrand.IsNew Then
            Dim pWebPortadaBrand = Api.FetchSync(Of DTOWebPortadaBrand)(exs, "WebPortadaBrand", oWebPortadaBrand.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWebPortadaBrand)(pWebPortadaBrand, oWebPortadaBrand, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Image(exs As List(Of Exception), oBrand As DTOProductBrand) As Task(Of Image)
        Return Await Api.FetchImage(exs, "WebPortadaBrand/image", oBrand.Guid.ToString())
    End Function


    Shared Async Function Update(value As DTOWebPortadaBrand, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("image", value.Image)
            retval = Await Api.Upload(oMultipart, exs, "WebPortadaBrand")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oWebPortadaBrand As DTOWebPortadaBrand, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWebPortadaBrand)(oWebPortadaBrand, exs, "WebPortadaBrand")
    End Function

    Shared Function ImageUrl(value As DTOWebPortadaBrand, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = UrlHelper.Image(DTO.Defaults.ImgTypes.WebPortadaBrand, value.Guid, AbsoluteUrl)
        Return retval
    End Function
End Class

Public Class WebPortadaBrands
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oChannel As DTODistributionChannel = Nothing) As Task(Of List(Of DTOWebPortadaBrand))
        Return Await Api.Fetch(Of List(Of DTOWebPortadaBrand))(exs, "WebPortadaBrands", OpcionalGuid(oChannel))
    End Function

    Shared Async Function Sort(exs As List(Of Exception), values As List(Of DTOWebPortadaBrand)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOWebPortadaBrand), Boolean)(values, exs, "WebPortadaBrands/sort")
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oLang As DTOLang) As Task(Of List(Of DTOWebPortadaBrand))
        Dim oChannel As DTODistributionChannel = DTODistributionChannel.wellknown(DTODistributionChannel.wellknowns.botiga)
        Return Await All(exs, oChannel)
    End Function

    Shared Async Function Sprite(exs As List(Of Exception), oLang As DTOLang) As Task(Of SpriteHelper.Sprite)
        Dim retval As SpriteHelper.Sprite = Nothing
        Dim oHeaders = Await Headers(exs, oLang)
        If exs.Count = 0 Then
            retval = SpriteHelper.Factory(SpriteUrl(), DTOWebPortadaBrand.width, DTOWebPortadaBrand.height)
            For Each item In oHeaders
                retval.addItem(item.Brand.nom, item.Brand.Url)
            Next
        End If
        Return retval
    End Function

    Shared Function SpriteUrl() As String
        Dim oChannel As DTODistributionChannel = DTODistributionChannel.wellknown(DTODistributionChannel.wellknowns.botiga)
        Return MmoUrl.ApiUrl("WebPortadaBrands/sprite", OpcionalGuid(oChannel))
    End Function
End Class
