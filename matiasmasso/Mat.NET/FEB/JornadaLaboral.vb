Public Class JornadaLaboral
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOJornadaLaboral)
        Return Await Api.Fetch(Of DTOJornadaLaboral)(exs, "JornadaLaboral", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oJornadaLaboral As DTOJornadaLaboral) As Boolean
        If Not oJornadaLaboral.IsLoaded And Not oJornadaLaboral.IsNew Then
            Dim pJornadaLaboral = Api.FetchSync(Of DTOJornadaLaboral)(exs, "JornadaLaboral", oJornadaLaboral.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOJornadaLaboral)(pJornadaLaboral, oJornadaLaboral, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Log(exs As List(Of Exception), oUser As DTOUser) As Task(Of DTOJornadaLaboral.Status)
        Return Await Api.Fetch(Of DTOJornadaLaboral.Status)(exs, "JornadaLaboral/log", oUser.Guid.ToString())
    End Function

    Shared Async Function Log(exs As List(Of Exception), mode As DTOJornadaLaboral.Modes, oUser As DTOUser) As Task(Of String) ' TO DEPRECATE
        Return Await Api.Fetch(Of String)(exs, "JornadaLaboral/log", CInt(mode), oUser.Guid.ToString())
    End Function

    Shared Async Function Update(exs As List(Of Exception), oJornadaLaboral As DTOJornadaLaboral) As Task(Of Boolean)
        Return Await Api.Update(Of DTOJornadaLaboral)(oJornadaLaboral, exs, "JornadaLaboral")
        oJornadaLaboral.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oJornadaLaboral As DTOJornadaLaboral) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOJornadaLaboral)(oJornadaLaboral, exs, "JornadaLaboral")
    End Function
End Class

Public Class JornadesLaborals
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), Optional oStaff As DTOStaff = Nothing) As Task(Of Models.JornadesLaboralsModel)
        If oStaff Is Nothing Then
            Return Await Api.Fetch(Of Models.JornadesLaboralsModel)(exs, "JornadesLaborals")
        Else
            Return Await Api.Fetch(Of Models.JornadesLaboralsModel)(exs, "JornadesLaborals", oStaff.Guid.ToString)
        End If
    End Function

    Shared Async Function FromUser(exs As List(Of Exception), oUser As DTOUser) As Task(Of Models.JornadesLaboralsModel)
        Return Await Api.Fetch(Of Models.JornadesLaboralsModel)(exs, "JornadesLaborals/fromUser", oUser.Guid.ToString)
    End Function

    Shared Async Function RemoveLast(exs As List(Of Exception), oUser As DTOUser) As Task(Of Models.JornadesLaboralsModel)
        Return Await Api.Fetch(Of Models.JornadesLaboralsModel)(exs, "JornadesLaborals/removeLast", oUser.Guid.ToString)
    End Function

    Shared Async Function MissingFchTo(exs As List(Of Exception), oStaff As DTOStaff) As Task(Of List(Of DTOJornadaLaboral))
        Return Await Api.Fetch(Of List(Of DTOJornadaLaboral))(exs, "JornadaLaborals/MissingFchTo", oStaff.Guid.ToString)
    End Function

End Class
