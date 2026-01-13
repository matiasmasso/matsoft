Public Class BankBranch

    Shared Function Find(oGuid As Guid) As DTOBankBranch
        Dim retval As DTOBankBranch = BankBranchLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Find(oBank As DTOBank, Id As String) As DTOBankBranch
        Dim retval As DTOBankBranch = BankBranchLoader.Find(oBank, Id)
        Return retval
    End Function

    Shared Function Find(oCountry As DTOCountry, BankId As String, BranchId As String) As DTOBankBranch
        Dim retval As DTOBankBranch = BankBranchLoader.Find(oCountry, BankId, BranchId)
        Return retval
    End Function

    Shared Function Update(oBankBranch As DTOBankBranch, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BankBranchLoader.Update(oBankBranch, exs)
        Return retval
    End Function

    Shared Function Delete(oBankBranch As DTOBankBranch, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = BankBranchLoader.Delete(oBankBranch, exs)
        Return retval
    End Function

End Class



Public Class BankBranches

    Shared Function All(oBank As DTOBank) As List(Of DTOBankBranch)
        Dim retval As List(Of DTOBankBranch) = BankBranchesLoader.FromBank(oBank)
        Return retval
    End Function

    Shared Function All(oLocation As DTOLocation) As List(Of DTOBankBranch)
        Dim retval As List(Of DTOBankBranch) = BankBranchesLoader.All(oLocation)
        Return retval
    End Function

    Shared Function reLocate(exs As List(Of Exception), oLocationTo As DTOLocation, oBankBranches As List(Of DTOBankBranch)) As Integer
        Return BankBranchesLoader.reLocate(exs, oLocationTo, oBankBranches)
    End Function
End Class
