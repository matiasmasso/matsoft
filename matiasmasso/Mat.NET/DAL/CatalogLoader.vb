Public Class CatalogLoader

    Shared Function Fetch(oUser As DTOUser, Optional includeObsolets As Boolean = False) As DTO.Models.CatalogModel
        Dim retval As New DTO.Models.CatalogModel
        Dim oEmp = oUser.Emp
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp, VwSkuNom.CategoryNomCat, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryNomPor ")
        sb.AppendLine(", VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp, VwSkuNom.SkuNomCat, VwSkuNom.SkuNomEng, VwSkuNom.SkuNomPor ")
        sb.AppendLine("FROM VwSkuNom ")
        If Not oUser.Rol.isStaff Then
            sb.AppendLine("INNER JOIN Tpa ON VwSkuNom.BrandGuid = Tpa.Guid ")
        End If
        sb.AppendLine("WHERE VwSkuNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        If Not oUser.Rol.isStaff Then
            'hide non-brands, spares and marketing items
            sb.AppendLine("AND Tpa.Web_Enabled_Pro = 1 ")
            sb.AppendLine("AND VwSkuNom.CategoryCodi < 2 ")
        End If
        sb.AppendLine("ORDER BY VwSkuNom.BrandNom, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNomESP, VwSkuNom.SkuNomESP ")
        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New Models.CatalogModel.Brand
        Dim oCategory As New Models.CatalogModel.Category
        Do While oDrd.Read
            If Not oDrd("BrandGuid").Equals(oBrand.Guid) Then
                oBrand = New Models.CatalogModel.Brand(oDrd("BrandGuid"), oDrd("BrandNom"))
                retval.Brands.Add(oBrand)
            End If
            If Not IsDBNull(oDrd("CategoryGuid")) AndAlso Not oDrd("CategoryGuid").Equals(oCategory.Guid) Then
                Dim oLangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                oCategory = New Models.CatalogModel.Category(oDrd("CategoryGuid"), oLangNom.Tradueix(oUser.Lang))
                oBrand.Categories.Add(oCategory)
            End If
            If Not IsDBNull(oDrd("SkuGuid")) Then
                Dim oLangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                oCategory.Skus.Add(New Models.CatalogModel.Sku(oDrd("SkuGuid"), oLangNom.Tradueix(oUser.Lang)))
            End If
        Loop
        oDrd.Close()

        retval.Stocks = SkuStocks(oUser.Emp.Mgz)
        retval.Prices = SkuPrices(oUser.Emp)
        Return retval
    End Function


    Shared Function SkuStocks(oMgz As DTOMgz) As List(Of Models.SkuStock)
        Dim retval As New List(Of Models.SkuStock)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid AS SkuGuid ")
        sb.AppendLine(", VwSkuStocks.Stock ")
        sb.AppendLine(", VwSkuPncs.Clients, VwSkuPncs.ClientsAlPot, VwSkuPncs.ClientsEnProgramacio, VwSkuPncs.Pn1, VwSkuPncs.ClientsBlockStock ")
        sb.AppendLine("FROM Art ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundleStocks VwSkuStocks ON Art.Guid = VwSkuStocks.SkuGuid AND VwSkuStocks.MgzGuid ='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("LEFT OUTER JOIN VwSkuAndBundlePncs VwSkuPncs ON Art.Guid = VwSkuPncs.SkuGuid ")
        sb.AppendLine("WHERE (VwSkuStocks.Stock <> 0 ")
        sb.AppendLine("OR VwSkuPncs.Clients <> 0 ")
        sb.AppendLine("OR VwSkuPncs.ClientsAlPot <> 0 ")
        sb.AppendLine("OR VwSkuPncs.ClientsEnProgramacio <> 0 ")
        sb.AppendLine("OR VwSkuPncs.ClientsBlockStock <> 0 ")
        sb.AppendLine("OR VwSkuPncs.Pn1 <> 0) ")
        sb.AppendLine("ORDER BY Art.Guid ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New Models.SkuStock()
            With item
                .Guid = oDrd("SkuGuid")
                .Stock = SQLHelper.GetIntegerFromDataReader(oDrd("Stock"))
                .Clients = SQLHelper.GetIntegerFromDataReader(oDrd("Clients"))
                .ClientsAlPot = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsAlPot"))
                .ClientsEnProgramacio = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsEnProgramacio"))
                .ClientsBlockStock = SQLHelper.GetIntegerFromDataReader(oDrd("ClientsBlockStock"))
                .Proveidors = SQLHelper.GetIntegerFromDataReader(oDrd("Pn1"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function SkuPrices(oEmp As DTOEmp) As Dictionary(Of Guid, String)
        Dim retval As New Dictionary(Of Guid, String)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwRetail.SkuGuid, VwRetail.Retail ")
        sb.AppendLine("FROM VwRetail ")
        sb.AppendLine("INNER JOIN Art ON VwRetail.SkuGuid = Art.Guid ")
        sb.AppendLine("WHERE Art.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND VwRetail.Retail<>0 ")
        sb.AppendLine("ORDER BY VwRetail.SkuGuid ")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim retail As Decimal = oDrd("Retail")
            retval.Add(oDrd("SkuGuid"), retail.ToString("F", System.Globalization.CultureInfo.InvariantCulture))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function SkuPrevisions(oEmp As DTOEmp) As Dictionary(Of Guid, Integer)
        Dim retval As New Dictionary(Of Guid, Integer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Sku, SUM(Qty) As Previsio ")
        sb.AppendLine("FROM ImportPrevisio ")
        sb.AppendLine("INNER JOIN Art ON ImportPrevisio.Sku = Art.Guid ")
        sb.AppendLine("WHERE Art.Emp = " & oEmp.Id & " ")
        sb.AppendLine("GROUP BY Sku")
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Sku"), oDrd("Previsio"))
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function ProductUrls(oEmp As DTOEmp) As List(Of Dictionary(Of String, Object))
        Dim retval As New List(Of Dictionary(Of String, Object))
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT UrlSegment.Target, UrlSegment.Segment, VwProductGuid.Cod, UrlSegment.Lang, UrlSegment.Canonical ")
        sb.AppendLine(", VwProductGuid.Brand, VwProductGuid.Dept, VwProductGuid.Category, VwProductGuid.Sku ")
        sb.AppendLine("FROM UrlSegment ")
        sb.AppendLine("INNER JOIN VwProductGuid ON UrlSegment.Target = VwProductGuid.Guid ")
        sb.AppendLine("ORDER BY UrlSegment.Target, UrlSegment.Lang, UrlSegment.Canonical ") 'Sort order important for iterate conversion on Client
        Dim SQL As String = sb.ToString
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item = Models.Min.ProductUrl.Factory(oDrd("Segment"), oDrd("Cod"), oDrd("Brand"), oDrd("Dept"), oDrd("Category"), oDrd("Sku"), oDrd("Lang"), oDrd("Canonical"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
