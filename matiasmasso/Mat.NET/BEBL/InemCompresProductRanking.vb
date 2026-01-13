Public Class InemCompresProductRanking

    Shared Function All(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOSkuAmtOrigin)
        Dim retval As List(Of DTOSkuAmtOrigin) = InemCompresProductRankingLoader.All(oEmp, oYearMonth)
        Return retval
    End Function

End Class
