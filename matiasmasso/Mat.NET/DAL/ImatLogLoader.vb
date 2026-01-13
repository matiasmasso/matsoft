Public Class ImatLogLoader
    Shared Function Log(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim SQL As String = "INSERT INTO IMATLOG(EMAIL) VALUES ('" & oUser.Guid.ToString & "')"
            SQLHelper.ExecuteNonQuery(SQL, exs)
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function
End Class
