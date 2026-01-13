Public Class ExceptionsHelper
    Shared Function ToFlatString(oExs As List(Of Exception)) As String
        Dim sb As New System.Text.StringBuilder
        For Each ex As Exception In oExs
            sb.AppendLine(ex.Message)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class
