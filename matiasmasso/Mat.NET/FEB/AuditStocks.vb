Public Class AuditStock
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOAuditStock)
        Return Await Api.Fetch(Of DTOAuditStock)(exs, "AuditStock", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oAuditStock As DTOAuditStock, exs As List(Of Exception)) As Boolean
        If Not oAuditStock.IsLoaded And Not oAuditStock.IsNew Then
            Dim pAuditStock = Api.FetchSync(Of DTOAuditStock)(exs, "AuditStock", oAuditStock.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOAuditStock)(pAuditStock, oAuditStock, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oAuditStock As DTOAuditStock, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOAuditStock)(oAuditStock, exs, "AuditStock")
        oAuditStock.IsNew = False
    End Function


    Shared Async Function Delete(oAuditStock As DTOAuditStock, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOAuditStock)(oAuditStock, exs, "AuditStock")
    End Function
End Class

Public Class AuditStocks
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOAuditStock))
        Return Await Api.Fetch(Of List(Of DTOAuditStock))(exs, "AuditStocks", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function Update(oAuditStocks As List(Of DTOAuditStock), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTOAuditStock))(oAuditStocks, exs, "AuditStocks")
    End Function

End Class
