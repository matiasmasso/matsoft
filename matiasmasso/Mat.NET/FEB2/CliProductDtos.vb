Public Class CliProductDtos
    Shared Async Function All(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of List(Of DTOCliProductDto))
        Return Await Api.Fetch(Of List(Of DTOCliProductDto))(exs, "CliProductDtos", oCustomer.Guid.ToString())
    End Function
    Shared Function AllSync(oCustomer As DTOCustomer, exs As List(Of Exception)) As List(Of DTOCliProductDto)
        Return Api.FetchSync(Of List(Of DTOCliProductDto))(exs, "CliProductDtos", oCustomer.Guid.ToString())
    End Function
    Shared Async Function Update(oCustomer As DTOCustomer, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCustomer)(oCustomer, exs, "CliProductDtos")
    End Function

End Class
