Public Class WebHelper
    Shared Function Indent(iCount As Integer) As String
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 1 To iCount
            sb.Append("&nbsp;")
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Shared Function IsPortugal(request As HttpRequest) As Boolean
        Dim tld As String = RequestHelper.TopLevelDomain(request)
        Dim retval As Boolean = (tld.ToLower = "pt")
        Return retval
    End Function


End Class
