Public Class CliCreditLoader


    Shared Function AlbsPerFacturar(oContact As DTOContact, aCredit As Boolean) As DTOAmt
        Dim retval = DTOAmt.Empty
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT SUM(Alb.Eur+ (CASE WHEN Alb.Pt2 IS NULL THEN 0 ELSE Alb.Pt2 END)) AS AlbEur ")
        sb.AppendLine("FROM Alb ")
        sb.AppendLine("INNER JOIN CliClient ON Alb.CliGuid = CliClient.Guid ") ''" & oContact.Guid.ToString & "'
        sb.AppendLine("WHERE (CliClient.Guid = '" & oContact.Guid.ToString & "' OR Cliclient.CcxGuid ='" & oContact.Guid.ToString & "') AND Alb.Facturable = 1 AND Alb.FraGuid IS NULL ")

        If aCredit Then
            sb.AppendLine("AND ALB.CashCod =" & CInt(DTOCustomer.CashCodes.credit).ToString & " ")
        Else
            sb.AppendLine("AND ALB.CashCod >" & CInt(DTOCustomer.CashCodes.credit).ToString & " ")
        End If

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = DAL.SQLHelper.GetDataReader(SQL, "@CliGuid", oContact.Guid.ToString())
        oDrd.Read()
        retval = SQLHelper.GetAmtFromDataReader(oDrd("AlbEur"))
        oDrd.Close()
        Return retval
    End Function

End Class