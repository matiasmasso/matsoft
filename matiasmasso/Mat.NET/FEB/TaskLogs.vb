Public Class TaskLog


#Region "CRUD"
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOTaskLog)
        Return Await Api.Fetch(Of DTOTaskLog)(exs, "tasklog", oGuid.ToString())
    End Function

    Shared Async Function Update(oTask As DTOTask, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval = Await Api.Update(Of DTOTask)(oTask, exs, "tasklog")
        Return retval
    End Function

    Shared Function UpdateSync(oTask As DTOTask, exs As List(Of Exception)) As Boolean
        Dim retval = Api.UpdateSync(Of DTOTask)(oTask, exs, "tasklog")
        Return retval
    End Function

    Shared Async Function Delete(oTaskLog As DTOTaskLog, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOTaskLog)(oTaskLog, exs, "tasklog")
    End Function

#End Region

End Class

Public Class TaskLogs

    Shared Async Function All(oTask As DTOTask, exs As List(Of Exception)) As Task(Of List(Of DTOTaskLog))
        Return Await Api.Fetch(Of List(Of DTOTaskLog))(exs, "tasklogs", oTask.Guid.ToString())
    End Function

    Shared Async Function Delete(oTaskLogs As List(Of DTOTaskLog), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOTaskLog))(oTaskLogs, exs, "tasklogs")
    End Function

End Class


