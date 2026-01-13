Public Class WtbolLogLoader
    Shared Function Log(oSite As DTOWtbolSite, Ip As String, exs As List(Of Exception)) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("INSERT INTO WtbolLog (Site, Ip) VALUES ('" & oSite.Guid.ToString & "','" & Ip & "')")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function
End Class
