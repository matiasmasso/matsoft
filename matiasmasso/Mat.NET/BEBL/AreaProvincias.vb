Public Class AreaProvincia

    Shared Function Find(oGuid As Guid) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = AreaProvinciaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromSpanishZipCod(sZipCod As String) As DTOAreaProvincia
        Dim retval As DTOAreaProvincia = AreaProvinciaLoader.FromSpanishZipCod(sZipCod)
        Return retval
    End Function

    Shared Function Load(ByRef oAreaProvincia As DTOAreaProvincia) As Boolean
        Dim retval As Boolean = AreaProvinciaLoader.Load(oAreaProvincia)
        Return retval
    End Function

    Shared Function Update(oAreaProvincia As DTOAreaProvincia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AreaProvinciaLoader.Update(oAreaProvincia, exs)
        Return retval
    End Function

    Shared Function Delete(oAreaProvincia As DTOAreaProvincia, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AreaProvinciaLoader.Delete(oAreaProvincia, exs)
        Return retval
    End Function

    Shared Function Zonas(oProvincia As DTOAreaProvincia) As List(Of DTOZona)
        Dim retval = AreaProvinciaLoader.Zonas(oProvincia)
        Return retval
    End Function

End Class

Public Class AreaProvincias

    Shared Function All(oCountry As DTOCountry) As List(Of DTOAreaProvincia)
        Dim retval As List(Of DTOAreaProvincia) = AreaProvinciasLoader.All(oCountry)
        Return retval
    End Function


End Class
