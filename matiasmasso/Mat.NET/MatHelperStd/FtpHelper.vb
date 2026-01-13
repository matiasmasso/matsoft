Imports System.Net

Public Class FtpHelper

    Shared Function SendFtp(exs As List(Of Exception), server As String, user As String, pwd As String, fileContents As Byte(), remotePath As String) As String
        Dim retval As String = ""
        Dim ftpPort = 21
        remotePath = If(remotePath.StartsWith("/"), "", "/") & remotePath
        Dim sFtpRequestString = String.Format("ftp://{0}:{1}{2}", server, ftpPort, remotePath)
        Dim request As FtpWebRequest = WebRequest.Create(sFtpRequestString)
        Try
            request.Method = WebRequestMethods.Ftp.UploadFile
            request.Credentials = New NetworkCredential(user, pwd)
            request.UsePassive = True '??

            request.ContentLength = fileContents.Length

            Using requestStream = request.GetRequestStream()
                requestStream.Write(fileContents, 0, fileContents.Length)
            End Using

            'Dim ftpResp As WebResponse = await(Task(Of WebResponse))
            'request.GetResponseAsync()


            Dim response As FtpWebResponse = request.GetResponse()
            retval = response.StatusDescription & response.ResponseUri.ToString
            response.Close()

        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function

End Class
