Public Class MailingLogs
    Shared Function All(oSource As DTOBaseGuid) As List(Of DTOMailingLog)
        Dim retval As List(Of DTOMailingLog) = MailingLogsLoader.All(oSource)
        Return retval
    End Function

End Class
