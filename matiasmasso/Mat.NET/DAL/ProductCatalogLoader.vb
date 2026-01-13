Public Class ProductCatalogLoader

    Shared Function Brands(oEmp As DTOEmp) As List(Of DTOProductBrand)
        'for UWP
        Dim retval As New List(Of DTOProductBrand)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Emp=" & oEmp.Id & " ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandObsoleto, VwSkuNom.CategoryObsoleto, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuNomEsp ")

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                    oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                    With oBrand
                        .emp = oEmp
                        SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                        .obsoleto = oDrd("BrandObsoleto")
                        .categories = New List(Of DTOProductCategory)
                    End With
                    retval.Add(oBrand)
                End If


                If Not IsDBNull(oDrd("CategoryGuid")) Then
                    oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                    With oCategory
                        .ord = oDrd("CategoryOrd")
                        SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                        .codi = oDrd("CategoryCodi")
                        .obsoleto = oDrd("CategoryObsoleto")
                        .skus = New List(Of DTOProductSku)
                    End With
                    oBrand.categories.Add(oCategory)
                End If
            End If

            If Not IsDBNull(oDrd("SkuGuid")) Then
                Dim oSku As New DTOProductSku(oDrd("SkuGuid"))
                With oSku
                    .id = oDrd("SkuId")
                    SQLHelper.LoadLangTextFromDataReader(oSku.nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    SQLHelper.LoadLangTextFromDataReader(oSku.nomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                    .ean13 = SQLHelper.GetEANFromDataReader(oDrd("Ean13"))
                    .obsoleto = oDrd("Obsoleto")
                End With
                oCategory.skus.Add(oSku)
            End If

        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function Factory(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing) As DTOProductCatalog
        Dim retval As New DTOProductCatalog
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid, Art.Art, Art.Obsoleto AS ArtObsoleto, Stp.Guid as Category, Art.CBar ")
        sb.AppendLine(", Stp.Ord AS StpOrd, Stp.Codi AS StpCodi, Stp.Obsoleto AS CategoryObsoleto, Tpa.Guid as Brand ")
        sb.AppendLine(", Tpa.Obsoleto AS BrandObsoleto ")
        sb.AppendLine(", BrandNom.Esp AS BrandNom ")
        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")
        sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat, SkuNomLlarg.Eng AS SkuNomLlargEng, SkuNomLlarg.Por AS SkuNomLlargPor ")
        sb.AppendLine("FROM Tpa ")
        sb.AppendLine("LEFT OUTER JOIN Stp ON Stp.Brand=Tpa.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Art ON  Art.Category=Stp.Guid ")
        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")

        If Not oContact Is Nothing Then
            sb.AppendLine("INNER JOIN Pnc ON Art.Guid=Pnc.ArtGuid ")
            sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid AND Pdc.CliGuid='" & oContact.Guid.ToString & "' ")
        End If
        sb.AppendLine("WHERE Tpa.Emp=" & oEmp.Id & " ")
        If Not oContact Is Nothing Then
            sb.AppendLine("GROUP BY Art.Guid, Art.Art, Art.Obsoleto, Art.Category, Art.CBar ")
            sb.AppendLine(", Stp.Obsoleto, Stp.Codi, Stp.Brand, Stp.Ord ")
            sb.AppendLine(", Tpa.Obsoleto, Stp.Guid, Tpa.Guid ")
            sb.AppendLine(", BrandNom.Esp ")
            sb.AppendLine(", CategoryNom.Esp, CategoryNom.Cat, CategoryNom.Eng, CategoryNom.Por ")
            sb.AppendLine(", SkuNom.Esp, SkuNom.Cat, SkuNom.Eng, SkuNom.Por ")
            sb.AppendLine(", SkuNomLlarg.Esp, SkuNomLlarg.Cat, SkuNomLlarg.Eng, SkuNomLlarg.Por ")
        End If
        sb.AppendLine("ORDER BY Tpa.Obsoleto, BrandNom.Esp, Tpa.Guid, Stp.Obsoleto, Stp.Codi, CategoryNom.Esp, Stp.Guid, SkuNom.Esp, Art.Guid")

        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCategory.Guid.Equals(oDrd("Category")) Then
                If Not oBrand.Guid.Equals(oDrd("Brand")) Then
                    oBrand = New DTOProductBrand(oDrd("Brand"))
                    With oBrand
                        .Emp = oEmp
                        SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                        .obsoleto = oDrd("BrandObsoleto")
                        .Categories = New List(Of DTOProductCategory)
                    End With
                    retval.Brands.Add(oBrand)
                    retval.Bases.Add(oBrand)
                End If

                'If oCategory.Nom = "TRIP" Then Stop

                If Not IsDBNull(oDrd("Category")) Then
                    oCategory = New DTOProductCategory(oDrd("Category"))
                    With oCategory
                        .Brand = oBrand
                        .Ord = oDrd("StpOrd")
                        SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                        .codi = oDrd("StpCodi")
                        .Obsoleto = oDrd("CategoryObsoleto")
                        .Skus = New List(Of DTOProductSku)
                    End With
                    oBrand.Categories.Add(oCategory)
                    retval.Categories.Add(oCategory)
                    retval.Bases.Add(oCategory)
                End If
            End If

            If Not IsDBNull(oDrd("Guid")) Then
                Dim oSku As New DTOProductSku(oDrd("Guid"))
                With oSku
                    .Id = oDrd("Art")
                    .Category = oCategory
                    SQLHelper.LoadLangTextFromDataReader(oSku.nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    SQLHelper.LoadLangTextFromDataReader(oSku.nomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                    .ean13 = SQLHelper.GetEANFromDataReader(oDrd("CBar"))
                    .Obsoleto = oDrd("ArtObsoleto")
                    '.Virtual = oDrd("Virtual")
                End With
                oCategory.Skus.Add(oSku)
                retval.Skus.Add(oSku)
                retval.Bases.Add(oSku)
            End If

        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function CompactTree(oEmp As DTOEmp, includeObsoletos As Boolean) As List(Of DTOCompactBrand)
        Dim retval As New List(Of DTOCompactBrand)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNom, VwSkuNom.Obsoleto ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        If Not includeObsoletos Then
            sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        End If
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid ")
        sb.AppendLine(", VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom ")
        sb.AppendLine(", VwSkuNom.Obsoleto, VwSkuNom.SkuNom ")

        Dim oBrand As New DTOCompactBrand
        Dim oCategory As New DTOCompactCategory

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Try
                If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                    If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                        oBrand = DTOCompactBrand.Factory(oDrd("BrandGuid"), oDrd("BrandNom"))
                        retval.Add(oBrand)
                    End If

                    If Not IsDBNull(oDrd("CategoryGuid")) Then
                        oCategory = DTOCompactCategory.Factory(oDrd("CategoryGuid"), oDrd("CategoryNom"))
                        oBrand.Categories.Add(oCategory)
                    End If
                End If

                If Not IsDBNull(oDrd("SkuGuid")) Then
                    Dim oSku = DTOCompactSku.Factory(oDrd("SkuGuid"), SQLHelper.GetStringFromDataReader(oDrd("SkuNom")))
                    oSku.Obsoleto = oDrd("Obsoleto")
                    oCategory.Skus.Add(oSku)
                End If

            Catch ex As Exception
                'Stop
            End Try
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Factory(oCustomer As DTOCustomer) As DTOProductCatalog
        Dim retval As New DTOProductCatalog
        Dim sb As New System.Text.StringBuilder

        sb.AppendLine("SELECT Pnc.ArtGuid, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN VwProductNom ON Pnc.ArtGuid = VwProductNom.Guid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON Pdc.CliGuid = VwCcxOrMe.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe Ccx3 ON VwCcxOrMe.Ccx = Ccx3.Ccx ")
        sb.AppendLine("WHERE Ccx3.Guid = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("GROUP BY Pnc.ArtGuid, VwProductNom.BrandGuid, VwProductNom.BrandNom, VwProductNom.CategoryGuid, VwProductNom.CategoryNom, VwProductNom.SkuNom, VwProductNom.SkuNomLlarg ")
        sb.AppendLine("ORDER BY VwProductNom.BrandNom, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                    oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                    With oBrand
                        SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                        .categories = New List(Of DTOProductCategory)
                    End With
                    retval.Brands.Add(oBrand)
                    retval.Bases.Add(oBrand)
                End If

                'If oCategory.Nom = "TRIP" Then Stop

                If Not IsDBNull(oDrd("CategoryGuid")) Then
                    oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                    With oCategory
                        .Brand = oBrand
                        SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNom", "CategoryNom", "CategoryNom", "CategoryNom")
                        .skus = New List(Of DTOProductSku)
                    End With
                    oBrand.Categories.Add(oCategory)
                    retval.Categories.Add(oCategory)
                    retval.Bases.Add(oCategory)
                End If
            End If

            If Not IsDBNull(oDrd("ArtGuid")) Then
                Dim oSku As New DTOProductSku(oDrd("ArtGuid"))
                With oSku
                    .Category = oCategory
                    SQLHelper.LoadLangTextFromDataReader(oSku.nom, oDrd, "SkuNom", "SkuNom", "SkuNom", "SkuNom")
                    SQLHelper.LoadLangTextFromDataReader(oSku.nomLlarg, oDrd, "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg", "SkuNomLlarg")
                End With
                oCategory.Skus.Add(oSku)
                retval.Skus.Add(oSku)
                retval.Bases.Add(oSku)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function BrandCategories(user As DTOUser) As List(Of DTOCompactNode)
        Dim retval As New List(Of DTOCompactNode)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT BrandGuid, BrandNom ")
        sb.AppendLine(", CategoryGuid, CategoryNom ")

        Select Case user.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                sb.AppendLine("FROM VwSkuNom ")
                sb.AppendLine("WHERE VwSkuNom.Obsoleto = 0 ")
            Case DTORol.Ids.SalesManager
                sb.AppendLine("FROM VwSalesManagerSkus ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerSkus.SalesManager = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("FROM VwRepSkus ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepSkus.Rep = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
            Case DTORol.Ids.CliLite, DTORol.Ids.CliFull
                sb.AppendLine("FROM VwCustomerSkus ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
                sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
            Case DTORol.Ids.manufacturer
                sb.AppendLine("FROM VwSkuNom ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
                sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
            Case Else
                Return retval
                Exit Function
        End Select

        sb.AppendLine("GROUP BY BrandGuid, BrandOrd, BrandNom ")
        sb.AppendLine(", CategoryGuid, CategoryCodi, CategoryOrd, CategoryNom ")
        sb.AppendLine("ORDER BY BrandOrd, BrandNom, CategoryCodi, CategoryOrd, CategoryNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oBrandNode As New DTOCompactNode
        Dim oCategoryNode As New DTOCompactNode
        Do While oDrd.Read
            If Not oBrandNode.guid.Equals(oDrd("BrandGuid")) Then
                oBrandNode = New DTOCompactNode
                With oBrandNode
                    .guid = oDrd("BrandGuid")
                    .nom = SQLHelper.GetStringFromDataReader(oDrd("BrandNom"))
                    .items = New List(Of DTOCompactNode)
                End With
                retval.Add(oBrandNode)
            End If
            If Not oCategoryNode.guid.Equals(oDrd("CategoryGuid")) Then
                oCategoryNode = New DTOCompactNode
                With oCategoryNode
                    .guid = oDrd("CategoryGuid")
                    .nom = SQLHelper.GetStringFromDataReader(oDrd("CategoryNom"))
                End With
                oBrandNode.items.Add(oCategoryNode)
            End If
        Loop
        oDrd.Close()
        Return retval

    End Function

    Shared Function CustomerBasicTree(oCustomer As DTOCustomer, oLang As DTOLang) As DTOBasicCatalog
        Dim retval As New DTOBasicCatalog
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT BrandGuid, BrandNom, CategoryGuid, CategoryNom, SkuGuid, CodExclusio ")
        sb.AppendLine(", SkuNom, SkuNomCat, SkuNomEng, SkuNomPor ")
        sb.AppendLine("FROM vwCustomerSkus where Customer = '" & oCustomer.Guid.ToString & "' ")
        sb.AppendLine("AND CategoryCodi <= " & DTOProductCategory.Codis.accessories & " ")
        sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY BrandOrd, BrandNom, BrandGuid, CategoryCodi, CategoryOrd, CategoryNom, CategoryGuid, SkuNom ")
        Dim SQL = sb.ToString
        Dim oBrand As New DTOBasicCatalog.Brand()
        Dim oCategory As New DTOBasicCatalog.Category()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oDrd("CategoryGuid").Equals(oCategory.Guid) Then
                If Not oDrd("BrandGuid").Equals(oBrand.Guid) Then
                    oBrand = New DTOBasicCatalog.Brand(oDrd("BrandGuid"), oDrd("BrandNom"))
                    retval.Add(oBrand)
                End If
                oCategory = New DTOBasicCatalog.Category(oDrd("CategoryGuid"), oDrd("CategoryNom"))
                oBrand.Categories.Add(oCategory)
            End If
            Dim skuNomCat = SQLHelper.GetStringFromDataReader(oDrd("SkuNomCat"))
            Dim skuNomEng = SQLHelper.GetStringFromDataReader(oDrd("SkuNomEng"))
            Dim skuNomPor = SQLHelper.GetStringFromDataReader(oDrd("SkuNomPor"))
            Dim oSku As New DTOBasicCatalog.Sku(oDrd("SkuGuid"), oLang.Tradueix(oDrd("SkuNom"), skuNomCat, skuNomEng, skuNomPor))
            oCategory.Skus.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function CompactSkus(user As DTOUser, category As DTOProductCategory, oMgz As DTOMgz) As List(Of DTOCompactGuidNomQtyEur)
        Dim retval As New List(Of DTOCompactGuidNomQtyEur)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuStocks.SkuGuid, SkuNom ")
        sb.AppendLine(", VwSkuStocks.Stock, VwSkuPncs.Clients ")

        Select Case user.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                sb.AppendLine("FROM VwSkuNom ")
                sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
                sb.AppendLine("WHERE VwSkuNom.Obsoleto = 0 ")
            Case DTORol.Ids.salesManager
                sb.AppendLine("FROM VwSalesManagerSkus ")
                sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSalesManagerSkus.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSalesManagerSkus.SkuGuid = VwSkuPncs.SkuGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSalesManagerSkus.SalesManager = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
            Case DTORol.Ids.rep, DTORol.Ids.comercial
                sb.AppendLine("FROM VwRepSkus ")
                sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwRepSkus.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwRepSkus.SkuGuid = VwSkuPncs.SkuGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwRepSkus.Rep = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
            Case DTORol.Ids.cliLite, DTORol.Ids.cliFull
                sb.AppendLine("FROM VwCustomerSkus ")
                sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwCustomerSkus.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwCustomerSkus.SkuGuid = VwSkuPncs.SkuGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwCustomerSkus.Customer = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
                sb.AppendLine("AND VwCustomerSkus.Obsoleto = 0 ")
            Case DTORol.Ids.manufacturer
                sb.AppendLine("FROM VwSkuNom ")
                sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid ='" & user.Guid.ToString & "' ")
                sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
            Case Else
                Return retval
                Exit Function
        End Select

        sb.AppendLine("ORDER BY SkuNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOCompactGuidNomQtyEur
            With item
                .guid = oDrd("SkuGuid")
                .nom = SQLHelper.GetStringFromDataReader(oDrd("SkuNom"))
                .qty = SQLHelper.GetIntegerFromDataReader(oDrd("Stock")) - SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()

        Return retval
    End Function

    Shared Function Refs() As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("WHERE VwSkuNom.Obsoleto=0 AND VwSkuNom.CategoryCodi<=1 AND VwSkuNom.Emp=1 AND VwSkuNom.BrandNom<>'Varios' AND NoPro=0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNom, VwSkuNom.SkuNom")
        Dim SQL As String = Sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oSku = SQLHelper.GetProductFromDataReader(oDrd)
            retval.Add(oSku)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class

