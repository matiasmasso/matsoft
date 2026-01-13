Public Class BankBranch

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOBankBranch)
        Return Await Api.Fetch(Of DTOBankBranch)(exs, "BankBranch", oGuid.ToString())
    End Function
    Shared Async Function Find(oBank As DTOBank, Id As String, exs As List(Of Exception)) As Task(Of DTOBankBranch)
        Return Await Api.Fetch(Of DTOBankBranch)(exs, "BankBranch", oBank.Guid.ToString, Id)
    End Function
    Shared Async Function Find(oCountry As DTOCountry, bankId As String, branchId As String, exs As List(Of Exception)) As Task(Of DTOBankBranch)
        Dim retval As DTOBankBranch = Nothing
        If oCountry IsNot Nothing Then
            If Not String.IsNullOrEmpty(bankId) Then
                If Not String.IsNullOrEmpty(branchId) Then
                    retval = Await Api.Fetch(Of DTOBankBranch)(exs, "BankBranch", oCountry.Guid.ToString, bankId, branchId)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Async Function FromIban(exs As List(Of Exception), IbanDigits As String) As Task(Of DTOBankBranch)
        Dim retval As DTOBankBranch = Await Api.Fetch(Of DTOBankBranch)(exs, "BankBranch/fromIban", IbanDigits)
        Return retval
    End Function

    Shared Function Load(ByRef oBankBranch As DTOBankBranch, exs As List(Of Exception)) As Boolean
        If Not oBankBranch.IsLoaded And Not oBankBranch.IsNew Then
            Dim pBankBranch = Api.FetchSync(Of DTOBankBranch)(exs, "BankBranch", oBankBranch.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOBankBranch)(pBankBranch, oBankBranch, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oBankBranch As DTOBankBranch, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOBankBranch)(oBankBranch.Trimmed, exs, "BankBranch")
        oBankBranch.IsNew = False
    End Function

    Shared Async Function Delete(oBankBranch As DTOBankBranch, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOBankBranch)(oBankBranch, exs, "BankBranch")
    End Function
End Class

Public Class BankBranches

    Shared Async Function All(oBank As DTOBank, exs As List(Of Exception)) As Task(Of List(Of DTOBankBranch))
        Dim retval As New List(Of DTOBankBranch)
        If oBank IsNot Nothing Then
            retval = Await Api.Fetch(Of List(Of DTOBankBranch))(exs, "BankBranches/fromBank", oBank.Guid.ToString())
            If retval IsNot Nothing Then
                For Each oBranch In retval
                    oBranch.Bank = oBank
                Next
            End If
        End If
        Return retval
    End Function

    Shared Async Function All(oLocation As DTOLocation, exs As List(Of Exception)) As Task(Of List(Of DTOBankBranch))
        Return Await Api.Fetch(Of List(Of DTOBankBranch))(exs, "BankBranches/fromLocation", oLocation.Guid.ToString())
    End Function

    Shared Async Function reLocate(exs As List(Of Exception), oLocateTo As DTOLocation, oBankBranches As List(Of DTOBankBranch)) As Task(Of Integer)
        Dim oGuids = oBankBranches.Select(Function(x) x.Guid).ToList
        Dim retval = Await Api.Execute(Of List(Of Guid), Integer)(oGuids, exs, "BankBranches/reLocate", oLocateTo.Guid.ToString())
        Return retval
    End Function

End Class
