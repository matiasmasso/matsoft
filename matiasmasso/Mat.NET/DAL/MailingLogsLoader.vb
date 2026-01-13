Public Class MailingLogsLoader

    Shared Function All(oSource As DTOBaseGuid) As List(Of DTOMailingLog)
        Dim retval As New List(Of DTOMailingLog)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT MailingLog.fch, MailingLog.Usuari ")
        sb.AppendLine(", Email.Nickname, Email.Nom, Email.Adr ")
        sb.AppendLine("FROM MailingLog ")
        sb.AppendLine("INNER JOIN Email ON MailingLog.Usuari = Email.Guid ")
        sb.AppendLine("WHERE MailingLog.Guid = '" & oSource.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY MailingLog.Fch")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim oUser As New DTOUser(oDrd("Usuari"))
            With oUser
                .emailAddress = SQLHelper.GetStringFromDataReader(oDrd("adr"))
                .nickName = SQLHelper.GetStringFromDataReader(oDrd("Nickname"))
                .nom = SQLHelper.GetStringFromDataReader(oDrd("Nom"))
            End With
            Dim item As New DTOMailingLog
            With item
                .user = oUser
                .fch = oDrd("Fch")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class
