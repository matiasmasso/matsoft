Public Class Rol
    Shared Async Function Find(id As Integer, exs As List(Of Exception)) As Task(Of DTORol)
        Return Await Api.Fetch(Of DTORol)(exs, "Rol", id)
    End Function

    Shared Function Load(ByRef oRol As DTORol, exs As List(Of Exception)) As Boolean
        If Not oRol.IsLoaded And Not oRol.IsNew Then
            Dim pRol = Api.FetchSync(Of DTORol)(exs, "Rol", CInt(oRol.Id))
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORol)(pRol, oRol, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oRol As DTORol, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTORol)(oRol, exs, "Rol")
        oRol.IsNew = False
    End Function


    Shared Async Function Delete(oRol As DTORol, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORol)(oRol, exs, "Rol")
    End Function
End Class

Public Class Rols

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTORol))
        Return Await Api.Fetch(Of List(Of DTORol))(exs, "Rols")
    End Function

End Class
