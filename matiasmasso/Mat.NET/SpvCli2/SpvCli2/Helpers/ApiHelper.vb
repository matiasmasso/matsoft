Public Class ApiHelper

    Public Shared Property UseLocalApi As Boolean


    Shared Function AbsoluteUrl(urlSegment As String) As String
        Dim retval As String
        If UseLocalApi Then
            retval = String.Format("http://localhost:55836/api/{0}", urlSegment)
        Else
            retval = String.Format("https://matiasmasso-api.azurewebsites.net/api/{0}", urlSegment)
        End If
        Return retval
    End Function

    Shared Function PostRequest(Of T, U)(ByVal urlSegment As String, ByVal input As T) As U
        Dim url As String = AbsoluteUrl(urlSegment)
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonInput As String = serializer.Serialize(input)
        Dim jsonoutput As String = ""
        Dim contentType As String = "application/json"
        Dim method As String = "POST"
        Dim exs As New List(Of Exception)
        Dim retval As U = Nothing
        If ApiHelper.SendRequest(url, jsonInput, contentType, "POST", jsonoutput, exs) Then
            retval = Newtonsoft.Json.JsonConvert.DeserializeObject(Of U)(jsonoutput)
        Else
            Stop
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

