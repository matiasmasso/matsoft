Public Class AtlasLoader


    Shared Function Full(oUser As DTOUser) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        Dim sField As String = oUser.Lang.Tradueix("Country.Nom_Esp", "Country.Nom_Cat", "Country.Nom_Eng")
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Country.Guid AS CountryGuid, " & sField & " AS CountryNom ")
        sb.AppendLine(", Zona.Guid AS ZonaGuid, Zona.Nom AS ZonaNom ")
        sb.AppendLine(", Location.Guid AS LocationGuid, Location.Nom AS LocationNom ")
        sb.AppendLine(", Zip.Guid AS ZipGuid, ZipCod ")
        sb.AppendLine(", CliGral.Guid AS ContactGuid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine("FROM Country ")
        sb.AppendLine("INNER JOIN Zona ON Country.Guid=Zona.Country ")
        sb.AppendLine("INNER JOIN Location ON Zona.Guid=Location.Zona ")
        sb.AppendLine("INNER JOIN Zip ON Location.Guid=Zip.Location ")
        sb.AppendLine("INNER JOIN CliAdr ON Zip.Guid=CliAdr.Zip AND CliAdr.Cod=1 ")
        sb.AppendLine("INNER JOIN CliGral ON CliAdr.SrcGuid=CliGral.Guid ")
        Select Case oUser.Rol.Id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager
            Case DTORol.Ids.Manufacturer
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN BrandArea ON VwAreaParent.ChildGuid = BrandArea.Area ")
                sb.AppendLine("INNER JOIN Tpa ON BrandArea.Brand = Tpa.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Tpa.Proveidor = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("INNER JOIN VwAreaParent ON VwAreaParent.ParentGuid = Country.Guid ")
                sb.AppendLine("INNER JOIN RepProducts ON VwAreaParent.ChildGuid = RepProducts.Area AND RepProducts.Cod = " & CInt(DTORepProduct.Cods.Included) & " ")
                sb.AppendLine("INNER JOIN ContactClass ON CliGral.Guid=ContactClass.Guid AND ContactClass.DistributionChannel = RepProducts.DistributionChannel ")
                sb.AppendLine("INNER JOIN Email_Clis ON RepProducts.Rep = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oUser.Guid.ToString & "' ")
                sb.AppendLine("WHERE (RepProducts.FchFrom IS NULL OR RepProducts.FchFrom<=GETDATE()) ")
                sb.AppendLine("AND (RepProducts.FchTo IS NULL OR RepProducts.FchTo>GETDATE()) ")
            Case Else
                sb.AppendLine("AND 1=2 ")
        End Select
        sb.AppendLine("WHERE CliGral.Emp = " & oUser.Emp.Id & " AND CliGral.Obsoleto = 0 ")
        sb.AppendLine("GROUP BY Country.Guid, " & sField & " ")
        sb.AppendLine(", Zona.Guid, Zona.Nom ")
        sb.AppendLine(", Location.Guid, Location.Nom ")
        sb.AppendLine(", Zip.Guid, ZipCod ")
        sb.AppendLine(", CliGral.Guid, CliGral.RaoSocial, CliGral.NomCom ")
        sb.AppendLine("ORDER BY CountryNom, ZonaNom, LocationNom, RaoSocial, NomCom ")

        Dim oCountry As New DTOCountry
        Dim oZona As New DTOZona
        Dim oLocation As New DTOLocation

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                oCountry = New DTOCountry(oDrd("CountryGuid"))
                With oCountry
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryNom", "CountryNom", "CountryNom", "CountryNom")
                    .Zonas = New List(Of DTOZona)
                End With
                retval.Add(oCountry)
            End If
            If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                oZona = New DTOZona(oDrd("ZonaGuid"))
                With oZona
                    .Nom = oDrd("ZonaNom")
                    .Locations = New List(Of DTOLocation)
                End With
                oCountry.Zonas.Add(oZona)
            End If
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                oLocation = New DTOLocation(oDrd("LocationGuid"))
                With oLocation
                    .Nom = oDrd("LocationNom")
                    .Contacts = New List(Of DTOContact)
                End With
                oZona.Locations.Add(oLocation)
            End If
            Dim oContact As New DTOContact(oDrd("ContactGuid"))
            With oContact
                .Emp = oUser.Emp
                .Nom = oDrd("RaoSocial")
                .NomComercial = oDrd("NomCom")
            End With
            oLocation.Contacts.Add(oContact)
        Loop
        oDrd.Close()
        Return retval
    End Function


    Shared Function Compact(user As DTOUser, Optional onlySales As Boolean = False) As List(Of DTOCompactNode)
        Dim retval As New List(Of DTOCompactNode)
        Dim sCountryNomField As String = user.lang.Tradueix("CountryEsp", "CountryCat", "CountryEng")
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CountryGuid, " & sCountryNomField & " ")
        sb.AppendLine(", ZonaGuid, ZonaNom ")
        sb.AppendLine(", LocationGuid, LocationNom ")
        sb.AppendLine(", ContactGuid ")
        sb.AppendLine(", (CASE WHEN NomCom='' THEN RaoSocial ELSE NomCom+ (CASE WHEN RaoSocial='' THEN '' ELSE ' ('+RaoSocial+')' END) END) AS ContactNom ")

        Select Case user.rol.id
            Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                sb.AppendLine("FROM VwAtlas ")
                If onlySales Then
                    sb.AppendLine("INNER JOIN Pdc ON VwAtlas.ContactGuid = Pdc.CliGuid AND Pdc.Cod = " & DTOPurchaseOrder.Codis.client & " ")
                End If
            Case DTORol.Ids.SalesManager
                sb.AppendLine("FROM VwAtlasSalesManager ")
                If onlySales Then
                    sb.AppendLine("INNER JOIN Pdc ON VwAtlasSalesManager.ContactGuid = Pdc.CliGuid AND Pdc.Cod = " & DTOPurchaseOrder.Codis.client & " ")
                End If
                sb.AppendLine("WHERE VwAtlasSalesManager.[User]='" & user.Guid.ToString & "' ")
            Case DTORol.Ids.Rep, DTORol.Ids.Comercial
                sb.AppendLine("FROM VwAtlasRep ")
                If onlySales Then
                    sb.AppendLine("INNER JOIN Pdc ON VwAtlasRep.ContactGuid = Pdc.CliGuid AND Pdc.Cod = " & DTOPurchaseOrder.Codis.client & " ")
                End If
                sb.AppendLine("WHERE VwAtlasRep.[User]='" & user.Guid.ToString & "' ")
            Case Else
                If user.Rol.isStaff Then
                    sb.AppendLine("FROM VwAtlas ")
                    sb.AppendLine("WHERE (VwAtlas.Rol = " & DTORol.Ids.cliFull & " OR VwAtlas.Rol = " & DTORol.Ids.cliLite & ") ")
                    sb.AppendLine("GROUP BY CountryGuid, " & sCountryNomField & " ")
                    sb.AppendLine(", ZonaGuid, ZonaNom ")
                    sb.AppendLine(", LocationGuid, LocationNom ")
                    sb.AppendLine(", ContactGuid,NomCom, RaoSocial ")

                Else
                    Return retval
                    Exit Function
                End If
        End Select

        If onlySales Then
            sb.AppendLine("GROUP BY CountryGuid, " & sCountryNomField & " ")
            sb.AppendLine(", ZonaGuid, ZonaNom ")
            sb.AppendLine(", LocationGuid, LocationNom ")
            sb.AppendLine(", ContactGuid,NomCom, RaoSocial ")

        End If
        sb.AppendLine("ORDER BY " & sCountryNomField & ", ZonaNom, LocationNom, ContactNom ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oCountryNode As New DTOCompactNode
        Dim oZonaNode As New DTOCompactNode
        Dim oLocationNode As New DTOCompactNode
        Do While oDrd.Read
            If Not oCountryNode.guid.Equals(oDrd("CountryGuid")) Then
                oCountryNode = New DTOCompactNode
                With oCountryNode
                    .guid = oDrd("CountryGuid")
                    .nom = SQLHelper.GetStringFromDataReader(oDrd(sCountryNomField))
                    .items = New List(Of DTOCompactNode)
                End With
                retval.Add(oCountryNode)
            End If
            If Not oZonaNode.guid.Equals(oDrd("ZonaGuid")) Then
                oZonaNode = New DTOCompactNode
                With oZonaNode
                    .guid = oDrd("ZonaGuid")
                    .nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                    .items = New List(Of DTOCompactNode)
                End With
                oCountryNode.items.Add(oZonaNode)
            End If
            If Not oLocationNode.guid.Equals(oDrd("LocationGuid")) Then
                oLocationNode = New DTOCompactNode
                With oLocationNode
                    .guid = oDrd("LocationGuid")
                    .nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                    .items = New List(Of DTOCompactNode)
                End With
                oZonaNode.items.Add(oLocationNode)
            End If
            Dim oContactNode As New DTOCompactNode
            With oContactNode
                .guid = oDrd("ContactGuid")
                .nom = oDrd("ContactNom")
            End With
            oLocationNode.items.Add(oContactNode)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
