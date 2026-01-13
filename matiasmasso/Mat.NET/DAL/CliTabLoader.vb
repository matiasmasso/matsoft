Public Class CliTabsLoader

    Shared Function Tabs(oContact As DTOContact) As List(Of DTOContact.Tabs)
        Dim retval As New List(Of DTOContact.Tabs)

        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT CliClient.Guid AS Client ")
        sb.AppendLine(", CliPrv.Guid AS Prv ")
        sb.AppendLine(", CliRep.Guid AS Rep ")
        sb.AppendLine(", CliStaff.Guid AS Staff ")
        sb.AppendLine(", CliBnc.Guid AS Bnc ")
        sb.AppendLine("FROM CliGral ")
        sb.AppendLine("LEFT OUTER JOIN CliClient ON CliGral.Guid = CliClient.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliPrv ON CliGral.Guid = CliPrv.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliRep ON CliGral.Guid = CliRep.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliStaff ON CliGral.Guid = CliStaff.Guid ")
        sb.AppendLine("LEFT OUTER JOIN CliBnc ON CliGral.Guid = CliBnc.Guid ")
        sb.AppendLine("WHERE CliGral.Guid = '" & oContact.Guid.ToString & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            If Not IsDBNull(oDrd("Client")) Then
                retval.Add(DTOContact.Tabs.Client)
            End If
            If Not IsDBNull(oDrd("Prv")) Then
                retval.Add(DTOContact.Tabs.Proveidor)
            End If
            If Not IsDBNull(oDrd("Rep")) Then
                retval.Add(DTOContact.Tabs.Rep)
            End If
            If Not IsDBNull(oDrd("Staff")) Then
                retval.Add(DTOContact.Tabs.Staff)
            End If
            If Not IsDBNull(oDrd("Bnc")) Then
                retval.Add(DTOContact.Tabs.Banc)
            End If
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
