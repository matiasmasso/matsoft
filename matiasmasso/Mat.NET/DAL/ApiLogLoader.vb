Public Class ApiLogLoader
    Shared Function Log(oApiLog As DTOApiLog, exs As List(Of Exception)) As Boolean
        Dim sb As New Text.StringBuilder
        sb.AppendLine("INSERT INTO ApiLog (Cod, Ip) VALUES (" & CInt(oApiLog.Cod) & ",'" & oApiLog.Ip & "')")
        Dim SQL As String = sb.ToString
        SQLHelper.ExecuteNonQuery(SQL, exs)
        Dim retval As Boolean = exs.Count = 0
        Return retval

    End Function
End Class
