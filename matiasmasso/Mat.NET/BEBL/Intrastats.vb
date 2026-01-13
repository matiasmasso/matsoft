Public Class Intrastat

    Shared Function Find(oGuid As Guid) As DTOIntrastat
        Dim retval As DTOIntrastat = IntrastatLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oIntrastat As DTOIntrastat, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IntrastatLoader.Update(oIntrastat, exs)
        Return retval
    End Function

    Shared Function Delete(oIntrastat As DTOIntrastat, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = IntrastatLoader.Delete(oIntrastat, exs)
        Return retval
    End Function

    Shared Function Factory(oEmp As DTOEmp, oFlujo As DTOIntrastat.Flujos, oYearMonth As DTOYearMonth) As DTOIntrastat
        Dim retval As New DTOIntrastat
        With retval
            .Emp = oEmp
            .Flujo = oFlujo
            .Yea = oYearMonth.Year
            .Mes = oYearMonth.Month
            .Partidas = New List(Of DTOIntrastat.Partida)
            .ExceptionSkus = New List(Of DTOProductSku)
            .Ord = IntrastatLoader.NextOrd(.Emp, .Flujo, oYearMonth)
        End With

        Return retval
    End Function

End Class


Public Class Intrastats
    Shared Function All(oEmp As DTOEmp) As List(Of DTOIntrastat)
        Dim retval As List(Of DTOIntrastat) = IntrastatsLoader.All(oEmp)
        Return retval
    End Function
End Class
