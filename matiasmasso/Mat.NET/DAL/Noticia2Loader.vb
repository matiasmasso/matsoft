Public Class NoticiaLoader

#Region "CRUD"


    Shared Function Find(oGuid As Guid) As DTONoticia
        Dim retval As DTONoticia = Nothing
        Dim oNoticia As New DTONoticia(oGuid)
        If Load(oNoticia) Then
            retval = oNoticia
        End If
        Return retval
    End Function

    Shared Function Image265x150(oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Image265x150 FROM Noticia WHERE Noticia.Guid='" & oGuid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = ImageMime.Factory(oDrd("Image265x150"))
        End If
        oDrd.Close()
        Return retval
    End Function

    '

    Shared Function Load(ByRef oNoticia As DTONoticia) As Boolean
        If Not oNoticia.IsLoaded And Not oNoticia.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Noticia.Cod, Noticia.Fch, Noticia.Visible, Noticia.Location, Noticia.Brand, Noticia.Cnap, Noticia.UrlFriendlySegment ")
            sb.AppendLine(", Noticia.DestacarFrom, Noticia.DestacarTo, Noticia.Professional ")
            sb.AppendLine(", Noticia.FchLastEdited, Noticia.Priority ")
            sb.AppendLine(", Noticia.FchFrom, Noticia.FchTo ")
            sb.AppendLine(", Keyword.Value as Tag, NoticiaCategoria.Categoria, CategoriaDeNoticia.Nom as CategoriaNom ")
            sb.AppendLine(", VwAreaNom.AreaCod, VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom, VwAreaNom.ZipGuid, VwAreaNom.ZipCod ")
            sb.AppendLine(", VwProductNom.Cod as ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
            sb.AppendLine(", WebLog.VisitCount ")
            sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
            sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
            sb.AppendLine(", LangText.Esp AS TextEsp, LangText.Cat AS TextCat, LangText.Eng AS TextEng, LangText.Por AS TextPor  ")
            sb.AppendLine(", Noticia.FchCreated, Noticia.UsrCreated, UsrCreated.adr as UsrCreatedEmailAddress, UsrCreated.Nickname as UsrCreatedNickName ")
            sb.AppendLine(", Noticia.FchLastEdited, Noticia.UsrLastEdited AS UsrLastEdited, UsrLastEdited.adr AS UsrLastEditedEmailAddress, UsrLastEdited.nickname as UsrLastEditedNickname")
            sb.AppendLine("FROM Noticia  ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangText ON Noticia.Guid = LangText.Guid AND LangText.Src = " & DTOLangText.Srcs.ContentText & " ")
            sb.AppendLine("LEFT OUTER JOIN Keyword ON Keyword.Target = Noticia.Guid ")
            sb.AppendLine("LEFT OUTER JOIN NoticiaCategoria ON NoticiaCategoria.Noticia=Noticia.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CategoriaDeNoticia ON NoticiaCategoria.Categoria=CategoriaDeNoticia.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwAreaNom ON Noticia.Location=VwAreaNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Noticia.Brand=VwProductNom.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrCreated ON Noticia.UsrCreated = UsrCreated.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Email AS UsrLastEdited ON Noticia.UsrLastEdited = UsrLastEdited.Guid ")
            sb.AppendLine("LEFT OUTER JOIN (SELECT WebLogBrowse.Doc, COUNT(DISTINCT WebLogBrowse.Guid) AS VisitCount FROM WebLogBrowse GROUP BY WebLogBrowse.Doc) WebLog ON Noticia.Guid = WebLog.Doc  ")
            sb.AppendLine("WHERE Noticia.Guid='" & oNoticia.Guid.ToString & "' ")
            Dim oCategoria As New DTOCategoriaDeNoticia
            Dim sTag As String = ""
            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oSrc As DTONoticia.Srcs = oDrd("Cod")
                Select Case oSrc
                    Case DTOContent.Srcs.Eventos
                        oNoticia = New DTOEvento(oDrd("Guid"))
                End Select
                If Not oNoticia.IsLoaded Then
                    With oNoticia
                        .fch = oDrd("Fch")
                        .visible = oDrd("Visible")

                        If Not IsDBNull(oDrd("Brand")) Then
                            .product = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("Brand"), oDrd("SkuNom"))
                        End If

                        .urlFriendlySegment = oDrd("UrlFriendlySegment").ToString

                        SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                        SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                        SQLHelper.LoadLangTextFromDataReader(.text, oDrd, "TextEsp", "TextCat", "TextEng", "TextPor")

                        .destacarFrom = SQLHelper.GetFchFromDataReader(oDrd("DestacarFrom"))
                        .destacarTo = SQLHelper.GetFchFromDataReader(oDrd("DestacarTo"))


                        .professional = oDrd("Professional").ToString
                        .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                        .priority = oDrd("Priority")

                        .src = oSrc
                        If .src = DTOContent.Srcs.Eventos Then
                            Dim oEvento As DTOEvento = oNoticia
                            With oEvento
                                .FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                                .FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
                                .Area = SQLHelper.GetAreaFromDataReader(oDrd)
                                'If Not IsDBNull(oDrd("Location")) Then
                                '.Area = AreaLoader.NewArea(DirectCast(oDrd("AreaCod"), DTOArea.Cods), DirectCast(oDrd("CountryGuid"), Guid), oDrd("CountryNomEsp").ToString, oDrd("CountryNomCat").ToString, oDrd("CountryNomEng").ToString, oDrd("CountryISO").ToString, oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZipGuid"), oDrd("ZipCod"))
                                '.Area.Cod = SQLHelper.getareacod(oDrd)
                                'End If
                            End With
                        End If


                        .visitCount = SQLHelper.GetIntegerFromDataReader(oDrd("VisitCount"))
                        .keywords = New List(Of String)
                        .categorias = New List(Of DTOCategoriaDeNoticia)
                        .IsLoaded = True
                        .IsNew = False
                    End With
                End If
                If Not IsDBNull(oDrd("Categoria")) Then
                    Dim oGuid As Guid = oDrd("Categoria")
                    If oGuid <> oCategoria.Guid Then
                        oCategoria = New DTOCategoriaDeNoticia(oGuid)
                        oCategoria.Nom = oDrd("CategoriaNom")
                        oNoticia.categorias.Add(oCategoria)
                    End If

                End If
                If Not IsDBNull(oDrd("Tag")) Then
                    oNoticia.keywords.Add(oDrd("Tag").ToString())
                End If
            Loop

            oDrd.Close()

            sb = New Text.StringBuilder
            sb.AppendLine("SELECT Channel ")
            sb.AppendLine("FROM NoticiaChannel ")
            sb.AppendLine("WHERE Noticia = '" & oNoticia.Guid.ToString & "' ")
            SQL = sb.ToString
            oDrd = SQLHelper.GetDataReader(SQL)
            oNoticia.distributionChannels = New List(Of DTODistributionChannel)
            Do While oDrd.Read
                Dim item As New DTODistributionChannel(oDrd("Channel"))
                oNoticia.distributionChannels.Add(item)
            Loop
            oDrd.Close()

        End If

        Dim retval As Boolean = oNoticia.IsLoaded
        Return retval
    End Function

    Shared Function Update(oNoticia As DTONoticia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oNoticia, oTrans)
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

    Shared Sub Update(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        UpdateHeader(oNoticia, oTrans)
        LangTextLoader.Update(oNoticia.title, oTrans)
        LangTextLoader.Update(oNoticia.excerpt, oTrans)
        LangTextLoader.Update(oNoticia.Text, oTrans)
        LangTextLoader.Update(oNoticia.UrlSegment, oTrans)
        If oNoticia.categorias IsNot Nothing Then
            If Not oNoticia.IsNew Then DeleteCategorias(oNoticia, oTrans)
            If oNoticia.categorias.Count > 0 Then UpdateCategorias(oNoticia, oTrans)
        End If
        If oNoticia.keywords IsNot Nothing Then
            If Not oNoticia.IsNew Then DeleteKeywords(oNoticia, oTrans)
            If oNoticia.keywords.Count > 0 Then UpdateKeywords(oNoticia, oTrans)
        End If

        If Not oNoticia.IsNew Then DeleteDistributionChannels(oNoticia, oTrans)
        UpdateDistributionChannels(oNoticia, oTrans)
    End Sub

    Shared Sub UpdateKeywords(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Keyword WHERE Target='" & oNoticia.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each sKeyword As String In oNoticia.keywords
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Target") = oNoticia.Guid
            oRow("Value") = sKeyword
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateDistributionChannels(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM NoticiaChannel WHERE Noticia = '" & oNoticia.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oDistributionChannel As DTODistributionChannel In oNoticia.distributionChannels
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Noticia") = oNoticia.Guid
            oRow("Channel") = oDistributionChannel.Guid
        Next
        oDA.Update(oDs)
    End Sub


    Shared Sub UpdateCategorias(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM NoticiaCategoria WHERE Noticia=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oNoticia.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oCategoria As DTOCategoriaDeNoticia In oNoticia.categorias
            Dim oRow As DataRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Noticia") = oNoticia.Guid
            oRow("Categoria") = oCategoria.Guid
        Next
        oDA.Update(oDs)
    End Sub

    Shared Sub UpdateHeader(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Noticia WHERE Guid='" & oNoticia.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oNoticia.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oNoticia
            oRow("Fch") = .fch
            oRow("Brand") = SQLHelper.NullableBaseGuid(.product)
            oRow("UrlFriendlySegment") = SQLHelper.NullableString(.urlFriendlySegment)

            'oRow("TitleEsp") = SQLHelper.NullableLangText(.Title, DTOLang.ESP)
            'oRow("TitleCat") = SQLHelper.NullableLangText(.Title, DTOLang.CAT)
            'oRow("TitleEng") = SQLHelper.NullableLangText(.Title, DTOLang.ENG)
            'oRow("TitlePor") = SQLHelper.NullableLangText(.Title, DTOLang.POR)

            'oRow("ExcerptEsp") = SQLHelper.NullableLangText(.Excerpt, DTOLang.ESP)
            'oRow("ExcerptCat") = SQLHelper.NullableLangText(.Excerpt, DTOLang.CAT)
            'oRow("ExcerptEng") = SQLHelper.NullableLangText(.Excerpt, DTOLang.ENG)
            'oRow("ExcerptPor") = SQLHelper.NullableLangText(.Excerpt, DTOLang.POR)

            'oRow("TextEsp") = SQLHelper.NullableLangText(.Text, DTOLang.ESP)
            'oRow("TextCat") = SQLHelper.NullableLangText(.Text, DTOLang.CAT)
            'oRow("TextEng") = SQLHelper.NullableLangText(.Text, DTOLang.ENG)
            'oRow("TextPor") = SQLHelper.NullableLangText(.Text, DTOLang.POR)

            oRow("DestacarFrom") = SQLHelper.NullableFch(.destacarFrom)
            oRow("DestacarTo") = SQLHelper.NullableFch(.destacarTo)
            oRow("Professional") = .professional
            oRow("Visible") = .visible
            oRow("Priority") = .priority
            SQLHelper.SetUsrLog(.UsrLog, oRow)
            oRow("Cod") = .Src

            If TypeOf oNoticia Is DTOEvento Then
                Dim oEvento As DTOEvento = oNoticia
                oRow("FchFrom") = SQLHelper.NullableFch(oEvento.FchFrom)
                oRow("FchTo") = SQLHelper.NullableFch(oEvento.FchTo)
                oRow("Location") = SQLHelper.NullableBaseGuid(oEvento.Area)
            End If

            oRow("Image265x150") = .Image265x150
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oNoticia As DTONoticia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oNoticia, oTrans)
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


    Shared Sub Delete(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        DeleteLog(oNoticia, oTrans)
        DeleteKeywords(oNoticia, oTrans)
        DeleteCategorias(oNoticia, oTrans)
        DeleteDistributionChannels(oNoticia, oTrans)
        DeleteHeader(oNoticia, oTrans)
    End Sub

    Shared Sub DeleteHeader(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        With oNoticia
            Dim SQL As String = "DELETE Noticia WHERE Guid = '" & oNoticia.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End With
    End Sub

    Shared Sub DeleteCategorias(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        With oNoticia
            Dim SQL As String = "DELETE NoticiaCategoria WHERE Noticia = '" & oNoticia.Guid.ToString & "'"
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
        End With
    End Sub

    Shared Sub DeleteKeywords(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Keyword WHERE Keyword.target = '" & oNoticia.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteDistributionChannels(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE NoticiaChannel WHERE Noticia = '" & oNoticia.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteLog(oNoticia As DTONoticia, ByRef oTrans As SqlTransaction)
        With oNoticia
            Dim SQL As String = "DELETE WebLogBrowse WHERE Doc=@Guid"
            SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oNoticia.Guid.ToString())
        End With
    End Sub
#End Region



    Shared Function SearchByUrl(sUrlFriendlySegment As String) As DTONoticia
        Dim retval As DTONoticia = Nothing
        If sUrlFriendlySegment > "" Then
            Dim oGuid As Guid
            Dim BlFound As Boolean
            If sUrlFriendlySegment.StartsWith("news/") Then sUrlFriendlySegment = sUrlFriendlySegment.Remove(0, 5)
            If sUrlFriendlySegment.StartsWith("eventos/") Then sUrlFriendlySegment = sUrlFriendlySegment.Remove(0, 8)
            Dim SQL As String = "SELECT Guid FROM Noticia WHERE UrlFriendlySegment=@UrlFriendlySegment"
            Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@UrlFriendlySegment", sUrlFriendlySegment)
            If oDrd.Read Then
                oGuid = oDrd("Guid")
                BlFound = True
            End If
            oDrd.Close()

            If BlFound Then
                retval = Find(oGuid)
            End If
        End If
        Return retval
    End Function

    Shared Sub LoadVisitCount(ByRef oNoticia As DTONoticia)
        Dim SQL As String = "SELECT COUNT(DISTINCT Guid) AS VisitCount FROM WebLogBrowse WHERE Doc=@Guid "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oNoticia.Guid.ToString())
        Do While oDrd.Read
            If Not IsDBNull(oDrd("VisitCount")) Then
                oNoticia.visitCount = oDrd("VisitCount")
            End If
        Loop
        oDrd.Close()
    End Sub

End Class

Public Class NoticiasLoader
    Shared Function All(oSrc As DTONoticia.Srcs) As List(Of DTONoticia)
        Dim retval As New List(Of DTONoticia)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid, Noticia.Fch, Noticia.FchFrom, Noticia.FchTo, Noticia.Visible, Noticia.UrlFriendlySegment, X.VisitCount, Y.CommentCount ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine(", Noticia.Professional, Noticia.Visible ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Doc,COUNT(DISTINCT Guid) AS VisitCount FROM WebLogBrowse GROUP BY Doc) X ON Noticia.Guid = X.Doc ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Parent,COUNT(DISTINCT Guid) AS CommentCount FROM PostComment GROUP BY Parent) Y ON Noticia.Guid = Y.Parent ")
        sb.AppendLine("WHERE Noticia.Cod=" & CInt(oSrc) & " ")
        Select Case oSrc
            Case DTOContent.Srcs.Eventos
                sb.AppendLine("ORDER BY Noticia.FchFrom DESC")
            Case Else
                sb.AppendLine("ORDER BY Noticia.Fch DESC")
        End Select

        Dim item As New DTONoticia()
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Select Case oSrc
                Case DTOContent.Srcs.News, DTOContent.Srcs.SabiasQue, DTOContent.Srcs.Blog
                    item = New DTONoticia(DirectCast(oDrd("Guid"), Guid))
                Case DTOContent.Srcs.Eventos
                    item = New DTOEvento(DirectCast(oDrd("Guid"), Guid))
                    DirectCast(item, DTOEvento).FchFrom = oDrd("FchFrom")
                    DirectCast(item, DTOEvento).FchTo = oDrd("FchTo")
            End Select
            With item
                .visible = oDrd("Visible")
                .professional = oDrd("Professional")
                .src = oSrc
                .fch = oDrd("Fch")
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
                .visitCount = SQLHelper.GetIntegerFromDataReader(oDrd("VisitCount"))
                .commentCount = SQLHelper.GetIntegerFromDataReader(oDrd("CommentCount"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Compact(oLang As DTOLang, HidePro As Boolean) As List(Of DTOContent.Compact)
        Dim retval As New List(Of DTOContent.Compact)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid, Noticia.Fch ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangUrl.Esp AS UrlEsp, LangUrl.Cat AS UrlCat, LangUrl.Eng AS UrlEng, LangUrl.Por AS UrlPor  ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwContentUrl AS LangUrl ON Noticia.Guid = LangUrl.Target ")
        sb.AppendLine("WHERE Noticia.Cod=" & DTOContent.Srcs.News & " ")
        If HidePro Then
            sb.AppendLine("AND Noticia.Professional=0 ")
        End If
        sb.AppendLine("AND Noticia.Visible=1 ")
        sb.AppendLine("ORDER BY Noticia.Fch DESC")

        Dim item As DTOContent.Compact = Nothing
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            item = New DTOContent.Compact(oDrd("Guid"))
            With item
                .Src = DTOContent.Srcs.News
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.UrlSegment, oDrd, "UrlEsp", "UrlCat", "UrlEng", "UrlPor")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
    Shared Function Headers(oSrc As DTONoticia.Srcs, Optional HidePro As Boolean = False, Optional OnlyVisible As Boolean = False) As List(Of DTONoticiaBase)
        Dim retval As New List(Of DTONoticiaBase)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid, Noticia.Fch, Noticia.FchFrom, Noticia.FchTo, Noticia.Visible, Noticia.UrlFriendlySegment, X.VisitCount ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", Noticia.FchCreated, Noticia.FchLastEdited ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Doc,COUNT(DISTINCT Guid) AS VisitCount FROM WebLogBrowse GROUP BY Doc) X ON Noticia.Guid = X.Doc ")
        sb.AppendLine("WHERE Noticia.Cod=" & CInt(oSrc) & " ")
        If HidePro Then
            sb.AppendLine("AND Noticia.Professional=0 ")
        End If
        If OnlyVisible Then
            sb.AppendLine("AND Noticia.Visible=1 ")
        End If
        Select Case oSrc
            Case DTOContent.Srcs.Eventos
                sb.AppendLine("ORDER BY Noticia.FchFrom DESC")
            Case Else
                sb.AppendLine("ORDER BY Noticia.Fch DESC")
        End Select

        Dim item As DTONoticiaBase = Nothing
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Select Case oSrc
                Case DTOContent.Srcs.News, DTOContent.Srcs.SabiasQue, DTOContent.Srcs.Blog, DTOContent.Srcs.Content
                    item = New DTONoticia(oDrd("Guid"))
                Case DTOContent.Srcs.Eventos
                    item = New DTOEvento(oDrd("Guid"))
                    DirectCast(item, DTOEvento).FchFrom = SQLHelper.GetFchFromDataReader(oDrd("FchFrom"))
                    DirectCast(item, DTOEvento).FchTo = SQLHelper.GetFchFromDataReader(oDrd("FchTo"))
            End Select
            With item
                .visible = oDrd("Visible")
                .src = oSrc
                .fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
                .visitCount = SQLHelper.GetIntegerFromDataReader(oDrd("VisitCount"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function HeadersForSitemap(oEmp As DTOEmp) As List(Of DTONoticia)
        Dim retVal As New List(Of DTONoticia)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid, Noticia.UrlFriendlySegment, Noticia.Visible, Noticia.Professional, Noticia.Priority ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", Noticia.FchCreated, Noticia.FchLastEdited ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("WHERE Professional = 0 AND Visible <> 0 AND Noticia.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Noticia.Cod=" & CInt(DTONoticia.Srcs.News) & " ")
        sb.AppendLine("ORDER BY Noticia.Fch DESC")
        Dim SQL = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As New Guid(oDrd("Guid").ToString())
            Dim oNoticia As New DTONoticia(oGuid)
            With oNoticia
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                If Not IsDBNull(oDrd("UrlFriendlySegment")) Then
                    .urlFriendlySegment = oDrd("UrlFriendlySegment").ToString
                End If
                .visible = oDrd("Visible")
                .professional = oDrd("Professional").ToString
                .priority = oDrd("Priority")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With

            retVal.Add(oNoticia)
        Loop
        oDrd.Close()
        Return retVal
    End Function

    Shared Function LastNoticia(oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As DTONoticia
        Dim retval As DTONoticia = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("Select TOP 1 Noticia.Guid, Noticia.Fch, Noticia.UrlFriendlySegment ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine(", Noticia.FchCreated, Noticia.FchLastEdited ")
        sb.AppendLine("From Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
        If oProduct Is Nothing Then
            If oUser IsNot Nothing Then
                Select Case oUser.Rol.id
                    Case DTORol.Ids.manufacturer
                        sb.AppendLine("INNER JOIN VwProductParent On Noticia.Brand = VwProductParent.Child ")
                        sb.AppendLine("INNER JOIN Tpa On VwProductParent.Parent = Tpa.Guid ")
                        sb.AppendLine("INNER JOIN Email_Clis On Tpa.Proveidor = Email_Clis.ContactGuid And Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                End Select
            End If
        Else
            sb.AppendLine("INNER JOIN VwProductParent PP1 ON Noticia.Brand = PP1.Child ")
            sb.AppendLine("INNER JOIN VwProductParent PP2 ON PP1.Parent = PP2.Parent And PP2.Child ='" & oProduct.Guid.ToString & "' ")
        End If

        sb.AppendLine("WHERE Noticia.Cod = 0 ")
        sb.AppendLine("AND Noticia.Visible = 1 ")
        Select Case oLang.Tag
            Case "ESP", "POR"
                sb.AppendLine("AND CAST(LangTitle." & oLang.Tag & " AS VARCHAR) > '' ")
            Case "CAT", "ENG"
                sb.AppendLine("AND (CAST(LangTitle.Esp AS VARCHAR) >'' OR CAST(LangTitle." & oLang.Tag & " AS VARCHAR) >'' ) ")
        End Select

        sb.AppendLine(SQLUserFilter(oUser))

        sb.AppendLine("AND Noticia.Fch <= GETDATE() ")
        sb.AppendLine("ORDER BY Fch DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTONoticia(oDrd("Guid"))
            With retval
                .Fch = oDrd("Fch")
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function SQLUserFilter(oUser As DTOUser) As String
        Dim sb As New Text.StringBuilder
        If oUser Is Nothing Then
            sb.AppendLine("AND Professional=0 ")
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("AND Noticia.Guid IN (")
                    sb.AppendLine("     SELECT NoticiaChannel.Noticia ")
                    sb.AppendLine("     FROM NoticiaChannel ")
                    sb.AppendLine("     INNER JOIN ProductChannel ON NoticiaChannel.Channel = ProductChannel.DistributionChannel ")
                    sb.AppendLine("     INNER JOIN Tpa ON ProductChannel.Product = Tpa.Guid ")
                    sb.AppendLine("     INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid ")
                    sb.AppendLine("     WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                    sb.AppendLine("     GROUP BY NoticiaChannel.Noticia ")
                    sb.AppendLine("     ) ")
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                    sb.AppendLine("AND Noticia.Guid IN (")
                    sb.AppendLine("     SELECT NoticiaChannel.Noticia ")
                    sb.AppendLine("     FROM NoticiaChannel ")
                    sb.AppendLine("     INNER JOIN RepProducts ON NoticiaChannel.Channel = RepProducts.DistributionChannel ")
                    sb.AppendLine("     INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid ")
                    sb.AppendLine("     WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "'")
                    sb.AppendLine("     AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo >= GETDATE())")
                    sb.AppendLine("     GROUP BY NoticiaChannel.Noticia ")
                    sb.AppendLine("     ) ")
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    sb.AppendLine("AND Noticia.Guid IN (")
                    sb.AppendLine("     SELECT NoticiaChannel.Noticia ")
                    sb.AppendLine("     FROM NoticiaChannel ")
                    sb.AppendLine("     INNER JOIN ContactClass ON NoticiaChannel.Channel = ContactClass.DistributionChannel ")
                    sb.AppendLine("     INNER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
                    sb.AppendLine("     INNER JOIN Email_Clis ON CliGral.Guid = Email_Clis.ContactGuid ")
                    sb.AppendLine("     WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                    sb.AppendLine("     GROUP BY NoticiaChannel.Noticia ")
                    sb.AppendLine("     ) ")

                Case DTORol.Ids.guest, DTORol.Ids.denied, DTORol.Ids.lead, DTORol.Ids.notSet, DTORol.Ids.unregistered
                    sb.AppendLine("AND Professional=0 ")
            End Select
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function NoticiaDestacada(oUser As DTOUser, oLang As DTOLang, Optional oProduct As DTOProduct = Nothing) As DTONoticia
        Dim retval As DTONoticia = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Noticia.Guid, Noticia.Fch, Noticia.UrlFriendlySegment ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine("From Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
        If oProduct IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwProductParent PP1 ON Noticia.Brand = PP1.Child ")
            sb.AppendLine("INNER JOIN VwProductParent PP2 ON PP1.Parent = PP2.Parent And PP2.Child='" & oProduct.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE Noticia.Cod = 0 ")
        sb.AppendLine("AND Noticia.Visible = 1 ")
        sb.AppendLine("AND GETDATE() BETWEEN Noticia.DestacarFrom AND Noticia.DestacarTo ")
        Select Case oLang.Tag
            Case "ESP", "POR"
                sb.AppendLine("AND CAST(LangTitle." & oLang.Tag & " AS VARCHAR) > '' ")
            Case "CAT", "ENG"
                sb.AppendLine("AND (CAST(LangTitle.Esp AS VARCHAR) > '' OR CAST(LangTitle." & oLang.Tag & " AS VARCHAR) > '') ")
        End Select

        sb.AppendLine(SQLUserFilter(oUser))

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTONoticia(oDrd("Guid"))
            With retval
                .Fch = oDrd("Fch")
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function NextEvento(oUser As DTOUser, Optional OnlyVisibles As Boolean = True) As DTOEvento
        Dim retval As DTOEvento = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Noticia.Guid, Noticia.UrlFriendlySegment ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")

        sb.AppendLine("WHERE Noticia.Cod=" & CInt(DTOContent.Srcs.Eventos) & " ")
        sb.AppendLine("AND Noticia.FchTo > '" & Format(Today, "yyyyMMdd") & "' ")

        sb.AppendLine(SQLUserFilter(oUser))

        If OnlyVisibles Then
            sb.AppendLine("AND Noticia.Visible <>0 ")
        End If
        sb.AppendLine("ORDER BY Noticia.FchTo")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOEvento(oDrd("Guid"))
            With retval
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                .urlFriendlySegment = oDrd("UrlFriendlySegment")
                .src = DTOContent.Srcs.Eventos
            End With
        End If
        oDrd.Close()
        Return retval
    End Function



    Shared Function FromKeyword(sKeyword As String) As List(Of DTONoticia)
        Dim retval As New List(Of DTONoticia)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Noticia.Guid, Noticia.Fch, Noticia.UrlFriendlySegment ")
        sb.AppendLine(", Visible, Professional, Priority, FchCreated, FchLastEdited ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("INNER JOIN Keyword ON Noticia.Guid = Keyword.Target ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
        sb.AppendLine("WHERE Keyword.value LIKE @Keyword ")
        sb.AppendLine("ORDER BY Noticia.Fch DESC ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@Keyword", "%" & sKeyword & "%")
        Do While oDrd.Read
            Dim oGuid As New Guid(oDrd("Guid").ToString())
            Dim oNoticia As New DTONoticia(oGuid)
            With oNoticia
                .fch = CDate(oDrd("fch"))
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment").ToString())
                .visible = oDrd("Visible")
                .professional = oDrd("Professional").ToString
                .priority = oDrd("Priority")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function LastVisibleNoticiaHeaders(oUser As DTOUser, Optional oLang As DTOLang = Nothing, Optional oProduct As DTOProduct = Nothing, Optional take As Integer = 0) As List(Of DTONoticia)
        Dim retVal As New List(Of DTONoticia)

        Dim sbWhere As New Text.StringBuilder
        sbWhere.AppendLine("WHERE Noticia.Cod=" & DTONoticia.Srcs.News & " ")
        sbWhere.AppendLine("AND Noticia.Visible = 1 ")

        If oLang IsNot Nothing Then
            Select Case oLang.Tag
                Case "ESP", "POR"
                    sbWhere.AppendLine("AND LEN(CAST(LangTitle." & oLang.Tag & " AS VARCHAR)) > 0 ")
                Case "CAT", "ENG"
                    sbWhere.AppendLine("AND (LEN(CAST(LangTitle.Esp AS VARCHAR)) > 0 OR LEN(CAST(LangTitle." & oLang.Tag & " AS VARCHAR)) > 0) ")
            End Select
        End If

        Dim sb As New System.Text.StringBuilder
        sb.Append("SELECT ")
        If take > 0 Then
            sb.Append("TOP " & take & " ")
        End If
        sb.AppendLine(" Noticia.Guid, Noticia.Fch, Noticia.UrlFriendlySegment, Noticia.Cod ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", Noticia.Priority, Noticia.Professional, Noticia.DestacarFrom, Noticia.DestacarTo ")
        sb.AppendLine(", Noticia.FchCreated, Noticia.UsrCreated, UsrCreated.adr as UsrCreatedEmailAddress, UsrCreated.Nickname as UsrCreatedNickName ")
        sb.AppendLine(", Noticia.FchLastEdited, Noticia.UsrLastEdited AS UsrLastEdited, UsrLastEdited.adr AS UsrLastEditedEmailAddress, UsrLastEdited.nickname as UsrLastEditedNickname")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN Email AS UsrCreated ON Noticia.UsrCreated = UsrCreated.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Email AS UsrLastEdited ON Noticia.UsrLastEdited = UsrLastEdited.Guid ")

        If oProduct IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwProductParent Brand ON Noticia.Brand = Brand.Child ")
            sb.AppendLine("INNER JOIN Tpa ON Brand.Parent = Tpa.Guid ")
            sb.AppendLine("INNER JOIN VwProductParent ON Tpa.Guid = VwProductParent.Parent AND VwProductParent.Child = '" & oProduct.Guid.ToString & "' ")
        End If

        sb.Append(sbWhere)
        sb.AppendLine(SQLUserFilter(oUser))

        sb.AppendLine("ORDER BY Noticia.Fch DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            Dim oNoticia As New DTONoticia(oDrd("Guid"))
            With oNoticia
                .Src = oDrd("Cod")
                .Fch = CDate(oDrd("fch"))
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment"))
                SQLHelper.LoadLangTextFromDataReader(.Title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                .priority = oDrd("Priority")
                .professional = oDrd("Professional").ToString
                .destacarFrom = SQLHelper.GetFchFromDataReader(oDrd("DestacarFrom"))
                .destacarTo = SQLHelper.GetFchFromDataReader(oDrd("DestacarTo"))
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With
            retVal.Add(oNoticia)
        Loop
        oDrd.Close()
        Return retVal
    End Function

    Shared Function LastNews(oSrc As DTONoticiaBase.Srcs, oUser As DTOUser, OnlyVisible As Boolean, iTop As Integer) As List(Of DTONoticia)
        Dim retVal As New List(Of DTONoticia)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT " & IIf(iTop > 0, "TOP " & iTop.ToString, "") & " ")
        sb.AppendLine("  Noticia.Guid, Noticia.Fch, Noticia.Cod, Noticia.Brand, Noticia.UrlFriendlySegment, Noticia.Location ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
        sb.AppendLine(", Noticia.Visible, Noticia.Professional, Noticia.Priority, Noticia.FchCreated, Noticia.FchLastEdited ")
        sb.AppendLine(", X.VisitCount, VwProductNom.Cod as ProductCod, VwProductNom.FullNom as ProductNom, Noticia.FchFrom ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
        sb.AppendLine("LEFT OUTER JOIN VwProductNom ON Noticia.Brand = VwProductNom.Guid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT Doc,COUNT(DISTINCT Guid) AS VisitCount FROM WebLogBrowse GROUP BY Doc) X ON Noticia.Guid = X.Doc ")

        sb.AppendLine("WHERE Noticia.Cod=" & CInt(oSrc) & " ")
        If OnlyVisible Then sb.AppendLine("AND Noticia.Visible = 1 ")

        sb.AppendLine(SQLUserFilter(oUser))

        If oSrc = DTOContent.Srcs.News Then
            sb.AppendLine("ORDER BY Noticia.Fch DESC ")
        ElseIf oSrc = DTOContent.Srcs.Eventos Then
            If OnlyVisible Then sb.AppendLine("AND Noticia.FchTo >=GETDATE() ")
            sb.AppendLine("ORDER BY Noticia.FchFrom DESC")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oNoticia As DTONoticia = Nothing
        Do While oDrd.Read
            Dim oGuid As New Guid(oDrd("Guid").ToString())
            If oSrc = DTOContent.Srcs.Eventos Then
                oNoticia = New DTOEvento(oGuid)
            Else
                oNoticia = New DTONoticia(oGuid)
            End If
            With oNoticia
                .visible = oDrd("Visible")
                .fch = CDate(oDrd("fch"))
                If Not IsDBNull(oDrd("Brand")) Then
                    .product = New DTOProduct(oDrd("Brand"))
                    .product.sourceCod = oDrd("ProductCod")
                    .product.nom = oDrd("ProductNom")
                End If
                If Not IsDBNull(oDrd("UrlFriendlySegment")) Then
                    .urlFriendlySegment = oDrd("UrlFriendlySegment").ToString
                End If

                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                SQLHelper.LoadLangTextFromDataReader(.excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")

                .professional = oDrd("Professional").ToString
                .priority = oDrd("Priority")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                If Not IsDBNull(oDrd("VisitCount")) Then
                    .VisitCount = oDrd("VisitCount")
                End If
                .src = oDrd("Cod")
                If TypeOf oNoticia Is DTOEvento Then
                    If Not IsDBNull(oDrd("FchFrom")) Then
                        DirectCast(oNoticia, DTOEvento).FchFrom = oDrd("FchFrom")
                    End If
                    If Not IsDBNull(oDrd("Location")) Then
                        DirectCast(oNoticia, DTOEvento).Area = New DTOArea(oDrd("Location"))
                    End If
                End If
                '.IsLoaded = True
            End With
            retVal.Add(oNoticia)
        Loop
        oDrd.Close()
        Return retVal
    End Function

    Shared Function FromCategoria(oCategoria As DTOCategoriaDeNoticia) As List(Of DTONoticia)
        Dim retVal As New List(Of DTONoticia)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid, Noticia.Fch, Noticia.UrlFriendlySegment ")
        sb.AppendLine(", Noticia.Visible, Noticia.Professional, Noticia.Priority, Noticia.FchCreated, Noticia.FchLastEdited ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("INNER JOIN NoticiaCategoria ON Noticia.Guid=NoticiaCategoria.Noticia ")
        sb.AppendLine("WHERE NoticiaCategoria.Categoria='" & oCategoria.Guid.ToString & "' AND Noticia.Visible<>0 ")
        sb.AppendLine("ORDER BY Noticia.Fch DESC")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As New Guid(oDrd("Guid").ToString())
            Dim oNoticia As New DTONoticia(oGuid)
            With oNoticia
                .fch = CDate(oDrd("fch"))
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                .urlFriendlySegment = SQLHelper.GetStringFromDataReader(oDrd("UrlFriendlySegment").ToString())
                .visible = oDrd("Visible")
                .professional = oDrd("Professional").ToString
                .priority = oDrd("Priority")
                .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
            End With

        Loop
        oDrd.Close()
        Return retVal
    End Function

    Shared Function Destacats(oSrc As DTONoticiaBase.Srcs, oUser As DTOUser) As List(Of DTONoticia)
        Dim retval As New List(Of DTONoticia)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Noticia.Guid, Noticia.Fch, Noticia.DestacarFrom, Noticia.DestacarTo ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("WHERE Noticia.Cod=" & CInt(oSrc) & " ")

        sb.AppendLine(SQLUserFilter(oUser))

        sb.AppendLine("AND Noticia.DestacarFrom IS NOT NULL ")
        sb.AppendLine("ORDER BY Noticia.DestacarFrom, Noticia.DestacarTo DESC")

        Dim item As New DTONoticia
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Select Case oSrc
                Case DTOContent.Srcs.News, DTOContent.Srcs.SabiasQue
                    item = New DTONoticia(DirectCast(oDrd("Guid"), Guid))
                Case DTOContent.Srcs.Eventos
                    item = New DTOEvento(DirectCast(oDrd("Guid"), Guid))
            End Select
            With item
                .Fch = oDrd("Fch")
                SQLHelper.LoadLangTextFromDataReader(.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
                .destacarFrom = SQLHelper.GetFchFromDataReader(oDrd("DestacarFrom"))
                .DestacarTo = SQLHelper.GetFchFromDataReader(oDrd("DestacarTo"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function



End Class
