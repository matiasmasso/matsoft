Imports DTO.Models.SellOutModel

Public Class YouTubeMovieLoader

#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOYouTubeMovie
        Dim retval As DTOYouTubeMovie = Nothing
        Dim oYouTubeMovie As New DTOYouTubeMovie(oGuid)
        If Load(oYouTubeMovie) Then
            retval = oYouTubeMovie
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oYouTubeMovie As DTOYouTubeMovie) As Boolean
        If Not oYouTubeMovie.IsLoaded And Not oYouTubeMovie.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT YouTube.Nom, YouTube.YouTubeId, YouTube.FchTo, YouTube.Duration, YouTube.Lang, YouTube.LangSet, YouTube.Tags, YouTube.FchCreated, YouTube.Obsoleto  ")
            sb.AppendLine(", X.ProductGuid, X.ProductCod, X.BrandGuid, X.BrandNom, X.CategoryGuid, X.CategoryNom, X.SkuNom ")
            sb.AppendLine(", LangNom.Esp AS NomEsp, LangNom.Cat AS NomCat, LangNom.Eng AS NomEng, LangNom.Por AS NomPor ")
            sb.AppendLine(", LangDsc.Esp AS DscEsp, LangDsc.Cat AS DscCat, LangDsc.Eng AS DscEng, LangDsc.Por AS DscPor ")
            sb.AppendLine("From YouTube ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText LangNom ON YouTube.Guid = LangNom.Guid AND LangNom.Src = " & DTOLangText.Srcs.YouTubeNom & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText LangDsc ON YouTube.Guid = LangDsc.Guid AND LangDsc.Src = " & DTOLangText.Srcs.YouTubeExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN (")
            sb.AppendLine("     SELECT ProductGuid, YouTubeGuid, VwProductNom.Cod As ProductCod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom  ")
            sb.AppendLine("     From YouTubeProducts INNER JOIN VwProductNom On YouTubeProducts.ProductGuid = VwProductNom.Guid ")
            sb.AppendLine("     ) X On YouTube.Guid = X.YouTubeGuid ")
            sb.AppendLine("WHERE YouTube.Guid='" & oYouTubeMovie.Guid.ToString & "' ")
            sb.AppendLine("ORDER BY X.BrandNom, X.CategoryNom, X.SkuNom ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read

                With oYouTubeMovie
                    If Not .IsLoaded Then
                        .YoutubeId = oDrd("YoutubeId")
                        SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                        SQLHelper.LoadLangTextFromDataReader(.Dsc, oDrd, "DscEsp", "DscCat", "DscEng", "DscPor")
                        .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                        .LangSet = New DTOLang.Set(oDrd("LangSet"))
                        If Not IsDBNull(oDrd("Duration")) Then
                            .Duration = TimeSpan.FromSeconds(oDrd("Duration"))
                        End If
                        If Not IsDBNull(oDrd("Tags")) Then
                            .Tags = oDrd("Tags").ToString().Split(",").Where(Function(x) Not String.IsNullOrEmpty(x)).ToList()
                        End If
                        .FchTo = SQLHelper.GetNullableFchFromDataReader(oDrd("FchTo"))
                        .FchCreated = oDrd("FchCreated")
                        .Obsoleto = oDrd("Obsoleto")
                        .IsLoaded = True
                    End If
                    If Not IsDBNull(oDrd("ProductGuid")) Then
                        Dim item As DTOProduct = ProductLoader.NewProduct(CInt(oDrd("ProductCod")), DirectCast(oDrd("BrandGuid"), Guid), oDrd("BrandNom").ToString, oDrd("CategoryGuid"), oDrd("CategoryNom"), oDrd("ProductGuid"), oDrd("SkuNom"))
                        .Products.Add(item)
                    End If
                End With
            Loop

            oDrd.Close()
        End If

        Dim retval As Boolean = oYouTubeMovie.IsLoaded
        Return retval
    End Function

    Shared Function Thumbnail(oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT YouTube.Thumbnail, YouTube.ThumbnailMime ")
        sb.AppendLine("From YouTube ")
        sb.AppendLine("WHERE YouTube.Guid='" & oGuid.ToString & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New ImageMime
            retval.ByteArray = SQLHelper.GetBytesFromDatareader(oDrd("Thumbnail"))
            retval.Mime = SQLHelper.GetIntegerFromDataReader(oDrd("ThumbnailMime"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oYouTubeMovie As DTOYouTubeMovie, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Update(oYouTubeMovie, oTrans)
            LangTextLoader.Update(oYouTubeMovie.Nom, oTrans)
            LangTextLoader.Update(oYouTubeMovie.Dsc, oTrans)

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


    Shared Sub Update(oYouTubeMovie As DTOYouTubeMovie, ByRef oTrans As SqlTransaction)
        UpdateHeader(oYouTubeMovie, oTrans)
        UpdateProducts(oYouTubeMovie, oTrans)
    End Sub

    Shared Sub UpdateHeader(oYouTubeMovie As DTOYouTubeMovie, ByRef oTrans As SqlTransaction)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM YouTube ")
        sb.AppendLine("WHERE YouTube.Guid='" & oYouTubeMovie.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oYouTubeMovie.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oYouTubeMovie
            oRow("YouTubeId") = .YoutubeId
            oRow("Lang") = SQLHelper.NullableLang(.Lang)
            oRow("LangSet") = .LangSet.Value
            oRow("Obsoleto") = .Obsoleto
            If .Duration Is Nothing Then
                oRow("Duration") = System.DBNull.Value
            Else
                Dim dtDuration As TimeSpan = CType(.Duration, TimeSpan)
                If (dtDuration.TotalSeconds) = 0 Then
                    oRow("Duration") = System.DBNull.Value
                Else
                    oRow("Duration") = dtDuration.TotalSeconds
                End If
            End If

            If .FchTo Is Nothing Then
                oRow("FchTo") = System.DBNull.Value
            Else
                oRow("FchTo") = SQLHelper.NullableFch(.FchTo)
            End If

            oRow("Tags") = If(.Tags.Count = 0, System.DBNull.Value, String.Join(",", .Tags.ToArray))
            If .Thumbnail IsNot Nothing AndAlso .Thumbnail.Length > 0 Then
                oRow("Thumbnail") = .Thumbnail
                oRow("ThumbnailMime") = .ThumbnailMime
            Else
                oRow("Thumbnail") = System.DBNull.Value
                oRow("ThumbnailMime") = System.DBNull.Value
            End If
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function Delete(oYouTubeMovie As DTOYouTubeMovie, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Delete(oYouTubeMovie, oTrans)
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


    Shared Sub Delete(oYouTubeMovie As DTOYouTubeMovie, ByRef oTrans As SqlTransaction)
        DeleteProducts(oYouTubeMovie, oTrans)
        LangTextLoader.Delete(oYouTubeMovie.Dsc, oTrans)
        LangTextLoader.Delete(oYouTubeMovie.Nom, oTrans)
        DeleteHeader(oYouTubeMovie, oTrans)
    End Sub

    Shared Sub DeleteHeader(oYouTubeMovie As DTOYouTubeMovie, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE YouTube WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oYouTubeMovie.Guid.ToString())
    End Sub

    Shared Sub DeleteProducts(oYouTubeMovie As DTOYouTubeMovie, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE YouTubeProducts WHERE YouTubeGuid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oYouTubeMovie.Guid.ToString())
    End Sub


    Shared Function Products(oMovie As DTOYouTubeMovie) As List(Of DTOProduct)
        Dim retval As New List(Of DTOProduct)
        Dim SQL As String = "SELECT PRODUCTGUID FROM YOUTUBEPRODUCTS WHERE YOUTUBEGUID='" & oMovie.Guid.ToString & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProduct(DirectCast(oDrd("PRODUCTGUID"), Guid))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Sub UpdateProducts(oMovie As DTOYouTubeMovie, oTrans As SqlTransaction)
        DeleteProducts(oMovie, oTrans)

        Dim SQL As String = "SELECT * FROM YOUTUBEPRODUCTS WHERE YOUTUBEGUID ='" & oMovie.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDS As New DataSet

        oDA.Fill(oDS)

        Dim oTb As DataTable = oDS.Tables(0)
        For Each oProduct As DTOProduct In oMovie.Products
            Dim oRow As DataRow = oTb.NewRow
            oRow("YoutubeGuid") = oMovie.Guid
            oRow("ProductGuid") = oProduct.Guid
            oTb.Rows.Add(oRow)
        Next

        oDA.Update(oDS)
    End Sub

#End Region

    Shared Function HasProduct(ByVal oProduct As DTOProduct) As Boolean
        Dim SQL As String = "SELECT TOP 1 YoutubeGuid FROM YouTubeProducts "

        If TypeOf oProduct Is DTOProductBrand Then
            SQL = SQL & "WHERE ProductGuid = '" & oProduct.Guid.ToString & "'"
        ElseIf TypeOf oProduct Is DTOProductCategory Then
            SQL = SQL & "WHERE ProductGuid = '" & oProduct.Guid.ToString & "'"
        ElseIf TypeOf oProduct Is DTOProductSku Then
            Dim oSku As DTOProductSku = oProduct
            SQL = SQL & "WHERE (ProductGuid = '" & oSku.Guid.ToString & "' OR ProductGuid = '" & oSku.Category.Guid.ToString & "')"
        End If

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()

        Return retval
    End Function
End Class

Public Class YouTubeMoviesLoader

    Shared Function ProductModel(oEmp As DTOEmp, oLang As DTOLang, Optional oUser As DTOUser = Nothing) As List(Of DTOYouTubeMovie)
        Dim retval As New List(Of DTOYouTubeMovie)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Youtube.Guid, Youtube.YouTubeId, YouTube.Nom, YouTube.Lang, YouTube.LangSet, YouTube.FchCreated ")
        sb.AppendLine(", LangNom.Esp AS NomEsp, LangNom.Cat AS NomCat, LangNom.Eng AS NomEng, LangNom.Por AS NomPor ")
        sb.AppendLine(", YoutubeProducts.ProductGuid  ")
        sb.AppendLine(", VwProductNom.Cod, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuGuid, VwProductNom.SkuNom ")
        sb.AppendLine("FROM YouTube  ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangNom ON YouTube.Guid = LangNom.Guid AND LangNom.Src = " & DTOLangText.Srcs.YouTubeNom & " ")
        sb.AppendLine("INNER JOIN YouTubeProducts on YouTube.Guid=YoutubeProducts.YouTubeGuid  ")
        sb.AppendLine("INNER JOIN VwProductNom on YoutubeProducts.ProductGuid = VwProductNom.Guid  ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductNom.BrandGuid = Tpa.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Stp ON VwProductNom.CategoryGuid = Stp.Guid ")

        If oUser IsNot Nothing Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN ProductChannel ON VwProductParent.Parent = ProductChannel.Product ")
                    sb.AppendLine("INNER JOIN ContactClass ON ProductChannel.DistributionChannel = ContactClass.DistributionChannel ")
                    sb.AppendLine("INNER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
                    sb.AppendLine("INNER JOIN Email_Clis ON CliGral.Guid=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN ProductChannel ON VwProductParent.Parent = ProductChannel.Product ")
                    sb.AppendLine("INNER JOIN RepProducts ON ProductChannel.DistributionChannel = RepProducts.DistributionChannel AND RepProducts.Cod = 1 AND (RepProducts.FchTo IS NULL OR RepProducts.fchTo>=GETDATE()) ")
                    sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            End Select
        End If

        sb.AppendLine("WHERE Youtube.Obsoleto = 0 ")
        sb.AppendLine("AND Tpa.Emp=" & oEmp.Id & " AND Tpa.Enliquidacio=0 AND VwProductNom.Obsoleto=0 ")

        Select Case oLang.id
            Case DTOLang.Ids.ESP, DTOLang.Ids.POR
                sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = '" & oLang.Tag & "' ) ")
            Case Else
                sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = 'ESP' OR Youtube.Lang = '" & oLang.Tag & "' ) ")
        End Select

        sb.AppendLine("ORDER BY YouTube.fchcreated desc, Youtube.Guid, Tpa.Ord, VwProductNom.BrandNom, Stp.Codi, Stp.Ord, VwProductNom.CategoryNom, Stp.Guid ")

        Dim SQL As String = sb.ToString
        Dim oVideo As New DTOYouTubeMovie
        Dim oProducts As New List(Of DTOProduct)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oVideo.Guid.Equals(oDrd("Guid")) Then
                oVideo = New DTOYouTubeMovie
                With oVideo
                    .Guid = oDrd("Guid")
                    .YoutubeId = oDrd("YouTubeId")
                    .LangSet = New DTOLang.Set(oDrd("LangSet"))
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                    .FchCreated = oDrd("FchCreated")
                End With
                retval.Add(oVideo)
            End If

            'Dim oProduct = SQLHelper.GetProductFromDataReader(oDrd)
            'oProducts.Add(oProduct)

            'Dim oProductVideos = retval.ProductVideos.FirstOrDefault(Function(x) x.Product.Equals(oProduct.Guid))
            'If oProductVideos Is Nothing Then
            '    oProductVideos = New DTOYouTubeMovie.ProductModel.ProductVideosClass()
            '    oProductVideos.Product = oProduct.Guid
            '    retval.ProductVideos.Add(oProductVideos)
            'End If

            'oProductVideos.Videos.Add(oVideo.Guid)

        Loop
        oDrd.Close()

        'Dim oUniqueProducts = oProducts.GroupBy(Function(x) x.Guid).Select(Function(y) y.First).ToList()

        'retval.Catalog = New DTOBasicCatalog()

        'Dim oBrands = oUniqueProducts.GroupBy(Function(x) DTOProduct.Brand(x).Guid).
        '    Select(Function(y) DTOProduct.Brand(y.First())).
        '    Select(Function(z) New DTOBasicCatalog.Brand With {.Guid = z.Guid, .Nom = z.Nom.Esp}).
        '    OrderBy(Function(o) o.Nom).ToList()
        'For Each oBrand In oBrands
        '    Dim oBrandGuid = oBrand.Guid
        '    retval.Catalog.Add(oBrand)
        '    Dim oCategories = oUniqueProducts.
        '        Where(Function(x) DTOProduct.Brand(x).Guid.Equals(oBrandGuid) And DTOProduct.Category(x) IsNot Nothing).
        '        GroupBy(Function(y) DTOProduct.Category(y).Guid).
        '        Select(Function(z) DTOProduct.Category(z.First())).
        '        Select(Function(a) New DTOBasicCatalog.Category With {.Guid = a.Guid, .Nom = a.Nom.Esp}).
        '        OrderBy(Function(b) b.Nom).ToList()
        '    For Each oCategory In oCategories
        '        oBrand.Categories.Add(New DTOBasicCatalog.Category(oCategory.Guid, oCategory.Nom))
        '        Dim oCategoryGuid = oCategory.Guid
        '        Dim oSkus = oUniqueProducts.Where(Function(x) x.SourceCod = DTOProduct.SourceCods.Sku AndAlso CType(x, DTOProductSku).Category.Guid.Equals(oCategoryGuid)).
        '            Select(Function(y) New DTOBasicCatalog.Sku With {.Guid = y.Guid, .Nom = y.Nom.Esp}).
        '            OrderBy(Function(z) z.Nom).ToList()
        '        oBrand.Categories.Last.Skus = oSkus
        '    Next
        'Next

        Return retval
    End Function
    Shared Function All(Optional oProduct As DTOProduct = Nothing, Optional oUser As DTOUser = Nothing, Optional oLang As DTOLang = Nothing) As List(Of DTOYouTubeMovie)
        Dim retval As New List(Of DTOYouTubeMovie)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Youtube.Guid, Youtube.YouTubeId, YouTube.Nom, YouTube.Duration, YouTube.Lang, YouTube.LangSet, YouTube.ThumbnailMime, YouTube.FchCreated, YouTube.Obsoleto ")
        sb.AppendLine(", LangNom.Esp AS NomEsp, LangNom.Cat AS NomCat, LangNom.Eng AS NomEng, LangNom.Por AS NomPor ")
        sb.AppendLine(", LangDsc.Esp AS DscEsp, LangDsc.Cat AS DscCat, LangDsc.Eng AS DscEng, LangDsc.Por AS DscPor ")
        sb.AppendLine("FROM YouTube  ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangNom ON YouTube.Guid = LangNom.Guid AND LangNom.Src = " & DTOLangText.Srcs.YouTubeNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangDsc ON YouTube.Guid = LangDsc.Guid AND LangDsc.Src = " & DTOLangText.Srcs.YouTubeExcerpt & " ")
        If oProduct IsNot Nothing Then
            sb.AppendLine("INNER JOIN YouTubeProducts on YouTube.Guid=YoutubeProducts.YouTubeGuid  ")
            sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid  ")
            sb.AppendLine("WHERE (VwProductParent.Parent='" & oProduct.Guid.ToString & "' OR VwProductParent.Child='" & oProduct.Guid.ToString & "') ")
        ElseIf oUser IsNot Nothing Then

            Select Case oUser.Rol.id
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    sb.AppendLine("INNER JOIN YoutubeProducts ON YouTube.Guid = YouTubeProducts.YouTubeGuid ")
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN ProductChannel ON VwProductParent.Parent = ProductChannel.Product ")
                    sb.AppendLine("INNER JOIN ContactClass ON ProductChannel.DistributionChannel = ContactClass.DistributionChannel ")
                    sb.AppendLine("INNER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
                    sb.AppendLine("INNER JOIN Email_Clis ON CliGral.Guid=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                    sb.AppendLine("INNER JOIN YoutubeProducts ON YouTube.Guid = YouTubeProducts.YouTubeGuid ")
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN ProductChannel ON VwProductParent.Parent = ProductChannel.Product ")
                    sb.AppendLine("INNER JOIN RepProducts ON ProductChannel.DistributionChannel = RepProducts.DistributionChannel AND RepProducts.Cod = 1 AND (RepProducts.FchTo IS NULL OR RepProducts.fchTo>=GETDATE()) ")
                    sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("INNER JOIN YoutubeProducts ON YouTube.Guid = YouTubeProducts.YouTubeGuid ")
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            End Select
            sb.AppendLine("WHERE 1=1 ")
        Else
            sb.AppendLine("WHERE 1=1 ")
        End If

        If oLang IsNot Nothing Then
            Select Case oLang.id
                Case DTOLang.Ids.CAT, DTOLang.Ids.ENG
                    sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = 'ESP' OR Youtube.Lang = '" & oLang.Tag & "') ")
                Case Else
                    sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = '" & oLang.Tag & "') ")
            End Select
        End If

        sb.AppendLine("GROUP BY Youtube.Guid, Youtube.YouTubeId, YouTube.Nom, YouTube.Duration, YouTube.Lang, YouTube.LangSet, YouTube.ThumbnailMime, YouTube.FchCreated, YouTube.Obsoleto ")
        sb.AppendLine(", LangNom.Esp, LangNom.Cat, LangNom.Eng, LangNom.Por ")
        sb.AppendLine(", LangDsc.Esp, LangDsc.Cat, LangDsc.Eng, LangDsc.Por ")
        sb.AppendLine("ORDER BY YouTube.fchcreated desc, Youtube.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOYouTubeMovie(oDrd("Guid"))
            With item
                .YoutubeId = oDrd("YouTubeId")
                .Lang = SQLHelper.GetLangFromDataReader(oDrd("Lang"))
                .LangSet = New DTOLang.Set(oDrd("LangSet"))
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                If Not IsDBNull(oDrd("Duration")) Then
                    .Duration = TimeSpan.FromSeconds(oDrd("Duration"))
                End If
                .ThumbnailMime = SQLHelper.GetIntegerFromDataReader(oDrd("ThumbnailMime"))
                .Obsoleto = oDrd("Obsoleto")
                .FchCreated = oDrd("FchCreated")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Last(Optional oProductChild As DTOProduct = Nothing, Optional oUser As DTOUser = Nothing, Optional oLang As DTOLang = Nothing) As DTOYouTubeMovie
        'Dim sCount As String = IIf(iCount = 0, "100%", iCount.ToString())
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Youtube.Guid, Youtube.YouTubeId, YouTube.FchCreated, YouTube.Obsoleto ")
        sb.AppendLine(", LangNom.Esp AS NomEsp, LangNom.Cat AS NomCat, LangNom.Eng AS NomEng, LangNom.Por AS NomPor ")
        sb.AppendLine(", LangDsc.Esp AS DscEsp, LangDsc.Cat AS DscCat, LangDsc.Eng AS DscEng, LangDsc.Por AS DscPor ")
        sb.AppendLine("FROM YouTube  ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangNom ON YouTube.Guid = LangNom.Guid AND LangNom.Src = " & DTOLangText.Srcs.YouTubeNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangDsc ON YouTube.Guid = LangDsc.Guid AND LangDsc.Src = " & DTOLangText.Srcs.YouTubeExcerpt & " ")
        If oProductChild IsNot Nothing Then
            sb.AppendLine("INNER JOIN YouTubeProducts on YouTube.Guid=YoutubeProducts.YouTubeGuid  ")
            sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid  ")
            sb.AppendLine("WHERE (VwProductParent.Parent='" & oProductChild.Guid.ToString & "' OR VwProductParent.Child='" & oProductChild.Guid.ToString & "') ")

        ElseIf oUser IsNot Nothing Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    sb.AppendLine("INNER JOIN YoutubeProducts ON YouTube.Guid = YouTubeProducts.YouTubeGuid ")
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN ProductChannel ON VwProductParent.Parent = ProductChannel.Product ")
                    sb.AppendLine("INNER JOIN ContactClass ON ProductChannel.DistributionChannel = ContactClass.DistributionChannel ")
                    sb.AppendLine("INNER JOIN CliGral ON ContactClass.Guid = CliGral.ContactClass ")
                    sb.AppendLine("INNER JOIN Email_Clis ON CliGral.Guid=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.rep, DTORol.Ids.comercial
                    sb.AppendLine("INNER JOIN YoutubeProducts ON YouTube.Guid = YouTubeProducts.YouTubeGuid ")
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN ProductChannel ON VwProductParent.Parent = ProductChannel.Product ")
                    sb.AppendLine("INNER JOIN RepProducts ON ProductChannel.DistributionChannel = RepProducts.DistributionChannel AND RepProducts.Cod = 1 AND (RepProducts.FchTo IS NULL OR RepProducts.fchTo>=GETDATE()) ")
                    sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("INNER JOIN YoutubeProducts ON YouTube.Guid = YouTubeProducts.YouTubeGuid ")
                    sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid ")
                    sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor=Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            End Select
            sb.AppendLine("WHERE 1=1 ")
        Else
            'TO DO: where langset
            sb.AppendLine("WHERE 1=1 ")
        End If

        If oLang IsNot Nothing Then
            sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = '" & oLang.Tag & "') ")
        End If

        sb.AppendLine("ORDER BY YouTube.fchcreated desc, Youtube.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oMovies As List(Of DTOYouTubeMovie) = YouTubeMoviesLoader.Load(oDrd)
        oDrd.Close()
        Dim retval As DTOYouTubeMovie = Nothing
        If oMovies.Count > 0 Then
            retval = oMovies.First
        End If
        Return retval
    End Function

    Shared Function FromChildrenOrSelf(oProduct As DTOProduct, Optional oLang As DTOLang = Nothing) As List(Of DTOYouTubeMovie)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Youtube.Guid, Youtube.YouTubeId, YouTube.FchCreated, YouTube.Obsoleto ")
        sb.AppendLine(", LangNom.Esp AS NomEsp, LangNom.Cat AS NomCat, LangNom.Eng AS NomEng, LangNom.Por AS NomPor ")
        sb.AppendLine(", LangDsc.Esp AS DscEsp, LangDsc.Cat AS DscCat, LangDsc.Eng AS DscEng, LangDsc.Por AS DscPor ")
        sb.AppendLine("from YouTube  ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangNom ON YouTube.Guid = LangNom.Guid AND LangNom.Src = " & DTOLangText.Srcs.YouTubeNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangDsc ON YouTube.Guid = LangDsc.Guid AND LangDsc.Src = " & DTOLangText.Srcs.YouTubeExcerpt & " ")
        sb.AppendLine("Inner join YouTubeProducts on YouTube.Guid=YoutubeProducts.YouTubeGuid  ")
        sb.AppendLine("Inner JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid  ")
        sb.AppendLine("WHERE (VwProductParent.Parent='" & oProduct.Guid.ToString & "' OR VwProductParent.Child='" & oProduct.Guid.ToString & "') ")

        If oLang IsNot Nothing Then
            sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = '" & oLang.Tag & "') ")
        End If

        sb.AppendLine("order by YouTube.fchcreated desc, Youtube.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As List(Of DTOYouTubeMovie) = YouTubeMoviesLoader.Load(oDrd)
        oDrd.Close()

        Return retval
    End Function

    Shared Function FromProduct(oProduct As DTOProduct, Optional oLang As DTOLang = Nothing) As List(Of DTOYouTubeMovie)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Youtube.Guid, Youtube.YouTubeId, YouTube.Nom, YouTube.FchCreated, YouTube.Obsoleto ")
        sb.AppendLine(", LangNom.Esp AS NomEsp, LangNom.Cat AS NomCat, LangNom.Eng AS NomEng, LangNom.Por AS NomPor ")
        sb.AppendLine(", LangDsc.Esp AS DscEsp, LangDsc.Cat AS DscCat, LangDsc.Eng AS DscEng, LangDsc.Por AS DscPor ")
        sb.AppendLine("FROM YouTube  ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangNom ON YouTube.Guid = LangNom.Guid AND LangNom.Src = " & DTOLangText.Srcs.YouTubeNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText LangDsc ON YouTube.Guid = LangDsc.Guid AND LangDsc.Src = " & DTOLangText.Srcs.YouTubeExcerpt & " ")
        sb.AppendLine("INNER join YouTubeProducts on YouTube.Guid = YoutubeProducts.YouTubeGuid  ")
        sb.AppendLine("WHERE YoutubeProducts.ProductGuid = '" & oProduct.Guid.ToString & "' ")

        If oLang IsNot Nothing Then
            Select Case oLang.id
                Case DTOLang.Ids.POR
                    sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = '" & oLang.Tag & "') ")
                Case Else
                    sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = 'ESP' OR Youtube.Lang = '" & oLang.Tag & "') ")
            End Select
        End If

        sb.AppendLine("ORDER BY YouTube.fchcreated desc, Youtube.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As List(Of DTOYouTubeMovie) = YouTubeMoviesLoader.Load(oDrd)
        oDrd.Close()

        Return retval
    End Function

    Shared Function ExistFromProduct(oGuid As Guid, Optional oLang As DTOLang = Nothing) As Boolean
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 YoutubeProducts.YouTubeGuid ")
        sb.AppendLine("FROM YouTubeProducts  ")
        sb.AppendLine("INNER JOIN VwProductParent on VwProductParent.Child=YoutubeProducts.ProductGuid  ")
        sb.AppendLine("INNER JOIN YouTube ON YouTubeProducts.YouTubeGuid = YouTube.Guid  ")
        sb.AppendLine("WHERE (VwProductParent.Parent='" & oGuid.ToString & "' OR VwProductParent.Child='" & oGuid.ToString & "')  ")

        If oLang IsNot Nothing Then
            sb.AppendLine("AND (Youtube.Lang IS NULL OR Youtube.Lang = '" & oLang.Tag & "') ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim retval As Boolean = oDrd.Read
        oDrd.Close()

        Return retval
    End Function


    Shared Function Load(oDrd As SqlDataReader) As List(Of DTOYouTubeMovie)
        Dim retval As New List(Of DTOYouTubeMovie)
        Dim oGuid As Guid = System.Guid.NewGuid
        Do While oDrd.Read
            If Not oGuid.Equals(oDrd("Guid")) Then 'filtra els duplicats dons el camp Dsc es text i SQL no pot fer GroupBy
                oGuid = oDrd("Guid")
                Dim oItem As New DTOYouTubeMovie(oGuid)
                With oItem
                    .YoutubeId = oDrd("YouTubeId")

                    If SQLHelper.FieldExists(oDrd, "NomEsp") Then
                        SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
                    End If
                    If SQLHelper.FieldExists(oDrd, "DscEsp") Then
                        SQLHelper.LoadLangTextFromDataReader(.Dsc, oDrd, "DscEsp", "DscCat", "DscEng", "DscPor")
                    End If
                    .FchCreated = oDrd("FchCreated")
                    .Obsoleto = oDrd("Obsoleto")
                End With
                retval.Add(oItem)
            End If
        Loop
        Return retval
    End Function

End Class
