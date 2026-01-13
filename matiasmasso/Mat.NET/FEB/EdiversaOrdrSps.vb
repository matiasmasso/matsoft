Public Class EdiversaOrdrSp
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOEdiversaOrdrsp)
        Return Await Api.Fetch(Of DTOEdiversaOrdrsp)(exs, "EdiversaOrdrSp", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oEdiversaOrdrSp As DTOEdiversaOrdrsp) As Boolean
        If Not oEdiversaOrdrSp.IsLoaded And Not oEdiversaOrdrSp.IsNew Then
            Dim pEdiversaOrdrSp = Api.FetchSync(Of DTOEdiversaOrdrsp)(exs, "EdiversaOrdrSp", oEdiversaOrdrSp.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOEdiversaOrdrsp)(pEdiversaOrdrSp, oEdiversaOrdrSp, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oEdiversaOrdrSp As DTOEdiversaOrdrsp) As Task(Of Boolean)
        Return Await Api.Update(Of DTOEdiversaOrdrsp)(oEdiversaOrdrSp, exs, "EdiversaOrdrSp")
        oEdiversaOrdrSp.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oEdiversaOrdrSp As DTOEdiversaOrdrsp) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiversaOrdrsp)(oEdiversaOrdrSp, exs, "EdiversaOrdrSp")
    End Function
End Class

Public Class EdiversaOrdrSps
    Inherits _FeblBase

    Shared Async Function Headers(exs As List(Of Exception)) As Task(Of List(Of DTOEdiversaOrdrsp))
        Return Await Api.Fetch(Of List(Of DTOEdiversaOrdrsp))(exs, "EdiversaOrdrSps")
    End Function

End Class
