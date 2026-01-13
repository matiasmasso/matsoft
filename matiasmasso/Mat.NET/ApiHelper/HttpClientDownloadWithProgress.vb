Imports System.IO
Imports System.Net.Http
Imports System.Threading

Class HttpClientDownloadWithProgress

    Private ReadOnly _Url As String
    Private ReadOnly _destinationFilePath As String
    Private ReadOnly _cancellationToken As CancellationToken?
    Private _httpClient As HttpClient

    Public Delegate Sub ProgressChangedHandler(ByVal totalFileSize As Long?, ByVal totalBytesDownloaded As Long, ByVal progressPercentage As Double?)
    Public Event ProgressChanged As ProgressChangedHandler

    Public Sub New(ByVal Url As String, ByVal destinationFilePath As String, ByVal Optional cancellationToken As CancellationToken? = Nothing)
        _Url = Url
        _destinationFilePath = destinationFilePath
        _cancellationToken = cancellationToken
    End Sub

    Public Async Function StartDownload() As Task
        _httpClient = New HttpClient With {
            .Timeout = TimeSpan.FromDays(1)
        }

        Using response = Await _httpClient.GetAsync(_Url, HttpCompletionOption.ResponseHeadersRead)
            Await DownloadFileFromHttpResponseMessage(response)
        End Using
    End Function

    Private Async Function DownloadFileFromHttpResponseMessage(ByVal response As HttpResponseMessage) As Task
        response.EnsureSuccessStatusCode()
        Dim totalBytes = response.Content.Headers.ContentLength

        Using contentStream = Await response.Content.ReadAsStreamAsync()
            Await ProcessContentStream(totalBytes, contentStream)
        End Using
    End Function

    Private Async Function ProcessContentStream(ByVal totalDownloadSize As Long?, ByVal contentStream As Stream) As Task
        Dim totalBytesRead = 0L
        Dim readCount = 0L
        Dim buffer = New Byte(8191) {}
        Dim isMoreToRead = True

        Using fileStream = New FileStream(_destinationFilePath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, True)

            Do
                Dim bytesRead As Integer

                If _cancellationToken.HasValue Then
                    bytesRead = Await contentStream.ReadAsync(buffer, 0, buffer.Length, _cancellationToken.Value)
                Else
                    bytesRead = Await contentStream.ReadAsync(buffer, 0, buffer.Length)
                End If

                If bytesRead = 0 Then
                    isMoreToRead = False
                    Continue Do
                End If

                Await fileStream.WriteAsync(buffer, 0, bytesRead)
                totalBytesRead += bytesRead
                readCount += 1
                If readCount Mod 10 = 0 Then TriggerProgressChanged(totalDownloadSize, totalBytesRead)
            Loop While isMoreToRead
        End Using

        TriggerProgressChanged(totalDownloadSize, totalBytesRead)
    End Function

    Private Sub TriggerProgressChanged(ByVal totalDownloadSize As Long?, ByVal totalBytesRead As Long)
        Dim progressPercentage As Double? = Nothing
        If totalDownloadSize.HasValue Then progressPercentage = Math.Round(CDbl(totalBytesRead) / totalDownloadSize.Value * 100, 2)
        RaiseEvent ProgressChanged(totalDownloadSize, totalBytesRead, progressPercentage)
    End Sub

    Public Sub Dispose()
        _httpClient?.Dispose()
    End Sub
End Class
