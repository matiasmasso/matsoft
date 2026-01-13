Public Class FourMomsLoader

    Shared Function SalePoints(oBrand As DTOProductBrand, DtFch As Date) As List(Of DTOProductAreaQty)
        Dim retval As New List(Of DTOProductAreaQty)
        Dim iMesos As Integer = 6

        Dim sFchFrom As String = Format(DtFch.AddMonths(-iMesos), "yyyyMMdd")
        Dim sFchTo As String = Format(DtFch, "yyyyMMdd")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.CategoryGuid as ProductGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomEng ")
        sb.AppendLine(", VwAddress.CountryGuid, VwAddress.CountryEng ")
        sb.AppendLine(", COUNT(DISTINCT Pdc.CliGuid) AS Clients ")
        sb.AppendLine("FROM            Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON PNC.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN VwAddress ON PDC.CliGuid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE        VwSkuNom.BrandGuid = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("AND (Pdc.Fch BETWEEN '" & sFchFrom & "' AND '" & sFchTo & "') ")
        sb.AppendLine("AND Pdc.Cod = 2 ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi = 0 ")
        sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY VwSkuNom.CategoryGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomEng, VwSkuNom.CategoryOrd ")
        sb.AppendLine(", VwAddress.CountryGuid, VwAddress.CountryISO, VwAddress.CountryEng ")
        sb.AppendLine("ORDER BY VwAddress.CountryISO, VwSkuNom.CategoryOrd")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct As New DTOProduct(oDrd("ProductGuid"))
            With oProduct
                .SourceCod = DTOProduct.SourceCods.Category
                .Nom = IIf(IsDBNull(oDrd("CategoryNomEng")), oDrd("CategoryNom"), oDrd("CategoryNomEng"))
            End With
            Dim oArea As New DTOArea(oDrd("CountryGuid"))
            With oArea
                .Cod = DTOArea.Cods.Country
                .Nom = oDrd("CountryEng")
            End With
            Dim item As New DTOProductAreaQty
            With item
                .Area = oArea
                .Product = oProduct
                .Qty = oDrd("Clients")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Sales(oBrand As DTOProductBrand, DtFch As Date) As List(Of DTOProductAreaQty)
        Dim retval As New List(Of DTOProductAreaQty)

        Dim sFchFrom As String = Format(DtFch.AddMonths(-1), "yyyyMMdd")
        Dim sFchTo As String = Format(DtFch, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.CategoryGuid as ProductGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomEng ")
        sb.AppendLine(", VwAddress.CountryGuid, VwAddress.CountryEng ")
        sb.AppendLine(", SUM(Pnc.Qty) AS Qty ")
        sb.AppendLine("FROM            Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON PNC.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN VwAddress ON PDC.CliGuid = VwAddress.SrcGuid ")
        sb.AppendLine("WHERE        VwSkuNom.BrandGuid = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("AND (Pdc.Fch BETWEEN '" & sFchFrom & "' AND '" & sFchTo & "') ")
        sb.AppendLine("AND Pdc.Cod = 2 ")
        sb.AppendLine("AND VwSkuNom.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY VwSkuNom.CategoryGuid, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomEng ")
        sb.AppendLine(", VwAddress.CountryGuid, VwAddress.CountryEng ")
        sb.AppendLine("ORDER BY VwSkuNom.CategoryOrd")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct As New DTOProduct(oDrd("ProductGuid"))
            With oProduct
                .SourceCod = DTOProduct.SourceCods.Category
                .Nom = IIf(IsDBNull(oDrd("CategoryNomEng")), oDrd("CategoryNom"), oDrd("CategoryNomEng"))
            End With
            Dim oArea As New DTOArea(oDrd("CountryGuid"))
            With oArea
                .Cod = DTOArea.Cods.Country
                .Nom = oDrd("CountryEng")
            End With
            Dim item As New DTOProductAreaQty
            With item
                .Area = oArea
                .Product = oProduct
                .Qty = oDrd("Qty")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Stocks(oBrand As DTOProductBrand, oMgz As DTOMgz, DtFch As Date) As List(Of DTOProductAreaQty)
        Dim retval As New List(Of DTOProductAreaQty)

        Dim sFch As String = Format(DtFch, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwSkuNom.CategoryGuid as ProductGuid, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomEng ")
        sb.AppendLine(", SUM(CASE WHEN ARC.COD < 50 THEN ARC.QTY ELSE - ARC.QTY END) AS Stock ")
        sb.AppendLine("FROM            ARC ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid = Alb.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom ON ARC.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("WHERE        Alb.MgzGuid = '" & oMgz.Guid.ToString & "' ")
        sb.AppendLine("AND Alb.fch < '" & sFch & "' ")
        sb.AppendLine("AND VwSkuNom.BrandGuid = '" & oBrand.Guid.ToString & "' ")
        sb.AppendLine("AND VwSkuNom.CategoryCodi = 0 ")
        sb.AppendLine("GROUP BY VwSkuNom.CategoryGuid, VwSkuNom.CategoryOrd, VwSkuNom.CategoryNom, VwSkuNom.CategoryNomEng ")
        sb.AppendLine("ORDER BY VwSkuNom.CategoryOrd")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oProduct As New DTOProduct(oDrd("ProductGuid"))
            With oProduct
                .SourceCod = DTOProduct.SourceCods.Category
                .Nom = IIf(IsDBNull(oDrd("CategoryNomEng")), oDrd("CategoryNom"), oDrd("CategoryNomEng"))
            End With
            Dim item As New DTOProductAreaQty
            With item
                .Product = oProduct
                .Qty = oDrd("Stock")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
