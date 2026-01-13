Public Class Emails
    Shared Async Function All(exs As List(Of Exception), oContact As DTOContact, Optional includeObsolets As Boolean = False) As Task(Of List(Of DTOEmail))
        Return Await Api.Fetch(Of List(Of DTOEmail))(exs, "Emails/fromContact", oContact.Guid.ToString, If(includeObsolets, 1, 0))
    End Function

End Class
