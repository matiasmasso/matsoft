Public Class Holding

    Shared Function Find(oGuid As Guid) As DTOHolding
        Return HoldingLoader.Find(oGuid)
    End Function

    Shared Function Update(oHolding As DTOHolding, exs As List(Of Exception)) As Boolean
        Return HoldingLoader.Update(oHolding, exs)
    End Function

    Shared Function Delete(oHolding As DTOHolding, exs As List(Of Exception)) As Boolean
        Return HoldingLoader.Delete(oHolding, exs)
    End Function

    Shared Function PendingExcel(oHolding As DTOHolding) As MatHelper.Excel.Sheet
        Return HoldingLoader.PendingExcel(oHolding)
    End Function
    Shared Function PendingPurchaseOrders(oHolding As DTOHolding) As List(Of DTOPurchaseOrder)
        Return HoldingLoader.PendingPurchaseOrders(oHolding)
    End Function

    Shared Function ComandesECIDuplicades() As String
        Return HoldingLoader.ComandesECIDuplicades()
    End Function

End Class


Public Class Holdings
    Shared Function All(oEmp As DTOEmp) As List(Of DTOHolding)
        Dim retval As List(Of DTOHolding) = HoldingsLoader.All(oEmp)
        Return retval
    End Function
End Class
