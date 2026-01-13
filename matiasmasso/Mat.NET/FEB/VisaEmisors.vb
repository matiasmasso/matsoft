Public Class VisaEmisor

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOVisaEmisor)
        Return Await Api.Fetch(Of DTOVisaEmisor)(exs, "VisaEmisor", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oVisaEmisor As DTOVisaEmisor, exs As List(Of Exception)) As Boolean
        If Not oVisaEmisor.IsLoaded And Not oVisaEmisor.IsNew Then
            Dim pVisaEmisor = Api.FetchSync(Of DTOVisaEmisor)(exs, "VisaEmisor", oVisaEmisor.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOVisaEmisor)(pVisaEmisor, oVisaEmisor, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oVisaEmisor As DTOVisaEmisor, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOVisaEmisor)(oVisaEmisor, exs, "VisaEmisor")
        oVisaEmisor.IsNew = False
    End Function

    Shared Async Function Delete(oVisaEmisor As DTOVisaEmisor, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOVisaEmisor)(oVisaEmisor, exs, "VisaEmisor")
    End Function
End Class

Public Class VisaEmisors

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOVisaEmisor))
        Return Await Api.Fetch(Of List(Of DTOVisaEmisor))(exs, "VisaEmisors")
    End Function

End Class
