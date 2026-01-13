Public Class Multa

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOMulta)
        Return Await Api.Fetch(Of DTOMulta)(exs, "Multa", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oMulta As DTOMulta, exs As List(Of Exception)) As Boolean
        If Not oMulta.IsLoaded And Not oMulta.IsNew Then
            Dim pMulta = Api.FetchSync(Of DTOMulta)(exs, "Multa", oMulta.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOMulta)(pMulta, oMulta, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oMulta As DTOMulta, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOMulta)(oMulta, exs, "Multa")
        oMulta.IsNew = False
    End Function


    Shared Async Function Delete(oMulta As DTOMulta, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOMulta)(oMulta, exs, "Multa")
    End Function
End Class


Public Class Multes
    Shared Async Function All(oSubjecte As DTOBaseGuid, exs As List(Of Exception)) As Task(Of List(Of DTOMulta))
        Return Await Api.Fetch(Of List(Of DTOMulta))(exs, "Multas", oSubjecte.Guid.ToString())
    End Function

End Class
