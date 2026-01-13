Public Class BancTransferBeneficiari

    Shared Function Find(oGuid As Guid) As DTOBancTransferBeneficiari
        Dim retval As DTOBancTransferBeneficiari = BancTransferBeneficiariLoader.Find(oGuid)
        Return retval
    End Function

End Class
