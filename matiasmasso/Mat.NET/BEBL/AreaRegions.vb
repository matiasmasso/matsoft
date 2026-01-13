Public Class AreaRegio


    Shared Function Find(oGuid As Guid) As DTOAreaRegio
        Dim retval As DTOAreaRegio = AreaRegioLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oAreaRegio As DTOAreaRegio) As Boolean
        Dim retval As Boolean = AreaRegioLoader.Load(oAreaRegio)
        Return retval
    End Function

    Shared Function Update(oAreaRegio As DTOAreaRegio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AreaRegioLoader.Update(oAreaRegio, exs)
        Return retval
    End Function

    Shared Function Delete(oAreaRegio As DTOAreaRegio, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AreaRegioLoader.Delete(oAreaRegio, exs)
        Return retval
    End Function


End Class

Public Class AreaRegions

    Shared Function All(oCountry As DTOCountry) As List(Of DTOAreaRegio)
        Dim retval As List(Of DTOAreaRegio) = AreaRegionsLoader.All(oCountry)
        Return retval
    End Function

End Class

