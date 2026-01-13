Public Class Projecte
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOProjecte)
        Return Await Api.Fetch(Of DTOProjecte)(exs, "Projecte", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oProjecte As DTOProjecte) As Boolean
        If Not oProjecte.IsLoaded And Not oProjecte.IsNew Then
            Dim pProjecte = Api.FetchSync(Of DTOProjecte)(exs, "Projecte", oProjecte.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOProjecte)(pProjecte, oProjecte, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oProjecte As DTOProjecte) As Task(Of Boolean)
        Return Await Api.Update(Of DTOProjecte)(oProjecte, exs, "Projecte")
        oProjecte.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oProjecte As DTOProjecte) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOProjecte)(oProjecte, exs, "Projecte")
    End Function

    Shared Async Function Items(exs As List(Of Exception), oProjecte As DTOProjecte) As Task(Of List(Of DTOCcb))
        Return Await Api.Fetch(Of List(Of DTOCcb))(exs, "Projecte/Items", oProjecte.Guid.ToString())
    End Function

End Class

Public Class Projectes
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOProjecte))
        Return Await Api.Fetch(Of List(Of DTOProjecte))(exs, "Projectes")
    End Function

End Class
