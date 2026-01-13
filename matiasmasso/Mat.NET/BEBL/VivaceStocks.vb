Public Class VivaceStocks
    Shared Function Fchs() As List(Of Date)
        Return VivaceStocksLoader.Fchs()
    End Function

    Shared Function All(oEmp As DTOEmp, DtFch As Date) As List(Of DTOVivaceStock)
        Return VivaceStocksLoader.All(oEmp, DtFch)
    End Function

    Shared Function Update(exs As List(Of Exception), oEmp As DTOEmp, DtFch As Date, values As List(Of DTOVivaceStock)) As Boolean
        Return VivaceStocksLoader.Update(exs, oEmp, DtFch, values)
    End Function

End Class
