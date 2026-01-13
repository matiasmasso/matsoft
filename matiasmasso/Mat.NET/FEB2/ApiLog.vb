Public Class ApiLog

    Shared Async Function Log(oCod As DTOApiLog.Cods, Ip As String, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oApiLog = DTOApiLog.Factory(oCod, Ip)
        Return Await Api.Execute(Of DTOApiLog)(oApiLog, exs, "ApiLog/Log")
    End Function

End Class
