Public Class BlogPost
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOBlogPost)
        Return Await Api.Fetch(Of DTOBlogPost)(exs, "BlogPost", oGuid.ToString())
    End Function

    Shared Async Function FromFriendlySegment(exs As List(Of Exception), friendlyUrlSegment As String, oLang As DTOLang) As Task(Of DTOBlogPostModel)
        Dim retval = Await Execute(Of String, DTOBlogPostModel)(friendlyUrlSegment, exs, "BlogPost/model/FromFriendlyUrl", oLang.Tag)
        Return retval
    End Function

    Shared Async Function LastModel(exs As List(Of Exception), oLang As DTOLang) As Task(Of DTOBlogPostModel)
        Dim retval = Await Api.Fetch(Of DTOBlogPostModel)(exs, "BlogPost/model/last", oLang.Tag)
        Return retval
    End Function

    Shared Async Function Model(exs As List(Of Exception), oPost As DTOBlogPost, oLang As DTOLang) As Task(Of DTOBlogPostModel)
        Return Await Api.Fetch(Of DTOBlogPostModel)(exs, "BlogPost/model", oPost.Guid.ToString, oLang.Tag)
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oBlogPost As DTOBlogPost) As Boolean
        If Not oBlogPost.IsLoaded And Not oBlogPost.IsNew Then
            Dim pBlogPost = Api.FetchSync(Of DTOBlogPost)(exs, "BlogPost", oBlogPost.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBlogPost)(pBlogPost, oBlogPost, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Upload(exs As List(Of Exception), value As DTOBlogPost) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("Thumbnail", value.Thumbnail)
            retval = Await Api.Upload(oMultipart, exs, "BlogPost")
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oBlogPost As DTOBlogPost) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBlogPost)(oBlogPost, exs, "BlogPost")
    End Function
End Class

Public Class BlogPosts
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oLang As DTOLang = Nothing) As Task(Of List(Of DTOBlogPost))
        If oLang Is Nothing Then
            Return Await Api.Fetch(Of List(Of DTOBlogPost))(exs, "BlogPosts")
        Else
            Return Await Api.Fetch(Of List(Of DTOBlogPost))(exs, "BlogPosts", oLang.Tag)
        End If
    End Function

    Shared Async Function Models(exs As List(Of Exception), oLang As DTOLang) As Task(Of List(Of DTOBlogPostModel))
        Return Await Api.Fetch(Of List(Of DTOBlogPostModel))(exs, "BlogPosts/Models", oLang.Tag)
    End Function

End Class

