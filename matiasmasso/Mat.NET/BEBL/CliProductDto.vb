Public Class CliProductDtos
    Shared Function All(ByRef oCustomer As DTOCustomer) As List(Of DTOCliProductDto)
        Dim retval As List(Of DTOCliProductDto) = CliProductDtosLoader.All(oCustomer)
        Return retval
    End Function

    Shared Function Update(oCustomer As DTOCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CliProductDtosLoader.Update(oCustomer, exs)
        Return retval
    End Function

End Class
