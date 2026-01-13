Public Class PgcSaldo
    Inherits _FeblBase
    Shared Async Function FromCtaCod(exs As List(Of Exception), oEmp As DTOEmp, oCtaCod As DTOPgcPlan.Ctas, Optional oContact As DTOContact = Nothing, Optional DtFch As Date = Nothing) As Task(Of Decimal)
        Return Await Api.Fetch(Of Decimal)(exs, "PgcSaldo/FromCtaCod", oEmp.Id, oCtaCod, OpcionalGuid(oContact), FormatFch(DtFch))
    End Function

    Shared Function FromCtaCodSync(exs As List(Of Exception), oEmp As DTOEmp, oCtaCod As DTOPgcPlan.Ctas, Optional oContact As DTOContact = Nothing, Optional DtFch As Date = Nothing) As Decimal
        Return Api.FetchSync(Of Decimal)(exs, "PgcSaldo/FromCtaCod", oEmp.Id, oCtaCod, OpcionalGuid(oContact), FormatFch(DtFch))
    End Function

End Class
Public Class PgcSaldos
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici, Optional HideEmptySaldo As Boolean = False) As Task(Of List(Of DTOPgcSaldo))
        Return Await Api.Fetch(Of List(Of DTOPgcSaldo))(exs, "PgcSaldos", oExercici.Emp.Id, oExercici.Year, If(HideEmptySaldo, 1, 0))
    End Function

End Class
