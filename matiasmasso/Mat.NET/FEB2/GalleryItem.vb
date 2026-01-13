Public Class GalleryItem
    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOGalleryItem)
        Return Await Api.Fetch(Of DTOGalleryItem)(exs, "GalleryItem", oGuid.ToString())
    End Function

    Shared Function ImageUrl(oGuid As Guid) As String
        Return FEB2.Api.ApiUrl("GalleryItem/Image", oGuid.ToString())
    End Function

    Shared Async Function Image(oGuid As Guid, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "GalleryItem/Image", oGuid.ToString())
    End Function

    Shared Async Function Thumbnail(exs As List(Of Exception), oGuid As Guid) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "GalleryItem/Thumbnail", oGuid.ToString)
    End Function

    Shared Function Load(ByRef oGalleryItem As DTOGalleryItem, exs As List(Of Exception)) As Boolean
        If Not oGalleryItem.IsLoaded And Not oGalleryItem.IsNew Then
            Dim pGalleryItem = Api.FetchSync(Of DTOGalleryItem)(exs, "GalleryItem", oGalleryItem.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOGalleryItem)(pGalleryItem, oGalleryItem, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function


    Shared Async Function Update(value As DTOGalleryItem, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Image IsNot Nothing Then
                oMultipart.AddFileContent("image", value.Image)
            End If
            retval = Await Api.Upload(oMultipart, exs, "GalleryItem")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oGalleryItem As DTOGalleryItem, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOGalleryItem)(oGalleryItem, exs, "GalleryItem")
    End Function
End Class

Public Class GalleryItems

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOGalleryItem))
        Return Await Api.Fetch(Of List(Of DTOGalleryItem))(exs, "GalleryItems")
    End Function

End Class
