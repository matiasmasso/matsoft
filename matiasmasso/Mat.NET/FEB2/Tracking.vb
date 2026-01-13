Public Class Tracking
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOTracking)
        Return Await Api.Fetch(Of DTOTracking)(exs, "Tracking", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oTracking As DTOTracking) As Boolean
        If Not oTracking.IsLoaded And Not oTracking.IsNew Then
            Dim pTracking = Api.FetchSync(Of DTOTracking)(exs, "Tracking", oTracking.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTracking)(pTracking, oTracking, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oTracking As DTOTracking) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTracking)(oTracking, exs, "Tracking")
        oTracking.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oTracking As DTOTracking) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTracking)(oTracking, exs, "Tracking")
    End Function
End Class

Public Class Trackings
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oTarget As DTOBaseGuid) As Task(Of DTOTracking.Collection)
        Return Await Api.Fetch(Of DTOTracking.Collection)(exs, "Trackings", oTarget.Guid.ToString)
    End Function

End Class

