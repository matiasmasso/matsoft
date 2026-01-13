Public Class TarifaLoader

    Shared Function Load(ByRef oTarifa As DTOTarifa) As Boolean
        Dim retval As Boolean
        oTarifa.Brands = New List(Of DTOProductBrand)

        Dim sFch As String = Format(oTarifa.Fch, "yyyyMMdd")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT  Art.Guid AS ArtGuid, Stp.Guid as StpGuid, Tpa.Guid as TpaGuid ")
        sb.AppendLine(", Tpa.Dsc as TpaNom, Stp.Dsc as StpNom, Art.Ord as ArtNomCurt, P2.Retail ")
        sb.AppendLine("FROM            dbo.PriceList_Customer AS P1 ")
        sb.AppendLine("INNER JOIN PricelistItem_Customer AS P2 ON P2.PriceList = P1.Guid ")
        sb.AppendLine("INNER JOIN dbo.ART ON P2.Art = dbo.ART.Guid ")
        sb.AppendLine("INNER JOIN dbo.STP ON dbo.ART.Category = dbo.STP.Guid ")
        sb.AppendLine("INNER JOIN dbo.TPA ON dbo.STP.Brand = dbo.TPA.Guid")

        sb.AppendLine("INNER JOIN (SELECT MAX(dbo.PriceList_Customer.Fch) AS FCH, PricelistItem_Customer.Art ")
        sb.AppendLine("FROM            dbo.PriceList_Customer ")
        sb.AppendLine("INNER JOIN PricelistItem_Customer ON dbo.PriceList_Customer.Guid = PricelistItem_Customer.PriceList ")
        sb.AppendLine("WHERE dbo.PriceList_Customer.Fch <= '" & sFch & "' ")
        sb.AppendLine("GROUP BY PricelistItem_Customer.Art) AS X ON P1.Fch = X.FCH AND P2.Art = X.Art ")

        sb.AppendLine("WHERE dbo.ART.emp = " & CInt(oTarifa.Emp.Id) & " AND dbo.ART.obsoleto = 0 AND dbo.STP.obsoleto = 0 AND dbo.STP.WEB_ENABLED_PRO = 1 AND dbo.ART.NoPro = 0 AND dbo.ART.NoWeb = 0  AND dbo.ART.NoStk = 0 AND dbo.Tpa.Obsoleto = 0 AND dbo.Tpa.Web_Enabled_Pro = 1 ")
        If oTarifa.Customer.EShopOnly Then
            sb.AppendLine(" AND BloqEshops=0 ")
        End If
        sb.AppendLine("ORDER BY dbo.Tpa.Ord, TpaNom, Stp.Ord, ArtNomCurt")


        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)


        Do While oDrd.Read
            If oBrand.NotEquals(oDrd("TpaGuid")) Then
                oBrand = New DTOProductBrand(oDrd("TpaGuid"))
                With oBrand
                    .Nom = oDrd("TpaNom")
                    .Categories = New List(Of DTOProductCategory)
                End With
                oTarifa.Brands.Add(oBrand)
            End If

            If oCategory.NotEquals(oDrd("StpGuid")) Then
                oCategory = New DTOProductCategory(oDrd("StpGuid"))
                With oCategory
                    .Brand = oBrand
                    .Nom = oDrd("StpNom")
                    .Skus = New List(Of DTOProductSku)
                End With
                oBrand.Categories.Add(oCategory)
            End If

            Dim DcRetail As Decimal = 0

            If Not IsDBNull(oDrd("Retail")) Then
                DcRetail = oDrd("Retail")
            End If

            Dim oProductSKU As New DTOProductSku(oDrd("ArtGuid"))
            With oProductSKU
                .Category = oCategory
                .NomCurt = oDrd("ArtNomCurt")
                .RRPP = DTOAmt.Factory(DcRetail)
            End With
            oCategory.Skus.Add(oProductSKU)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
