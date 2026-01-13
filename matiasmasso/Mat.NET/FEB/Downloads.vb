Public Class Download

    Shared Async Function Find(guid As Guid, exs As List(Of Exception)) As Task(Of DTOProductDownload)
        Dim retval = Await Api.Fetch(Of DTOProductDownload)(exs, "Download", guid.ToString())
        If exs.Count = 0 Then
            If retval.DocFile IsNot Nothing Then
                Dim FriendlyHash = CryptoHelper.UrlFriendlyBase64(retval.DocFile.Hash)
                Dim exs2 As New List(Of Exception) 'si no hi ha imatge continua sense ella
                retval.DocFile.Thumbnail = Await Api.FetchImage(exs2, "docfile/thumbnail", FriendlyHash)
            End If
        End If
        Return retval
    End Function

    Shared Async Function Upload(value As DTOProductDownload, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Download")
        End If
        Return retval
    End Function

    Shared Async Function Delete(value As DTOProductDownload, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval = Await Api.Delete(Of DTOProductDownload)(value, exs, "Download")
        Return retval
    End Function

End Class
Public Class Downloads

    Shared Function FromProductOrParentSync(exs As List(Of Exception), oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oLang As DTOLang = Nothing) As List(Of DTOProductDownload)
        'Shared Function FromProductOrParentSync(exs As List(Of Exception), oProduct As DTOProduct, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oLang As DTOLang = Nothing) As List(Of DTOProductDownload)
        'Sync per calls des de Razor
        Dim retval = Api.FetchSync(Of List(Of DTOProductDownload))(exs, "downloads/fromProductOrParent", oProduct.Guid.ToString())
        If oLang IsNot Nothing Then
            retval = retval.Where(Function(x) x.Lang Is Nothing OrElse oLang.Equals(x.Lang)).ToList
        End If
        If Not IncludeObsoletos Then
            retval = retval.Where(Function(x) x.Obsoleto = False).ToList
        End If
        If OnlyConsumerEnabled Then
            retval = retval.Where(Function(x) x.PublicarAlConsumidor = True).ToList
        End If
        'If oSrc <> DTOProductDownload.Srcs.NotSet Then
        ' retval = retval.Where(Function(x) x.Src = oSrc).ToList
        ' End If
        Return retval
    End Function

    Shared Async Function FromProductOrParent(exs As List(Of Exception), oProduct As DTOProduct, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oLang As DTOLang = Nothing) As Task(Of List(Of DTOProductDownload))
        'Shared Async Function FromProductOrParent(exs As List(Of Exception), oProduct As DTOProduct, Optional oSrc As DTOProductDownload.Srcs = DTOProductDownload.Srcs.NotSet, Optional ByVal IncludeObsoletos As Boolean = True, Optional ByVal OnlyConsumerEnabled As Boolean = False, Optional oLang As DTOLang = Nothing) As Task(Of List(Of DTOProductDownload))
        Dim retval = Await Api.Fetch(Of List(Of DTOProductDownload))(exs, "downloads/fromProductOrParent", oProduct.Guid.ToString())
        If oLang IsNot Nothing Then
            Select Case oLang.id
                Case DTOLang.Ids.CAT, DTOLang.Ids.ENG
                    retval = retval.Where(Function(x) x.lang Is Nothing OrElse x.lang.Equals(DTOLang.ESP) OrElse oLang.Equals(x.lang)).ToList
                Case Else
                    retval = retval.Where(Function(x) x.lang Is Nothing OrElse oLang.Equals(x.lang)).ToList
            End Select
        End If
        If Not IncludeObsoletos Then
            retval = retval.Where(Function(x) x.Obsoleto = False).ToList
        End If
        If OnlyConsumerEnabled Then
            retval = retval.Where(Function(x) x.PublicarAlConsumidor = True).ToList
        End If
        'If oSrc <> DTOProductDownload.Srcs.NotSet Then
        'retval = retval.Where(Function(x) x.Src = oSrc).ToList
        'End If
        Return retval
    End Function

End Class
