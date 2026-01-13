Public Class JsonLog
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOJsonLog)
        Return Await Api.Fetch(Of DTOJsonLog)(exs, "JsonLog", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oJsonLog As DTOJsonLog) As Boolean
        If Not oJsonLog.IsLoaded And Not oJsonLog.IsNew Then
            Dim pJsonLog = Api.FetchSync(Of DTOJsonLog)(exs, "JsonLog", oJsonLog.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOJsonLog)(pJsonLog, oJsonLog, exs)
                'oJsonLog.Json = pJsonLog.Json
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oJsonLog As DTOJsonLog) As Task(Of Boolean)
        Return Await Api.Update(Of DTOJsonLog)(oJsonLog, exs, "JsonLog")
        oJsonLog.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oJsonLog As DTOJsonLog) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOJsonLog)(oJsonLog, exs, "JsonLog")
    End Function
End Class

Public Class JsonLogs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional searchkey As String = "") As Task(Of List(Of DTOJsonLog))
        Return Await Api.Execute(Of String, List(Of DTOJsonLog))(searchkey, exs, "JsonLogs")
    End Function

End Class