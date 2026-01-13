Public Class SumasYSaldos

    Shared Function All(oEmp As DTOEmp, DtFch As Date) As DTOSumasYSaldos
        Dim retval As DTOSumasYSaldos = SumasYSaldosLoader.All(oEmp, DtFch)
        Return retval
    End Function

    Shared Function All(ByVal oExercici As DTOExercici,
                        Optional HideEmptySaldo As Boolean = False,
                        Optional oRange As DTO.Defaults.ContactRange = DTO.Defaults.ContactRange.AllContacts,
                        Optional oContact As DTOContact = Nothing) As List(Of DTOPgcSaldo)

        Dim retval As List(Of DTOPgcSaldo) = PgcSaldosLoader.All(oExercici, HideEmptySaldo, oRange, oContact)
        Return retval
    End Function

    Shared Function Summary(oEmp As DTOEmp, DtFch As Date) As DTOSumasYSaldos
        Dim retval As DTOSumasYSaldos = SumasYSaldosLoader.Summary(oEmp, DtFch)
        Return retval
    End Function


    Shared Function Summary(ByVal oExercici As DTOExercici, Optional DtFch As Date = Nothing) As List(Of DTOPgcSaldo)
        Dim retval As List(Of DTOPgcSaldo) = PgcSaldosLoader.Summary(oExercici, DtFch)
        Return retval
    End Function

    Shared Function SubComptes(oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOPgcSaldo)
        Dim retval As List(Of DTOPgcSaldo) = PgcSaldosLoader.SubComptes(oExercici, oCta)
        Return retval
    End Function

    Shared Function Years(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing) As List(Of Integer)
        Dim retval As List(Of Integer) = PgcSaldosLoader.Years(oEmp, oContact, oCta)
        Return retval
    End Function

End Class
