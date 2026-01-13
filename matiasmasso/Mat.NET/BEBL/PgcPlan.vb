Public Class PgcPlan

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOPgcPlan
        Dim retval As DTOPgcPlan = PgcPlanLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPgcPlan As DTOPgcPlan) As Boolean
        Dim retval As Boolean = PgcPlanLoader.Load(oPgcPlan)
        Return retval
    End Function

    Shared Function Update(oPgcPlan As DTOPgcPlan, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PgcPlanLoader.Update(oPgcPlan, exs)
        Return retval
    End Function

    Shared Function Delete(oPgcPlan As DTOPgcPlan, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PgcPlanLoader.Delete(oPgcPlan, exs)
        Return retval
    End Function
#End Region


    Shared Function Current() As DTOPgcPlan
        Dim retval As DTOPgcPlan = FromYear(DTO.GlobalVariables.Today().Year)
        Return retval
    End Function

    Shared Function FromYear(iYear As Integer) As DTOPgcPlan
        Dim retval As DTOPgcPlan = PgcPlanLoader.FromYear(iYear)
        Return retval
    End Function
End Class

Public Class PgcPlans
    Shared Function All() As List(Of DTOPgcPlan)
        Dim retval As List(Of DTOPgcPlan) = PgcPlansLoader.All
        Return retval
    End Function
End Class
