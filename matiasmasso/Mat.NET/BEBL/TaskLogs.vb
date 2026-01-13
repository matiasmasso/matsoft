Public Class TaskLog


#Region "CRUD"

    Shared Function Find(oGuid As Guid) As DTOTaskLog
        Dim retval = TaskLogLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oTask As DTOTask, exs As List(Of Exception)) As Boolean
        Return TaskLogLoader.Update(oTask, exs)
    End Function

    Shared Function Delete(oTask As DTOTaskLog, exs As List(Of Exception)) As Boolean
        Return TaskLogLoader.Delete(oTask, exs)
    End Function


#End Region

End Class

Public Class TaskLogs

    Shared Function All(oTask As DTOTask) As List(Of DTOTaskLog)
        Dim retval As List(Of DTOTaskLog) = TaskLogsLoader.All(oTask)
        Return retval
    End Function

    Shared Function Delete(oTaskLogs As List(Of DTOTaskLog), exs As List(Of Exception)) As Boolean
        Return TaskLogsLoader.Delete(oTaskLogs, exs)
    End Function

End Class

