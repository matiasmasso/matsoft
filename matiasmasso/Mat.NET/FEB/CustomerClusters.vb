

Public Class CustomerCluster
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOCustomerCluster)
        Return Await Api.Fetch(Of DTOCustomerCluster)(exs, "CustomerCluster", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oCustomerCluster As DTOCustomerCluster) As Boolean
        If Not oCustomerCluster.IsLoaded And Not oCustomerCluster.IsNew Then
            Dim pCustomerCluster = Api.FetchSync(Of DTOCustomerCluster)(exs, "CustomerCluster", oCustomerCluster.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCustomerCluster)(pCustomerCluster, oCustomerCluster, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oCustomerCluster As DTOCustomerCluster) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCustomerCluster)(oCustomerCluster, exs, "CustomerCluster")
        oCustomerCluster.IsNew = False
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oCustomerCluster As DTOCustomerCluster) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCustomerCluster)(oCustomerCluster, exs, "CustomerCluster")
    End Function

    Shared Async Function Children(exs As List(Of Exception), oParent As DTOCustomerCluster) As Task(Of List(Of DTOCustomer))
        Return Await Api.Fetch(Of List(Of DTOCustomer))(exs, "CustomerCluster/Children", oParent.Guid.ToString())
    End Function

End Class

Public Class CustomerClusters
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oParent As DTOGuidNom) As Task(Of List(Of DTOCustomerCluster))
        Return Await Api.Fetch(Of List(Of DTOCustomerCluster))(exs, "CustomerClusters", oParent.Guid.ToString())
    End Function

End Class

