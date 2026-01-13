Public Class YearMonthsLoader
    Shared Function All(ByVal DtFchFrom As Date, DtFchTo As Date) As List(Of DTOYearMonth)
        Dim retval As New List(Of DTOYearMonth)
        For iMes As Integer = 0 To 11
            Dim DtFch As Date = DtFchFrom.AddMonths(iMes)
            Dim item As New DTOYearMonth(DtFch.Year, DtFch.Month)
            retval.Add(item)
        Next
        Return retval
    End Function

End Class
