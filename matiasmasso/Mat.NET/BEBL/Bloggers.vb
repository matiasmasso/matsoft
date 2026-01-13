Public Class Blogger
    Shared Function Find(oGuid As Guid) As DTOBlogger
        Dim retval As DTOBlogger = BloggerLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oBlogger As DTOBlogger) As Boolean
        Dim retval As Boolean = BloggerLoader.Load(oBlogger)
        Return retval
    End Function

    Shared Function Update(oBlogger As DTOBlogger, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BloggerLoader.Update(oBlogger, exs)
        Return retval
    End Function

    Shared Function Delete(oBlogger As DTOBlogger, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BloggerLoader.Delete(oBlogger, exs)
        Return retval
    End Function
End Class

Public Class Bloggers
    Shared Function All() As List(Of DTOBlogger)
        Dim retval As List(Of DTOBlogger) = BloggersLoader.All()
        Return retval
    End Function

End Class
