Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BannerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Banner/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Banner.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)

            retval.Headers.CacheControl = New Headers.CacheControlHeaderValue()
            retval.Headers.CacheControl.Public = True
            retval.Headers.CacheControl.MaxAge = New TimeSpan(30, 0, 0, 0)

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Banner")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Banner/image/{guid}")>
    Public Function GetImage(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oImageMime = GlobalVariables.CachedImages.ImageMime(guid, Defaults.ImgTypes.banner)
            If oImageMime Is Nothing Then
                oImageMime = BEBL.Banner.Image(guid)
                GlobalVariables.CachedImages.Add(guid, Defaults.ImgTypes.banner, oImageMime)
            End If
            retval = MyBase.HttpImageMimeResponseMessage(oImageMime)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la image del Banner")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Banner")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOBanner)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Banner")
            Else
                value.Image = oHelper.GetImage("image")
                If DAL.BannerLoader.Update(value, exs) Then
                    GlobalVariables.CachedImages.Reset(value.Guid)
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.BannerLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.BannerLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Banner/delete")>
    Public Function Delete(<FromBody> value As DTOBanner) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Banner.Delete(value, exs) Then
                GlobalVariables.CachedImages.Reset(value.Guid)
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Banner")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Banner")
        End Try
        Return retval
    End Function

End Class

Public Class BannersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Banners/{includeObsolets}")>
    Public Function All(includeObsolets As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Banners.All(includeObsolets = 1)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Banners")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Banners/sprite")>
    Public Function Sprite(<FromBody> guids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Banners.Sprite(guids)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Banners")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Banners/Active/{lang}")>
    Public Function Active(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.Banners.Active(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Banners")
        End Try
        Return retval
    End Function

End Class
