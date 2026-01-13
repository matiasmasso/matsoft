Public Class NewsletterLoader

    Shared Function Find(oGuid As Guid) As DTONewsletter
        Dim retval As DTONewsletter = Nothing
        Dim oNewsletter As New DTONewsletter(oGuid)
        If Load(oNewsletter) Then
            retval = oNewsletter
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oNewsletter As DTONewsletter, Optional forceReload As Boolean = False) As Boolean
        If forceReload Or (Not oNewsletter.IsLoaded And Not oNewsletter.IsNew) Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Newsletter.Id, Newsletter.Fch, Newsletter.Title  ")
            sb.AppendLine(", NewsletterSource.SourceGuid, NewsletterSource.SourceCod ")
            sb.AppendLine(", LangTitle.Esp AS SourceTitleEsp, LangTitle.Cat AS SourceTitleCat, LangTitle.Eng AS SourceTitleEng, LangTitle.Por AS SourceTitlePor ")
            sb.AppendLine(", LangExcerpt.Esp AS SourceExcerptEsp, LangExcerpt.Cat AS SourceExcerptCat, LangExcerpt.Eng AS SourceExcerptEng, LangExcerpt.Por AS SourceExcerptPor ")
            sb.AppendLine(", LangUrl.Esp AS SourceUrlEsp, LangUrl.Cat AS SourceUrlCat, LangUrl.Eng AS SourceUrlEng, LangUrl.Por AS SourceUrlPor ")
            sb.AppendLine(", NewsletterSource.Ord  ")
            sb.AppendLine("From Newsletter  ")
            sb.AppendLine("LEFT OUTER JOIN NewsletterSource  ON Newsletter.Guid = NewsletterSource.Newsletter ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON NewsletterSource.SourceGuid = LangTitle.Guid AND (LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " OR LangTitle.Src = " & DTOLangText.Srcs.BlogTitle & " OR LangTitle.Src = " & DTOLangText.Srcs.IncentiuTitle & ") ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON NewsletterSource.SourceGuid = LangExcerpt.Guid AND (LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " OR LangExcerpt.Src = " & DTOLangText.Srcs.BlogExcerpt & ") ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangUrl ON NewsletterSource.SourceGuid = langUrl.Guid AND (langUrl.Src = " & DTOLangText.Srcs.ContentUrl & " OR LangExcerpt.Src = " & DTOLangText.Srcs.BlogUrl & ")  ")
            'sb.AppendLine("LEFT OUTER JOIN VwContentUrl AS LangUrl ON NewsletterSource.SourceGuid = LangUrl.Target ")
            sb.AppendLine("WHERE Newsletter.Guid='" & oNewsletter.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY NewsletterSource.Ord")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                With oNewsletter
                    If Not .IsLoaded Then
                        .Id = oDrd("Id")
                        .Fch = oDrd("Fch")
                        .Title = SQLHelper.GetStringFromDataReader(oDrd("Title"))
                        .IsLoaded = True
                        .Sources = New List(Of DTONewsletterSource)
                    End If
                End With
                If Not IsDBNull(oDrd("SourceCod")) Then
                    Dim item As New DTONewsletterSource
                    Dim oCod As DTONewsletterSource.Cods = oDrd("SourceCod")
                    With item
                        .Cod = oCod
                        .Title = SQLHelper.GetLangTextFromDataReader(oDrd, "SourceTitleEsp", "SourceTitleCat", "SourceTitleEng", "SourceTitlePor")
                        .Excerpt = SQLHelper.GetLangTextFromDataReader(oDrd, "SourceExcerptEsp", "SourceExcerptCat", "SourceExcerptEng", "SourceExcerptPor")
                        .Url = SQLHelper.GetStringFromDataReader(oDrd("SourceUrlEsp"))
                        Select Case oCod
                            Case DTONewsletterSource.Cods.News
                                .Tag = New DTONoticia(oDrd("SourceGuid"))
                                CType(.Tag, DTONoticia).urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("SourceUrlEsp"))
                                '.Url = SQLHelper.GetStringFromDataReader(oDrd("urlFriendlySegment"))
                            Case DTONewsletterSource.Cods.Events
                                .Tag = New DTOEvento(oDrd("SourceGuid"))
                            Case DTONewsletterSource.Cods.Blog
                                .Tag = New DTOBlogPost(oDrd("SourceGuid"))
                                CType(.Tag, DTOBlogPost).UrlSegment = SQLHelper.GetLangTextFromDataReader(oDrd, "SourceUrlEsp", "SourceUrlCat", "SourceUrlEng", "SourceUrlPor")
                            Case DTONewsletterSource.Cods.Promo
                                .Tag = New DTOIncentiu(oDrd("SourceGuid"))
                                '.ImageUrl = Defaults.GetImageUrl(DTO.Defaults.ImgTypes.Incentiu, .Tag.Guid, True)
                                '.Url = UrlHelper.Factory(True, "incentiu", .Tag.Guid.ToString())
                        End Select
                    End With
                    oNewsletter.Sources.Add(item)
                End If
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oNewsletter.IsLoaded
        Return retval
    End Function

    Shared Function Update(oNewsletter As DTONewsletter, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oNewsletter, oTrans)
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

    Shared Sub Update(oNewsletter As DTONewsletter, ByRef oTrans As SqlTransaction)
        UpdateHeader(oNewsletter, oTrans)
        If oNewsletter.Sources IsNot Nothing Then
            UpdateSources(oNewsletter, oTrans)
        End If
    End Sub


    Shared Sub UpdateHeader(oNewsletter As DTONewsletter, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Newsletter WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oNewsletter.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oNewsletter.Guid
            If oNewsletter.Id = 0 Then oNewsletter.Id = NextId(oNewsletter, oTrans)
        Else
            oRow = oTb.Rows(0)
        End If

        With oNewsletter
            oRow("Fch") = .Fch
            oRow("Id") = .Id
            oRow("Title") = .Title
        End With

        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateSources(oNewsletter As DTONewsletter, ByRef oTrans As SqlTransaction)
        If Not oNewsletter.IsNew Then DeleteSources(oNewsletter, oTrans)
        Dim SQL As String = "SELECT * FROM NewsletterSource WHERE Newsletter='" & oNewsletter.Guid.ToString & "' "
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim idx As Integer
        For Each item As DTONewsletterSource In oNewsletter.Sources
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            With item
                oRow("Newsletter") = oNewsletter.Guid
                oRow("Ord") = idx
                oRow("SourceCod") = .Cod

                Dim oTag = item.Tag.toobject(Of DTOBaseGuid)
                oRow("SourceGuid") = oTag.guid
            End With
            idx += 1
        Next
        oDA.Update(oDs)
    End Sub


    Private Shared Function NextId(oNewsletter As DTONewsletter, ByRef oTrans As SqlTransaction) As Integer
        Dim LastId As Integer = 0
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MAX(Id) AS LastId FROM Newsletter ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow = oTb.Rows(0)
        If Not IsDBNull(oRow("LastId")) Then
            LastId = CInt(oRow("LastId")) + 1
        End If
        Dim retval As Integer = LastId + 1
        Return retval
    End Function


    Shared Function Delete(oNewsletter As DTONewsletter, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oNewsletter, oTrans)
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

    Shared Sub Delete(oNewsletter As DTONewsletter, ByRef oTrans As SqlTransaction)
        DeleteSources(oNewsletter, oTrans)
        DeleteHeader(oNewsletter, oTrans)
    End Sub


    Shared Sub DeleteSources(oNewsletter As DTONewsletter, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE NewsletterSource WHERE Newsletter='" & oNewsletter.Guid.ToString & "' "
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Sub DeleteHeader(oNewsletter As DTONewsletter, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Newsletter WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oNewsletter.Guid.ToString())
    End Sub

End Class

Public Class NewslettersLoader
    Shared Function Headers() As List(Of DTONewsletter)
        Dim retval As New List(Of DTONewsletter)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Newsletter.Guid,Newsletter.Id,Newsletter.Fch,Newsletter.Title ")
        sb.AppendLine("FROM Newsletter ")
        sb.AppendLine("ORDER BY Newsletter.Id DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTONewsletter(oDrd("Guid"))
            With item
                .Id = oDrd("Id")
                .Fch = oDrd("Fch")
                .Title = SQLHelper.GetStringFromDataReader(oDrd("Title"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class


