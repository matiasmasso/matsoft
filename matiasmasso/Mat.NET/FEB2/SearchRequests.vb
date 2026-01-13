Public Class SearchRequest

    Shared Async Function Load(value As DTOSearchRequest, exs As List(Of Exception)) As Task(Of DTOSearchRequest)
        Dim retval As DTOSearchRequest = Await Api.Execute(Of DTOSearchRequest, DTOSearchRequest)(value, exs, "SearchRequest")
        Return retval
    End Function

End Class

Public Class SearchRequests

    Shared Async Function All(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOSearchRequest))
        Return Await Api.Fetch(Of List(Of DTOSearchRequest))(exs, "SearchRequests", oEmp.Id)
    End Function

End Class

