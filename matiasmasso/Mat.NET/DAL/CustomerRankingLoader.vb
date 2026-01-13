Public Class CustomerRankingLoader

    Shared Function Load(oQuery As DTOCustomerRanking) As Boolean
        Dim retval As Boolean

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT CliGral.Guid, CliGral.FullNom, SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100) AS Eur ")
        sb.AppendLine("FROM Pnc ")
        sb.AppendLine("INNER JOIN Pdc ON Pnc.PdcGuid = Pdc.Guid ")
        sb.AppendLine("                 AND Pdc.Cod = " & CInt(DTOPurchaseOrder.Codis.Client) & " ")
        sb.AppendLine("                 AND (Pdc.Fch BETWEEN '" & Format(oQuery.FchFrom, "yyyyMMdd") & "' AND '" & Format(oQuery.FchTo, "yyyyMMdd") & "') ")

        sb.AppendLine("INNER JOIN CliClient ON Pdc.CliGuid = CliClient.Guid ")
        sb.AppendLine("INNER JOIN CliGral ON Pdc.CliGuid = CliGral.Guid ")
        Select Case oQuery.User.Rol.id
            Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager
            Case DTORol.Ids.rep, DTORol.Ids.comercial
                sb.AppendLine("INNER JOIN Email_Clis ON Pnc.RepGuid = Email_Clis.ContactGuid AND Email_Clis.EmailGuid = '" & oQuery.User.Guid.ToString & "' ")
            Case DTORol.Ids.manufacturer
                sb.AppendLine("INNER JOIN VwProductParent ON Pnc.ArtGuid = VwProductParent.Child ")
                sb.AppendLine("INNER JOIN Tpa ON VwProductParent.Parent = Tpa.Guid ")
                sb.AppendLine("INNER JOIN Email_Clis ON Email_Clis.ContactGuid = Tpa.Proveidor AND Email_Clis.EmailGuid = '" & oQuery.User.Guid.ToString & "' ")
        End Select
        If oQuery.Area IsNot Nothing Then
            sb.AppendLine("INNER JOIN CliAdr ON CliGral.Guid = CliAdr.SrcGuid AND CliAdr.Cod = 1 ")
            sb.AppendLine("INNER JOIN VwAreaParent ON CliAdr.Zip = VwAreaParent.ChildGuid ")
        End If

        sb.AppendLine("WHERE CliGral.Emp = " & oQuery.User.Emp.Id & " ")
        If oQuery.Area IsNot Nothing Then
            sb.AppendLine("AND VwAreaParent.ParentGuid = '" & oQuery.Area.Guid.ToString & "' ")
        End If

        sb.AppendLine("AND (CliGral.Rol = " & CInt(DTORol.Ids.CliFull) & " OR CliGral.Rol = " & CInt(DTORol.Ids.CliLite) & ") ")

        sb.AppendLine("GROUP BY CliGral.Guid, CliGral.FullNom ")
        sb.AppendLine("HAVING SUM(Pnc.Qty*Pnc.Eur*(100-Pnc.Dto)/100)>0 ")
        sb.AppendLine("ORDER BY Eur DESC ")

        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oCustomer As New DTOCustomer(oDrd("Guid"))
            oCustomer.FullNom = oDrd("FullNom")
            Dim DcEur As Decimal = Math.Round(oDrd("Eur"), 2, MidpointRounding.AwayFromZero)
            oQuery.AddItem(oCustomer, DcEur)
        Loop
        oDrd.Close()
        retval = True
        Return retval
    End Function


End Class
