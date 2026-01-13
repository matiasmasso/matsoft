Public Class StatQuotaOnlineLoader
    Shared Function Quotas(Optional oProveidor As DTOContact = Nothing) As List(Of DTOStatQuotaOnline)
        Dim retval As New List(Of DTOStatQuotaOnline)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Year(Pdc.Fch) AS Year, datepart(q,Pdc.Fch) AS Quarter, SUM(QTY*PNC.EUR*(100-PNC.DTO)/100) AS Base , SUM(QTY*PNC.EUR*(QUOTAONLINE/100)*(100-PNC.DTO)/100) AS Online ")
        sb.AppendLine("FROM PNC ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid ")
        sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid=CliClient.Guid ")
        sb.AppendLine("INNER JOIN Art ON Pnc.ArtGuid=Art.Guid ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category=Stp.Guid ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand = Tpa.Guid ")
        sb.AppendLine("WHERE Year(Pdc.Fch)>2009 AND Pdc.Cod=2 ")
        If oProveidor IsNot Nothing Then
            sb.AppendLine("AND Tpa.Proveidor='" & oProveidor.Guid.ToString & "' ")
        End If
        sb.AppendLine("Group By Year(Pdc.Fch), datepart(q,Pdc.Fch) ")
        sb.AppendLine("Order by Year(Pdc.Fch) DESC,datepart(q,Pdc.Fch) desc")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oQuota As New DTOStatQuotaOnline
            With oQuota
                .Year = oDrd("Year")
                .Quarter = oDrd("Quarter")
                .Base = DTOAmt.Factory(CDec(oDrd("Base")))
                .Online = DTOAmt.Factory(CDec(oDrd("Online")))
            End With
            retval.Add(oQuota)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
