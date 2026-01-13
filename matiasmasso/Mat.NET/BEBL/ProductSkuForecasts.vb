Public Class ProductSkuForecasts

    Shared Function All(oMgz As DTOMgz, Optional oProveidor As DTOProveidor = Nothing, Optional oProduct As DTOProduct = Nothing, Optional FromNowOn As Boolean = False) As DTOProductSkuForecast.Collection
        Dim retval = ProductSkuForecastLoader.All(oMgz, oProveidor, oProduct, FromNowOn)
        Return retval
    End Function

    Shared Function Insert(oSkus As DTOProductSkuForecast.Collection, ByRef exs As List(Of Exception)) As Boolean
        Dim retval = ProductSkuForecastLoader.Insert(oSkus, exs)
        Return retval
    End Function

End Class
