Public Class ProductCategoryLoader


    Shared Function Find(oGuid As Guid) As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing
        Dim oCategory As New DTOProductCategory(oGuid)
        oCategory.SourceCod = DTOProduct.SourceCods.Category
        If Load(oCategory) Then
            retval = oCategory
        End If
        Return retval
    End Function



    Shared Function Load(ByRef oCategory As DTOProductCategory) As Boolean
        If Not oCategory.IsLoaded And Not oCategory.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Stp.*, Tpa.Proveidor, Tpa.ShowAtlas, Tpa.MadeIn AS TpaMadeIn ")
            sb.AppendLine(", Tpa.CodiMercancia AS TpaCodiMercancia, TpaCodisMercancia.Dsc AS TpaCodisMercanciaDsc, CliGral.FullNom ")
            sb.AppendLine(", StpMadeIn.ISO AS StpMadeInISO, StpMadeIn.Nom_Eng AS StpMadeInEng ")
            sb.AppendLine(", TpaMadeIn.ISO AS TpaMadeInISO, TpaMadeIn.Nom_Eng AS TpaMadeInEng ")
            sb.AppendLine(", CodisMercancia.Dsc AS CodiMercanciaDsc ")
            sb.AppendLine(", BrandNom.Esp AS BrandNom ")
            sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
            sb.AppendLine(", SeoTitle.Esp AS SeoTitleEsp, SeoTitle.Cat AS SeoTitleCat, SeoTitle.Eng AS SeoTitleEng, SeoTitle.Por AS SeoTitlePor ")
            sb.AppendLine(", CategoryExcerpt.Esp AS CategoryExcerptEsp, CategoryExcerpt.Cat AS CategoryExcerptCat, CategoryExcerpt.Eng AS CategoryExcerptEng, CategoryExcerpt.Por AS CategoryExcerptPor ")
            sb.AppendLine(", CategoryContent.Esp AS CategoryContentEsp, CategoryContent.Cat AS CategoryContentCat, CategoryContent.Eng AS CategoryContentEng, CategoryContent.Por AS CategoryContentPor  ")
            sb.AppendLine(", VwCnap.Id AS CnapId, VwCnap.ShortNomEsp AS CnapShortEsp, VwCnap.LongNomEsp AS CnapLongESP ")
            sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor ")
            sb.AppendLine("FROM Stp ")
            sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Tpa.Proveidor = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText SeoTitle ON Stp.Guid = SeoTitle.Guid AND SeoTitle.Src = " & DTOLangText.Srcs.SeoTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryExcerpt ON Stp.Guid = CategoryExcerpt.Guid AND CategoryExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryContent ON Stp.Guid = CategoryContent.Guid AND CategoryContent.Src = " & DTOLangText.Srcs.ProductText & " ")
            sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Stp.Guid = Url.Guid ")

            sb.AppendLine("LEFT OUTER JOIN Country AS StpMadeIn ON Stp.MadeIn=StpMadeIn.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Country AS TpaMadeIn ON Tpa.MadeIn=TpaMadeIn.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwCnap ON Stp.CnapGuid=VwCnap.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CodisMercancia ON Stp.CodiMercancia=CodisMercancia.Id ")
            sb.AppendLine("LEFT OUTER JOIN CodisMercancia TpaCodisMercancia ON Tpa.CodiMercancia=TpaCodisMercancia.Id ")
            sb.AppendLine("WHERE Stp.Guid='" & oCategory.Guid.ToString & "' ")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oCategory.Guid.ToString())
            If oDrd.Read Then
                With oCategory
                    .Brand = New DTOProductBrand(oDrd("Brand"))
                    With .Brand
                        SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                        If Not IsDBNull(oDrd("TpaMadeIn")) Then
                            .MadeIn = SQLHelper.GetCountryFromDataReader(oDrd, "TpaMadeIn", "TpaMadeInEng", "TpaMadeInEng", "TpaMadeInEng", "TpaMadeInEng", "TpaMadeInISO")
                        End If
                        If Not IsDBNull(oDrd("TpaCodiMercancia")) Then
                            .CodiMercancia = New DTOCodiMercancia(oDrd("TpaCodiMercancia"))
                            .CodiMercancia.Dsc = SQLHelper.GetStringFromDataReader(oDrd("TpaCodisMercanciaDsc"))
                        End If
                        .ShowAtlas = oDrd("ShowAtlas")
                    End With
                    If Not IsDBNull(oDrd("Proveidor")) Then
                        .Brand.Proveidor = New DTOProveidor(oDrd("Proveidor"))
                        .Brand.Proveidor.FullNom = SQLHelper.GetStringFromDataReader(oDrd("FullNom"))
                    End If
                    .Codi = oDrd("Codi")
                    .Dsc_PropagateToChildren = oDrd("Dsc_PropagateToChildren")
                    .Ord = oDrd("Ord")
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    SQLHelper.LoadLangTextFromDataReader(.SeoTitle, oDrd, "SeoTitleEsp", "SeoTitleCat", "SeoTitleEng", "SeoTitlePor")
                    SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "CategoryExcerptEsp", "CategoryExcerptCat", "CategoryExcerptEng", "CategoryExcerptPor")
                    SQLHelper.LoadLangTextFromDataReader(.Content, oDrd, "CategoryContentEsp", "CategoryContentCat", "CategoryContentEng", "CategoryContentPor")
                    .UrlCanonicas = SQLHelper.GetProductCategoryUrlCanonicasFromDataReader(oDrd)
                    .KgBrut = SQLHelper.GetIntegerFromDataReader(oDrd("Kg"))
                    .KgNet = oDrd("KgNet")
                    .NoDimensions = oDrd("NoDimensions")
                    .DimensionL = SQLHelper.GetDecimalFromDataReader(oDrd("DimensionL"))
                    .DimensionW = SQLHelper.GetDecimalFromDataReader(oDrd("DimensionW"))
                    .DimensionH = SQLHelper.GetDecimalFromDataReader(oDrd("DimensionH"))
                    .InnerPack = oDrd("InnerPack")
                    .OuterPack = oDrd("OuterPack")
                    .ForzarInnerPack = oDrd("ForzarInnerPack")
                    .IsBundle = oDrd("IsBundle")

                    If Not IsDBNull(oDrd("CnapGuid")) Then
                        .Cnap = New DTOCnap(oDrd("CnapGuid"))
                        With .Cnap
                            .Id = SQLHelper.GetStringFromDataReader(oDrd("CnapId"))
                            .NomShort = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapShortEsp", "CnapShortEsp", "CnapShortEsp", "CnapShortEsp")
                            .NomLong = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapLongEsp", "CnapLongEsp", "CnapLongEsp", "CnapLongEsp")
                        End With
                    End If

                    If Not IsDBNull(oDrd("LaunchYear")) Then
                        .Launchment = New DTOYearMonth(CInt(oDrd("LaunchYear")), CInt(oDrd("LaunchMonth")))
                    End If

                    .HideUntil = SQLHelper.GetFchFromDataReader(oDrd("HideUntil"))

                    If Not IsDBNull(oDrd("MadeIn")) Then
                        .MadeIn = SQLHelper.GetCountryFromDataReader(oDrd, "MadeIn", "StpMadeInEng", "StpMadeInEng", "StpMadeInEng", "StpMadeInEng", "StpMadeInISO")
                    End If

                    If Not IsDBNull(oDrd("CodiMercancia")) Then
                        .CodiMercancia = New DTOCodiMercancia(oDrd("CodiMercancia"))
                        .CodiMercancia.Dsc = SQLHelper.GetStringFromDataReader(oDrd("CodiMercanciaDsc"))
                    End If

                    .EnabledxConsumer = oDrd("Web_Enabled_Consumer")
                    .EnabledxPro = oDrd("Web_Enabled_Pro")

                    .NoStk = oDrd("NoStk")
                    If Not IsDBNull(oDrd("LaunchYear")) Then
                        .Launchment = New DTOYearMonth(CInt(oDrd("LaunchYear")), CInt(oDrd("LaunchMonth")))
                    End If

                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .obsoleto = oDrd("Obsoleto")
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
        End If

        oCategory.UrlSegments = UrlSegmentsLoader.All(oCategory)
        ' oCategory.Urls = ProductLoader.Urls(oCategory)

        Dim retval As Boolean = oCategory.IsLoaded
        Return retval
    End Function

    Shared Function FromNom(oBrand As DTOProductBrand, sNom As String) As DTOProductCategory
        Dim retval As DTOProductCategory = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Stp.Guid ")
        sb.AppendLine(", VwProductText.NomEsp, VwProductText.NomCat, VwProductText.NomEng, VwProductText.NomPor ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("INNER JOIN VwProductText ON Stp.Guid = VwProductText.Guid ")
        sb.AppendLine("INNER JOIN UrlSegment ON Stp.Guid = UrlSegment.Target ")
        sb.AppendLine("WHERE Stp.Brand = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("AND UrlSegment.Segment = '" & sNom & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@DecodedNom", UrlHelper.DecodedUrlSegment(sNom))
        If oDrd.Read Then
            retval = New DTOProductCategory(oDrd("Guid"))
            retval.Brand = oBrand
            SQLHelper.LoadLangTextFromDataReader(retval.Nom, oDrd, "NomEsp", "NomCat", "NomEng", "NomPor")
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function Thumbnail(oCategory As DTOProductCategory) As Byte() 'To deprecate
        Dim retval As Byte() = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Thumbnail ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("WHERE Guid = '" & oCategory.Guid.ToString & "' ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("Thumbnail")
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Image(oCategoryGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Image ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("WHERE Guid = '" & oCategoryGuid.ToString & "' ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New ImageMime()
            retval.ByteArray = oDrd("Image")
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Update(oCategory As DTOProductCategory, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oCategory, oTrans)
            LangTextLoader.Update(oCategory.Nom, oTrans)
            LangTextLoader.Update(oCategory.SeoTitle, oTrans)
            LangTextLoader.Update(oCategory.Excerpt, oTrans)
            LangTextLoader.Update(oCategory.Content, oTrans)
            UrlSegmentsLoader.Update(oCategory, oCategory.UrlSegments, oTrans)
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

    Shared Sub Update(oCategory As DTOProductCategory, ByRef oTrans As SqlTransaction)
        If oCategory.IsNew Then oCategory.Ord = LastOrd(oCategory.Brand, oTrans) + 1

        Dim SQL As String = "SELECT * FROM Stp WHERE Guid = '" & oCategory.Guid.ToString() & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oCategory.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        'If oCategory.Id = 0 Then oCategory.Id = LastId(oCategory.Brand, oTrans) + 1

        With oCategory
            oRow("Brand") = .Brand.Guid
            oRow("Codi") = .Codi
            oRow("Dsc_PropagateToChildren") = .Dsc_PropagateToChildren
            oRow("Web_Enabled_Consumer") = .EnabledxConsumer
            oRow("Web_Enabled_Pro") = .EnabledxPro
            oRow("Ord") = .Ord
            oRow("MadeIn") = SQLHelper.NullableBaseGuid(.MadeIn)

            oRow("Kg") = .KgBrut
            oRow("KgNet") = .KgNet
            oRow("NoDimensions") = .NoDimensions
            oRow("DimensionL") = SQLHelper.NullableInt(.DimensionL)
            oRow("DimensionW") = SQLHelper.NullableInt(.DimensionW)
            oRow("DimensionH") = SQLHelper.NullableInt(.DimensionH)
            oRow("InnerPack") = .InnerPack
            oRow("OuterPack") = .OuterPack
            oRow("ForzarInnerPack") = .ForzarInnerPack

            oRow("CnapGuid") = SQLHelper.NullableBaseGuid(.Cnap)
            oRow("MadeIn") = SQLHelper.NullableBaseGuid(.MadeIn)
            oRow("NoStk") = .NoStk
            oRow("Obsoleto") = .obsoleto

            If .Image IsNot Nothing Then
                oRow("Image") = .Image
                Dim oImage As System.Drawing.Image = Nothing
                Dim ms = New IO.MemoryStream(.Image)
                oImage = System.Drawing.Image.FromStream(ms)
                Dim oThumbnail = LegacyHelper.ImageHelper.GetThumbnailToFill(oImage, DTOProductCategory.THUMBNAILWIDTH, DTOProductCategory.THUMBNAILHEIGHT)
                oRow("Thumbnail") = LegacyHelper.ImageHelper.Converter(oThumbnail)
            End If

            If .Launchment IsNot Nothing Then
                oRow("LaunchYear") = .Launchment.Year
                oRow("LaunchMonth") = .Launchment.Month
            End If

            oRow("HideUntil") = SQLHelper.NullableFch(.HideUntil)
            oRow("IsBundle") = .IsBundle
            If .CodiMercancia Is Nothing Then
                oRow("CodiMercancia") = System.DBNull.Value
            Else
                oRow("CodiMercancia") = .CodiMercancia.Id
            End If
        End With

        oDA.Update(oDs)
    End Sub


    Shared Function Delete(oCategory As DTOProductCategory, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            LangTextLoader.Delete(oCategory, oTrans)
            UrlSegmentsLoader.Delete(oCategory, oTrans)
            Delete(oCategory, oTrans)
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


    Shared Sub Delete(oCategory As DTOProductCategory, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Stp WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oCategory.Guid.ToString())
    End Sub

    Shared Function LastOrd(oBrand As DTOProductBrand, ByRef oTrans As SqlTransaction) As Integer
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Max(Ord) AS LastOrd ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("WHERE Brand='" & oBrand.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        Dim retval As Integer
        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastOrd")) Then
                retval = CInt(oRow("LastOrd"))
            End If
        End If
        Return retval
    End Function

    Shared Function SortSkus(exs As List(Of Exception), oCategoryGuid As Guid, oDict As Dictionary(Of Guid, Integer))
        Dim retval As Boolean
        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim SQL = String.Format("UPDATE Art SET Ord = 0 WHERE Category = '{0}'", oCategoryGuid.ToString())
            SQLHelper.ExecuteNonQuery(SQL, oTrans)
            For Each key In oDict.Keys
                Dim idx As Integer = oDict(key)
                SQL = String.Format("UPDATE Art SET Ord = {0} WHERE Guid = '{1}'", idx, key.ToString())
                SQLHelper.ExecuteNonQuery(SQL, oTrans)
            Next
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

End Class

Public Class ProductCategoriesLoader

    Shared Function All(oEmp As DTOEmp) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Stp.Guid, Stp.Brand, Stp.Dsc_PropagateToChildren, Stp.Codi, Stp.CnapGuid, Stp.Ord ")
        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        'sb.AppendLine(", CategoryExcerpt.Esp AS CategoryExcerptEsp, CategoryExcerpt.Cat AS CategoryExcerptCat, CategoryExcerpt.Eng AS CategoryExcerptEng, CategoryExcerpt.Por AS CategoryExcerptPor ")
        sb.AppendLine(", Stp.Web_Enabled_Consumer, Stp.Web_Enabled_Pro, Stp.Obsoleto ")
        sb.AppendLine(", Stp.IsBundle, Stp.LaunchYear, Stp.LaunchMonth, Stp.HideUntil ")
        'sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
        'sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryExcerpt ON Stp.Guid = CategoryExcerpt.Guid AND CategoryExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")
        'sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Stp.Guid = Url.Guid ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Tpa.Ord, Tpa.Guid, Stp.Obsoleto, Stp.Codi, Stp.Ord, CategoryNom.Esp ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New DTOProductBrand
        Do While oDrd.Read
            If Not oDrd("Brand").Equals(oBrand.Guid) Then oBrand = New DTOProductBrand(oDrd("Brand"))
            Dim item As New DTOProductCategory(oDrd("Guid"))
            With item
                .Brand = oBrand
                .Ord = oDrd("Ord")
                .Dsc_PropagateToChildren = oDrd("Dsc_PropagateToChildren")
                '.Content = SQLHelper.GetLangTextFromDataReader(oDrd, "esp", "cat", "eng", "por")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                'SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "CategoryExcerptEsp", "CategoryExcerptCat", "CategoryExcerptEng", "CategoryExcerptPor")
                '.UrlCanonicas = SQLHelper.GetProductCategoryUrlCanonicasFromDataReader(oDrd)

                If Not IsDBNull(oDrd("LaunchYear")) Then
                    .Launchment = New DTOYearMonth(CInt(oDrd("LaunchYear")), CInt(oDrd("LaunchMonth")))
                End If
                .HideUntil = SQLHelper.GetFchFromDataReader(oDrd("HideUntil"))
                .Codi = oDrd("Codi")
                .EnabledxConsumer = oDrd("Web_Enabled_Consumer")
                .EnabledxPro = oDrd("Web_Enabled_Pro")
                If Not IsDBNull(oDrd("CnapGuid")) Then
                    .Cnap = New DTOCnap(oDrd("CnapGuid"))
                End If
                .IsBundle = oDrd("IsBundle")
                .obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oBrand As DTOProductBrand, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsolets As Boolean = False, Optional oSortOrder As DTOProductCategory.SortOrders = DTOProductCategory.SortOrders.Custom, Optional stockOnly As Boolean = False, Optional skipEmptyCategories As Boolean = False) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Stp.Guid, Stp.Dsc_PropagateToChildren, Stp.Codi, Stp.CnapGuid, Stp.Ord ")
        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine(", CategoryExcerpt.Esp AS CategoryExcerptEsp, CategoryExcerpt.Cat AS CategoryExcerptCat, CategoryExcerpt.Eng AS CategoryExcerptEng, CategoryExcerpt.Por AS CategoryExcerptPor ")
        sb.AppendLine(", Stp.Web_Enabled_Consumer, Stp.Web_Enabled_Pro, Stp.Obsoleto ")
        sb.AppendLine(", Stp.IsBundle, Stp.LaunchYear, Stp.LaunchMonth, Stp.HideUntil ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = " & DTOLangText.Srcs.ProductNom & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS CategoryExcerpt ON Stp.Guid = CategoryExcerpt.Guid AND CategoryExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Stp.Guid = Url.Guid ")

        If stockOnly And oMgz IsNot Nothing Then
            sb.AppendLine("INNER JOIN (SELECT Art.Category FROM Art INNER JOIN Arc ON Art.Guid=Arc.ArtGuid INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid WHERE Arc.MgzGuid='" & oMgz.Guid.ToString & "' GROUP BY Art.Category HAVING SUM(CASE WHEN Cod<50 THEN Qty ELSE -Qty END)>0) X ON Stp.Guid = X.Category ")
        End If

        If skipEmptyCategories Then
            sb.AppendLine("INNER JOIN (SELECT Category FROM Art WHERE Art.Obsoleto = 0 GROUP BY Category) Sku ON Sku.Category = Stp.Guid  ")
        End If

        sb.AppendLine("WHERE Stp.Brand='" & oBrand.Guid.ToString & "' ")
        If Not IncludeObsolets Then
            sb.AppendLine("AND Stp.Obsoleto=0 ")
        End If

        If oSortOrder = DTOProductCategory.SortOrders.Custom Then
            sb.AppendLine("ORDER BY Stp.Obsoleto, Stp.Codi, Stp.Ord, CategoryNom.Esp ")
        Else
            sb.AppendLine("ORDER BY Stp.Obsoleto, Stp.Codi, CategoryNom.Esp ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductCategory(oDrd("Guid"))
            With item
                .Brand = oBrand
                .Ord = oDrd("Ord")
                .Dsc_PropagateToChildren = oDrd("Dsc_PropagateToChildren")
                '.Content = SQLHelper.GetLangTextFromDataReader(oDrd, "esp", "cat", "eng", "por")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "CategoryExcerptEsp", "CategoryExcerptCat", "CategoryExcerptEng", "CategoryExcerptPor")
                .UrlCanonicas = SQLHelper.GetProductCategoryUrlCanonicasFromDataReader(oDrd)

                If Not IsDBNull(oDrd("LaunchYear")) Then
                    .Launchment = New DTOYearMonth(CInt(oDrd("LaunchYear")), CInt(oDrd("LaunchMonth")))
                End If
                .HideUntil = SQLHelper.GetFchFromDataReader(oDrd("HideUntil"))
                .Codi = oDrd("Codi")
                .EnabledxConsumer = oDrd("Web_Enabled_Consumer")
                '.EnabledxPro = oDrd("Web_Enabled_Pro")
                If Not IsDBNull(oDrd("CnapGuid")) Then
                    .Cnap = New DTOCnap(oDrd("CnapGuid"))
                End If
                .IsBundle = oDrd("IsBundle")
                .obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function AllWithFilterItems(oBrand As DTOProductBrand) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Stp.Guid, Stp.CnapGuid, Stp.Codi, Stp.Web_Enabled_Consumer, Y.FilterItem ")
        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEspEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine("FROM ")
        sb.AppendLine("(SELECT X.Category, FilterItem ")
        sb.AppendLine("FROM ")
        sb.AppendLine("(SELECT Stp.Brand, Stp.Guid AS Category, NULL AS FilterItem ")
        sb.AppendLine("FROM Stp ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT Stp.brand, Stp.Guid AS Category, FilterTarget.FilterItem ")
        sb.AppendLine("FROM FilterTarget ")
        sb.AppendLine("INNER JOIN Stp ON FilterTarget.Target=Stp.Guid ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT Stp.Brand, Stp.Guid, FilterTarget.FilterItem ")
        sb.AppendLine("FROM FilterTarget ")
        sb.AppendLine("INNER JOIN Art ON FilterTarget.Target = Art.Guid ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category=Stp.Guid) X ")
        sb.AppendLine("WHERE X.Brand = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY X.Category, X.FilterItem) Y ")
        sb.AppendLine("INNER JOIN Stp ON Stp.Guid = Y.Category ")
        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        'sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical ON Stp.Guid = VwProductUrlCanonical.Guid ") (ralenteix molt)
        sb.AppendLine("WHERE Stp.Obsoleto = 0 AND Stp.Codi < " & DTOProductCategory.Codis.accessories & " ")
        sb.AppendLine("ORDER BY Stp.Ord, Stp.Guid ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oCategory As New DTOProductCategory
        Do While oDrd.Read
            If Not oCategory.Guid.Equals(oDrd("Guid")) Then
                oCategory = New DTOProductCategory(oDrd("Guid"))
                With oCategory
                    If Not IsDBNull(oDrd("CnapGuid")) Then
                        .Cnap = New DTOCnap(oDrd("CnapGuid"))
                    End If
                    SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    .Codi = SQLHelper.GetIntegerFromDataReader(oDrd("Codi"))
                    .EnabledxConsumer = oDrd("Web_Enabled_Consumer")
                    .FilterItems = New DTOFilter.Item.Collection
                End With
                retval.Add(oCategory)
            End If
            If Not IsDBNull(oDrd("FilterItem")) Then
                Dim item As New DTOFilter.Item(oDrd("FilterItem"))
                oCategory.FilterItems.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CompactTree_Old(oEmp As DTOEmp, oLang As DTOLang) As DTOCatalog
        Dim retval As New DTOCatalog
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwCategoryNom.BrandGuid, VwCategoryNom.BrandNom, VwCategoryNom.BrandObsoleto ")
        sb.AppendLine(", VwCategoryNom.CategoryGuid, VwCategoryNom.CategoryNom, VwCategoryNom.CategoryNomCat, VwCategoryNom.CategoryNomEng, VwCategoryNom.CategoryNomPor ")
        sb.AppendLine(", VwCategoryNom.CategoryIsBundle, VwCategoryNom.Obsoleto ")
        sb.AppendLine("FROM VwCategoryNom ")
        sb.AppendLine("WHERE VwCategoryNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY VwCategoryNom.BrandObsoleto, VwCategoryNom.BrandOrd, VwCategoryNom.BrandNom")
        sb.AppendLine(", VwCategoryNom.Obsoleto, VwCategoryNom.Codi, VwCategoryNom.CategoryOrd, VwCategoryNom.CategoryNom  ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New DTOCatalog.Brand()
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOCatalog.Brand(oDrd("BrandGuid"), oDrd("BrandNom"))
                With oBrand
                    .Nom = oDrd("BrandNom")
                    .Obsoleto = oDrd("BrandObsoleto")
                End With
                retval.Add(oBrand)
            End If
            If Not IsDBNull(oDrd("CategoryGuid")) Then
                Dim oNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                Dim oCategory As New DTOCatalog.Category(oDrd("CategoryGuid"), oNom.Tradueix(oLang))
                oCategory.Obsoleto = oDrd("Obsoleto")
                oCategory.IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("CategoryIsBundle"))
                oBrand.Categories.Add(oCategory)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CompactTree(oEmp As DTOEmp, oLang As DTOLang) As DTOCatalog
        Dim retval As New DTOCatalog
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwBrandCategories.BrandGuid, VwBrandCategories.BrandNom, VwBrandCategories.BrandObsoleto ")
        sb.AppendLine(", VwBrandCategories.CategoryGuid, VwBrandCategories.CategoryNom, VwBrandCategories.CategoryNomCat, VwBrandCategories.CategoryNomEng, VwBrandCategories.CategoryNomPor ")
        sb.AppendLine(", VwBrandCategories.CategoryIsBundle, VwBrandCategories.Obsoleto ")
        sb.AppendLine("FROM VwBrandCategories ")
        sb.AppendLine("WHERE VwBrandCategories.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY VwBrandCategories.BrandObsoleto, VwBrandCategories.BrandOrd, VwBrandCategories.BrandNom")
        sb.AppendLine(", VwBrandCategories.Obsoleto, VwBrandCategories.Codi, VwBrandCategories.CategoryOrd, VwBrandCategories.CategoryNom  ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New DTOCatalog.Brand()
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOCatalog.Brand(oDrd("BrandGuid"), SQLHelper.GetStringFromDataReader(oDrd("BrandNom")))
                With oBrand
                    .Obsoleto = oDrd("BrandObsoleto")
                End With
                retval.Add(oBrand)
            End If
            If Not IsDBNull(oDrd("CategoryGuid")) Then
                'If oDrd("CategoryGuid").ToString() = "426430dd-0c90-48d0-87ea-12f9a2ecdd38" Then Stop
                Dim oNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                Dim oCategory As New DTOCatalog.Category(oDrd("CategoryGuid"), oNom.Tradueix(oLang))
                oCategory.Obsoleto = oDrd("Obsoleto")
                oCategory.IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("CategoryIsBundle"))
                oBrand.Categories.Add(oCategory)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function FromCustomerOrders(oContact As DTOContact, oBrand As DTOProductBrand) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Stp.Guid, Stp.Obsoleto, Stp.Ord, Web_Enabled_Consumer ")
        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN Art ON Pnc.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category = Stp.Guid ")
        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        sb.AppendLine("WHERE Pdc.CliGuid = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Stp.Brand = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Stp.Guid, Stp.Obsoleto, Stp.Ord, Web_Enabled_Consumer ")
        sb.AppendLine(", CategoryNom.Esp, CategoryNom.Cat, CategoryNom.Eng, CategoryNom.Por ")
        sb.AppendLine("ORDER BY Stp.Obsoleto, Stp.Ord, CategoryNom.Esp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductCategory(oDrd("Guid"))
            With item
                .Brand = oBrand
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                .Ord = oDrd("Ord")
                .EnabledxConsumer = oDrd("Web_Enabled_Consumer")
                .obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Move(oValues As List(Of DTOProductCategory), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            Dim sb As New Text.StringBuilder
            sb.AppendLine("SELECT Stp.Guid, Stp.Ord ")
            sb.AppendLine("FROM Stp ")
            sb.AppendLine("WHERE (")
            For Each item As DTOProductCategory In oValues
                If oValues.IndexOf(item) > 0 Then sb.Append(" OR ")
                sb.Append("Stp.Guid = '" & item.Guid.ToString & "' ")
            Next
            sb.AppendLine(") ")

            Dim SQL As String = sb.ToString
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)

            For Each oRow As DataRow In oTb.Rows
                Dim oRowGuid As Guid = oRow("Guid")
                Dim item As DTOProductCategory = oValues.Where(Function(x) x.Guid.Equals(oRowGuid)).First
                oRow("Ord") = item.Ord
            Next

            oDA.Update(oDs)
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


    Shared Function CreateThumbnails() As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction

        Dim SQL As String = "SELECT Guid, Image, Thumbnail FROM Stp WHERE Image IS NOT NULL"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        For Each oRow In oTb.Rows
            Dim imgBytes As Byte() = oRow("Image")
            Dim ms = New IO.MemoryStream(imgBytes)
            Dim oImage = System.Drawing.Image.FromStream(ms)
            Dim oThumbnail = LegacyHelper.ImageHelper.GetThumbnailToFit(oImage, DTOProductCategory.THUMBNAILWIDTH, DTOProductCategory.THUMBNAILHEIGHT)
            oRow("Thumbnail") = LegacyHelper.ImageHelper.Converter(oThumbnail)
        Next

        oDA.Update(oDs)

        oTrans.Commit()
        retval = True
        'oTrans.Rollback()
        oConn.Close()

        Return retval
    End Function


End Class
