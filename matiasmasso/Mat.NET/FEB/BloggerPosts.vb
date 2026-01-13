Public Class BloggerPost

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBloggerPost)
        Return Await Api.Fetch(Of DTOBloggerPost)(exs, "BloggerPost", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oBloggerPost As DTOBloggerPost, exs As List(Of Exception)) As Boolean
        If Not oBloggerPost.IsLoaded And Not oBloggerPost.IsNew Then
            Dim pBloggerPost = Api.FetchSync(Of DTOBloggerPost)(exs, "BloggerPost", oBloggerPost.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBloggerPost)(pBloggerPost, oBloggerPost, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oBloggerPost As DTOBloggerPost, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOBloggerPost)(oBloggerPost, exs, "BloggerPost")
        oBloggerPost.IsNew = False
    End Function


    Shared Async Function Delete(oBloggerPost As DTOBloggerPost, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBloggerPost)(oBloggerPost, exs, "BloggerPost")
    End Function
End Class

Public Class BloggerPosts

    Shared Async Function FromProductOrParent(oProduct As DTOProduct, exs As List(Of Exception)) As Task(Of List(Of DTOBloggerPost))
        Return Await Api.Fetch(Of List(Of DTOBloggerPost))(exs, "BloggerPosts/FromProductOrParent", oProduct.Guid.ToString())
    End Function
    Shared Function FromProductOrParentSync(oProduct As DTOProduct, exs As List(Of Exception)) As List(Of DTOBloggerPost)
        Return Api.FetchSync(Of List(Of DTOBloggerPost))(exs, "BloggerPosts/FromProductOrParent", oProduct.Guid.ToString())
    End Function

    Shared Async Function HighlightedPosts(exs As List(Of Exception), oEmp As DTOEmp, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOBloggerPost))
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Return Await Api.Fetch(Of List(Of DTOBloggerPost))(exs, "BloggerPosts/HighlightedPosts", oEmp.Id, DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Function PostOfTheDaySync(oEmp As DTOEmp, oLang As DTOLang, exs As List(Of Exception)) As DTOBloggerPost
        Return Api.FetchSync(Of DTOBloggerPost)(exs, "BloggerPosts/PostOfTheDay", oEmp.Id, oLang.Tag)
    End Function


End Class

