Public Class BancTerm
    Inherits _FeblBase

    Shared Async Function Update(oBancTerm As DTOBancTerm, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOBancTerm)(oBancTerm, exs, "BancTerm")
        oBancTerm.IsNew = False
    End Function


    Shared Async Function Delete(oBancTerm As DTOBancTerm, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBancTerm)(oBancTerm, exs, "BancTerm")
    End Function
End Class

Public Class BancTerms
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, Optional oBanc As DTOBanc = Nothing) As Task(Of List(Of DTOBancTerm))
        Return Await Api.Fetch(Of List(Of DTOBancTerm))(exs, "BancTerms", oEmp.Id, OpcionalGuid(oBanc))
    End Function

End Class
