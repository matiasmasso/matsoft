Public Class Sii

    Shared Async Function SendEmeses(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of DTOTaskResult)
        Return Await Api.Fetch(Of DTOTaskResult)(exs, "sii/sendEmeses", CInt(oEmp.Id))
    End Function

    Shared Async Function SendRebudes(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of DTOTaskResult)
        Return Await Api.Fetch(Of DTOTaskResult)(exs, "sii/sendRebudes", CInt(oEmp.Id))
    End Function

End Class
