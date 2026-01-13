Public Class ProductRank
    Shared Function Load(oUser As DTOUser, oPeriod As DTOProductRank.Periods, oArea As DTOArea, unit As DTOProductRank.Units) As DTOProductRank
        Return ProductRankLoader.Load(oUser, oPeriod, oArea, unit)
    End Function
End Class
