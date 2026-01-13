Public Class Filter
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOFilter)
        Return Await Api.Fetch(Of DTOFilter)(exs, "Filter", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oFilter As DTOFilter) As Boolean
        If Not oFilter.IsLoaded And Not oFilter.IsNew Then
            Dim pFilter = Api.FetchSync(Of DTOFilter)(exs, "Filter", oFilter.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOFilter)(pFilter, oFilter, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oFilter As DTOFilter) As Task(Of Boolean)
        Return Await Api.Update(Of DTOFilter)(oFilter, exs, "Filter")
        oFilter.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oFilter As DTOFilter) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOFilter)(oFilter, exs, "Filter")
    End Function
End Class

Public Class Filters
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of DTOFilter.Collection)
        Dim retval As New DTOFilter.Collection
        Try
            retval = Await Api.Fetch(Of DTOFilter.Collection)(exs, "Filters")
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oFilters As List(Of DTOFilter)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTOFilter))(oFilters, exs, "Filters")
    End Function

End Class
