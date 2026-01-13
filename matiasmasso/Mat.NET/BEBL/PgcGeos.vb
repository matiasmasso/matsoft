Public Class PgcGeos

    Shared Function FromExercici(oExercici As DTOExercici) As List(Of DTOPgcGeo)
        Dim retval As List(Of DTOPgcGeo) = PgcGeosLoader.FromExercici(oExercici)
        Return retval
    End Function


End Class
