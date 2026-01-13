Public Class TpvLog

    Shared Function Find(oGuid As Guid) As DTOTpvLog
        Dim retval As DTOTpvLog = TpvLogLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromOrder(sDs_Order As String) As DTOTpvLog
        Dim retval As DTOTpvLog = TpvLogLoader.FromOrder(sDs_Order)
        Return retval
    End Function

    Shared Function Update(oTpvLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TpvLogLoader.Update(oTpvLog, exs)
        Return retval
    End Function

    Shared Function Delete(oTpvLog As DTOTpvLog, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = TpvLogLoader.Delete(oTpvLog, exs)
        Return retval
    End Function

    Shared Function BookRequest(oTpvLog As DTOTpvLog, ByRef exs As List(Of Exception)) As Boolean
        Return TpvLogLoader.BookRequest(oTpvLog, exs)
    End Function
End Class



Public Class TpvLogs
    Shared Function All() As List(Of DTOTpvLog)
        Dim retval As List(Of DTOTpvLog) = TpvLogsLoader.All()
        Return retval
    End Function
End Class
