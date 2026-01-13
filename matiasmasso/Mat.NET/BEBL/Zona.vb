Public Class Zona

    Shared Function Find(oGuid As Guid) As DTOZona
        Dim retval As DTOZona = ZonaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNom(sNom As String, Optional sCountryISO As String = "") As DTOZona
        Dim retval As DTOZona = ZonaLoader.FromNom(sNom.Replace("_", " "), sCountryISO)
        Return retval
    End Function

    Shared Function FromZip(oCountry As DTOCountry, sZipCod As String) As DTOZona
        Dim retval As DTOZona = ZonaLoader.FromZip(oCountry, sZipCod)
        Return retval
    End Function

    Shared Function Update(oZona As DTOZona, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ZonaLoader.Update(oZona, exs)
        Return retval
    End Function

    Shared Function Delete(exs As List(Of Exception), oZonaFrom As DTOZona, oZonaTo As DTOZona) As Boolean
        Dim retval As Boolean = ZonaLoader.Delete(exs, oZonaFrom, oZonaTo)
        Return retval
    End Function


    Shared Function FullNom(oZona As DTOZona, oLang As DTOLang) As String
        Dim retval As String = oZona.Nom
        Dim oCountry As DTOCountry = oZona.Country
        If oCountry IsNot Nothing Then
            If oCountry.ISO <> "ES" Then
                Dim sNom As String = oCountry.LangNom.Tradueix(oLang)
                If sNom > "" Then
                    retval = retval & " (" & sNom & ")"
                End If
            End If
        End If
        Return retval
    End Function
End Class

Public Class Zonas
    Shared Function All(oCountry As DTOCountry) As List(Of DTOZona)
        Return ZonasLoader.All(oCountry)
    End Function

    Shared Function All(oPostalCode As Google.Geonames.postalCodeClass) As List(Of DTOZona)
        Return ZonasLoader.All(oPostalCode)
    End Function

    Shared Function All(oProvincia As DTOAreaProvincia) As List(Of DTOZona)
        Return ZonasLoader.All(oProvincia)
    End Function
End Class
