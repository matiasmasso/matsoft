Public Class PeriodProgressLoader

    Shared Function AreaCustomers(oEmp As DTOEmp, oArea As DTOArea) As List(Of DTOPeriodProgress)
        Dim retval As New List(Of DTOPeriodProgress)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom, CliGral.Rol, CliClient.Ref ")
        sb.AppendLine(", SUM(CASE WHEN Pdc.Fch>DATEADD(d,-90,GETDATE()) THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS QuarterCurrent ")
        sb.AppendLine(", SUM(CASE WHEN Pdc.Fch BETWEEN DATEADD(d,-180,GETDATE()) AND DATEADD(d,-90,GETDATE()) THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS QuarterPrevious ")
        sb.AppendLine(", CliAdr.Adr, CliAdr.Zip, Area2.CountryGuid, Area2.CountryISO, Area2.CountryNomEsp, Area2.CountryNomCat, Area2.CountryNomEng, Area2.ZonaGuid, Area2.ZonaNom, Area2.LocationGuid, Area2.LocationNom, Area2.ZipCod ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid = CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
        sb.AppendLine("INNER JOIN Area2 ON CliAdr.Zip = Area2.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pdc ON Pdc.CliGuid = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON Pdc.Guid = Pnc.PdcGuid ")
        sb.AppendLine("WHERE CliGral.Emp =" & oEmp.Id & " ")
        sb.AppendLine("AND (Area2.ZipGuid = '" & oArea.Guid.ToString & "' OR Area2.LocationGuid = '" & oArea.Guid.ToString & "'  OR Area2.ZonaGuid = '" & oArea.Guid.ToString & "'  OR Area2.CountryGuid = '" & oArea.Guid.ToString & "') ")
        sb.AppendLine("AND Pdc.Fch > DateAdd(d, -180, GETDATE()) ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom, CliGral.Rol, CliClient.Ref ")
        sb.AppendLine(", CliAdr.Adr, CliAdr.Zip, Area2.CountryISO, Area2.CountryGuid, Area2.CountryNomEsp, Area2.CountryNomCat, Area2.CountryNomEng, Area2.ZonaGuid, Area2.ZonaNom, Area2.LocationGuid, Area2.LocationNom, Area2.ZipCod ")
        sb.AppendLine("ORDER BY SUM(CASE WHEN Pdc.Fch>DATEADD(d,-90,GETDATE()) THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) DESC, CliGral.RaoSocial ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("Guid"))
            With oCustomer
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
                If Not IsDBNull(oDrd("Ref")) Then
                    .Ref = oDrd("Ref")
                End If
                .Rol = New DTORol(oDrd("Rol"))
                .Address = New DTOAddress
                .Address.Text = oDrd("Adr")
                .Address.Zip = ZipLoader.NewZip(oDrd("Zip"), oDrd("ZipCod"), oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
            End With
            Dim item As New DTOPeriodProgress
            With item
                .Contact = oCustomer
                If Not IsDBNull(oDrd("QuarterCurrent")) Then
                    .PeriodCurrent = DTOAmt.Factory(CDec(oDrd("QuarterCurrent")))
                End If
                If Not IsDBNull(oDrd("QuarterPrevious")) Then
                    .PeriodPrevious = DTOAmt.Factory(CDec(oDrd("QuarterPrevious")))
                End If
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function RepQuarters(oEmp As DTOEmp) As List(Of DTOPeriodProgress)
        Dim retval As New List(Of DTOPeriodProgress)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliRep.Guid, CliRep.Abr ")
        sb.AppendLine(", SUM(CASE WHEN Pdc.Fch>DATEADD(d,-90,GETDATE()) THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS QuarterCurrent ")
        sb.AppendLine(", SUM(CASE WHEN Pdc.Fch BETWEEN DATEADD(d,-180,GETDATE()) AND DATEADD(d,-90,GETDATE()) THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS QuarterPrevious ")
        sb.AppendLine("FROM CliRep ")
        sb.AppendLine("LEFT OUTER JOIN Pnc ON CliRep.Guid = Pnc.RepGuid ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("WHERE Pdc.Emp =" & oEmp.Id & " And Pdc.Fch > DateAdd(d, -180, GETDATE()) ")
        sb.AppendLine("GROUP BY CliRep.Guid, CliRep.Abr ")
        sb.AppendLine("ORDER BY CliRep.Abr ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRep As New DTORep(oDrd("Guid"))
            With oRep
                .Nom = oDrd("Abr")
            End With
            Dim item As New DTOPeriodProgress
            With item
                .Contact = oRep
                .PeriodCurrent = DTOAmt.Factory(CDec(oDrd("QuarterCurrent")))
                .PeriodPrevious = DTOAmt.Factory(CDec(oDrd("QuarterPrevious")))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
