Public Class BloggerPost

    Shared Function Find(oGuid As Guid) As DTOBloggerPost
        Dim retval As DTOBloggerPost = BloggerpostLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oBloggerPost As DTOBloggerPost, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BloggerpostLoader.Update(oBloggerPost, exs)
        Return retval
    End Function

    Shared Function Delete(oBloggerPost As DTOBloggerPost, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BloggerpostLoader.Delete(oBloggerPost, exs)
        Return retval
    End Function

End Class



Public Class BloggerPosts
    Shared Function FromProductOrParent(oProduct As DTOProduct) As List(Of DTOBloggerPost)
        Dim retval As List(Of DTOBloggerPost) = BloggerpostsLoader.FromProductOrParent(oProduct)
        Return retval
    End Function

    Shared Function HighlightedPosts(oEmp As DTOEmp, Optional DtFch As Date = Nothing) As List(Of DTOBloggerPost)
        Dim retval As List(Of DTOBloggerPost) = BloggerpostsLoader.HighlightedPosts(DtFch)
        Return retval
    End Function

    Shared Function PostOfTheDay(oEmp As DTOEmp, oLang As DTOLang) As DTOBloggerPost
        Dim retval As DTOBloggerPost = Nothing
        Dim oTunedLang As DTOLang = DTOLang.PortugueseOrEsp(oLang)
        Dim oPosts As List(Of DTOBloggerPost) = BloggerpostsLoader.HighlightedPosts(DTO.GlobalVariables.Today(), oTunedLang)
        If oPosts.Count > 0 Then
            retval = oPosts(0)
        End If
        Return retval
    End Function

End Class
