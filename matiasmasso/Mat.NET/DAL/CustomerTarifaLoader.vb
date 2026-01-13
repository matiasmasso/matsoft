Public Class CustomerTarifaLoader

End Class

Public Class CustomerTarifaItemsLoader

    Shared Function Items(oUserOrContact As DTOBaseGuid, DtFch As Date, Optional oMgz As DTOMgz = Nothing, Optional oLang As DTOLang = Nothing, Optional IncludeObsoletos As Boolean = False) As List(Of DTOProductBrand.Treenode)
        'web
        Dim retval As New List(Of DTOProductBrand.Treenode)
        Dim oEmp As DTOEmp = Nothing
        Dim blCustomRef As Boolean
        Dim sFch As String = DtFch.ToString(System.Globalization.CultureInfo.InvariantCulture) ' String.Format(DtFch, "yyyyMMdd")
        Dim sb As New Text.StringBuilder
        If oLang Is Nothing Then oLang = DTOLang.ESP

        Dim oRol As DTORol = Nothing
        If TypeOf oUserOrContact Is DTOUser Then
            oRol = DirectCast(oUserOrContact, DTOUser).Rol
            oEmp = DirectCast(oUserOrContact, DTOUser).Emp
        ElseIf TypeOf oUserOrContact Is DTOContact Then
            Dim oContact As DTOContact = oUserOrContact
            If oContact.Rol Is Nothing Then ContactLoader.Load(oContact)
            oEmp = oContact.Emp
            oRol = oContact.Rol
        Else
            oEmp = New DTOEmp(DTOEmp.Ids.MatiasMasso)
            oRol = New DTORol(DTORol.Ids.unregistered)
        End If

        Select Case oRol.id
            Case DTORol.Ids.manufacturer
                sb.AppendLine("SELECT Tpa.Guid AS BrandGuid, Tpa.CodiMercancia AS TpaCodiMercancia, Tpa.CodDist ")
                sb.AppendLine(", Stp.Guid AS CategoryGuid ")
                sb.AppendLine(", Art.Guid As SkuGuid, Art.Art AS SkuId, Art.Ref AS SkuRef, Art.RefPrv AS SkuPrvNom, Art.Cbar as EAN13, Art.PackageEan ")
                sb.AppendLine(", Art.HeredaDimensions, Art.NoPro ")
                sb.AppendLine(", Art.InnerPack AS SkuMoq, Art.ForzarInnerPack AS SkuForzarMoq ")
                sb.AppendLine(", Art.Kg AS SkuKg, Art.DimensionH AS SkuDimensionH, Art.DimensionW AS SkuDimensionW, Art.DimensionL AS SkuDimensionL, Art.CodiMercancia AS SkuCodiMercancia ")
                sb.AppendLine(", (CASE WHEN Art.Image IS NULL THEN 0 ELSE 1 END) AS SkuImageExists ")
                sb.AppendLine(", Stp.InnerPack AS CategoryMoq, Stp.ForzarInnerPack AS CategoryForzarMoq ")
                sb.AppendLine(", Stp.Kg AS CategoryKg, Stp.DimensionH AS CategoryDimensionH, Stp.DimensionW AS CategoryDimensionW, Stp.DimensionL AS CategoryDimensionL, Stp.CodiMercancia AS StpCodiMercancia ")
                sb.AppendLine(", (CASE WHEN Art.CodiMercancia IS NULL THEN (CASE WHEN Stp.CodiMercancia IS NULL THEN Tpa.CodiMercancia ELSE Stp.CodiMercancia END) ELSE Art.CodiMercancia END) AS CodiMercancia ")
                sb.AppendLine(", VwRetailPrice.Retail ")
                sb.AppendLine(", Madein.Guid AS MadeInGuid, MadeIn.ISO AS MadeInISO ")

                sb.AppendLine(", BrandNom.Esp AS BrandNom ")
                sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
                sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
                sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
                sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")
                sb.AppendLine(", (Art.Obsoleto|Stp.Obsoleto) AS SkuObsoleto ")

                sb.AppendLine("FROM Tpa ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid = Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid = Art.Category ")

                sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
                sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")

                sb.AppendLine("INNER JOIN ( ")
                sb.AppendLine("         SELECT Art, MAX(Fch) AS Fch FROM VwRetailPrice ")
                sb.AppendLine("         WHERE Fch <= '" & sFch & "' ")
                sb.AppendLine("          AND (FchEnd IS NULL OR FchEnd >= '" & sFch & "') ")
                sb.AppendLine("         GROUP BY Art ")
                sb.AppendLine("         ) X ON Art.Guid = X.Art ")
                sb.AppendLine("INNER JOIN VwRetailPrice ON X.Art = VwRetailPrice.Art And VwRetailPrice.Fch = X.Fch ")
                sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON MadeIn.Guid = (CASE WHEN Art.MadeIn IS NULL THEN (CASE WHEN Stp.MadeIn IS NULL THEN Tpa.MadeIn ELSE Stp.MadeIn END) ELSE Art.MadeIn END) ")
                If TypeOf oUserOrContact Is DTOUser Then
                    sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUserOrContact.Guid.ToString & "' ")
                    sb.AppendLine("WHERE Tpa.Emp =" & oEmp.Id & " ")
                Else
                    sb.AppendLine("WHERE Tpa.Emp =" & oEmp.Id & " ")
                    sb.AppendLine("AND Tpa.Proveidor = '" & oUserOrContact.Guid.ToString & "'")
                End If
                If Not IncludeObsoletos Then
                    sb.AppendLine("AND Stp.Obsoleto = 0 ")
                    sb.AppendLine("AND Art.Obsoleto = 0 ")
                End If
                sb.AppendLine("AND (Stp.Codi = 0 Or Stp.Codi = 1) ")
                sb.AppendLine("ORDER BY Tpa.Ord, BrandNom.Esp, Stp.Ord, CategoryNom.Esp, SkuNom.Esp ")

            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                sb.AppendLine("SELECT VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandNom, VwCustomerSkus.CodDist ")
                sb.AppendLine(", VwCustomerSkus.CategoryGuid, VwCustomerSkus.CategoryNomEsp, VwCustomerSkus.CategoryNomCat, VwCustomerSkus.CategoryNomEng, VwCustomerSkus.CategoryNomPor ")
                sb.AppendLine(", VwCustomerSkus.SkuGuid, VwCustomerSkus.SkuNomEsp, VwCustomerSkus.SkuNomCat, VwCustomerSkus.SkuNomEng, VwCustomerSkus.SkuNomPor ")
                sb.AppendLine(", VwCustomerSkus.SkuNomLlargEsp, VwCustomerSkus.SkuNomLlargCat, VwCustomerSkus.SkuNomLlargEng, VwCustomerSkus.SkuNomLlargPor ")
                sb.AppendLine(", VwCustomerSkus.SkuRef, VwCustomerSkus.SkuPrvNom, VwCustomerSkus.EAN13, VwCustomerSkus.PackageEan ")
                sb.AppendLine(", VwCustomerSkus.HeredaDimensions, VwCustomerSkus.SkuImageExists ")
                sb.AppendLine(", VwCustomerSkus.SkuId, VwCustomerSkus.SkuMoq, VwCustomerSkus.SkuForzarMoq ")
                sb.AppendLine(", VwCustomerSkus.SkuKg, VwCustomerSkus.SkuDimensionH, VwCustomerSkus.SkuDimensionW, VwCustomerSkus.SkuDimensionL ")
                sb.AppendLine(", VwCustomerSkus.CategoryMoq, VwCustomerSkus.CategoryForzarMoq ")
                sb.AppendLine(", VwCustomerSkus.CategoryKg, VwCustomerSkus.CategoryDimensionH, VwCustomerSkus.CategoryDimensionW, VwCustomerSkus.CategoryDimensionL ")
                sb.AppendLine(", VwCustomerSkus.CodiMercancia, VwCustomerSkus.Obsoleto AS SkuObsoleto ")
                sb.AppendLine(", W.Retail ")
                sb.AppendLine(", Madein.Guid AS MadeInGuid, MadeIn.ISO AS MadeInISO ")
                sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

                If oMgz IsNot Nothing Then
                    sb.AppendLine(", VwSkuAndBundleStocks.Stock, VwSkuPncs.Clients ")
                End If

                sb.AppendLine("FROM VwCustomerSkus ")
                sb.AppendLine("INNER JOIN (")
                sb.AppendLine("SELECT X.Art, VwRetailPrice.Retail ")
                sb.AppendLine("FROM VwRetailPrice")
                sb.AppendLine("INNER JOIN (")
                sb.AppendLine("         SELECT Y.Art, max(Y.PreFch) AS PreFch, min(Y.PostFch) AS PostFch FROM (")
                sb.AppendLine("             SELECT Art, Fch AS PreFch, NULL AS PostFch ")
                sb.AppendLine("             FROM VwRetailPrice ")
                sb.AppendLine("             WHERE Fch <= '" & sFch & "' ")
                sb.AppendLine("             UNION ")
                sb.AppendLine("             SELECT Art, NULL AS PreFch, Fch AS PostFch ")
                sb.AppendLine("             FROM VwRetailPrice ")
                sb.AppendLine("             WHERE Fch > '" & sFch & "' ")
                sb.AppendLine("         ) Y GROUP BY Y.Art ")
                sb.AppendLine(") X ON VwRetailPrice.Art = X.Art AND VwRetailPrice.Fch = (CASE WHEN X.PreFch IS NULL THEN X.PostFch ELSE X.PreFch END)  ")
                sb.AppendLine("		 UNION ")
                sb.AppendLine("SELECT  dbo.SkuBundle.Bundle, SUM((dbo.SkuBundle.Qty * dbo.VwRetailPrice.Retail) * (100 - Bundle.BundleDto) / 100) AS Retail ")
                sb.AppendLine("FROM            dbo.VwRetailPrice INNER JOIN ")
                sb.AppendLine("                             (SELECT        Art, MAX(PreFch) AS PreFch, MIN(PostFch) AS PostFch ")
                sb.AppendLine("                               FROM            (SELECT        Art, Fch AS PreFch, NULL AS PostFch ")
                sb.AppendLine("                                                         FROM            dbo.VwRetailPrice AS VwRetailPrice_2 ")
                sb.AppendLine("                                                         WHERE        (Fch <= '" & sFch & "') ")
                sb.AppendLine("                                                         UNION ")
                sb.AppendLine("                                                         SELECT        Art, NULL AS PreFch, Fch AS PostFch ")
                sb.AppendLine("                                                         FROM            dbo.VwRetailPrice AS VwRetailPrice_1 ")
                sb.AppendLine("                                                         WHERE        (Fch > '" & sFch & "')) AS Y ")
                sb.AppendLine("                               GROUP BY Art) AS X ON dbo.VwRetailPrice.Art = X.Art AND dbo.VwRetailPrice.Fch = (CASE WHEN X.PreFch IS NULL THEN X.PostFch ELSE X.PreFch END) INNER JOIN ")
                sb.AppendLine("                         dbo.SkuBundle ON X.Art = dbo.SkuBundle.Sku INNER JOIN ")
                sb.AppendLine("                         dbo.ART AS Bundle ON dbo.SkuBundle.Bundle = Bundle.Guid ")
                sb.AppendLine("						 GROUP BY  dbo.SkuBundle.Bundle ")
                sb.AppendLine(") W ON VwCustomerSkus.SkuGuid = W.Art ")
                sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON MadeIn.Guid = VwCustomerSkus.MadeIn ")
                sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON W.Art = Url.Guid ")

                If oMgz IsNot Nothing Then
                    sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuAndBundleStocks ON VwCustomerSkus.SkuGuid = VwSkuAndBundleStocks.SkuGuid AND VwSkuAndBundleStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                    sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwCustomerSkus.SkuGuid = VwSkuPncs.SkuGuid ")
                End If

                If TypeOf oUserOrContact Is DTOUser Then
                    sb.AppendLine("     INNER JOIN Email_Clis ON VwCustomerSkus.Customer = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUserOrContact.Guid.ToString & "' ")
                    sb.AppendLine("WHERE VwCustomerSkus.Emp = " & oEmp.Id & " ")
                Else
                    sb.AppendLine("WHERE VwCustomerSkus.Customer = '" & oUserOrContact.Guid.ToString & "' ")
                End If

                sb.AppendLine("AND VwCustomerSkus.CodExclusio IS NULL ")
                If Not IncludeObsoletos Then
                    sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
                End If

                sb.AppendLine("GROUP BY VwCustomerSkus.BrandGuid, VwCustomerSkus.BrandNom, VwCustomerSkus.CodDist ")
                sb.AppendLine(", VwCustomerSkus.CategoryGuid, VwCustomerSkus.CategoryNomEsp, VwCustomerSkus.CategoryNomCat, VwCustomerSkus.CategoryNomEng, VwCustomerSkus.CategoryNomPor ")
                sb.AppendLine(", VwCustomerSkus.SkuGuid, VwCustomerSkus.SkuNomEsp, VwCustomerSkus.SkuNomCat, VwCustomerSkus.SkuNomEng, VwCustomerSkus.SkuNomPor ")
                sb.AppendLine(", VwCustomerSkus.SkuNomLlargEsp, VwCustomerSkus.SkuNomLlargCat, VwCustomerSkus.SkuNomLlargEng, VwCustomerSkus.SkuNomLlargPor ")
                sb.AppendLine(", VwCustomerSkus.SkuRef, VwCustomerSkus.SkuPrvNom, VwCustomerSkus.EAN13, VwCustomerSkus.PackageEan ")
                sb.AppendLine(", VwCustomerSkus.HeredaDimensions, VwCustomerSkus.SkuImageExists ")
                sb.AppendLine(", VwCustomerSkus.SkuId, VwCustomerSkus.SkuMoq, VwCustomerSkus.SkuForzarMoq ")
                sb.AppendLine(", VwCustomerSkus.SkuKg, VwCustomerSkus.SkuDimensionH, VwCustomerSkus.SkuDimensionW, VwCustomerSkus.SkuDimensionL ")
                sb.AppendLine(", VwCustomerSkus.CategoryMoq, VwCustomerSkus.CategoryForzarMoq ")
                sb.AppendLine(", VwCustomerSkus.CategoryKg, VwCustomerSkus.CategoryDimensionH, VwCustomerSkus.CategoryDimensionW, VwCustomerSkus.CategoryDimensionL ")
                sb.AppendLine(", VwCustomerSkus.CodiMercancia ")
                sb.AppendLine(", VwCustomerSkus.Obsoleto ")
                sb.AppendLine(", W.Retail,VwCustomerSkus.BrandOrd, VwCustomerSkus.CategoryOrd, VwCustomerSkus.CategoryCodi ")
                sb.AppendLine(", Madein.Guid, MadeIn.ISO ")
                sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

                If oMgz IsNot Nothing Then
                    sb.AppendLine(", VwSkuAndBundleStocks.Stock, VwSkuPncs.Clients ")
                End If

                sb.AppendLine("ORDER BY VwCustomerSkus.BrandNom,  VwCustomerSkus.CategoryCodi,  VwCustomerSkus.CategoryNomEsp , VwCustomerSkus.SkuNomEsp ")

            Case DTORol.Ids.comercial, DTORol.Ids.rep
                sb.AppendLine("SELECT Tpa.Guid AS BrandGuid, Tpa.CodDist ")
                sb.AppendLine(", Stp.Guid AS CategoryGuid ")
                sb.AppendLine(", Art.Guid As SkuGuid, Art.Ref AS SkuRef, Art.RefPrv AS SkuPrvNom, Art.Cbar as EAN13, Art.PackageEan ")
                sb.AppendLine(", Art.HeredaDimensions ")
                sb.AppendLine(", Art.Art AS SkuId, Art.InnerPack AS SkuMoq, Art.ForzarInnerPack AS SkuForzarMoq ")
                sb.AppendLine(", Art.Kg AS SkuKg, Art.DimensionH AS SkuDimensionH, Art.DimensionW AS SkuDimensionW, Art.DimensionL AS SkuDimensionL ")
                sb.AppendLine(", (CASE WHEN Art.Image IS NULL THEN 0 ELSE 1 END) AS SkuImageExists ")
                sb.AppendLine(", Stp.InnerPack AS CategoryMoq, Stp.ForzarInnerPack AS CategoryForzarMoq ")
                sb.AppendLine(", Stp.Kg AS CategoryKg, Stp.DimensionH AS CategoryDimensionH, Stp.DimensionW AS CategoryDimensionW, Stp.DimensionL AS CategoryDimensionL ")
                sb.AppendLine(", (CASE WHEN Art.CodiMercancia IS NULL THEN (CASE WHEN Stp.CodiMercancia IS NULL THEN Tpa.CodiMercancia ELSE Stp.CodiMercancia END) ELSE Art.CodiMercancia END) AS CodiMercancia ")
                sb.AppendLine(", VwRetailPrice.Retail ")
                sb.AppendLine(", Madein.Guid AS MadeInGuid, MadeIn.ISO AS MadeInISO ")

                sb.AppendLine(", BrandNom.Esp AS BrandNom ")
                sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
                sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
                sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
                sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")

                If oMgz IsNot Nothing Then
                    sb.AppendLine(", VwSkuAndBundleStocks.Stock, VwSkuPncs.Clients ")
                End If
                sb.AppendLine(", (Art.Obsoleto|Stp.Obsoleto) AS SkuObsoleto ")

                sb.AppendLine("FROM Tpa ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid = Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid = Art.Category ")

                sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
                sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")

                If oMgz IsNot Nothing Then
                    sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuAndBundleStocks ON Art.Guid = VwSkuAndBundleStocks.SkuGuid AND VwSkuAndBundleStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                    sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
                End If


                sb.AppendLine("INNER JOIN ( ")
                sb.AppendLine("         SELECT Art, MAX(Fch) AS Fch FROM VwRetailPrice ")
                sb.AppendLine("         WHERE Fch <= '" & sFch & "' ")
                sb.AppendLine("         GROUP BY Art ")
                sb.AppendLine("         ) X ON Art.Guid = X.Art ")
                sb.AppendLine("INNER JOIN VwRetailPrice ON X.Art = VwRetailPrice.Art And VwRetailPrice.Fch = X.Fch ")

                sb.AppendLine("INNER JOIN ( ")
                sb.AppendLine("         SELECT VwProductParent.Parent AS ProductGuid ")
                sb.AppendLine("			FROM VwProductParent ")
                sb.AppendLine("			INNER JOIN RepProducts ON VwProductParent.Child = RepProducts.Product ")

                If TypeOf oUserOrContact Is DTOUser Then
                    sb.AppendLine("     INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUserOrContact.Guid.ToString & "' ")
                    sb.AppendLine("     WHERE 1 = 1 ")
                Else
                    sb.AppendLine("     WHERE RepProducts.Rep = '" & oUserOrContact.Guid.ToString & "' ")
                End If

                sb.AppendLine("         AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=GETDATE()) ")
                sb.AppendLine("         AND RepProducts.Cod = 1 ")
                sb.AppendLine("			GROUP BY VwProductParent.Parent ")
                sb.AppendLine("			) Y ON Tpa.Guid = Y.ProductGuid ")

                sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON MadeIn.Guid = (CASE WHEN Art.MadeIn IS NULL THEN (CASE WHEN Stp.MadeIn IS NULL THEN Tpa.MadeIn ELSE Stp.MadeIn END) ELSE Art.MadeIn END) ")

                sb.AppendLine("WHERE (Stp.Codi = 0 Or Stp.Codi = 1) ")
                If Not IncludeObsoletos Then
                    sb.AppendLine("AND Stp.Obsoleto = 0 ")
                    sb.AppendLine("AND Art.Obsoleto = 0 ")
                End If
                sb.AppendLine("ORDER BY Tpa.Ord, BrandNom.Esp, Stp.Ord, CategoryNom.Esp, SkuNom.Esp ")
            Case Else
                sb.AppendLine("SELECT Tpa.Guid AS BrandGuid, Tpa.CodDist ")
                sb.AppendLine(", Stp.Guid AS CategoryGuid ")
                sb.AppendLine(", Art.Guid As SkuGuid, Art.Ref AS SkuRef, Art.RefPrv AS SkuPrvNom, Art.Cbar as EAN13, Art.PackageEan ")
                sb.AppendLine(", Art.HeredaDimensions ")
                sb.AppendLine(", Art.Art AS SkuId, Art.InnerPack AS SkuMoq, Art.ForzarInnerPack AS SkuForzarMoq ")
                sb.AppendLine(", Art.Kg AS SkuKg, Art.DimensionH AS SkuDimensionH, Art.DimensionW AS SkuDimensionW, Art.DimensionL AS SkuDimensionL ")
                sb.AppendLine(", (CASE WHEN Art.Image IS NULL THEN 0 ELSE 1 END) AS SkuImageExists ")
                sb.AppendLine(", Stp.InnerPack AS CategoryMoq, Stp.ForzarInnerPack AS CategoryForzarMoq ")
                sb.AppendLine(", Stp.Kg AS CategoryKg, Stp.DimensionH AS CategoryDimensionH, Stp.DimensionW AS CategoryDimensionW, Stp.DimensionL AS CategoryDimensionL ")
                sb.AppendLine(", (CASE WHEN Art.CodiMercancia IS NULL THEN (CASE WHEN Stp.CodiMercancia IS NULL THEN Tpa.CodiMercancia ELSE Stp.CodiMercancia END) ELSE Art.CodiMercancia END) AS CodiMercancia ")
                sb.AppendLine(", VwRetailPrice.Retail ")
                sb.AppendLine(", Madein.Guid AS MadeInGuid, MadeIn.ISO AS MadeInISO ")

                sb.AppendLine(", BrandNom.Esp AS BrandNom ")
                sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
                sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
                sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
                sb.AppendLine(", Url.IncludeDeptOnUrl, Url.UrlBrandEsp, Url.UrlBrandCat, Url.UrlBrandEng, Url.UrlBrandPor, Url.UrlDeptEsp, Url.UrlDeptCat, Url.UrlDeptEng, Url.UrlDeptPor, Url.UrlCategoryEsp, Url.UrlCategoryCat, Url.UrlCategoryEng, Url.UrlCategoryPor, Url.UrlSkuEsp, Url.UrlSkuCat, Url.UrlSkuEng, Url.UrlSkuPor ")
                sb.AppendLine(", (Art.Obsoleto|Stp.Obsoleto) AS SkuObsoleto ")

                If oMgz IsNot Nothing Then
                    sb.AppendLine(", VwSkuAndBundleStocks.Stock, VwSkuPncs.Clients ")
                End If

                sb.AppendLine("FROM Tpa ")
                sb.AppendLine("INNER JOIN Stp ON Tpa.Guid = Stp.Brand ")
                sb.AppendLine("INNER JOIN Art ON Stp.Guid = Art.Category ")

                sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
                sb.AppendLine("INNER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
                sb.AppendLine("LEFT OUTER JOIN VwProductUrlCanonical AS Url ON Art.Guid = Url.Guid ")

                If oMgz IsNot Nothing Then
                    sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuAndBundleStocks ON Art.Guid = VwSkuAndBundleStocks.SkuGuid AND VwSkuAndBundleStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                    sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
                End If

                sb.AppendLine("INNER JOIN ( ")
                sb.AppendLine("         SELECT Art, MAX(Fch) AS Fch FROM VwRetailPrice ")
                sb.AppendLine("         WHERE Fch <= '" & sFch & "' ")
                sb.AppendLine("         GROUP BY Art ")
                sb.AppendLine("         ) X ON Art.Guid = X.Art ")
                sb.AppendLine("INNER JOIN VwRetailPrice ON X.Art = VwRetailPrice.Art And VwRetailPrice.Fch = X.Fch ")

                sb.AppendLine("LEFT OUTER JOIN Country AS MadeIn ON MadeIn.Guid = (CASE WHEN Art.MadeIn IS NULL THEN (CASE WHEN Stp.MadeIn IS NULL THEN Tpa.MadeIn ELSE Stp.MadeIn END) ELSE Art.MadeIn END) ")

                sb.AppendLine("WHERE (Stp.Codi = 0 Or Stp.Codi = 1) ")
                If Not IncludeObsoletos Then
                    sb.AppendLine("AND Tpa.Obsoleto = 0 AND Stp.Obsoleto = 0 ")
                    sb.AppendLine("AND Art.Obsoleto = 0 ")
                End If
                sb.AppendLine("ORDER BY Tpa.Ord, BrandNom.Esp, Stp.Ord, CategoryNom.Esp, SkuNom.Esp ")

        End Select
        Dim SQL As String = sb.ToString

        Dim oBrand As DTOProductBrand.Treenode = DTOProductBrand.Treenode.Factory(Guid.NewGuid)
        Dim oCategory = DTOProductCategory.Treenode.Factory(Guid.NewGuid)
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)

        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = DTOProductBrand.Treenode.Factory(oDrd("BrandGuid"))
                oBrand.Nom = DTOLangText.Compact.Factory(oLang, oDrd("BrandNom"))
                'oBrand.CodDist = oDrd("CodDist")
                retval.Add(oBrand)
            End If
            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                oCategory = DTOProductCategory.Treenode.Factory(oDrd("CategoryGuid"))
                With oCategory
                    .Nom = DTOLangText.Compact.Factory(oLang, oDrd("CategoryNomEsp"), oDrd("CategoryNomCat"), oDrd("CategoryNomEng"), oDrd("CategoryNomPor"))
                    .KgBrut = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryKg"))
                    .DimensionH = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryDimensionH"))
                    .DimensionL = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryDimensionL"))
                    .DimensionW = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryDimensionW"))
                    .InnerPack = SQLHelper.GetIntegerFromDataReader(oDrd("CategoryMoq"))
                    .ForzarInnerPack = SQLHelper.GetBooleanFromDatareader(oDrd("CategoryForzarMoq"))
                End With
                oBrand.Categories.Add(oCategory)
            End If
            Dim oSku = DTOProductSku.Treenode.Factory(oDrd("SkuGuid"))
            With oSku
                .Id = oDrd("SkuId")
                .Nom = DTOLangText.Compact.Factory(oLang, oDrd("SkuNomEsp"), oDrd("SkuNomCat"), oDrd("SkuNomEng"), oDrd("SkuNomPor"))
                .NomLlarg = DTOLangText.Compact.Factory(oLang, oDrd("SkuNomLlargEsp"), oDrd("SkuNomLlargCat"), oDrd("SkuNomLlargEng"), oDrd("SkuNomLlargPor"))
                .UrlCanonicas = SQLHelper.GetProductUrlCanonicasFromDataReader(oDrd)
                .RefProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuRef"))
                .NomProveidor = SQLHelper.GetStringFromDataReader(oDrd("SkuPrvNom"))
                .HeredaDimensions = SQLHelper.GetBooleanFromDatareader(oDrd("HeredaDimensions"))
                .KgBrut = SQLHelper.GetIntegerFromDataReader(oDrd("SkuKg"))
                .DimensionH = SQLHelper.GetIntegerFromDataReader(oDrd("SkuDimensionH"))
                .DimensionL = SQLHelper.GetIntegerFromDataReader(oDrd("SkuDimensionL"))
                .DimensionW = SQLHelper.GetIntegerFromDataReader(oDrd("SkuDimensionW"))
                .InnerPack = SQLHelper.GetIntegerFromDataReader(oDrd("SkuMoq"))
                .ForzarInnerPack = SQLHelper.GetBooleanFromDatareader(oDrd("SkuForzarMoq"))
                .Ean13 = SQLHelper.GetEANFromDataReader(oDrd("EAN13"))
                .PackageEan = SQLHelper.GetEANFromDataReader(oDrd("PackageEan"))
                .Rrpp = SQLHelper.GetAmtCompactFromDataReader(oDrd("Retail"))
                .ImageExists = SQLHelper.GetBooleanFromDatareader(oDrd("SkuImageExists"))
                If Not IsDBNull(oDrd("CodiMercancia")) Then
                    .CodiMercancia = New DTOCodiMercancia(oDrd("CodiMercancia"))
                End If
                If blCustomRef Then
                    .RefCustomer = SQLHelper.GetStringFromDataReader(oDrd("CustomRef"))
                End If
                If Not IsDBNull(oDrd("MadeInGuid")) Then
                    .MadeIn = DTOGuidNom.Compact.Factory(oDrd("MadeInGuid"), SQLHelper.GetStringFromDataReader(oDrd("MadeInISO")))
                End If
                If oMgz IsNot Nothing Then
                    .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                    .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                End If
                .Obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("SkuObsoleto"))

            End With
            oCategory.Skus.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
