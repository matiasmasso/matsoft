Public Class BlogPostLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOBlogPost
        Dim retval As DTOBlogPost = Nothing
        Dim oBlogPost As New DTOBlogPost(oGuid)
        If Load(oBlogPost) Then
            retval = oBlogPost
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oBlogPost As DTOBlogPost) As Boolean
        If Not oBlogPost.IsLoaded And Not oBlogPost.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT BlogPost.Fch, BlogPost.Visible ")
            sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
            sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
            sb.AppendLine(", LangText.Esp AS TextEsp, LangText.Cat AS TextCat, LangText.Eng AS TextEng, LangText.Por AS TextPor  ")
            sb.AppendLine(", LangUrl.Esp AS UrlEsp, LangUrl.Cat AS UrlCat, LangUrl.Eng AS UrlEng, LangUrl.Por AS UrlPor  ")
            sb.AppendLine(", BlogPost.FchCreated, BlogPost.UsrCreated, UsrCreated.adr as UsrCreatedEmailAddress, UsrCreated.Nickname as UsrCreatedNickName ")
            sb.AppendLine(", BlogPost.FchLastEdited, BlogPost.UsrLastEdited AS UsrLastEdited, UsrLastEdited.adr AS UsrLastEditedEmailAddress, UsrLastEdited.nickname as UsrLastEditedNickname")
            sb.AppendLine("FROM BlogPost ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON BlogPost.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.BlogTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON BlogPost.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.BlogExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangText ON BlogPost.Guid = LangText.Guid AND LangText.Src = " & DTOLangText.Srcs.BlogText & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangUrl ON BlogPost.Guid = langUrl.Guid AND langUrl.Src = " & DTOLangText.Srcs.BlogUrl & " ")
            'sb.AppendLine("LEFT OUTER JOIN VwContentUrl AS LangUrl ON BlogPost.Guid = langUrl.Target ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrCreated ON BlogPost.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrLastEdited ON BlogPost.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("WHERE BlogPost.Guid='" & oBlogPost.Guid.ToString & "' ")


            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oBlogPost
                    .Fch = oDrd("Fch")
                    .Visible = oDrd("Visible")
                    SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                    SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                    SQLHelper.LoadLangTextFromDataReader(.Text, oDrd, "TextEsp", "TextCat", "TextEng", "TextPor")
                    SQLHelper.LoadLangTextFromDataReader(.UrlSegment, oDrd, "UrlEsp", "UrlCat", "UrlEng", "UrlPor")
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oBlogPost.IsLoaded
        Return retval
    End Function

    Shared Function FromFriendlyUrl(UrlFriendlySegment) As DTOBlogPost
        Dim retval As DTOBlogPost = Nothing
        Dim sb As New System.Text.StringBuilder
        'sb.AppendLine("SELECT ContentUrl.Target ")
        'sb.AppendLine("FROM ContentUrl ")
        'sb.AppendLine("WHERE UrlSegment = '" & UrlFriendlySegment & "' ")
        sb.AppendLine("SELECT TOP 1 LangText.Guid ")
        sb.AppendLine("FROM LangText ")
        sb.AppendLine("WHERE CAST(Text AS VARCHAR(MAX)) = '" & UrlFriendlySegment & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOBlogPost(oDrd("Guid"))
        End If

        oDrd.Close()
        If retval IsNot Nothing Then Load(retval)
        Return retval
    End Function

    Shared Function Thumbnail(oGuid As Guid) As Byte()
        Dim retval As Byte() = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BlogPost.Thumbnail  ")
        sb.AppendLine("FROM BlogPost ")
        sb.AppendLine("WHERE BlogPost.Guid='" & oGuid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Thumbnail")
        End If

        oDrd.Close()
        Return retval
    End Function



    Shared Function Update(oBlogPost As DTOBlogPost, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBlogPost, oTrans)
            LangTextLoader.Update(oBlogPost.Title, oTrans)
            LangTextLoader.Update(oBlogPost.Excerpt, oTrans)
            LangTextLoader.Update(oBlogPost.Text, oTrans)
            LangTextLoader.Update(oBlogPost.UrlSegment, oTrans)
            oTrans.Commit()
            oBlogPost.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Update(oBlogPost As DTOBlogPost, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM BlogPost ")
        sb.AppendLine("WHERE Guid='" & oBlogPost.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBlogPost.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBlogPost
            oRow("Fch") = .Fch
            oRow("Visible") = .Visible
            If .Thumbnail IsNot Nothing Then
                oRow("Thumbnail") = .Thumbnail
            End If
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oBlogPost As DTOBlogPost, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oBlogPost, oTrans)
            oTrans.Commit()
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function


    Shared Sub Delete(oBlogPost As DTOBlogPost, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE BlogPost WHERE Guid='" & oBlogPost.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


#End Region

End Class

Public Class BlogPostsLoader


    Shared Function Compact(Optional oLang As DTOLang = Nothing, Optional onlyVisible As Boolean = True) As List(Of DTOContent.Compact)
        Dim retval As New List(Of DTOContent.Compact)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT BlogPost.Guid, BlogPost.Fch ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangUrl.Esp AS UrlEsp, LangUrl.Cat AS UrlCat, LangUrl.Eng AS UrlEng, LangUrl.Por AS UrlPor  ")
        sb.AppendLine("FROM BlogPost ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON BlogPost.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.BlogTitle & " ")
        'sb.AppendLine("LEFT OUTER JOIN VwContentUrl AS LangUrl ON BlogPost.Guid = VwContentUrl.Target ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangUrl ON BlogPost.Guid = langUrl.Guid AND langUrl.Src = " & DTOLangText.Srcs.BlogUrl & " ")
        If oLang IsNot Nothing Then
            Select Case oLang.Tag
                Case "ESP", "POR"
                    sb.AppendLine("WHERE LangTitle." & oLang.Tag & " >'' ")
                Case "CAT", "ENG"
                    sb.AppendLine("WHERE (LangTitle.Esp >'' OR LangTitle." & oLang.Tag & " >'') ")
            End Select
        End If
        If onlyVisible Then
            sb.AppendLine("AND BlogPost.Visible = 1 ")
        End If
        sb.AppendLine("ORDER BY BlogPost.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContent.Compact(oDrd("Guid"))
            With item
                .Src = DTOContent.Srcs.Blog
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.UrlSegment, oDrd, "UrlEsp", "UrlCat", "UrlEng", "UrlPor")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(Optional oLang As DTOLang = Nothing, Optional take As Integer = 0, Optional onlyVisible As Boolean = False) As List(Of DTOBlogPost)
        Dim retval As New List(Of DTOBlogPost)
        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT ")
        If take > 0 Then
            sb.Append("TOP " & take & " ")
        End If
        sb.AppendLine("  BlogPost.Guid, BlogPost.Fch, BlogPost.Visible ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor  ")
        sb.AppendLine(", LangUrl.Esp AS UrlEsp, LangUrl.Cat AS UrlCat, LangUrl.Eng AS UrlEng, LangUrl.Por AS UrlPor  ")
        sb.AppendLine(", BlogPost.FchCreated, BlogPost.UsrCreated, UsrCreated.adr as UsrCreatedEmailAddress, UsrCreated.Nickname as UsrCreatedNickName ")
        sb.AppendLine(", BlogPost.FchLastEdited, BlogPost.UsrLastEdited AS UsrLastEdited, UsrLastEdited.adr AS UsrLastEditedEmailAddress, UsrLastEdited.nickname as UsrLastEditedNickname")
        sb.AppendLine("FROM BlogPost ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON BlogPost.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.BlogTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON BlogPost.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.BlogExcerpt & " ")
        'sb.AppendLine("LEFT OUTER JOIN VwContentUrl AS LangUrl ON BlogPost.Guid = LangUrl.Target ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangUrl ON BlogPost.Guid = langUrl.Guid AND langUrl.Src = " & DTOLangText.Srcs.BlogUrl & " ")
        sb.AppendLine("LEFT OUTER JOIN Email AS UsrCreated ON BlogPost.UsrCreated = UsrCreated.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS UsrLastEdited ON BlogPost.UsrLastEdited = UsrLastEdited.Guid ")
        If oLang IsNot Nothing Then
            Select Case oLang.Tag
                Case "ESP", "POR"
                    sb.AppendLine("WHERE LangTitle." & oLang.Tag & " >'' ")
                Case "CAT", "ENG"
                    sb.AppendLine("WHERE (LangTitle.Esp >'' OR LangTitle." & oLang.Tag & " >'') ")
            End Select
        End If
        If onlyVisible Then
            sb.AppendLine("AND BlogPost.Visible = 1 ")
        End If
        sb.AppendLine("ORDER BY BlogPost.Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOBlogPost(oDrd("Guid"))
            With item
                .Fch = oDrd("Fch")
                .Visible = oDrd("Visible")
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                SQLHelper.LoadLangTextFromDataReader(.UrlSegment, oDrd, "UrlEsp", "UrlCat", "UrlEng", "UrlPor")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
