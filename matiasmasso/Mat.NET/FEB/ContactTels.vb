
Public Class ContactTels

    Shared Async Function All(oContact As DTOContact, cod As DTOContactTel.Cods, exs As List(Of Exception)) As Task(Of List(Of DTOContactTel))
        Return Await Api.Fetch(Of List(Of DTOContactTel))(exs, "ContactTels", oContact.Guid.ToString, cod)
    End Function

    Shared Function AllSync(oContact As DTOContact, exs As List(Of Exception)) As List(Of DTOContactTel)
        Return Api.FetchSync(Of List(Of DTOContactTel))(exs, "ContactTels", oContact.Guid.ToString())
    End Function

End Class

