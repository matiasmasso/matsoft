Imports System.IO
Imports System.Net

Public Class ApiImg

    Shared Function PostRequest(Of T)(ByVal urlSegment As String, ByVal input As T) As System.Drawing.Image
        Dim url As String = String.Format("https://matiasmasso-api.azurewebsites.net/api2/{0}", urlSegment)
        Dim serializer As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim jsonInput As String = serializer.Serialize(input)
        Dim output As Image = Nothing
        Dim contentType As String = "application/json"
        Dim method As String = "POST"
        Dim exs As New List(Of Exception)
        Dim retval As Image = Nothing
        If SendRequest(url, jsonInput, contentType, "POST", output, exs) Then
            Stop
        Else
            Stop
        End If
        Return retval
    End Function

    Shared Function SendRequest(url As String, jsonInputString As String, contentType As String, method As String, ByRef Output As Image, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim lnBuffer As Byte()
        Dim lnFile As Byte()
        Dim lxRequest As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)

        Using lxResponse As HttpWebResponse = CType(lxRequest.GetResponse(), HttpWebResponse)

            Using lxBR As BinaryReader = New BinaryReader(lxResponse.GetResponseStream())

                Using lxMS As MemoryStream = New MemoryStream()
                    lnBuffer = lxBR.ReadBytes(1024)

                    While lnBuffer.Length > 0
                        lxMS.Write(lnBuffer, 0, lnBuffer.Length)
                        lnBuffer = lxBR.ReadBytes(1024)
                    End While

                    lnFile = New Byte(CInt(lxMS.Length) - 1) {}
                    lxMS.Position = 0
                    lxMS.Read(lnFile, 0, lnFile.Length)

                    Output = Image.FromStream(lxMS)
                    retval = True
                End Using
            End Using
        End Using
        Return retval
    End Function


End Class
