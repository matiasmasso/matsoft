Public Class GalleryItem

    Shared Function Find(oGuid As Guid) As DTOGalleryItem
        Dim retval As DTOGalleryItem = GalleryItemLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function ImageMime(oGuid As Guid) As ImageMime
        Return GalleryItemLoader.ImageMime(oGuid)
    End Function

    Shared Function ThumbnailMime(oGuid As Guid) As ImageMime
        Return GalleryItemLoader.ThumbnailMime(oGuid)
    End Function

    Shared Function FromHash(hash As String) As DTOGalleryItem
        Return GalleryItemLoader.FromHash(hash)
    End Function

    Shared Function Update(oGalleryItem As DTOGalleryItem, exs As List(Of Exception)) As Boolean
        Return GalleryItemLoader.Update(oGalleryItem, exs)
    End Function

    Shared Function Delete(oGalleryItem As DTOGalleryItem, exs As List(Of Exception)) As Boolean
        Return GalleryItemLoader.Delete(oGalleryItem, exs)
    End Function

End Class



Public Class GalleryItems
    Shared Function All() As List(Of DTOGalleryItem)
        Dim retval As List(Of DTOGalleryItem) = GalleryItemsLoader.All()
        Return retval
    End Function
End Class