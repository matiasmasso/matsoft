Imports System.IO
Imports System.Net
Imports Newtonsoft.Json

Public Class DirtyTables

    Public Shared Function GetValues() As List(Of DirtyTableModel)
        Return SQLHelper.DirtyTables()
    End Function

    Public Shared Function KeepAlive(oTaskLog As DTOTaskLog) As DTOTaskLog
        Dim dataString As String
        Dim vm As List(Of DirtyTableModel) = GetValues()
        Using client As New WebClient()
            dataString = JsonConvert.SerializeObject(vm)
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json")
            client.UploadString(New Uri("https://beta.matiasmasso.es/culture/keepalive"), "POST", dataString)
        End Using
        Dim retval As DTOTaskLog = oTaskLog
        retval.ResultMsg = dataString
        Return retval
    End Function

    Public Shared Function KeepAliveOld(oTaskLog As DTOTaskLog) As DTOTaskLog
        Dim retval As DTOTaskLog = oTaskLog
        Dim FchFrom = Now

        Dim exs As New List(Of Exception)
        Dim webClient As New System.Net.WebClient
        Dim result As String = webClient.DownloadString("https://beta.matiasmasso.es/culture/keepalive")
        Dim FchTo = Now
        Dim interval = FchTo - FchFrom
        retval.ResultMsg = String.Format("{0} {1:N0} ms", result, interval.TotalMilliseconds)
        Return retval
    End Function


    Public Function Ping(ByVal url As String, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim request As WebRequest = WebRequest.Create(url)
            Dim encoding = System.Text.UTF8Encoding.UTF8
            Using response As WebResponse = request.GetResponse()
                Using reader As New StreamReader(response.GetResponseStream(), encoding)
                    '_MailMessage.Body = reader.ReadToEnd()
                End Using
            End Using
            retval = True
        Catch ex As Exception
            exs.Add(New Exception(String.Format("Error al descarregar el cos del missatge de {0}", url)))
            exs.Add(ex)
        End Try
        Return retval
    End Function
End Class
