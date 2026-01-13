Public Class Country

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCountry)
        Return Await Api.Fetch(Of DTOCountry)(exs, "country", oGuid.ToString())
    End Function

    Shared Async Function FromIso(iso As String, exs As List(Of Exception)) As Task(Of DTOCountry)
        Return Await Api.Fetch(Of DTOCountry)(exs, "country/fromIso", iso)
    End Function

    Shared Function FromIsoSync(iso As String, exs As List(Of Exception)) As DTOCountry
        Return Api.FetchSync(Of DTOCountry)(exs, "country/fromIso", iso)
    End Function

    Shared Function Load(ByRef oCountry As DTOCountry, exs As List(Of Exception)) As Boolean
        If Not oCountry.IsLoaded And Not oCountry.IsNew Then
            Dim pCountry = Api.FetchSync(Of DTOCountry)(exs, "Country", oCountry.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCountry)(pCountry, oCountry, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(value As DTOCountry, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCountry)(value, exs, "Country")
        value.IsNew = False
    End Function

    Shared Async Function Delete(oCountry As DTOCountry, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCountry)(oCountry, exs, "country")
    End Function
End Class

Public Class Countries

    Shared Async Function All(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOCountry))
        Return Await Api.Fetch(Of List(Of DTOCountry))(exs, "countries", oLang.Tag)
    End Function
    Shared Function AllSync(oLang As DTOLang, exs As List(Of Exception)) As List(Of DTOCountry)
        Return Api.FetchSync(Of List(Of DTOCountry))(exs, "countries", oLang.Tag)
    End Function

    Shared Function AllSync(oUser As DTOUser, exs As List(Of Exception)) As List(Of DTOCountry)
        Return Api.FetchSync(Of List(Of DTOCountry))(exs, "countriesByUser", oUser.Guid.ToString())
    End Function

    Shared Async Function GuidNoms(oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOGuidNom))
        Return Await Api.Fetch(Of List(Of DTOGuidNom))(exs, "countries/guidnoms", oLang.Tag)
    End Function
    '
End Class
