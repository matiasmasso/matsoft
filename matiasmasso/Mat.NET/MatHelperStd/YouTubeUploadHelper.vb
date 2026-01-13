Imports System.IO
Imports System.Reflection
Imports System.Threading
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports Google.Apis.Upload
Imports Google.Apis.YouTube.v3
Imports Google.Apis.YouTube.v3.Data


Public Class YouTubeUploadHelper



    Public Async Function Upload(oStream As IO.Stream, filename As String) As Task

        Dim oClientSecrets = Me.ClientSecrets()
        Dim oScopes = {YouTubeService.Scope.YoutubeUpload}
        Dim username = "info@matiasmasso.es"
        Dim oCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(oClientSecrets, oScopes, username, CancellationToken.None).Result

        Dim oYoutubeService = New YouTubeService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = oCredential,
                .ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            })

        Dim video = New Video()
        video.Snippet = New VideoSnippet()
        video.Snippet.Title = "Default Video Title"
        video.Snippet.Description = "Default Video Description"
        video.Snippet.Tags = New String() {"89EED8A6-D759-4B82-8D6A-274D6A0EAAD5", "89EED8A6-D759-4B82-8D6A-274D6A0EAAD0"}
        video.Snippet.CategoryId = "22"
        video.Status = New VideoStatus()
        video.Status.PrivacyStatus = "unlisted"
        Dim filePath = filename

        Dim videosInsertRequest = oYoutubeService.Videos.Insert(video, "snippet,status", oStream, "video/*")
        AddHandler videosInsertRequest.ProgressChanged, AddressOf videosInsertRequest_ProgressChanged
        AddHandler videosInsertRequest.ResponseReceived, AddressOf videosInsertRequest_ResponseReceived
        Await videosInsertRequest.UploadAsync()
    End Function
    Public Async Function Upload(filename As String) As Task

        Dim oClientSecrets = Me.ClientSecrets()
        Dim oScopes = {YouTubeService.Scope.YoutubeUpload}
        Dim username = "info@matiasmasso.es"
        Dim oCredential As UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(oClientSecrets, oScopes, username, CancellationToken.None).Result

        Dim oYoutubeService = New YouTubeService(New BaseClientService.Initializer() With {
                .HttpClientInitializer = oCredential,
                .ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            })

        Dim video = New Video()
        video.Snippet = New VideoSnippet()
        video.Snippet.Title = "Default Video Title"
        video.Snippet.Description = "Default Video Description"
        video.Snippet.Tags = New String() {"tag1", "tag2"}
        video.Snippet.CategoryId = "22"
        video.Status = New VideoStatus()
        video.Status.PrivacyStatus = "unlisted"
        Dim filePath = filename

        Using fileStream = New FileStream(filePath, FileMode.Open)
            Dim videosInsertRequest = oYoutubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*")
            AddHandler videosInsertRequest.ProgressChanged, AddressOf videosInsertRequest_ProgressChanged
            AddHandler videosInsertRequest.ResponseReceived, AddressOf videosInsertRequest_ResponseReceived
            Await videosInsertRequest.UploadAsync()
        End Using
    End Function

    Private Sub videosInsertRequest_ProgressChanged(ByVal progress As Google.Apis.Upload.IUploadProgress)
        Select Case progress.Status
            Case UploadStatus.Uploading
                Console.WriteLine("{0} bytes sent.", progress.BytesSent)
            Case UploadStatus.Failed
                Console.WriteLine("An error prevented the upload from completing." & vbLf & "{0}", progress.Exception)
        End Select
    End Sub

    Private Sub videosInsertRequest_ResponseReceived(ByVal video As Video)
        Console.WriteLine("Video id '{0}' was successfully uploaded.", video.Id)
    End Sub

    Private Function ClientSecrets() As ClientSecrets
        Dim retval As New ClientSecrets With {
            .ClientId = "497879715486-1sb5q7nv8evu3bmutdmf7634fu19bs4k.apps.googleusercontent.com",
            .ClientSecret = "qob-2VFWiNxhxmQFgL4mJG8V"
        }
        Return retval
    End Function
End Class

