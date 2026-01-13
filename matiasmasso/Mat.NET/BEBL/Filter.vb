Public Class Filter

    Shared Function Find(oGuid As Guid) As DTOFilter
        Return FilterLoader.Find(oGuid)
    End Function

    Shared Function Update(oFilter As DTOFilter, exs As List(Of Exception)) As Boolean
        Return FilterLoader.Update(oFilter, exs)
    End Function

    Shared Function Delete(oFilter As DTOFilter, exs As List(Of Exception)) As Boolean
        Return FilterLoader.Delete(oFilter, exs)
    End Function

End Class



Public Class Filters
    Shared Function All() As List(Of DTOFilter)
        Dim retval As List(Of DTOFilter) = FiltersLoader.All()
        Return retval
    End Function

    Shared Function All(oBrand As DTOProductBrand) As List(Of DTOFilter)
        Dim retval As List(Of DTOFilter) = FiltersLoader.All(oBrand)
        Return retval
    End Function


    Shared Function Update(oFilters As List(Of DTOFilter), ByRef exs As List(Of Exception)) As Boolean
        Return FiltersLoader.Update(oFilters, exs)
    End Function

End Class

