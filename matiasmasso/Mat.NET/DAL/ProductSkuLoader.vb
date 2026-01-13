Imports Newtonsoft.Json.Linq

Public Class ProductSkuLoader

    Shared Function Find(oGuid As Guid, oMgz As DTOMgz) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim oSku As New DTOProductSku(oGuid)
        If Load(oSku, oMgz) Then
            retval = oSku
        End If
        Return retval
    End Function

    Shared Function FromId(oEmp As DTOEmp, id As Integer) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Emp=" & oEmp.Id & " AND VwSkuNom.SkuId =" & id & " ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetProductFromDataReader(oDrd)
        End If
        oDrd.Close()

        Return retval
    End Function

    Shared Function SearchById(oCustomer As DTOCustomer, skuId As Integer, oMgz As DTOMgz) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.SkuNomLlarg, VwSkuNom.LastProduction, VwSkuNom.Obsoleto ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
        sb.AppendLine(", VwSkuNom.CategoryMoq, VwSkuNom.CategoryForzarMoq ")
        sb.AppendLine(", VwSkuNom.SkuMoq, VwSkuNom.SkuForzarMoq ")
        sb.AppendLine(", VwRetail.Retail ")
        sb.AppendLine(", VwCustomerSkusLite.ExclusionCod ")
        sb.AppendLine(", CliDto.Dto AS Dto ")
        sb.AppendLine(", VwSkuNom.SkuMoq, VwSkuNom.IsBundle ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine(", VwCustomerSkusLite.Ccx ")
        sb.AppendLine("FROM VwCustomerSkusLite ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwCustomerSkusLite.SkuGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON VwSkuNom.SkuGuid = VwRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid And VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString() & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN CliDto On VwCustomerSkusLite.Ccx = CliDto.Customer And (VwSkuNom.BrandGuid = CliDto.Brand OR VwSkuNom.CategoryGuid = CliDto.Brand OR VwSkuNom.SkuGuid = CliDto.Brand) ")
        sb.AppendLine("WHERE VwCustomerSkusLite.Customer = '" & oCustomer.Guid.ToString() & "' ")
        sb.AppendLine("AND VwSkuNom.SkuId=" & skuId.ToString() & " ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetProductFromDataReader(oDrd)
            With retval
                .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                If Not IsDBNull(oDrd("Retail")) Then
                    .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                    '.price = .rrpp.deductPercent(retailOrChannelDto)
                End If
                .CustomerDto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .CodExclusio = SQLHelper.GetIntegerFromDataReader(oDrd("ExclusionCod"))
            End With
        End If
        oDrd.Close()


        Return retval
    End Function

    Shared Function FromEan(oEan As DTOEan) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim SQL As String = "SELECT Guid FROM Art WHERE (CBar ='" & oEan.Value & "' OR PackageEan = '" & oEan.Value & "')"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = New DTOProductSku(oDrd("Guid"))
            With retval
                .Ean13 = oEan
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromNom(oCategory As DTOProductCategory, sNom As String) As DTOProductSku
        Dim retval As DTOProductSku = Nothing

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid ")
        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("WHERE Art.Category='" & oCategory.Guid.ToString & "' ")
        sb.AppendLine("AND SkuNom.Esp COLLATE Latin1_General_CI_AI = @DecodedNom ") ' & sNom.Replace("_", " ") & "' ") 'Case insensitive
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@DecodedNom", MatHelperStd.UrlHelper.DecodedUrlSegment(sNom))
        If oDrd.Read Then
            retval = New DTOProductSku(oDrd("Guid"))
            With retval
                .Category = oCategory
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
            End With
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function Load(ByRef oSku As DTOProductSku, Optional oMgz As DTOMgz = Nothing) As Boolean
        If Not oSku.IsLoaded And Not oSku.IsNew Then

            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("SELECT Art.Art ")
            sb.AppendLine(", BrandNom.Esp AS BrandNom ")
            sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
            sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
            sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
            sb.AppendLine(", SeoTitle.Esp AS SeoTitleEsp, SeoTitle.Cat AS SeoTitleCat, SeoTitle.Eng AS SeoTitleEng, SeoTitle.Por AS SeoTitlePor ")
            sb.AppendLine(", LangExcerpt.Esp AS ExcerptEsp, LangExcerpt.Cat AS ExcerptCat, LangExcerpt.Eng AS ExcerptEng, LangExcerpt.Por AS ExcerptPor ")
            sb.AppendLine(", LangText.Esp AS TextEsp, LangText.Cat AS TextCat, LangText.Eng AS TextEng, LangText.Por AS TextPor  ")
            sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

            sb.AppendLine(", Art.IvaCod, Art.Hereda, Art.NoWeb, Art.NoEcom, Art.HideUntil, Art.MadeIn, Art.IsBundle, Art.bundleDto, Art.Obsoleto, Art.ObsoletoConfirmed ")
            sb.AppendLine(", (CASE WHEN Art.Image IS NULL THEN 0 ELSE 1 END) AS ImgExists, Art.Pmc, Art.LastProduction, Art.Availability ")
            sb.AppendLine(", Art.Outlet, Art.OutletDto, Art.OutletQty ")
            sb.AppendLine(", Art.HeredaDimensions, Art.InnerPack AS ArtInnerPack, Art.ForzarInnerPack AS ArtForzarInnerPack, Art.OuterPack AS ArtOuterPack ")
            sb.AppendLine(", Stp.InnerPack AS StpInnerPack, Stp.ForzarInnerPack AS StpForzarInnerPack, Stp.OuterPack AS StpOuterPack ")
            sb.AppendLine(", Art.NoDimensions AS ArtNoDimensions, Art.Kg AS ArtKgBrut, Art.KgNet AS ArtKgNet ")
            sb.AppendLine(", Art.DimensionL AS ArtDimensionL, Art.DimensionW AS ArtDimensionW, Art.DimensionH AS ArtDimensionH ")
            sb.AppendLine(", Art.Category, Stp.Brand, Tpa.Proveidor, Tpa.ShowAtlas ")
            sb.AppendLine(", Art.Ref, Art.RefPrv, Art.Cbar, Art.PackageEan ")
            sb.AppendLine(", Art.CnapGuid AS ArtCnap, Stp.CnapGuid AS StpCnap, VwCnap.Id AS CnapId, VwCnap.ShortNomEsp AS CnapNomShort, VwCnap.LongNomEsp AS CnapNomLong ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
            sb.AppendLine(", VwRetail.Retail, Art.SecurityStock, Art.NoStk, Art.NoPro, Art.Notarifa ")
            sb.AppendLine(", Country.ISO AS MadeInISO, Country.Nom_Esp AS MadeInEsp, Country.Nom_Cat AS MadeInCat, Country.Nom_Eng AS MadeInEng, Country.Nom_Por AS MadeInPor ")
            sb.AppendLine(", Art.CodiMercancia, CodisMercancia.Dsc AS CodiMercanciaDsc ")
            sb.AppendLine(", Art.FchCreated, Art.FchLastEdited ")
            sb.AppendLine(", Art.FchObsoleto, Art.Substitute, SkuSubstitute.CategoryGuid AS SubstituteCategoryGuid, SkuSubstitute.CategoryNom AS SubstituteCategoryNom, SkuSubstitute.BrandGuid AS SubstituteBrandGuid, SkuSubstitute.BrandNom AS SubstituteBrandNom, SkuSubstitute.SkuNom AS SubstituteSkuNom, SkuSubstitute.FullNom AS SubstituteSkuFullNom ")

            If oMgz IsNot Nothing Then
                sb.AppendLine(", VwSkuStocks.Stock ")
            End If

            sb.AppendLine("FROM Art ")
            sb.AppendLine("INNER JOIN Stp ON Art.Category = Stp.Guid ")
            sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")

            sb.AppendLine("LEFT OUTER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText SeoTitle ON Art.Guid = SeoTitle.Guid AND SeoTitle.Src = " & DTOLangText.Srcs.SeoTitle & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Art.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ProductExcerpt & " ")
            sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangText ON Art.Guid = LangText.Guid AND LangText.Src = " & DTOLangText.Srcs.ProductText & " ")
            sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")

            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwCnap ON VwCnap.Guid = (CASE WHEN Art.CnapGuid IS NULL THEN Stp.CnapGuid ELSE Art.CnapGuid END) ")
            sb.AppendLine("LEFT OUTER JOIN CodisMercancia ON Art.CodiMercancia=CodisMercancia.Id ")
            sb.AppendLine("LEFT OUTER JOIN Country ON Art.MadeIn=Country.Guid ")
            sb.AppendLine("LEFT OUTER JOIN VwRetail ON Art.Guid = VwRetail.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwProductNom SkuSubstitute ON Art.Substitute = SkuSubstitute.Guid ")

            If oMgz IsNot Nothing Then
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid ='" & oMgz.Guid.ToString & "' ")
            End If

            sb.AppendLine("WHERE Art.Guid='" & oSku.Guid.ToString & "'")

            Dim SQL As String = sb.ToString
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            If oDrd.Read Then
                Dim oBrand As New DTOProductBrand(oDrd("Brand"))
                With oBrand
                    SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                    .ShowAtlas = oDrd("ShowAtlas")
                    If Not IsDBNull(oDrd("Proveidor")) Then
                        .Proveidor = New DTOProveidor(oDrd("Proveidor"))
                    End If
                End With
                Dim oCategory As New DTOProductCategory(oDrd("Category"))
                With oCategory
                    .Brand = oBrand
                    SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    .InnerPack = oDrd("StpInnerPack")
                    .OuterPack = SQLHelper.GetIntegerFromDataReader(oDrd("StpOuterPack"))
                    .ForzarInnerPack = oDrd("StpForzarInnerPack")
                    If Not IsDBNull(oDrd("StpCnap")) Then
                        .Cnap = New DTOCnap(oDrd("StpCnap"))
                        If IsDBNull(oDrd("ArtCnap")) Then
                            .Cnap.Id = SQLHelper.GetStringFromDataReader(oDrd("CnapId"))
                            .Cnap.NomLong = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapNomLong")
                            .Cnap.NomShort = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapNomShort")
                        End If
                    End If
                End With
                With oSku
                    .Id = oDrd("Art")

                    SQLHelper.LoadLangTextFromDataReader(oSku.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    SQLHelper.LoadLangTextFromDataReader(oSku.NomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                    SQLHelper.LoadLangTextFromDataReader(.SeoTitle, oDrd, "SeoTitleEsp", "SeoTitleCat", "SeoTitleEng", "SeoTitlePor")
                    SQLHelper.LoadLangTextFromDataReader(.Excerpt, oDrd, "ExcerptEsp", "ExcerptCat", "ExcerptEng", "ExcerptPor")
                    SQLHelper.LoadLangTextFromDataReader(.Content, oDrd, "TextEsp", "TextCat", "TextEng", "TextPor")
                    .UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)

                    .RefProveidor = oDrd("Ref")
                    .NomProveidor = oDrd("RefPrv")
                    .Ean13 = SQLHelper.GetEANFromDataReader(oDrd("CBar"))
                    .PackageEan = SQLHelper.GetEANFromDataReader(oDrd("PackageEan"))
                    If Not IsDBNull(oDrd("ArtCnap")) Then
                        .Cnap = New DTOCnap(oDrd("ArtCnap"))
                        .Cnap.Id = SQLHelper.GetStringFromDataReader(oDrd("CnapId"))
                        .Cnap.NomLong = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapNomLong")
                        .Cnap.NomShort = SQLHelper.GetLangTextFromDataReader(oDrd, "CnapNomShort")
                    End If
                    .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("IsBundle"))
                    .BundleDto = SQLHelper.GetDecimalFromDataReader(oDrd("bundleDto"))

                    If oMgz IsNot Nothing Then
                        .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    End If

                    .ImageExists = SQLHelper.GetBooleanFromDatareader(oDrd("ImgExists"))

                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1")) ' - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))

                    .IvaCod = oDrd("IvaCod")
                    '.virtual = oDrd("Virtual")
                    .NoWeb = oDrd("NoWeb")
                    .NoEcom = oDrd("NoEcom")
                    .NoStk = oDrd("NoStk")
                    .SecurityStock = SQLHelper.GetIntegerFromDataReader(oDrd("SecurityStock"))
                    .NoPro = oDrd("NoPro")
                    .NoTarifa = SQLHelper.GetBooleanFromDatareader(oDrd("NoTarifa"))
                    .HideUntil = SQLHelper.GetFchFromDataReader(oDrd("HideUntil"))
                    .obsoleto = oDrd("Obsoleto")
                    .FchObsoleto = SQLHelper.GetFchFromDataReader(oDrd("FchObsoleto"))
                    .ObsoletoConfirmed = SQLHelper.GetFchFromDataReader(oDrd("ObsoletoConfirmed"))

                    If IsDBNull(oDrd("Substitute")) Then
                        .Substitute = Nothing
                    Else
                        Dim oSubstituteBrand As New DTOProductBrand(oDrd("SubstituteBrandGuid"))
                        SQLHelper.LoadLangTextFromDataReader(oSubstituteBrand.Nom, oDrd, "SubstituteBrandNom", "SubstituteBrandNom", "SubstituteBrandNom", "SubstituteBrandNom")
                        Dim oSubstituteCategory As New DTOProductCategory(oDrd("SubstituteCategoryGuid"))
                        oSubstituteCategory.Brand = oSubstituteBrand
                        SQLHelper.LoadLangTextFromDataReader(oSubstituteCategory.Nom, oDrd, "SubstituteCategoryNom", "SubstituteCategoryNom", "SubstituteCategoryNom", "SubstituteCategoryNom")
                        .Substitute = New DTOProductSku(oDrd("Substitute"))
                        .Substitute.Category = oSubstituteCategory
                        .Substitute.NomLlarg.Esp = SQLHelper.GetStringFromDataReader(oDrd("SubstituteSkuFullNom"))
                        .Substitute.Nom.Esp = SQLHelper.GetStringFromDataReader(oDrd("SubstituteSkuNom"))
                    End If

                    .Category = oCategory
                    .Hereda = oDrd("Hereda")
                    .NoDimensions = SQLHelper.GetBooleanFromDatareader(oDrd("ArtNoDimensions"))
                    .KgBrut = SQLHelper.GetDecimalFromDataReader(oDrd("ArtKgBrut"))
                    .KgNet = SQLHelper.GetDecimalFromDataReader(oDrd("ArtKgNet"))
                    .DimensionL = SQLHelper.GetIntegerFromDataReader(oDrd("ArtDimensionL"))
                    .DimensionW = SQLHelper.GetIntegerFromDataReader(oDrd("ArtDimensionW"))
                    .DimensionH = SQLHelper.GetIntegerFromDataReader(oDrd("ArtDimensionH"))
                    .InnerPack = oDrd("ArtInnerPack")
                    .OuterPack = SQLHelper.GetIntegerFromDataReader(oDrd("ArtOuterPack"))
                    .ForzarInnerPack = oDrd("ArtForzarInnerPack")
                    .HeredaDimensions = oDrd("HeredaDimensions")
                    .PackageEan = SQLHelper.GetEANFromDataReader(oDrd("PackageEan"))
                    .LastProduction = SQLHelper.GetBooleanFromDatareader(oDrd("LastProduction"))
                    .Availability = SQLHelper.GetFchFromDataReader(oDrd("Availability"))

                    .Outlet = SQLHelper.GetBooleanFromDatareader(oDrd("Outlet"))
                    .OutletDto = SQLHelper.GetDecimalFromDataReader(oDrd("OutletDto"))
                    .OutletQty = SQLHelper.GetIntegerFromDataReader(oDrd("OutletQty"))

                    If Not IsDBNull(oDrd("MadeIn")) Then
                        .MadeIn = New DTOCountry(oDrd("MadeIn"))
                        With .MadeIn
                            .ISO = SQLHelper.GetStringFromDataReader(oDrd("MadeInISO"))
                            .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "MadeInEsp", "MadeInCat", "MadeInEng", "MadeInPor")
                        End With
                    End If

                    If Not IsDBNull(oDrd("CodiMercancia")) Then
                        .CodiMercancia = New DTOCodiMercancia(oDrd("CodiMercancia"))
                        .CodiMercancia.Dsc = SQLHelper.GetStringFromDataReader(oDrd("CodiMercanciaDsc"))
                    End If

                    .Rrpp = Defaults.AmtOrNothing(oDrd("Retail"))
                    .UsrLog = SQLHelper.getUsrLogFromDataReader(oDrd)
                    .IsLoaded = True
                End With
            End If

            oDrd.Close()
            oSku.UrlSegments = UrlSegmentsLoader.All(oSku)
            If oSku.IsBundle Then oSku.BundleSkus = BundleSkus(oSku)
        End If

        Dim retval As Boolean = oSku.IsLoaded
        Return retval
    End Function


    Shared Function Update(oSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try

            Update(oSku, oTrans)
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

    Shared Sub Update(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        UpdateHeader(oSku, oTrans)
        LangTextLoader.Update(oSku.Nom, oTrans)
        LangTextLoader.Update(oSku.NomLlarg, oTrans)
        LangTextLoader.Update(oSku.SeoTitle, oTrans)
        LangTextLoader.Update(oSku.Excerpt, oTrans)
        LangTextLoader.Update(oSku.Content, oTrans)
        UrlSegmentsLoader.Update(oSku, oSku.UrlSegments, oTrans)
        UpdateBundles(oSku, oTrans)
    End Sub


    Shared Sub UpdateHeader(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        If oSku.IsNew And oSku.Id = 0 Then oSku.Id = LastId(oSku.Category.Brand.Emp, oTrans) + 1

        Dim SQL As String = "SELECT * FROM Art WHERE Guid='" & oSku.Guid.ToString & "'"
        Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
        Dim oDs As New DataSet
        oDA.Fill(oDs)
        Dim oTb As DataTable = oDs.Tables(0)
        Dim oRow As DataRow
        If oTb.Rows.Count = 0 Then
            oRow = oTb.NewRow
            oTb.Rows.Add(oRow)
            oRow("Guid") = oSku.Guid
            oRow("Emp") = oSku.Category.Brand.Emp.Id
        Else
            oRow = oTb.Rows(0)
        End If

        With oSku
            If .Id > 0 Then oRow("Art") = .Id
            oRow("Category") = SQLHelper.NullableBaseGuid(.Category)
            oRow("Ref") = .RefProveidor
            oRow("RefPrv") = .NomProveidor
            oRow("cBar") = SQLHelper.NullableEan(.Ean13)
            oRow("PackageEan") = SQLHelper.NullableEan(.PackageEan)
            oRow("ivaCod") = .IvaCod
            oRow("Hereda") = .Hereda
            oRow("InnerPack") = .InnerPack
            oRow("NoStk") = .NoStk
            oRow("SecurityStock") = .SecurityStock
            'oRow("Virtual") = .virtual
            oRow("MadeIn") = SQLHelper.NullableBaseGuid(.MadeIn)
            oRow("isBundle") = .IsBundle
            oRow("bundleDto") = SQLHelper.NullableDecimal(.BundleDto)

            If .Image IsNot Nothing Then
                oRow("Image") = .Image
                Dim oImage As System.Drawing.Image = Nothing
                Dim ms = New IO.MemoryStream(.Image)
                oImage = System.Drawing.Image.FromStream(ms)
                Dim oThumbnail = LegacyHelper.ImageHelper.GetThumbnailToFill(oImage, DTOProductSku.THUMBNAILWIDTH, DTOProductSku.THUMBNAILHEIGHT)
                oRow("Thumbnail") = LegacyHelper.ImageHelper.Converter(oThumbnail)
            End If

            If .CodiMercancia Is Nothing Then
                oRow("CodiMercancia") = System.DBNull.Value
            Else
                oRow("CodiMercancia") = .CodiMercancia.Id
            End If

            oRow("NoWeb") = .NoWeb
            oRow("NoPro") = .NoPro
            oRow("NoStk") = .NoStk
            oRow("NoEcom") = .NoEcom

            oRow("Obsoleto") = .obsoleto
            oRow("FchObsoleto") = SQLHelper.NullableFch(.FchObsoleto)
            oRow("ObsoletoConfirmed") = SQLHelper.NullableFch(.ObsoletoConfirmed)
            oRow("Substitute") = SQLHelper.NullableBaseGuid(.Substitute)

            If .Category.Cnap Is Nothing Then
                oRow("CnapGuid") = SQLHelper.NullableBaseGuid(.Cnap)
            ElseIf .Category.Cnap.Equals(.Cnap) Then
                oRow("CnapGuid") = System.DBNull.Value
            Else
                oRow("CnapGuid") = SQLHelper.NullableBaseGuid(.Cnap)
            End If

            oRow("NoTarifa") = .NoTarifa
            oRow("HideUntil") = SQLHelper.NullableFch(.HideUntil)
            oRow("HeredaDimensions") = .HeredaDimensions
            oRow("NoDimensions") = .NoDimensions
            oRow("KgNet") = .KgNet
            oRow("Kg") = .KgBrut
            oRow("DimensionL") = .DimensionL
            oRow("DimensionW") = .DimensionW
            oRow("DimensionH") = .DimensionH
            oRow("OuterPack") = .OuterPack
            oRow("ForzarInnerPack") = .ForzarInnerPack
            oRow("LastProduction") = .LastProduction
            oRow("Availability") = SQLHelper.NullableFch(.Availability)
            oRow("Outlet") = .Outlet
            oRow("OutletDto") = .OutletDto
            oRow("OutletQty") = .OutletQty
            SQLHelper.SetUsrLog(.UsrLog, oRow)
        End With

        oDA.Update(oDs)
    End Sub


    Shared Sub UpdateBundles(oBundle As DTOProductSku, ByRef oTrans As SqlTransaction)
        If Not oBundle.IsNew Then DeleteBundles(oBundle, oTrans)

        If oBundle.BundleSkus.Count > 0 Then
            Dim SQL As String = "SELECT * FROM SkuBundle WHERE Bundle = '" & oBundle.Guid.ToString & "'"
            Dim oDA As SqlDataAdapter = SQLHelper.GetSQLDataAdapter(SQL, oTrans)
            Dim oDs As New DataSet
            oDA.Fill(oDs)
            Dim oTb As DataTable = oDs.Tables(0)
            For Each item In oBundle.BundleSkus
                Dim oRow As DataRow = oTb.NewRow
                oTb.Rows.Add(oRow)
                oRow("Bundle") = oBundle.Guid
                oRow("Ord") = oBundle.BundleSkus.IndexOf(item)
                oRow("Sku") = item.Sku.Guid
                oRow("Qty") = item.Qty
            Next
            oDA.Update(oDs)
        End If

    End Sub

    Shared Function SQLfriendly(ByVal Src As String) As String
        'enmascara apostrofes
        If Src Is Nothing Then
            Return ""
        Else
            Dim iPos As Integer = 0
            Dim iStartIndex As Integer = 0
            Dim sResult As String = Src
            iPos = sResult.IndexOf("'")
            Do While iPos >= 0
                sResult = sResult.Substring(0, iPos) & "''" & sResult.Substring(iPos + 1)
                iStartIndex = iPos + 2
                iPos = sResult.IndexOf("'", iStartIndex)
            Loop
            Return sResult
        End If
    End Function

    Shared Function LastId(oEmp As DTOEmp, ByRef oTrans As SqlTransaction) As Integer
        Dim retval As Integer

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 1 Art AS LastId ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("WHERE Art.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY Art DESC")

        Dim SQL As String = sb.ToString
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

    Shared Function Delete(oSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim oConn As SqlConnection = SQLHelper.SQLConnection(True)
        Dim oTrans As SqlTransaction = oConn.BeginTransaction
        Try
            LangTextLoader.Delete(oSku, oTrans)
            Delete(oSku, oTrans)
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

    Shared Sub Delete(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        ProductLoader.DeleteRelateds(oSku, oTrans)
        ProductLoader.DeleteLangTexts(oSku, oTrans)
        LangTextLoader.Delete(oSku, oTrans)
        UrlSegmentsLoader.Delete(oSku, oTrans)
        DeletePriceList(oSku, oTrans)
        DeleteBundles(oSku, oTrans)
        DeleteHeader(oSku, oTrans)
    End Sub

    Shared Sub DeletePriceList(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE PriceListItem_Customer WHERE PriceListItem_Customer.Art = '" & oSku.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub


    Shared Sub DeleteBundles(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE SkuBundle WHERE SkuBundle.Bundle = '" & oSku.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Sub DeleteHeader(oSku As DTOProductSku, ByRef oTrans As SqlTransaction)
        Dim SQL As String = "DELETE Art WHERE Art.Guid = '" & oSku.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, oTrans)
    End Sub

    Shared Function FromProveidor(oProveidor As DTOContact, sRef As String) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim SQL As String = "SELECT * FROM VwSkuNom WHERE VwSkuNom.Proveidor ='" & oProveidor.Guid.ToString & "' AND VwSkuNom.SkuRef = '" & sRef & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetProductFromDataReader(oDrd)
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromProveidor(sRef As String) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim SQL As String = "SELECT * FROM VwSkuNom WHERE VwSkuNom.SkuRef = '" & sRef & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function



    Shared Function LastCost(oSku As DTOProductSku, oMgz As DTOMgz, Optional DtFch As Date = Nothing) As DTOAmt
        'de l'Excel de Vivace
        Dim retval As DTOAmt = Nothing
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Arc.Pts, Arc.Cur, Arc.Eur, Arc.Dto ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN ( ")
        sb.AppendLine("             SELECT Arc.ArtGuid, Arc.Cod, MAX(Fch) AS LastFch ")
        sb.AppendLine("             FROM Arc ")
        sb.AppendLine("             INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        If DtFch <> Nothing Then
            sb.AppendLine("         WHERE Alb.Fch <= '" & Format(DtFch, "yyyyMMdd") & "' ")
        End If
        sb.AppendLine("             GROUP BY Arc.ArtGuid, Arc.Cod ")
        sb.AppendLine("             ) X ")
        sb.AppendLine("ON Arc.ArtGuid = X.ArtGuid AND Arc.Cod = X.Cod AND Alb.Fch = X.LastFch ")
        sb.AppendLine("WHERE Arc.ArtGuid = '" & oSku.Guid.ToString & "' ")
        sb.AppendLine("AND Arc.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("AND Arc.Cod = 11 ")
        sb.AppendLine("AND Arc.Eur > 0 ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim Preu As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
            Dim Dto As Decimal = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
            Dim Cost As Decimal = Preu * (100 - Dto) / 100
            retval = DTOAmt.Factory(Cost)
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function ImageMime(oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim SQL As String = "SELECT Image FROM Art WHERE Guid='" & oGuid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Image")) Then
                retval = MatHelperStd.ImageMime.Factory(oDrd("Image"), MimeCods.Jpg)
            End If
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function ThumbnailMime(oGuid As Guid) As ImageMime
        Dim retval As ImageMime = Nothing
        Dim SQL As String = "SELECT Thumbnail FROM Art WHERE Guid='" & oGuid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            If Not IsDBNull(oDrd("Thumbnail")) Then
                retval = MatHelperStd.ImageMime.Factory(oDrd("Thumbnail"), MimeCods.Jpg)
            End If
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function LastProductionAvailableUnits(oSku As DTOProductSku) As Integer
        Dim retVal As Integer = 0

        Dim SQL As String = "SELECT VwSkuPncs.Pn1, VwSkuPncs.Clients FROM VwSkuPncs WHERE VwSkuPncs.SkuGuid='" & oSku.Guid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            Dim IntStk As Integer = oSku.Stock
            Dim IntPn2 As Integer = oDrd("Clients")
            Dim IntPn1 As Integer = oDrd("Pn1")
            retVal = IntStk + IntPn1 - IntPn2
        End If
        oDrd.Close()

        Return retVal
    End Function

    Shared Function DeleteImage(ByRef oSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim SQL As String = "UPDATE ART SET IMAGE=NULL, ImgFch=GETDATE() WHERE Guid='" & oSku.Guid.ToString & "'"
        SQLHelper.ExecuteNonQuery(SQL, exs)
        If exs.Count = 0 Then
            retval = True
            oSku.Image = Nothing
            oSku.ImageFch = DTO.GlobalVariables.Now()
        End If
        Return retval
    End Function

    Shared Function UpdateBigImg(ByRef oSku As DTOProductSku, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim SQL As String = "SELECT Guid, Image, ImgExists, ImgFch FROM Art WHERE Guid='" & oSku.Guid.ToString & "'"
        Dim oConn As SqlConnection = SQLHelper.SQLConnection()
        Dim oCmd As SqlCommand = SQLHelper.GetSQLCommand(SQL, oConn)
        oConn.Open()

        Dim oDS As New DataSet
        Dim oDA As New SqlDataAdapter
        oDA.SelectCommand = oCmd
        Dim oCB As New SqlCommandBuilder(oDA)

        Try
            oDA.Fill(oDS)
            Dim oTb As DataTable = oDS.Tables(0)
            Dim oRow As DataRow = Nothing
            If oTb.Rows.Count > 0 Then
                oRow = oTb.Rows(0)
                oRow("Image") = If(oSku.Image Is Nothing, System.DBNull.Value, oSku.Image)
                oRow("ImgFch") = DTO.GlobalVariables.Now()
                oSku.ImageExists = oSku.Image IsNot Nothing
                oRow("ImgExists") = oSku.ImageExists
                oDA.Update(oDS)
            End If
            retval = True

        Catch e As SqlException
            exs.Add(e)

        Finally
            oConn.Close()
            oConn = Nothing
        End Try

        Return retval
    End Function

    Shared Function BundleSkus(oBundle As DTOProductSku) As List(Of DTOSkuBundle)
        'simple
        Dim retval As New List(Of DTOSkuBundle)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.*, SkuBundle.Qty, VwSkuRetail.Retail, Art.BundleDto ")
        sb.AppendLine("FROM SkuBundle ")
        sb.AppendLine("INNER JOIN VwSkuNom ON SkuBundle.Sku = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Art ON SkuBundle.Bundle = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON SkuBundle.Sku = VwSkuRetail.SkuGuid ")
        sb.AppendLine("WHERE SkuBundle.Bundle = '" & oBundle.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY SkuBundle.Ord ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSkuBundle
            item.Sku = SQLHelper.GetProductFromDataReader(oDrd)
            item.Qty = oDrd("Qty")
            If Not IsDBNull(oDrd("Retail")) Then
                Dim oRetail As DTOAmt = DTOAmt.Factory(oDrd("Retail"))
                item.Sku.Rrpp = oRetail.DeductPercent(SQLHelper.GetDecimalFromDataReader(oDrd("BundleDto")))
            End If
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SeBuscaEmailsForMailing(oSku As DTOProductSku) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.Adr ")
        sb.AppendLine("FROM ARC ")
        sb.AppendLine("INNER JOIN ALB ON ARC.AlbGuid = ALB.Guid ")
        sb.AppendLine("INNER JOIN Email_CLIS ON ALB.CliGuid = Email_CLIS.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_CLIS.EmailGuid = Email.Guid ")
        sb.AppendLine("WHERE ARC.ArtGuid = '" & oSku.Guid.ToString & "' ")
        sb.AppendLine("AND ARC.COD = 51 ")
        sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        sb.AppendLine("GROUP BY Email.Adr ")
        sb.AppendLine("ORDER BY Email.Adr ")
        Dim SQL As String = sb.ToString()
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("Guid"), Guid))
            oUser.EmailAddress = oDrd("Adr")
            retval.Add(oUser)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function EmailsFromPncs(oSku As DTOProductSku) As List(Of DTOUser)
        Dim retval As New List(Of DTOUser)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.Adr ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ON Pdc.CliGuid = Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN Email ON Email_Clis.EmailGuid = Email.Guid ")
        sb.AppendLine("WHERE Pnc.ArtGuid = '" & oSku.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Cod = 2 ")
        sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        sb.AppendLine("GROUP BY Email.Adr ")
        sb.AppendLine("ORDER BY Email.Adr ")

        Dim SQL As String = sb.ToString()
        Dim oDrd As SqlClient.SqlDataReader = DAL.SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(DirectCast(oDrd("Guid"), Guid))
            oUser.EmailAddress = oDrd("Adr")
            retval.Add(oUser)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

Public Class ProductSkusLoader
    Shared Function All(oEmp As DTOEmp) As List(Of DTOProductSku)
        'simple
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.CategoryOrd, VwSkuNom.Obsoleto, VwSkuNom.SkuOrd, VwSkuNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            item.ImageExists = SQLHelper.GetBooleanFromDatareader(oDrd("SkuImageExists"))
            item.NoPro = SQLHelper.GetBooleanFromDatareader(oDrd("EnabledxPro")) = False
            item.NoWeb = SQLHelper.GetBooleanFromDatareader(oDrd("SkuNoWeb"))
            item.NoEcom = SQLHelper.GetBooleanFromDatareader(oDrd("SkuNoEcom"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oCategory As DTOProductCategory, oCustomer As DTOCustomer, oMgz As DTOMgz, IncludeObsolets As Boolean, stockOnly As Boolean) As List(Of DTOProductSku)
        'simple
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerSkus.SkuGuid, VwCustomerSkus.SkuId, VwCustomerSkus.SkuNom, VwCustomerSkus.SkuNomLlarg, Art.Hereda, Art.NoWeb, Art.NoPro, Art.NoEcom, Art.LastProduction, Art.Obsoleto ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("INNER JOIN Art ON VwCustomerSkus.SkuGuid = Art.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid ='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE VwCustomerSkus.CategoryGuid='" & oCategory.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.Customer = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.CodExclusio IS NULL ")
        If Not IncludeObsolets Then
            sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        End If
        sb.AppendLine("ORDER BY Art.Obsoleto, Art.Ord, VwCustomerSkus.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductSku(oDrd("SkuGuid"))
            With item
                .Category = oCategory
                .Id = oDrd("SkuId")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNom", "SkuNom", "SkuNom", "SkuNom")
                SQLHelper.LoadLangTextFromDataReader(.NomLlarg, oDrd, "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg")
                .Hereda = oDrd("Hereda")
                .NoWeb = oDrd("NoWeb")
                .NoPro = oDrd("NoPro")
                .NoEcom = oDrd("NoEcom")
                .obsoleto = oDrd("Obsoleto")
                .LastProduction = oDrd("LastProduction")
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
            End With

            Dim skip As Boolean = False
            If item.LastProduction Then
                Dim available As Integer = item.Stock - item.Clients + item.Proveidors
                skip = available <= 0
            End If

            If stockOnly Then
                Dim available As Integer = item.Stock - (item.Clients - item.ClientsAlPot - item.ClientsEnProgramacio)
                If available <= 0 Then skip = True
            End If
            If Not skip Then retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer, Optional oMgz As DTOMgz = Nothing, Optional IncludeExcludedProducts As Boolean = False) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerSkus.*, VwRetail.Retail ")
        If oMgz IsNot Nothing Then
            sb.AppendLine(", Stks.Stock, Stks.Clients, Stks.ClientsAlPot, Stks.ClientsEnProgramacio, Stks.ClientsBlockStock, Stks.Pn1 ")
        End If
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON VwCustomerSkus.SkuGuid = VwRetail.SkuGuid ")

        If oMgz IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN ( ")
            sb.AppendLine("SELECT SkuGuid, SUM(Stock) AS Stock, SUM(Clients) AS Clients, SUM(ClientsAlPot) AS ClientsAlPot, SUM(ClientsEnProgramacio) AS ClientsEnProgramacio, SUM(ClientsBlockStock) AS ClientsBlockStock, SUM(Pn1) AS Pn1 ")
            sb.AppendLine("FROM (")
            sb.AppendLine("SELECT SkuGuid, Stock, 0 AS Clients, 0 AS ClientsAlPot, 0 AS ClientsEnProgramacio, 0 AS ClientsBlockStock, 0 AS Pn1 ")
            sb.AppendLine("FROM VwSkuStocks ")
            sb.AppendLine("WHERE MgzGuid = '" & oMgz.Guid.ToString & "' ")
            sb.AppendLine("UNION ")
            sb.AppendLine("SELECT SkuGuid, 0 AS Stock, Clients, ClientsAlPot, ClientsEnProgramacio, ClientsBlockStock, Pn1 ")
            sb.AppendLine("FROM VwSkuPncs) AS X ")
            sb.AppendLine("GROUP BY X.SkuGuid ")
            sb.AppendLine(") Stks ON VwCustomerSkus.SkuGuid = Stks.SkuGuid ")
        End If

        sb.AppendLine("WHERE VwCustomerSkus.Customer='" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        If Not IncludeExcludedProducts Then
            sb.AppendLine("AND VwCustomerSkus.CodExclusio IS NULL ")
        End If

        sb.AppendLine("ORDER BY VwCustomerSkus.BrandOrd, VwCustomerSkus.BrandNom, VwCustomerSkus.BrandGuid, VwCustomerSkus.CategoryCodi, VwCustomerSkus.CategoryOrd, VwCustomerSkus.CategoryNom, VwCustomerSkus.SkuNom")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)

            If oSku.Category.Equals(oCategory) Then
                oSku.Category = oCategory
            Else
                oCategory = oSku.Category
                If oCategory.Brand.Equals(oBrand) Then
                    oCategory.Brand = oBrand
                Else
                    oBrand = oCategory.Brand
                End If
            End If

            With oSku
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                If oMgz IsNot Nothing Then
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                End If
                .CodExclusio = SQLHelper.GetIntegerFromDataReader(oDrd("codExclusio"))
            End With

            retval.Add(oSku)
        Loop
        oDrd.Close()

        Return retval
    End Function


    Shared Function All(oCnap As DTOCnap, Optional IncludeObsoletos As Boolean = False) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")

        If oCnap.Guid = Nothing Then
            sb.AppendLine("WHERE VwSkuNom.CnapGuid IS NULL ")
        Else
            sb.AppendLine("WHERE VwSkuNom.CnapGuid = '" & oCnap.Guid.ToString & "' ")
        End If

        If Not IncludeObsoletos Then
            sb.AppendLine("AND VwSkuNom.Obsoleto=0 ")
        End If
        sb.AppendLine("ORDER BY VwSkuNom.Obsoleto, VwSkuNom.SkuNomLlarg ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Excerpts() As List(Of DTOLangText)
        Dim retval As New List(Of DTOLangText)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwProductText.Guid,ExcerptEsp, ExcerptCat, ExcerptEng, ExcerptPor ")
        sb.AppendLine("FROM VwProductText INNER JOIN VwSkuNom ON VwProductText.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE VwSkuNom.Obsoleto=0 AND ExcerptEsp IS NOT NULL ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOLangText(CType(oDrd("Guid"), Guid), DTOLangText.Srcs.ProductExcerpt, SQLHelper.GetStringFromDataReader(oDrd("ExcerptEsp")), SQLHelper.GetStringFromDataReader(oDrd("ExcerptCat")), SQLHelper.GetStringFromDataReader(oDrd("ExcerptEng")), SQLHelper.GetStringFromDataReader(oDrd("ExcerptPor")))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All_Deprecated(oCategory As DTOProductCategory, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsoletos As Boolean = False) As List(Of DTOProductSku)
        'per Mat.Net cataleg
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid, Art.Art, Art.NoWeb, Art.NoEcom, VwRetail.Retail ")
        sb.AppendLine(", Art.HeredaDimensions, Art.InnerPack AS ArtInnerPack, Art.ForzarInnerPack AS ArtForzarInnerPack, Art.isBundle ")
        sb.AppendLine(", (CASE WHEN Art.[Image] IS NULL THEN 0 ELSE 1 END) AS Img ")
        sb.AppendLine(", Art.LastProduction, Art.Outlet, Art.OutletDto, Art.OutletQty, Art.Obsoleto, Art.ObsoletoConfirmed, Art.CBar, Art.PackageEan, Art.NoPro ")

        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
        sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

        If oMgz IsNot Nothing Then
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock, VwSkuStocks.Stock, Previsio.Previsio ")
        End If

        sb.AppendLine("FROM Art ")

        If oMgz IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN (SELECT Sku, SUM(Qty) AS Previsio  ")
            sb.AppendLine("                 FROM ImportPrevisio ")
            sb.AppendLine("                 GROUP BY Sku) AS Previsio ON ART.Guid=Previsio.Sku ")
        End If

        sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON Art.Guid = VwRetail.SkuGuid ")


        sb.AppendLine("WHERE Art.Category='" & oCategory.Guid.ToString & "' ")
        If Not IncludeObsoletos Then
            If oMgz Is Nothing Then
                sb.AppendLine("AND Art.Obsoleto=0 AND Art.NoWeb = 0 ") 'important per coherencia amb Sprite
            Else
                sb.AppendLine("AND (Art.ObsoletoConfirmed IS NULL OR VwSkuStocks.Stock<>0 OR VwSkuPncs.Clients<>0 OR VwSkuPncs.Pn1<>0 ) ")
            End If
        End If

        If IncludeObsoletos Then
            sb.AppendLine("ORDER BY Art.Obsoleto, SkuNom.Esp ")
        Else
            sb.AppendLine("ORDER BY SkuNom.Esp ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductSku(oDrd("Guid"))
            With item
                .Category = oCategory
                .Id = oDrd("Art")
                'If .Id = 23328 Then Stop '============================================================================================================
                .NoPro = oDrd("NoPro")
                .NoEcom = oDrd("NoEcom")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                SQLHelper.LoadLangTextFromDataReader(.NomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                .UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                .LastProduction = oDrd("LastProduction")
                .NoWeb = oDrd("NoWeb")
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                .Ean13 = SQLHelper.GetEANFromDataReader(oDrd("CBar"))
                .PackageEan = SQLHelper.GetEANFromDataReader(oDrd("PackageEan"))
                .InnerPack = oDrd("ArtInnerPack")
                .ForzarInnerPack = oDrd("ArtForzarInnerPack")
                .HeredaDimensions = oDrd("HeredaDimensions")
                .ImageExists = oDrd("Img")
                .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                '.virtual = oDrd("Virtual")
                .Outlet = oDrd("Outlet")
                .OutletDto = oDrd("OutletDto")
                .OutletQty = oDrd("OutletQty")
                .obsoleto = oDrd("Obsoleto")
                .ObsoletoConfirmed = SQLHelper.GetFchFromDataReader(oDrd("ObsoletoConfirmed"))

                If oMgz IsNot Nothing Then
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1")) ' - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Previsions = SQLHelper.GetIntegerFromDataReader(oDrd("Previsio")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function All(oCategory As DTOProductCategory, oLang As DTOLang, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsoletos As Boolean = False) As JArray
        'per Mat.Net cataleg
        Dim retval As New JArray

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid, Art.Art, Art.NoWeb, VwRetail.Retail ")
        sb.AppendLine(", Art.HeredaDimensions, Art.InnerPack AS ArtInnerPack, Art.ForzarInnerPack AS ArtForzarInnerPack, Art.isBundle ")
        sb.AppendLine(", (CASE WHEN Art.[Image] IS NULL THEN 0 ELSE 1 END) AS ImageExists ")
        sb.AppendLine(", Art.LastProduction, Art.Outlet, Art.OutletDto, Art.OutletQty, Art.Obsoleto, Art.ObsoletoConfirmed, Art.CBar, Art.PackageEan, Art.NoPro ")

        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
        sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

        If oMgz IsNot Nothing Then
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock, VwSkuStocks.Stock, Previsio.Previsio ")
        End If

        sb.AppendLine("FROM Art ")

        If oMgz IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN (SELECT Sku, SUM(Qty) AS Previsio  ")
            sb.AppendLine("                 FROM ImportPrevisio ")
            sb.AppendLine("                 GROUP BY Sku) AS Previsio ON ART.Guid=Previsio.Sku ")
        End If

        sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON Art.Guid = VwRetail.SkuGuid ")


        sb.AppendLine("WHERE Art.Category='" & oCategory.Guid.ToString & "' ")
        If Not IncludeObsoletos Then
            If oMgz Is Nothing Then
                sb.AppendLine("AND Art.Obsoleto=0 AND Art.NoWeb = 0 ") 'important per coherencia amb Sprite
            Else
                sb.AppendLine("AND (Art.ObsoletoConfirmed IS NULL OR VwSkuStocks.Stock<>0 OR VwSkuPncs.Clients<>0 OR VwSkuPncs.Pn1<>0 ) ")
            End If
        End If

        If IncludeObsoletos Then
            sb.AppendLine("ORDER BY Art.Obsoleto, Art.Ord, SkuNom.Esp ")
        Else
            sb.AppendLine("ORDER BY Art.Ord, SkuNom.Esp ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            'Try
            Dim oNomCurt As New JObject
            oNomCurt.Add("Esp", SQLHelper.LangTextResultFromDataReader(oLang, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor"))

            Dim oNomLlarg As New JObject
            oNomLlarg.Add("Esp", SQLHelper.LangTextResultFromDataReader(oLang, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor"))

            Dim item As New JObject
            item.Add("Guid", oDrd("Guid").ToString())
            item.Add("Id", oDrd("Art").ToString())
            item.Add("Nom", oNomCurt)
            item.Add("NomLlarg", oNomLlarg)
            item.Add("UrlCanonicas", SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd).ToJObject())

            If oDrd("NoPro") Then item.Add("NoPro", SQLHelper.GetBooleanFromDatareader(oDrd("NoPro")).ToString)
            If oDrd("NoWeb") Then item.Add("NoWeb", SQLHelper.GetBooleanFromDatareader(oDrd("NoWeb")).ToString)
            If oDrd("ImageExists") Then item.Add("ImageExists", SQLHelper.GetBooleanFromDatareader(oDrd("ImageExists")).ToString)
            If oDrd("ArtForzarInnerPack") Then item.Add("ForzarInnerPack", SQLHelper.GetBooleanFromDatareader(oDrd("ArtForzarInnerPack")).ToString)
            If SQLHelper.GetIntegerFromDataReader(oDrd("ArtInnerPack")) > 1 Then item.Add("InnerPack", oDrd("ArtInnerPack").ToString)
            If Not IsDBNull(oDrd("CBar")) AndAlso Not String.IsNullOrEmpty(oDrd("CBar")) Then
                Dim oEan13 As New JObject
                oEan13.Add("Value", oDrd("CBar").ToString())
                item.Add("Ean13", oEan13)
            End If
            If Not IsDBNull(oDrd("PackageEan")) AndAlso Not String.IsNullOrEmpty(oDrd("PackageEan")) Then
                Dim oPackageEan As New JObject
                oPackageEan.Add("Value", oDrd("PackageEan").ToString())
                item.Add("PackageEan", oPackageEan)
            End If

            If Not IsDBNull(oDrd("Retail")) Then
                Dim oCur As New JObject
                oCur.Add("Tag", "EUR")
                Dim oRetail As New JObject
                oRetail.Add("Eur", CType(oDrd("Retail"), Decimal).ToString("F2", System.Globalization.CultureInfo.InvariantCulture))
                oRetail.Add("Cur", oCur)
                item.Add("Rrpp", oRetail)
            End If

            If oDrd("LastProduction") Then item.Add("LastProduction", SQLHelper.GetBooleanFromDatareader(oDrd("LastProduction")).ToString)
            If oDrd("HeredaDimensions") Then item.Add("HeredaDimensions", SQLHelper.GetBooleanFromDatareader(oDrd("HeredaDimensions")).ToString)
            If SQLHelper.GetBooleanFromDatareader(oDrd("IsBundle")) Then item.Add("IsBundle", SQLHelper.GetBooleanFromDatareader(oDrd("IsBundle")).ToString)
            If oDrd("Obsoleto") Then item.Add("Obsoleto", SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto")).ToString)
            If Not IsDBNull(oDrd("ObsoletoConfirmed")) Then item.Add("ObsoletoConfirmed", SQLHelper.GetFchFromDataReader(oDrd("ObsoletoConfirmed")).ToString("yyyy-MM-dd'T'HH:mm:ss"))
            If oMgz IsNot Nothing Then
                If Not IsDBNull(oDrd("Stock")) AndAlso oDrd("Stock") <> 0 Then item.Add("Stock", SQLHelper.GetIntegerFromDataReader(oDrd("Stock")).ToString)
                If Not IsDBNull(oDrd("Clients")) AndAlso oDrd("Clients") <> 0 Then item.Add("Clients", SQLHelper.GetIntegerFromDataReader(oDrd("Clients")).ToString())
                If Not IsDBNull(oDrd("ClientsAlPot")) AndAlso oDrd("ClientsAlPot") <> 0 Then item.Add("ClientsAlPot", SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")).ToString)
                If Not IsDBNull(oDrd("ClientsEnProgramacio")) AndAlso oDrd("ClientsEnProgramacio") <> 0 Then item.Add("ClientsEnProgramacio", SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")).ToString)
                If Not IsDBNull(oDrd("ClientsBlockStock")) AndAlso oDrd("ClientsBlockStock") <> 0 Then item.Add("ClientsBlockStock", SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")).ToString)
                If Not IsDBNull(oDrd("Pn1")) AndAlso oDrd("Pn1") <> 0 Then item.Add("Proveidors", SQLHelper.GetIntegerFromDataReader(oDrd("Pn1")).ToString)
                If Not IsDBNull(oDrd("Previsio")) AndAlso oDrd("Previsio") <> 0 Then item.Add("Previsions", SQLHelper.GetIntegerFromDataReader(oDrd("Previsio")).ToString)
            End If
            retval.Add(item)
            'Catch ex As Exception
            'Stop
            'End Try
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function SkuColorImages(oCategory As DTOProductCategory, Optional IncludeObsoletos As Boolean = False) As List(Of Byte())
        Dim retval As New List(Of Byte())

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Thumbnail ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("WHERE Art.Category='" & oCategory.Guid.ToString & "' ")
        If Not IncludeObsoletos Then
            sb.AppendLine("AND Art.Obsoleto=0 AND Art.NoWeb=0 ")
        End If
        sb.AppendLine("ORDER BY Art.Obsoleto, Art.Ord, SkuNom.Esp ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oImage = oDrd("Thumbnail")
            retval.Add(oImage)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Bundles(oCategory As DTOProductCategory, Optional oMgz As DTOMgz = Nothing, Optional IncludeObsoletos As Boolean = False) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT BundleArt.SkuGuid AS BundleGuid, BundleArt.SkuId AS BundleId, BundleRetail.Retail AS BundleRetail ")

        sb.AppendLine(", Art.Guid, Art.Art, Art.NoWeb, Art.NoEcom, VwRetail.Retail ")
        sb.AppendLine(", Art.HeredaDimensions, Art.InnerPack AS ArtInnerPack, Art.ForzarInnerPack AS ArtForzarInnerPack, Art.isBundle ")
        sb.AppendLine(", (CASE WHEN Art.[Image] IS NULL THEN 0 ELSE 1 END) AS Img ")
        sb.AppendLine(", Art.LastProduction, Art.Outlet, Art.OutletDto, Art.OutletQty, Art.Obsoleto, Art.CBar, Art.PackageEan, Art.NoPro ")

        sb.AppendLine(", BundleNom.Esp AS BundleNomEsp, BundleNom.Cat AS BundleNomCat, BundleNom.Eng AS BundleNomEng, BundleNom.Por AS BundleNomPor ")
        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
        sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

        If oMgz IsNot Nothing Then
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock, VwSkuStocks.Stock, Previsio.Previsio ")
        End If

        sb.AppendLine("FROM Art ")

        If oMgz IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN (SELECT Sku, SUM(Qty) AS Previsio  ")
            sb.AppendLine("                 FROM ImportPrevisio ")
            sb.AppendLine("                 GROUP BY Sku) AS Previsio ON ART.Guid=Previsio.Sku ")
        End If

        sb.AppendLine("LEFT OUTER JOIN VwRetail ON Art.Guid = VwRetail.SkuGuid ")

        'for bundles:
        sb.AppendLine("LEFT OUTER JOIN SkuBundle ON Art.Guid = SkuBundle.Sku ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail AS BundleRetail ON SkuBundle.Bundle = BundleRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom AS BundleArt ON SkuBundle.Bundle = BundleArt.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText BundleNom ON SkuBundle.Bundle = BundleNom.Guid AND BundleNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON SkuBundle.Bundle = Url.Guid ")

        sb.AppendLine("WHERE BundleArt.CategoryGuid ='" & oCategory.Guid.ToString & "' ")

        If Not IncludeObsoletos Then
            sb.AppendLine("AND BundleArt.Obsoleto=0 ")
        End If

        sb.AppendLine("ORDER BY BundleArt.Obsoleto, BundleArt.SkuNom, BundleArt.SkuGuid, SkuNom.Esp ")

        Dim oBundle As New DTOProductSku
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBundle.Guid.Equals(oDrd("BundleGuid")) Then
                oBundle = New DTOProductSku(oDrd("BundleGuid"))
                With oBundle
                    .Id = oDrd("BundleId")
                    .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("BundleRetail"))
                    SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "BundleNomEsp", "BundleNomCat", "BundleNomEng", "BundleNomPor")
                    .UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                End With
                retval.Add(oBundle)
            End If

            Dim item As New DTOSkuBundle()
            item.Sku = New DTOProductSku(oDrd("Guid"))
            With item.Sku
                .Category = oCategory
                .Id = oDrd("Art")
                .NoEcom = oDrd("NoEcom")
                .NoPro = oDrd("NoPro")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                SQLHelper.LoadLangTextFromDataReader(.NomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                .LastProduction = oDrd("LastProduction")
                .NoWeb = oDrd("NoWeb")
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                .Ean13 = SQLHelper.GetEANFromDataReader(oDrd("CBar"))
                .PackageEan = SQLHelper.GetEANFromDataReader(oDrd("PackageEan"))
                .InnerPack = oDrd("ArtInnerPack")
                .ForzarInnerPack = oDrd("ArtForzarInnerPack")
                .HeredaDimensions = oDrd("HeredaDimensions")
                .ImageExists = oDrd("Img")
                .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                '.virtual = oDrd("Virtual")
                .Outlet = oDrd("Outlet")
                .OutletDto = oDrd("OutletDto")
                .OutletQty = oDrd("OutletQty")
                .obsoleto = oDrd("Obsoleto")

                If oMgz IsNot Nothing Then
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1")) ' - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Previsions = SQLHelper.GetIntegerFromDataReader(oDrd("Previsio")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                End If
            End With
            oBundle.BundleSkus.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function All(oProveidor As DTOContact) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim SQL As String = "SELECT * FROM VwSkuNom WHERE VwSkuNom.Proveidor ='" & oProveidor.Guid.ToString & "' "
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function AllWithEan() As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid, Art.Art, Art.CBar, Art.PackageEan, Art.Obsoleto ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("WHERE CBar>'' ")
        sb.AppendLine("ORDER BY CBar")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As New DTOProductSku(oDrd("Guid"))
            With oSku
                .Id = oDrd("Art")
                .Ean13 = SQLHelper.GetEANFromDataReader(oDrd("CBar"))
                .PackageEan = SQLHelper.GetEANFromDataReader(oDrd("PackageEan"))
                .obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CompactTree(oEmp As DTOEmp, oLang As DTOLang, includeObsolets As Boolean) As DTOCatalog
        Dim retval As New DTOCatalog
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNomEsp, VwSkuNom.CodDist ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor  ")
        sb.AppendLine(", VwSkuNom.SkuId, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom, VwSkuNom.Ean13, VwSkuNom.Obsoleto  ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        If Not includeObsolets Then
            sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        End If
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom")
        sb.AppendLine(", VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom  ")
        sb.AppendLine(", VwSkuNom.Obsoleto, VwSkuNom.SkuOrd, VwSkuNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New DTOCatalog.Brand()
        Dim oCategory As New DTOCatalog.Category()
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOCatalog.Brand(oDrd("BrandGuid"), oDrd("BrandNomEsp"))
                With oBrand
                    .CodDist = oDrd("CodDist")
                    .Obsoleto = oDrd("Obsoleto")
                End With
                retval.Add(oBrand)
            End If
            If Not IsDBNull(oDrd("CategoryGuid")) Then
                If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                    Dim oNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    oCategory = New DTOCatalog.Category(oDrd("CategoryGuid"), oNom.Tradueix(oLang))
                    oBrand.Categories.Add(oCategory)
                End If
                If Not IsDBNull(oDrd("SkuGuid")) Then
                    Dim oNom = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    Dim oSku As New DTOCatalog.Sku(oDrd("SkuGuid"), oNom.Tradueix(oLang))
                    oSku.Id = oDrd("SkuId")
                    oCategory.Skus.Add(oSku)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CatalogBrands(oCustomer As DTOCustomer) As DTOCatalog
        Dim retval As New DTOCatalog
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandNom ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON VwCustomerSkus.Customer = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandNom, VwCustomerSkus.BrandOrd ")
        sb.AppendLine("ORDER BY VwCustomerSkus.BrandOrd, VwCustomerSkus.BrandNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oSkus As New List(Of DTOProductSku)
        Do While oDrd.Read
            Dim oBrand As New DTOCatalog.Brand(oDrd("BrandGuid"), oDrd("BrandNom"))
            retval.Add(oBrand)
        Loop
        oDrd.Close()
        Return retval
    End Function



    Shared Function CompactTree(oCustomer As DTOCustomer, oLang As DTOLang, oMgz As DTOMgz) As DTOCatalog
        Dim sb As New System.Text.StringBuilder

        Dim sSkuNomField = oLang.Tradueix("VwCustomerSkus.SkuNom", "VwCustomerSkus.SkuNomCat", "VwCustomerSkus.SkuNomEng", "VwCustomerSkus.SkuNomPor")
        sb.AppendLine("SELECT VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandNom ")
        sb.AppendLine(",VwCustomerSkus.CategoryGuid, VwCustomerSkus.CategoryNom ")
        sb.AppendLine(", VwCustomerSkus.SkuGuid, VwCustomerSkus.SkuNom, VwCustomerSkus.SkuNomCat, VwCustomerSkus.SkuNomEng, VwCustomerSkus.SkuNomPor, VwCustomerSkus.CodExclusio ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM VwCustomerSkus ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwCustomerSkus.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid ='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwCustomerSkus.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON VwCustomerSkus.Customer = VwCcxOrMe.Ccx ")
        sb.AppendLine("WHERE VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND VwCustomerSkus.CodExclusio IS NULL ")
        sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY VwCustomerSkus.BrandOrd, VwCustomerSkus.BrandNom, VwCustomerSkus.CategoryCodi, VwCustomerSkus.CategoryOrd, VwCustomerSkus.CategoryNom, VwCustomerSkus.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oSkus As New List(Of DTOProductSku)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
            End With
            oSkus.Add(oSku)

        Loop
        oDrd.Close()

        Dim retval = DTOCatalog.Factory(oSkus, oCustomer.Lang)
        Return retval
    End Function



    Shared Function CompactTree(oUser As DTOUser, Optional includeObsolets As Boolean = False) As DTOCatalog
        Dim retval As New DTOCatalog
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNom, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor  ")
        sb.AppendLine(", VwSkuNom.SkuId, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom, VwSkuNom.Ean13, VwSkuNom.Obsoleto  ")
        sb.AppendLine("FROM VwSkuNom ")
        If oUser.Rol.isStaff Then
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    sb.AppendLine("INNER JOIN VwCustomerSkus ON VwSkuNom.SkuGuid = VwCustomerSkus.SkuGuid AND VwCustomerSkus.Obsoleto = 0 ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer = Email_Clis.ContactGuid ")
                    sb.AppendLine("AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.rep
                    sb.AppendLine("INNER JOIN VwRepSkus ON VwSkuNom.SkuGuid = VwRepSkus.SkuGuid ")
                    sb.AppendLine("INNER JOIN Email_Clis ON VwRepSkus.Rep = Email_Clis.ContactGuid ")
                    sb.AppendLine("AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                Case DTORol.Ids.manufacturer
                    sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor = Email_Clis.ContactGuid ")
                    sb.AppendLine("AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            End Select
        End If
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oUser.Emp.Id & " ")
        If Not includeObsolets Then
            sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        End If
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom")
        sb.AppendLine(", VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom  ")
        sb.AppendLine(", VwSkuNom.Obsoleto, VwSkuNom.SkuOrd, VwSkuNom.SkuNom ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New DTOCatalog.Brand()
        Dim oCategory As New DTOCatalog.Category()
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOCatalog.Brand(oDrd("BrandGuid"), oDrd("BrandNom"))
                With oBrand
                    .Nom = oDrd("BrandNom")
                    .Obsoleto = oDrd("Obsoleto")
                End With
                retval.Add(oBrand)
            End If
            If Not IsDBNull(oDrd("CategoryGuid")) Then
                If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                    Dim oNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    oCategory = New DTOCatalog.Category(oDrd("CategoryGuid"), oNom.Tradueix(oUser.Lang))
                    oBrand.Categories.Add(oCategory)
                End If
                If Not IsDBNull(oDrd("SkuGuid")) Then
                    Dim oNom = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuNom", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    Dim oSku As New DTOCatalog.Sku(oDrd("SkuGuid"), oNom.Tradueix(oUser.Lang))
                    oSku.Id = oDrd("SkuId")
                    oCategory.Skus.Add(oSku)
                End If
            End If
        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function Relateds(oProduct As DTOProduct, oMgz As DTOMgz, oRelatedCod As DTOProductSku.Relateds) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid, Art.Art, VwSkuStocks.Stock, Preus.Retail, Art.IsBundle ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine(", (CASE WHEN Art.[Image] IS NULL THEN 0 ELSE 1 END) AS Img ")
        sb.AppendLine(", Art.LastProduction, Art.OutletDto, Previsio.Previsio AS Previsions, Art.Obsoleto ")

        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
        sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")

        sb.AppendLine("FROM VwProductParent ")
        sb.AppendLine("INNER JOIN ArtSpare ON VwProductParent.Parent = ArtSpare.TargetGuid")
        sb.AppendLine("INNER JOIN Art ON ArtSpare.ProductGuid = Art.Guid ")

        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")

        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid ='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT P2.Art, P2.Retail ")
        sb.AppendLine("                 FROM PriceListItem_Customer AS P2 ")
        sb.AppendLine("                 INNER JOIN PriceList_Customer AS P1 ON P2.PriceList = P1.Guid ")
        sb.AppendLine("                 INNER JOIN (SELECT MAX(dbo.PriceList_Customer.Fch) AS FCH, dbo.PriceListItem_Customer.Art FROM PriceList_Customer INNER JOIN PriceListItem_Customer ON dbo.PriceList_Customer.Guid = dbo.PriceListItem_Customer.PriceList AND PriceList_Customer.Fch <= GETDATE() AND PriceList_Customer.Customer IS NULL ")
        sb.AppendLine("                 GROUP BY PriceListItem_Customer.Art) AS P3 ON P1.Fch = P3.FCH And P3.Art = P2.Art) Preus ON Preus.Art=Art.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT Sku, SUM(Qty) AS Previsio ")
        sb.AppendLine("                 FROM ImportPrevisio ")
        sb.AppendLine("                 GROUP BY Sku) AS Previsio ON ART.Guid=Previsio.Sku ")

        sb.AppendLine("WHERE VwProductParent.Child ='" & oProduct.Guid.ToString & "' AND ArtSpare.Cod=" & oRelatedCod & " ")
        sb.AppendLine("ORDER BY Art.Obsoleto, Art.Ord, SkuNom.Esp ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductSku(oDrd("Guid"))
            With item
                .Id = oDrd("Art")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                SQLHelper.LoadLangTextFromDataReader(.NomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                .LastProduction = oDrd("LastProduction")
                .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("IsBundle"))
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock")) '+ SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1")) ' - SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Previsions = SQLHelper.GetIntegerFromDataReader(oDrd("Previsions")) '- SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                .ImageExists = oDrd("Img")
                .obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Search(oEmp As DTOEmp, searchKey As String, oMgz As DTOMgz, DtFch As Date) As DTOProductSku.Collection
        Dim retval As New DTOProductSku.Collection
        Dim searchkeys = searchKey.Split("+").ToList()
        Dim sb As New System.Text.StringBuilder


        sb.AppendLine("SELECT TOP(100)PERCENT VwSkuNom.* ")
        If oMgz IsNot Nothing Then
            sb.AppendLine(", VwSkuStocks.Stock ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        End If
        sb.AppendLine(", W.Retail ")
        sb.AppendLine("FROM VwSkuNom ")

        If oMgz IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid ='" & oMgz.Guid.ToString & "' ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        End If

        If DtFch = Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwRetail W ON VwSkuNom.SkuGuid = W.SkuGuid ")
        Else
            Dim sFch = Format(DtFch, "yyyyMMdd")
            sb.AppendLine("LEFT OUTER JOIN (")
            sb.AppendLine("SELECT        dbo.VwRetailPrice.Art AS SkuGuid, dbo.VwRetailPrice.Retail ")

            sb.AppendLine("FROM VwRetailPrice ")
            sb.AppendLine("INNER JOIN ( ")
            sb.AppendLine("         SELECT Y.Art, max(Y.PreFch) AS PreFch, min(Y.PostFch) AS PostFch FROM ( ")
            sb.AppendLine("             SELECT Art, Fch AS PreFch, NULL AS PostFch  ")
            sb.AppendLine("             FROM VwRetailPrice  ")
            sb.AppendLine("             WHERE Fch <= '" & sFch & "' ")
            sb.AppendLine("             UNION  ")
            sb.AppendLine("             SELECT Art, NULL AS PreFch, Fch AS PostFch  ")
            sb.AppendLine("             FROM VwRetailPrice  ")
            sb.AppendLine("             WHERE Fch > '" & sFch & "' ")
            sb.AppendLine("         ) Y GROUP BY Y.Art  ")
            sb.AppendLine(") X ON VwRetailPrice.Art = X.Art AND VwRetailPrice.Fch = (CASE WHEN X.PreFch IS NULL THEN X.PostFch ELSE X.PreFch END)  ")

            sb.AppendLine("		 UNION  ")
            sb.AppendLine("SELECT  dbo.SkuBundle.Bundle, SUM((dbo.SkuBundle.Qty * dbo.VwRetailPrice.Retail) * (100 - Bundle.BundleDto) / 100) AS Retail  ")
            sb.AppendLine("FROM            dbo.VwRetailPrice INNER JOIN  ")
            sb.AppendLine("                             (SELECT        Art, MAX(PreFch) AS PreFch, MIN(PostFch) AS PostFch  ")
            sb.AppendLine("                               FROM            (SELECT        Art, Fch AS PreFch, NULL AS PostFch  ")
            sb.AppendLine("                                                         FROM            dbo.VwRetailPrice AS VwRetailPrice_2  ")
            sb.AppendLine("                                                         WHERE        (Fch <= '" & sFch & "')  ")
            sb.AppendLine("                                                         UNION  ")
            sb.AppendLine("                                                         SELECT        Art, NULL AS PreFch, Fch AS PostFch  ")
            sb.AppendLine("                                                         FROM            dbo.VwRetailPrice AS VwRetailPrice_1  ")
            sb.AppendLine("                                                         WHERE        (Fch > '" & sFch & "')) AS Y  ")
            sb.AppendLine("                               GROUP BY Art) AS X ON dbo.VwRetailPrice.Art = X.Art AND dbo.VwRetailPrice.Fch = (CASE WHEN X.PreFch IS NULL THEN X.PostFch ELSE X.PreFch END) INNER JOIN  ")
            sb.AppendLine("                         dbo.SkuBundle ON X.Art = dbo.SkuBundle.Sku INNER JOIN  ")
            sb.AppendLine("                         dbo.ART AS Bundle ON dbo.SkuBundle.Bundle = Bundle.Guid  ")
            sb.AppendLine("						 GROUP BY  dbo.SkuBundle.Bundle  ")
            sb.AppendLine(") W ON VwSkuNom.SkuGuid = W.SkuGuid  ")
        End If

        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND ( ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.BrandNom", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.CategoryNom", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.SkuNomLlarg", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.SkuNom", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.SkuNomCat", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.SkuNomEng", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.SkuNomPor", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.SkuPrvNom", searchkeys) & "OR ")
        sb.AppendLine(SQLHelper.FieldValueContainsAll("VwSkuNom.SkuRef", searchkeys))

        'sb.AppendLine("     VwSkuNom.BrandNom LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.CategoryNom LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.SkuNomLlarg LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.SkuNom LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.SkuNomCat LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.SkuNomEng LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.SkuNomPor LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.SkuPrvNom LIKE '%" & searchKey & "%' ")
        'sb.AppendLine("     OR VwSkuNom.SkuRef LIKE '%" & searchKey & "%' ")
        If IsNumeric(searchKey) Then
            If Integer.TryParse(searchKey, 0) Then
                sb.AppendLine("OR VwSkuNom.SkuId = " & CInt(searchKey).ToString() & " ")
            Else
                sb.AppendLine("OR VwSkuNom.EAN13 LIKE '%" & searchKey & "%' ")
            End If
        End If
        sb.AppendLine(") ")

        sb.AppendLine("ORDER BY VwSkuNom.Obsoleto, VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNom, VwSkuNom.SkuNomLlarg ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                If oMgz IsNot Nothing Then
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                End If
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()


        Return retval
    End Function

    Shared Function SimpleSearch(oEmp As DTOEmp, oMgz As DTOMgz, searchkey As String) As DTOProductSku.Collection
        Dim retval As New DTOProductSku.Collection
        Dim sb As New System.Text.StringBuilder

        sb = New Text.StringBuilder()
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNomLlarg, VwSkuNom.SkuNom ")
        sb.AppendLine(", VwSkuNom.Skuid, VwSkuNom.SkuPrvNom, VwSkuNom.SkuRef, VwSkuNom.EAN13 ")
        sb.AppendLine(", VwSkuNom.LastProduction, VwSkuNom.Obsoleto ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", (CASE WHEN VwSkuNom.isBundle = 0 THEN VwSkuRetail.Retail ELSE VwBundleRetail.Retail END) AS Retail ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwBundleRetail ON VwSkuNom.SkuGuid = VwBundleRetail.Bundle ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON VwSkuNom.SkuGuid = VwSkuRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString() & "' ")
        sb.AppendLine("WHERE ")
        sb.AppendLine("VwSkuNom.Emp = " & CInt(oEmp.Id).ToString() & " ")
        sb.AppendLine("AND ( ")
        sb.AppendLine("     VwSkuNom.BrandNom LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.CategoryNom LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.SkuNomLlarg LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.SkuNom LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.SkuNomCat LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.SkuNomEng LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.SkuNomPor LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.SkuPrvNom LIKE '%" & searchkey & "%' ")
        sb.AppendLine("     OR VwSkuNom.SkuRef LIKE '%" & searchkey & "%' ")
        If IsNumeric(searchkey) Then
            If Integer.TryParse(searchkey, 0) Then
                sb.AppendLine("OR VwSkuNom.SkuId = " & CInt(searchkey).ToString() & " ")
            Else
                sb.AppendLine("OR VwSkuNom.EAN13 LIKE '%" & searchkey & "%' ")
            End If
        End If
        sb.AppendLine(") ")
        sb.AppendLine("ORDER BY VwSkuNom.Obsoleto, VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid ")
        sb.AppendLine(", VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom ")
        sb.AppendLine(", VwSkuNom.SkuOrd, VwSkuNom.SkuNom ")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Search(oEmp As DTOEmp, skuIds As List(Of Integer)) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder

        sb = New Text.StringBuilder()
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	     skuId int NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(skuId) ")

        Dim idx As Integer = 0
        For Each skuId In skuIds
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.Append("(" & skuId & ")")
            idx += 1
        Next
        sb.AppendLine(" ")

        sb.AppendLine("SELECT TOP(100)PERCENT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("INNER JOIN @Table X ON VwSkuNom.SkuId = X.SkuId AND VwSkuNom.Emp = " & oEmp.Id & " ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(oSku)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Ids(oEmp As DTOEmp) As List(Of DTOProductSkuId)
        Dim retval As New List(Of DTOProductSkuId)
        Dim sb As New Text.StringBuilder()
        sb.AppendLine("SELECT Art.Guid, Art.Art, Art.Ref, Art.Cbar ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("WHERE Art.Emp = " & oEmp.Id & " ")
        sb.AppendLine("ORDER BY CBar ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As New DTOProductSkuId
            With oSku
                .Guid = oDrd("Guid")
                .Id = oDrd("Art")
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .Ean = SQLHelper.GetStringFromDataReader(oDrd("CBar"))
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Search(oCustomer As DTOCustomer, oEans As List(Of DTOEan), oMgz As DTOMgz) As DTOProductSku.Collection
        Dim retval As New DTOProductSku.Collection
        If oEans.Count > 0 Then

            Dim sb As New System.Text.StringBuilder

            sb = New Text.StringBuilder()
            sb.AppendLine("DECLARE @Table TABLE( ")
            sb.AppendLine("	      Idx int NOT NULL")
            sb.AppendLine("	    , EAN13 varchar(13) NOT NULL ")
            sb.AppendLine("        ) ")
            sb.AppendLine("INSERT INTO @Table(Idx, EAN13) ")

            Dim idx As Integer = 0
            For Each oEan In oEans
                sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
                sb.AppendFormat("({0},'{1}') ", idx, oEan.Value.ToString())
                idx += 1
            Next

            sb.AppendLine("SELECT VwSkuNom.SkuGuid, X.Ean13 ")
            sb.AppendLine(", VwCustomerSkusLite.ExclusionCod ")
            sb.AppendLine(", CliDto.Dto ")
            sb.AppendLine("FROM VwCustomerSkusLite ")
            sb.AppendLine("INNER JOIN VwSkuNom ON VwCustomerSkusLite.SkuGuid = VwSkuNom.SkuGuid ")
            sb.AppendLine("INNER JOIN @Table X ON VwSkuNom.Ean13 = X.Ean13 ")
            sb.AppendLine("LEFT OUTER JOIN CliDto On VwCustomerSkusLite.Ccx = CliDto.Customer And (VwSkuNom.BrandGuid = CliDto.Brand OR VwSkuNom.CategoryGuid = CliDto.Brand OR VwSkuNom.SkuGuid = CliDto.Brand) ")
            sb.AppendLine("WHERE VwCustomerSkusLite.Customer = '" & oCustomer.Guid.ToString() & "' ")
            sb.AppendLine("GROUP BY VwSkuNom.SkuGuid, X.Ean13 ")
            sb.AppendLine(", VwCustomerSkusLite.ExclusionCod, CliDto.Dto ")
            sb.AppendLine(", VwSkuNom.Obsoleto, VwSkuNom.BrandOrd, VwSkuNom.CategoryOrd, VwSkuNom.SkuNomLlarg, X.Idx ")
            sb.AppendLine("ORDER BY X.Idx")


            Dim oSkus As New DTOProductSku.Collection
            Dim SQL = sb.ToString()
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oSku As New DTOProductSku(oDrd("SkuGuid"))
                With oSku
                    .CodExclusio = oDrd("ExclusionCod")
                    .CustomerDto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                    .Ean13 = DTOEan.Factory(oDrd("EAN13"))
                End With
                oSkus.Add(oSku)
            Loop
            oDrd.Close()

            retval = FillSkus(oSkus, oMgz)

            'afegeix els productes no trobats amb codi 'fora de cataleg', guid de producte desconegut i Ean a la descripció
            Dim oEanValues = oEans.Select(Function(x) x.Value).ToList()
            Dim oSkuEanValues = oSkus.Select(Function(x) x.Ean13.Value).ToList()
            Dim oMissingEanValues = oEanValues.Except(oSkuEanValues).ToList()
            For Each EanValue In oMissingEanValues
                Dim oSku = DTOProductSku.Wellknown(DTOProductSku.Wellknowns.UnknownSku)
                With oSku
                    .Ean13 = DTOEan.Factory(EanValue)
                    .NomLlarg.Esp = String.Format("producte amb Ean '{0}' desconegut", EanValue)
                    .CodExclusio = DTOProductSku.CodisExclusio.OutOfCatalog
                End With
                retval.Add(oSku)
            Next

        End If

        Return retval
    End Function

    Shared Function Search(oEmp As DTOEmp, oLang As DTOLang, sSearchKey As String) As List(Of DTOProductSku.Compact)
        Dim retval As New List(Of DTOProductSku.Compact)
        Dim searchTerms = sSearchKey.Split("+").ToList()
        Dim langField As String = ""
        If oLang.id <> DTOLang.Ids.ESP Then langField = oLang.Tag
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Art.Guid, Art.Art, Art.Obsoleto AS ArtObsoleto, Stp.Obsoleto AS StpObsoleto, Tpa.Obsoleto AS TpaObsoleto ")
        sb.AppendLine(", VwLangText.Esp, VwLangText.Cat, VwLangText.Eng, VwLangText.Por ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category = Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText ON Art.Guid = VwLangText.Guid AND VwLangText.Src = 27 ")
        sb.AppendLine("WHERE Tpa.Emp = " & oEmp.Id & " ")

        sb.AppendLine("AND ( ")

        If IsNumeric(sSearchKey) Then
            sb.AppendLine("( ")
            sb.AppendLine("Art.Art = " & sSearchKey & " ")
            sb.AppendLine("OR ")
            sb.AppendLine("Art.Cbar = '" & sSearchKey & "' ")
            sb.AppendLine("OR ")
            sb.AppendLine("Art.Ref = '" & sSearchKey & "' ")
            sb.AppendLine(") ")
        End If

        If IsNumeric(sSearchKey) Then
            sb.Append(" OR (")
        End If

        For Each searchTerm In searchTerms
            If searchTerms.IndexOf(searchTerm) > 0 Then sb.Append("AND ")
            sb.Append("(")
            sb.Append("VwLangText.Esp COLLATE Latin1_General_CI_AI LIKE '%" & searchTerm & "%' ")
            If langField > "" Then
                sb.Append("OR VwLangText." & langField & " COLLATE Latin1_General_CI_AI LIKE '%" & searchTerm & "%' ")
            End If
            sb.AppendLine(") ")
        Next

        If IsNumeric(sSearchKey) Then
            sb.Append(" )")
        End If

        sb.AppendLine(" )")

        sb.AppendLine("ORDER BY Tpa.Obsoleto, Stp.Obsoleto, Art.Obsoleto, VwLangText.Esp")

        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As New DTOProductSku.Compact
            With oSku
                .Guid = oDrd("Guid")
                .Id = oDrd("Art")
                .NomLlarg = DTOLangText.Compact.Factory(oLang, oDrd("Esp"), oDrd("Cat"), oDrd("Eng"), oDrd("Por"))
                .Obsoleto = (oDrd("ArtObsoleto") Or oDrd("StpObsoleto") Or oDrd("TpaObsoleto"))
            End With
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromGuids(oGuids As List(Of Guid), Optional oMgz As DTOMgz = Nothing) As DTOProductSku.Collection
        Dim retval As New DTOProductSku.Collection
        If oGuids.Count > 0 Then
            Dim sb As New Text.StringBuilder
            sb = New Text.StringBuilder()
            sb.AppendLine("DECLARE @Table ")
            sb.AppendLine("TABLE(Guid uniqueidentifier NOT NULL) ")
            sb.AppendLine("INSERT INTO @Table(Guid) ")
            sb.AppendLine("VALUES ")

            For Each oGuid In oGuids.Distinct()
                If oGuids.IndexOf(oGuid) > 0 Then sb.AppendLine(", ")
                sb.Append("('" & oGuid.ToString() & "')")
            Next

            sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.SkuNomLlarg, VwSkuNom.LastProduction, VwSkuNom.Obsoleto, VwSkuNom.FchObsoleto ")
            sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
            sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
            sb.AppendLine(", VwSkuNom.CategoryMoq, VwSkuNom.CategoryForzarMoq, VwSkuNom.CategoryCodi ")
            sb.AppendLine(", VwSkuNom.SkuMoq, VwSkuNom.SkuForzarMoq ")
            sb.AppendLine(", VwSkuNom.Ean13, VwSkuNom.IsBundle ")
            sb.AppendLine(", VwRetail.Retail ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")

            If oMgz IsNot Nothing Then
                sb.AppendLine(", VwSkuStocks.Stock ")
            End If

            sb.AppendLine("FROM @Table X ")
            sb.AppendLine("INNER JOIN VwSkuNom ON X.Guid = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwRetail ON X.Guid = VwRetail.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON X.Guid = VwSkuPncs.SkuGuid ")

            If oMgz IsNot Nothing Then
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON X.Guid = VwSkuStocks.SkuGuid And VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
            End If

            sb.AppendLine("GROUP BY VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.SkuNomLlarg, VwSkuNom.LastProduction, VwSkuNom.Obsoleto, VwSkuNom.FchObsoleto ")
            sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom, VwSkuNom.BrandOrd ")
            sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryOrd ")
            sb.AppendLine(", VwSkuNom.CategoryMoq, VwSkuNom.CategoryForzarMoq, VwSkuNom.CategoryCodi ")
            sb.AppendLine(", VwSkuNom.SkuMoq, VwSkuNom.SkuForzarMoq ")
            sb.AppendLine(", VwSkuNom.Ean13, VwSkuNom.IsBundle ")
            sb.AppendLine(", VwRetail.Retail ")

            If oMgz IsNot Nothing Then
                sb.AppendLine(", VwSkuStocks.Stock ")
            End If

            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
            sb.AppendLine("ORDER BY VwSkuNom.Obsoleto, VwSkuNom.BrandOrd, VwSkuNom.CategoryOrd, VwSkuNom.SkuNomLlarg ")

            Dim SQL = sb.ToString

            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                With oSku

                    .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                    If oMgz IsNot Nothing Then .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                    If Not IsDBNull(oDrd("Retail")) Then
                        .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                        '.price = .rrpp.deductPercent(retailOrChannelDto)
                    End If

                End With
                retval.Add(oSku)
            Loop
            oDrd.Close()
        End If
        Return retval
    End Function

    Private Shared Function FillSkus(oSkus As List(Of DTOProductSku), oMgz As DTOMgz) As DTOProductSku.Collection
        Dim retval As New DTOProductSku.Collection
        If oSkus.Count > 0 Then
            Dim sb As New Text.StringBuilder
            sb = New Text.StringBuilder()
            sb.AppendLine("DECLARE @Table ")
            sb.AppendLine("TABLE(Guid uniqueidentifier NOT NULL) ")
            sb.AppendLine("INSERT INTO @Table(Guid) ")
            sb.AppendLine("VALUES ")

            For Each oSku In oSkus
                If oSkus.IndexOf(oSku) > 0 Then sb.AppendLine(", ")
                sb.Append("('" & oSku.Guid.ToString() & "')")
            Next

            sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.SkuNomLlarg, VwSkuNom.LastProduction, VwSkuNom.Obsoleto, VwSkuNom.FchObsoleto ")
            sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
            sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
            sb.AppendLine(", VwSkuNom.CategoryMoq, VwSkuNom.CategoryForzarMoq, VwSkuNom.CategoryCodi ")
            sb.AppendLine(", VwSkuNom.SkuMoq, VwSkuNom.SkuForzarMoq ")
            sb.AppendLine(", VwSkuNom.Ean13, VwSkuNom.IsBundle ")
            sb.AppendLine(", VwRetail.Retail ")
            sb.AppendLine(", VwSkuStocks.Stock ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
            sb.AppendLine("FROM @Table X ")
            sb.AppendLine("INNER JOIN VwSkuNom ON X.Guid = VwSkuNom.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwRetail ON X.Guid = VwRetail.SkuGuid ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON X.Guid = VwSkuStocks.SkuGuid And VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON X.Guid = VwSkuPncs.SkuGuid ")

            sb.AppendLine("GROUP BY VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.SkuNomLlarg, VwSkuNom.LastProduction, VwSkuNom.Obsoleto, VwSkuNom.FchObsoleto ")
            sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom, VwSkuNom.BrandOrd ")
            sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryOrd ")
            sb.AppendLine(", VwSkuNom.CategoryMoq, VwSkuNom.CategoryForzarMoq, VwSkuNom.CategoryCodi ")
            sb.AppendLine(", VwSkuNom.SkuMoq, VwSkuNom.SkuForzarMoq ")
            sb.AppendLine(", VwSkuNom.Ean13, VwSkuNom.IsBundle ")
            sb.AppendLine(", VwRetail.Retail ")
            sb.AppendLine(", VwSkuStocks.Stock ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
            sb.AppendLine("ORDER BY VwSkuNom.Obsoleto, VwSkuNom.BrandOrd, VwSkuNom.CategoryOrd, VwSkuNom.SkuNomLlarg ")

            Dim SQL = sb.ToString

            Dim oDrd = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim pSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                With pSku
                    .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                    .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                    If Not IsDBNull(oDrd("Retail")) Then
                        .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                        '.price = .rrpp.deductPercent(retailOrChannelDto)
                    End If
                End With

                Dim oSku = oSkus.FirstOrDefault(Function(x) x.Equals(pSku))
                If oSku IsNot Nothing Then
                    pSku.CodExclusio = oSku.CodExclusio
                    pSku.CustomerDto = oSku.CustomerDto
                End If

                retval.Add(pSku)
            Loop
            oDrd.Close()
        End If
        Return retval
    End Function

    Shared Function LoadFromCustomer(oSkuGuid As Guid, oCustomer As DTOCustomer, oMgz As DTOMgz) As DTOProductSku
        Dim retval As DTOProductSku = Nothing
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine(", VwRetail.Retail ")
        sb.AppendLine(", VwCustomerSkusExcluded.Cod AS CodExclusio ")
        'sb.AppendLine(", VwCustomerDto.Dto AS RetailDto ")
        'sb.AppendLine(", CliDto.Dto AS Dto ")
        sb.AppendLine(", VwSkuNom.SkuMoq, VwSkuNom.IsBundle ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON VwSkuNom.SkuGuid = VwRetail.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid And VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwCcxOrMe ON VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")
        'sb.AppendLine("LEFT OUTER JOIN VwCustomerDto ON (VwSkuNom.BrandGuid = VwCustomerDto.Product Or VwSkuNom.CategoryGuid = VwCustomerDto.Product Or VwSkuNom.SkuGuid = VwCustomerDto.Product) AND VwCustomerDto.Customer = VwCcxOrMe.Ccx ")
        'sb.AppendLine("LEFT OUTER JOIN VwCustomerDto ON (VwSkuNom.BrandGuid = VwCustomerDto.Product Or VwSkuNom.CategoryGuid = VwCustomerDto.Product Or VwSkuNom.SkuGuid = VwCustomerDto.Product) ")
        'sb.AppendLine("LEFT OUTER JOIN CliDto On VwCcxOrMe.Ccx = CliDto.Customer And (VwSkuNom.BrandGuid = CliDto.Brand OR VwSkuNom.CategoryGuid = CliDto.Brand OR VwSkuNom.SkuGuid = CliDto.Brand) ")
        sb.AppendLine("LEFT OUTER JOIN VwCustomerSkusExcluded ON VwCcxOrMe.Ccx = VwCustomerSkusExcluded.Customer And VwSkuNom.SkuGuid = VwCustomerSkusExcluded.Sku ")
        sb.AppendLine("WHERE VwSkuNom.SkuGuid = '" & oSkuGuid.ToString & "' ")
        'sb.AppendLine("AND VwCcxOrMe.Guid = '" & oCustomer.Guid.ToString & "' ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = SQLHelper.GetProductFromDataReader(oDrd)
            With retval
                .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
                If Not IsDBNull(oDrd("Retail")) Then
                    .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                End If
                '.CustomerDto = SQLHelper.GetDecimalFromDataReader(oDrd("Dto"))
                .CodExclusio = SQLHelper.GetIntegerFromDataReader(oDrd("CodExclusio"))
            End With
        End If
        oDrd.Close()

        Return retval
    End Function



    Shared Function FromCustomerOrders(oContact As DTOContact, oCategory As DTOProductCategory) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Art.Guid, Art.NoWeb, Art.NoEcom, Art.Obsoleto, Art.isBundle ")
        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
        sb.AppendLine("FROM Pdc ")
        sb.AppendLine("INNER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("INNER JOIN Art ON Pnc.ArtGuid = Art.Guid ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("WHERE Pdc.CliGuid = '" & oContact.Guid.ToString & "' ")
        sb.AppendLine("AND Art.Category = '" & oCategory.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Art.Guid, Art.NoWeb, Art.NoEcom, Art.Obsoleto, Art.isBundle ")
        sb.AppendLine(", SkuNom.Esp, SkuNom.Cat, SkuNom.Eng, SkuNom.Por ")
        sb.AppendLine("ORDER BY Art.Obsoleto, SkuNom.Esp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductSku(oDrd("Guid"))
            With item
                .Category = oCategory
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                .NoWeb = oDrd("NoWeb")
                .NoEcom = oDrd("NoEcom")
                .IsBundle = SQLHelper.GetBooleanFromDatareader(oDrd("isBundle"))
                .obsoleto = oDrd("Obsoleto")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ObsoletsCount(oEmp As DTOEmp, FromFch As Date) As Integer
        Dim retval As Integer
        Dim fchFrom = FromFch.ToString("o")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT COUNT(Art.Guid) AS SkusCount ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE Art.Obsoleto <> 0 ")
        sb.AppendLine("AND VwSkuNom.BrandNom<>'VARIOS' AND VwSkuNom.CategoryCodi<2 AND Art.FchObsoleto > CAST('" & fchFrom & "' AS datetimeoffset) ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()
        retval = SQLHelper.GetIntegerFromDataReader(oDrd("SkusCount"))
        oDrd.Close()
        Return retval
    End Function

    Shared Function Obsolets(oUser As DTOUser, oLang As DTOLang, FromFch As Date) As MatHelper.Excel.Sheet
        Dim caption = String.Format("M+O {0} {1:yyyy-MM-dd HH.mm} - {2:yyyy-MM-dd HH.mm}", oLang.Tradueix("Productos Descatalogados", "Productes descatalogats", "Outdated products"), FromFch, DTO.GlobalVariables.Now())
        Dim retval As New MatHelper.Excel.Sheet(caption, caption & ".xlsx")
        With retval
            .AddColumn(oLang.Tradueix("Fecha", "Data", "Date"))
            .AddColumn(oLang.Tradueix("ref.M+O", "ref.M+O", "M+O code"))
            .AddColumn(oLang.Tradueix("ref.Fabricante", "ref.Fabricant", "Manufacturer code"))
            .AddColumn("EAN 13")
            .AddColumn(oLang.Tradueix("Marca comercial", "Marca comercial", "Brand"))
            .AddColumn(oLang.Tradueix("Categoría", "Categoria", "Category"))
            .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"))
            .AddColumn(oLang.Tradueix("Nueva ref.M+O", "Nova ref.M+O", "New M+O code"))
            .AddColumn(oLang.Tradueix("Nueva ref.Fabricante", "Nova ref.Fabricant", "New Manufacturer code"))
            .AddColumn(oLang.Tradueix("Nuevo EAN 13", "Nou EAN 13", "New EAN 13 code"))
            .AddColumn(oLang.Tradueix("Nuevo Producto", "Nou Producte", "New Product"))
        End With

        Dim fchFrom = FromFch.ToString("o")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.SkuRef, VwSkuNom.EAN13, VwSkuNom.SkuNom, Art.FchObsoleto ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom, VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
        sb.AppendLine(", Substitute.SkuId AS SubstituteId, Substitute.SkuRef AS SubstituteSkuRef, Substitute.Ean13 AS SubstituteEan13, Substitute.SkuNom AS SubstituteNom ")
        sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom Substitute ON Art.Substitute = Substitute.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                Dim oProveidors = UserLoader.GetProveidors(oUser)
                If oProveidors.Count = 0 Then
                    sb.AppendLine("WHERE 1=2 ")
                Else
                    sb.AppendLine("WHERE VwSkuNom.Proveidor = '" & oProveidors.First.Guid.ToString() & "' ")
                End If

            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                Dim oCustomers = UserLoader.GetCustomers(oUser)
                If oCustomers.Count = 0 Then
                    sb.AppendLine("WHERE 1=2 ")
                Else
                    Dim oCustomer = oCustomers.First
                    sb.AppendLine("INNER JOIN VwCustomerSkusLite ON Art.Guid = VwCustomerSkusLite.SkuGuid ")
                    sb.AppendLine("WHERE VwCustomerSkusLite.Customer = '" & oCustomer.Guid.ToString() & "' AND VwCustomerSkusLite.ExclusionCod=4 ")
                End If
            Case Else
                sb.AppendLine("WHERE 1=1 ")
        End Select

        sb.AppendLine("AND VwSkuNom.BrandNom<>'VARIOS' AND VwSkuNom.CategoryCodi<2 AND Art.FchObsoleto > CAST('" & fchFrom & "' AS datetimeoffset) ")
        sb.AppendLine("ORDER BY Art.FchObsoleto DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            Dim oSkuUrl = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
            Dim sSkuUrl = If(oSkuUrl Is Nothing, "", oSkuUrl.AbsoluteUrl(oLang))
            Dim oCategoryUrl = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
            Dim sCategoryUrl = If(oCategoryUrl Is Nothing, "", oCategoryUrl.AbsoluteUrl(oLang))
            Dim oBrandUrl = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
            Dim sBrandUrl = If(oBrandUrl Is Nothing, "", oBrandUrl.AbsoluteUrl(oLang))
            Dim oRow = retval.AddRow
            With oRow
                .AddCell(SQLHelper.GetDateTimeOffsetFromDataReader(oDrd("FchObsoleto")).ToString("dd/MM/yy HH:mm"))
                .AddCell(oSku.Id, sSkuUrl)
                .AddCell(oSku.RefProveidor)
                .AddCell(DTOProductSku.Ean(oSku))
                .AddCell(oSku.Category.Brand.Nom.Tradueix(oLang), sBrandUrl)
                .AddCell(oSku.Category.Nom.Tradueix(oLang), sCategoryUrl)
                .AddCell(oSku.Nom.Tradueix(oLang), sSkuUrl)
                .AddCell(SQLHelper.GetStringFromDataReader(oDrd("SubstituteId")))
                .AddCell(SQLHelper.GetStringFromDataReader(oDrd("SubstituteSkuRef")))
                .AddCell(SQLHelper.GetStringFromDataReader(oDrd("SubstituteEan13")))
                .AddCell(SQLHelper.GetStringFromDataReader(oDrd("SubstituteNom")))
            End With
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Descatalogats(oUser As DTOUser, ExcludeConfirmed As Boolean) As List(Of DTODescatalogat)
        Dim retval As New List(Of DTODescatalogat)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.SkuRef, VwSkuNom.EAN13, VwSkuNom.SkuNom, Art.FchObsoleto ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNom, VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
        sb.AppendLine(", Art.Substitute, Art.ObsoletoConfirmed, VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.Pn1 ")
        sb.AppendLine(", Substitute.SkuId AS SubstituteId, Substitute.SkuRef AS SubstituteSkuRef, Substitute.Ean13 AS SubstituteEan13, Substitute.SkuNom AS SubstituteNom ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Art.Guid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Emp ON VwSkuNom.Emp = Emp.Emp ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = Emp.Mgz ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuNom Substitute ON Art.Substitute = Substitute.SkuGuid ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.manufacturer
                Dim oProveidors = UserLoader.GetProveidors(oUser)
                If oProveidors.Count = 0 Then
                    sb.AppendLine("WHERE 1=2 ")
                Else
                    sb.AppendLine("WHERE VwSkuNom.Proveidor = '" & oProveidors.First.Guid.ToString() & "' ")
                End If

            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                Dim oCustomers = UserLoader.GetCustomers(oUser)
                If oCustomers.Count = 0 Then
                    sb.AppendLine("WHERE 1=2 ")
                Else
                    Dim oCustomer = oCustomers.First
                    sb.AppendLine("INNER JOIN VwCustomerSkusLite ON Art.Guid = VwCustomerSkusLite.SkuGuid ")
                    sb.AppendLine("WHERE VwCustomerSkusLite.Customer = '" & oCustomer.Guid.ToString() & "' AND VwCustomerSkusLite.ExclusionCod=4 ")
                End If
            Case Else
                sb.AppendLine("WHERE 1=1 ")
        End Select

        If ExcludeConfirmed Then
            sb.AppendLine("AND Art.ObsoletoConfirmed IS NULL ")
        End If

        sb.AppendLine("AND VwSkuNom.Emp = " & oUser.Emp.Id & " ")
        sb.AppendLine("AND VwSkuNom.BrandNom<>'VARIOS' AND VwSkuNom.CategoryCodi<2 ")
        sb.AppendLine("ORDER BY Art.FchObsoleto DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            Dim iStock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
            Dim iCustomerPending = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            Dim iSupplierPending = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
            Dim item As New DTODescatalogat(oSku.Guid)
            With item
                .Fch = SQLHelper.GetFchFromDataReader(oDrd("FchObsoleto"))
                .Nom = oSku.NomCurt(oUser.Lang)
                .Brand = DTOGuidNom.Compact.Factory(DTOProductSku.Brand(oSku).Guid, oSku.brandNom())
                .Category = DTOGuidNom.Compact.Factory(oSku.Category.Guid, oSku.categoryNom())
                .Id = oSku.Id
                .Ref = oSku.RefProveidor
                .Ean = DTOProductSku.Ean(oSku).ToString
                .Warn = iStock <> 0 Or iCustomerPending <> 0 Or iSupplierPending <> 0
                .Confirmed = Not IsDBNull(oDrd("ObsoletoConfirmed"))
                If Not IsDBNull(oDrd("Substitute")) Then
                    .Substitute = New DTODescatalogat(oDrd("Substitute"))
                    With .Substitute
                        .Nom = SQLHelper.GetStringFromDataReader(oDrd("SubstituteNom"))
                        .Id = SQLHelper.GetStringFromDataReader(oDrd("SubstituteId"))
                        .Ref = SQLHelper.GetStringFromDataReader(oDrd("SubstituteSkuRef"))
                        .Ean = SQLHelper.GetStringFromDataReader(oDrd("SubstituteEan13"))
                    End With
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ConfirmDescatalogats(exs As List(Of Exception), oGuids As List(Of Guid)) As Boolean
        Dim sb As New System.Text.StringBuilder

        sb = New Text.StringBuilder()
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	     Guid uniqueIdentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.Append("('" & oGuid.ToString() & "')")
            idx += 1
        Next
        sb.AppendLine(" ")

        sb.AppendLine("UPDATE Art ")
        sb.AppendLine("SET Art.ObsoletoConfirmed =GETDATE() ")
        sb.AppendLine(", Art.Obsoleto = 1 ")
        sb.AppendLine(", Art.FchObsoleto = (CASE WHEN FchObsoleto IS NULL THEN GETDATE() ELSE FchObsoleto END) ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN @Table X ON Art.Guid = X.Guid ")

        Dim SQL As String = sb.ToString
        Dim rc = SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval

    End Function

    Shared Function FromRefProveidor(oEmp As DTOEmp, src As String) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND VwSkuNom.SkuRef = '" & src & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromEanValues(eanValues As HashSet(Of String), Optional oMgz As DTOMgz = Nothing) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Ean VARCHAR(13) NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Ean) ")

        Dim idx As Integer = 0
        For Each eanValue In eanValues
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, eanValue)
            idx += 1
        Next

        sb.AppendLine("SELECT VwSkuNom.*, VwSkuRetail.Retail ")
        If oMgz IsNot Nothing Then
            sb.AppendLine(", VwSkuStocks.Stock ")
            sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        End If
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuRetail ON VwSkuNom.SkuGuid = VwSkuRetail.SkuGuid ")
        If oMgz IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid And VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString() & "' ")
            sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        End If
        sb.AppendLine("INNER JOIN @Table X ON VwSkuNom.Ean13 = X.Ean ")
        sb.AppendLine("ORDER BY X.Idx ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With item
                If oMgz IsNot Nothing Then
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                    .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                    .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                    .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                End If
                .obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
                .Rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
            End With

            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Sub LoadImages(oSkus As List(Of DTOProductSku))
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Art.Guid, Art.Image ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("WHERE ( ")
        Dim FirstRec As Boolean = True
        For Each oSku As DTOProductSku In oSkus
            If Not FirstRec Then
                sb.Append("OR ")
            End If
            FirstRec = False
            sb.Append("Art.Guid='" & oSku.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oSku As DTOProductSku = oSkus.FirstOrDefault(Function(x) x.Guid.Equals(oGuid))
            oSku.Image = oDrd("Image")
        Loop
        oDrd.Close()
    End Sub


    Shared Function LastImageFch(oSkus As List(Of DTOProductSku)) As DateTime
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT MAX(Art.ImgFch) AS LastImgFch ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("WHERE ( ")
        Dim FirstRec As Boolean = True
        For Each oSku As DTOProductSku In oSkus
            If Not FirstRec Then
                sb.Append("OR ")
            End If
            FirstRec = False
            sb.Append("Art.Guid='" & oSku.Guid.ToString & "' ")
        Next
        sb.AppendLine(")")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()

        Dim retval As DateTime = Date.MinValue
        If Not IsDBNull(oDrd("LastImgFch")) Then
            retval = SQLHelper.GetFchFromDataReader(oDrd("LastImgFch"))
        End If
        oDrd.Close()
        Return retval
    End Function


    Shared Function Sprite(oGuids As List(Of Guid), itemWidth As Integer, itemHeight As Integer) As List(Of Byte())
        Dim sb As New Text.StringBuilder
        sb.AppendLine("DECLARE @Table TABLE( ")
        sb.AppendLine("	      Idx int NOT NULL")
        sb.AppendLine("	    , Guid uniqueidentifier NOT NULL ")
        sb.AppendLine("        ) ")
        sb.AppendLine("INSERT INTO @Table(Idx,Guid) ")

        Dim idx As Integer = 0
        For Each oGuid In oGuids
            sb.AppendLine(IIf(idx = 0, "VALUES ", ", "))
            sb.AppendFormat("({0},'{1}') ", idx, oGuid.ToString())
            idx += 1
        Next

        sb.AppendLine()
        sb.AppendLine("SELECT Art.Thumbnail ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN @Table X ON Art.Guid = X.Guid ")
        sb.AppendLine("ORDER BY X.Idx")
        Dim SQL = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim retval As New List(Of Byte())
        Do While oDrd.Read
            Dim oImage = oDrd("Thumbnail")
            retval.Add(oImage)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Sprite(oCategory As DTOProductCategory) As List(Of Byte())
        Dim retval As New List(Of Byte())

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Thumbnail ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("WHERE Art.Category='" & oCategory.Guid.ToString & "' ")
        sb.AppendLine("AND Art.Obsoleto=0 ")
        sb.AppendLine("ORDER BY SkuNom.Esp ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oImage = oDrd("Thumbnail")
            retval.Add(oImage)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class


