Public Class Country


    Shared Function Find(oGuid As Guid) As DTOCountry
        Dim retval As DTOCountry = CountryLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(ISO As String) As DTOCountry
        Dim retval As DTOCountry = CountryLoader.Find(ISO)
        Return retval
    End Function

    Shared Function Load(ByRef oCountry As DTOCountry) As Boolean
        Dim retval As Boolean = CountryLoader.Load(oCountry)
        Return retval
    End Function

    Shared Function Update(oCountry As DTOCountry, exs As List(Of Exception)) As Boolean
        Return CountryLoader.Update(oCountry, exs)
    End Function

    Shared Function Delete(oCountry As DTOCountry, exs As List(Of Exception)) As Boolean
        Return CountryLoader.Delete(oCountry, exs)
    End Function

End Class

Public Class Countries
    Shared Function All(oLang As DTOLang) As List(Of DTOCountry)
        Return CountriesLoader.All(oLang)
    End Function

    Shared Function All(oUser As DTOUser) As List(Of DTOCountry)
        Return CountriesLoader.All(oUser)
    End Function

    Shared Function GuidNoms(oLang As DTOLang) As List(Of DTOGuidNom)
        Return CountriesLoader.GuidNoms(oLang)
    End Function

End Class
