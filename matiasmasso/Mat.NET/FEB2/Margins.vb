Public Class Margins
    Shared Async Function Fetch(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, mode As Models.MarginsModel.Modes, Optional target As DTOBaseGuid = Nothing) As Task(Of Models.MarginsModel)
        If target Is Nothing Then
            Return Await Api.Fetch(Of Models.MarginsModel)(exs, "Margins", oEmp.Id, year, mode)
        Else
            Return Await Api.Fetch(Of Models.MarginsModel)(exs, "Margins", oEmp.Id, year, mode, target.Guid.ToString())
        End If
    End Function

End Class
