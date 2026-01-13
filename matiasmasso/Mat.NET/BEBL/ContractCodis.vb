Public Class ContractCodi

    Shared Function Find(oGuid As Guid) As DTOContractCodi
        Dim retval As DTOContractCodi = ContractCodiLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oContractCodi As DTOContractCodi, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContractCodiLoader.Update(oContractCodi, exs)
        Return retval
    End Function

    Shared Function Delete(oContractCodi As DTOContractCodi, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = ContractCodiLoader.Delete(oContractCodi, exs)
        Return retval
    End Function

End Class



Public Class ContractCodis
    Shared Function All() As List(Of DTOContractCodi)
        Dim retval As List(Of DTOContractCodi) = ContractCodisLoader.All()
        Return retval
    End Function

    Shared Function Delete(values As List(Of DTOContractCodi), exs As List(Of Exception)) As Boolean
        Return ContractCodisLoader.Delete(values, exs)
    End Function
End Class

