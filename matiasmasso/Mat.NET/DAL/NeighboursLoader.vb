Public Class NeighboursLoader

    Shared Function NearestNeighbours(oUser As DTOUser, oCoordenadas As GeoHelper.Coordenadas, Optional iCount As Integer = 7, Optional includeSellout As Boolean = False) As List(Of DTONeighbour)
        Dim retval As New List(Of DTONeighbour)
        Dim WKT As String = String.Format("POINT({0} {1})", oCoordenadas.Longitud, oCoordenadas.Latitud).Replace(",", ".")
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT TOP(" & iCount & ") VwAddress.Geo.Lat AS Lat, VwAddress.Geo.Long AS Lng, VwAddress.Geo.STDistance('" & WKT & "') AS Distance ")
        sb.AppendLine(", CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", CliGral.NIF, CliGral.NifCod, CliGral.Nif2, CliGral.Nif2Cod ")
        sb.AppendLine(", VwAddress.Adr, VwAddress.ZipGuid, VwAddress.LocationGuid, VwAddress.LocationNom, VwTel.TelNum ")

        If includeSellout Then
            sb.AppendLine(", Turnover.Turnover ")
        End If

        sb.AppendLine("FROM VwAddress  ")
        sb.AppendLine("INNER JOIN CliGral ON VwAddress.SrcGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwTel ON CliGral.Guid = VwTel.Contact ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SalesManager
                sb.AppendLine("INNER JOIN VwSalesManagerCustomers ON CliGral.Guid = VwSalesManagerCustomers.Customer ")
                sb.AppendLine("INNER JOIN Email_Clis On VwSalesManagerCustomers.SalesManager = Email_Clis.ContactGuid ")
                sb.AppendLine("     	 And Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass = ContactClass.Guid ")
                sb.AppendLine("INNER JOIN (")
                sb.AppendLine("     Select RepProducts.Area, RepProducts.DistributionChannel ")
                sb.AppendLine("     FROM RepProducts ")
                sb.AppendLine("     INNER JOIN Email_Clis On RepProducts.Rep = Email_Clis.ContactGuid ")
                sb.AppendLine("          And (RepProducts.FchTo Is NULL Or RepProducts.FchTo>=GETDATE()) ")
                sb.AppendLine("          And (RepProducts.Cod = 1) ")
                sb.AppendLine("     	 And Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("		 GROUP BY RepProducts.Area, RepProducts.DistributionChannel ")
                sb.AppendLine("		 ) X ON X.DistributionChannel=ContactClass.DistributionChannel ")
                sb.AppendLine("				AND (X.Area=VwAddress.CountryGuid OR X.Area=VwAddress.ZonaGuid OR X.Area=VwAddress.LocationGuid) ")
        End Select

        If includeSellout Then
            sb.AppendLine("LEFT OUTER JOIN (SELECT Pdc.CliGuid, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Turnover ")
            sb.AppendLine("     FROM Pnc INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
            sb.AppendLine("     WHERE Pdc.Fch > DATEADD(d,-90,GETDATE()) ")
            sb.AppendLine("     GROUP BY Pdc.CliGuid) Turnover ON CliGral.Guid = Turnover.CliGuid ")
        End If

        sb.AppendLine("WHERE VwAddress.Geo.STDistance('" & WKT & "') IS NOT NULL ")
        sb.AppendLine("AND (CliGral.Rol = " & DTORol.Ids.CliFull & " OR CliGral.Rol=" & DTORol.Ids.CliLite & ") ")
        sb.AppendLine("AND CliGral.Obsoleto = 0 ")

        sb.AppendLine("ORDER BY VwAddress.Geo.STDistance('" & WKT & "')")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim lastGuid As Guid = Guid.NewGuid
        Do While oDrd.Read
            If Not lastGuid.Equals(oDrd("Guid")) Then 'sino surten duplicats
                lastGuid = oDrd("Guid")
                Dim item As New DTONeighbour(lastGuid)
                With item
                    .Nom = oDrd("RaoSocial")
                    .NomComercial = oDrd("NomCom")
                    .Nifs = SQLHelper.GetNifsFromDataReader(oDrd)
                    .Address = SQLHelper.GetAddressFromDataReader(oDrd)
                    .Telefon = SQLHelper.GetStringFromDataReader(oDrd("TelNum"))
                    .Distance = Math.Truncate(SQLHelper.GetDecimalFromDataReader(oDrd("Distance")))
                    If includeSellout Then .Amt = SQLHelper.GetAmtFromDataReader2(oDrd, "Turnover")
                End With
                retval.Add(item)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
