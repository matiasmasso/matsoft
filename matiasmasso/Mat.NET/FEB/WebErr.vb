Public Class WebErr
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWebErr)
        Return Await Api.Fetch(Of DTOWebErr)(exs, "WebErr", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWebErr As DTOWebErr) As Boolean
        If Not oWebErr.IsLoaded And Not oWebErr.IsNew Then
            Dim pWebErr = Api.FetchSync(Of DTOWebErr)(exs, "WebErr", oWebErr.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWebErr)(pWebErr, oWebErr, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oWebErr As DTOWebErr) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWebErr)(oWebErr, exs, "WebErr")
        oWebErr.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oWebErr As DTOWebErr) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWebErr)(oWebErr, exs, "WebErr")
    End Function
End Class

Public Class WebErrs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOWebErr))
        Return Await Api.Fetch(Of List(Of DTOWebErr))(exs, "WebErrs")
    End Function

    Shared Async Function Reset(exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "WebErrs/reset")
    End Function

End Class

