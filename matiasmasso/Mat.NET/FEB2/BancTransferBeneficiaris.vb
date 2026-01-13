Public Class BancTransferBeneficiari
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBancTransferBeneficiari)
        Return Await Api.Fetch(Of DTOBancTransferBeneficiari)(exs, "BancTransferBeneficiari", oGuid.ToString())
    End Function

End Class
