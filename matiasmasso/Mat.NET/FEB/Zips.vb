Public Class Zip
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOZip)
        Return Await Api.Fetch(Of DTOZip)(exs, "Zip", oGuid.ToString())
    End Function

    Shared Async Function FromZipCod(oCountry As DTOCountry, sZipCod As String, exs As List(Of Exception)) As Task(Of DTOZip)
        Return Await Api.Fetch(Of DTOZip)(exs, "Zip", oCountry.Guid.ToString, sZipCod)
    End Function

    Shared Async Function FindOrCreate(exs As List(Of Exception), oCountry As DTOCountry, zipcod As String, locationNom As String) As Task(Of DTOZip)
        Dim retval = Await FromZipCod(oCountry, zipcod, exs)
        If retval Is Nothing Then
            Dim oLocation = Await Location.FromNom(exs, locationNom, oCountry.ISO)
            If oLocation Is Nothing Then
                Dim oUnknownZona = Await Zona.FromNom(exs, "-", oCountry.ISO)
                If oUnknownZona Is Nothing Then
                    oUnknownZona = DTOZona.Factory(oCountry, "-")
                    oUnknownZona.ExportCod = oCountry.ExportCod
                    oUnknownZona.Lang = oCountry.Lang
                    Await Zona.Update(oUnknownZona, exs)
                End If
                oLocation = DTOLocation.Factory(oUnknownZona, locationNom)
                Await Location.Update(oLocation, exs)
            Else
                retval = DTOZip.Factory(oLocation, zipcod)
                Await Update(retval, exs)
            End If
        End If
        Return retval
    End Function

    Shared Async Function FromString(src As String, exs As List(Of Exception)) As Task(Of DTOZip)
        Dim retval As DTOZip = Nothing
        'Dim sZipRegexPattern As String = "^([1-9]{2}|[0-9][1-9]|[1-9][0-9])[0-9]{3}$"
        Dim sZipRegexPattern As String = "\b([1-9]{5}|[0-9][1-9]|[1-9][0-9])[0-9]{3}\b"
        Dim oMatch As System.Text.RegularExpressions.Match = System.Text.RegularExpressions.Regex.Match(src, sZipRegexPattern)
        If oMatch.Success Then
            Dim sZip As String = src.Substring(oMatch.Index, oMatch.Length)
            Dim oCountry = DTOCountry.Wellknown(DTOCountry.Wellknowns.Spain)
            retval = Await Zip.FromZipCod(oCountry, sZip, exs)
        End If
        Return retval
    End Function

    Shared Function Load(ByRef oZip As DTOZip, exs As List(Of Exception)) As Boolean
        If Not oZip.IsLoaded And Not oZip.IsNew Then
            Dim pZip = Api.FetchSync(Of DTOZip)(exs, "Zip", oZip.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOZip)(pZip, oZip, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOZip, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOZip)(value, exs, "Zip")
    End Function

    Shared Async Function Delete(value As DTOZip, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOZip)(value, exs, "Zip")
    End Function

End Class
Public Class Zips
    Shared Async Function Tree(exs As List(Of Exception), Optional oLang As DTOLang = Nothing) As Task(Of List(Of DTOCountry))
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim retval = Await Api.Fetch(Of List(Of DTOCountry))(exs, "zips", oLang.Tag)
        If exs.Count = 0 Then
            For Each oCountry In retval
                For Each oZona In oCountry.zonas
                    oZona.Country = oCountry
                    For Each oLocation In oZona.locations
                        oLocation.Zona = oZona
                        For Each oZip In oLocation.Zips
                            oZip.Location = oLocation
                        Next
                    Next
                Next
            Next
        End If
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), Optional oLang As DTOLang = Nothing) As Task(Of List(Of DTOZip))
        Dim retval As New List(Of DTOZip)
        If oLang Is Nothing Then oLang = DTOLang.ESP
        Dim oCountriesTree = Await Api.Fetch(Of List(Of DTOCountry))(exs, "zips", oLang.Tag)
        'restaura valors que haguessin provocat error de bucle al deserialitzar el json
        For Each oCountry In oCountriesTree
            For Each oZona In oCountry.Zonas
                oZona.Country = oCountry
                For Each oLocation In oZona.Locations
                    oLocation.Zona = oZona
                    For Each oZip In oLocation.Zips
                        oZip.Location = oLocation
                        retval.Add(oZip)
                    Next
                    oLocation.Zips = New List(Of DTOZip)
                Next
                oZona.Locations = New List(Of DTOLocation)
            Next
            oCountry.Zonas = New List(Of DTOZona) 'per evitar que ho arrossegui per tot arreu
            For Each oRegio In oCountry.Regions
                oRegio.Country = oCountry
                For Each oProvincia In oRegio.Provincias
                    oProvincia.Regio = oRegio
                    For Each oZona In oCountry.Zonas.Where(Function(x) oProvincia.Equals(x.Provincia))
                        oZona.Provincia = oProvincia
                        'oProvincia.Zonas.Add(oZona)
                    Next
                Next
                oRegio.Provincias = New List(Of DTOAreaProvincia)
            Next
            oCountry.Regions = New List(Of DTOAreaRegio)
        Next
        Return retval
    End Function

    Shared Async Function Merge(exs As List(Of Exception), oZips As List(Of DTOZip)) As Task(Of Boolean)
        Dim oGuids = oZips.Select(Function(x) x.Guid).ToList()
        Return Await Api.Execute(Of List(Of Guid), Boolean)(oGuids, exs, "zips/merge")
    End Function

    Shared Async Function All(exs As List(Of Exception), oCountry As DTOCountry, sZipCod As String) As Task(Of DTOZip.Collection)
        Return Await Api.Execute(Of String, DTOZip.Collection)(sZipCod, exs, "zips/FromZipcod", oCountry.Guid.ToString())
    End Function
End Class
