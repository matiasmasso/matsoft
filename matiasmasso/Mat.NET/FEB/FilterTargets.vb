Public Class FilterTargets
    Inherits _FeblBase

    Shared Async Function Filters(exs As List(Of Exception), oTargets As IEnumerable(Of DTOBaseGuid)) As Task(Of List(Of DTOFilter))
        Dim oGuids = oTargets.Select(Function(x) x.Guid).ToList()
        Return Await Api.Execute(Of List(Of Guid), List(Of DTOFilter))(oGuids, exs, "FilterTargets/Filters")
    End Function

    Shared Async Function All(exs As List(Of Exception), oTarget As DTOBaseGuid) As Task(Of List(Of DTOFilter.Item))
        Return Await Api.Fetch(Of List(Of DTOFilter.Item))(exs, "FilterTargets", oTarget.Guid.ToString)
    End Function

    Shared Async Function Update(exs As List(Of Exception), oTarget As DTOBaseGuid, oItems As List(Of DTOFilter.Item)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTOFilter.Item))(oItems, exs, "FilterTargets", oTarget.Guid.ToString)
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oTarget As DTOBaseGuid) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "FilterTargets/delete", oTarget.Guid.ToString)
    End Function


End Class

