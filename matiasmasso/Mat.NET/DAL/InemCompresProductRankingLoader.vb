Public Class InemCompresProductRankingLoader
    Shared Function All(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOSkuAmtOrigin)
        Dim retval As New List(Of DTOSkuAmtOrigin)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Art.Guid AS SkuGuid, Art.Ref, sum(Qty) AS Qty, SUM(Qty*Arc.Eur*(100-Arc.Dto)/100) AS Amt, SUM(Qty*Arc.Eur*(100-Arc.Dto)/100)/SUM(Qty) ")
        sb.AppendLine(", VwZip.CountryGuid, VwZip.CountryEsp, VwZip.CountryCat, VwZip.CountryEng, VwZip.CountryPor ")
        sb.AppendLine(", SkuNomLlarg.Esp AS SkuNomLlargEsp, SkuNomLlarg.Cat AS SkuNomLlargCat ")
        sb.AppendLine("FROM Arc ")
        sb.AppendLine("INNER JOIN Art ON Arc.ArtGuid=Art.Guid ")
        sb.AppendLine("INNER JOIN VwLangText SkuNomLlarg ON Art.Guid = SkuNomLlarg.Guid AND SkuNomLlarg.Src = 27 ")
        sb.AppendLine("INNER JOIN Alb ON Arc.AlbGuid=Alb.Guid ")
        sb.AppendLine("INNER JOIN VwZip ON Alb.Zip=VwZip.ZipGuid ")
        sb.AppendLine("WHERE Alb.Emp=" & oEmp.Id & " AND Arc.Cod=11 AND Month(Alb.Fch)=" & oYearMonth.Month & " AND Year(Alb.Fch)=" & oYearMonth.Year & " ")
        sb.AppendLine("GROUP BY Art.Guid, Art.Ref, SkuNomLlarg.Text, VwZip.CountryGuid, VwZip.CountryEsp, VwZip.CountryCat, VwZip.CountryEng, VwZip.CountryPor ")
        sb.AppendLine("ORDER BY  SUM(Qty*Arc.Eur*(100-Arc.Dto)/100) DESC ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOSkuAmtOrigin()
            With item
                .Sku = New DTOProductSku(oDrd("SkuGuid"))
                .Sku.RefProveidor = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                SQLHelper.LoadLangTextFromDataReader(.Sku.nomLlarg, oDrd, "SkuNomLlargEsp", "SkuNomLlargCat", "SkuNomLlargEng", "SkuNomLlargPor")
                .Qty = SQLHelper.GetIntegerFromDataReader(oDrd("Qty"))
                .Amt = SQLHelper.GetAmtFromDataReader(oDrd("Amt"))
                .Country = New DTOCountry(oDrd("CountryGuid"))
                .Country.LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
