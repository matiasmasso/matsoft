Public Class CliReturn
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCliReturn)
        Return Await Api.Fetch(Of DTOCliReturn)(exs, "CliReturn", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCliReturn As DTOCliReturn, exs As List(Of Exception)) As Boolean
        If Not oCliReturn.IsLoaded And Not oCliReturn.IsNew Then
            Dim pCliReturn = Api.FetchSync(Of DTOCliReturn)(exs, "CliReturn", oCliReturn.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCliReturn)(pCliReturn, oCliReturn, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCliReturn As DTOCliReturn, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCliReturn)(oCliReturn, exs, "CliReturn")
        oCliReturn.IsNew = False
    End Function

    Shared Async Function Delete(oCliReturn As DTOCliReturn, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCliReturn)(oCliReturn, exs, "CliReturn")
    End Function
End Class

Public Class CliReturns

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOCliReturn))
        Return Await Api.Fetch(Of List(Of DTOCliReturn))(exs, "CliReturns")
    End Function

End Class
