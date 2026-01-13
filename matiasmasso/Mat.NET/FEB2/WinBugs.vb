Public Class WinBug
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOWinBug)
        Return Await Api.Fetch(Of DTOWinBug)(exs, "WinBug", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oWinBug As DTOWinBug) As Boolean
        If Not oWinBug.IsLoaded And Not oWinBug.IsNew Then
            Dim pWinBug = Api.FetchSync(Of DTOWinBug)(exs, "WinBug", oWinBug.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWinBug)(pWinBug, oWinBug, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function UpdateSync(exs As List(Of Exception), oWinBug As DTOWinBug) As Boolean
        Return Api.UpdateSync(Of DTOWinBug)(oWinBug, exs, "WinBug")
        oWinBug.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oWinBug As DTOWinBug) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWinBug)(oWinBug, exs, "WinBug")
    End Function
End Class

Public Class WinBugs
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOWinBug))
        Return Await Api.Fetch(Of List(Of DTOWinBug))(exs, "WinBugs")
    End Function

End Class
