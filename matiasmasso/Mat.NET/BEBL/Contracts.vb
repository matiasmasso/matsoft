Public Class Contract

    Shared Function Find(oGuid As Guid) As DTOContract
        Dim retval As DTOContract = ContractLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oContract As DTOContract, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContractLoader.Update(oContract, exs)
        Return retval
    End Function

    Shared Function Delete(oContract As DTOContract, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContractLoader.Delete(oContract, exs)
        Return retval
    End Function

End Class



Public Class Contracts
    Shared Function All(oUser As DTOUser, Optional oCodi As DTOContractCodi = Nothing, Optional oContact As DTOContact = Nothing) As List(Of DTOContract)
        Dim retval As List(Of DTOContract) = ContractsLoader.All(oUser, oCodi, oContact)
        Return retval
    End Function
    Shared Function All(oContact As DTOContact) As List(Of DTOContract)
        Dim retval As List(Of DTOContract) = ContractsLoader.All(oContact:=oContact)
        Return retval
    End Function
End Class



