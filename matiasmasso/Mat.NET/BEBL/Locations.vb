Public Class Location
    Shared Function Find(oGuid As Guid) As DTOLocation
        Dim retval As DTOLocation = LocationLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNom(sNom As String, Optional sCountryISO As String = "") As DTOLocation
        Dim retval As DTOLocation = LocationLoader.FromNom(sNom, sCountryISO)
        Return retval
    End Function

    Shared Function FromZip(oCountry As DTOCountry, sZipCod As String) As DTOLocation
        Dim retval As DTOLocation = LocationLoader.FromZip(oCountry, sZipCod)
        Return retval
    End Function

    Shared Function Update(oLocation As DTOLocation, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = LocationLoader.Update(oLocation, exs)
        Return retval
    End Function

    Shared Function Delete(exs As List(Of Exception), oLocation As DTOLocation) As Boolean
        Dim retval As Boolean = LocationLoader.Delete(exs, oLocation)
        Return retval
    End Function

End Class

Public Class Locations
    Shared Function FromZona(oZona As DTOZona) As List(Of DTOLocation)
        Dim retval As List(Of DTOLocation) = LocationsLoader.FromZona(oZona)
        Return retval
    End Function

    Shared Function reLocate(exs As List(Of Exception), oZonaTo As DTOZona, oLocations As List(Of DTOLocation)) As Integer
        Return LocationsLoader.reLocate(exs, oZonaTo, oLocations)
    End Function

End Class

