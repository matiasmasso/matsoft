Public Class Promofarma
    Public Shared Function Feed(oMgz As DTOMgz) As DTO.Integracions.Promofarma.Feed
        Return PromofarmaFeedLoader.Feed(oMgz)
    End Function

    Public Shared Function DisableCheaperThan(min As Decimal) As DTO.Integracions.Promofarma.Feed
        'not implemented yet
        Return New DTO.Integracions.Promofarma.Feed()
    End Function



    Shared Function Find(oGuid As Guid) As DTO.Integracions.Promofarma.Feed.Item
        Return PromofarmaFeedLoader.Find(oGuid)
    End Function

    Shared Function Update(item As DTO.Integracions.Promofarma.Feed.Item, exs As List(Of Exception)) As Boolean
        Return PromofarmaFeedLoader.Update(item, exs)
    End Function
End Class

Public Class PromofarmaFeeds
    Shared Function Enable(oItems As List(Of DTO.Integracions.Promofarma.Feed.Item), blEnabled As Boolean, exs As List(Of Exception)) As Boolean
        Return PromofarmaFeedsLoader.Enable(oItems, blEnabled, exs)
    End Function
End Class