Public Class RepCustomersLoader
    Shared Function All(repOrUser As DTOBaseGuid, Optional oArea As DTOArea = Nothing, Optional IncludeFuture As Boolean = False) As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)

        Dim oLang As DTOLang = Nothing

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT VwAddress.CountryGuid, VwAddress.CountryISO  ")
        sb.AppendLine(", VwAddress.CountryEsp, VwAddress.CountryCat, VwAddress.CountryEng,VwAddress.CountryPor ")
        sb.AppendLine(", VwAddress.ZonaGuid, VwAddress.ZonaNom  ")
        sb.AppendLine(", VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine(", VwAddress.ZipGuid, VwAddress.ZipCod ")
        sb.AppendLine(", VwAddress.Adr, VwAddress.Longitud, VwAddress.Latitud ")
        sb.AppendLine(", VwRepCustomers.Customer, CliGral.RaoSocial, CliGral.NomCom, CliClient.Ref ")

        sb.AppendLine("FROM VwRepCustomers ")
        sb.AppendLine("INNER JOIN CliGral ON VwRepCustomers.Customer = CliGral.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON VwRepCustomers.Customer = CliClient.Guid ")
        sb.AppendLine("INNER JOIN VwAddress ON VwRepCustomers.Customer = VwAddress.SrcGuid ")

        Dim oGuid As Guid = repOrUser.Guid
        If TypeOf repOrUser Is DTORep Then
            oLang = If(DirectCast(repOrUser, DTORep).Lang, DTOLang.ESP)
            sb.AppendLine("WHERE VwRepCustomers.Rep = '" & oGuid.ToString & "' ")
        ElseIf TypeOf repOrUser Is DTOUser Then
            oLang = If(DirectCast(repOrUser, DTOUser).Lang, DTOLang.ESP)
            sb.AppendLine("INNER JOIN Email_Clis ON VwRepCustomers.Rep = Email_Clis.ContactGuid ")
            sb.AppendLine("WHERE Email_Clis.EmailGuid = '" & oGuid.ToString & "' ")
        Else
            sb.AppendLine("WHERE 1=0 ") 'torna zero resultats
        End If

        If Not IncludeFuture Then
            sb.AppendLine("AND VwRepCustomers.FchFrom <= GETDATE() ")
        End If
        If oArea IsNot Nothing Then
            Dim sArea As String = oArea.Guid.ToString
            sb.AppendLine("AND (")
            sb.AppendLine("VwAddress.CountryGuid = '" & sArea & "' ")
            sb.AppendLine("OR VwAddress.ZonaGuid = '" & sArea & "' ")
            sb.AppendLine("OR VwAddress.LocationGuid = '" & sArea & "' ")
            sb.AppendLine("OR VwAddress.ZipGuid = '" & sArea & "' ")
            sb.AppendLine(") ")
        End If

        sb.AppendLine("GROUP BY VwAddress.CountryGuid, VwAddress.CountryISO ")
        sb.AppendLine(", VwAddress.CountryEsp, VwAddress.CountryCat, VwAddress.CountryEng,VwAddress.CountryPor ")
        sb.AppendLine(", VwAddress.ZonaGuid, VwAddress.ZonaNom ")
        sb.AppendLine(", VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine(", VwAddress.ZipGuid, VwAddress.ZipCod ")
        sb.AppendLine(", VwAddress.Adr, VwAddress.Longitud, VwAddress.Latitud ")
        sb.AppendLine(", VwRepCustomers.Customer, CliGral.RaoSocial, CliGral.NomCom, CliClient.Ref  ")

        Dim sCountryNomField As String = oLang.Tradueix("CountryEsp", "CountryCat", "CountryEng", "CountryPor")
        sb.AppendLine("ORDER BY " & sCountryNomField & " ")
        sb.AppendLine(", VwAddress.ZonaNom ")
        sb.AppendLine(", VwAddress.LocationNom ")
        sb.AppendLine(", (CASE WHEN CliGral.RaoSocial='' THEN CliGral.NomCom ELSE CliGral.RaoSocial END)  ")
        sb.AppendLine(", CliClient.Ref ")

        Dim oCountry As New DTOCountry
        Dim oZona As New DTOZona
        Dim oLocation As New DTOLocation
        Dim oZip As New DTOZip

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                oCountry = New DTOCountry(oDrd("CountryGuid"))
                With oCountry
                    .ISO = SQLHelper.GetStringFromDataReader(oDrd("CountryISO"))
                    .LangNom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor")
                    .Zonas = New List(Of DTOZona)
                End With
            End If
            If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                oZona = New DTOZona(oDrd("ZonaGuid"))
                With oZona
                    .Country = oCountry
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("ZonaNom"))
                    .Locations = New List(Of DTOLocation)
                End With
                oCountry.Zonas.Add(oZona)
            End If
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                oLocation = New DTOLocation(oDrd("LocationGuid"))
                With oLocation
                    .Zona = oZona
                    .Nom = SQLHelper.GetStringFromDataReader(oDrd("LocationNom"))
                    .Contacts = New List(Of DTOContact)
                End With
                oZona.Locations.Add(oLocation)
            End If
            If Not oZip.Guid.Equals(oDrd("ZipGuid")) Then
                oZip = New DTOZip(oDrd("ZipGuid"))
                oZip.ZipCod = SQLHelper.GetStringFromDataReader(oDrd("ZipCod"))
                oZip.Location = oLocation
            End If
            Dim oCustomer As New DTOCustomer(oDrd("Customer"))
            With oCustomer
                .Nom = SQLHelper.GetStringFromDataReader(oDrd("RaoSocial"))
                .NomComercial = SQLHelper.GetStringFromDataReader(oDrd("NomCom"))
                .Ref = SQLHelper.GetStringFromDataReader(oDrd("Ref"))
                .FullNom = ContactLoader.FullNom(.Nom, .NomComercial, .Ref)
                .Address = New DTOAddress()
                With .Address
                    .Text = SQLHelper.GetStringFromDataReader(oDrd("Adr"))
                    .Codi = DTOAddress.Codis.Fiscal
                    .Zip = oZip
                    'If Not IsDBNull(oDrd("Latitud")) Then
                    '.Coordenadas = New GeoHelper.Coordenadas(oDrd("Latitud"), oDrd("Longitud"))
                    'End If
                End With
            End With
            oLocation.Contacts.Add(oCustomer)
            retval.Add(oCustomer)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
