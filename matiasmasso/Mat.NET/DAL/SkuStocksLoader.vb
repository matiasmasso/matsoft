Public Class SkuStocksLoader

    Shared Function ForRep(oUser As DTOUser, oMgz As DTOMgz) As DTOProductCatalog
        Dim retval As New DTOProductCatalog

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwRepSkus.SkuGuid, VwSkuNom.BrandGuid, VwSkuNom.CategoryGuid ")
        sb.AppendLine(", VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuId, VwSkuNom.EAN13 ")
        sb.AppendLine(", VwSkuNom.SkuNom, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ")
        sb.AppendLine(", VwSkuStocks.Stock, VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio , VwSkuPncs.ClientsBlockStock ")

        sb.AppendLine("FROM VwRepSkus  ")
        sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwRepSkus.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwRepSkus.SkuGuid=VwSkuNom.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwRepSkus.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = VwRepSkus.Rep AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")

        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd,VwSkuNom.BrandNom,VwSkuNom.BrandGuid, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom")

        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand()
        Dim oCategory As New DTOProductCategory()
        retval.Brands = New List(Of DTOProductBrand)

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim iStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
            Dim iClients As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            Dim iClientsAlPot As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
            Dim iClientsEnProgramacio As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) 'de mes de una setmana
            Dim iClientsBlockStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
            'Dim iProveidors As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))

            Dim DisplayableClients As Integer = iClients - iClientsAlPot - iClientsEnProgramacio
            Dim BlProcede As Boolean = iStock > DisplayableClients

            If BlProcede Then

                If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                    oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                    SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
                    retval.brands.Add(oBrand)
                End If
                If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                    oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                    With oCategory
                        '.Brand = oBrand
                        SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                    End With
                    oBrand.categories.Add(oCategory)
                End If


                Dim oSku As New DTOProductSku(DirectCast(oDrd("SkuGuid"), Guid))
                With oSku
                    .Id = oDrd("SkuId")
                    .Category = oCategory
                    .Ean13 = New DTOEan(SQLHelper.GetStringFromDataReader(oDrd("EAN13")))
                    SQLHelper.LoadLangTextFromDataReader(oSku.Nom, oDrd, "SkuNom", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    SQLHelper.LoadLangTextFromDataReader(oSku.NomLlarg, oDrd, "SkuNom", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                    .Stock = iStock
                    .Clients = iClients
                    .ClientsAlPot = iClientsAlPot
                    .ClientsEnProgramacio = iClientsEnProgramacio 'unitats en programació a mes de una setmana vista
                    .ClientsBlockStock = iClientsBlockStock 'unitats de comandes amb stock reservat
                End With
                oCategory.skus.Add(oSku)
                retval.Skus.Add(oSku)
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ForWeb(oUser As DTOUser, oMgz As DTOMgz) As DTOProductCatalog
        Dim oSkus As New List(Of DTOProductSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.EAN13, VwSkuNom.SkuRef, VwSkuNom.SkuNomLlarg, VwSkuNom.SkuNom, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock, VwSkuPncs.Pn1 ")
        sb.AppendLine("FROM VwCustomerSkusLite ")
        sb.AppendLine("INNER JOIN VwSkuNom ON VwCustomerSkusLite.SkuGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwCustomerSkusLite.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString() & "' ")
                sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON VwCcxOrMe.Ccx = VwCustomerSkusLite.Customer ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwCcxOrMe.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString() & "' ")
        sb.AppendLine("AND (VwCustomerSkusLite.ExclusionCod = 0 OR (VwCustomerSkusLite.ExclusionCod = 4 AND (VwSkuStocks.Stock>0 OR VwSkuPncs.Pn1>0 OR VwSkuPncs.Clients<>0))) ")
        sb.AppendLine("AND VwSkuNom.isBundle = 0 ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi < 2 ")
        sb.AppendLine("AND VwSkuNom.SkuNOSTK = 0 ")
        sb.AppendLine("AND VwSkuNom.EnabledxPro = 1 ")
        sb.AppendLine("AND VwSkuNom.SkuNOWEB = 0 ")
        sb.AppendLine("GROUP BY VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.BrandOrd, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.EAN13, VwSkuNom.SkuRef, VwSkuNom.SkuNomLlarg, VwSkuNom.SkuNom, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock, VwSkuPncs.Pn1 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim iStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
            Dim iClients As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            Dim iClientsAlPot As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
            Dim iClientsEnProgramacio As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) 'de mes de una setmana
            Dim iClientsBlockStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
            Dim iProveidors As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))

            Dim DisplayableClients As Integer = iClients - iClientsAlPot - iClientsEnProgramacio
            Dim BlProcede As Boolean = iStock > DisplayableClients
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.manufacturer
                    BlProcede = True
            End Select

            If BlProcede Then
                Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                With oSku
                    .Stock = iStock
                    .Clients = iClients
                    .ClientsAlPot = iClientsAlPot
                    .ClientsEnProgramacio = iClientsEnProgramacio 'unitats en programació a mes de una setmana vista
                    .ClientsBlockStock = iClientsBlockStock 'unitats de comandes amb stock reservat
                    .Proveidors = iProveidors
                End With
                oSkus.Add(oSku)
            End If
        Loop
        oDrd.Close()

        Dim retval As New DTOProductCatalog
        retval.brands = oSkus.GroupBy(Function(x) x.category.brand.Guid).Select(Function(y) y.First.category.brand).ToList()
        For Each oBrand In retval.brands
            Dim oBrandGuid = oBrand.Guid
            oBrand.categories = oSkus.Where(Function(x) x.category.brand.Guid.Equals(oBrandGuid)).GroupBy(Function(y) y.category.Guid).Select(Function(z) z.First.category).ToList()
            For Each oCategory In oBrand.categories
                Dim oCategoryGuid = oCategory.Guid
                oCategory.skus = oSkus.Where(Function(x) x.category.Guid.Equals(oCategoryGuid)).ToList()
            Next
        Next

        For Each oSku In oSkus
            oSku.category = Nothing
        Next
        Return retval
    End Function

    Shared Function ForManufacturer(oUser As DTOUser, oMgz As DTOMgz) As DTOProductCatalog
        Dim oSkus As New List(Of DTOProductSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuId, VwSkuNom.EAN13, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock, VwSkuPncs.Pn1 ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString() & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor = Email_Clis.ContactGuid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString() & "' ")
        sb.AppendLine("AND VwSkuNom.isBundle = 0 ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi < 2 ")
        sb.AppendLine("AND VwSkuNom.SkuNOSTK = 0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim iStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
            Dim iClients As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            Dim iClientsAlPot As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
            Dim iClientsEnProgramacio As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) 'de mes de una setmana
            Dim iClientsBlockStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
            Dim iProveidors As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
            With oSku
                .stock = iStock
                .clients = iClients
                .clientsAlPot = iClientsAlPot
                .clientsEnProgramacio = iClientsEnProgramacio 'unitats en programació a mes de una setmana vista
                .clientsBlockStock = iClientsBlockStock 'unitats de comandes amb stock reservat
                .proveidors = iProveidors
            End With
            oSkus.Add(oSku)
        Loop
        oDrd.Close()

        Dim retval As New DTOProductCatalog
        retval.brands = oSkus.GroupBy(Function(x) x.category.brand.Guid).Select(Function(y) y.First.category.brand).ToList()
        For Each oBrand In retval.brands
            Dim oBrandGuid = oBrand.Guid
            oBrand.categories = oSkus.Where(Function(x) x.category.brand.Guid.Equals(oBrandGuid)).GroupBy(Function(y) y.category.Guid).Select(Function(z) z.First.category).ToList()
            For Each oCategory In oBrand.categories
                Dim oCategoryGuid = oCategory.Guid
                oCategory.skus = oSkus.Where(Function(x) x.category.Guid.Equals(oCategoryGuid)).ToList()
            Next
        Next

        For Each oSku In oSkus
            oSku.category = Nothing
        Next
        Return retval
    End Function

    Shared Function ForStaff(oUser As DTOUser, oMgz As DTOMgz) As DTOProductCatalog
        Dim oSkus As New List(Of DTOProductSku)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuId, VwSkuNom.EAN13, VwSkuNom.SkuNom ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.ClientsBlockStock, VwSkuPncs.Pn1 ")
        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("INNER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid = '" & oMgz.Guid.ToString() & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oUser.Emp.Id & " ")
        sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        sb.AppendLine("AND VwSkuNom.isBundle = 0 ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi < 2 ")
        sb.AppendLine("AND VwSkuNom.SkuNOSTK = 0 ")
        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.BrandGuid, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryGuid, VwSkuNom.SkuNom ")

        Dim SQL As String = sb.ToString

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim iStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
            Dim iClients As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            Dim iClientsAlPot As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
            Dim iClientsEnProgramacio As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) 'de mes de una setmana
            Dim iClientsBlockStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
            Dim iProveidors As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))

            Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd, skuNomLlargField:="SkuPrvNom")
            With oSku
                .stock = iStock
                .clients = iClients
                .clientsAlPot = iClientsAlPot
                .clientsEnProgramacio = iClientsEnProgramacio 'unitats en programació a mes de una setmana vista
                .clientsBlockStock = iClientsBlockStock 'unitats de comandes amb stock reservat
                .proveidors = iProveidors
            End With
            oSkus.Add(oSku)
        Loop
        oDrd.Close()

        Dim retval As New DTOProductCatalog
        retval.brands = oSkus.GroupBy(Function(x) x.category.brand.Guid).Select(Function(y) y.First.category.brand).ToList()
        For Each oBrand In retval.brands
            Dim oBrandGuid = oBrand.Guid
            oBrand.categories = oSkus.Where(Function(x) x.category.brand.Guid.Equals(oBrandGuid)).GroupBy(Function(y) y.category.Guid).Select(Function(z) z.First.category).ToList()
            For Each oCategory In oBrand.categories
                Dim oCategoryGuid = oCategory.Guid
                oCategory.skus = oSkus.Where(Function(x) x.category.Guid.Equals(oCategoryGuid)).ToList()
            Next
        Next

        For Each oSku In oSkus
            oSku.category = Nothing
        Next
        Return retval
    End Function

    Shared Function ForWeb2_Deprecated(oUser As DTOUser, oBrands As List(Of DTOProductBrand), oMgz As DTOMgz) As DTOProductCatalog
        Dim retval As New DTOProductCatalog

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.* ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine(", VwRetail.Retail ")

        sb.AppendLine("FROM VwSkuNom ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON VwSkuNom.SkuGuid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON VwSkuNom.SkuGuid=VwSkuPncs.SkuGuid ")
        sb.AppendLine("LEFT OUTER JOIN VwRetail ON VwSkuNom.SkuGuid=VwRetail.SkuGuid ")

        sb.AppendLine("WHERE VwSkuNom.Emp=" & oUser.Emp.Id & " ")
        sb.AppendLine("AND VwSkuNom.SkuNOSTK = 0 ")
        sb.AppendLine("AND (VwSkuNom.Obsoleto=0 OR (VwSkuStocks.Stock>0 OR VwSkuPncs.Pn1>0 OR VwSkuPncs.Clients<>0)) ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                sb.AppendLine("AND (VwSkuNom.CategoryCodi=" & DTOProductCategory.Codis.standard & " OR VwSkuNom.CategoryCodi=" & DTOProductCategory.Codis.accessories & ") ")
                sb.AppendLine("AND VwSkuNom.EnabledxPro = 1 ")
                sb.AppendLine("AND VwSkuNom.SkuNOWEB = 0 ")
        End Select

        sb.AppendLine("ORDER BY VwSkuNom.BrandOrd, VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.SkuNom ")

        Dim SQL As String = sb.ToString

        Dim oBrand As New DTOProductBrand()
        Dim oCategory As New DTOProductCategory()
        retval.brands = oBrands
        For Each oBrand In retval.brands
            oBrand.categories = New List(Of DTOProductCategory)
        Next

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If retval.brands.Exists(Function(x) x.Guid = oDrd("BrandGuid")) Then

                Dim iStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                Dim iClients As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                Dim iClientsAlPot As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                Dim iClientsEnProgramacio As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) 'de mes de una setmana
                Dim iClientsBlockStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                Dim iProveidors As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))

                Dim DisplayableClients As Integer = iClients - iClientsAlPot - iClientsEnProgramacio
                Dim BlProcede As Boolean = iStock > DisplayableClients
                Select Case oUser.Rol.id
                    Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.manufacturer
                        BlProcede = True
                End Select

                If BlProcede Then
                    If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                        oBrand = retval.brands.Find(Function(x) x.Guid = oDrd("BrandGuid"))
                        oCategory = New DTOProductCategory(CType(oDrd("CategoryGuid"), Guid))
                        oCategory.brand = oBrand
                        SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNom", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                        oCategory.codi = oDrd("CategoryCodi")
                        oCategory.skus = New List(Of DTOProductSku)
                        oBrand.categories.Add(oCategory)
                    End If

                    Dim oSku As DTOProductSku = SQLHelper.GetProductFromDataReader(oDrd)
                    With oSku
                        .category = Nothing 'evita referencies circulars
                        .stock = iStock
                        .rrpp = SQLHelper.GetAmtFromDataReader(oDrd("Retail"))
                        .clients = iClients
                        .clientsAlPot = iClientsAlPot
                        .clientsEnProgramacio = iClientsEnProgramacio 'unitats en programació a mes de una setmana vista
                        .clientsBlockStock = iClientsBlockStock 'unitats de comandes amb stock reservat
                        .proveidors = iProveidors
                    End With
                    oCategory.skus.Add(oSku)
                    retval.skus.Add(oSku)
                End If
            End If

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function forApi(oBrand As DTOProductBrand, oMgz As DTOMgz, oUser As DTOUser) As List(Of DTOProductCategory)
        Dim retval As New List(Of DTOProductCategory)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Stp.Guid as StpGuid ")
        sb.AppendLine(", Art.Guid AS ArtGuid, Art.Ref, Art.RefPrv ")
        sb.AppendLine(", Stocks.Stock ")
        sb.AppendLine(", Pncs.Clients ") ', Pncs.ClientsAlPot, Pncs.ClientsEnProgramacio ")

        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")

        sb.AppendLine("FROM Art ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category=Stp.Guid ")

        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT Arc.ArtGuid, SUM(CASE WHEN Arc.Cod<50 THEN Arc.Qty ELSE -Arc.Qty END) AS Stock ")
        sb.AppendLine("                 FROM Arc ")
        sb.AppendLine("                 WHERE Arc.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("                 GROUP BY Arc.ArtGuid) Stocks ON Stocks.ArtGuid = Art.Guid ")

        sb.AppendLine("LEFT OUTER JOIN (SELECT Pnc.ArtGuid ")
        sb.AppendLine("                 ,SUM(CASE WHEN Pdc.Cod=2 THEN Pnc.Pn2 ELSE 0 END) AS Clients ") 'totes les comandes pendents
        sb.AppendLine("                 FROM Pnc INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid ")
        sb.AppendLine("                 GROUP BY Pnc.ArtGuid) AS Pncs ON Art.Guid=Pncs.ArtGuid ")

        sb.AppendLine("WHERE Stp.Brand='" & oBrand.Guid.ToString & "' And Stp.Web_Enabled_Pro = 1 ")
        sb.AppendLine("AND Art.NOPRO = 0 AND Art.NOSTK = 0 AND Art.NOWEB = 0 ")
        sb.AppendLine("AND ((Stp.Obsoleto=0 AND Art.Obsoleto = 0) OR (Stocks.Stock>0 OR Pncs.Clients<>0)) ")

        sb.AppendLine("ORDER BY Stp.Ord, Stp.Guid, SkuNom.Esp ")

        Dim SQL As String = sb.ToString

        Dim oCategory As New DTOProductCategory

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read

            Dim iStock As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
            Dim iClients As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
            'Dim iClientsAlPot As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
            'Dim iClientsEnProgramacio As Integer = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio")) 'de mes de una setmana
            Dim DisplayableClients As Integer = iClients '- iClientsAlPot - iClientsEnProgramacio

            'Dim BlProcede As Boolean = iStock > DisplayableClients
            If Not oCategory.Guid.Equals(oDrd("StpGuid")) Then
                oCategory = New DTOProductCategory(CType(oDrd("StpGuid"), Guid))
                oCategory.Brand = oBrand
                SQLHelper.LoadLangTextFromDataReader(oCategory.nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                oCategory.skus = New List(Of DTOProductSku)

                retval.Add(oCategory)
            End If

            Dim oSku As New DTOProductSku(DirectCast(oDrd("ArtGuid"), Guid))
            With oSku
                .Category = oCategory
                SQLHelper.LoadLangTextFromDataReader(oSku.nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                .refProveidor = oDrd("Ref")
                .NomProveidor = oDrd("RefPrv")

                .stock = iStock
                .clients = iClients
                '.ClientsAlPot = iClientsAlPot
                '.ClientsEnProgramacio = iClientsEnProgramacio 'unitats en programació a mes de una setmana vista
            End With
            oCategory.skus.Add(oSku)

        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function StockMovementsExcelSheet(oMgz As DTOMgz, oUser As DTOUser, year As Integer) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet
        Dim skip As Boolean
        Dim oProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Roemer)
        If oUser.Rol.id = DTORol.Ids.superUser Then
        Else
            Dim oProveidors = UserLoader.GetProveidors(oUser)
            If oProveidors.Count = 0 Then
                skip = True
            Else
                oProveidor = oProveidors.First
            End If
        End If

        If skip = False Then
            Dim sb As New Text.StringBuilder
            sb.AppendLine("SELECT Alb.Alb, FORMAT(Alb.Fch, 'dd/MM/yyyy ') AS Fch, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
            sb.AppendLine(", (CASE WHEN Arc.Cod<50 THEN Qty ELSE 0 END) AS Inp ")
            sb.AppendLine(", (CASE WHEN Arc.Cod>=50 THEN Qty ELSE 0 END) AS Outp ")
            sb.AppendLine("FROM VwSkuNom ")
            sb.AppendLine("INNER JOIN Arc ON VwSkuNom.SkuGuid = Arc.ArtGuid ")
            sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
            sb.AppendLine("WHERE VwSkuNom.Proveidor='" & oProveidor.Guid.ToString() & "' ")
            sb.AppendLine("AND Alb.Yea=" & year & " AND VwSkuNom.IsBundle=0 AND VwSkuNom.CategoryCodi<2 AND Arc.MgzGuid='" & oMgz.Guid.ToString() & "' ")
            sb.AppendLine("ORDER BY Alb.Alb, Arc.Lin ")
            Dim SQL = sb.ToString
            Dim oDrd = SQLHelper.GetDataReader(SQL)
            With retval
                .AddColumn("Document")
                .AddColumn("Date")
                .AddColumn("Product code")
                .AddColumn("Product name")
                .AddColumn("Entries")
                .AddColumn("Exits")
            End With
            Do While oDrd.Read
                Dim oRow = retval.AddRow()
                oRow.AddCell(oDrd("Alb"))
                oRow.AddCell(oDrd("Fch"))
                oRow.AddCell(oDrd("SkuRef"))
                oRow.AddCell(oDrd("SkuPrvNom"))
                oRow.AddCell(oDrd("Inp"))
                oRow.AddCell(oDrd("Outp"))
            Loop
        End If
        Return retval
    End Function
End Class
