Public Class CompactDiari
    Inherits _FeblBase

    Shared Async Function Items(exs As List(Of Exception), value As DTOSalesQuery) As Task(Of List(Of DTOSalesQuery.Item))
        Return Await Api.Execute(Of DTOSalesQuery, List(Of DTOSalesQuery.Item))(value, exs, "SalesQuery")
    End Function


End Class

