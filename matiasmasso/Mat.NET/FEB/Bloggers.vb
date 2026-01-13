Public Class Blogger

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBlogger)
        Return Await Api.Fetch(Of DTOBlogger)(exs, "Blogger", oGuid.ToString())
    End Function

    Shared Async Function Logo(oGuid As Guid, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "Blogger/logo", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oBlogger As DTOBlogger, exs As List(Of Exception)) As Boolean
        If Not oBlogger.IsLoaded And Not oBlogger.IsNew Then
            Dim pBlogger = Api.FetchSync(Of DTOBlogger)(exs, "Blogger", oBlogger.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBlogger)(pBlogger, oBlogger, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTOBlogger, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("logo", value.Logo)
            retval = Await Api.Upload(oMultipart, exs, "Blogger")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oBlogger As DTOBlogger, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBlogger)(oBlogger, exs, "Blogger")
    End Function

    Shared Function LogoUrl(oBlogger As DTOBlogger, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.BloggerLogo, oBlogger.Guid, AbsoluteUrl)
    End Function



End Class

Public Class Bloggers

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOBlogger))
        Return Await Api.Fetch(Of List(Of DTOBlogger))(exs, "Bloggers")
    End Function

End Class

