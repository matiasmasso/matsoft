Public Class PgcSaldo

    Shared Function FromCtaCod(oCtaCod As DTOPgcPlan.Ctas, oContact As DTOContact, DtFch As Date) As Decimal
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim retval As Decimal = PgcSaldoLoader.FromCtaCod(oCtaCod, oContact, DtFch)
        Return retval
    End Function

    Shared Function FromCtaCod(oEmp As DTOEmp, oCtaCod As DTOPgcPlan.Ctas, DtFch As Date) As Decimal
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Dim retval As Decimal = PgcSaldoLoader.FromCtaCod(oCtaCod, Nothing, DtFch, oEmp)
        Return retval
    End Function

End Class

Public Class PgcSaldos
    Shared Function All(oExercici As DTOExercici, Optional HideEmptySaldo As Boolean = False) As List(Of DTOPgcSaldo)
        Dim retval As List(Of DTOPgcSaldo) = PgcSaldosLoader.All(oExercici, HideEmptySaldo)
        Return retval
    End Function

End Class
