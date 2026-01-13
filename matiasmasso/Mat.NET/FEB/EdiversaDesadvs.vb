Public Class EdiversaDesadv
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOEdiversaDesadv)
        Return Await Api.Fetch(Of DTOEdiversaDesadv)(exs, "EdiversaDesadv", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oEdiversaDesadv As DTOEdiversaDesadv) As Boolean
        If Not oEdiversaDesadv.IsLoaded And Not oEdiversaDesadv.IsNew Then
            Dim pEdiversaDesadv = Api.FetchSync(Of DTOEdiversaDesadv)(exs, "EdiversaDesadv", oEdiversaDesadv.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEdiversaDesadv)(pEdiversaDesadv, oEdiversaDesadv, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oEdiversaDesadv As DTOEdiversaDesadv) As Task(Of Boolean)
        Return Await Api.Update(Of DTOEdiversaDesadv)(oEdiversaDesadv, exs, "EdiversaDesadv")
        oEdiversaDesadv.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oEdiversaDesadv As DTOEdiversaDesadv) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiversaDesadv)(oEdiversaDesadv, exs, "EdiversaDesadv")
    End Function
End Class

Public Class EdiversaDesadvs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaDesadv))
        Dim retval = Await Api.Fetch(Of List(Of DTOEdiversaDesadv))(exs, "EdiversaDesadvs")
        Return retval
    End Function

End Class
