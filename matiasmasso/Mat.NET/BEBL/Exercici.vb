Public Class Exercici

    Shared Function Find(oGuid As Guid) As DTOExercici
        Dim retval As DTOExercici = ExerciciLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Saldos(oExercici As DTOExercici, Optional SkipTancament As Boolean = True) As List(Of DTOPgcSaldo)
        Return ExerciciLoader.Saldos(oExercici, SkipTancament)
    End Function

    Shared Function RetrocedeixAssentamentsApertura(exs As List(Of Exception), oExercici As DTOExercici) As Boolean
        Return ExerciciLoader.RetrocedeixAssentamentsApertura(exs, oExercici)
    End Function

    Shared Function RenumeraAssentaments(exs As List(Of Exception), oExercici As DTOExercici) As Integer
        Return ExerciciLoader.RenumeraAssentaments(exs, oExercici)
    End Function

    Shared Function EliminaTancaments(exs As List(Of Exception), ByVal oExercici As DTOExercici) As Boolean
        Return ExerciciLoader.EliminaTancaments(oExercici, exs)
    End Function

End Class

Public Class Exercicis

    Shared Function All(oEmp As DTOEmp, Optional oContact As DTOContact = Nothing, Optional oCta As DTOPgcCta = Nothing) As List(Of DTOExercici)
        Return ExercicisLoader.All(oEmp, oContact, oCta)
    End Function


    Shared Function Years(oEmp As DTOEmp) As List(Of Integer)
        Dim retval As List(Of Integer) = ExercicisLoader.Years(oEmp)
        Return retval
    End Function

End Class
