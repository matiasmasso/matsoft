Public Class SearchResultLoader

End Class

Public Class SearchResultsLoader

    Shared Function FromContactPro(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)

        Dim sb As New System.Text.StringBuilder
        Select Case oSearchRequest.User.rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                sb.AppendLine("SELECT CliGral.Guid, Clx.Clx ")
                sb.AppendLine("FROM Clx ")
                sb.AppendLine("INNER JOIN CliGral ON Clx.Guid = CliGral.Guid ")
                sb.AppendLine("WHERE CliGral.Emp=" & oSearchRequest.User.emp.id & " ")
                sb.AppendLine("AND Clx.Clx LIKE '%" & oSearchRequest.SearchKey & "%' ")
                sb.AppendLine("AND CliGral.Obsoleto = 0 ")
                sb.AppendLine("ORDER BY Clx.Clx")
            Case DTORol.Ids.SalesManager
                sb.AppendLine("SELECT CliGral.Guid, Clx.Clx ")
                sb.AppendLine("FROM Clx ")
                sb.AppendLine("INNER JOIN CliGral ON Clx.Guid = CliGral.Guid ")
                sb.AppendLine("WHERE CliGral.Emp=" & oSearchRequest.User.emp.id & " ")
                sb.AppendLine("AND Clx.Clx LIKE '%" & oSearchRequest.SearchKey & "%' ")
                sb.AppendLine("AND (CliGral.Rol =" & DTORol.Ids.CliFull & " OR CliGral.Rol =" & DTORol.Ids.CliLite & ") ")
                sb.AppendLine("AND CliGral.Obsoleto = 0 ")
                sb.AppendLine("ORDER BY Clx.Clx")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("SELECT CliGral.Guid, Clx.Clx ")
                sb.AppendLine("FROM Clx ")
                sb.AppendLine("INNER JOIN CliGral ON Clx.Guid = CliGral.Guid ")
                sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid = CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
                sb.AppendLine("INNER JOIN AreaParent ON CliAdr.Zip = AreaParent.ChildGuid ")
                sb.AppendLine("INNER JOIN RepProducts ON AreaParent.ParentGuid = RepProducts.Area AND RepProducts.Rep = '" & oSearchRequest.Contact.Guid.ToString & "' AND RepProducts.FchFrom<=GETDATE() AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=GETDATE()) ")
                sb.AppendLine("WHERE CliGral.Emp=" & oSearchRequest.User.emp.id & " ")
                sb.AppendLine("AND Clx.Clx LIKE '%" & oSearchRequest.SearchKey & "%' ")
                sb.AppendLine("AND (CliGral.Rol =" & DTORol.Ids.CliFull & " OR CliGral.Rol =" & DTORol.Ids.CliLite & ") ")
                sb.AppendLine("AND CliGral.Obsoleto = 0 ")
                sb.AppendLine("GROUP BY CliGral.Guid, Clx.Clx")
                sb.AppendLine("ORDER BY Clx.Clx")
        End Select

        Dim SQL As String = sb.ToString
        If SQL > "" Then
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oContact As New DTOContact(oDrd("Guid"))
                With oContact
                    .FullNom = oDrd("Clx")
                End With
                Dim oItem As New DTOSearchResult
                With oItem
                    .Caption = oContact.FullNom
                    .BaseGuid = oContact
                    .Cod = DTOSearchResult.Cods.Contact
                    .Url = oContact.UrlSegment
                End With
                retval.Add(oItem)
            Loop
            oDrd.Close()
        End If
        Return retval
    End Function

    Shared Function FromGeoPro(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)
        Dim sb As New System.Text.StringBuilder
        Select Case oSearchRequest.User.rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                sb.AppendLine("SELECT Nom, Guid FROM Location WHERE Nom LIKE '%" & oSearchRequest.SearchKey & "%' ")
            Case DTORol.Ids.SalesManager
                sb.AppendLine("SELECT Location.Guid, Location.Nom ")
                sb.AppendLine("FROM CliGral ")
                sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid = CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
                sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip = Zip.Guid ")
                sb.AppendLine("INNER JOIN Location ON Zip.Location = Location.Guid ")
                sb.AppendLine("WHERE CliGral.Emp=" & oSearchRequest.User.emp.id & " ")
                sb.AppendLine("AND Location.Nom LIKE '%" & oSearchRequest.SearchKey & "%' ")
                sb.AppendLine("AND (CliGral.Rol =" & DTORol.Ids.CliFull & " OR CliGral.Rol =" & DTORol.Ids.CliLite & ") ")
                sb.AppendLine("AND CliGral.Obsoleto = 0 ")
                sb.AppendLine("GROUP BY Location.Guid, Location.Nom ")
                sb.AppendLine("ORDER BY Location.Nom")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("SELECT Location.Guid, Location.Nom ")
                sb.AppendLine("FROM CliGral ")
                sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
                sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid = CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
                sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip = Zip.Guid ")
                sb.AppendLine("INNER JOIN Location ON Zip.Location = Location.Guid ")
                sb.AppendLine("INNER JOIN AreaParent ON CliAdr.Zip = AreaParent.ChildGuid ")
                sb.AppendLine("INNER JOIN RepProducts ON AreaParent.ParentGuid = RepProducts.Area AND RepProducts.Rep = '" & oSearchRequest.Contact.Guid.ToString & "' AND RepProducts.FchFrom<=GETDATE() AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=GETDATE()) ")
                sb.AppendLine("WHERE CliGral.Emp=" & oSearchRequest.User.emp.id & " ")
                sb.AppendLine("AND Location.Nom LIKE '%" & oSearchRequest.SearchKey & "%' ")
                sb.AppendLine("AND (CliGral.Rol =" & DTORol.Ids.CliFull & " OR CliGral.Rol =" & DTORol.Ids.CliLite & ") ")
                sb.AppendLine("AND CliGral.Obsoleto = 0 ")
                If oSearchRequest.User.rol.id = DTORol.Ids.Rep Then
                    sb.AppendLine("AND CliClient.norep = 0 ")
                End If
                sb.AppendLine("GROUP BY Location.Guid, Location.Nom ")
                sb.AppendLine("ORDER BY Location.Nom")
        End Select

        Dim SQL As String = sb.ToString
        If SQL > "" Then
            Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
            Do While oDrd.Read
                Dim oLocation As New DTOLocation(oDrd("Guid"))
                With oLocation
                    .Nom = oDrd("Nom")
                End With
                Dim oItem As New DTOSearchResult
                With oItem
                    .Caption = oSearchRequest.Lang.tradueix("Clientes en " & oDrd("Nom").ToString, "Clients a " & oDrd("Nom").ToString, "Customers on " & oDrd("Nom").ToString())
                    .BaseGuid = oLocation
                    .Cod = DTOSearchResult.Cods.Location
                    .Url = oLocation.UrlCustomersSegment()
                End With
                retval.Add(oItem)
            Loop
            oDrd.Close()
        End If
        Return retval
    End Function


    Shared Function FromGeo(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)
        Dim SQL As String = "SELECT Tpa.Guid, BrandNom.Esp AS BrandNom, AreaParentNom.ParentNom, COUNT(DISTINCT Web.Client) AS CliCount " _
        & "FROM Web " _
        & "INNER JOIN Tpa ON Web.Brand=Tpa.Guid " _
        & "INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 " _
        & "INNER JOIN AreaParentNom ON AreaParentNom.ChildGuid = Web.Location " _
        & "WHERE Tpa.Web_Enabled_Consumer<>0 AND Tpa.Obsoleto=0 AND AreaParentNom.ParentNom LIKE @SearchKey " _
        & "AND Impagat = 0 " _
        & "GROUP BY Tpa.Guid, BrandNom.Esp, Tpa.Ord, AreaParentNom.ParentNom " _
        & "ORDER BY AreaParentNom.ParentNom, CliCount DESC"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@SearchKey", "%" & oSearchRequest.SearchKey & "%")
        Do While oDrd.Read

            Dim oBrand As New DTOProductBrand(oDrd("Guid"))
            SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")

            Dim iCount As Integer = CInt(oDrd("CliCount"))
            Dim sExcerpt As String = ""
            If iCount = 1 Then
                'sExcerpt = oSearchRequest.Lang.tradueix( _
                '   "Se ha encontrado un punto de venta autorizado de productos " & oBrand.Nom & " en " & oSearchRequest.SearchKey, _
                '   "Localitzat un punt de venda autoritzat de productes " & oBrand.Nom & " a " & oSearchRequest.SearchKey, _
                '   "Found a " & oBrand.Nom & " authorized dealer in " & oSearchRequest.SearchKey)
            Else
                'sExcerpt = oSearchRequest.Lang.tradueix( _
                '   "Se han encontrado " & iCount & " puntos de venta autorizados de productos " & oBrand.Nom & " en " & oSearchRequest.SearchKey, _
                '  "Localitzats " & iCount & " punts de venda autoritzats de productes " & oBrand.Nom & " a " & oSearchRequest.SearchKey, _
                ' "Found " & iCount & " " & oBrand.Nom & " authorized dealers in " & oSearchRequest.SearchKey)
            End If

            Dim oItem As New DTOSearchResult
            With oItem
                .Caption = oSearchRequest.Lang.tradueix("Red de Distribuidores Oficiales de " & oBrand.nom.Esp, "Xarxa de Distribuïdors Oficials de " & oBrand.nom.tradueix(DTOLang.CAT), oBrand.nom.tradueix(DTOLang.ENG) & " Official Distributors Network")
                .Cod = DTOSearchResult.Cods.Brand
                .BaseGuid = oBrand
                .Url = oBrand.UrlSegmentSalePointsPerArea(oSearchRequest.SearchKey)
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()

        Return retval
    End Function



    Shared Function FromProducts(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Tpa.Guid as TpaGuid, Stp.Guid as StpGuid, Stp.Obsoleto AS StpObsoleto, Tpa.Obsoleto AS TpaObsoleto ")
        sb.AppendLine(", BrandNom.Esp AS BrandNom ")
        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("INNER JOIN Stp ON Tpa.Guid = Stp.Brand ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        sb.AppendLine("WHERE Tpa.Emp =" & oSearchRequest.Emp.id & " ")
        sb.AppendLine("AND Stp.WEB_ENABLED_CONSUMER=1 ")
        sb.AppendLine("AND (BrandNom.Esp+' '+CategoryNom.Esp) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE N'%" & oSearchRequest.SearchKey & "%' ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oBrand As New DTOProductBrand(oDrd("TpaGuid"))
            SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")

            Dim oCategory As New DTOProductCategory(oDrd("StpGuid"))
            SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
            oCategory.brand = oBrand

            Dim oItem As New DTOSearchResult
            With oItem
                .Caption = oBrand.nom.Esp & " " & oCategory.nom.Esp
                .BaseGuid = oCategory
                .Cod = DTOSearchResult.Cods.Category
                .Url = oCategory.UrlFullSegment
                .Descatalogat = (oDrd("TpaObsoleto") <> 0 Or oDrd("StpObsoleto") <> 0)
                If Not .Url.StartsWith("/") Then .Url = "/" & .Url
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()

        If retval.Count = 0 Then
            retval = FromSkus(oSearchRequest)
        End If
        Return retval
    End Function

    Shared Function FromSkus(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)
        Dim oDomain = oSearchRequest.Lang.Domain()
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("INNER JOIN Art ON VwSkuNom.SkuGuid = Art.Guid ")
        sb.AppendLine("WHERE VwSkuNom.Emp =" & oSearchRequest.User.emp.id & " ")
        sb.AppendLine("AND VwSkuNom.EnabledxConsumer = 1 ")
        sb.AppendLine("AND ((VwSkuNom.SkuNomLlarg +' '+VwSkuNom.SkuPrvNom COLLATE SQL_Latin1_General_CP1_CI_AI LIKE N'%" & oSearchRequest.SearchKey & "%' ")
        If IsNumeric(oSearchRequest.SearchKey) Then
            sb.AppendLine("OR VwSkuNom.Ean13='" & oSearchRequest.SearchKey & "' ")
            If oSearchRequest.SearchKey.Length < 8 Then
                sb.AppendLine("OR VwSkuNom.SkuId='" & oSearchRequest.SearchKey & "') ")
            End If
        End If
        sb.AppendLine(") ")
        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku = SQLHelper.GetProductFromDataReader(oDrd)
            'oSku.Nom = oDrd("SkuNomLlarg")

            Dim oItem As New DTOSearchResult
            With oItem
                .Descatalogat = (oDrd("Obsoleto") <> 0)
                If .Descatalogat Then
                    .Caption = "(descatalogado) " & oSku.nom.Esp
                Else
                    .Caption = oSku.nom.Esp
                End If
                .BaseGuid = oSku
                .Cod = DTOSearchResult.Cods.Sku
                .Url = CType(oSku, DTOProductSku).GetUrl(oDomain, False)
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()

        If retval.Count = 0 Then

        End If
        Return retval
    End Function

    Shared Function FromNoticiaKeywords(oSearchRequest As DTOSearchRequest) As List(Of DTOSearchResult)
        Dim retval As New List(Of DTOSearchResult)

        Dim oLang As DTOLang = oSearchRequest.Lang

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT TOP 10 Noticia.Guid as NoticiaGuid, Noticia.Fch, Noticia.UrlFriendlySegment, Noticia.Cod ")
        sb.AppendLine(", LangTitle.Esp AS TitleEsp, LangTitle.Cat AS TitleCat, LangTitle.Eng AS TitleEng, LangTitle.Por AS TitlePor ")
        sb.AppendLine("FROM Noticia ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangTitle ON Noticia.Guid = LangTitle.Guid AND LangTitle.Src = " & DTOLangText.Srcs.ContentTitle & " ")
        sb.AppendLine("LEFT OUTER JOIN VwLangText AS LangExcerpt ON Noticia.Guid = LangExcerpt.Guid AND LangExcerpt.Src = " & DTOLangText.Srcs.ContentExcerpt & " ")
        sb.AppendLine("LEFT OUTER JOIN Keyword ON SrcGuid = Noticia.Guid ")
        sb.AppendLine("WHERE Noticia.Visible = 1 ")

        If oSearchRequest.User Is Nothing Then
            sb.AppendLine("AND Professional=0 ")
        ElseIf Not oSearchRequest.User.Rol.IsProfesional Then
            sb.AppendLine("AND Professional=0 ")
        End If

        sb.AppendLine("AND (")
        sb.AppendLine("LangTitle.Esp LIKE '%" & oSearchRequest.SearchKey & "%' OR LangExcerpt.Esp LIKE '%" & oSearchRequest.SearchKey & "%' ")
        Select Case oLang.id
            Case DTOLang.Ids.CAT
                sb.AppendLine("OR LangTitle.Cat LIKE '%" & oSearchRequest.SearchKey & "%' OR LangExcerpt.Cat LIKE '%" & oSearchRequest.SearchKey & "%' ")
            Case DTOLang.Ids.ENG
                sb.AppendLine("OR LangTitle.Eng LIKE '%" & oSearchRequest.SearchKey & "%' OR LangExcerpt.Eng LIKE '%" & oSearchRequest.SearchKey & "%' ")
            Case DTOLang.Ids.POR
                sb.AppendLine("OR LangTitle.Por LIKE '%" & oSearchRequest.SearchKey & "%' OR LangExcerpt.Por LIKE '%" & oSearchRequest.SearchKey & "%' ")
        End Select
        sb.AppendLine("OR '%'+Keyword.Value+'%' LIKE '%" & oSearchRequest.SearchKey & "%'")
        sb.AppendLine(") ")
        sb.AppendLine("GROUP BY Noticia.Guid, Noticia.Fch, Noticia.UrlFriendlySegment, Noticia.Cod ")
        sb.AppendLine(", LangTitle.Esp, LangTitle.Cat, LangTitle.Eng, LangTitle.Por ")
        sb.AppendLine("ORDER BY Noticia.Fch DESC")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oNoticia As New DTONoticia(oDrd("NoticiaGuid"))
            SQLHelper.LoadLangTextFromDataReader(oNoticia.title, oDrd, "TitleEsp", "TitleCat", "TitleEng", "TitlePor")
            oNoticia.urlFriendlySegment = oDrd("UrlFriendlySegment").ToString
            oNoticia.Fch = SQLHelper.GetFchFromDataReader(oDrd("Fch"))
            oNoticia.Src = oDrd("Cod")
            Dim oItem As New DTOSearchResult
            With oItem
                .Caption = String.Format("{0:dd/MM/yy} {1}", oNoticia.Fch, oNoticia.Title.tradueix(oLang))
                .BaseGuid = oNoticia
                .Cod = DTOSearchResult.Cods.Noticia
                .Url = oNoticia.UrlSegment
            End With
            retval.Add(oItem)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

