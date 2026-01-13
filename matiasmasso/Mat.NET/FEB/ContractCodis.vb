Public Class ContractCodi

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOContractCodi)
        Return Await Api.Fetch(Of DTOContractCodi)(exs, "ContractCodi", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oContractCodi As DTOContractCodi, exs As List(Of Exception)) As Boolean
        If Not oContractCodi.IsLoaded And Not oContractCodi.IsNew Then
            Dim pContractCodi = Api.FetchSync(Of DTOContractCodi)(exs, "ContractCodi", oContractCodi.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOContractCodi)(pContractCodi, oContractCodi, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oContractCodi As DTOContractCodi, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOContractCodi)(oContractCodi, exs, "ContractCodi")
        oContractCodi.IsNew = False
    End Function

    Shared Async Function Delete(oContractCodi As DTOContractCodi, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOContractCodi)(oContractCodi, exs, "ContractCodi")
    End Function
End Class

Public Class ContractCodis

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOContractCodi))
        Return Await Api.Fetch(Of List(Of DTOContractCodi))(exs, "ContractCodis")
    End Function

    Shared Async Function Delete(oContractCodis As List(Of DTOContractCodi), exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOContractCodi))(oContractCodis, exs, "ContractCodis/delete")
    End Function

End Class
