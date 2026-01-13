Public Class DTOBlogPost
    Property Id As Integer
    Property fch As Date
    Property Title As String
    Property Excerpt As String
    Property Content As String
    Property CommentCount As Integer
    Property VirtualPath As String
    Property FeaturedImageUrl As String

    Shared Function blogUrl() As String
        Return "https://www.matiasmasso.es/blog"
    End Function



End Class
