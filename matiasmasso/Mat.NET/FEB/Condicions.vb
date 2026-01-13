Public Class Condicio

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCondicio)
        Return Await Api.Fetch(Of DTOCondicio)(exs, "Condicio", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCondicio As DTOCondicio, exs As List(Of Exception)) As Boolean
        If Not oCondicio.IsLoaded And Not oCondicio.IsNew Then
            Dim pCondicio = Api.FetchSync(Of DTOCondicio)(exs, "Condicio", oCondicio.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCondicio)(pCondicio, oCondicio, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCondicio As DTOCondicio, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCondicio)(oCondicio, exs, "Condicio")
        oCondicio.IsNew = False
    End Function


    Shared Async Function Delete(oCondicio As DTOCondicio, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCondicio)(oCondicio, exs, "Condicio")
    End Function
End Class

Public Class Condicions

    Shared Async Function Headers(exs As List(Of Exception)) As Task(Of DTOCondicio.Collection)
        Return Await Api.Fetch(Of DTOCondicio.Collection)(exs, "Condicions")
    End Function

End Class
