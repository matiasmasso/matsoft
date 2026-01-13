Public Class SpvOut

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOSpvOut)
        Return Await Api.Fetch(Of DTOSpvOut)(exs, "SpvOut", oGuid.ToString())
    End Function

    Shared Async Function Update(oEmp As DTOEmp, oSpvOut As DTOSpvOut, exs As List(Of Exception)) As Task(Of DTOSpvOut)
        Dim retval = Await Api.Execute(Of DTOSpvOut, DTOSpvOut)(oSpvOut, exs, "spvout", oEmp.Id)
        If retval IsNot Nothing Then retval.RestoreObjects()
        Return retval
    End Function

    Shared Async Function Delete(oSpvOut As DTOSpvOut, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSpvOut)(oSpvOut, exs, "SpvOut")
    End Function
End Class
