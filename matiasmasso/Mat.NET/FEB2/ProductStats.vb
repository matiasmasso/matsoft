Public Class ProductStats
    Shared Async Function All(oCategory As DTOProductCategory, exs As List(Of Exception)) As Task(Of List(Of DTOProductStat))
        Return Await Api.Fetch(Of List(Of DTOProductStat))(exs, "ProductStats", oCategory.Guid.ToString())
    End Function

End Class
