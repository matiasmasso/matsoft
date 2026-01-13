Public Class Statement
    Inherits _FeblBase

    Shared Async Function Years(exs As List(Of Exception), oContact As DTOContact) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "Statement/years/fromContact", oContact.Guid.ToString())
    End Function

    Shared Async Function Fetch(exs As List(Of Exception), oContact As DTOContact, year As Integer) As Task(Of DTOStatement)
        Dim retval = Await Api.Fetch(Of DTOStatement)(exs, "Statement/fromContact", oContact.Guid.ToString(), year.ToString())
        Return retval
    End Function

End Class
