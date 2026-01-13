Public Class Zona
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOZona)
        Return Await Api.Fetch(Of DTOZona)(exs, "Zona", oGuid.ToString())
    End Function

    Shared Async Function FromNom(exs As List(Of Exception), sNom As String, Optional sCountryISO As String = "") As Task(Of DTOZona)
        If sCountryISO = "" Then sCountryISO = "ES"
        Return Await Api.Fetch(Of DTOZona)(exs, "Zona/FromNom", MatHelperStd.UrlHelper.EncodedUrlSegment(sNom), sCountryISO)
    End Function

    Shared Function FromNomSync(exs As List(Of Exception), sNom As String, Optional sCountryISO As String = "") As DTOZona
        If sCountryISO = "" Then sCountryISO = "ES"
        Return Api.FetchSync(Of DTOZona)(exs, "Zona/FromNom", MatHelperStd.UrlHelper.EncodedUrlSegment(sNom), sCountryISO)
    End Function
    Shared Function FromZipSync(exs As List(Of Exception), oCountry As DTOCountry, zip As String) As DTOZona
        Return Api.FetchSync(Of DTOZona)(exs, "Zona/FromZip", oCountry.Guid.ToString, zip)
    End Function

    Shared Function Load(ByRef oZona As DTOZona, exs As List(Of Exception)) As Boolean
        If Not oZona.IsLoaded And Not oZona.IsNew Then
            Dim pZona = Api.FetchSync(Of DTOZona)(exs, "Zona", oZona.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOZona)(pZona, oZona, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oZona As DTOZona, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOZona)(oZona, exs, "Zona")
        oZona.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oZonaFrom As DTOZona, Optional oZonaTo As DTOZona = Nothing) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Zona/delete", oZonaFrom.Guid.ToString, OpcionalGuid(oZonaTo))
    End Function

End Class

Public Class Zonas

    Shared Async Function All(oCountry As DTOCountry, exs As List(Of Exception)) As Task(Of List(Of DTOZona))
        Return Await Api.Fetch(Of List(Of DTOZona))(exs, "Zonas", oCountry.Guid.ToString())
    End Function
    Shared Async Function All(exs As List(Of Exception), oPostalCode As DTO.Google.Geonames.postalCodeClass) As Task(Of List(Of DTOZona))
        Return Await Api.Execute(Of DTO.Google.Geonames.postalCodeClass, List(Of DTOZona))(oPostalCode, exs, "Zonas/fromGeoNamePostalCode")
    End Function

End Class
