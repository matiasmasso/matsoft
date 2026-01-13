Public Class WpPostLoader

    Shared Function FromId(iPostId As Integer) As DTOBlogpost
        Dim sOpenQuery As String = "SELECT P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid as url, P1.post_name as url2, X.Guid as FeaturedImageUrl " _
        & "FROM wp_posts AS P1 LEFT OUTER JOIN " _
        & "(SELECT p2.post_parent, P2.Guid FROM wp_posts P2 INNER JOIN wp_postmeta PM ON PM.meta_value=p2.Id AND PM.meta_key=''_thumbnail_id'' ) X " _
        & "ON X.post_parent = P1.Id " _
        & "WHERE P1.Post_type = ''post'' And P1.post_title <> ''Borrador automatico'' And P1.Id=" & iPostId.ToString & " " _
        & "GROUP BY P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid, P1.post_name , X.Guid " _
        & "ORDER BY P1.Id DESC"

        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetWpDataReader(sOpenQuery)
        Dim oBlogPosts As List(Of DTOBlogpost) = WpPostsLoader.Load(oDrd)
        oDrd.Close()

        Dim retval As DTOBlogpost = oBlogPosts.First
        Return retval
    End Function

End Class

Public Class WpPostsLoader

    Shared Function Last() As DTOBlogPost
        Dim retval As DTOBlogPost = Nothing
        Dim sb As New Text.StringBuilder

        sb.AppendLine("SELECT P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid as url, P1.post_name as url2, X.Guid as FeaturedImageUrl ")
        sb.AppendLine("FROM wp_posts AS P1 ")
        sb.AppendLine("LEFT OUTER JOIN ")
        sb.AppendLine("     (SELECT p2.post_parent, P2.Guid ")
        sb.AppendLine("      FROM wp_posts P2 ")
        sb.AppendLine("      INNER JOIN wp_postmeta PM ON PM.meta_value=p2.Id AND PM.meta_key=''_thumbnail_id'' ")
        sb.AppendLine("     ) X ")
        sb.AppendLine("ON X.post_parent = P1.Id ")
        sb.AppendLine("WHERE P1.Post_type = ''post'' ")
        sb.AppendLine("AND P1.post_title <> ''Borrador automatico'' ")
        sb.AppendLine("AND P1.post_status=''publish'' ")
        sb.AppendLine("GROUP BY P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid, P1.post_name, X.Guid ")
        sb.AppendLine("ORDER BY P1.Id DESC")
        sb.AppendLine("LIMIT 1")

        Try
            Dim sOpenQuery = sb.ToString
            Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetWpDataReader(sOpenQuery)
            retval = WpPostsLoader.Load(oDrd).First
            oDrd.Close()

        Catch ex As Exception

        End Try

        Return retval

    End Function

    Shared Function Last(iCount As Integer) As List(Of DTOBlogPost)
        Dim retval As New List(Of DTOBlogPost)
        Dim sOpenQuery As String = "SELECT P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid as url, P1.post_name as url2, X.Guid as FeaturedImageUrl " _
        & "FROM wp_posts AS P1 LEFT OUTER JOIN " _
        & "(SELECT p2.post_parent, P2.Guid FROM wp_posts P2 INNER JOIN wp_postmeta PM ON PM.meta_value=p2.Id AND PM.meta_key=''_thumbnail_id'' ) X " _
        & "ON X.post_parent = P1.Id " _
        & "WHERE P1.Post_type = ''post'' And P1.post_title <> ''Borrador automatico''  AND P1.post_status=''publish'' " _
        & "GROUP BY P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid, P1.post_name, X.Guid " _
        & "ORDER BY P1.Id DESC"

        Try
            Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetWpDataReader(sOpenQuery)
            retval = WpPostsLoader.Load(oDrd)
            oDrd.Close()

        Catch ex As Exception

        End Try

        Return retval
    End Function

    Shared Function All() As List(Of DTOBlogPost)
        Dim retval As New List(Of DTOBlogPost)
        Dim sOpenQuery As String = "SELECT P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid as url, P1.post_name as url2, X.Guid as FeaturedImageUrl " _
        & "FROM wp_posts AS P1 LEFT OUTER JOIN " _
        & "(SELECT p2.post_parent, P2.Guid FROM wp_posts P2 INNER JOIN wp_postmeta PM ON PM.meta_value=p2.Id AND PM.meta_key=''_thumbnail_id'' ) X " _
        & "ON X.post_parent = P1.Id " _
        & "WHERE P1.Post_type = ''post'' And P1.post_title <> ''Borrador automatico''  AND P1.post_status=''publish'' " _
        & "GROUP BY P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid, P1.post_name, X.Guid " _
        & "ORDER BY P1.post_date DESC"

        Try
            Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetWpDataReader(sOpenQuery)
            retval = WpPostsLoader.Load(oDrd)
            oDrd.Close()

        Catch ex As Exception

        End Try

        Return retval
    End Function

    Shared Function FromTag(sTag As String) As List(Of DTOBlogPost)
        Dim sOpenQuery As String = "SELECT P1.Id, P1.post_date, P1.post_title, P1.comment_count, P1.post_excerpt, P1.guid as url, P1.post_name as url2, X.Guid as FeaturedImageUrl " _
        & "FROM wp_posts AS P1 " _
        & "INNER JOIN wp_term_relationships WTR on P1.id = WTR.Object_Id " _
        & "INNER JOIN wp_terms WT on WTR.term_taxonomy_id = WT.term_id " _
        & "LEFT OUTER JOIN (SELECT p2.post_parent, P2.Guid FROM wp_posts P2 INNER JOIN wp_postmeta PM ON PM.meta_value=p2.Id AND PM.meta_key=''_thumbnail_id'' GROUP BY p2.post_parent, P2.Guid) X ON X.post_parent = P1.Id " _
        & "WHERE name=''" & sTag & "'' " _
        & "ORDER BY object_id DESC"

        Dim oDrd As SqlClient.SqlDataReader = SQLHelper.GetWpDataReader(sOpenQuery)
        Dim retval As List(Of DTOBlogPost) = WpPostsLoader.Load(oDrd)
        oDrd.Close()

        Return retval
    End Function


    Shared Function Load(oDrd As SqlDataReader) As List(Of DTOBlogPost)
        Dim retval As New List(Of DTOBlogPost)

        Do While oDrd.Read
            Dim oBlogPost As New DTOBlogPost
            With oBlogPost
                .Id = oDrd("Id")
                .fch = CDate(oDrd("post_date"))
                .Title = oDrd("post_title").ToString
                .Excerpt = oDrd("post_excerpt").ToString
                .CommentCount = oDrd("comment_count")
                .VirtualPath = MmoUrl.Factory(True, "blog", .fch.Year.ToString, Format(.fch.Month, "00"), oDrd("url2").ToString())
                If Not IsDBNull(oDrd("FeaturedImageUrl")) Then
                    .FeaturedImageUrl = oDrd("FeaturedImageUrl").ToString.Replace("http://", "https://")
                End If
            End With
            retval.Add(oBlogPost)
        Loop
        oDrd.Close()

        Return retval
    End Function
End Class
