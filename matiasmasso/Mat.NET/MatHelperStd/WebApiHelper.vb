Public Class WebApiHelper


    Shared Function SendGetRequest(url As String, oParams As Dictionary(Of String, String), ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean

        Dim sb As New System.Text.StringBuilder
        sb.Append(url)

        If oParams IsNot Nothing Then
            For i As Integer = 0 To oParams.Count - 1
                If i = 0 Then sb.Append("?") Else sb.Append("&")
                sb.Append(oParams.Keys(i) & "=" & oParams.Values(i))
            Next
        End If

        Dim urlWithQueryString As String = sb.ToString
        Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(urlWithQueryString)
        req.ContentType = "application/json"
        req.Method = "GET"

        Try
            Dim oResponseStream As System.IO.Stream = req.GetResponse.GetResponseStream()
            Dim reader As New System.IO.StreamReader(oResponseStream)
            jsonOutputString = reader.ReadToEnd()
            reader.Close()
            oResponseStream.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function


End Class
