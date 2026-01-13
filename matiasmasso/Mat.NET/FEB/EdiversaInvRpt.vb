Public Class EdiversaInvRpt
    Inherits _FeblBase

    Shared Async Function Send(exs As List(Of Exception), oEmp As DTOEmp, oCustomer As DTOCustomer) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "EdiversaInvRpt/send", oEmp.Id, oCustomer.Guid.ToString())
    End Function

    Shared Async Function Src(exs As List(Of Exception), oEmp As DTOEmp, oCustomer As DTOCustomer) As Task(Of String)
        Return Await Api.Fetch(Of String)(exs, "EdiversaInvRpt/Src", oEmp.Id, oCustomer.Guid.ToString())
    End Function

    Shared Async Function Excel(exs As List(Of Exception), oEmp As DTOEmp, oCustomer As DTOCustomer) As Task(Of MatHelper.Excel.Sheet)
        Return Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "EdiversaInvRpt/Excel", oEmp.Id, oCustomer.Guid.ToString())
    End Function

End Class
