Public Class BancTransferPool
    Shared Function Find(oGuid As Guid) As DTOBancTransferPool
        Dim retval As DTOBancTransferPool = BancTransferPoolLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromCca(oCca As DTOCca) As DTOBancTransferPool
        Dim retval As DTOBancTransferPool = BancTransferPoolLoader.FromCca(oCca)
        Return retval
    End Function

    Shared Function Load(ByRef oBancTransferPool As DTOBancTransferPool) As Boolean
        Dim retval As Boolean = BancTransferPoolLoader.Load(oBancTransferPool)
        Return retval
    End Function

    Shared Function Update(oBancTransferPool As DTOBancTransferPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancTransferPoolLoader.Update(oBancTransferPool, exs)
        Return retval
    End Function

    Shared Function SaveRef(oBancTransferPool As DTOBancTransferPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancTransferPoolLoader.SaveRef(oBancTransferPool, exs)
        Return retval
    End Function


    Shared Function Delete(oBancTransferPool As DTOBancTransferPool, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BancTransferPoolLoader.Delete(oBancTransferPool, exs)
        Return retval
    End Function
End Class
Public Class BancTransferPools
    Shared Function All(oEmp As DTOEmp, Optional oBanc As DTOBanc = Nothing) As List(Of DTOBancTransferPool)
        Dim retval As List(Of DTOBancTransferPool) = BancTransferPoolsLoader.All(oEmp, oBanc)
        Return retval
    End Function

End Class
