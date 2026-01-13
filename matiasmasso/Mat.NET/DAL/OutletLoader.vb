Public Class OutletLoader

    Shared Function All(oUser As DTOUser, oBrands As List(Of DTOProductBrand), oMgz As DTOMgz) As List(Of DTOProductSku)
        Dim retval As New List(Of DTOProductSku)
        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("select art.guid, art.ref, art.refprv, VwSkuStocks.stock, VwRetail.retail ")
        sb.AppendLine(", Art.OutletDto, Art.OutletQty ")
        sb.AppendLine(", Tpa.Guid as BrandGuid ")
        sb.AppendLine(", Stp.Guid as CategoryGuid ")

        sb.AppendLine(", BrandNom.Esp AS BrandNom ")
        sb.AppendLine(", CategoryNom.Esp AS CategoryNomEsp, CategoryNom.Cat AS CategoryNomCat, CategoryNom.Eng AS CategoryNomEng, CategoryNom.Por AS CategoryNomPor ")
        sb.AppendLine(", SkuNom.Esp AS SkuNomEsp, SkuNom.Cat AS SkuNomCat, SkuNom.Eng AS SkuNomEng, SkuNom.Por AS SkuNomPor ")

        sb.AppendLine("FROM Art ")
        sb.AppendLine("inner join VwSkuStocks on VwSkuStocks.SkuGuid=art.guid and VwSkuStocks.MgzGuid='" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("inner join stp on art.category=stp.guid ")
        sb.AppendLine("inner join tpa on stp.brand=tpa.guid ")

        sb.AppendLine("INNER JOIN VwLangText BrandNom ON Tpa.Guid = BrandNom.Guid AND BrandNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText CategoryNom ON Stp.Guid = CategoryNom.Guid AND CategoryNom.Src = 28 ")
        sb.AppendLine("INNER JOIN VwLangText SkuNom ON Art.Guid = SkuNom.Guid AND SkuNom.Src = 28 ")


        sb.AppendLine("LEFT OUTER JOIN VwRetail on art.guid=VwRetail.SkuGuid ")
        sb.AppendLine("WHERE art.OutletDto<>0 and VwSkuStocks.Stock>0 ")

        sb.AppendLine("AND ( ")
        For Each oUserBrand As DTOProductBrand In oBrands
            If oUserBrand.UnEquals(oBrands.First) Then
                sb.Append("OR ")
            End If
            sb.AppendLine("Stp.Brand = '" & oUserBrand.Guid.ToString & "' ")
        Next
        sb.AppendLine(") ")

        sb.AppendLine("order by BrandNom.Esp, CategoryNom.Esp, SkuNom.Esp")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                SQLHelper.LoadLangTextFromDataReader(oBrand.Nom, oDrd, "BrandNom", "BrandNom", "BrandNom", "BrandNom")
            End If
            If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                SQLHelper.LoadLangTextFromDataReader(oCategory.Nom, oDrd, "CategoryNomEsp", "CategoryNomCat", "CategoryNomEng", "CategoryNomPor")
                oCategory.Brand = oBrand
            End If
            Dim item As New DTOProductSku(oDrd("Guid"))
            With item
                .Category = oCategory
                .RefProveidor = oDrd("Ref")
                .NomProveidor = oDrd("RefPrv")
                SQLHelper.LoadLangTextFromDataReader(.Nom, oDrd, "SkuNomEsp", "SkuNomCat", "SkuNomEng", "SkuNomPor")
                .Stock = oDrd("Stock")
                If Not IsDBNull(oDrd("Retail")) Then
                    .Rrpp = DTOAmt.Factory(CDec(oDrd("Retail")))
                End If
                .OutletDto = CDec(oDrd("OutletDto"))
                .OutletQty = SQLHelper.GetIntegerFromDataReader(oDrd("OutletQty"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
