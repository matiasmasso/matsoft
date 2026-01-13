Public Class Subscription


#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOSubscription
        Dim retval As DTOSubscription = SubscriptionLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oSubscription As DTOSubscription, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SubscriptionLoader.Update(oSubscription, exs)
        Return retval
    End Function

    Shared Function Delete(oSubscription As DTOSubscription, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SubscriptionLoader.Delete(oSubscription, exs)
        Return retval
    End Function
#End Region

    Shared Function Subscriptors(oSubscription As DTOSubscription) As List(Of DTOSubscriptor)
        Dim retval As List(Of DTOSubscriptor) = SubscriptorsLoader.All(oSubscription)
        Return retval
    End Function

    Shared Function Subscriptors(oSubscription As DTOSubscription, oContact As DTOContact) As List(Of DTOSubscriptor)
        Dim retval As List(Of DTOSubscriptor) = SubscriptorsLoader.All(oSubscription, oContact)
        Return retval
    End Function

    Shared Function SubscriptorsWithManufacturer(oSubscription As DTOSubscription) As List(Of DTOSubscriptor)
        Dim retval As List(Of DTOSubscriptor) = SubscriptorsLoader.AllWithManufacturer(oSubscription)
        Return retval
    End Function
End Class

Public Class Subscriptions

    Shared Function All(oUser As DTOUser) As List(Of DTOSubscription) 'iMat 3.4
        Return SubscriptionsLoader.All(oUser)
    End Function

    Shared Function All(oEmp As DTOEmp, Optional oUser As DTOUser = Nothing) As List(Of DTOSubscription)
        Dim retval As List(Of DTOSubscription) = SubscriptionsLoader.All(oEmp, oUser)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, oRol As DTORol) As List(Of DTOSubscription)
        Dim retval As List(Of DTOSubscription) = SubscriptionsLoader.All(oEmp, oRol)
        Return retval
    End Function

    Shared Function Update(oUser As DTOUser, ByVal oNewSscs As List(Of DTOSubscription), ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = SubscriptionsLoader.Update(oUser, oNewSscs, exs)
        Return retval
    End Function
End Class
