Public Class Tracking

    Shared Function Find(oGuid As Guid) As DTOTracking
        Return TrackingLoader.Find(oGuid)
    End Function

    Shared Function Update(oTracking As DTOTracking, exs As List(Of Exception)) As Boolean
        Return TrackingLoader.Update(oTracking, exs)
    End Function

    Shared Function Delete(oTracking As DTOTracking, exs As List(Of Exception)) As Boolean
        Return TrackingLoader.Delete(oTracking, exs)
    End Function

End Class



Public Class Trackings
    Shared Function All(oTarget As DTOBaseGuid) As DTOTracking.Collection
        Dim retval As DTOTracking.Collection = TrackingsLoader.All(oTarget)
        Return retval
    End Function
End Class

