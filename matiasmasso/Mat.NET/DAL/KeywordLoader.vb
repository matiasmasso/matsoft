
Public Class KeywordsLoader
    Shared Function FromSrc(target As Guid) As List(Of String)
        Dim retval As New List(Of String)
        Dim SQL As String = "SELECT * FROM Keyword WHERE Target='" & target.ToString() & "'"
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            retval.Add(oDrd("Value").ToString())
        Loop
        oDrd.Close()
        Return retval
    End Function

End Class


