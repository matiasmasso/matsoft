Public Class CondicioCapitol
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCondicio.Capitol)
        Return Await Api.Fetch(Of DTOCondicio.Capitol)(exs, "CondicioCapitol", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCondicioCapitol As DTOCondicio.Capitol, exs As List(Of Exception)) As Boolean
        If Not oCondicioCapitol.IsLoaded And Not oCondicioCapitol.IsNew Then
            Dim pCondicioCapitol = Api.FetchSync(Of DTOCondicio.Capitol)(exs, "CondicioCapitol", oCondicioCapitol.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCondicio.Capitol)(pCondicioCapitol, oCondicioCapitol, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCondicioCapitol As DTOCondicio.Capitol, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCondicio.Capitol)(oCondicioCapitol, exs, "CondicioCapitol")
        oCondicioCapitol.IsNew = False
    End Function


    Shared Async Function Delete(oCondicioCapitol As DTOCondicio.Capitol, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCondicio.Capitol)(oCondicioCapitol, exs, "CondicioCapitol")
    End Function
End Class

Public Class CondicioCapitols

    Shared Async Function Headers(oCondicio As DTOCondicio, exs As List(Of Exception)) As Task(Of DTOCondicio.Capitol.Collection)
        Return Await Api.Fetch(Of DTOCondicio.Capitol.Collection)(exs, "CondicioCapitols", oCondicio.Guid.ToString())
    End Function

End Class
