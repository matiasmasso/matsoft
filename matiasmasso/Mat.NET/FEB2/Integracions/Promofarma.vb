Public Class PromofarmaFeed

    Shared Async Function Fetch(exs As List(Of Exception)) As Threading.Tasks.Task(Of DTO.Integracions.Promofarma.Feed)
        Return Await Api.Fetch(Of DTO.Integracions.Promofarma.Feed)(exs, "Promofarma/feed")
    End Function

    Shared Async Function DisableCheaperThan(exs As List(Of Exception), min As Decimal) As Task(Of Boolean)
        Return Await Api.Execute(Of Decimal, Boolean)(min, exs, "Promofarma/feed/DisableCheaperThan")
    End Function

End Class

Public Class PromofarmaFeedItem
    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTO.Integracions.Promofarma.Feed.Item)
        Return Await Api.Fetch(Of DTO.Integracions.Promofarma.Feed.Item)(exs, "Promofarma/feed", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oItem As DTO.Integracions.Promofarma.Feed.Item) As Boolean
        If Not oItem.IsLoaded And Not oItem.IsNew Then
            Dim pItem = Api.FetchSync(Of DTO.Integracions.Promofarma.Feed.Item)(exs, "Promofarma/feed", oItem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of Integracions.Promofarma.Feed.Item)(pItem, oItem, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oItem As Integracions.Promofarma.Feed.Item) As Task(Of Boolean)
        Return Await Api.Update(Of Integracions.Promofarma.Feed.Item)(oItem, exs, "Promofarma/feed")
        oItem.IsNew = False
    End Function


End Class

Public Class PromofarmaFeedItems
    Shared Async Function Enable(exs As List(Of Exception), items As List(Of Integracions.Promofarma.Feed.Item), blEnable As Boolean) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of Integracions.Promofarma.Feed.Item))(items, exs, "Promofarma/feed/enable", If(blEnable, "1", "0"))
    End Function
End Class
