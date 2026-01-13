Public Class PgcClass
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPgcClass)
        Return Await Api.Fetch(Of DTOPgcClass)(exs, "PgcClass", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oPgcClass As DTOPgcClass, exs As List(Of Exception)) As Boolean
        If Not oPgcClass.IsLoaded And Not oPgcClass.IsNew Then
            Dim pPgcClass = Api.FetchSync(Of DTOPgcClass)(exs, "PgcClass", oPgcClass.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPgcClass)(pPgcClass, oPgcClass, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oPgcClass As DTOPgcClass, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOPgcClass)(oPgcClass, exs, "PgcClass")
        oPgcClass.IsNew = False
    End Function


    Shared Async Function Delete(oPgcClass As DTOPgcClass, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOPgcClass)(oPgcClass, exs, "PgcClass")
    End Function
End Class

Public Class PgcClasses
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oPlan As DTOPgcPlan = Nothing) As Task(Of List(Of DTOPgcClass))
        If oPlan Is Nothing Then oPlan = DTOApp.Current.PgcPlan
        Return Await Api.Fetch(Of List(Of DTOPgcClass))(exs, "PgcClasses", oPlan.Guid.ToString())
    End Function
    Shared Async Function Tree(exs As List(Of Exception), oEmp As DTOEmp, FromYear As Integer, Optional oPlan As DTOPgcPlan = Nothing) As Task(Of List(Of DTOPgcClass))
        If oPlan Is Nothing Then oPlan = DTOApp.Current.PgcPlan
        Return Await Api.Fetch(Of List(Of DTOPgcClass))(exs, "PgcClasses/tree", oEmp.Id, FromYear, oPlan.Guid.ToString())
    End Function

End Class
