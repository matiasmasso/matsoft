Public Class BlogPost

    Shared Function Find(oGuid As Guid) As DTOBlogPost
        Return BlogPostLoader.Find(oGuid)
    End Function

    Shared Function Model(oGuid As Guid, oLang As DTOLang, Optional includeComment As DTOPostComment = Nothing) As DTOBlogPostModel
        Dim retval As DTOBlogPostModel = Nothing
        Dim oBlogPost = BlogPostLoader.Find(oGuid)
        If oBlogPost IsNot Nothing Then
            retval = Model(oBlogPost, oLang, includeComment)
        End If
        Return retval
    End Function

    Shared Function ModelFromFriendlyUrl(friendlyUrl As String, oLang As DTOLang) As DTOBlogPostModel
        Dim retval As DTOBlogPostModel = Nothing
        Dim oBlogPost = BlogPostLoader.FromFriendlyUrl(friendlyUrl)
        If oBlogPost IsNot Nothing Then
            retval = Model(oBlogPost, oLang)
            retval.Comments = BEBL.PostComments.TreeModel(oBlogPost, oLang, 15)
        End If
        Return retval
    End Function

    Shared Function Model(oBlogPost As DTOBlogPost, oLang As DTOLang, Optional includeComment As DTOPostComment = Nothing) As DTOBlogPostModel
        Dim retval = DTOBlogPostModel.Factory(oBlogPost, oLang)
        retval.Posts = BlogPosts.Models(oLang)
        retval.Posts.RemoveAll(Function(x) x.Guid.Equals(oBlogPost.Guid)) 'treu el seleccionat de la llista
        retval.Comments = PostCommentsLoader.TreeModel(oBlogPost, oLang, 0, 15, includeComment)
        Return retval
    End Function

    Shared Function LastModel(oLang As DTOLang) As DTOBlogPostModel
        Dim oFilteredPosts = BlogPosts.Models(oLang)
        Dim oPost = BEBL.BlogPost.Find(oFilteredPosts.First().Guid)
        Dim retval = DTOBlogPostModel.Factory(oPost, oLang)
        retval.Posts = oFilteredPosts
        'retval.Posts.Remove(retval) 'treu el seleccionat de la llista
        retval.Comments = PostCommentsLoader.TreeModel(oPost, oLang, from:=0, take:=15)
        Return retval
    End Function


    Shared Function Thumbnail(oGuid As Guid) As Byte()
        Return BlogPostLoader.Thumbnail(oGuid)
    End Function

    Shared Function Update(oBlogPost As DTOBlogPost, exs As List(Of Exception)) As Boolean
        Return BlogPostLoader.Update(oBlogPost, exs)
    End Function

    Shared Function Delete(oBlogPost As DTOBlogPost, exs As List(Of Exception)) As Boolean
        Return BlogPostLoader.Delete(oBlogPost, exs)
    End Function

End Class



Public Class BlogPosts
    Shared Function All(Optional oLang As DTOLang = Nothing, Optional take As Integer = 0, Optional onlyVisible As Boolean = False) As List(Of DTOBlogPost)
        Dim retval As List(Of DTOBlogPost) = BlogPostsLoader.All(oLang, take, onlyVisible)
        Return retval
    End Function

    Shared Function Compact(oLang As DTOLang) As List(Of DTOContent.Compact)
        Return BlogPostsLoader.Compact(oLang, onlyVisible:=True)
    End Function

    Shared Function Models(oLang As DTOLang) As List(Of DTOBlogPostModel)
        Dim retval As New List(Of DTOBlogPostModel)
        Dim oAllPosts = BlogPosts.All(oLang)
        For Each item In oAllPosts
            If item.Visible And item.Fch <= DTO.GlobalVariables.Today() Then
                retval.Add(DTOBlogPostModel.Factory(item, oLang))
            End If
        Next
        Return retval
    End Function

    Shared Function HeadersForSitemap(oEmp As DTOEmp) As List(Of DTOBlogPost)
        Dim retval As List(Of DTOBlogPost) = BlogPostsLoader.All()
        Return retval
    End Function


End Class
