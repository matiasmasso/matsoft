Public Class PgcCta
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPgcCta)
        Return Await Api.Fetch(Of DTOPgcCta)(exs, "PgcCta", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oPgcCta As DTOPgcCta, exs As List(Of Exception)) As Boolean
        If Not oPgcCta.IsLoaded And Not oPgcCta.IsNew Then
            Dim pPgcCta = Api.FetchSync(Of DTOPgcCta)(exs, "PgcCta", oPgcCta.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPgcCta)(pPgcCta, oPgcCta, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oPgcCta As DTOPgcCta, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPgcCta)(oPgcCta, exs, "PgcCta")
        oPgcCta.IsNew = False
    End Function

    Shared Async Function Delete(oPgcCta As DTOPgcCta, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPgcCta)(oPgcCta, exs, "PgcCta")
    End Function

    Shared Async Function Saldo(exs As List(Of Exception), oEmp As DTOEmp, oCta As DTOPgcCta, Optional oContact As DTOContact = Nothing, Optional ByVal DtFch As Date = Nothing) As Task(Of DTOAmt)
        If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
        Return Await Api.Fetch(Of DTOAmt)(exs, "PgcCta/Saldo", oEmp.Id, oCta.Guid.ToString, OpcionalGuid(oContact), FormatFch(DtFch))
    End Function

    Shared Async Function FromCod(oCod As DTOPgcPlan.Ctas, oExercici As DTOExercici, exs As List(Of Exception)) As Task(Of DTOPgcCta)
        Return Await Api.Fetch(Of DTOPgcCta)(exs, "PgcCta/FromCod", oCod, oExercici.Emp.Id, oExercici.Year)
    End Function
    Shared Async Function FromCod(oCod As DTOPgcPlan.Ctas, oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of DTOPgcCta)
        Dim oExercici = DTOExercici.Current(oEmp)
        Return Await Api.Fetch(Of DTOPgcCta)(exs, "PgcCta/FromCod", oCod, oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Function FromCodSync(oCod As DTOPgcPlan.Ctas, oExercici As DTOExercici, exs As List(Of Exception)) As DTOPgcCta
        Return Api.FetchSync(Of DTOPgcCta)(exs, "PgcCta/FromCod", oCod, oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Function FromCodSync(oCod As DTOPgcPlan.Ctas, oEmp As DTOEmp, exs As List(Of Exception)) As DTOPgcCta
        Dim oExercici = DTOExercici.Current(oEmp)
        Return FromCodSync(oCod, oExercici, exs)
    End Function

    Shared Async Function FromId(oPlan As DTOPgcPlan, id As String, exs As List(Of Exception)) As Task(Of DTOPgcCta)
        Return Await Api.Fetch(Of DTOPgcCta)(exs, "PgcCta/FromId", OpcionalGuid(oPlan), id)
    End Function


End Class


Public Class PgcCtas
    Inherits _FeblBase

    Shared Async Function SearchAsync(oPlan As DTOPgcPlan, searchKey As String, exs As List(Of Exception)) As Task(Of List(Of DTOPgcCta))
        Return Await Api.Fetch(Of List(Of DTOPgcCta))(exs, "pgcctas/search", oPlan.Guid.ToString, searchKey)
    End Function

    Shared Function SearchSync(oPlan As DTOPgcPlan, searchKey As String, exs As List(Of Exception)) As List(Of DTOPgcCta)
        Return Api.FetchSync(Of List(Of DTOPgcCta))(exs, "pgcctas/search", oPlan.Guid.ToString, searchKey)
    End Function

    Shared Async Function All(exs As List(Of Exception), Optional year As Integer = 0) As Task(Of List(Of DTOPgcCta))
        If year = 0 Then year = DTO.GlobalVariables.Today().Year
        Return Await Api.Fetch(Of List(Of DTOPgcCta))(exs, "pgcctas", year)
    End Function
    Shared Async Function All(exs As List(Of Exception), oPlan As DTOPgcPlan, Optional oClass As DTOPgcClass = Nothing) As Task(Of List(Of DTOPgcCta))
        Return Await Api.Fetch(Of List(Of DTOPgcCta))(exs, "pgcctas", oPlan.Guid.ToString, OpcionalGuid(oClass))
    End Function

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici, Optional oContact As DTOContact = Nothing) As Task(Of List(Of DTOPgcCta))
        Dim oContactGuid As Guid = Nothing
        If oContact IsNot Nothing Then oContactGuid = oContact.Guid
        Dim retval = Await Api.Fetch(Of List(Of DTOPgcCta))(exs, "pgcctas", oExercici.Emp.Id, oExercici.Year, OpcionalGuid(oContact))
        Dim oDietas = retval.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Dietas)
        Return retval
    End Function

End Class
