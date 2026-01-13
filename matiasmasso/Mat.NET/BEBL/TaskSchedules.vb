Public Class TaskSchedule


#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOTaskSchedule
        Dim retval As DTOTaskSchedule = TaskScheduleLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oTaskSchedule As DTOTaskSchedule, exs As List(Of Exception)) As Boolean
        Return TaskScheduleLoader.Update(oTaskSchedule, exs)
    End Function

    Shared Function Delete(oTaskSchedule As DTOTaskSchedule, exs As List(Of Exception)) As Boolean
        Return TaskScheduleLoader.Delete(oTaskSchedule, exs)
    End Function

#End Region

End Class

Public Class TaskSchedules

    Shared Function All(oTask As DTOTask) As List(Of DTOTaskSchedule)
        Dim retval As List(Of DTOTaskSchedule) = TaskSchedulesLoader.All(oTask)
        Return retval
    End Function


End Class
