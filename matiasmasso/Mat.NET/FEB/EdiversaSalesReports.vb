Public Class EdiversaSalesReport

    Shared Async Function Items(value As DTOStat, exs As List(Of Exception)) As Task(Of List(Of DTOStatItem))
        Return Await Api.Execute(Of DTOStat, List(Of DTOStatItem))(value, exs, "EdiversaSalesReport/items")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oEdiversaSalesReport As DTOEdiversaSalesReport) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOEdiversaSalesReport)(oEdiversaSalesReport, exs, "EdiversaSalesReport/delete")
    End Function

End Class

Public Class EdiversaSalesReports

    Shared Async Function Years(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "EdiversaSalesReports/years", oEmp.Id)
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, year As Integer) As Task(Of List(Of DTOEdiversaSalesReport))
        Return Await Api.Fetch(Of List(Of DTOEdiversaSalesReport))(exs, "EdiversaSalesReports", oEmp.Id, year)
    End Function

End Class
