Public Class FourMoms


    Shared Function SalePointsSync(DtFch As Date, exs As List(Of Exception)) As List(Of DTOProductAreaQty)
        Return Api.FetchSync(Of List(Of DTOProductAreaQty))(exs, "FourMoms/SalePoints", DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Function SalesSync(DtFch As Date, exs As List(Of Exception)) As List(Of DTOProductAreaQty)
        Return Api.FetchSync(Of List(Of DTOProductAreaQty))(exs, "FourMoms/Sales", DtFch.ToString("yyyy-MM-dd"))
    End Function

    Shared Function StocksSync(DtFch As Date, exs As List(Of Exception)) As List(Of DTOProductAreaQty)
        Return Api.FetchSync(Of List(Of DTOProductAreaQty))(exs, "FourMoms/Stocks", DtFch.ToString("yyyy-MM-dd"))
    End Function

End Class
