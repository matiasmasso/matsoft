Public Class CustomerRanking
    Shared Function Load(oQuery As DTOCustomerRanking) As Boolean
        Dim retval As Boolean = CustomerRankingLoader.Load(oQuery)
        Return retval
    End Function
End Class
