Public Class IvaLiquidacio
    Inherits _FeblBase

    Shared Async Function Factory(exs As List(Of Exception), oExercici As DTOExercici, iMonth As Integer) As Task(Of DTOIVALiquidacio)
        Return Await Api.Fetch(Of DTOIVALiquidacio)(exs, "IvaLiquidacio/factory", oExercici.Emp.Id, oExercici.Year, iMonth)
    End Function


End Class
