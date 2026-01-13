Public Class Forecasts

    Shared Async Function Insert(ByVal values As DTOProductSkuForecast.Collection, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOProductSkuForecast.Collection)(values, exs, "forecasts/insert")
    End Function

    Shared Async Function All(oMgz As DTOMgz, exs As List(Of Exception), Optional oProveidor As DTOProveidor = Nothing, Optional oProduct As DTOProduct = Nothing) As Task(Of DTOProductSkuForecast.Collection)
        If oProduct Is Nothing Then
            Return Await Api.Fetch(Of DTOProductSkuForecast.Collection)(exs, "forecast/FromProveidor", oMgz.Guid.ToString, oProveidor.Guid.ToString())
        Else
            Return Await Api.Fetch(Of DTOProductSkuForecast.Collection)(exs, "forecast/FromProduct", oMgz.Guid.ToString, oProduct.Guid.ToString())
        End If
    End Function

    Shared Async Function Excel(oUser As DTOUser, oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)
        Return Await Api.Fetch(Of ExcelHelper.Sheet)(exs, "forecast/Excel", oUser.Guid.ToString, oMgz.Guid.ToString())
    End Function

End Class
