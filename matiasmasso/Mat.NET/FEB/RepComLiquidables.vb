Public Class RepComLiquidable
    Shared Async Function Update(orepcomliquidable As DTORepComLiquidable, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTORepComLiquidable)(orepcomliquidable, exs, "repcomliquidable")
    End Function

    Shared Async Function Descarta(oRepComLiquidable As DTORepComLiquidable, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "repcomliquidable", oRepComLiquidable.Guid.ToString())
    End Function

End Class

Public Class RepComLiquidables

    Shared Async Function PendentsDeLiquidar(exs As List(Of Exception)) As Task(Of List(Of DTORepComLiquidable))
        Return Await Api.Fetch(Of List(Of DTORepComLiquidable))(exs, "repcomliquidables/pendents")
    End Function

    Shared Async Function sincronitza(exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "repcomliquidables/sincronitza")
    End Function
End Class
