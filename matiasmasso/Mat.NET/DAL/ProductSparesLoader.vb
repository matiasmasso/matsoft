Public Class ProductSparesLoader
    Shared Function All() As List(Of DTOProductSpare)
        Dim retval As New List(Of DTOProductSpare)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT * ")
        sb.AppendLine("FROM ArtSpare ")
        Dim SQL As String = sb.ToString
        Dim oDrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While oDrd.Read
            Dim item As New DTOProductSpare()
            With item
                .Product = oDrd("ProductGuid")
                .Target = oDrd("TargetGuid")
                .Cod = oDrd("Cod")
            End With
            retval.Add(item)
        Loop
        oDrd.Close()
        Return retval
    End Function
End Class
