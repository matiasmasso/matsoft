Public Class imatLoader
    Shared Sub Log(oUser As DTOUser)
        Dim SQL As String = "INSERT INTO iMatLog(eMail) VALUES ('" & oUser.Guid.ToString & "')"
        Dim exs As New List(Of Exception)
        SQLHelper.ExecuteNonQuery(SQL, exs)
    End Sub
End Class
