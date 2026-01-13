Public Class QualityDistributionLoader

    Shared Function All(oProveidor As DTOProveidor, fchFrom As Date)
        Dim retval As New List(Of DTOQualityDistribution)
        Dim sFchFrom = Format(fchFrom, "yyyyMMdd")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.FullNom, VwSkuNom.SkuGuid, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("INNER JOIN VwSkuNom on VwSkuNom.SkuGuid=Pnc.ArtGuid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Pdc.Fch>='" & sFchFrom & "' ")
        sb.AppendLine("AND VwSkuNom.Proveidor='" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("AND Pdc.Cod=2 ")
        sb.AppendLine("GROUP BY CliGral.FullNom, CliGral.Guid, VwSkuNom.SkuGuid, VwSkuNom.SkuRef, VwSkuNom.SkuPrvNom ")
        sb.AppendLine("ORDER BY CliGral.FullNom, CliGral.Guid")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oCustomer As New DTOCustomer
        Dim item As DTOQualityDistribution = Nothing
        Do While oDrd.Read
            If Not oCustomer.Guid.Equals(oDrd("Guid")) Then
                oCustomer = New DTOCustomer(oDrd("Guid"))
                oCustomer.FullNom = oDrd("FullNom")
                item = New DTOQualityDistribution
                item.Customer = oCustomer
                retval.Add(item)
            End If
            With item
                .Skus.Add(SQLHelper.GetProductFromDataReader(oDrd))
            End With
        Loop
        oDrd.Close()
        Return retval

    End Function
End Class
