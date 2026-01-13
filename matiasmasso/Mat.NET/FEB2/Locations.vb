Public Class Location
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOLocation)
        Return Await Api.Fetch(Of DTOLocation)(exs, "Location", oGuid.ToString())
    End Function

    Shared Function FromNomSync(exs As List(Of Exception), LocationNom As String, Optional ISOpais As String = "") As DTOLocation
        If ISOpais = "" Then
            Return Api.ExecuteSync(Of String, DTOLocation)(LocationNom, exs, "Location/FromNom")
        Else
            Return Api.ExecuteSync(Of String, DTOLocation)(LocationNom, exs, "Location/FromNom", ISOpais)
        End If
    End Function

    Shared Async Function FromNom(exs As List(Of Exception), LocationNom As String, ISOpais As String) As Task(Of DTOLocation)
        Return Await Api.Execute(Of String, DTOLocation)(LocationNom, exs, "Location/FromNom")
    End Function

    Shared Async Function FromZip(oCountry As DTOCountry, sZipCod As String, exs As List(Of Exception)) As Task(Of DTOLocation)
        Return Await Api.Fetch(Of DTOLocation)(exs, "Location/FromZip", oCountry.Guid.ToString, sZipCod)
    End Function

    Shared Function Load(ByRef oLocation As DTOLocation, exs As List(Of Exception)) As Boolean
        If Not oLocation.IsLoaded And Not oLocation.IsNew Then
            Dim pLocation = Api.FetchSync(Of DTOLocation)(exs, "Location", oLocation.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOLocation)(pLocation, oLocation, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oLocation As DTOLocation, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOLocation)(oLocation, exs, "Location")
        oLocation.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oLocation As DTOLocation) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Location/delete", oLocation.Guid.ToString())
    End Function


End Class


Public Class Locations
    Shared Async Function FromZona(oZona As DTOZona, exs As List(Of Exception)) As Task(Of DTOLocation)
        Return Await Api.Fetch(Of DTOLocation)(exs, "Locations/FromZona", oZona.Guid.ToString())
    End Function

    Shared Async Function FromZip(oCountry As DTOCountry, sZipCod As String, exs As List(Of Exception)) As Task(Of List(Of DTOLocation))
        Return Await Api.Fetch(Of List(Of DTOLocation))(exs, "Locations/FromZip", oCountry.Guid.ToString, sZipCod)
    End Function

    Shared Async Function reLocate(exs As List(Of Exception), oZonaTo As DTOZona, oLocations As List(Of DTOLocation)) As Task(Of Integer)
        Dim oGuids = oLocations.Select(Function(x) x.Guid).ToList
        Dim retval = Await Api.Execute(Of List(Of Guid), Integer)(oGuids, exs, "Locations/reLocate", oZonaTo.Guid.ToString())
        Return retval
    End Function
End Class