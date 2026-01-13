Public Class Cur
    Shared Async Function Update(oCur As DTOCur, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCur)(oCur, exs, "Cur")
    End Function

End Class

Public Class Curs
    Shared LastUpdate As Date


    Shared Async Function AllAsync(exs As List(Of Exception)) As Task(Of DTOCur.Collection)
        Dim retval As New DTOCur.Collection
        If (DTO.GlobalVariables.Now() - LastUpdate).TotalHours > 24 Then
            LastUpdate = DTO.GlobalVariables.Now()
            retval = Await Api.Fetch(Of DTOCur.Collection)(exs, "curs")
        End If
        Return retval
    End Function

    Shared Function AllSync(exs As List(Of Exception)) As DTOCur.Collection
        Dim retval As New DTOCur.Collection
        If (DTO.GlobalVariables.Now() - LastUpdate).TotalHours > 24 Then
            LastUpdate = DTO.GlobalVariables.Now()
            retval = Api.FetchSync(Of DTOCur.Collection)(exs, "curs")
        End If
        Return retval
    End Function

End Class
