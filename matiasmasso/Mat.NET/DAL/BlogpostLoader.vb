Imports System.Text.RegularExpressions

Public Class BlogpostLoader

    Shared Function ReBuild(exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = False
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("DELETE Blog2Post ")
            Dim SQL As String = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'afegeix els posts
            sb = New Text.StringBuilder
            sb.AppendLine("INSERT INTO Blog2Post(id,fch,title,commentscount, Excerpt, Txt, AuthorId, url) ")
            sb.AppendLine("SELECT id,post_date, post_title, comment_count, post_excerpt, post_content, post_author ")
            sb.AppendLine(", CAST(year(post_date) AS VARCHAR)+'/'+format(month(post_date), '0#')+'/'+post_name AS url ")
            sb.AppendLine("FROM OPENQUERY(WORDPRESS995,'SELECT id,post_date,post_title,comment_count, post_name, post_excerpt, post_content, post_author FROM wp_posts where post_type like ''post'' AND post_status like ''publish'' ORDER BY post_date DESC')")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'afegeix les imatges dels posts
            sb = New Text.StringBuilder
            sb.AppendLine("SELECT Id, ThumbnailUrl ")
            sb.AppendLine("FROM OPENQUERY(WORDPRESS995, ")
            sb.AppendLine("'SELECT P1.Id, X.Guid as ThumbnailUrl ")
            sb.AppendLine("FROM wp_posts AS P1 LEFT OUTER JOIN ")
            sb.AppendLine("(SELECT p2.post_parent, P2.Guid FROM wp_posts P2 INNER JOIN wp_postmeta PM ON PM.meta_value=p2.Id AND PM.meta_key=''_thumbnail_id'' ) X ")
            sb.AppendLine("ON X.post_parent = P1.Id ")
            sb.AppendLine("WHERE P1.Post_type = ''post'' And P1.post_title <> ''Borrador automatico''  AND P1.post_status=''publish'' ")
            sb.AppendLine("GROUP BY P1.Id, X.Guid ")
            sb.AppendLine("ORDER BY P1.post_date DESC') ")
            SQL = sb.ToString
            Dim oDrd = SQLHelper.GetDataReader(SQL)
            Dim oDictionary As New Dictionary(Of Integer, String)
            Do While oDrd.Read
                Dim thumbnailUrl = SQLHelper.GetStringFromDataReader(oDrd("ThumbnailUrl"))
                If Not String.IsNullOrEmpty(thumbnailUrl) Then
                    oDictionary.Add(oDrd("Id"), thumbnailUrl)
                End If
            Loop
            oDrd.Close()

            For Each item As KeyValuePair(Of Integer, String) In oDictionary
                Dim id As Integer = item.Key
                Dim thumbnailUrl As String = item.Value
                Dim thumbnail = MatHelperStd.ImageHelper.DownloadFromWebsite(thumbnailUrl)
                If thumbnail IsNot Nothing Then
                    SQL = "SELECT Guid, Thumbnail FROM Blog2Post WHERE Id =" & id & " "
                    Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                    Dim oDs As New DataSet
                    oDA.Fill(oDs)
                    Dim oTb As DataTable = oDs.Tables(0)
                    If oTb.Rows.Count > 0 Then
                        Dim oRow As DataRow = oTb.Rows(0)
                        oRow("Thumbnail") = SQLHelper.NullableImage(thumbnail)
                        oDA.Update(oDs)
                    End If
                End If
            Next

            sb = New Text.StringBuilder
            sb.AppendLine("DELETE Blog2Transl")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'afegeix les correspondencies de les traduccions
            sb = New Text.StringBuilder
            sb.AppendLine("INSERT INTO Blog2Transl(PorId, EspId) ")
            sb.AppendLine("SELECT ")
            sb.AppendLine(" SUBSTRING(description, CHARINDEX('""pt"";i:', description)+7, CHARINDEX(';', description, CHARINDEX('""pt"";i:', description)+7)-CHARINDEX('""pt"";i:', description)-7) AS PT ")
            sb.AppendLine(", SUBSTRING(description, CHARINDEX('""es"";i:', description)+7, CHARINDEX(';', description, CHARINDEX('""es"";i:', description)+7)-CHARINDEX('""es"";i:', description)-7) AS ES ")
            sb.AppendLine("FROM OPENQUERY(WORDPRESS995,' ")
            sb.AppendLine("     SELECT description ")
            sb.AppendLine("     FROM wp_term_taxonomy ")
            sb.AppendLine("     WHERE taxonomy = ''post_translations'' ")
            sb.AppendLine("') ")
            sb.AppendLine("WHERE description LIKE 'a:2:{s:2:%' ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)


            sb = New System.Text.StringBuilder
            sb.AppendLine("DELETE Langtext WHERE Src BETWEEN 19 AND 22")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'afegeix l'espanyol
            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT Blog2Post.Guid, 19, 'ESP', Blog2Post.Title ")
            sb.AppendLine("FROM Blog2Post ")
            sb.AppendLine("LEFT OUTER JOIN blog2Transl ON Blog2Post.Id = blog2Transl.PorId ")
            sb.AppendLine("WHERE blog2Transl.PorId IS NULL AND Blog2Post.Title > '' ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT Blog2Post.Guid, 20, 'ESP', Blog2Post.Excerpt ")
            sb.AppendLine("FROM Blog2Post ")
            sb.AppendLine("LEFT OUTER JOIN blog2Transl ON Blog2Post.Id = blog2Transl.PorId ")
            sb.AppendLine("WHERE blog2Transl.PorId IS NULL AND Blog2Post.Excerpt IS NOT NULL ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT Blog2Post.Guid, 21, 'ESP', Blog2Post.Txt ")
            sb.AppendLine("FROM Blog2Post ")
            sb.AppendLine("LEFT OUTER JOIN blog2Transl ON Blog2Post.Id = blog2Transl.PorId ")
            sb.AppendLine("WHERE blog2Transl.PorId IS NULL AND Blog2Post.Txt IS NOT NULL ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT Blog2Post.Guid, 22, 'ESP', Blog2Post.Url ")
            sb.AppendLine("FROM Blog2Post ")
            sb.AppendLine("LEFT OUTER JOIN blog2Transl ON Blog2Post.Id = blog2Transl.PorId ")
            sb.AppendLine("WHERE blog2Transl.PorId IS NULL AND Blog2Post.Url IS NOT NULL ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'afegeix el portugues
            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT BES.Guid, 19, 'POR', BPT.Title ")
            sb.AppendLine("from blog2Transl LANG ")
            sb.AppendLine("INNER JOIN Blog2Post BES ON LANG.EspId = BES.Id ")
            sb.AppendLine("INNER JOIN Blog2Post BPT ON LANG.PorId = BPT.Id ")
            sb.AppendLine("WHERE BPT.Title IS NOT NULL ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT BES.Guid, 20, 'POR', BPT.Excerpt ")
            sb.AppendLine("from blog2Transl LANG ")
            sb.AppendLine("INNER JOIN Blog2Post BES ON LANG.EspId = BES.Id ")
            sb.AppendLine("INNER JOIN Blog2Post BPT ON LANG.PorId = BPT.Id ")
            sb.AppendLine("WHERE BPT.Excerpt IS NOT NULL ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT BES.Guid, 21, 'POR', BPT.Txt ")
            sb.AppendLine("from blog2Transl LANG ")
            sb.AppendLine("INNER JOIN Blog2Post BES ON LANG.EspId = BES.Id ")
            sb.AppendLine("INNER JOIN Blog2Post BPT ON LANG.PorId = BPT.Id ")
            sb.AppendLine("WHERE BPT.Txt IS NOT NULL ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb = New System.Text.StringBuilder
            sb.AppendLine("INSERT INTO LangText(Guid, Src, Lang, Text) ")
            sb.AppendLine("SELECT BES.Guid, 22, 'POR', BPT.Url ")
            sb.AppendLine("from blog2Transl LANG ")
            sb.AppendLine("INNER JOIN Blog2Post BES ON LANG.EspId = BES.Id ")
            sb.AppendLine("INNER JOIN Blog2Post BPT ON LANG.PorId = BPT.Id ")
            sb.AppendLine("WHERE BPT.Url IS NOT NULL ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)


            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try


        Dim items = LangTextsLoader.All()
        Dim texts = items.Where(Function(x) x.Src = 21).ToList

        Dim pattern = "\<img.+src\=(?:\""|\')(.+?)(?:\""|\')(?:.+?)\>"
        Dim reg_exp As New Regex(pattern, RegexOptions.IgnoreCase)

        Dim results As New List(Of String)
        For Each item In texts
            AddResults(item.Esp, reg_exp, results)
            If item.Por > "" Then
                AddResults(item.Por, reg_exp, results)
            End If
        Next

        Dim oDictionary2 As New Dictionary(Of String, DTOGalleryItem)
        Dim idx As Integer
        For Each result In results
            idx += 1
            Try
                Dim oImage As SixLabors.ImageSharp.Image = ImageHelper.DownloadFromWebsite(result)
                If oImage IsNot Nothing Then
                    Dim hash = CryptoHelper.HashMD5(oImage)
                    Dim oItemGallery = GalleryItemLoader.FromHash(hash)
                    If oItemGallery Is Nothing Then
                        Dim oMime = MimeHelper.GetMimeFromExtension(result)
                        oItemGallery = DTOGalleryItem.Factory(oImage, oMime)
                        oItemGallery.Nom = result.Substring(result.LastIndexOf("/") + 1)
                        If Not GalleryItemLoader.Update(oItemGallery, exs) Then
                            Stop
                        End If
                    End If
                    oDictionary2.Add(result, oItemGallery)
                End If
            Catch ex As Exception
                Stop
            End Try
        Next
        Stop

        Dim i As Integer = 0
        For Each item In oDictionary2
            i += 1
            pattern = item.Key
            reg_exp = New Regex(pattern, RegexOptions.IgnoreCase)
            Dim j = 0
            For Each Txt In texts
                j += 1
                Try
                    Dim dirty As Boolean = False
                    If Txt.Esp.Contains(item.Key) Then
                        dirty = True
                        Txt.Esp = Txt.Esp.Replace(item.Key, item.Value.Url())
                    End If
                    If Txt.Por > "" AndAlso Txt.Por.Contains(item.Key) Then
                        dirty = True
                        Txt.Por = Txt.Por.Replace(item.Key, item.Value.Url())
                    End If
                    If dirty Then
                        LangTextLoader.Update(exs, Txt)
                        If exs.Count > 0 Then Stop
                    End If

                Catch ex As Exception
                    Stop
                End Try
            Next
        Next
        Return exs.Count = 0
    End Function



    Shared Function InsertComments(exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = False
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            Dim sb As System.Text.StringBuilder = Nothing
            Dim SQL As String = ""

            sb = New Text.StringBuilder
            sb.AppendLine("DELETE Blog2Comment")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'afegeix els comentaris
            sb = New Text.StringBuilder
            sb.AppendLine("INSERT INTO Blog2Comment(Id, PostId, Lang,AuthorNom,AuthorEmail,Fch,FchGmt,ParentId, Txt)  ")
            sb.AppendLine("SELECT comment_ID, (CASE WHEN Blog2Transl.PorId IS NULL THEN comment_post_ID ELSE Blog2Transl.EspId END) ")
            sb.AppendLine(", (CASE WHEN Blog2Transl.PorId IS NULL THEN 'ESP' ELSE 'POR' END) ")
            sb.AppendLine(", comment_author,comment_author_email,comment_date,comment_date_GMT, comment_parent, comment_Content ")
            sb.AppendLine("FROM OPENQUERY(WORDPRESS995, ")
            sb.AppendLine("'SELECT comment_ID,comment_post_ID,comment_author,comment_author_email,comment_date,comment_date_GMT, comment_parent, comment_Content ")
            sb.AppendLine("FROM wp_comments ")
            sb.AppendLine("WHERE comment_approved=1 AND comment_author_email>'''' ') ")
            sb.AppendLine("LEFT OUTER JOIN Blog2Transl ON comment_post_ID=Blog2Transl.PorId ")
            sb.AppendLine("ORDER BY comment_date DESC ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'afegeix a la base de dades els nous emails dels comentaris
            sb = New Text.StringBuilder
            sb.AppendLine("INSERT INTO Email(Guid, adr, nickname, Source, FchCreated) ")
            sb.AppendLine("SELECT NEWID(),authoremail, authornom , 2, GETDATE() ")
            sb.AppendLine("FROM Blog2Comment ")
            sb.AppendLine("LEFT OUTER JOIN Email ON AuthorEmail = Email.Adr ")
            sb.AppendLine("where email.Guid is null ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            'inserta el Guid de l'autor dels comentaris
            sb = New Text.StringBuilder
            sb.AppendLine("UPDATE Blog2Comment SET Blog2Comment.Author = Email.Guid ")
            sb.AppendLine("FROM Blog2Comment ")
            sb.AppendLine("INNER JOIN Email ON Blog2Comment.AuthorEmail = Email.Adr ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return exs.Count = 0
    End Function

    Shared Function AppendCommentsFromConsultas(exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = False
        Dim PostId = 928 'Consultas
        Dim PostGuid = DTOContent.Wellknown(DTOContent.Wellknowns.consultasBlog).Guid
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            Dim sb As System.Text.StringBuilder = Nothing
            Dim SQL As String = ""

            sb = New Text.StringBuilder
            sb.AppendLine("DELETE PostComment WHERE Guid='" & PostGuid.ToString & "' ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            sb.AppendLine("INSERT INTO PostComment(Guid, Parent, ParentSource, [User], Lang, Fch, Text, Answering, FchApproved)")
            sb.AppendLine("SELECT Request.Guid, '" & PostGuid.ToString() & "' , 3 , Request.Author, Request.Lang, Request.Fch, Request.Txt, Answering.Guid, Request.Fch ")
            sb.AppendLine("FROM Blog2Comment Request ")
            sb.AppendLine("LEFT OUTER JOIN Blog2Comment Answering ON Answering.Id = Request.ParentId ")
            sb.AppendLine("WHERE Request.PostId=" & PostId & " ORDER BY Request.ParentId, Request.Fch DESC ")
            SQL = sb.ToString
            SQLHelper.ExecuteNonQuery(SQL, oTrans)

            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return exs.Count = 0
    End Function


    Shared Function ReplaceLangText(exs As List(Of Exception), searchkey As String, replaceText As String) As Boolean
        Dim retval As Boolean = False

        Dim SQL As String = "SELECT  * from langtext where CAST(TExt AS Varchar(MAX)) LIKE '%" & searchkey & "%'"
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oGuids As New List(Of Guid)
        Do While oDrd.Read
            oGuids.Add(oDrd("PKey"))
        Loop
        oDrd.Close()

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Try
            For Each oGuid In oGuids
                SQL = "SELECT PKey, Text FROM LangText WHERE PKey ='" & oGuid.ToString & "'"
                Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
                Dim oDs As New DataSet
                oDA.Fill(oDs)
                Dim oTb As DataTable = oDs.Tables(0)
                Dim oRow = oTb.Rows(0)
                Dim sText As String = oRow("Text")
                sText = sText.Replace(searchkey, replaceText)
                oRow("Text") = sText
                oDA.Update(oDs)
            Next

            Dim sb As System.Text.StringBuilder = Nothing
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return exs.Count = 0
    End Function




    Private Shared Sub AddResults(text As String, reg_exp As Regex, results As List(Of String))
        Dim matches As MatchCollection = reg_exp.Matches(text)
        For Each a_match As Match In matches
            If a_match.Value.Contains("/blog/wp-content/uploads/") Then
                Dim attribute = a_match.Value.Split(" ").FirstOrDefault(Function(x) x.Contains("/blog/wp-content/uploads/"))
                Dim start = attribute.IndexOf("http")
                Dim result = attribute.Substring(start).Replace("""", "")
                If Not results.Contains(result) Then
                    results.Add(result)
                End If
            End If
        Next a_match

    End Sub
    Shared Function Find(id As Integer) As DTOBlogPost
        Dim retval As DTOBlogPost = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * FROM OPENQUERY ")
        sb.AppendLine("(WORDPRESS995,'SELECT Id, post_date, post_title, comment_count, guid as url FROM wp_posts where post_type like ''post'' AND post_status like ''publish''') ")
        sb.AppendLine("WHERE ID=" & id & " ORDER BY post_date DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOBlogPost
            With retval
                .Id = id
                .fch = CDate(oDrd("post_date"))
                .Title = oDrd("post_title").ToString
                .CommentCount = CInt(oDrd("Comment_Count"))
                .VirtualPath = oDrd("url").ToString
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function emailsDelPostNoSuscritsAlBlog(iPostId As Integer) As List(Of String)
        Dim retval As New List(Of String)
        Dim SQL As String = "SELECT comment_author_email FROM OPENQUERY(WORDPRESS995,'SELECT comment_post_ID,comment_author,comment_author_email FROM wp_comments WHERE comment_approved=1 and comment_author_email>''''') " _
                                    & "WHERE comment_post_ID=@Id AND comment_author_email collate Modern_Spanish_CI_AS not in (SELECT EMAIL FROM WPSUBSCRIPTORS) GROUP BY comment_author_email ORDER BY comment_author_email"
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@Id", iPostId)
        Do While oDrd.Read
            retval.Add(oDrd("comment_author_email"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function emailsDelPostSuscritsAlBlog(iPostId As Integer) As List(Of String)
        Dim retval As New List(Of String)
        Dim SQL As String = "SELECT comment_author_email FROM OPENQUERY(WORDPRESS995,'SELECT comment_post_ID,comment_author,comment_author_email FROM wp_comments WHERE comment_approved=1 and comment_author_email>''''') " _
                                    & "WHERE comment_post_ID=@Id AND comment_author_email collate Modern_Spanish_CI_AS  in (SELECT EMAIL FROM WpSUBSCRIPTORS) " _
                                    & "GROUP BY comment_author_email ORDER BY comment_author_email"
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@Id", iPostId)
        Do While oDrd.Read
            retval.Add(oDrd("comment_author_email"))
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class BlogpostsLoader
    Shared Function All() As List(Of DTOBlogPost)
        Dim retval As New List(Of DTOBlogPost)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT id,post_date,post_title,comment_count,url ")
        sb.AppendLine("FROM OPENQUERY(WORDPRESS995,'SELECT id,post_date,post_title,comment_count, guid as url FROM wp_posts where post_type like ''post'' AND post_status like ''publish'' ORDER BY post_date DESC')")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBlogPost
            With item
                .Id = oDrd("id")
                .fch = SQLHelper.GetFchFromDataReader(oDrd("post_date"))
                .Title = SQLHelper.GetStringFromDataReader(oDrd("post_title"))
                .CommentCount = SQLHelper.GetIntegerFromDataReader(oDrd("Comment_Count"))
                .VirtualPath = SQLHelper.GetStringFromDataReader(oDrd("url"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Polylangs() As List(Of DTOWpPolylang)
        Dim retval As New List(Of DTOWpPolylang)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT description ")
        sb.AppendLine("FROM OPENQUERY(WORDPRESS995,'")
        sb.AppendLine("     SELECT description ")
        sb.AppendLine("     FROM wp_term_taxonomy ")
        sb.AppendLine("     WHERE taxonomy = ''post_translations'' ")
        sb.AppendLine("')")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim src As String = oDrd("description")
            'a:2:{s:2:"pt";i:1621;s:2:"es";i:928;} post 1621 es la traducció alportugues del post 928
            Dim patternArray As String = TextHelper.RegexSelectBetween("{", "}")
            Dim patternLang As String = TextHelper.RegexSelectBetween(Chr(34), Chr(34))
            Dim regexMatch As String = Text.RegularExpressions.Regex.Match(src, patternArray).Value
            Dim fields() As String = regexMatch.Split(";")
            If fields.Length >= 4 Then
                Dim item As New DTOWpPolylang
                With item
                    .lang = DTOLang.FactoryByLocale(Text.RegularExpressions.Regex.Match(fields(0), patternLang).Value)
                    .postId = fields(1).Replace("i:", "")
                    .sourceLang = DTOLang.FactoryByLocale(Text.RegularExpressions.Regex.Match(fields(2), patternLang).Value)
                    .sourcePostId = fields(3).Replace("i:", "")
                End With
                retval.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
        'SELECT description FROM OPENQUERY(WORDPRESS995,'SELECT description FROM wp_term_taxonomy WHERE taxonomy = ''post_translations''')
    End Function

End Class
