Public Class CustomerCluster

    Shared Function Find(oGuid As Guid) As DTOCustomerCluster
        Return CustomerClusterLoader.Find(oGuid)
    End Function

    Shared Function Update(oCustomerCluster As DTOCustomerCluster, exs As List(Of Exception)) As Boolean
        Return CustomerClusterLoader.Update(oCustomerCluster, exs)
    End Function

    Shared Function Delete(oCustomerCluster As DTOCustomerCluster, exs As List(Of Exception)) As Boolean
        Return CustomerClusterLoader.Delete(oCustomerCluster, exs)
    End Function

    Shared Function Children(oCustomerCluster As DTOCustomerCluster) As List(Of DTOCustomer)
        Return CustomerClusterLoader.Children(oCustomerCluster)
    End Function

End Class



Public Class CustomerClusters
    Shared Function All(oHolding As DTOHolding) As List(Of DTOCustomerCluster)
        Dim retval As List(Of DTOCustomerCluster) = CustomerClustersLoader.All(oHolding)
        Return retval
    End Function
End Class
