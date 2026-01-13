Public Class CliApertura
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCliApertura)
        Return Await Api.Fetch(Of DTOCliApertura)(exs, "CliApertura", oGuid.ToString())
    End Function

    Shared Function FindSync(oGuid As Guid, exs As List(Of Exception)) As DTOCliApertura
        Return Api.FetchSync(Of DTOCliApertura)(exs, "CliApertura", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oCliApertura As DTOCliApertura, exs As List(Of Exception)) As Boolean
        If Not oCliApertura.IsLoaded And Not oCliApertura.IsNew Then
            Dim pCliApertura = Api.FetchSync(Of DTOCliApertura)(exs, "CliApertura", oCliApertura.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCliApertura)(pCliApertura, oCliApertura, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCliApertura As DTOCliApertura, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCliApertura)(oCliApertura, exs, "CliApertura")
        oCliApertura.IsNew = False
    End Function

    Shared Async Function Delete(oCliApertura As DTOCliApertura, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCliApertura)(oCliApertura, exs, "CliApertura")
    End Function

    Shared Async Function Send(oEmp As DTOEmp, oCliApertura As DTOCliApertura, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "CliApertura/send", oEmp.Id, oCliApertura.Guid.ToString())
    End Function
End Class

Public Class CliAperturas

    Shared Async Function All(oUser As DTOUser, exs As List(Of Exception)) As Task(Of DTOCliApertura.Collection)
        Return Await Api.Fetch(Of DTOCliApertura.Collection)(exs, "CliAperturas", oUser.Guid.ToString())
    End Function

End Class

