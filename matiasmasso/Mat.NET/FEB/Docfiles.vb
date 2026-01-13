
Public Class DocFile

    Shared Async Function Find(hash As String, exs As List(Of Exception)) As Task(Of DTODocFile)
        Return Await Api.Fetch(Of DTODocFile)(exs, "docfile", hash)
    End Function
    Shared Async Function FindBySha256(sha256 As String, exs As List(Of Exception)) As Task(Of DTODocFile)
        Return Await Api.Fetch(Of DTODocFile)(exs, "docfile/bySha256", CryptoHelper.UrlFriendlyBase64(sha256))
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oDocFile As DTODocFile, Optional LoadStream As Boolean = False, Optional LoadThumbnail As Boolean = False) As Boolean
        Dim pDocfile = Api.FetchSync(Of DTODocFile)(exs, "Docfile", oDocFile.Hash)
        If exs.Count = 0 Then
            DTOBaseGuid.CopyPropertyValues(Of DTODocFile)(pDocfile, oDocFile, exs)
            If LoadStream Then
                oDocFile.Stream = Api.FetchBinarySync(exs, "docfile/Stream", oDocFile.Hash)
            End If
            If LoadThumbnail Then
                oDocFile.Thumbnail = Api.FetchImageSync(exs, "Docfile/thumbnail", oDocFile.Hash)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function FindWithStream(hash As String, exs As List(Of Exception)) As Task(Of DTODocFile)
        Dim retval As DTODocFile = Await Find(hash, exs)
        If exs.Count = 0 Then
            If retval IsNot Nothing Then
                retval.Stream = Await Api.FetchBinary(exs, "docfile/Stream", hash)
            End If
        End If
        Return retval
    End Function

    Shared Async Function StreamOrDownload(oDocfile As DTODocFile, exs As List(Of Exception)) As Task(Of Byte())
        Dim retval As Byte() = oDocfile.Stream
        If oDocfile.Stream Is Nothing Then
            retval = Await Api.FetchBinary(exs, "docfile/Stream", oDocfile.Hash)
        End If
        Return retval
    End Function

    Shared Function FindSync(hash As String) As DTODocFile
        Dim exs As New List(Of Exception)
        Return Api.FetchSync(Of DTODocFile)(exs, "docfile", hash)
    End Function


    Shared Function Exists(sHash As String, ByRef DtFch As Date) As Boolean
        Dim retval As Boolean
        Dim oDocfile = FindSync(sHash)
        If oDocfile IsNot Nothing Then
            retval = True
            DtFch = oDocfile.FchCreated
        End If
        Return retval
    End Function

    Shared Async Function Thumbnail(exs As List(Of Exception), hash As String, Optional width As Integer = 0) As Task(Of Byte())
        Dim retval As Byte() = Nothing
        If width = 0 Then
            retval = Await Api.FetchImage(exs, "Docfile/thumbnail", hash)
        Else
            retval = Await Api.FetchImage(exs, "Docfile/thumbnail", hash, width)
        End If
        Return retval
    End Function

    Shared Async Function Upload(value As DTODocFile, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("thumbnail", value.Thumbnail)
            oMultipart.AddFileContent("stream", value.Stream)
            retval = Await Api.Upload(oMultipart, exs, "Docfile")
        End If
        Return retval
    End Function

    Shared Async Function Delete(value As DTODocFile, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval = Await Api.Delete(Of DTODocFile)(value, exs, "DocFile")
        Return retval
    End Function

    Shared Async Function Log(oDocFile As DTODocFile, oUser As DTOUser, exs As List(Of Exception)) As Task(Of Boolean)
        Dim oGuid As Guid = Guid.Empty
        If oUser IsNot Nothing Then oGuid = oUser.Guid
        Return Await Api.Fetch(Of Boolean)(exs, "docfile/log", oGuid.ToString, oDocFile.Hash)
    End Function

    Shared Async Function Srcs(oDocFile As DTODocFile, exs As List(Of Exception)) As Task(Of List(Of DTODocFileSrc))
        Return Await Api.Fetch(Of List(Of DTODocFileSrc))(exs, "docfile/srcs", CryptoHelper.UrlFriendlyBase64(oDocFile.Hash))
    End Function

    Shared Async Function Logs(oDocFile As DTODocFile, exs As List(Of Exception)) As Task(Of List(Of DTODocFileLog))
        Return Await Api.Fetch(Of List(Of DTODocFileLog))(exs, "docfile/logs", CryptoHelper.UrlFriendlyBase64(oDocFile.Hash))
    End Function

    Shared Function ThumbnailUrl(oDocfile As DTODocFile, Optional AbsoluteUrl As Boolean = True) As String
        Dim retval = oDocfile.ThumbnailUrl(AbsoluteUrl)
        Return retval
    End Function

    Shared Function DownloadUrl(oDocfile As DTODocFile, oDomain As DTOWebDomain) As String
        Dim retval = oDomain.Url("doc", DTODocFile.Cods.download, CryptoHelper.StringToHexadecimal(oDocfile.hash))
        Return retval
    End Function

    Shared Function DownloadUrl(oDocfile As DTODocFile, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oDocfile IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.download, CryptoHelper.StringToHexadecimal(oDocfile.Hash))
        End If
        Return retval
    End Function



    Shared Function HexHash(oDocFile As DTODocFile) As String
        Dim retval As String = CryptoHelper.StringToHexadecimal(oDocFile.Hash)
        Return retval
    End Function

    Shared Function FileNameOrDefault(oDocFile As DTODocFile) As String
        Dim retval As String = oDocFile.Filename
        If String.IsNullOrEmpty(retval) Then
            retval = DocFile.HexHash(oDocFile) & "." & oDocFile.Mime.ToString
        End If
        Return retval
    End Function


End Class
Public Class Docfiles

    Shared Async Function All(iYear As Integer, exs As List(Of Exception)) As Task(Of List(Of DTODocFile))
        Return Await Api.Fetch(Of List(Of DTODocFile))(exs, "Docfiles", iYear)
    End Function


End Class
