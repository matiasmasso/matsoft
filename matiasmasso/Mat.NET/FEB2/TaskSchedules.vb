Public Class TaskSchedule

#Region "CRUD"
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOTaskSchedule)
        Return Await Api.Fetch(Of DTOTaskSchedule)(exs, "TaskSchedule", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oTaskSchedule As DTOTaskSchedule, exs As List(Of Exception)) As Boolean
        If Not oTaskSchedule.IsLoaded And Not oTaskSchedule.IsNew Then
            Dim pTaskSchedule = Api.FetchSync(Of DTOTaskSchedule)(exs, "TaskSchedule", oTaskSchedule.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTaskSchedule)(pTaskSchedule, oTaskSchedule, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oTaskSchedule As DTOTaskSchedule, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTaskSchedule)(oTaskSchedule, exs, "TaskSchedule")
        oTaskSchedule.IsNew = False
    End Function


    Shared Async Function Delete(oTaskSchedule As DTOTaskSchedule, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTaskSchedule)(oTaskSchedule, exs, "TaskSchedule")
    End Function

#End Region

End Class

Public Class TaskSchedules
    Shared Async Function All(oTask As DTOTask, exs As List(Of Exception)) As Task(Of List(Of DTOTaskSchedule))
        Return Await Api.Fetch(Of List(Of DTOTaskSchedule))(exs, "TaskSchedules", oTask.Guid.ToString())
    End Function


End Class
