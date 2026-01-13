Public Class Balance

    Shared Function Tree(oEmp As DTOEmp, yearmonthFrom As DTOYearMonth, YearmonthTo As DTOYearMonth) As DTOBalance
        Return BalanceLoader.Tree(oEmp, yearmonthFrom, YearmonthTo)
    End Function

    Shared Function SumasYSaldos(oExercici As DTOExercici, Optional DtFch As Date = Nothing) As List(Of DTOBalanceSaldo)
        Dim retval As List(Of DTOBalanceSaldo) = BalanceSaldosLoader.SumasYSaldos(oExercici, DtFch)
        Return retval
    End Function

    Shared Function Cce(oEmp As DTOEmp, oCta As DTOPgcCta, DtFch As Date) As List(Of DTOBalanceSaldo)
        Dim oExercici As DTOExercici = DTOExercici.FromYear(oEmp, DtFch.Year)
        Dim retval As List(Of DTOBalanceSaldo) = BalanceSaldosLoader.All(oExercici, oCta, DtFch, Nothing)
        Return retval
    End Function
End Class
