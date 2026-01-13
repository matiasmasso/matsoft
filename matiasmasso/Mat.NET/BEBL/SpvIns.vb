Public Class SpvIn
    Shared Function Find(oGuid As Guid) As DTOSpvIn
        Dim retval As DTOSpvIn = SpvInLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(oEmp As DTOEmp, iYea As Integer, iNum As Integer) As DTOSpvIn
        Dim retval As DTOSpvIn = SpvInLoader.Find(oEmp, iYea, iNum)
        Return retval
    End Function

    Shared Function Load(ByRef oSpvIn As DTOSpvIn) As Boolean
        Dim retval As Boolean = SpvInLoader.Load(oSpvIn)
        Return retval
    End Function

    Shared Function Update(ByRef oSpvIn As DTOSpvIn, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SpvInLoader.Update(oSpvIn, exs)
        Return retval
    End Function

    Shared Function Delete(oSpvIn As DTOSpvIn, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SpvInLoader.Delete(oSpvIn, exs)
        Return retval
    End Function
End Class

Public Class SpvIns
    Shared Function All(oEmp As DTOEmp) As List(Of DTOSpvIn)
        Dim retval As List(Of DTOSpvIn) = SpvInsLoader.All(oEmp)
        Return retval
    End Function

End Class
