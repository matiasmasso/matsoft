
Public Class Subscriptor
    Shared Function Find(oUserGuid As Guid, oSsc As DTOSubscription) As DTOSubscriptor
        Return SubscriptorLoader.Find(oUserGuid, oSsc)
    End Function

End Class
Public Class Subscriptors

    Shared Function All(oSubscription As DTOSubscription) As List(Of DTOSubscriptor)
        Dim retval As List(Of DTOSubscriptor) = SubscriptorsLoader.All(oSubscription)
        Return retval
    End Function

    Shared Function All(oSubscription As DTOSubscription, oContact As DTOContact) As List(Of DTOSubscriptor)
        Dim retval As List(Of DTOSubscriptor) = SubscriptorsLoader.All(oSubscription, oContact)
        Return retval
    End Function

    Shared Function AllWithManufacturer(oSubscription As DTOSubscription) As List(Of DTOSubscriptor)
        Dim retval As List(Of DTOSubscriptor) = SubscriptorsLoader.AllWithManufacturer(oSubscription)
        Return retval
    End Function

    Shared Function Recipients(oSubscription As DTOSubscription, Optional oContact As DTOContact = Nothing) As List(Of String)
        Return SubscriptorsLoader.Recipients(oSubscription, oContact)
    End Function

    Shared Function isSubscribed(oSubscriptor As DTOSubscriptor) As Boolean
        Return SubscriptorsLoader.IsSubscribed(oSubscriptor)
    End Function

    Shared Function Subscribe(exs As List(Of Exception), oSubscriptors As List(Of DTOSubscriptor)) As Boolean
        Return SubscriptorsLoader.Subscribe(oSubscriptors, exs)
    End Function

    Shared Function Unsubscribe(exs As List(Of Exception), oSubscriptors As List(Of DTOSubscriptor)) As Boolean
        Return SubscriptorsLoader.Unsubscribe(oSubscriptors, exs)
    End Function

End Class
