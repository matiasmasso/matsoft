Public Class Credencial


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCredencial)
        Return Await Api.Fetch(Of DTOCredencial)(exs, "Credencial", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCredencial As DTOCredencial, exs As List(Of Exception)) As Boolean
        If Not oCredencial.IsLoaded And Not oCredencial.IsNew Then
            Dim pCredencial = Api.FetchSync(Of DTOCredencial)(exs, "Credencial", oCredencial.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCredencial)(pCredencial, oCredencial, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCredencial As DTOCredencial, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCredencial)(oCredencial, exs, "Credencial2") 'doncs el POST a credencial ja el te l'iMat per el Find
        oCredencial.IsNew = False
    End Function

    Shared Async Function Delete(oCredencial As DTOCredencial, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCredencial)(oCredencial, exs, "Credencial")
    End Function
End Class

Public Class Credencials

    Shared Async Function All(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOCredencial))
        Return Await Api.Fetch(Of List(Of DTOCredencial))(exs, "Credencials", oUser.Guid.ToString())
    End Function

    Shared Async Function Owners(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of List(Of DTOUser))
        Return Await Api.Fetch(Of List(Of DTOUser))(exs, "Credencials/owners", CInt(oEmp.Id))
    End Function

End Class
