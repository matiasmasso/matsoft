Public Class PrvCliNum

    Shared Async Function Customer(oProveidor As DTOProveidor, clinum As String, exs As List(Of Exception)) As Task(Of DTOCustomer)
        Return Await Api.Fetch(Of DTOCustomer)(exs, "prvclinums", oProveidor.Guid.ToString, clinum)
    End Function

End Class

Public Class PrvCliNums

    Shared Async Function All(oProveidor As DTOProveidor, exs As List(Of Exception)) As Task(Of List(Of DTOPrvCliNum))
        Return Await Api.Fetch(Of List(Of DTOPrvCliNum))(exs, "prvclinums", oProveidor.Guid.ToString())
    End Function

    Shared Async Function Update(oProveidor As DTOProveidor, oPrvCliNums As List(Of DTOPrvCliNum), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTOPrvCliNum))(oPrvCliNums, exs, "prvclinums", oProveidor.Guid.ToString())
    End Function

End Class
