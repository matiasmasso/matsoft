Public Class CurExchangeRate
    Shared Async Function Closest(oCur As DTOCur, Dtfch As Date, exs As List(Of Exception)) As Task(Of DTOCurExchangeRate)
        Return Await Api.Fetch(Of DTOCurExchangeRate)(exs, "curExchangeRates/closest", oCur.Tag, Dtfch.Year, Dtfch.Month, Dtfch.Day)
    End Function
End Class

Public Class CurExchangeRates
    Shared Async Function UpdateRates(exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "curExchangeRates/update")
    End Function

End Class
