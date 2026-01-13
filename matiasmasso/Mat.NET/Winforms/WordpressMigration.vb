Class WordpressMigration
    Shared Sub ImportPosts()
        Dim SQL As String = "DELETE MySqlPosts"
        Dim exs As New List(Of Exception)
        DAL.SQLHelper.ExecuteNonQuery(SQL, exs)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Id, Post_author, Post_date, Post_Content, Post_Title, Post_Excerpt, Post_Status, Comment_Status ")
        sb.AppendLine(", post_name, post_parent, guid, post_type, post_mime_type, comment_count")
        sb.AppendLine("FROM wp_posts ")
        sb.AppendLine("ORDER BY Id ")
        SQL = sb.ToString
        Dim MySQLConnectionString As String = "SERVER=loopslan.matiasmasso.es;DATABASE=wordpress995;USER=wordpressuser1;PASSWORD=u%pZjXwJ]oZT;"
        Dim oMySQLConn As New MySql.Data.MySqlClient.MySqlConnection(MySQLConnectionString)
        Dim oCmd As New MySql.Data.MySqlClient.MySqlCommand(SQL, oMySQLConn)
        oMySQLConn.Open()
        Dim src As New List(Of Dictionary(Of String, String))
        Dim oDrd As MySql.Data.MySqlClient.MySqlDataReader = oCmd.ExecuteReader(CommandBehavior.CloseConnection)
        Do While oDrd.Read
            Dim item As New Dictionary(Of String, String)
            item("Id") = oDrd("Id")
            item("Post_author") = oDrd("Post_Author")
            item("Post_date") = oDrd("Post_date")
            item("Post_Content") = oDrd("Post_Content").ToString.Replace(vbCrLf, "<br/>")
            item("Post_Title") = oDrd("Post_Title")
            item("Post_Excerpt") = oDrd("Post_Excerpt")
            item("Post_Status") = oDrd("Post_Status")
            item("Comment_Status") = oDrd("Comment_Status")
            item("post_name") = oDrd("post_name")
            item("post_parent") = oDrd("post_parent")
            item("url") = String.Format("{0}/{1}/{2}.html", item("Post_date").Substring(6, 4), item("Post_date").Substring(3, 2), item("post_name"))
            item("post_type") = oDrd("post_type")
            item("post_mime_type") = oDrd("post_mime_type")
            item("comment_count") = oDrd("comment_count")
            src.Add(item)
        Loop
        oDrd.Close()
        oMySQLConn.Close()

        Dim oFrm As New Frm_Progress("posts", "posts")
        oFrm.Show()
        Dim BlCancel As Boolean

        SQL = "SELECT * FROM MySqlPosts"
        Dim oConn As SqlClient.SqlConnection = DAL.SQLHelper.SQLConnection()
        Dim oDA As SqlClient.SqlDataAdapter = DAL.SQLHelper.GetSQLDataAdapter(SQL, oConn)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In src
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Id") = item("Id")
            oRow("Post_author") = item("Post_author")
            oRow("Post_date") = item("Post_date")
            oRow("Post_Content") = item("Post_Content")
            oRow("Post_Title") = item("Post_Title")
            oRow("Post_Excerpt") = item("Post_Excerpt")
            oRow("Post_Status") = item("Post_Status")
            oRow("Comment_Status") = item("Comment_Status")
            oRow("post_name") = item("post_name")
            oRow("post_parent") = item("post_parent")
            oRow("url") = item("url")
            oRow("post_type") = item("post_type")
            oRow("post_mime_type") = item("post_mime_type")
            oRow("comment_count") = item("comment_count")
            oFrm.ShowProgress(0, src.Count, src.IndexOf(item), item("post_name"), BlCancel)
        Next
        oDA.Update(oDs)
        oConn.Close()

        SQL = "DELETE Noticia WHERE Cod=5"
        DAL.SQLHelper.ExecuteNonQuery(SQL, exs)

        SQL = "INSERT INTO Noticia(Guid, Fch, UrlFriendlySegment, Visible, Cod, TitleEsp, ExcerptEsp, TextEsp) " _
            & "SELECT Guid,CONVERT(DATE,POST_DATE,103), url,1,5,post_title, post_excerpt, post_Content " _
            & "FROM MySqlPosts " _
            & "where POST_TYPE='POST' AND post_status='publish' order by post_date"
        DAL.SQLHelper.ExecuteNonQuery(SQL, exs)
        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Shared Sub ImportComments()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Comment_agent, Comment_approved, Comment_author, Comment_author_email, Comment_author_Ip ")
        sb.AppendLine(", Comment_author_url, Comment_content, Comment_date, Comment_ID, Comment_karma, Comment_parent ")
        sb.AppendLine(", Comment_post_ID, Comment_type, user_Id ")
        sb.AppendLine("FROM wp_comments ")
        sb.AppendLine("ORDER BY Comment_date")

        Dim Sql As String = sb.ToString
        Dim MySQLConnectionString As String = "SERVER=loopslan.matiasmasso.es;DATABASE=wordpress995;USER=wordpressuser1;PASSWORD=u%pZjXwJ]oZT;"
        Dim oMySQLConn As New MySql.Data.MySqlClient.MySqlConnection(MySQLConnectionString)
        Dim oCmd As New MySql.Data.MySqlClient.MySqlCommand(Sql, oMySQLConn)
        oMySQLConn.Open()
        Dim src As New List(Of Dictionary(Of String, String))
        Dim oDrd As MySql.Data.MySqlClient.MySqlDataReader = oCmd.ExecuteReader(CommandBehavior.CloseConnection)
        Do While oDrd.Read
            Dim item As New Dictionary(Of String, String)
            item("Comment_agent") = oDrd("Comment_agent")
            item("Comment_approved") = oDrd("Comment_approved")
            item("Comment_author") = oDrd("Comment_author")
            item("Comment_content") = oDrd("Comment_content").ToString.Replace(vbCrLf, "<br/>")
            item("Comment_author_email") = oDrd("Comment_author_email")
            item("Comment_author_Ip") = oDrd("Comment_author_Ip")
            item("Comment_author_url") = oDrd("Comment_author_url")
            item("Comment_date") = oDrd("Comment_date")
            item("Comment_ID") = oDrd("Comment_ID")
            item("Comment_karma") = oDrd("Comment_karma")
            item("Comment_parent") = oDrd("Comment_parent")
            item("Comment_post_ID") = oDrd("Comment_post_ID")
            item("Comment_type") = oDrd("Comment_type")
            item("user_Id") = oDrd("user_Id")
            src.Add(item)
        Loop
        oDrd.Close()
        oMySQLConn.Close()

        Dim exs As New List(Of Exception)
        Sql = "DELETE FROM MySqlComments"
        DAL.SQLHelper.ExecuteNonQuery(Sql, exs)

        Sql = "SELECT * FROM MySqlComments"

        Dim oFrm As New Frm_Progress("posts", "posts")
        oFrm.Show()
        Dim BlCancel As Boolean

        Dim oConn As SqlClient.SqlConnection = DAL.SQLHelper.SQLConnection()
        Dim oDA As SqlClient.SqlDataAdapter = DAL.SQLHelper.GetSQLDataAdapter(Sql, oConn)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each item In src
            Dim oRow As DataRow = oTb.NewRow
            Try
                oTb.Rows.Add(oRow)
                oRow("Comment_agent") = item("Comment_agent")
                oRow("Comment_approved") = item("Comment_approved")
                oRow("Comment_author") = item("Comment_author")
                oRow("Comment_content") = item("Comment_content").ToString.Replace(vbCrLf, "<br/>")
                oRow("Comment_author_email") = item("Comment_author_email")
                oRow("Comment_author_Ip") = item("Comment_author_Ip")
                oRow("Comment_author_url") = item("Comment_author_url")
                oRow("Comment_date") = item("Comment_date")
                oRow("Comment_ID") = item("Comment_ID")
                oRow("Comment_karma") = item("Comment_karma")
                oRow("Comment_parent") = item("Comment_parent")
                oRow("Comment_post_ID") = item("Comment_post_ID")
                oRow("Comment_type") = item("Comment_type")
                oRow("user_Id") = item("user_Id")
                oDA.Update(oDs)
                oFrm.ShowProgress(0, src.Count, src.IndexOf(item), item("Comment_date"), BlCancel)
                If BlCancel Then Exit For
            Catch ex As Exception
                Stop
            Finally
                oConn.Close()
            End Try
        Next
    End Sub

    Shared Sub InsertComments()
        Dim SQL As String = "DELETE PostComment WHERE ParentSource=2"
        Dim exs As New List(Of Exception)
        DAL.SQLHelper.ExecuteNonQuery(SQL, exs)
        If exs.Count = 0 Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("INSERT INTO PostComment(Guid, Parent, ParentSource, [User], Fch, [Text], Answering, FchApproved) ")
            sb.AppendLine("SELECT MySqlComments.Guid, MySqlPosts.Guid, 2, Email.Guid ")
            sb.AppendLine(", MySqlComments.Comment_date, MySQLComments.comment_content, ParentComment.Guid, MySqlComments.Comment_date ")
            sb.AppendLine("FROM MySqlComments ")
            sb.AppendLine("INNER JOIN Email ON MySqlComments.Comment_author_email = Email.Adr AND EMAIL.EMP=1 ")
            sb.AppendLine("INNER JOIN MySqlPosts ON MySqlComments.Comment_post_id=MySqlPosts.Id ")
            sb.AppendLine("LEFT OUTER JOIN MySQLComments ParentComment ON MySqlComments.comment_parent=ParentComment.comment_Id ")
            sb.AppendLine("WHERE MySQLComments.Comment_approved='1' ")
            SQL = sb.ToString
            DAL.SQLHelper.ExecuteNonQuery(SQL, exs)
            If exs.Count > 0 Then UIHelper.WarnError(exs)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Shared Sub ImportPostThumbnails()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT result.post_parent, result.guid AS featuredImage ")
        sb.AppendLine("FROM wp_posts as p ")
        sb.AppendLine("INNER JOIN wp_postmeta AS pm ON p.ID=pm.post_id ")
        sb.AppendLine("INNER JOIN wp_posts as result ON pm.meta_value = result.ID ")
        sb.AppendLine("WHERE pm.meta_key = '_thumbnail_id' AND p.post_type='post' AND p.post_status='publish' ")
        Dim SQL As String = sb.ToString
        Dim MySQLConnectionString As String = "SERVER=loopslan.matiasmasso.es;DATABASE=wordpress995;USER=wordpressuser1;PASSWORD=u%pZjXwJ]oZT;"
        Dim oMySQLConn As New MySql.Data.MySqlClient.MySqlConnection(MySQLConnectionString)
        Dim oCmd As New MySql.Data.MySqlClient.MySqlCommand(SQL, oMySQLConn)
        oMySQLConn.Open()
        Dim src As New Dictionary(Of Integer, String)
        Dim oDrd As MySql.Data.MySqlClient.MySqlDataReader = oCmd.ExecuteReader(CommandBehavior.CloseConnection)
        Do While oDrd.Read
            src.Add(oDrd("post_parent"), oDrd("featuredImage"))
        Loop
        oDrd.Close()
        oMySQLConn.Close()

        Dim posts As New Dictionary(Of Integer, Guid)
        SQL = "SELECT ID, Guid FROM MySQLPosts WHERE Post_type='post' AND Post_status='publish'"
        Dim oDrd2 As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd2.Read
            posts.Add(oDrd2("ID"), oDrd2("Guid"))
        Loop
        oDrd.Close()

        For Each item As KeyValuePair(Of Integer, String) In src
            Dim sUrl As String = item.Value
            Dim postId As Integer = item.Key
            If posts.ContainsKey(postId) Then
                Dim oPostGuid As Guid = posts(postId)
                Dim oImage As Image = BLL.ImageHelper.DownloadFromWebsite(sUrl)
                If oImage IsNot Nothing Then
                    Dim oNoticia As DTONoticia = BLLNoticia.Find(oPostGuid)
                    If oNoticia IsNot Nothing Then
                        oNoticia.Image265x150 = oImage
                        Dim exs As New List(Of Exception)
                        If Not BLLNoticia.Update(oNoticia, exs) Then
                            UIHelper.WarnError(exs)
                        End If
                    End If
                End If
            End If

        Next
        MsgBox("Done!")
    End Sub
End Class
