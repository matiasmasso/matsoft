Public Class MediaResource
    Inherits _FeblBase

    Shared Async Function FromHash(exs As List(Of Exception), hash As String) As Task(Of DTOMediaResource)
        Dim retval = Await Api.Fetch(Of DTOMediaResource)(exs, "MediaResource/fromHash", CryptoHelper.UrlFriendlyBase64(hash))
        Return retval
    End Function

    Shared Async Function Find(exs As List(Of Exception), guid As Guid) As Task(Of DTOMediaResource)
        Dim retval = Await Api.Fetch(Of DTOMediaResource)(exs, "MediaResource", guid.ToString())
        Return retval
    End Function

    Shared Async Function Thumbnail(exs As List(Of Exception), oMediaResource As DTOMediaResource) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "MediaResource/thumbnail", oMediaResource.Guid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oMediaResource As DTOMediaResource, Optional LoadThumbnail As Boolean = False) As Boolean
        'Dim urlFriendlyHash = CryptoHelper.UrlFriendlyBase64(oMediaResource.Hash)
        If Not oMediaResource.IsNew Then
            Dim pMediaResource = Api.FetchSync(Of DTOMediaResource)(exs, "MediaResource", oMediaResource.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOMediaResource)(pMediaResource, oMediaResource, exs)
                'Dim sBase64Hash = CryptoHelper.UrlFriendlyBase64(oMediaResource.Hash)
                If LoadThumbnail Then
                    oMediaResource.Thumbnail = Api.FetchImageSync(exs, "MediaResource/thumbnail", oMediaResource.Guid.ToString())
                End If
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTOMediaResource, exs As List(Of Exception)) As Task(Of Boolean)
        'Called by Mat.Net, sendd data + file to the web server to persist the file and to reforward the data to the api
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("thumbnail", value.Thumbnail)
            oMultipart.AddFileContent("stream", value.Stream)
            Dim oWebServerUrl = DTOWebDomain.Factory("es").Url("/MediaResource/upload")
            'oWebServerUrl = "https://localhost:44332/MediaResource/upload" ' temporarily for testing local website
            Dim oResult = Await Api.Upload(Of DTOApiResult)(oMultipart, exs, oWebServerUrl)
            If oResult Is Nothing Then
                exs.Add(New Exception("error de sistema"))
            Else
                If oResult.Success Then
                    retval = True
                Else
                    exs.AddRange(oResult.exs)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Async Function UpdateData(value As DTOMediaResource, exs As List(Of Exception)) As Task(Of Boolean)
        'called by the web server once successfully persisted the file, to forward the data to the Api
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("thumbnail", value.Thumbnail)
            'oMultipart.AddFileContent("stream", value.Stream)
            retval = Await Api.Upload(oMultipart, exs, "MediaResource")
        End If
        Return retval
    End Function

    Shared Function UpdateSync(value As DTOMediaResource, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("thumbnail", value.Thumbnail)
            oMultipart.AddFileContent("stream", value.Stream)
            retval = Api.UploadSync(oMultipart, exs, "MediaResource")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oMediaResource As DTOMediaResource, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOMediaResource)(oMediaResource, exs, "MediaResource")
    End Function

    Shared Function DeleteSync(oMediaResource As DTOMediaResource, exs As List(Of Exception)) As Boolean
        Return Api.DeleteSync(Of DTOMediaResource)(oMediaResource, exs, "MediaResource")
    End Function

    Shared Function Url(oMediaResource As DTOMediaResource, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "MediaResource/media", oMediaResource.Guid.ToString())
    End Function

    Shared Function ThumbnailUrl(oMediaResource As DTOMediaResource, Optional AbsoluteUrl As Boolean = False)
        Return UrlHelper.Factory(AbsoluteUrl, "MediaResource/thumbnail", oMediaResource.Guid.ToString())
    End Function
End Class

Public Class MediaResources
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTOMediaResource))
        Return Await Api.Fetch(Of List(Of DTOMediaResource))(exs, "MediaResources", oProduct.Guid.ToString())
    End Function

    Shared Async Function AllWithThumbnails(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTOMediaResource))
        Dim items = Await Api.Fetch(Of List(Of DTOMediaResource))(exs, "MediaResources", oProduct.Guid.ToString())
        Dim retval = Await ThumbnailsLoaded(exs, items)
        Return retval
    End Function

    Shared Async Function ProductSpecificWithThumbnails(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTOMediaResource))
        Dim items = Await Api.Fetch(Of List(Of DTOMediaResource))(exs, "MediaResources/ProductSpecific", oProduct.Guid.ToString)
        Dim retval = Await ThumbnailsLoaded(exs, items)
        Return retval
    End Function

    Private Shared Async Function ThumbnailsLoaded(exs As List(Of Exception), items As List(Of DTOMediaResource)) As Threading.Tasks.Task(Of List(Of DTOMediaResource))
        Dim retval As New List(Of DTOMediaResource)
        If items IsNot Nothing AndAlso items.Count > 0 Then
            'Dim sHashes = items.Select(Function(x) x.Hash).ToList()
            Dim oGuids = items.Select(Function(x) x.Guid).ToList()
            Dim oSpriteImage = Await Api.downloadImage(Of List(Of Guid))(oGuids, exs, "MediaResources/Sprite")
            Dim idx As Integer = 0
            For Each item In items
                item.Thumbnail = LegacyHelper.SpriteHelper.Extract(oSpriteImage, idx, items.Count)
                retval.Add(item)
                idx += 1
            Next
        End If
        Return retval
    End Function

    Shared Async Function Delete(exs As List(Of Exception), values As List(Of DTOMediaResource)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOMediaResource))(values, exs, "MediaResources")
    End Function

    Shared Function DownloadAllText(items As List(Of DTOMediaResource), oLang As DTOLang) As String
        Dim dWeight As Double = items.Sum(Function(x) x.Length)
        Dim sWeight As String = MediaHelper.LengthFormatted(dWeight)
        Dim esp As String = String.Format("descargar todo ({0})", sWeight)
        Dim cat As String = String.Format("descarregar-ho tot ({0})", sWeight)
        Dim eng As String = String.Format("download all ({0})", sWeight)
        Dim retval As String = oLang.Tradueix(esp, cat, eng)
        Return retval
    End Function

    Shared Function DownloadAllUrl(oProduct As DTOProduct, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = UrlHelper.Dox(AbsoluteUrl, DTODocFile.Cods.ZipGalleryDownloads, "product", oProduct.Guid.ToString())
        Return retval
    End Function

    Shared Function ExistsFromProductOrChildrenSync(exs As List(Of Exception), oProduct As DTOProduct) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "MediaResources/ExistsFromProductOrChildren", oProduct.Guid.ToString())
    End Function

    Shared Async Function GetMissingResources(exs As List(Of Exception), existingFilenames As List(Of String)) As Task(Of DTOMediaResource.Collection)
        Return Await Api.Execute(Of List(Of String), DTOMediaResource.Collection)(existingFilenames, exs, "MediaResources/MissingResources")
    End Function
End Class


