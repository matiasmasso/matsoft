Public Class EdiversaInterlocutorLoader

End Class

Public Class EdiversaInterlocutorsLoader

    Shared Function Contacts() As List(Of DTOContact)
        Dim retval As New List(Of DTOContact)
        Dim sb As New Text.StringBuilder
        sb.AppendLine("SELECT EdiversaInterlocutor.EAN, CliGral.Guid, CliGral.RaoSocial ")
        sb.AppendLine("FROM EdiversaInterlocutor ")
        sb.AppendLine("INNER JOIN CliGral ON EdiversaInterlocutor.EAN = CliGral.Gln ")
        sb.AppendLine("ORDER BY EdiversaInterlocutor.EAN ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOContact(oDrd("Guid"))
            item.Nom = oDrd("RaoSocial")
            item.GLN = SQLHelper.GetEANFromDataReader(oDrd("Ean"))
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
