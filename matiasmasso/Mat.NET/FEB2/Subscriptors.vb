Public Class Subscriptor
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oSubscriptionGuid As Guid, oUserGuid As Guid) As Task(Of DTOSubscriptor)
        Dim retval = Await Api.Fetch(Of DTOSubscriptor)(exs, "Subscriptor", oSubscriptionGuid.ToString(), oUserGuid.ToString())
        Return retval
    End Function


    Shared Async Function Find(exs As List(Of Exception), oWellknownSubscription As DTOSubscription.Wellknowns, oUser As DTOUser) As Task(Of DTOSubscriptor)
        Return Await Find(exs, oWellknownSubscription, oUser.Guid)
    End Function

    Shared Async Function Find(exs As List(Of Exception), oWellknownSubscription As DTOSubscription.Wellknowns, oUserGuid As Guid) As Task(Of DTOSubscriptor)
        Dim retval = Await Api.Fetch(Of DTOSubscriptor)(exs, "Subscriptor", oWellknownSubscription, oUserGuid.ToString())
        Return retval
    End Function

    Shared Async Function IsSubscribed(exs As List(Of Exception), oWellknownSubscription As DTOSubscription.Wellknowns, oUser As DTOUser) As Task(Of Boolean)
        Dim retval = Await Api.Fetch(Of Boolean)(exs, "Subscriptor/IsSubscribed", oWellknownSubscription, oUser.Guid.ToString())
        Return retval
    End Function

    Shared Async Function UnSubscribe(exs As List(Of Exception), oSubscriptor As DTOSubscriptor) As Task(Of Boolean)
        Dim oSubscriptors = {oSubscriptor}.ToList()
        Dim retval = Await Subscriptors.Unsubscribe(exs, oSubscriptors)
        Return retval
    End Function

End Class

Public Class Subscriptors
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oSubscription As DTOSubscription, Optional oContact As DTOContact = Nothing) As Task(Of List(Of DTOSubscriptor))
        Return Await Api.Fetch(Of List(Of DTOSubscriptor))(exs, "Subscriptors", oSubscription.Guid.ToString, OpcionalGuid(oContact))
    End Function

    Shared Async Function Recipients(exs As List(Of Exception), oEmp As DTOEmp, oSubscriptionWellknown As DTOSubscription.Wellknowns, Optional oContact As DTOContact = Nothing) As Task(Of List(Of String))
        Dim oSubscription = DTOSubscription.Wellknown(oSubscriptionWellknown)
        Return Await Api.Fetch(Of List(Of String))(exs, "Subscriptors/Recipients", oSubscription.Guid.ToString, OpcionalGuid(oContact))
    End Function

    Shared Async Function Subscribe(exs As List(Of Exception), values As List(Of DTOSubscriptor)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOSubscriptor))(values, exs, "Subscriptors/Subscribe")
    End Function

    Shared Async Function Unsubscribe(exs As List(Of Exception), values As List(Of DTOSubscriptor)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOSubscriptor))(values, exs, "Subscriptors/Unsubscribe")
    End Function

End Class
