Public Class WinBug

    Shared Function Find(oGuid As Guid) As DTOWinBug
        Dim retval As DTOWinBug = WinBugLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oWinBug As DTOWinBug) As Boolean
        Dim retval As Boolean = WinBugLoader.Load(oWinBug)
        Return retval
    End Function

    Shared Function Update(oWinBug As DTOWinBug, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WinBugLoader.Update(oWinBug, exs)
        Return retval
    End Function

    Shared Function Delete(oWinBug As DTOWinBug, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WinBugLoader.Delete(oWinBug, exs)
        Return retval
    End Function


    Shared Function Log(sObs As String, Optional oUser As DTOUser = Nothing) As Boolean
        Dim oWinBug As New DTOWinBug
        With oWinBug
            .Fch = DTO.GlobalVariables.Now()
            .Obs = sObs
            .User = oUser
        End With

        Dim exs As New List(Of Exception)
        Dim retval As Boolean = Update(oWinBug, exs)
        Return retval
    End Function

End Class

Public Class WinBugs

    Shared Function All() As List(Of DTOWinBug)
        Dim retval As List(Of DTOWinBug) = WinBugsLoader.All()
        Return retval
    End Function

    Shared Function Delete(oWinBugs As List(Of DTOWinBug), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = WinBugsLoader.Delete(oWinBugs, exs)
        Return retval
    End Function

End Class
