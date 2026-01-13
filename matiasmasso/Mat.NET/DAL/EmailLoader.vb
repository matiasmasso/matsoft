Public Class EmailsLoader

    Shared Function All(oContact As DTOContact, includeObsoletos As Boolean) As List(Of DTOEmail)

        Dim retval As New List(Of DTOEmail)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Email.Guid, Email.Adr, Email.BadmailGuid, Email.Obsoleto, Email.Privat, Email.Obs ")
        sb.AppendLine("FROM Email ")
        sb.AppendLine("INNER JOIN Email_Clis ON Email.Guid = Email_Clis.EmailGuid ")
        sb.AppendLine("WHERE Email_Clis.ContactGuid = '" & oContact.Guid.ToString & "' ")
        If Not includeObsoletos Then
            sb.AppendLine("AND Email.Privat=0 ")
            sb.AppendLine("AND Email.Obsoleto=0 ")
            sb.AppendLine("AND Email.BadMailGuid IS NULL ")
        End If
        sb.AppendLine("ORDER BY Email.Id")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOEmail(oDrd("Guid"))
            With item
                .EmailAddress = oDrd("adr")
                .Obs = SQLHelper.GetStringFromDataReader(oDrd("Obs"))
                If Not IsDBNull(oDrd("BadMailGuid")) Then .BadMail = New DTOCod(oDrd("BadMailGuid"))
                .Privat = SQLHelper.GetBooleanFromDatareader(oDrd("Privat"))
                .Obsoleto = SQLHelper.GetBooleanFromDatareader(oDrd("Obsoleto"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval

    End Function
End Class
