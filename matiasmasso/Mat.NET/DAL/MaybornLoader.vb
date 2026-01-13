Public Class MaybornLoader

    Shared Function Sales(oProveidor As DTOProveidor, year As Integer) As List(Of DTOProductMonthQtySalepoint)
        Dim retval As New List(Of DTOProductMonthQtySalepoint)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  Month(Pdc.Fch) as Mes, VwSkuNom.SkuGuid, VwSkuNom.SkuRef, VwSkuNom.SkuId, VwSkuNom.Ean13, VwSkuNom.SkuPrvNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNomEsp ")
        sb.AppendLine(", SUM(Pnc.Qty) AS Qty ")
        sb.AppendLine(", SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur ")
        sb.AppendLine(", COUNT(DISTINCT(Pdc.CliGuid)) AS SalePoints ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid AND Pdc.Cod=2 ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE VwSkuNom.Proveidor='" & oProveidor.Guid.ToString & "' AND VwSkuNom.CategoryCodi<2 ")
        sb.AppendLine("AND Year(Pdc.Fch)=" & year & " ")
        sb.AppendLine("GROUP BY Month(Pdc.Fch) as Mes, VwSkuNom.SkuGuid, VwSkuNom.SkuRef, VwSkuNom.SkuId, VwSkuNom.Ean13, VwSkuNom.SkuPrvNom ")
        sb.AppendLine(", VwSkuNom.CategoryGuid, VwSkuNom.CategoryNomEsp ")
        sb.AppendLine(", VwSkuNom.BrandGuid, VwSkuNom.BrandNomEsp ")
        sb.AppendLine("ORDER BY VwSkuNom.CategoryNomEsp, VwSkuNom.SkuRef")

        Dim SQL As String = sb.ToString
        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory
        Dim oSku As New DTOProductSku
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oSku.Guid.Equals(oDrd("Guid")) Then
                If Not oCategory.Guid.Equals(oDrd("CategoryGuid")) Then
                    If Not oBrand.Guid.Equals(oDrd("BrandGuid")) Then
                        oBrand = New DTOProductBrand(oDrd("BrandGuid"))
                        SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "BrandNomEsp", "BrandNomEsp", "BrandNomEsp", "BrandNomEsp")
                    End If
                    oCategory = New DTOProductCategory(oDrd("CategoryGuid"))
                    SQLHelper.LoadLangTextFromDataReader(oBrand.nom, oDrd, "CategoryNomEsp", "CategoryNomEsp", "CategoryNomEsp", "CategoryNomEsp")
                    oCategory.brand = oBrand
                End If
                oSku = New DTOProductSku(oDrd("Guid"))
                With DirectCast(oSku, DTOProductSku)
                    .Category = oCategory
                    .id = oDrd("SkuId")
                    .ean13 = SQLHelper.GetEANFromDataReader(oDrd("Ean13"))
                    .nomProveidor = oDrd("SkuPrvNom")
                    .refProveidor = oDrd("SkuRef")
                End With
            End If
            Dim item As New DTOProductMonthQtySalepoint()
            With item
                .Product = oSku
                .Month = oDrd("Mes")
                .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .Eur = SQLHelper.GetDecimalFromDataReader(oDrd("Eur"))
                .SalePoints = SQLHelper.GetDecimalFromDataReader(oDrd("SalePoints"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
