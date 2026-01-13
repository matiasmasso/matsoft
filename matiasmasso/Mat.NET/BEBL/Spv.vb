Public Class Spv

    Shared Function Find(ByRef oGuid As Guid) As DTOSpv
        Return SpvLoader.Find(oGuid)
    End Function

    Shared Function FromId(oEmp As DTOEmp, iYear As Integer, iId As Integer) As DTOSpv
        Dim retval = SpvLoader.FromId(oEmp, iYear, iId)
        Return retval
    End Function

    Shared Function Load(ByRef oSpv As DTOSpv) As Boolean
        Dim retval = SpvLoader.Load(oSpv)
        Return retval
    End Function

    Shared Function Update(oSpv As DTOSpv, ByRef exs As List(Of Exception)) As Boolean
        Return SpvLoader.Update(oSpv, exs)
    End Function

    Shared Function Delete(oSpv As DTOSpv, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SpvLoader.Delete(oSpv, exs)
        Return retval
    End Function

End Class

Public Class Spvs
    Shared Function ArrivalPending(oEmp As DTOEmp) As List(Of DTOSpv)
        Dim retval As List(Of DTOSpv) = SpvsLoader.ArrivalPending(oEmp)
        Return retval
    End Function

    Shared Function NotRead(oEmp As DTOEmp, ByRef oSpvs As List(Of DTOSpv), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SpvsLoader.NotRead(oEmp, oSpvs, exs)
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOSpv)
        Dim retval As List(Of DTOSpv) = SpvsLoader.All(oCustomer)
        Return retval
    End Function

    Shared Function All(oSpvIn As DTOSpvIn) As List(Of DTOSpv)
        Dim retval As List(Of DTOSpv) = SpvsLoader.All(oSpvIn)
        Return retval
    End Function

    Shared Function Headers(oEmp As DTOEmp, Optional customer As DTOCustomer = Nothing, Optional onlyOpen As Boolean = False, Optional year As Integer = 0) As List(Of DTOSpv)
        Dim retval As List(Of DTOSpv) = SpvsLoader.Headers(oEmp, customer, onlyOpen, year)
        Return retval
    End Function

End Class
