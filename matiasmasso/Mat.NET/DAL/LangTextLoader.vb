Public Class LangTextLoader


    Shared Function SearchProducts(oRequest As DTOSearchRequest) As List(Of DTOSearchRequest.Result)
        Dim retval As New List(Of DTOSearchRequest.Result)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Title.Guid, Title.Src, Title.Text AS Title ")
        sb.AppendLine(", VwProductUrlCanonical.* ")
        sb.AppendLine(", VwProductNom.* ")
        sb.AppendLine("FROM VwProductNom ")
        sb.AppendLine("INNER JOIN LangText Title ON VwProductNom.Guid = Title.Guid AND Title.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("INNER JOIN LangText Content ON VwProductNom.Guid = Content.Guid AND Content.Lang = Title.Lang AND Content.Src = " & DTOLangText.Srcs.ProductText & " ")
        sb.AppendLine("INNER JOIN VwProductUrlCanonical ON VwProductNom.Guid = VwProductUrlCanonical.Guid ")
        sb.AppendLine("WHERE VwProductNom.Obsoleto = 0 ")
        sb.AppendLine("AND FREETEXT(Content.Text,'" & oRequest.SearchKey & "') ")

        Select Case oRequest.Lang.id
            Case DTOLang.Ids.ESP, DTOLang.Ids.POR
                sb.AppendLine("AND Title.Lang='" & oRequest.Lang.Tag & "' ")
            Case DTOLang.Ids.CAT, DTOLang.Ids.ENG
                sb.AppendLine("AND (Title.Lang='" & oRequest.Lang.Tag & "' OR Title.Lang='ESP') ")
        End Select
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct = SQLHelper.GetProductFromDataReader(oDrd)
            Dim oSrc As DTOLangText.Srcs = oDrd("Src")
            Dim item As New DTOSearchRequest.Result()
            With item
                .BaseGuid = New DTOBaseGuid(oDrd("Guid"))
                .Cod = DTOSearchRequest.Result.Cods.Product
                .Caption = oProduct.FullNom(oRequest.Lang) ' oDrd("Title")
                .CanonicalUrl = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                '.Url = DTOProductUrl.Factory(oRequest.Lang, SQLHelper.GetStringFromDataReader(oDrd("BrandUrlEsp")),
                'SQLHelper.GetStringFromDataReader(oDrd("DeptUrlEsp")),
                'SQLHelper.GetStringFromDataReader(oDrd("CategoryUrlEsp")),
                'SQLHelper.GetStringFromDataReader(oDrd("SkuUrlEsp"))).Url()
                '.ThumbnailUrl = oProduct.thumbnailUrl()
                .Fch = oDrd("FchCreated")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function SearchNoticias(oRequest As DTOSearchRequest) As List(Of DTOSearchRequest.Result)
        Dim retval As New List(Of DTOSearchRequest.Result)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Title.Guid, Title.Src, Title.Text AS Title, Url.UrlSegment AS Url ")
        sb.AppendLine(", Noticia.Fch ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("INNER JOIN LangText Title ON Noticia.Guid = Title.Guid AND Title.Src = " & DTOLangText.Srcs.BlogTitle & " ")
        sb.AppendLine("INNER JOIN ContentUrl Url ON Noticia.Guid = Url.Target AND Title.Lang = Url.Lang ")
        sb.AppendLine("INNER JOIN LangText Content ON Noticia.Guid = Content.Guid AND Content.Lang = Title.Lang AND Content.Src = " & DTOLangText.Srcs.BlogText & " ")
        sb.AppendLine("LEFT OUTER JOIN Tpa ON Noticia.Brand = Tpa.Guid ")
        sb.AppendLine("WHERE Url.Text IS NOT NULL ")
        sb.AppendLine("AND Noticia.Visible = 1 ")
        sb.AppendLine("AND Noticia.Professional = 0 ")
        sb.AppendLine("AND (TPA.Obsoleto IS NULL OR Tpa.Obsoleto = 0) ")
        sb.AppendLine("AND (TPA.EnLiquidacio IS NULL OR Tpa.EnLiquidacio = 0) ")
        sb.AppendLine("AND FREETEXT(Content.Text,'" & oRequest.SearchKey & "') ")

        Select Case oRequest.Lang.id
            Case DTOLang.Ids.ESP, DTOLang.Ids.POR
                sb.AppendLine("AND Title.Lang='" & oRequest.Lang.Tag & "' ")
            Case DTOLang.Ids.CAT, DTOLang.Ids.ENG
                sb.AppendLine("AND (Title.Lang='" & oRequest.Lang.Tag & "' OR Title.Lang='ESP') ")
        End Select
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oNoticia As New DTONoticia(oDrd("Guid"))
            SQLHelper.LoadLangTextFromDataReader(oNoticia.UrlSegment, oDrd, "Url")
            Dim oSrc As DTOLangText.Srcs = oDrd("Src")
            Dim item As New DTOSearchRequest.Result()
            With item
                .BaseGuid = New DTOBaseGuid(oDrd("Guid"))
                .Cod = DTOSearchRequest.Result.Cods.Noticia
                .Caption = oDrd("Title")
                .Url = oNoticia.Url().RelativeUrl(oRequest.Lang)
                .ThumbnailUrl = oNoticia.ThumbnailUrl(False)
                .Fch = oDrd("Fch")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function SearchBlog(oRequest As DTOSearchRequest) As List(Of DTOSearchRequest.Result)
        Dim retval As New List(Of DTOSearchRequest.Result)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Title.Guid, Title.Src, Title.Text AS Title, Url.UrlSegment AS Url ")
        sb.AppendLine(", BlogPost.Fch ")
        sb.AppendLine("FROM BlogPost ")
        sb.AppendLine("INNER JOIN LangText Title ON BlogPost.Guid = Title.Guid AND Title.Src = " & DTOLangText.Srcs.BlogTitle & " ")
        sb.AppendLine("INNER JOIN ContentUrl Url ON BlogPost.Guid = Url.Target AND Title.Lang = Url.Lang ")
        sb.AppendLine("INNER JOIN LangText Content ON BlogPost.Guid = Content.Guid AND Content.Lang = Title.Lang AND Content.Src = " & DTOLangText.Srcs.BlogText & " ")
        sb.AppendLine("WHERE Url.Text IS NOT NULL ")
        sb.AppendLine("AND BlogPost.Visible = 1 ")
        sb.AppendLine("AND FREETEXT(Content.Text,'" & oRequest.SearchKey & "') ")

        Select Case oRequest.Lang.id
            Case DTOLang.Ids.ESP, DTOLang.Ids.POR
                sb.AppendLine("AND Title.Lang='" & oRequest.Lang.Tag & "' ")
            Case DTOLang.Ids.CAT, DTOLang.Ids.ENG
                sb.AppendLine("AND (Title.Lang='" & oRequest.Lang.Tag & "' OR Title.Lang='ESP') ")
        End Select
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBlogPost As New DTOBlogPost(oDrd("Guid"))
            oBlogPost.UrlSegment.Esp = oDrd("Url")
            Dim oSrc As DTOLangText.Srcs = oDrd("Src")
            Dim item As New DTOSearchRequest.Result()
            With item
                .BaseGuid = New DTOBaseGuid(oDrd("Guid"))
                .Cod = DTOSearchRequest.Result.Cods.BlogPost
                .Caption = oDrd("Title")
                .Url = oBlogPost.Url.RelativeUrl(oRequest.Lang)
                .ThumbnailUrl = oBlogPost.ThumbnailUrl()
                .Fch = oDrd("Fch")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Find(oGuid As Guid, src As DTOLangText.Srcs) As DTOLangText
        Dim retval As DTOLangText = Nothing
        Dim oLangText As New DTOLangText(oGuid, src)
        If Load(oLangText) Then
            retval = oLangText
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oLangText As DTOLangText) As Boolean
        If Not oLangText.IsLoaded Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT * ")
            sb.AppendLine("FROM VwLangText ")
            sb.AppendLine("WHERE Guid='" & oLangText.Guid.ToString & "' ")
            sb.AppendLine("AND Src=" & oLangText.Src & " ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                With oLangText
                    .Esp = oDrd("Esp")
                    .Cat = oDrd("Cat")
                    .Eng = oDrd("Eng")
                    .Por = oDrd("Por")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        Dim retval As Boolean = oLangText.IsLoaded
        Return retval
    End Function

    Shared Function Update(exs As List(Of Exception), oLangText As DTOLangText) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oLangText, oTrans)
            oTrans.Commit()
            oLangText.IsNew = False
            retval = True
        Catch ex As Exception
            oTrans.Rollback()
            exs.Add(ex)
        Finally
            oConn.Close()
        End Try

        Return retval
    End Function

    Shared Sub UpdateOld(oLangText As DTOLangText, ByRef oTrans As SqlTransaction)
        Dim persistedLangText = Find(oLangText.Guid, oLangText.Src)
        If persistedLangText Is Nothing Then

        End If
        Delete(oLangText, oTrans)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM LangText ")
        sb.AppendLine("WHERE Guid='" & oLangText.Guid.ToString & "' ")
        sb.AppendLine("AND Src=" & oLangText.Src & " ")

        Dim SQL = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oLangText.Esp.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oLangText.Guid
            oRow("Src") = oLangText.Src
            oRow("Lang") = "ESP"
            oRow("Text") = oLangText.Esp
        End If

        If oLangText.Cat.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oLangText.Guid
            oRow("Src") = oLangText.Src
            oRow("Lang") = "CAT"
            oRow("Text") = oLangText.Cat
        End If

        If oLangText.Eng.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oLangText.Guid
            oRow("Src") = oLangText.Src
            oRow("Lang") = "ENG"
            oRow("Text") = oLangText.Eng
        End If

        If oLangText.Por.isNotEmpty Then
            Dim oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oLangText.Guid
            oRow("Src") = oLangText.Src
            oRow("Lang") = "POR"
            oRow("Text") = oLangText.Por
        End If

        oDA.Update(oDs)

    End Sub

    Shared Sub Update(oLangText As DTOLangText, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM LangText ")
        sb.AppendLine("WHERE Guid='" & oLangText.Guid.ToString & "' ")
        sb.AppendLine("AND Src=" & oLangText.Src & " ")

        Dim SQL = sb.ToString
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRowEsp As DataRow = Nothing
        Dim oRowCat As DataRow = Nothing
        Dim oRowEng As DataRow = Nothing
        Dim oRowPor As DataRow = Nothing
        For Each oRow In oTb.Rows
            If oRow("Lang") = "ESP" Then oRowEsp = oRow
            If oRow("Lang") = "CAT" Then oRowCat = oRow
            If oRow("Lang") = "ENG" Then oRowEng = oRow
            If oRow("Lang") = "POR" Then oRowPor = oRow
        Next

        Dim isDirty As Boolean = False
        If Update(oLangText, DTOLang.ESP(), oTb, oRowEsp) Then isDirty = True
        If Update(oLangText, DTOLang.CAT(), oTb, oRowCat) Then isDirty = True
        If Update(oLangText, DTOLang.ENG(), oTb, oRowEng) Then isDirty = True
        If Update(oLangText, DTOLang.POR(), oTb, oRowPor) Then isDirty = True
        If isDirty Then oDA.Update(oDs)
    End Sub

    Shared Function Update(oLangText As DTOLangText, oLang As DTOLang, ByRef oTb As DataTable, ByRef oRow As DataRow) As Boolean
        Dim retval As Boolean = False
        If String.IsNullOrEmpty(oLangText.Text(oLang)) Then
            If oRow IsNot Nothing Then
                oRow.Delete()
                retval = True
            End If
        Else
            If oRow Is Nothing Then
                oRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Guid") = oLangText.Guid
                oRow("Src") = oLangText.Src
                oRow("Lang") = oLang.Tag
            End If
            If IsDBNull(oRow("Text")) OrElse oRow("Text") <> oLangText.Text(oLang) Then
                oRow("Text") = oLangText.Text(oLang)
                oRow("FchCreated") = Now
                retval = True
            End If
        End If
        Return retval
    End Function

    Shared Sub Delete(oLangText As DTOLangText, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE LangText ")
        sb.AppendLine("WHERE Guid='" & oLangText.Guid.ToString & "' ")
        sb.AppendLine("AND Src=" & oLangText.Src & " ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub Delete(oProduct As DTOProduct, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DELETE LangText ")
        sb.AppendLine("WHERE Guid='" & oProduct.Guid.ToString & "' ")
        Dim SQL = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub
End Class

Public Class LangTextsLoader

    Shared Function All(Optional src As DTOLangText.Srcs = DTOLangText.Srcs.notset, Optional oLang As DTOLang = Nothing, Optional searchkey As String = "") As List(Of DTOLangText)
        Dim retval As New List(Of DTOLangText)
        Dim wheres As New List(Of String)
        If src <> DTOLangText.Srcs.notset Then
            wheres.Add("Src = " & src & " ")
        End If
        If oLang IsNot Nothing Then
            wheres.Add("Lang = '" & oLang.Tag & "' ")
        End If
        If Not String.IsNullOrEmpty(searchkey) Then
            wheres.Add("Text ~* '" & searchkey & "' ")
        End If

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * FROM VwLangText ")
        If wheres.Count > 0 Then
            sb.AppendLine("WHERE ")
            sb.AppendLine(String.Join(" AND ", wheres))
            sb.AppendLine(" ")
        End If

        sb.AppendLine("ORDER BY Src, Guid")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOLangText(oDrd("Guid"), oDrd("Src"), oDrd("Esp"), oDrd("Cat"), oDrd("Eng"), oDrd("Por"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function MissingTranslations() As List(Of DTOLangText)
        Dim retval As New List(Of DTOLangText)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT        LangText.Guid, Src ")
        sb.AppendLine(", MAX(CAST(CASE WHEN Lang = 'ESP' THEN Text ELSE '' END AS VARCHAR(MAX))) AS Esp ")
        sb.AppendLine(", MAX(CASE WHEN Lang='ESP' THEN FchCreated ELSE null END) AS FchEsp ")
        sb.AppendLine(", MAX(CAST(CASE WHEN Lang = 'CAT' THEN Text ELSE '' END AS VARCHAR(MAX))) AS Cat ")
        sb.AppendLine(", MAX(CASE WHEN Lang='CAT' THEN FchCreated ELSE null END) AS FchCat ")
        sb.AppendLine(", MAX(CAST(CASE WHEN Lang = 'ENG' THEN Text ELSE '' END AS VARCHAR(MAX))) AS Eng ")
        sb.AppendLine(", MAX(CASE WHEN Lang='ENG' THEN FchCreated ELSE null END) AS FchEng ")
        sb.AppendLine(", MAX(CAST(CASE WHEN Lang = 'POR' THEN Text ELSE '' END AS VARCHAR(MAX))) AS Por ")
        sb.AppendLine(", MAX(CASE WHEN Lang='POR' THEN FchCreated ELSE null END) AS FchPor ")
        sb.AppendLine("FROM            dbo.LangText ")
        sb.AppendLine("LEFT OUTER JOIN VwProductGuid ON LangText.Guid = VwProductGuid.Guid ")
        sb.AppendLine("WHERE VwProductGuid.Obsoleto is null or VwProductGuid.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY LangText.Guid, Src ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim tests As New List(Of String)
        Do While oDrd.Read
            Try
                Dim item As New DTOLangText(oDrd("Guid"), oDrd("Src"), oDrd("Esp"), oDrd("Cat"), oDrd("Eng"), oDrd("Por"))
                Dim fchEsp = SQLHelper.GetFchFromDataReader(oDrd("FchEsp"))
                Dim isCatOutdated = SQLHelper.GetFchFromDataReader(oDrd("FchCat")) < fchEsp
                Dim isEngOutdated = SQLHelper.GetFchFromDataReader(oDrd("FchEng")) < fchEsp
                Dim isPorOutdated = SQLHelper.GetFchFromDataReader(oDrd("FchPor")) < fchEsp
                item.Outdated = If(isCatOutdated, DTOLangText.Outdateds.Cat, 0) Or If(isEngOutdated, DTOLangText.Outdateds.Eng, 0) Or If(isPorOutdated, DTOLangText.Outdateds.Por, 0)
                tests.Add(String.Format("{0} {1} {2} {3}", If(isCatOutdated, 1, 0), If(isEngOutdated, 1, 0), If(isPorOutdated, 1, 0), item.Outdated))
                retval.Add(item)

            Catch ex As Exception
                oDrd.Close()
                Stop
            End Try
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

