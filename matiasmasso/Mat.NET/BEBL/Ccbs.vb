Public Class Ccbs
    Shared Function AllWithContact(oContact As DTOContact, oExercici As DTOExercici, oCta As DTOPgcCta, Optional FchTo As Date = Nothing) As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb) = CcbsLoader.All(oContact, oExercici, oCta, FchTo)
        Return retval
    End Function

    Shared Function All2(oEmp As DTOEmp, oYearMonth As DTOYearMonth) As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb) = CcbsLoader.All(oEmp, oYearMonth)
        Return retval
    End Function

    Shared Function All(oExercici As DTOExercici, oCta As DTOPgcCta) As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb) = CcbsLoader.All(oExercici, oCta)
        Return retval
    End Function

    Shared Function LlibreMajor(oExercici As DTOExercici) As List(Of DTOCcb)
        Dim retval As List(Of DTOCcb) = CcbsLoader.LlibreMajor(oExercici)
        Return retval
    End Function

End Class
