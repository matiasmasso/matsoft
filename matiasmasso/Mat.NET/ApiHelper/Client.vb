Imports System.Net
Imports System.Net.Http
Imports Newtonsoft.Json

Public Class Client
    Property RootUrl As String
    Property LocalPort As Integer
    Property UseLocalApi As Boolean

    Shared Property Verbose As Boolean = True

    Public Event onBug(sender As Object, e As MatEventArgs)

    Public Sub New(rootUrl As String, localPort As Integer, useLocalApi As Boolean)
        _RootUrl = rootUrl
        _LocalPort = localPort
        _UseLocalApi = useLocalApi
    End Sub

    Private Sub Do_Verbose(url As String, len As Integer, proc As String)
        If Verbose Then
            If len > 1000000 Then
                Dim msg = String.Format("High Api download {0:0,000} {1} {2}", len, proc, url)
                RaiseEvent onBug(Me, New MatEventArgs(msg))
            End If
        End If

    End Sub

    Public Async Function GetRequest(Of T)(exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As Task(Of T)
        Dim url As String = ApiUrl(urlSegments)
        Return Await GetRequest(Of T)(exs, headers, url)
    End Function

    Shared Async Function GetRequest(Of T)(exs As List(Of Exception), headers As Dictionary(Of String, String), url As String) As Task(Of T)
        Dim retval As T
        Try

            Dim handler As New HttpClientHandler() With {
            .AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
        }

            Using client As New HttpClient(handler)
                For Each entry As KeyValuePair(Of String, String) In headers
                    client.DefaultRequestHeaders.Add(entry.Key, entry.Value)
                Next

                Using response As HttpResponseMessage = Await client.GetAsync(url)
                    If response.IsSuccessStatusCode Then

                        Dim json = Await response.Content.ReadAsStringAsync()

                        'Dim byteArray = Await response.Content.ReadAsByteArrayAsync()
                        'Dim json = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length)

                        retval = JsonConvert.DeserializeObject(json, GetType(T), JsonSettings(exs))
                        'Do_Verbose(url, json.Length, "GetRequest")

                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using

            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function



    Public Async Function FetchImage(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        'HttpGet For Large File In the Right Way

        Dim retval As Byte() = Nothing
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Using response As HttpResponseMessage = Await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
                    If response.IsSuccessStatusCode Then
                        retval = Await response.Content.ReadAsByteArrayAsync()
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Public Async Function downloadImage(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        'HttpGet For Large File In the Right Way

        Dim retval As Byte() = Nothing
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")

        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Using response As Net.Http.HttpResponseMessage = Await client.PostAsync(url, stringContent)
                    If response.IsSuccessStatusCode Then
                        retval = Await response.Content.ReadAsByteArrayAsync()
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Async Function DownloadSystemDrawingImage(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        'HttpGet For Large File In the Right Way

        Dim retval As Byte() = Nothing
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")

        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Using response As Net.Http.HttpResponseMessage = Await client.PostAsync(url, stringContent)
                    If response.IsSuccessStatusCode Then
                        retval = Await response.Content.ReadAsByteArrayAsync()
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Public Function FetchImageSync(exs As List(Of Exception), ParamArray urlSegments() As String) As Byte()
        Dim retval As Byte() = Nothing
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Dim response = client.GetAsync(url).Result

                If response.IsSuccessStatusCode Then
                    If response.Content.Headers.ContentLength > 0 Then
                        retval = response.Content.ReadAsByteArrayAsync.Result
                        Do_Verbose(url, response.Content.Headers.ContentLength, "Fetch")

                    End If
                Else
                    Dim errMsg = response.Content.ReadAsStringAsync().Result
                    For Each line In errMsg.Split(vbCrLf)
                        Dim ex As New Exception(line.Trim)
                        exs.Add(ex)
                    Next
                End If
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Function FetchImageBytesSync(exs As List(Of Exception), ParamArray urlSegments() As String) As Byte()
        Dim retval As Byte() = Nothing
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Dim response = client.GetAsync(url).Result

                If response.IsSuccessStatusCode Then
                    If response.Content.Headers.ContentLength > 0 Then
                        retval = response.Content.ReadAsByteArrayAsync.Result

                    End If
                Else
                    Dim errMsg = response.Content.ReadAsStringAsync().Result
                    For Each line In errMsg.Split(vbCrLf)
                        Dim ex As New Exception(line.Trim)
                        exs.Add(ex)
                    Next
                End If
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Public Async Function GetBinaryRequest(exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As Task(Of Byte())
        'HttpGet For Large File In the Right Way (http://www.tugberkugurlu.com/archive/efficiently-streaming-large-http-responses-with-httpclient)
        Dim retval As Byte() = Nothing
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                If headers IsNot Nothing Then
                    For Each entry As KeyValuePair(Of String, String) In headers
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value)
                    Next
                End If
                Using response As HttpResponseMessage = Await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
                    If response.IsSuccessStatusCode Then
                        retval = Await response.Content.ReadAsByteArrayAsync()
                        Do_Verbose(url, response.Content.Headers.ContentLength, "Fetch Binary")
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Async Function FetchBinary(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        'HttpGet For Large File In the Right Way (http://www.tugberkugurlu.com/archive/efficiently-streaming-large-http-responses-with-httpclient)
        Dim retval As Byte() = Nothing
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Using response As HttpResponseMessage = Await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
                    If response.IsSuccessStatusCode Then
                        retval = Await response.Content.ReadAsByteArrayAsync()
                        Do_Verbose(url, response.Content.Headers.ContentLength, "Fetch Binary")
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Function FetchBinarySync(exs As List(Of Exception), ParamArray urlSegments() As String) As Byte()
        'HttpGet For Large File In the Right Way (http://www.tugberkugurlu.com/archive/efficiently-streaming-large-http-responses-with-httpclient)
        Dim retval As Byte() = Nothing
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Dim response = client.GetAsync(url).Result

                If response.IsSuccessStatusCode Then
                    If response.StatusCode = Net.HttpStatusCode.NoContent Then
                        retval = Nothing
                    Else
                        retval = response.Content.ReadAsByteArrayAsync().Result
                    End If
                    Do_Verbose(url, response.Content.Headers.ContentLength, "Fetch Binary Sync")
                Else
                    Dim errMsg = response.Content.ReadAsStringAsync().Result
                    For Each line In errMsg.Split(vbCrLf)
                        Dim ex As New Exception(line.Trim)
                        exs.Add(ex)
                    Next
                End If
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Public Function FetchSync(Of T)(exs As List(Of Exception), ParamArray urlSegments() As String) As T
        Dim retval As T
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Dim response = client.GetAsync(url).Result

                If response.IsSuccessStatusCode Then
                    Dim json = response.Content.ReadAsStringAsync().Result
                    retval = JsonConvert.DeserializeObject(json, GetType(T), JsonSettings(exs))

                    Do_Verbose(url, response.Content.Headers.ContentLength, "Fetch Sync")

                Else
                    Dim errMsg = response.Content.ReadAsStringAsync().Result
                    For Each line In errMsg.Split(vbCrLf)
                        Dim ex As New Exception(line.Trim)
                        exs.Add(ex)
                    Next
                End If

            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval

    End Function
    Public Async Function Fetch(Of T)(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of T)
        Dim retval As T
        Try
            Dim url As String = ApiUrl(urlSegments)
            Using client As New System.Net.Http.HttpClient
                Dim response = Await client.GetAsync(url)

                If response.IsSuccessStatusCode Then
                    Dim json = response.Content.ReadAsStringAsync().Result
                    retval = JsonConvert.DeserializeObject(json, GetType(T), JsonSettings(exs))

                    Do_Verbose(url, response.Content.Headers.ContentLength, "Fetch Async")

                Else
                    Dim errMsg = response.Content.ReadAsStringAsync().Result
                    For Each line In errMsg.Split(vbCrLf)
                        Dim ex As New Exception(line.Trim)
                        exs.Add(ex)
                    Next
                End If

            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval

    End Function


    Public Async Function Update(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Return Await Execute(Of T)(value, exs, urlSegments)
    End Function

    Public Async Function Update(Of T, U)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of U)
        Return Await Execute(Of T, U)(value, exs, urlSegments)
    End Function

    Public Function UpdateSync(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Return ExecuteSync(Of T)(value, exs, urlSegments)
    End Function

    Public Async Function Delete(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Dim url As String = ApiUrl(urlSegments.ToArray())
        Return Await DeleteAsync(url, exs)
    End Function

    Public Async Function Delete(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Dim urlsegmentsList = urlSegments.ToList
        urlsegmentsList.Add("delete")
        Return Await Execute(Of T)(value, exs, urlsegmentsList.ToArray)
    End Function

    Public Function DeleteSync(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Dim urlsegmentsList = urlSegments.ToList
        urlsegmentsList.Add("delete")
        Return ExecuteSync(Of T)(value, exs, urlsegmentsList.ToArray)
    End Function

    Public Async Function Execute(exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Dim url As String = ApiUrl(urlSegments)
        Return Await PostAsync(url, exs)
    End Function

    Public Function ExecuteSync(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Dim url As String = ApiUrl(urlSegments)
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Return PostSync(url, exs, stringContent)
    End Function

    Public Function ExecuteSync(Of T, U)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As U
        Dim url As String = ApiUrl(urlSegments)
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Return PostSync(Of U)(url, exs, stringContent)
    End Function

    Public Async Function Execute(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Dim url As String = ApiUrl(urlSegments)
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Return Await PostAsync(url, exs, stringContent)
    End Function

    Public Async Function ExecuteBinary(Of T)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Byte())
        Dim url As String = ApiUrl(urlSegments)
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Return Await PostBinaryAsync(url, exs, stringContent)
    End Function

    Public Async Function Execute(Of T, U)(value As T, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of U)
        Dim url As String = ApiUrl(urlSegments)
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Dim headers As New Dictionary(Of String, String)
        Return Await PostAsync(Of U)(url, exs, stringContent, headers)
    End Function

    Public Async Function PostRequest(Of T, U)(value As T, exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As Task(Of U)
        Dim url As String = ApiUrl(urlSegments)
        Return Await PostRequest(Of T, U)(value, exs, headers, url)
    End Function

    Public Function PostRequestSync(Of T, U)(value As T, exs As List(Of Exception), headers As Dictionary(Of String, String), ParamArray urlSegments() As String) As U
        Dim url As String = ApiUrl(urlSegments)
        Return PostRequestSync(Of T, U)(value, exs, headers, url)
    End Function

    Shared Async Function PostRequest(Of T, U)(value As T, exs As List(Of Exception), headers As Dictionary(Of String, String), url As String) As Task(Of U)
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Return Await PostAsync(Of U)(url, exs, stringContent, headers)
    End Function

    Public Function PostRequestSync(Of T, U)(value As T, exs As List(Of Exception), headers As Dictionary(Of String, String), url As String) As U
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Return PostSync(Of U)(url, exs, stringContent, headers)
    End Function

    Public Async Function PutRequest(Of T)(exs As List(Of Exception), value As T, headers As Dictionary(Of String, String), url As String) As Task(Of Boolean)
        Dim jsonInput = JsonConvert.SerializeObject(value, JsonSettings(exs))
        Dim stringContent As New Net.Http.StringContent(jsonInput, Text.Encoding.UTF8, "application/json")
        Return Await PutAsync(Of T)(url, exs, stringContent, headers)
    End Function

    Public Async Function Upload(value As MultipartHelper, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of Boolean)
        Dim url As String = ApiUrl(urlSegments) '.Replace("/api/", "/") '.Replace("http://", "https://")
        Dim oMultipartFormContent = value.FormContent
        Dim retval = Await PostAsync(url, exs, oMultipartFormContent)
        Return retval
    End Function

    Public Async Function Upload(Of T)(value As MultipartHelper, exs As List(Of Exception), ParamArray urlSegments() As String) As Task(Of T)
        Dim url As String = ApiUrl(urlSegments)
        Dim headers As New Dictionary(Of String, String)
        Return Await PostAsync(Of T)(url, exs, value.FormContent, headers)
    End Function

    Public Function UploadSync(value As MultipartHelper, exs As List(Of Exception), ParamArray urlSegments() As String) As Boolean
        Dim url As String = ApiUrl(urlSegments)
        Return PostSync(url, exs, value.FormContent)
    End Function

    Public Function PostSync(url As String, exs As List(Of Exception), Optional Content As HttpContent = Nothing) As Boolean
        Dim retval As Boolean
        Try
            Using client As New System.Net.Http.HttpClient
                Using response As Net.Http.HttpResponseMessage = client.PostAsync(url, Content).Result
                    If response.IsSuccessStatusCode Then
                        retval = True
                        Do_Verbose(url, response.Content.Headers.ContentLength, "Post Sync")
                    Else
                        Dim errMsg = response.Content.ReadAsStringAsync().Result
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Function PostSync(Of U)(url As String, exs As List(Of Exception), Optional Content As HttpContent = Nothing, Optional headers As Dictionary(Of String, String) = Nothing) As U
        Dim retval As U
        Try
            Using client As New System.Net.Http.HttpClient
                If headers IsNot Nothing Then
                    For Each entry As KeyValuePair(Of String, String) In headers
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value)
                    Next
                End If
                Using response As Net.Http.HttpResponseMessage = client.PostAsync(url, Content).Result
                    If response.IsSuccessStatusCode Then
                        Dim json = response.Content.ReadAsStringAsync().Result
                        retval = JsonConvert.DeserializeObject(json, GetType(U), JsonSettings(exs))

                        Do_Verbose(url, response.Content.Headers.ContentLength, "Post Sync")

                    Else
                        Dim errMsg = response.Content.ReadAsStringAsync().Result
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Async Function PostAsync(url As String, exs As List(Of Exception), Optional Content As Object = Nothing) As Task(Of Boolean)
        Dim retval As Boolean
        Try
            Using client As New System.Net.Http.HttpClient
                Using response As Net.Http.HttpResponseMessage = Await client.PostAsync(url, Content)
                    If response.IsSuccessStatusCode Then
                        retval = True

                        Do_Verbose(url, response.Content.Headers.ContentLength, "Post Async")

                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        If errMsg = "" Then
                            Dim ex As New Exception(response.ReasonPhrase)
                            exs.Add(ex)
                        Else
                            For Each line In errMsg.Split(vbCrLf)
                                Dim ex As New Exception(line.Trim)
                                exs.Add(ex)
                            Next
                        End If
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Async Function DeleteAsync(url As String, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Try
            Using client As New System.Net.Http.HttpClient
                Using response As Net.Http.HttpResponseMessage = Await client.DeleteAsync(url)
                    If response.IsSuccessStatusCode Then
                        retval = True

                        Do_Verbose(url, response.Content.Headers.ContentLength, "Post Async")

                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        If errMsg = "" Then
                            Dim ex As New Exception(response.ReasonPhrase)
                            exs.Add(ex)
                        Else
                            For Each line In errMsg.Split(vbCrLf)
                                Dim ex As New Exception(line.Trim)
                                exs.Add(ex)
                            Next
                        End If
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Async Function PostAsync(Of T)(url As String, exs As List(Of Exception), Optional Content As Object = Nothing, Optional headers As Dictionary(Of String, String) = Nothing) As Task(Of T)
        Dim retval As T
        Try
            Using client As New System.Net.Http.HttpClient
                If headers IsNot Nothing Then
                    For Each entry As KeyValuePair(Of String, String) In headers
                        client.DefaultRequestHeaders.Add(entry.Key, entry.Value)
                    Next
                End If

                Using response As Net.Http.HttpResponseMessage = Await client.PostAsync(url, Content)
                    If response.IsSuccessStatusCode Then
                        Dim json = Await response.Content.ReadAsStringAsync()
                        retval = JsonConvert.DeserializeObject(json, GetType(T), JsonSettings(exs))
                        'MatHelperStd.FileSystemHelper.SaveTextToFile(json, "C:\json.txt", exs)
                        'Do_Verbose(url, response.Content.Headers.ContentLength, "Post async")

                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Async Function PutAsync(Of T)(url As String, exs As List(Of Exception), Optional Content As Object = Nothing, Optional headers As Dictionary(Of String, String) = Nothing) As Task(Of Boolean)
        Dim retval As Boolean
        Try
            Using client As New System.Net.Http.HttpClient
                For Each entry As KeyValuePair(Of String, String) In headers
                    client.DefaultRequestHeaders.Add(entry.Key, entry.Value)
                Next

                Using response As Net.Http.HttpResponseMessage = Await client.PutAsync(url, Content)
                    If response.IsSuccessStatusCode Then
                        'Dim json = Await response.Content.ReadAsStringAsync()
                        'retval = JsonConvert.DeserializeObject(json, GetType(T), JsonSettings(exs))
                        'Do_Verbose(url, response.Content.Headers.ContentLength, "Post async")
                        retval = True
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Public Async Function PostBinaryAsync(url As String, exs As List(Of Exception), Optional Content As Object = Nothing) As Task(Of Byte())
        Dim retval As Byte() = Nothing
        Try
            Using client As New System.Net.Http.HttpClient
                Using response As Net.Http.HttpResponseMessage = Await client.PostAsync(url, Content)
                    If response.IsSuccessStatusCode Then
                        retval = Await response.Content.ReadAsByteArrayAsync()
                        Do_Verbose(url, response.Content.Headers.ContentLength, "PostBinaryAsync")
                    Else
                        Dim errMsg = Await response.Content.ReadAsStringAsync()
                        For Each line In errMsg.Split(vbCrLf)
                            Dim ex As New Exception(line.Trim)
                            exs.Add(ex)
                        Next
                    End If
                End Using
            End Using

        Catch ex As Exception
            exs.Add(ex)
        End Try
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

            'Do_Verbose(url, jsonOutputString.Length, "SendRequest")

            reader.Close()
            oResponseStream.Close()
            retval = True
        Catch ex As Exception
            exs.Add(ex)
        End Try

        Return retval
    End Function


    Public Function ApiUrl(ParamArray urlSegments() As String) As String
        Dim retval As String = ""
        If urlSegments.Count = 1 AndAlso urlSegments(0).StartsWith("http") Then
            retval = urlSegments(0)
        Else
            retval = Url("api", urlSegments)
        End If
        Return retval
    End Function

    Public Function LocalApiUrl(ParamArray urlSegments() As String) As String
        _UseLocalApi = True
        Return Url("api", urlSegments)
    End Function

    Public Function IsCore() As Boolean
        Dim retval = _LocalPort = 7018
        If _RootUrl = "https://matgenapi.azurewebsites.net" Then retval = True
        Return retval
    End Function

    Private Function Url(qualifier As String, ParamArray urlSegments() As String) As String
        Dim retval As String
        Dim urlsegment = String.Join("/", urlSegments)

        If _UseLocalApi Then
            _UseLocalApi = True
            If String.IsNullOrEmpty(qualifier) Then
                'retval = String.Format("http://localhost:{0}/{1}", _LocalPort, urlsegment)
                retval = String.Format("https://localhost:{0}/{1}", _LocalPort, urlsegment)
            Else
                If IsCore() Then
                    retval = String.Format("https://localhost:{0}/{1}", _LocalPort, urlsegment)
                Else
                    'retval = String.Format("http://localhost:{0}/{1}/{2}", _LocalPort, qualifier, urlsegment)
                    retval = String.Format("https://localhost:{0}/{1}/{2}", _LocalPort, qualifier, urlsegment)
                End If
            End If
        Else
            If String.IsNullOrEmpty(qualifier) Or IsCore() Then
                retval = String.Format("{0}/{1}", _RootUrl, urlsegment)
            Else
                retval = String.Format("{0}/{1}/{2}", _RootUrl, qualifier, urlsegment)
            End If
        End If
        Return retval
    End Function

    Shared Function DeSerialize(Of T)(json As String, exs As List(Of Exception)) As T
        Return JsonConvert.DeserializeObject(json, GetType(T), JsonSettings(exs))
    End Function

    Shared Function Serialize(Value As Object, exs As List(Of Exception)) As String
        Dim retval As String = ""
        Try
            'Dim defaultSerializer = New JavaScriptSerializer() (no protegeix contra referencies circulars)
            'retval = defaultSerializer.Serialize(Value)


            retval = JsonConvert.SerializeObject(Value, JsonSettings(exs))
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

    Shared Function Serialize(exs As List(Of Exception), Value As Object, Optional oReferenceLoopHandling As ReferenceLoopHandling = ReferenceLoopHandling.Ignore) As String
        Dim retval As String = ""
        Try
            'Dim defaultSerializer = New JavaScriptSerializer() (no protegeix contra referencies circulars)
            'retval = defaultSerializer.Serialize(Value)


            retval = JsonConvert.SerializeObject(Value, JsonSettings(exs, oReferenceLoopHandling))
        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function


    Shared Function JsonSettings(exs As List(Of Exception), Optional oReferenceLoopHandling As ReferenceLoopHandling = ReferenceLoopHandling.Ignore) As JsonSerializerSettings
        Dim retval As New JsonSerializerSettings()
        With retval
            .ReferenceLoopHandling = oReferenceLoopHandling
            .NullValueHandling = NullValueHandling.Ignore
            .TypeNameHandling = TypeNameHandling.Auto
            .Formatting = Formatting.Indented
            '.Converters.Add(New Converters.IsoDateTimeConverter With {.DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss"})
            '.Converters.Add(New Converters.IsoDateTimeConverter With {.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss"})
            '.DateFormatHandling = DateFormatHandling.IsoDateFormat
            '.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ss"
            .[Error] = Sub(sender, args)
                           If System.Diagnostics.Debugger.IsAttached Then
                               System.Diagnostics.Debugger.Break()
                           End If
                           Dim sb As New Text.StringBuilder
                           sb.AppendLine("Error de serialització:")
                           If args IsNot Nothing AndAlso args.ErrorContext IsNot Nothing AndAlso args.ErrorContext.OriginalObject IsNot Nothing Then
                               sb.AppendLine("en object: " & args.ErrorContext.OriginalObject.ToString())
                           End If
                           exs.Add(New Exception(sb.ToString))
                           exs.Add(New Exception(args.ErrorContext.Path))
                       End Sub
        End With
        Return retval
    End Function

    '----a optimitzar 
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
