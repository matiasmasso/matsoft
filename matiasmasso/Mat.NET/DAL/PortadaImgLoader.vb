Public Class PortadaImgLoader

    Shared Function ImageMime(id As String) As ImageMime
        Dim retval As New ImageMime()
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PortadaImg ")
        sb.AppendLine("WHERE Id='" & id & "' ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        If oDrd.Read Then
            retval.Mime = oDrd("Mime")
            retval.ByteArray = oDrd("Img")
        End If

        oDrd.Close()

        Return retval
    End Function

End Class

Public Class PortadaImgsLoader

    Shared Function All() As List(Of PortadaImgModel)
        Dim retval As New List(Of PortadaImgModel)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM PortadaImg ")
        sb.AppendLine("ORDER BY Id")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New PortadaImgModel()
            With item
                .Id = oDrd("Id")
                .Title = SQLHelper.GetStringFromDataReader(oDrd("Title"))
                .NavigateTo = SQLHelper.GetStringFromDataReader(oDrd("Url"))
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function


End Class

