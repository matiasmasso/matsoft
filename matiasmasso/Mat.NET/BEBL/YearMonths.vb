Public Class YearMonths
    Shared Function All(ByVal DtFchFrom As Date, DtFchTo As Date) As List(Of DTOYearMonth)
        Dim retval As List(Of DTOYearMonth) = YearMonthsLoader.All(DtFchFrom, DtFchTo)
        Return retval
    End Function

End Class
