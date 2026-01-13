Public Class PrvCliNum

    Shared Function Customer(oProveidor As DTOProveidor, clinum As String) As DTOCustomer
        Dim oPrvCliNum = PrvCliNumLoader.Find(oProveidor, clinum)
        Dim retval As DTOCustomer = Nothing
        If oPrvCliNum IsNot Nothing Then
            retval = oPrvCliNum.Customer
            CustomerLoader.Load(retval)
        End If
        Return retval
    End Function

End Class
Public Class PrvCliNums

    Shared Function All(oProveidor As DTOProveidor) As List(Of DTOPrvCliNum)
        Return PrvCliNumsLoader.All(oProveidor)
    End Function

    Shared Function Update(oProveidor As DTOProveidor, oPrvCliNums As List(Of DTOPrvCliNum), exs As List(Of Exception)) As Boolean
        Return PrvCliNumsLoader.Update(oProveidor, oPrvCliNums, exs)
    End Function

End Class
