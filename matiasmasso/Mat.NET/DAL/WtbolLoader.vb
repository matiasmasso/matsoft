Public Class WtbolLoader
    Shared Function Model(oUser As DTOUser) As Models.Wtbol.Model
        Dim retval As New Models.Wtbol.Model
        LoadSite(retval, oUser)
        LoadLandingPages(retval)
        LoadStocks(retval)
        LoadCatalog(retval)
        Return retval
    End Function

    Shared Sub LoadSite(ByRef oModel As Models.Wtbol.Model, oUser As DTOUser)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT WtbolSite.Guid, WtbolSite.MerchantId, WtbolSite.Web ")
        sb.AppendLine(", WtbolSite.Customer, CliGral.RaoSocial ")
        sb.AppendLine(", WtbolSite.ContactNom, WtbolSite.ContactEmail, WtbolSite.ContactTel ")
        sb.AppendLine(", LastStk.LastStkFch ")
        sb.AppendLine("FROM WtbolSite ")
        sb.AppendLine("INNER JOIN CliGral ON WtbolSite.Customer = CliGral.Guid ")
        sb.AppendLine("INNER JOIN Email_Clis ON CliGral.Guid = Email_Clis.ContactGuid ")
        sb.AppendLine("LEFT OUTER JOIN (SELECT WtbolStock.Site, MAX(WtbolStock.FchCreated) AS LastStkFch FROM WtbolStock GROUP BY WtbolStock.Site) LastStk ON WtbolSite.Guid = LastStk.Site ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString() & "' ")
        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            With oModel.Site
                .Guid = oDrd("Guid")
                .MerchantId = SQLHelper.GetStringFromDataReader(oDrd("MerchantId"))
                .Customer = New Models.Base.GuidNom(oDrd("Customer"), oDrd("RaoSocial"))
                .ContactNom = SQLHelper.GetStringFromDataReader(oDrd("ContactNom"))
                .ContactEmail = SQLHelper.GetStringFromDataReader(oDrd("ContactEmail"))
                .ContactTel = SQLHelper.GetStringFromDataReader(oDrd("ContactTel"))
                .Website = SQLHelper.GetStringFromDataReader(oDrd("Web"))
                .LastStkFch = SQLHelper.GetFchFromDataReader(oDrd("LastStkFch"))
            End With
        End If
        oDrd.Close()
    End Sub
    Shared Sub LoadLandingPages(ByRef oModel As Models.Wtbol.Model)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Product, Url ")
        sb.AppendLine("FROM WtbolLandingPage ")
        sb.AppendLine("WHERE Site='" & oModel.Site.Guid.ToString() & "' ")
        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            oModel.Site.AddLandingPage(oDrd("Product"), oDrd("Url"))
        Loop
        oDrd.Close()
    End Sub

    Shared Sub LoadStocks(ByRef oModel As Models.Wtbol.Model)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT Stock, Sku, FchCreated ")
        sb.AppendLine("FROM VwWtbolStock ")
        sb.AppendLine("WHERE Site='" & oModel.Site.Guid.ToString() & "' ")
        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim firstTime As Boolean = True
        Do While oDrd.Read
            If firstTime Then
                oModel.Site.LastStkFch = oDrd("fchcreated")
            End If
            oModel.Site.AddStock(oDrd("Sku"), oDrd("Stock"))
        Loop
        oDrd.Close()
    End Sub
    Shared Sub LoadCatalog(ByRef oModel As Models.Wtbol.Model)
        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwProductNom.BrandGuid, VwProductNom.BrandNom ")
        sb.AppendLine(", VwProductNom.CategoryGuid, VwProductNom.CategoryNom ")
        sb.AppendLine(", VwProductNom.SkuGuid, VwProductNom.SkuNom ")
        sb.AppendLine("FROM VwProductNom ")
        sb.AppendLine("INNER JOIN Tpa ON VwProductNom.BrandGuid = Tpa.Guid ")
        sb.AppendLine("WHERE VwProductNom.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND VwProductNom.Obsoleto = 0 ")
        sb.AppendLine("AND VwProductNom.CategoryCodi < 2 ")
        sb.AppendLine("AND Tpa.Web_Enabled_Pro = 1 ")
        sb.AppendLine("ORDER BY VwProductNom.BrandNom, VwProductNom.CategoryCodi, VwProductNom.CategoryNom, VwProductNom.SkuNom ")
        Dim SQL = sb.ToString()
        Dim oDrd = SQLHelper.GetDataReader(SQL)
        Dim oBrand As New Models.CatalogModel.Brand
        Dim oCategory As New Models.CatalogModel.Category
        Do While oDrd.Read
            If Not oDrd("BrandGuid").Equals(oBrand.Guid) Then
                oBrand = New Models.CatalogModel.Brand(oDrd("BrandGuid"), oDrd("BrandNom"))
                oModel.Catalog.Brands.Add(oBrand)
            End If
            If Not IsDBNull(oDrd("CategoryGuid")) AndAlso Not oDrd("CategoryGuid").Equals(oCategory.Guid) Then
                oCategory = New Models.CatalogModel.Category(oDrd("CategoryGuid"), oDrd("CategoryNom"))
                oBrand.Categories.Add(oCategory)
            End If
            If Not IsDBNull(oDrd("SkuGuid")) Then
                oCategory.Skus.Add(New Models.CatalogModel.Sku(oDrd("SkuGuid"), oDrd("SkuNom")))
            End If
        Loop
        oDrd.Close()
    End Sub
End Class
