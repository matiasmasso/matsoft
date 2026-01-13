Public Class EdiversaExceptions
    Shared Async Function All(oParent As DTOBaseGuid, exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaException))
        Return Await Api.Fetch(Of List(Of DTOEdiversaException))(exs, "EdiversaExceptions", oParent.Guid.ToString())
    End Function

End Class
