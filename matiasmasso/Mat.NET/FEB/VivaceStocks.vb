Public Class VivaceStocks
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date) As Task(Of List(Of DTOVivaceStock))
        Return Await Api.Fetch(Of List(Of DTOVivaceStock))(exs, "VivaceStocks", oEmp.Id, FormatFch(DtFch))
    End Function

    Shared Async Function Fchs(exs As List(Of Exception)) As Task(Of List(Of Date))
        Return Await Api.Fetch(Of List(Of Date))(exs, "VivaceStocks/fchs")
    End Function

    Shared Async Function Update(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date, values As List(Of DTOVivaceStock)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTOVivaceStock))(values, exs, "VivaceStocks", oEmp.Id, FormatFch(DtFch))
    End Function



End Class
