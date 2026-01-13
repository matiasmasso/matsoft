Public Class IncidenciaCod

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOIncidenciaCod)
        Return Await Api.Fetch(Of DTOIncidenciaCod)(exs, "IncidenciaCod", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oIncidenciaCod As DTOIncidenciaCod, exs As List(Of Exception)) As Boolean
        If Not oIncidenciaCod.IsLoaded And Not oIncidenciaCod.IsNew Then
            Dim pIncidenciaCod = Api.FetchSync(Of DTOIncidenciaCod)(exs, "IncidenciaCod", oIncidenciaCod.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOIncidenciaCod)(pIncidenciaCod, oIncidenciaCod, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oIncidenciaCod As DTOIncidenciaCod, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOIncidenciaCod)(oIncidenciaCod, exs, "IncidenciaCod")
        oIncidenciaCod.IsNew = False
    End Function

    Shared Async Function Delete(oIncidenciaCod As DTOIncidenciaCod, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOIncidenciaCod)(oIncidenciaCod, exs, "IncidenciaCod")
    End Function

End Class

Public Class IncidenciaCods

    Shared Async Function All(cod As DTOIncidenciaCod.cods, exs As List(Of Exception)) As Task(Of List(Of DTOIncidenciaCod))
        Return Await Api.Fetch(Of List(Of DTOIncidenciaCod))(exs, "IncidenciaCods", cod)
    End Function

End Class

