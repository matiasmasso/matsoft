Public Class RemittanceAdvice

    Shared Function FromCca(oCca As DTOCca) As DTORemittanceAdvice
        Dim retval = RemittanceAdviceLoader.FromCca(oCca)
        Dim oIbans = BEBL.Ibans.FromContact(retval.Proveidor, True, DTOIban.Cods.proveidor, DTO.GlobalVariables.Today())
        If oIbans.Count > 0 Then
            retval.Iban = oIbans.First
        End If
        Return retval
    End Function
End Class
