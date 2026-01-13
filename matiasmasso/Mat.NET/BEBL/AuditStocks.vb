Public Class AuditStock

    Shared Function Find(oGuid As Guid) As DTOAuditStock
        Dim retval As DTOAuditStock = AuditStockLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oAuditStock As DTOAuditStock, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AuditStockLoader.Update(oAuditStock, exs)
        Return retval
    End Function

    Shared Function Delete(oAuditStock As DTOAuditStock, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AuditStockLoader.Delete(oAuditStock, exs)
        Return retval
    End Function

End Class



Public Class AuditStocks

    Shared Function All(oExercici As DTOExercici) As List(Of DTOAuditStock)
        Dim retval As List(Of DTOAuditStock) = AuditStocksLoader.All(oExercici)
        Return retval
    End Function


    Shared Function Update(oAuditStocks As List(Of DTOAuditStock), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = AuditStocksLoader.Update(oAuditStocks, exs)
        Return retval
    End Function


End Class
