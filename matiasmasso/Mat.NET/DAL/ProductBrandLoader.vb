Public Class ProductBrandLoader

    Shared Function Find(oGuid As Guid) As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing
        Dim oBrand As New DTOProductBrand(oGuid)
        oBrand.SourceCod = DTOProduct.SourceCods.Brand
        If Load(oBrand) Then
            retval = oBrand
        End If
        Return retval
    End Function

    Shared Function FromNom(oEmp As DTOEmp, sNom As String) As DTOProductBrand
        Dim retval As DTOProductBrand = Nothing

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid, VwProductNom.BrandNom, Tpa.Proveidor, Tpa.CodDist, Tpa.ShowAtlas, Tpa.EnLiquidacio , Tpa.Obsoleto ")
        sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN UrlSegment ON Tpa.Guid = UrlSegment.Target ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical Url ON Tpa.Guid = Url.Guid ")
        sb.AppendLine("WHERE Tpa.Emp =" & oEmp.Id & " ")
        sb.AppendLine("AND UrlSegment.Segment = '" & sNom & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOProductBrand(oDrd("Guid"))
            With retval
                .emp = oEmp
                SQLHelper.LoadLangTextFromDataReader(.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                If Not IsDBNull(oDrd("Proveidor")) Then
                    .proveidor = New DTOProveidor(oDrd("Proveidor"))
                    .proveidor.emp = oEmp
                End If
                .CodDist = oDrd("CodDist")
                .ShowAtlas = oDrd("ShowAtlas")
                .UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                .EnLiquidacio = oDrd("EnLiquidacio")
                .obsoleto = oDrd("Obsoleto")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oBrand As DTOProductBrand) As Boolean
        If Not oBrand.IsLoaded And Not oBrand.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Tpa.*, BrandNom.Esp AS BrandNomEsp, BrandNom.Cat AS BrandNomCat, BrandNom.Eng AS BrandNomEng, BrandNom.Por AS BrandNomPor, CliGral.FullNom  ") ', PremiumLine.Nom AS PremiumLineNom ")
            sb.AppendLine(", BrandExcerpt.Esp AS BrandExcerptEsp, BrandExcerpt.Cat AS BrandExcerptCat, BrandExcerpt.Eng AS BrandExcerptEng, BrandExcerpt.Por AS BrandExcerptPor ")
            sb.AppendLine(", BrandContent.Esp AS BrandContentEsp, BrandContent.Cat AS BrandContentCat, BrandContent.Eng AS BrandContentEng, BrandContent.Por AS BrandContentPor  ")
            sb.AppendLine(", Country.ISO AS MadeInISO, Country.Nom_Esp AS MadeInEsp, Country.Nom_Cat AS MadeInCat, Country.Nom_Eng AS MadeInEng, Country.Nom_Por AS MadeInPor ")
            sb.AppendLine(", CodisMercancia.Dsc AS CodiMercanciaDsc ")
            sb.AppendLine(", VwCnap.Id AS CnapId, VwCnap.ShortNomEsp AS CnapShortEsp, VwCnap.LongNomEsp AS CnapLongESP ")
            sb.AppendLine(", SeoTitle.Esp AS SeoTitleEsp, SeoTitle.Cat AS SeoTitleCat, SeoTitle.Eng AS SeoTitleEng, SeoTitle.Por AS SeoTitlePor ")
            sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor ")
            sb.AppendLine("FROM Tpa ")
            sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText SeoTitle ON Tpa.Guid = SeoTitle.Guid AND SeoTitle.Src = " & DTOLangText.Srcs.SeoTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS BrandExcerpt ON Tpa.Guid = BrandExcerpt.Guid AND BrandExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS BrandContent ON Tpa.Guid = BrandContent.Guid AND BrandContent.Src = " & DTOLangText.Srcs.ProductText & " ")
            sb.AppendLine("LEFT OUTER JOIN CliGral ON Tpa.Proveidor = CliGral.Guid ")
            sb.AppendLine("LEFT OUTER JOIN Country ON Tpa.MadeIn=Country.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwCnap ON Tpa.CnapGuid=VwCnap.Guid ")
            sb.AppendLine("LEFT OUTER JOIN CodisMercancia ON Tpa.CodiMercancia=CodisMercancia.Id ")
            sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Tpa.Guid = Url.Guid ")
            sb.AppendLine("WHERE Tpa.Guid=@Guid")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Guid", oBrand.Guid.ToString())
            If oDrd.Read Then
                With oBrand
                    .emp = New DTOEmp(oDrd("Emp"))
                    SQLHelper.LoadLangTextFromDataReader(.SeoTitle, oDrd, "SeoTitleEsp", "SeoTitleCat", "SeoTitleEng", "SeoTitlePor")
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "BrandNomEsp", "BrandNomCat", "BrandNomEng", "BrandNomPor")
                    SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "BrandExcerptEsp", "BrandExcerptCat", "BrandExcerptEng", "BrandExcerptPor")
                    SQLHelper.LoadLangTextFromDataReader(.Content, oDrd, "BrandContentEsp", "BrandContentCat", "BrandContentEng", "BrandContentPor")
                    .UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)

                    If Not IsDBNull(oDrd("Proveidor")) Then
                        .Proveidor = New DTOProveidor(oDrd("Proveidor"))
                        With .Proveidor
                            .Emp = oBrand.Emp
                            .FullNom = oDrd("FullNom")
                        End With
                    End If

                    .CodDist = oDrd("CodDist")

                    If Not IsDBNull(oDrd("MadeIn")) Then
                        .MadeIn = New DTOCountry(oDrd("MadeIn"))
                        With .MadeIn
                            .ISO = oDrd("MadeInISO")
                            .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "MadeInEsp", "MadeInCat", "MadeInEng", "MadeInPor")
                        End With
                    End If

                    If Not IsDBNull(oDrd("CnapGuid")) Then
                        .Cnap = New DTOCnap(oDrd("CnapGuid"))
                        With .Cnap
                            .Id = SQLHelper.GetStringFromDataReader(oDrd("CnapId"))
                            .NomShort = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapShortEsp", "CnapShortEsp", "CnapShortEsp", "CnapShortEsp")
                            .NomLong = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapLongEsp", "CnapLongEsp", "CnapLongEsp", "CnapLongEsp")
                        End With
                    End If

                    If Not IsDBNull(oDrd("CodiMercancia")) Then
                        .CodiMercancia = New DTOCodiMercancia(oDrd("CodiMercancia"))
                        .CodiMercancia.Dsc = SQLHelper.GetStringFromDataReader(oDrd("CodiMercanciaDsc"))
                    End If

                    .ShowAtlas = oDrd("ShowAtlas")
                    .WebAtlasDeadline = SQLHelper.GetIntegerFromDataReader(oDrd("WebAtlasDeadline"))
                    .WebAtlasRafflesDeadline = SQLHelper.GetIntegerFromDataReader(oDrd("WebAtlasRafflesDeadline"))
                    .EnabledxConsumer = oDrd("Web_Enabled_Consumer")
                    .EnabledxPro = oDrd("Web_Enabled_Pro")
                    .EnLiquidacio = oDrd("EnLiquidacio")
                    .Obsoleto = oDrd("Obsoleto")
                    .IsLoaded = True
                End With

            End If

            oDrd.Close()
        End If

        oBrand.UrlSegments = UrlSegmentsLoader.All(oBrand)
        'oBrand.Urls = ProductLoader.Urls(oBrand)

        Dim retval As Boolean = oBrand.IsLoaded
        Return retval
    End Function

    Shared Function Logo(oBrand As DTOProductBrand) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Logo ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("WHERE Tpa.Guid='" & oBrand.Guid.ToString() & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Logo")) Then
                retval = MatHelperStd.ImageMime.Factory(oDrd("Logo"), MimeCods.Jpg)
            End If
        End If

        oDrd.Close()
        Return retval
    End Function

    Shared Function LogoDistribuidorOficial(oBrand As DTOProductBrand) As Byte()
        Dim retval As Byte() = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.LogoDistribuidorOficial ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("WHERE Tpa.Guid='" & oBrand.Guid.ToString() & "' ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = oDrd("LogoDistribuidorOficial")
        End If

        oDrd.Close()
        Return retval
    End Function


    Shared Function Update(oBrand As DTOProductBrand, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oBrand, oTrans)
            With oBrand
                LangTextLoader.Update(.Nom, oTrans)
                LangTextLoader.Update(.SeoTitle, oTrans)
                LangTextLoader.Update(.Excerpt, oTrans)
                LangTextLoader.Update(.Content, oTrans)
                UrlSegmentsLoader.Update(oBrand, .UrlSegments, oTrans)
            End With
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

    Shared Sub Update(oBrand As DTOProductBrand, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "SELECT * FROM Tpa WHERE Guid=@Guid"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans, "@Guid", oBrand.Guid.ToString())
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oBrand.Guid
        Else
            oRow = oTb.Rows(0)
        End If

        With oBrand
            oRow("Emp") = .Emp.Id
            oRow("Proveidor") = SQLHelper.NullableBaseGuid(.proveidor)
            oRow("CodDist") = .CodDist

            oRow("MadeIn") = SQLHelper.NullableBaseGuid(.MadeIn)
            oRow("CnapGuid") = SQLHelper.NullableBaseGuid(.Cnap)
            oRow("ShowAtlas") = .ShowAtlas
            oRow("WebAtlasDeadline") = .WebAtlasDeadline
            oRow("WebAtlasRafflesDeadline") = .WebAtlasRafflesDeadline
            oRow("Web_Enabled_Consumer") = .EnabledxConsumer
            oRow("Web_Enabled_Pro") = .EnabledxPro

            If .CodiMercancia Is Nothing Then
                oRow("CodiMercancia") = System.DBNull.Value
            Else
                oRow("CodiMercancia") = .CodiMercancia.Id
            End If

            If .logo IsNot Nothing Then
                oRow("Logo") = .Logo
            End If

            oRow("EnLiquidacio") = .EnLiquidacio
            oRow("Obsoleto") = .Obsoleto
        End With

        oDA.Update(oDs)
    End Sub

    Shared Function LastId(oEmp As DTOEmp, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer
        Dim SQL As String = "SELECT TOP 1 Tpa AS LastId FROM Tpa " _
        & "WHERE Emp=" & oEmp.Id & " " _
        & "ORDER BY Tpa DESC"

        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)

        If oTb.Rows.Count > 0 Then
            Dim oRow As DataRow = oTb.Rows(0)
            If Not IsDBNull(oRow("LastId")) Then
                retval = CInt(oRow("LastId"))
            End If
        End If
        Return retval
    End Function

    Shared Function Delete(oBrand As DTOProductBrand, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            LangTextLoader.Delete(oBrand, oTrans)
            UrlSegmentsLoader.Delete(oBrand, oTrans)
            Delete(oBrand, oTrans)
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


    Shared Sub Delete(oBrand As DTOProductBrand, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Tpa WHERE Guid=@Guid"
        SQLHelper.ExecuteNonQuery(SQL, oTrans, "@Guid", oBrand.Guid.ToString())
    End Sub


End Class

Public Class ProductBrandsLoader

    Shared Function RoutingConstraints(oEmp As DTOEmp) As List(Of String)
        Dim retval As New List(Of String)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT X.BrandNom ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN ( ")
        sb.AppendLine("SELECT LangText.Guid, CAST(LangText.Text AS VARCHAR(MAX)) AS BrandNom")
        sb.AppendLine("FROM LangText ")
        sb.AppendLine("WHERE LangText.Src = 28 ")
        sb.AppendLine("UNION ")
        sb.AppendLine("SELECT UrlSegment.Target, UrlSegment.Segment ")
        sb.AppendLine("FROM UrlSegment ")
        sb.AppendLine(") X ON Tpa.Guid = X.Guid ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " AND Tpa.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY X.BrandNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("BrandNom"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Tree(oEmp As DTOEmp) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid AS BrandGuid, BrandNom.Esp AS BrandNom, Tpa.Obsoleto AS BrandObsoleto")
        sb.AppendLine(", Stp.Guid AS CategoryGuid, CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine(", BrandUrl.UrlBrandEsp, BrandUrl.UrlBrandCat, BrandUrl.UrlBrandEng, BrandUrl.UrlBrandPor ")
        sb.AppendLine(", CategoryUrl.IncludeDeptOnUrl, CategoryUrl.UrlDeptEsp, CategoryUrl.UrlDeptCat, CategoryUrl.UrlDeptEng, CategoryUrl.UrlDeptPor, CategoryUrl.UrlCategoryEsp, CategoryUrl.UrlCategoryCat, CategoryUrl.UrlCategoryEng, CategoryUrl.UrlCategoryPor ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("LEFT OUTER JOIN Stp ON Tpa.Guid = Stp.Brand ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS BrandUrl ON Tpa.Guid = BrandUrl.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS CategoryUrl ON Stp.Guid = CategoryUrl.Guid ")
        sb.AppendLine("WHERE Tpa.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Tpa.Obsoleto, Tpa.Ord, BrandNom.Esp, Tpa.Guid, Stp.Obsoleto, Stp.Codi, Stp.Ord, CategoryNom.Esp")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New DTOProductBrand
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                With oBrand
                    SQLHelper.LoadLangTextFromDataReader(.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                    .UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                    .obsoleto = oDrd("BrandObsoleto")
                End With
                retval.Add(oBrand)
            End If
            If Not IsDBNull(oDrd("CategoryGuid")) Then
                Dim oCategory As New DTOProductCategory(oDrd("CategoryGuid"))
                With oCategory
                    SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    .UrlCanonicas = SQLHelper.GetProductCategoryUrlCanonicasFromDataReader(oDrd)
                    .obsoleto = oDrd("CategoryObsoleto")
                End With
                oBrand.Categories.Add(oCategory)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oEmp As DTOEmp, IncludeObsolets As Boolean) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid, BrandNom.Esp AS BrandNomEsp, Tpa.Proveidor, Tpa.CodDist, Tpa.Obsoleto, Tpa.Web_Enabled_Consumer, CliGral.FullNom ")
        sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Tpa.Guid = Url.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Tpa.Proveidor = CliGral.Guid ")
        sb.AppendLine("WHERE Tpa.Emp=" & oEmp.Id & " ")
        If Not IncludeObsolets Then
            sb.AppendLine("AND Tpa.Obsoleto=0")
        End If
        sb.AppendLine("ORDER BY Tpa.Obsoleto, BrandNomEsp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductBrand(oDrd("Guid"))
            With item
                .emp = oEmp
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "BrandNomEsp")
                If Not IsDBNull(oDrd("Proveidor")) Then
                    .proveidor = New DTOProveidor(oDrd("Proveidor"))
                    With .proveidor
                        .emp = oEmp
                        .FullNom = oDrd("FullNom")
                    End With
                End If
                .UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                .codDist = oDrd("CodDist")
                .enabledxConsumer = oDrd("Web_Enabled_Consumer")
                .Categories = New List(Of DTOProductCategory)
                .obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromProveidor(oUser As DTOUser, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid, Tpa.Proveidor, Tpa.CodDist, Tpa.Obsoleto, Tpa.Web_Enabled_Consumer, CliGral.FullNom ")
        sb.AppendLine(", BrandNom.Esp AS BrandNomEsp, BrandNom.Cat AS BrandNomCat, BrandNom.Eng AS BrandNomEng, BrandNom.Por AS BrandNomPor ")
        sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Tpa.Proveidor = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Tpa.Guid = Url.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & oUser.Guid.ToString & "' ")
        If Not IncludeObsolets Then
            sb.AppendLine("AND Tpa.Obsoleto=0")
        End If
        sb.AppendLine("ORDER BY Tpa.Obsoleto, BrandNom.Esp")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductBrand(oDrd("Guid"))
            With item
                If Not IsDBNull(oDrd("Proveidor")) Then
                    .Proveidor = New DTOProveidor(oDrd("Proveidor"))
                    With .Proveidor
                        .FullNom = oDrd("FullNom")
                    End With
                End If
                .Emp = oUser.Emp
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "BrandNomEsp", "BrandNomCat", "BrandNomEng", "BrandNomPor")
                .UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                .codDist = oDrd("CodDist")
                .enabledxConsumer = oDrd("Web_Enabled_Consumer")
                .Categories = New List(Of DTOProductCategory)
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromProveidor(oProveidor As DTOProveidor, Optional IncludeObsolets As Boolean = False) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid, Tpa.CodDist, Tpa.Obsoleto, Tpa.Web_Enabled_Consumer ")
        sb.AppendLine(", BrandNom.Esp AS BrandNomEsp, BrandNom.Cat AS BrandNomCat, BrandNom.Eng AS BrandNomEng, BrandNom.Por AS BrandNomPor ")
        sb.AppendLine(", Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Tpa.Guid = Url.Guid ")
        sb.AppendLine("WHERE Tpa.Proveidor ='" & oProveidor.Guid.ToString & "' ")
        If Not IncludeObsolets Then
            sb.AppendLine("AND Tpa.Obsoleto=0")
        End If
        sb.AppendLine("ORDER BY Tpa.Obsoleto, BrandNom.Esp")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductBrand(oDrd("Guid"))
            With item
                .Emp = oProveidor.Emp
                .Proveidor = oProveidor
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "BrandNomEsp", "BrandNomCat", "BrandNomEng", "BrandNomPor")
                .UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                .codDist = oDrd("CodDist")
                .enabledxConsumer = oDrd("Web_Enabled_Consumer")
                .Categories = New List(Of DTOProductCategory)
                .Obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function OfficiallyDistributed(oRep As DTORep) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT VwProductNom.BrandGuid, VwProductNom.BrandNom, Tpa.Web_Enabled_Consumer ")
        sb.AppendLine(", Url.UrlBrandEsp ")
        sb.AppendLine("FROM VwProductNom ")
        sb.AppendLine("INNER JOIN VwProductParent ON VwProductNom.BrandGuid=VwProductParent.Parent ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductNom.BrandGuid=Tpa.Guid ")
        sb.AppendLine("INNER JOIN RepProducts ON RepProducts.Product=VwProductParent.Child ")
        sb.AppendLine("INNER JOIN ProductChannel ON Tpa.Guid=ProductChannel.Product AND RepProducts.DistributionChannel = ProductChannel.DistributionChannel AND ProductChannel.Cod = 0 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Tpa.Guid = Url.Guid ")
        sb.AppendLine("WHERE RepProducts.Rep='" & oRep.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=getdate()) ")
        sb.AppendLine("AND (RepProducts.Cod = 1) ")
        'sb.AppendLine("AND Tpa.CodDist=1 ")
        sb.AppendLine("GROUP BY VwProductNom.BrandGuid, VwProductNom.BrandNom, Tpa.Web_Enabled_Consumer, Url.UrlBrandEsp ")
        sb.AppendLine("ORDER BY VwProductNom.BrandNom")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductBrand(oDrd("BrandGuid"))
            SQLHelper.LoadLangTextFromDataReader(item.nom, oDrd, "BrandNom")
            item.UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
            item.enabledxConsumer = oDrd("Web_Enabled_Consumer") 'important per web topnavbar
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromCustomerOrders(oContact As DTOContact) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Tpa.Guid, Tpa.Obsoleto, BrandNom.Esp AS BrandNom ")
        sb.AppendLine(", Url.UrlBrandEsp ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN Art ON Pnc.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category = Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Tpa.Guid = Url.Guid ")
        sb.AppendLine("WHERE Pdc.CliGuid = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Tpa.Guid, Tpa.Obsoleto, BrandNom.Esp, Tpa.Ord, Url.UrlBrandEsp ")
        sb.AppendLine("ORDER BY Tpa.Obsoleto, Tpa.Ord, BrandNom.Esp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductBrand(oDrd("Guid"))
            SQLHelper.LoadLangTextFromDataReader(item.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
            item.UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
            item.obsoleto = oDrd("Obsoleto")
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromCustomer(oEmp As DTOEmp, oUser As DTOUser) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandNom, VwCustomerSkus.EnabledxConsumer ")
        sb.AppendLine(", Url.UrlBrandEsp ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer = Email_Clis.ContactGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON VwCustomerSkus.BrandGuid = Url.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.Emp =" & oEmp.Id & " ")
        sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandOrd, VwCustomerSkus.BrandNom, VwCustomerSkus.EnabledxConsumer ")
        sb.AppendLine(", Url.UrlBrandEsp ")
        sb.AppendLine("ORDER BY VwCustomerSkus.BrandOrd, VwCustomerSkus.BrandNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductBrand(oDrd("BrandGuid"))
            With item
                .Emp = oEmp
                SQLHelper.LoadLangTextFromDataReader(.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                item.UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                .enabledxConsumer = oDrd("EnabledxConsumer")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromCustomer(oContact As DTOContact) As List(Of DTOProductBrand)
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandNom, VwCustomerSkus.EnabledxConsumer ")
        sb.AppendLine(", Url.UrlBrandEsp ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON VwCustomerSkus.BrandGuid = Url.Guid ")
        sb.AppendLine("WHERE VwCustomerSkus.Customer = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandOrd, VwCustomerSkus.BrandNom, VwCustomerSkus.EnabledxConsumer ")
        sb.AppendLine(", Url.UrlBrandEsp ")
        sb.AppendLine("ORDER BY VwCustomerSkus.BrandOrd, VwCustomerSkus.BrandNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductBrand(oDrd("BrandGuid"))
            With item
                SQLHelper.LoadLangTextFromDataReader(.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                item.UrlCanonicas = SQLHelper.GetProductBrandUrlCanonicasFromDataReader(oDrd)
                .enabledxConsumer = oDrd("EnabledxConsumer")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
