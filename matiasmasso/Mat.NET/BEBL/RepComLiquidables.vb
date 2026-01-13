Public Class RepComLiquidable
    Shared Function Update(oRepComLiquidable As DTORepComLiquidable, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepComLiquidableLoader.Update(oRepComLiquidable, exs)
        Return retval
    End Function

    Shared Function Descarta(oRepComLiquidable As DTORepComLiquidable, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = RepComLiquidableLoader.Descarta(oRepComLiquidable, exs)
        Return retval
    End Function
End Class

Public Class RepComLiquidables
    Shared Function PendentsDeLiquidar(Optional oRep As DTORep = Nothing) As List(Of DTORepComLiquidable)
        Dim retval As List(Of DTORepComLiquidable) = RepComLiquidablesLoader.PendentsDeLiquidar ' RepComLiquidablesLoader.Pendents(oRep)
        Return retval
    End Function

    Shared Function Sincronitza(exs As List(Of Exception)) As Boolean
        Return RepComLiquidablesLoader.Sincronitza(exs)
    End Function

End Class
