Public Class PgcPlan
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPgcPlan)
        Return Await Api.Fetch(Of DTOPgcPlan)(exs, "PgcPlan", oGuid.ToString())
    End Function

    Shared Async Function FromYear(year As Integer, exs As List(Of Exception)) As Task(Of DTOPgcPlan)
        Return Await Api.Fetch(Of DTOPgcPlan)(exs, "PgcPlan/FromYear", year)
    End Function


    Shared Function FromYearSync(year As Integer, exs As List(Of Exception)) As DTOPgcPlan
        Return Api.FetchSync(Of DTOPgcPlan)(exs, "PgcPlan/FromYear", year)
    End Function

    Shared Function Load(ByRef oPgcPlan As DTOPgcPlan, exs As List(Of Exception)) As Boolean
        If Not oPgcPlan.IsLoaded And Not oPgcPlan.IsNew Then
            Dim pPgcPlan = Api.FetchSync(Of DTOPgcPlan)(exs, "PgcPlan", oPgcPlan.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPgcPlan)(pPgcPlan, oPgcPlan, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oPgcPlan As DTOPgcPlan, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPgcPlan)(oPgcPlan, exs, "PgcPlan")
        oPgcPlan.IsNew = False
    End Function

    Shared Async Function Delete(oPgcPlan As DTOPgcPlan, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPgcPlan)(oPgcPlan, exs, "PgcPlan")
    End Function
End Class

Public Class PgcPlans
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOPgcPlan))
        Return Await Api.Fetch(Of List(Of DTOPgcPlan))(exs, "PgcPlans")
    End Function

End Class

