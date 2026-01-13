Public Class PremiumCustomer

#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOPremiumCustomer
        Dim retval As DTOPremiumCustomer = PremiumCustomerLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oPremiumCustomer As DTOPremiumCustomer) As Boolean
        Dim retval As Boolean = PremiumCustomerLoader.Load(oPremiumCustomer)
        Return retval
    End Function

    Shared Function Update(oPremiumCustomer As DTOPremiumCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PremiumCustomerLoader.Update(oPremiumCustomer, exs)
        Return retval
    End Function

    Shared Function Delete(oPremiumCustomer As DTOPremiumCustomer, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = PremiumCustomerLoader.Delete(oPremiumCustomer, exs)
        Return retval
    End Function
#End Region

End Class

Public Class PremiumCustomers

    Shared Function All(oPremiumLine As DTOPremiumLine) As List(Of DTOPremiumCustomer)
        Dim retval As List(Of DTOPremiumCustomer) = PremiumCustomersLoader.All(oPremiumLine)
        Return retval
    End Function

    Shared Function All(oCustomer As DTOCustomer) As List(Of DTOPremiumCustomer)
        Dim retval As List(Of DTOPremiumCustomer) = PremiumCustomersLoader.All(oCustomer)
        Return retval
    End Function


End Class