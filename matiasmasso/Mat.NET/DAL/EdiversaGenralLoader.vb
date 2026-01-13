Public Class EdiversaGenralsLoader

    Shared Function All(Optional sSearchKey As String = "", Optional HideObsolets As Boolean = False) As List(Of DTOEdiversaGenral)
        Dim retval As New List(Of DTOEdiversaGenral)
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("SELECT Guid, Fch, Text ")
        sb.AppendLine("FROM Edi ")
        sb.AppendLine("WHERE Tag='GENRAL_D_96A_UN_EAN003' ")
        If sSearchKey > "" Then
            sb.AppendLine("AND Text LIKE '%" & sSearchKey & "%' ")
        End If
        If HideObsolets Then
            sb.AppendLine("AND Result=0 ")
        End If
        sb.AppendLine("ORDER BY Fch DESC")

        Dim SQL As String = sb.ToString
        Dim odrd As SqlDataReader = SQLHelper.GetDataReader(SQL)
        Do While odrd.Read
            Dim item As New DTOEdiversaGenral(odrd("Guid"))
            Dim sText As String = odrd("Text")
            Dim sLines() As String = sText.Split(vbCrLf)
            Dim sTextLines() As String = Array.FindAll(Of String)(sLines, Function(x) x.Contains("FTX|AAI|"))
            Dim result As String = String.Join(Of String)(vbCrLf, sTextLines)
            With item
                .Fch = odrd("Fch")
                .Text = result
            End With
            retval.Add(item)
        Loop
        odrd.Close()
        Return retval
    End Function

End Class
