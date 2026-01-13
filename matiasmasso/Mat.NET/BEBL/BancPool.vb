Public Class BancPool


    Shared Function Find(oGuid As Guid) As DTOBancPool
        Dim retval As DTOBancPool = BancPoolLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oBancPool As DTOBancPool) As Boolean
        Dim retval As Boolean = BancPoolLoader.Load(oBancPool)
        Return retval
    End Function

    Shared Function Update(oBancPool As DTOBancPool, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancPoolLoader.Update(oBancPool, exs)
        Return retval
    End Function

    Shared Function Delete(oBancPool As DTOBancPool, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancPoolLoader.Delete(oBancPool, exs)
        Return retval
    End Function

End Class



Public Class BancPools
    Shared Function All(Optional oBank As DTOBank = Nothing, Optional DtFch As Date = Nothing) As List(Of DTOBancPool)
        Dim retval As List(Of DTOBancPool) = BancPoolsLoader.All(oBank, DtFch)
        Return retval
    End Function
End Class
