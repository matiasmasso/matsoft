Public Module Api
    Property ApiClient As ApiHelper.Client

    Public Sub Initialize(rootUrl As String, Optional localPort As Integer = 0, Optional useLocalApi As Boolean = False)
        ApiClient = New ApiHelper.Client(rootUrl, localPort, useLocalApi)
        AddHandler ApiClient.onBug, AddressOf LogBug
    End Sub

    Private Sub LogBug(sender As Object, e As MatEventArgs)
        Dim oWinbug = DTOWinBug.Factory(e.Argument)
        Dim exs As New List(Of Exception)
        WinBug.UpdateSync(exs, oWinbug)
    End Sub

    Public Sub UseLocalApi(value As Boolean)
        ApiClient.UseLocalApi = value
        MmoUrl.useLocalApi = value
    End Sub

    Public Function UseLocalApi() As Boolean
        Return ApiClient.UseLocalApi
    End Function

    Public Async Function Fetch(Of T)(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of T)
        Dim headers As New Dictionary(Of String, String)
        Return Await ApiClient.GetRequest(Of T)(exs, headers, urlSegments)
    End Function

    Public Async Function GetRequest(Of T)(exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As Task(Of T)
        Return Await ApiClient.GetRequest(Of T)(exs, headers, urlSegments)
    End Function

    Public Async Function GetBinaryRequest(exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As Task(Of Byte())
        Return Await ApiClient.GetBinaryRequest(exs, headers, urlSegments)
    End Function

    Public Async Function FetchBinary(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        Return Await ApiClient.FetchBinary(exs, urlSegments)
    End Function

    Public Function FetchBinarySync(exs As List(Of Exception), ParamArray urlSegments() As String) As Byte()
        Return ApiClient.FetchBinarySync(exs, urlSegments)
    End Function

    Public Async Function FetchImage(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        Return Await ApiClient.FetchImage(exs, urlSegments)
    End Function

    Public Async Function downloadImage(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        Return Await ApiClient.DownloadSystemDrawingImage(value, exs, urlSegments)
    End Function


    Public Async Function DownloadImageStream(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        Dim retval As Byte() = Await ApiClient.DownloadSystemDrawingImage(value, exs, urlSegments)
        'Dim ms As New System.IO.MemoryStream(oByteArray)
        'Dim oImage = System.Drawing.Image.FromStream(ms)
        Return retval
    End Function


    Public Function FetchImageBytesSync(exs As List(Of Exception), ParamArray urlSegments() As String) As Byte()
        Return ApiClient.FetchImageBytesSync(exs, urlSegments)
    End Function

    Public Function FetchImageSync(exs As List(Of Exception), ParamArray urlSegments() As String) As Byte()
        Return ApiClient.FetchImageSync(exs, urlSegments)
    End Function


    Public Function FetchSync(Of T)(exs As List(Of Exception), ParamArray urlSegments() As String) As T
        Return ApiClient.FetchSync(Of T)(exs, urlSegments)
    End Function


    Public Async Function Update(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Return Await ApiClient.Update(Of T)(value, exs, urlSegments)
    End Function

    Public Async Function Update(Of T, U)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of U)
        Return Await ApiClient.Update(Of T, U)(value, exs, urlSegments)
    End Function

    Public Function UpdateSync(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Return ApiClient.UpdateSync(Of T)(value, exs, urlSegments)
    End Function


    Public Async Function Delete(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Return Await ApiClient.Delete(Of T)(value, exs, urlSegments)
    End Function

    Public Function DeleteSync(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Return ApiClient.DeleteSync(Of T)(value, exs, urlSegments)
    End Function

    Public Async Function Upload(value As ApiHelper.MultipartHelper, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Return Await ApiClient.Upload(value, exs, urlSegments)
    End Function

    Public Async Function Upload(Of T)(value As ApiHelper.MultipartHelper, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of T)
        Return Await ApiClient.Upload(Of T)(value, exs, urlSegments)
    End Function

    Public Function UploadSync(value As ApiHelper.MultipartHelper, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Return ApiClient.UploadSync(value, exs, urlSegments)
    End Function

    Public Async Function Execute(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Return Await ApiClient.Execute(exs, urlSegments)
    End Function


    Public Function ExecuteSync(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Return ApiClient.ExecuteSync(Of T)(value, exs, urlSegments)
    End Function

    Public Function ExecuteSync(Of T, U)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As U
        Return ApiClient.ExecuteSync(Of T, U)(value, exs, urlSegments)
    End Function

    Public Async Function Execute(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Return Await ApiClient.Execute(Of T)(value, exs, urlSegments)
    End Function

    Public Async Function ExecuteBinary(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        Return Await ApiClient.ExecuteBinary(Of T)(value, exs, urlSegments)
    End Function

    Public Async Function Execute(Of T, U)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of U)
        Return Await ApiClient.Execute(Of T, U)(value, exs, urlSegments)
    End Function
    Public Async Function PostRequest(Of T, U)(value As T, exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As Task(Of U)
        Return Await ApiClient.PostRequest(Of T, U)(value, exs, headers, urlSegments)
    End Function

    Public Function PostRequestSync(Of T, U)(value As T, exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As U
        Return ApiClient.PostRequestSync(Of T, U)(value, exs, headers, urlSegments)
    End Function

    Public Async Function PutRequest(Of T)(exs As List(Of Exception), value As T, headers As Dictionary(Of String, String), url As String) As Task(Of Boolean)
        Return Await ApiClient.PutRequest(Of T)(exs, value, headers, url)
    End Function

    Public Function DeSerialize(Of T)(json As String, exs As List(Of Exception)) As T
        Return ApiHelper.Client.DeSerialize(Of T)(json, exs)
    End Function

    Public Function Serialize(Value As Object, exs As List(Of Exception)) As String
        Return ApiHelper.Client.Serialize(Value, exs) 'Newtonsoft
    End Function

    Public Function Serialize(exs As List(Of Exception), Value As Object, Optional oReferenceLoopHandling As Newtonsoft.Json.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore) As String
        Return ApiHelper.Client.Serialize(exs, Value, oReferenceLoopHandling) 'system.web no protegeix contra referencies circulars
    End Function

    Public Function Url(ParamArray urlSegments() As String) As String
        Return ApiClient.ApiUrl(urlSegments)
    End Function




    Public Function LocalApiUrl(ParamArray urlSegments() As String) As String
        Return ApiClient.LocalApiUrl(urlSegments)
    End Function

    Public Function ApiUrl(ParamArray urlSegments() As String) As String
        Return ApiClient.ApiUrl(urlSegments)
    End Function

    Public Function SendRequest(url As String, jsonInputString As String, contentType As String, method As String, ByRef jsonOutputString As String, exs As List(Of Exception)) As Boolean
        Return ApiHelper.Client.SendRequest(url, jsonInputString, contentType, method, jsonOutputString, exs)
    End Function

End Module