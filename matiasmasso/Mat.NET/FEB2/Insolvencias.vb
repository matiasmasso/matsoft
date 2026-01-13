Public Class Insolvencia
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOInsolvencia)
        Return Await Api.Fetch(Of DTOInsolvencia)(exs, "Insolvencia", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oInsolvencia As DTOInsolvencia, exs As List(Of Exception)) As Boolean
        If Not oInsolvencia.IsLoaded And Not oInsolvencia.IsNew Then
            Dim pInsolvencia = Api.FetchSync(Of DTOInsolvencia)(exs, "Insolvencia", oInsolvencia.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOInsolvencia)(pInsolvencia, oInsolvencia, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oInsolvencia As DTOInsolvencia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOInsolvencia)(oInsolvencia, exs, "Insolvencia")
        oInsolvencia.IsNew = False
    End Function


    Shared Async Function Delete(oInsolvencia As DTOInsolvencia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOInsolvencia)(oInsolvencia, exs, "Insolvencia")
    End Function

    Shared Async Function IsInsolvent(exs As List(Of Exception), oContact As DTOContact) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Insolvencia/IsInsolvent", oContact.Guid.ToString())
    End Function
    Shared Function IsInsolventSync(exs As List(Of Exception), oContact As DTOContact) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "Insolvencia/IsInsolvent", oContact.Guid.ToString())
    End Function

End Class

Public Class Insolvencias
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOInsolvencia))
        Return Await Api.Fetch(Of List(Of DTOInsolvencia))(exs, "Insolvencias")
    End Function

End Class
