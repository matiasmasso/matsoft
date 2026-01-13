Public Class Ranking

    Shared Sub Load(ByRef oRanking As DTORanking)
        RankingLoader.Load(oRanking)
    End Sub

    Shared Sub LoadItems(ByRef oRanking As DTORanking)
        RankingLoader.Load(oRanking)
    End Sub

    Shared Sub LoadCatalog(ByRef oRanking As DTORanking)
        RankingLoader.LoadCatalog(oRanking)
    End Sub

    Shared Function CustomerRanking(oUser As DTOUser,
                                    Optional oProduct As DTOProduct = Nothing) As DTORanking
        Dim retval As New DTORanking
        With retval
            .User = oUser
            .ConceptType = DTORanking.ConceptTypes.Geo
            .Product = oProduct
        End With
        RankingLoader.Load(retval)
        Return retval
    End Function

End Class

