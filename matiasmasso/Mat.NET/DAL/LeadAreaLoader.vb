Public Class LeadAreasLoader

    Shared Function Consumers(oEmp As DTOEmp, oLang As DTOLang) As DTOLeadAreas
        Dim retval As New DTOLeadAreas
        retval.Countries = New DTOLeadAreas.Country.Collection
        Dim iMaxCount As Integer = Count()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.Adr, Email.Rol ")
        sb.AppendLine(", VwZip.CountryGuid, VwZip.CountryEsp, VwZip.CountryCat, VwZip.CountryEng, VwZip.CountryPor ")
        sb.AppendLine(", VwZip.ZonaGuid, VwZip.ZonaNom, VwZip.LocationNom ")
        sb.AppendLine(", VwZip.LocationGuid, VwZip.LocationNom ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN VwZip ON Email.Pais = VwZip.CountryISO AND Email.ZipCod = VwZip.ZipCod AND VwZip.ZipCod <> '' ")
        sb.AppendLine("WHERE Email.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Email.Rol = " & DTORol.Ids.lead & " ")
        sb.AppendLine("AND Email.FchDeleted IS NULL ")
        sb.AppendLine("AND Email.BadmailGuid IS NULL ")
        sb.AppendLine("AND Email.Privat = 0 ")
        sb.AppendLine("AND Email.NoNews = 0 ")
        sb.AppendLine("AND Email.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY VwZip.CountryEsp, VwZip.ZonaNom, VwZip.LocationNom, VwZip.LocationGuid, Email.Adr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oCountry As New DTOLeadAreas.Country

        Dim oZona As New DTOLeadAreas.Zona
        Dim oLocation As New DTOLeadAreas.Location
        Dim iCount As Integer = 0
        Do While oDrd.Read
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                    If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                        oCountry = New DTOLeadAreas.Country
                        oCountry.Guid = oDrd("CountryGuid")
                        oCountry.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor").Tradueix(oLang)
                        oCountry.Zonas = New DTOLeadAreas.Zona.Collection
                        retval.Countries.Add(oCountry)
                    End If
                    oZona = New DTOLeadAreas.Zona
                    oZona.Guid = oDrd("ZonaGuid")
                    oZona.Nom = oDrd("ZonaNom")
                    oZona.Locations = New DTOLeadAreas.Location.Collection
                    oCountry.Zonas.Add(oZona)
                End If
                oLocation = New DTOLeadAreas.Location
                oLocation.Guid = oDrd("LocationGuid")
                oLocation.Nom = oDrd("LocationNom")
                oLocation.Leads = New DTOLeadAreas.Lead.Collection
                oZona.Locations.Add(oLocation)
            End If
            Dim oLead As New DTOLeadAreas.Lead
            oLead.Guid = oDrd("Guid")
            oLead.Email = oDrd("Adr")
            oLocation.Leads.Add(oLead)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Pro(oEmp As DTOEmp, oLang As DTOLang) As DTOLeadAreas
        Dim retval As New DTOLeadAreas
        retval.Countries = New DTOLeadAreas.Country.Collection
        Dim iMaxCount As Integer = Count()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.Adr, Email.Rol, CliGral.ContactClass ")
        sb.AppendLine(", VwAddress.CountryGuid, VwAddress.CountryEsp, VwAddress.CountryCat, VwAddress.CountryEng, VwAddress.CountryPor ")
        sb.AppendLine(", VwAddress.ZonaGuid, VwAddress.ZonaNom, VwAddress.LocationNom ")
        sb.AppendLine(", VwAddress.LocationGuid, VwAddress.LocationNom ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email.Guid = Email_Clis.EmailGuid ")
        sb.AppendLine("INNER JOIN VwAddress ON Email_Clis.ContactGuid = VwAddress.SrcGuid AND VwAddress.Cod = " & DTOAddress.Codis.Fiscal & " ")
        sb.AppendLine("LEFT OUTER JOIN CliGral ON Email_Clis.ContactGuid = CliGral.Guid ")
        sb.AppendLine("WHERE Email.Emp = " & oEmp.Id & " ")
        sb.AppendLine("AND Email.FchDeleted IS NULL ")
        sb.AppendLine("AND Email.BadmailGuid IS NULL ")
        sb.AppendLine("AND Email.Privat = 0 ")
        sb.AppendLine("AND Email.NoNews = 0 ")
        sb.AppendLine("AND Email.Obsoleto = 0 ")
        sb.AppendLine("AND CliGral.Obsoleto = 0 ")
        sb.AppendLine("ORDER BY VwAddress.CountryEsp, VwAddress.ZonaNom, VwAddress.LocationNom, VwAddress.LocationGuid, Email.Adr")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Dim oCountry As New DTOLeadAreas.Country

        Dim oZona As New DTOLeadAreas.Zona
        Dim oLocation As New DTOLeadAreas.Location
        Dim iCount As Integer = 0
        Do While oDrd.Read
            If Not oLocation.Guid.Equals(oDrd("LocationGuid")) Then
                If Not oZona.Guid.Equals(oDrd("ZonaGuid")) Then
                    If Not oCountry.Guid.Equals(oDrd("CountryGuid")) Then
                        oCountry = New DTOLeadAreas.Country
                        oCountry.Guid = oDrd("CountryGuid")
                        oCountry.Nom = SQLHelper.GetLangTextFromDataReader(oDrd, "CountryEsp", "CountryCat", "CountryEng", "CountryPor").Tradueix(oLang)
                        oCountry.Zonas = New DTOLeadAreas.Zona.Collection
                        retval.Countries.Add(oCountry)
                    End If
                    oZona = New DTOLeadAreas.Zona
                    oZona.Guid = oDrd("ZonaGuid")
                    oZona.Nom = oDrd("ZonaNom")
                    oZona.Locations = New DTOLeadAreas.Location.Collection
                    oCountry.Zonas.Add(oZona)
                End If
                oLocation = New DTOLeadAreas.Location
                oLocation.Guid = oDrd("LocationGuid")
                oLocation.Nom = oDrd("LocationNom")
                oLocation.Leads = New DTOLeadAreas.Lead.Collection
                oZona.Locations.Add(oLocation)
            End If
            Dim oLead As New DTOLeadAreas.Lead
            oLead.Guid = oDrd("Guid")
            oLead.Email = oDrd("Adr")
            oLead.Rol = oDrd("Rol")
            If Not IsDBNull(oDrd("ContactClass")) Then
                oLead.ContactClass = oDrd("ContactClass")
            End If
            oLocation.Leads.Add(oLead)
        Loop
        oDrd.Close()
        Return retval
    End Function

    Shared Function Count() As Integer
        Dim retval As Integer = 0
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Count(Email.Guid) AS Guids")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN VwAreaNom ON Email.ZipCod = VwAreaNom.ZipCod ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        oDrd.Read()

        If Not IsDBNull(oDrd("Guids")) Then
            retval = oDrd("Guids")
        End If
        oDrd.Close()
        Return retval
    End Function
End Class
