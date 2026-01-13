Public Class ProductStatsLoader

    Shared Function All(oCategory As DTOProductCategory, exs As List(Of Exception)) As List(Of DTOProductStat)
        Dim retval As New List(Of DTOProductStat)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Pnc.ArtGuid, VwProductNom.SkuNom, YEAR(Pdc.Fch) AS Year, MONTH(Pdc.Fch) AS Month, SUM(Pnc.Qty) AS Qty ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwProductNom on Pnc.ArtGuid=VwProductNom.SkuGuid AND VwProductNom.CategoryGuid ='" & oCategory.Guid.ToString & "' ")
        sb.AppendLine("WHERE Pdc.Cod = 2 ")
        sb.AppendLine("GROUP BY Pnc.ArtGuid, VwProductNom.SkuNom, YEAR(Pdc.Fch), MONTH(Pdc.Fch) ")
        sb.AppendLine("ORDER BY VwProductNom.SkuNom, YEAR(Pdc.Fch), MONTH(Pdc.Fch) ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oStat As New DTOProductStat
        Do While oDrd.Read
            If Not oStat.Guid.Equals(oDrd("ArtGuid")) Then
                Dim oSku As New DTOProductSku(oDrd("ArtGuid"))
                SQLHelper.LoadLangTextFromDataReader(oSku.Nom, oDrd, "SkuNom")
                oStat = DTOProductStat.Factory(oSku, exs)
                retval.Add(oStat)
            End If
            Dim item As New DTOYearMonth(oDrd("Year"), oDrd("Month"), oDrd("Qty"))
            oStat.items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
