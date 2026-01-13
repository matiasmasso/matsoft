Public Class EdiversaExceptionsLoader

    Shared Function All(oParent As DTOBaseGuid) As List(Of DTOEdiversaException)
        Dim retval As New List(Of DTOEdiversaException)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Cod, TagGuid, TagCod, Msg ")
        sb.AppendLine("FROM EdiversaExceptions ")
        sb.AppendLine("WHERE Parent='" & oParent.Guid.ToString & "' ")
        sb.AppendLine("ORDER BY TagGuid, TagCod")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim ex = New DTOEdiversaException(oDrd("Guid"))
            With ex
                .Cod = oDrd("Cod")
                .TagCod = oDrd("TagCod")
                If Not IsDBNull(oDrd("TagGuid")) Then

                    .Tag = New DTOBaseGuid(oDrd("TagGuid"))
                End If
                .Msg = SQLHelper.GetStringFromDataReader(oDrd("Msg"))
            End With
            retval.Add(ex)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class
