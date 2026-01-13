Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class GalleryItemController
    Inherits _BaseController

    <HttpGet>
    <Route("api/GalleryItem/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.GalleryItem.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la GalleryItem")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/GalleryItem/FromHash")>
    Public Function FromHash(hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.GalleryItem.FromHash(hash)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la GalleryItem")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/GalleryItem/image/{guid}")>
    Public Function GetImage(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.galleryItem)
            If oImageMime Is Nothing Then
                oImageMime = BEBL.GalleryItem.ImageMime(guid)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.galleryItem, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del GalleryItem")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/GalleryItem/thumbnail/{guid}")>
    Public Function GetThumbnail(guid As Guid) As HttpResponseMessage 'To deprecate for poor performance
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.galleryItemThumbnail)
            If oImageMime Is Nothing Then
                oImageMime = BEBL.GalleryItem.ThumbnailMime(guid)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.galleryItemThumbnail, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la thumbnail del GalleryItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/GalleryItem")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOGalleryItem)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la GalleryItem")
            Else
                value.Image = oHelper.GetFileBytes("image")
                Dim oImage = LegacyHelper.ImageHelper.Converter(value.Image)
                value.Thumbnail = LegacyHelper.ImageHelper.GetThumbnailToFill(oImage, DTOGalleryItem.THUMBNAIL_WIDTH, DTOGalleryItem.THUMBNAIL_HEIGHT).Bytes
                If DAL.GalleryItemLoader.Update(value, exs) Then
                    GlobalVariables.CachedImages.Reset(value.Guid)
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.GalleryItemLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.GalleryItemLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/GalleryItem/delete")>
    Public Function Delete(<FromBody> value As DTOGalleryItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.GalleryItem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                GlobalVariables.CachedImages.Reset(value.Guid)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la GalleryItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la GalleryItem")
        End Try
        Return retval
    End Function

End Class


Public Class GalleryItemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/GalleryItems")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.GalleryItems.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les GalleryItems")
        End Try
        Return retval
    End Function

End Class
