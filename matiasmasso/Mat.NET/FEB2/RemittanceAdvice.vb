Public Class RemittanceAdvice

    Shared Async Function FromCca(exs As List(Of Exception), oCca As DTOCca) As Task(Of DTORemittanceAdvice)
        Return Await Api.Fetch(Of DTORemittanceAdvice)(exs, "RemittanceAdvice/FromCca", oCca.Guid.ToString())
    End Function



End Class
