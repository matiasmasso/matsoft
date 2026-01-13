Public Class YearMonths

    Shared Async Function All(fchFrom As Date, fchTo As Date, exs As List(Of Exception)) As Task(Of List(Of DTOYearMonth))
        Return Await Api.Fetch(Of List(Of DTOYearMonth))(exs, "YearMonths", fchFrom.ToString("yyyy-MM-dd"), fchTo.ToString("yyyy-MM-dd"))
    End Function

End Class
