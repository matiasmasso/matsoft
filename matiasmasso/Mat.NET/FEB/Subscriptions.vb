Public Class Subscription
    Shared Async Function Find(id As DTOSubscription.Ids, exs As List(Of Exception)) As Task(Of DTOSubscription)
        Return Await Api.Fetch(Of DTOSubscription)(exs, "subscription", CInt(id).ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oSubscription As DTOSubscription) As Boolean
        If Not oSubscription.IsLoaded Then
            Dim pSubscription = Api.FetchSync(Of DTOSubscription)(exs, "Subscription", oSubscription.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOSubscription)(pSubscription, oSubscription, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oSubscription As DTOSubscription) As Task(Of Boolean)
        Return Await Api.Update(Of DTOSubscription)(oSubscription, exs, "Subscription")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oSubscription As DTOSubscription) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOSubscription)(oSubscription, exs, "Subscription")
    End Function

    Shared Async Function Subscribe(exs As List(Of Exception), oEmp As DTOEmp, oSubscription As DTOSubscription, oUser As DTOUser) As Task(Of Boolean)
        oSubscription.Emp = oEmp
        Dim oSubscriptors As New List(Of DTOSubscriptor)
        Dim oSubscriptor As New DTOSubscriptor(oUser.Guid, oSubscription)
        oSubscriptors.Add(oSubscriptor)
        Dim retval = Await FEB.Subscriptors.Subscribe(exs, oSubscriptors)
        Return retval
    End Function

    Shared Async Function UnSubscribe(exs As List(Of Exception), oEmp As DTOEmp, oSubscription As DTOSubscription, oUser As DTOUser) As Task(Of Boolean)
        oSubscription.Emp = oEmp
        Dim oSubscriptors As New List(Of DTOSubscriptor)
        Dim oSubscriptor As New DTOSubscriptor(oUser.Guid, oSubscription)
        oSubscriptors.Add(oSubscriptor)
        Dim retval = Await FEB.Subscriptors.Unsubscribe(exs, oSubscriptors)
        Return retval
    End Function

    Shared Async Function Subscriptors(oSubscription As DTOSubscription, exs As List(Of Exception)) As Task(Of List(Of DTOSubscriptor))
        Return Await Api.Fetch(Of List(Of DTOSubscriptor))(exs, "subscription", oSubscription.Guid.ToString, "subscriptors")
    End Function

    Shared Async Function Subscriptors(oSubscription As DTOSubscription, oContact As DTOContact, exs As List(Of Exception)) As Task(Of List(Of DTOSubscriptor))
        Return Await Api.Fetch(Of List(Of DTOSubscriptor))(exs, "subscription", oSubscription.Guid.ToString, "subscriptors", oContact.Guid.ToString())
    End Function

    Shared Async Function SubscriptorsWithManufacturer(oSubscription As DTOSubscription, exs As List(Of Exception)) As Task(Of List(Of DTOSubscriptor))
        Return Await Api.Fetch(Of List(Of DTOSubscriptor))(exs, "subscription", oSubscription.Guid.ToString(), "SubscriptorsWithManufacturer")
    End Function

End Class


Public Class Subscriptions
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOSubscription))
        Dim retval = Await Api.Fetch(Of List(Of DTOSubscription))(exs, "subscriptions", oUser.Guid.ToString)
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, Optional oUser As DTOUser = Nothing) As Task(Of List(Of DTOSubscription))
        Dim retval = Await Api.Fetch(Of List(Of DTOSubscription))(exs, "subscriptions/FromEmpUser", oEmp.Id, OpcionalGuid(oUser))
        Return retval
    End Function

    Shared Function AllSync(exs As List(Of Exception), oEmp As DTOEmp, Optional oUser As DTOUser = Nothing) As List(Of DTOSubscription)
        Return Api.FetchSync(Of List(Of DTOSubscription))(exs, "subscriptions/FromEmpUser", oEmp.Id, OpcionalGuid(oUser))
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oRol As DTORol) As Task(Of List(Of DTOSubscription))
        Return Await Api.Fetch(Of List(Of DTOSubscription))(exs, "subscriptions/FromEmpRol", oEmp.Id, oRol.Id)
    End Function

    Shared Function AllSync(exs As List(Of Exception), oEmp As DTOEmp, oRol As DTORol) As List(Of DTOSubscription)
        Return Api.FetchSync(Of List(Of DTOSubscription))(exs, "subscriptions/FromEmpRol", oEmp.Id, oRol.Id)
    End Function

    Shared Async Function Update(exs As List(Of Exception), oUser As DTOUser, oSubscriptions As List(Of DTOSubscription)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOSubscription))(oSubscriptions, exs, "Subscriptions", oUser.Guid.ToString())
    End Function

End Class


