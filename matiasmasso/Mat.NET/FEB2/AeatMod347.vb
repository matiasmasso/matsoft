Public Class AeatMod347

    Shared Async Function Factory(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of DTOAeatMod347)
        Dim retval = Await Api.Fetch(Of DTOAeatMod347)(exs, "AeatMod347", oExercici.Emp.Id, oExercici.Year)
        If retval IsNot Nothing Then
            For Each item In retval.Items
                item.Parent = retval
            Next
        End If
        Return retval
    End Function

    Shared Async Function Csv(exs As List(Of Exception), oMod347 As DTOAeatMod347) As Task(Of DTOCsv)
        Return Await Api.Execute(Of DTOAeatMod347, DTOCsv)(oMod347, exs, "AeatMod347/Csv")
    End Function

    Shared Async Function CompresDetall(exs As List(Of Exception), oExercici As DTOExercici, oContact As DTOContact) As Task(Of List(Of DTOAeatMod347Cca))
        Return Await Api.Fetch(Of List(Of DTOAeatMod347Cca))(exs, "AeatMod347/CompresDetall", oExercici.Emp.Id, oExercici.Year, oContact.Guid.ToString())
    End Function

    Shared Async Function VendesDetall(exs As List(Of Exception), oExercici As DTOExercici, oContact As DTOContact) As Task(Of List(Of DTOAeatMod347Cca))
        Return Await Api.Fetch(Of List(Of DTOAeatMod347Cca))(exs, "AeatMod347/VendesDetall", oExercici.Emp.Id, oExercici.Year, oContact.Guid.ToString())
    End Function

    Shared Async Function MinimDeclarable(exs As List(Of Exception)) As Task(Of Decimal)
        Return Await Api.Fetch(Of Decimal)(exs, "AeatMod347/minimDeclarable")
    End Function

    Shared Async Function Declarable(value As DTOAeatMod347Item, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOAeatMod347Item)(value, exs, "AeatMod347/declarable")
    End Function

End Class
