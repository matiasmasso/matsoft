Public Class ContentLoader

    Shared Function Find(oGuid As Guid, Optional includeText As Boolean = False) As DTOContent
        Dim retval As DTOContent = Nothing
        Dim oContent As New DTOContent(oGuid)
        If Load(oContent) Then
            retval = oContent
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oContent As DTOContent, Optional includeText As Boolean = False) As Boolean
        If Not oContent.IsLoaded And Not oContent.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Content.Visible, Content.FchCreated ")
            sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
            sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
            sb.AppendLine(", LangText.Esp AS TextEsp, LangText.Cat AS TextCat, LangText.Eng AS TextEng, LangText.Por AS TextPor  ")
            sb.AppendLine(", LangUrl.Esp AS UrlEsp, LangUrl.Cat AS UrlCat, LangUrl.Eng AS UrlEng, LangUrl.Por AS UrlPor  ")
            sb.AppendLine("FROM Content ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Content.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Content.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangText ON Content.Guid = LangText.Guid AND LangText.Src = " & DTOLangText.Srcs.ContentText & " ")
            sb.AppendLine("LEFT OUTER JOIN VwContentUrl AS LangUrl ON Content.Guid = LangUrl.Target ")
            sb.AppendLine("WHERE Content.Guid='" & oContent.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oContent
                    .Src = DTOContent.Srcs.Content
                    .Visible = oDrd("Visible")
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                    SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                    SQLHelper.LoadLangTextFromDataReader(.Text, oDrd, "TextEsp", "TextCat", "TextEng", "TextPor")
                    SQLHelper.LoadLangTextFromDataReader(.UrlSegment, oDrd, "UrlEsp", "UrlCat", "UrlEng", "UrlPor")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()


        End If

        Dim retval As Boolean = oContent.IsLoaded
        Return retval
    End Function


    Shared Function SearchByUrl(sUrlFriendlySegment As String) As DTOContent
        Dim retval As DTOContent = Nothing
        If sUrlFriendlySegment > "" Then
            Dim oGuid As Guid
            Dim BlFound As Boolean
            Dim SQL As String = "SELECT Target FROM ContentUrl WHERE UrlSegment = '" & sUrlFriendlySegment & "'"
            Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                oGuid = oDrd("Target")
                BlFound = True
            End If
            oDrd.Close()

            If BlFound Then
                retval = Find(oGuid)
            End If
        End If
        Return retval
    End Function


    Shared Function Update(oContent As DTOContent, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oContent, oTrans)
            oTrans.Commit()
            oContent.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub Update(oContent As DTOContent, oTrans As SqlTransaction)
        UpdateHeader(oContent, oTrans)
        With oContent
            LangTextLoader.Update(.Title, oTrans)
            LangTextLoader.Update(.Excerpt, oTrans)
            LangTextLoader.Update(.Text, oTrans)
            UpdateUrl(oContent, oTrans)
        End With
    End Sub



    Shared Sub UpdateUrl(oContent As DTOContent, ByRef oTrans As SqlTransaction)
        DeleteUrl(oContent, oTrans)

        Dim oLangText = oContent.UrlSegment
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ContentUrl ")
        sb.AppendLine("WHERE Target='" & oLangText.Guid.ToString & "' ")

        Dim SQL = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oLangText.Esp.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Target") = oLangText.Guid
            oRow("Lang") = "ESP"
            oRow("UrlSegment") = oLangText.Esp
        End If

        If oLangText.Cat.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Target") = oLangText.Guid
            oRow("Lang") = "CAT"
            oRow("UrlSegment") = oLangText.Cat
        End If

        If oLangText.Eng.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Target") = oLangText.Guid
            oRow("Lang") = "ENG"
            oRow("UrlSegment") = oLangText.Eng
        End If

        If oLangText.Por.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Target") = oLangText.Guid
            oRow("Lang") = "POR"
            oRow("UrlSegment") = oLangText.Por
        End If

        oDA.Update(oDs)


    End Sub

    Shared Sub UpdateHeader(oContent As DTOContent, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM Content ")
        sb.AppendLine("WHERE Guid='" & oContent.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oContent.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oContent
            oRow("Visible") = .Visible
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oContent As DTOContent, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            DeleteUrl(oContent, oTrans)
            Delete(oContent, oTrans)
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

    Shared Sub DeleteUrl(oContent As DTOContent, ByRef oTrans As SqlTransaction)
        Dim SQL = "DELETE ContentUrl WHERE Target = '" & oContent.Guid.ToString() & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Delete(oContent As DTOContent, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Content WHERE Guid='" & oContent.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

End Class


Public Class ContentsLoader

    Shared Function All() As List(Of DTOContent)
        Dim retval As New List(Of DTOContent)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Content.Guid, Content.Visible, Content.FchCreated ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangUrl.Esp AS UrlEsp, LangUrl.Cat AS UrlCat, LangUrl.Eng AS UrlEng, LangUrl.Por AS UrlPor  ")
        sb.AppendLine("FROM Content ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Content.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangUrl ON Content.Guid = LangUrl.Guid AND LangUrl.Src = " & DTOLangText.Srcs.ContentUrl & " ")
        sb.AppendLine("ORDER BY Content.FchCreated DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContent(oDrd("Guid"))
            With item
                .Src = DTOContent.Srcs.Content
                .Visible = oDrd("Visible")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.UrlSegment, oDrd, "UrlEsp", "UrlCat", "UrlEng", "UrlPor")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class