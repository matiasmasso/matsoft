Public Class ProductStats
    Shared Function All(oCategory As DTOProductCategory, exs As List(Of Exception)) As List(Of DTOProductStat)
        Return ProductStatsLoader.All(oCategory, exs)
    End Function

End Class
