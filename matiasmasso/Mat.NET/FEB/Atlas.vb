Public Class Atlas
    Inherits _FeblBase

    Shared Async Function Compact(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOCompactNode))
        Return Await Api.Fetch(Of List(Of DTOCompactNode))(exs, "atlas/compact", oUser.Guid.ToString)
    End Function

End Class
