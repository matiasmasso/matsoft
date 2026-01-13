Public Class InemCompresProductRanking
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oYearMonth As DTOYearMonth) As Task(Of List(Of DTOSkuAmtOrigin))
        Return Await Api.Fetch(Of List(Of DTOSkuAmtOrigin))(exs, "InemCompresProductRanking", oEmp.Id, oYearMonth.Year, oYearMonth.Month)
    End Function

End Class
