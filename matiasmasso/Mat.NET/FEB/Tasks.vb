Public Class Task

#Region "CRUD"
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOTask)
        Return Await Api.Fetch(Of DTOTask)(exs, "Task", oGuid.ToString())
    End Function
    Shared Function FindSync(oCod As DTOTask.Cods, exs As List(Of Exception)) As DTOTask
        Return Api.FetchSync(Of DTOTask)(exs, "taskByCod", CInt(oCod))
    End Function


    Shared Function Load(ByRef oTask As DTOTask, exs As List(Of Exception)) As Boolean
        If Not oTask.IsLoaded And Not oTask.IsNew Then
            Dim pTask = Api.FetchSync(Of DTOTask)(exs, "Task", oTask.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOTask)(pTask, oTask, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Log(oTask As DTOTask, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOTask)(oTask, exs, "task/log")
    End Function

    Shared Function LogSync(oTask As DTOTask, exs As List(Of Exception)) As Boolean
        Return Api.ExecuteSync(Of DTOTask)(oTask, exs, "task/log")
    End Function

    Shared Async Function Update(oTask As DTOTask, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOTask)(oTask, exs, "task")
    End Function

    Shared Async Function Delete(oTask As DTOTask, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTask)(oTask, exs, "task")
    End Function


#End Region

    Shared Async Function Execute(oTask As DTOTask, oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "task/execute", oTask.Guid.ToString, oUser.Guid.ToString())
    End Function

End Class

Public Class Tasks
    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOTask))
        Return Await Api.Fetch(Of List(Of DTOTask))(exs, "tasks")
    End Function

    Shared Async Function DueTasks(exs As List(Of Exception)) As Task(Of List(Of DTOTask))
        Return Await Api.Fetch(Of List(Of DTOTask))(exs, "tasks/due")
    End Function

    Shared Async Function ExecuteAsync(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOTask))
        Return Await Api.Fetch(Of List(Of DTOTask))(exs, "tasks/execute", oUser.Guid.ToString())
    End Function

    Shared Function ExecuteSync(oUser As DTOUser, exs As List(Of Exception)) As List(Of DTOTask)
        Return Api.FetchSync(Of List(Of DTOTask))(exs, "tasks/execute", oUser.Guid.ToString())
    End Function
End Class
