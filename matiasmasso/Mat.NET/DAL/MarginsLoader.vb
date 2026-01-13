Public Class MarginsLoader

    Shared Function Fetch(oEmp As DTOEmp, year As Integer, mode As Models.MarginsModel.Modes, Optional target As Nullable(Of Guid) = Nothing) As Models.MarginsModel
        Dim retval As New Models.MarginsModel
        retval.Brands = New List(Of Models.MarginsModel.Brand)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.BrandGuid, VwSkuNom.BrandNomEsp, VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuGuid, VwSkuNom.SkuNomEsp ")
        sb.AppendLine(", Arc.Qty, Arc.eur, Arc.dto, Arc.pmc, Alb.Guid AS AlbGuid, Alb.Alb, Alb.fch ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN Arc ON Alb.guid=arc.albguid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe ON Alb.CliGuid=VwCcxOrMe.Guid ")
        If mode = Models.MarginsModel.Modes.Holding Then
            sb.AppendLine("INNER JOIN CliClient Ccx ON VwCcxOrMe.Ccx=Ccx.Guid ")
        End If
        sb.AppendLine("INNER JOIN VwSkuNom ON Arc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE Alb.Emp = " & oEmp.Id & " AND Alb.yea=" & year & " AND Alb.Cod = 2 ")
        sb.AppendLine("AND Arc.Pmc<>0 ")
        sb.AppendLine("AND VwSkuNom.BrandGuid <> '" & DTOProductBrand.Wellknown(DTOProductBrand.Wellknowns.Varios).Guid.ToString() & "' ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi <= " & DTOProductCategory.Codis.accessories & " ")
        Select Case mode
            Case Models.MarginsModel.Modes.Full
            Case Models.MarginsModel.Modes.Customer
                sb.AppendLine("AND Alb.CliGuid='" & target.ToString & "' ")
            Case Models.MarginsModel.Modes.Ccx
                sb.AppendLine("AND VwCcxOrMe.Ccx='" & target.ToString & "' ")
            Case Models.MarginsModel.Modes.Holding
                sb.AppendLine("AND Ccx.Holding='" & target.ToString & "' ")
            Case Models.MarginsModel.Modes.Proveidor
                sb.AppendLine("AND VwSkuNom.Proveidor ='" & target.ToString & "' ")
        End Select
        sb.AppendLine("ORDER BY VwSkuNom.BrandNomEsp, VwSkuNom.CategoryCodi, VwSkuNom.CategoryNomEsp, VwSkuNom.SkuNomEsp, VwSkuNom.SkuGuid, Alb.Alb DESC ")
        Dim SQL As String = sb.ToString
        Dim oBrand As New Models.MarginsModel.Brand
        Dim oCategory As New Models.MarginsModel.Category
        Dim oSku As New Models.MarginsModel.Sku
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New Models.MarginsModel.Brand()
                oBrand.Guid = oDrd("BrandGuid")
                oBrand.Nom = oDrd("BrandNomEsp")
                oBrand.Categories = New List(Of Models.MarginsModel.Category)
                retval.Brands.Add(oBrand)
            End If
            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                oCategory = New Models.MarginsModel.Category()
                oCategory.Guid = oDrd("CategoryGuid")
                oCategory.Nom = oDrd("CategoryNomEsp")
                oCategory.Skus = New List(Of Models.MarginsModel.Sku)
                oBrand.Categories.Add(oCategory)
            End If
            If Not oSku.Guid.Equals(oDrd("SkuGuid")) Then
                oSku = New Models.MarginsModel.Sku()
                oSku.Guid = oDrd("SkuGuid")
                oSku.Nom = oDrd("SkuNomEsp")
                oSku.Items = New List(Of Models.MarginsModel.Item)
                oCategory.Skus.Add(oSku)
            End If

            Dim item As New Models.MarginsModel.Item
            With item
                .AlbGuid = oDrd("AlbGuid")
                .Alb = oDrd("Alb")
                .Fch = oDrd("Fch")
                .Qty = oDrd("Qty")
                .Pmc = SQLHelper.GetDecimalFromDataReader(oDrd("Pmc"))
                .Eur = oDrd("Eur")
                .Dto = oDrd("Dto")
            End With
            oSku.Items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
