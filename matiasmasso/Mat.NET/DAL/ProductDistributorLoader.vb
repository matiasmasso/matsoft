Public Class ProductDistributorsLoader


    Shared Function Britax(days As Integer) As List(Of DTOProductRetailer)
        Dim retval As New List(Of DTOProductRetailer)
        Dim BritaxGuid As String = "47C3A677-89C3-4B5E-86A4-25434CE415D5"

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT (CASE WHEN CliGral.NomCom='' THEN CliGral.RaoSocial ELSE CliGral.NomCom END) AS Nom ")
        sb.AppendLine(", CliGral.Cli, VwAddress.adr, Art.Ref  ")
        sb.AppendLine(", VwAddress.ZipCod, VwAddress.LocationNom, VwAddress.ZonaNom, VwAddress.CountryISO  ")
        sb.AppendLine("        FROM Pnc  ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid  ")
        sb.AppendLine("INNER JOIN VwCcxOrMe C1 ON Pdc.CliGuid = C1.Guid ")
        sb.AppendLine("INNER JOIN VwCcxOrMe C2 ON C1.Ccx = C2.Ccx ")
        sb.AppendLine("INNER JOIN CliGral ON C2.Guid = CliGral.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON CliGral.Guid = VwAddress.SrcGuid  ")
        sb.AppendLine("INNER JOIN Art ON Pnc.ArtGuid=Art.Guid  ")
        sb.AppendLine("INNER JOIN Stp ON Art.Category=Stp.Guid  ")
        sb.AppendLine("INNER JOIN Tpa ON Stp.Brand=Tpa.Guid  ")
        sb.AppendLine("WHERE Tpa.Proveidor='" & BritaxGuid & "'  ")
        sb.AppendLine("AND CliGral.Obsoleto = 0  ")
        sb.AppendLine("AND Art.Ref>''  ")
        sb.AppendLine("AND Pdc.Fch > DATEADD(dd,-60,GETDATE())  ")
        sb.AppendLine("GROUP BY CliGral.Cli, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", VwAddress.adr, Art.Ref  ")
        sb.AppendLine(", VwAddress.ZipCod, VwAddress.LocationNom, VwAddress.ZonaNom, VwAddress.CountryISO  ")
        sb.AppendLine("ORDER BY VwAddress.CountryISO , VwAddress.ZonaNom , VwAddress.LocationNom, CliGral.NomCom, CliGral.RaoSocial, CliGral.Cli, Art.Ref  ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim iCli As Integer
        Dim oRetailer As DTOProductRetailer = Nothing
        Do While oDrd.Read
            If iCli <> oDrd("Cli") Then
                iCli = oDrd("Cli")
                oRetailer = New DTOProductRetailer
                With oRetailer
                    .Id = iCli
                    .Country = oDrd("CountryISO")
                    .Region = oDrd("ZonaNom")
                    .Location = oDrd("LocationNom")
                    .Name = oDrd("Nom")
                    .Address = oDrd("Adr").ToString.Replace(vbCrLf, "\n")
                    .ZipCod = oDrd("ZipCod")
                    .Items = New List(Of DTOProductSku)
                End With
                retval.Add(oRetailer)
            End If
            Dim item As New DTOProductSku
            item.RefProveidor = oDrd("Ref")
            oRetailer.Items.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromBrand(oProductBrand As DTOProductBrand) As List(Of DTOProductRetailer)

        Dim retval As New List(Of DTOProductRetailer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Country.Nom_Esp AS CountryNom, Zona.Nom AS ZonaNom, Location.Nom AS LocationNom ")
        sb.AppendLine(", Web.Client AS CliGuid, Web.Nom AS CliNom, Web.Adr, Zip.ZipCod, VwTel.telNum ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN Location ON Web.Location=Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("LEFT OUTER JOIN VwTel ON Web.Client=VwTel.Contact ")
        sb.AppendLine("INNER JOIN CliAdr ON Web.Client = CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip = Zip.Guid ")

        sb.AppendLine("WHERE Web.Brand='" & oProductBrand.Guid.ToString & "' ")
        sb.AppendLine("AND Web.Obsoleto=0 AND Web.Impagat = 0 ")
        sb.AppendLine("GROUP BY Country.Nom_Esp, Zona.Nom, Location.Nom, Web.Client, Web.Nom, Web.Adr, Zip.ZipCod, VwTel.telNum ")
        sb.AppendLine("ORDER BY Country.Nom_Esp, Zona.Nom, Location.Nom, Web.Nom, Web.Adr ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductRetailer
            With item
                .Id = DirectCast(oDrd("CliGuid"), Guid).ToString
                .Country = oDrd("CountryNom")
                .Region = oDrd("ZonaNom")
                .Location = oDrd("LocationNom")
                .Name = oDrd("CliNom")
                .Address = oDrd("Adr").ToString.Replace(vbCrLf, "-")
                .ZipCod = SQLHelper.GetStringFromDataReader(oDrd("ZipCod"))
                .Tel = SQLHelper.GetStringFromDataReader(oDrd("TelNum"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function FromManufacturer(oProveidor As DTOProveidor) As List(Of DTOProductRetailer)

        Dim retval As New List(Of DTOProductRetailer)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwAddress.CountryEsp, VwAddress.ZonaNom, VwAddress.LocationNom ")
        sb.AppendLine(", Web.Nom AS CliNom, VwAddress.Adr, VwAddress.ZipCod ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN VwAddress ON Web.Client=VwAddress.SrcGuid ")
        sb.AppendLine("INNER JOIN Tpa ON Web.Brand = Tpa.Guid ")
        sb.AppendLine("WHERE Tpa.Proveidor='" & oProveidor.Guid.ToString & "' ")
        sb.AppendLine("AND Web.Obsoleto=0 AND Web.Impagat = 0 ")
        sb.AppendLine("GROUP BY VwAddress.CountryEsp, VwAddress.ZonaNom, VwAddress.LocationNom ")
        sb.AppendLine(", Web.Nom, VwAddress.Adr, VwAddress.ZipCod ")
        sb.AppendLine("ORDER BY VwAddress.CountryEsp, VwAddress.ZonaNom, VwAddress.LocationNom, Web.Nom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductRetailer
            With item
                .Country = oDrd("CountryEsp")
                .Region = oDrd("ZonaNom")
                .Location = oDrd("LocationNom")
                .Name = oDrd("CliNom")
                .Address = oDrd("Adr").ToString.Replace(vbCrLf, "-")
                .ZipCod = SQLHelper.GetStringFromDataReader(oDrd("ZipCod"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function List(oRep As DTORep) As List(Of DTOProductDistributor)
        Dim retval As New List(Of DTOProductDistributor)
        Dim sField As String = "Nom_Esp"

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, Country.Guid AS CountryGuid, Country." & sField & " AS CountryNom ")
        sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Location.Guid AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine(", (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom WHEN CliGral.NomCom='' THEN CliGral.RaoSocial ELSE CliGral.RaoSocial+' - '+CliGral.NomCom END) AS CliNom ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("INNER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ChildGuid=Zip.Guid ")
        sb.AppendLine("INNER JOIN RepProducts ON RepProducts.Area=VwAreaParent.ParentGuid ")
        sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass=ContactClass.Guid AND ContactClass.DistributionChannel=RepProducts.DistributionChannel ")
        sb.AppendLine("WHERE RepProducts.rep='" & oRep.Guid.ToString & "' ")
        sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=getdate()) ")
        sb.AppendLine("AND RepProducts.Cod=1 ")
        sb.AppendLine("AND CliGral.Obsoleto=0 ")
        sb.AppendLine("AND CliClient.NoRep=0 ")
        sb.AppendLine("GROUP BY Country.Guid, Country." & sField & ", Zona.Guid, Zona.Nom, Location.Guid, Location.Nom, CliGral.RaoSocial, CliGral.NomCom, CliGral.Guid ")
        sb.AppendLine("ORDER BY Country." & sField & ", Zona.Nom, Zona.Guid, Location.Nom, Location.Guid ")
        sb.AppendLine(", (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom ELSE CliGral.RaoSocial+' '+CliGral.NomCom END)")
        Dim SQL As String = sb.ToString
        Dim oCountry As New DTOArea
        Dim oZona As New DTOArea
        Dim oLocation As New DTOArea
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then oCountry = New DTOArea(oDrd("CountryGuid"), oDrd("CountryNom"))
            If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then oZona = New DTOArea(oDrd("ZonaGuid"), oDrd("ZonaNom"))
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then oLocation = New DTOArea(oDrd("LocationGuid"), oDrd("LocationNom"))
            Dim item As New DTOProductDistributor(oDrd("Guid"))
            With item
                .Nom = oDrd("CliNom")
                .Location = oLocation
                .Zona = oZona
                .Country = oCountry
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function List(oUser As DTOUser) As List(Of DTOProductDistributor)
        Dim retval As New List(Of DTOProductDistributor)
        Dim sField As String = "Nom_Esp"

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, Country.Guid AS CountryGuid, Country." & sField & " AS CountryNom ")
        sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Location.Guid AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine(", (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom WHEN CliGral.NomCom='' THEN CliGral.RaoSocial ELSE CliGral.RaoSocial+' - '+CliGral.NomCom END) AS CliNom ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("INNER JOIN CliClient ON CliGral.Guid=CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid=CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
        sb.AppendLine("INNER JOIN Zip ON CliAdr.Zip=Zip.Guid ")
        sb.AppendLine("INNER JOIN Location ON Zip.Location=Location.Guid ")
        sb.AppendLine("INNER JOIN Zona ON Location.Zona=Zona.Guid ")
        sb.AppendLine("INNER JOIN Country ON Zona.Country=Country.Guid ")
        sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ChildGuid=Zip.Guid ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.rep, DTORol.Ids.comercial
                sb.AppendLine("INNER JOIN RepProducts ON RepProducts.Area=VwAreaParent.ParentGuid ")
                sb.AppendLine("INNER JOIN ContactClass ON CliGral.ContactClass=ContactClass.Guid AND ContactClass.DistributionChannel=RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.rep = Email_Clis.ContactGuid ")
                sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=getdate()) ")
                sb.AppendLine("AND RepProducts.Cod=1 ")
        End Select

        sb.AppendLine("AND CliGral.Obsoleto=0 ")
        sb.AppendLine("AND CliClient.NoRep=0 ")
        sb.AppendLine("GROUP BY Country.Guid, Country." & sField & ", Zona.Guid, Zona.Nom, Location.Guid, Location.Nom, CliGral.RaoSocial, CliGral.NomCom, CliGral.Guid ")
        sb.AppendLine("ORDER BY Country." & sField & ", Zona.Nom, Zona.Guid, Location.Nom, Location.Guid ")
        sb.AppendLine(", (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom ELSE CliGral.RaoSocial+' '+CliGral.NomCom END)")
        Dim SQL As String = sb.ToString
        Dim oCountry As New DTOArea
        Dim oZona As New DTOArea
        Dim oLocation As New DTOArea
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then oCountry = New DTOArea(oDrd("CountryGuid"), oDrd("CountryNom"))
            If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then oZona = New DTOArea(oDrd("ZonaGuid"), oDrd("ZonaNom"))
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then oLocation = New DTOArea(oDrd("LocationGuid"), oDrd("LocationNom"))
            Dim item As New DTOProductDistributor(oDrd("Guid"))
            With item
                .Nom = oDrd("CliNom")
                .Location = oLocation
                .Zona = oZona
                .Country = oCountry
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function DistribuidorsOficials(oUser As DTOUser, oBrand As DTOProductBrand, Optional oIncentiu As DTOIncentiu = Nothing) As List(Of DTOProductDistributor)
        Dim retval As New List(Of DTOProductDistributor)
        Dim oLang As DTOLang = oUser.lang
        If oLang Is Nothing Then oLang = DTOLang.Factory("ESP")
        Dim sField As String = oLang.Tradueix("CountryNomEsp", "CountryNomCat", "CountryNomEng")

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom." & sField & " AS CountryNom, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")

        If oIncentiu IsNot Nothing Then
            sb.AppendLine(", X.Promo ")
        End If

        sb.AppendLine("FROM CliTpa ")
        sb.AppendLine("INNER JOIN CliGral ON CliGral.Guid=CliTpa.CliGuid ")
        sb.AppendLine("INNER JOIN CliClient ON CliClient.Guid=CliTpa.CliGuid ")
        sb.AppendLine("INNER JOIN CliAdr ON CliTpa.CliGuid=CliAdr.SrcGuid AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN VwAreaNom ON VwAreaNom.Guid=CliAdr.Zip ")

        Select Case oUser.Rol.id
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                sb.AppendLine("INNER JOIN VwAreaParent  ON VwAreaParent.ChildGuid=CliAdr.Zip ")
                sb.AppendLine("INNER JOIN RepProducts ON RepProducts.Area=VwAreaParent.ParentGuid ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep=Email_Clis.ContactGuid AND Email_Clis.ContactGuid='" & oUser.Guid.ToString & "' ")
                sb.AppendLine("AND RepProducts.FchFrom<=getdate() ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>=getdate()) ")
        End Select

        'nomes per la promo de Inglesina
        If oIncentiu IsNot Nothing Then
            sb.AppendLine("LEFT OUTER JOIN (SELECT Pdc.CliGuid, COUNT(Pdc.Guid) AS Promo FROM Pdc WHERE Pdc.Promo='" & oIncentiu.Guid.ToString & "' GROUP BY CliGuid) X ON X.CliGuid = CliTpa.CliGuid ")
        End If

        sb.AppendLine("WHERE ProductGuid='" & oBrand.Guid.ToString & "' and (CliTpa.cod=1 or CliTpa.cod=4) ")
        Select Case oUser.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager, DTORol.Ids.operadora
            Case DTORol.Ids.comercial, DTORol.Ids.rep
                sb.AppendLine("AND CliClient.Norep = 0 ")
            Case DTORol.Ids.manufacturer
            Case Else
                sb.AppendLine("AND CliTpa.CliGuid<>CliTpa.CliGuid ")
        End Select

        sb.AppendLine("AND CliGral.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine(", VwAreaNom.CountryGuid, VwAreaNom." & sField & ", VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")

        If oIncentiu IsNot Nothing Then
            sb.AppendLine(", X.Promo ")
        End If

        sb.AppendLine("ORDER BY CountryNom, VwAreaNom.ZonaNom, VwAreaNom.LocationNom, RaoSocial")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductDistributor(oDrd("Guid"))
            With item
                .Nom = oDrd("RaoSocial") & IIf(oDrd("NomCom") > "", " '" & oDrd("NomCom") & "'", "")
                If oIncentiu IsNot Nothing Then
                    If Not IsDBNull(oDrd("Promo")) Then
                        .Promo = True
                    End If
                End If
                .Location = oDrd("LocationNom")
                .Zona = oDrd("ZonaNom")
                .Country = oDrd("CountryNom")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function GetZonas(oProduct As DTOProduct, oCountry As DTOCountry) As List(Of DTOZona)
        Dim retval As New List(Of DTOZona)
        Dim SQL As String = "SELECT Zona.Guid, Zona.Nom FROM Zona " _
                            & "INNER JOIN Location ON Zona.Guid = Location.Zona " _
                            & "INNER JOIN Web ON Location.Guid = Web.Location " _
                            & "INNER JOIN VwProductParent ON Web.Category = VwProductParent.Child " _
                            & "WHERE Zona.Country = @Country " _
                            & "AND VwProductParent.Parent = @Product " _
                            & "AND Web.Impagat = 0 " _
                            & "AND Web.Obsoleto = 0 " _
                            & "AND Web.Blocked = 0 " _
                            & "GROUP BY Zona.Guid, Zona.Nom " _
                            & "ORDER BY Zona.Nom"

        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL, "@Product", oProduct.Guid.ToString, "@Country", oCountry.Guid.ToString())
        Do While oDrd.Read
            Dim oGuid As Guid = oDrd("Guid")
            Dim oZona As New DTOZona(oGuid)
            oZona.Nom = oDrd("Nom")
            retval.Add(oZona)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function BestLocation(oProduct As DTOProduct, Optional oParentArea As DTOArea = Nothing) As DTOLocation
        Dim retval As DTOLocation = Nothing

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")
        sb.AppendLine("FROM Web ")
        sb.AppendLine("INNER JOIN VwProductParent ON Web.Category = VwProductParent.Child ")
        sb.AppendLine("INNER JOIN VwAreaNom ON Web.Location = VwAreaNom.Guid ")
        If oParentArea IsNot Nothing Then
            sb.AppendLine("INNER JOIN VwAreaParent ON Web.Location = VwAreaParent.ChildGuid ")
        End If
        sb.AppendLine("WHERE VwProductParent.Parent = '" & oProduct.Guid.ToString & "' ")
        If oParentArea IsNot Nothing Then
            sb.AppendLine("AND VwAreaParent.ParentGuid = '" & oParentArea.Guid.ToString & "' ")
        End If
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Impagat = 0 ")
        sb.AppendLine("AND Web.Obsoleto = 0 ")
        sb.AppendLine("AND Web.Blocked = 0 ")
        sb.AppendLine("GROUP BY VwAreaNom.CountryGuid, VwAreaNom.CountryISO, VwAreaNom.CountryNomEsp, VwAreaNom.CountryNomCat, VwAreaNom.CountryNomEng, VwAreaNom.ZonaGuid, VwAreaNom.ZonaNom, VwAreaNom.LocationGuid, VwAreaNom.LocationNom ")
        sb.AppendLine("ORDER BY SUM(SumCcxVal) DESC")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval = LocationLoader.NewLocation(oDrd("LocationGuid"), oDrd("LocationNom"), oDrd("ZonaGuid"), oDrd("ZonaNom"), oDrd("CountryISO"), oDrd("CountryGuid"), oDrd("CountryNomEsp"), oDrd("CountryNomCat"), oDrd("CountryNomEng"))
        End If
        oDrd.Close()
        Return retval
    End Function

    Shared Function PerChannel(oUser As DTOUser, iYear As Integer) As MatHelper.Excel.Book
        Dim sFilename As String = String.Format("M+O Channel Split {0:yyyy.MM.dd}", DTO.GlobalVariables.Today())
        Dim retval As New MatHelper.Excel.Book(sFilename)
        retval.Sheets.Add(PerChannelSummary(oUser, iYear))
        retval.Sheets.Add(PerChannelDetail(oUser, iYear))
        Return retval
    End Function

    Shared Function PerChannelSummary(oUser As DTOUser, iYear As Integer) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("Summary")
        With retval
            .AddColumn("Channel", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Sale points", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Volume", MatHelper.Excel.Cell.NumberFormats.Euro)
            For i As Integer = 1 To 12
                .AddColumn(DTOLang.ENG.MesAbr(i), MatHelper.Excel.Cell.NumberFormats.Euro)
            Next
        End With
        Dim oRowSum As MatHelper.Excel.Row = retval.AddRow
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT DistributionChannel.NomEsp, COUNT(DISTINCT Pdc.CliGuid) AS Clis ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=1 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M01 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=2 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M02 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=3 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M03 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=4 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M04 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=5 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M05 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=6 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M06 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=7 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M07 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=8 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M08 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=9 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M09 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=10 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M10 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=11 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M11 ")
        sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=12 THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M12 ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid AND Pdc.Cod=2 ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor=Email_Clis.ContactGuid ")
        sb.AppendLine("INNER JOIN VwContactChannel ON Pdc.CliGuid = VwContactChannel.Contact ")
        sb.AppendLine("INNER JOIN DistributionChannel ON VwContactChannel.Channel = DistributionChannel.Guid ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid='" & oUser.Guid.ToString & "' ")
        sb.AppendLine("AND Year(Pdc.FchCreated)=" & iYear & " ")
        sb.AppendLine("GROUP BY DistributionChannel.Ord, DistributionChannel.NomEsp ")
        sb.AppendLine("ORDER BY DistributionChannel.Ord, DistributionChannel.NomEsp ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(oDrd("NomEsp"))
            oRow.AddCell(oDrd("Clis"))
            Dim oCellTot As MatHelper.Excel.Cell = oRow.AddCell(0)
            oCellTot.FormulaR1C1 = "SUM(R[0]C[+1]:R[0]C[+12])"
            For i As Integer = 1 To 12
                Dim iVal As Decimal = oDrd("M" & Format(i, "00"))
                oRow.AddCell(iVal)
            Next
        Loop
        oDrd.Close()

        With oRowSum
            Dim oCell As MatHelper.Excel.Cell = .AddCell("Total")
            oCell.CellStyle = MatHelper.Excel.Cell.CellStyles.Total
            For i As Integer = 1 To 14
                oCell = .AddCell()
                oCell.FormulaR1C1 = "SUM(R[1]C[0]:R[+" & retval.Rows.Count & "]C[0])"
                oCell.CellStyle = MatHelper.Excel.Cell.CellStyles.Total
            Next
        End With

        Return retval
    End Function


    Shared Function PerChannelDetail(oUser As DTOUser, iYear As Integer) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("Detail")
        With retval
            .AddColumn("Channel", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Country", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Area", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Location", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Customer", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Volume", MatHelper.Excel.Cell.NumberFormats.Euro)
            For i As Integer = 1 To 12
                .AddColumn(DTOLang.ENG.MesAbr(i), MatHelper.Excel.Cell.NumberFormats.Euro)
            Next
        End With
        Dim oRowSum As MatHelper.Excel.Row = retval.AddRow
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT DistributionChannel.NomEsp AS Channel, VwAddress.CountryEsp, VwAddress.ZonaNom, VwAddress.LocationNom, (CASE WHEN CliGral.RaoSocial>'' THEN CliGral.RaoSocial+' '+CliGral.NomCom ELSE CliGral.NomCom END) AS CliNom ")
        For i As Integer = 1 To 12
            sb.AppendLine(", CAST(SUM(CASE WHEN MONTH(Pdc.FchCreated)=" & i & " THEN Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100 ELSE 0 END) AS INT) AS M" & Format(i, "00") & " ")
        Next
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid=Pdc.Guid AND Pdc.Cod=2  ")
        sb.AppendLine("INNER JOIN VwSkuNom ON Pnc.ArtGuid = VwSkuNom.SkuGuid  ")
        sb.AppendLine("INNER JOIN Email_Clis ON VwSkuNom.Proveidor=Email_Clis.ContactGuid  ")
        sb.AppendLine("INNER JOIN VwContactChannel ON Pdc.CliGuid = VwContactChannel.Contact  ")
        sb.AppendLine("INNER JOIN DistributionChannel ON VwContactChannel.Channel = DistributionChannel.Guid  ")
        sb.AppendLine("INNER JOIN VwAddress ON Pdc.CliGuid = VwAddress.SrcGuid  ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid  ")
        sb.AppendLine("WHERE Email_Clis.EmailGuid='7e33a7bd-2941-43fa-acd3-6ad264eb3a5c'  ")
        sb.AppendLine("AND Year(Pdc.FchCreated)=2017  ")
        sb.AppendLine("GROUP BY DistributionChannel.Ord, DistributionChannel.NomEsp, DistributionChannel.NomEsp, VwAddress.CountryEsp, VwAddress.ZonaNom, VwAddress.LocationNom, CliGral.RaoSocial,CliGral.NomCom ")
        sb.AppendLine("ORDER BY DistributionChannel.Ord, DistributionChannel.NomEsp, VwAddress.CountryEsp, VwAddress.ZonaNom, VwAddress.LocationNom ,(CASE WHEN CliGral.RaoSocial>'' THEN CliGral.RaoSocial+' '+CliGral.NomCom ELSE CliGral.NomCom END) ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(oDrd("Channel"))
            oRow.AddCell(oDrd("CountryEsp"))
            oRow.AddCell(oDrd("ZonaNom"))
            oRow.AddCell(oDrd("LocationNom"))
            oRow.AddCell(oDrd("CliNom"))
            Dim oCellTot As MatHelper.Excel.Cell = oRow.AddCell(0)
            oCellTot.FormulaR1C1 = "SUM(R[0]C[+1]:R[0]C[+12])"
            For i As Integer = 1 To 12
                Dim iVal As Decimal = oDrd("M" & Format(i, "00"))
                oRow.AddCell(iVal)
            Next
        Loop
        oDrd.Close()

        With oRowSum
            Dim oCell As MatHelper.Excel.Cell = .AddCell("Total")
            oCell.CellStyle = MatHelper.Excel.Cell.CellStyles.Total
            For i As Integer = 1 To 4
                oCell = .AddCell()
                oCell.CellStyle = MatHelper.Excel.Cell.CellStyles.Total
            Next
            For i As Integer = 1 To 13
                oCell = .AddCell()
                oCell.FormulaR1C1 = "SUM(R[1]C[0]:R[+" & retval.Rows.Count & "]C[0])"
                oCell.CellStyle = MatHelper.Excel.Cell.CellStyles.Total
            Next
        End With

        Return retval
    End Function
End Class
