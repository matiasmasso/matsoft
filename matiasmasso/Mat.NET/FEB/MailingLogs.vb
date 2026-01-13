Public Class MailingLogs

    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oSource As DTOBaseGuid) As Task(Of List(Of DTOMailingLog))
        Return Await Api.Fetch(Of List(Of DTOMailingLog))(exs, "MailingLogs", oSource.Guid.ToString())
    End Function

End Class

