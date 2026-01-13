Public Class Incoterm
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), id As String) As Task(Of DTOIncoterm)
        Return Await Api.Fetch(Of DTOIncoterm)(exs, "Incoterm", id)
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oIncoterm As DTOIncoterm) As Boolean
        If Not String.IsNullOrEmpty(oIncoterm.Id) Then
            Dim pIncoterm = Api.FetchSync(Of DTOIncoterm)(exs, "Incoterm", oIncoterm.Id)
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOIncoterm)(pIncoterm, oIncoterm, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oIncoterm As DTOIncoterm) As Task(Of Boolean)
        Return Await Api.Update(Of DTOIncoterm)(oIncoterm, exs, "Incoterm")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oIncoterm As DTOIncoterm) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOIncoterm)(oIncoterm, exs, "Incoterm")
    End Function
End Class

Public Class Incoterms
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOIncoterm))
        Return Await Api.Fetch(Of List(Of DTOIncoterm))(exs, "Incoterms")
    End Function

End Class

