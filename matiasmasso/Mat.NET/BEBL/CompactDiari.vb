Public Class CompactDiari
    Shared Function Items(exs As List(Of Exception), value As DTOSalesQuery) As List(Of DTOSalesQuery.Item)
        Return CompactDiariLoader.Items(exs, value)
    End Function


End Class
