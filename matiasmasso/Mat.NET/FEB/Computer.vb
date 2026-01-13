Public Class Computer
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOComputer)
        Return Await Api.Fetch(Of DTOComputer)(exs, "Computer", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oComputer As DTOComputer) As Boolean
        If Not oComputer.IsLoaded And Not oComputer.IsNew Then
            Dim pComputer = Api.FetchSync(Of DTOComputer)(exs, "Computer", oComputer.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOComputer)(pComputer, oComputer, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oComputer As DTOComputer) As Task(Of Boolean)
        Return Await Api.Update(Of DTOComputer)(oComputer, exs, "Computer")
        oComputer.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oComputer As DTOComputer) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOComputer)(oComputer, exs, "Computer")
    End Function
End Class

Public Class Computers
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOComputer))
        Return Await Api.Fetch(Of List(Of DTOComputer))(exs, "Computers")
    End Function

End Class
