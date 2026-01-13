Imports Newtonsoft.Json

Public Class ApiHelper_Old


    Shared Function PostRequest(Of T, U)(ByVal urlSegment As String, ByVal input As T) As U
        Dim url As String = Api.ApiUrl(urlSegment)

        Dim jsonSettings = New JsonSerializerSettings()
        jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        Dim jsonInput As String = JsonConvert.SerializeObject(input, jsonSettings)

        Dim jsonoutput As String = ""
        Dim contentType As String = "application/json"
        Dim method As String = "POST"
        Dim exs As New List(Of Exception)
        Dim retval As U = Nothing
        If ApiHelper_Old.SendRequest(url, jsonInput, contentType, "POST", jsonoutput, exs) Then
            retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of U)(jsonoutput)
        End If
        Return retval
    End Function


    Shared Function SendRequest(url As String, jsonInputString As String, contentType As String, method As String, ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim uri As New Uri(url)
        Dim jsonDataBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(jsonInputString)
        Dim req As System.Net.WebRequest = System.Net.WebRequest.Create(uri)
        req.ContentType = contentType
        req.Method = method
        req.ContentLength = jsonDataBytes.Length

        Try
            Dim stream = req.GetRequestStream()
            If jsonDataBytes IsNot Nothing Then
                stream.Write(jsonDataBytes, 0, jsonDataBytes.Length)
            End If
            stream.Close()

            Dim oResponse As Net.WebResponse = req.GetResponse()
            Dim oResponseStream = req.GetResponse().GetResponseStream()

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
