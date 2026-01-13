Public Class Irpf
    Inherits _FeblBase

    Shared Async Function Factory(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, month As Integer) As Task(Of DTOIrpf)
        Return Await Api.Fetch(Of DTOIrpf)(exs, "Irpf", oEmp.Id, year, month)
    End Function
End Class
