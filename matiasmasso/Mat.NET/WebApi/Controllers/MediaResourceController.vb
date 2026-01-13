Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MediaResourceController
    Inherits _BaseController

    <HttpGet>
    <Route("api/MediaResource/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.MediaResource.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/MediaResource/fromHash/{urlFriendlyHash}")>
    Public Function FromHash(urlFriendlyHash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(urlFriendlyHash)
            Dim value = BEBL.MediaResource.FromHash(hash)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/MediaResource/thumbnail/{guid}")>
    Public Function GetThumbnail(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMediaResource As New DTOMediaResource(guid)
            Dim value = BEBL.MediaResource.Thumbnail(oMediaResource)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la thumbnail del MediaResource")
        End Try
        Return retval
    End Function

    '<HttpGet>
    '<Route("api/MediaResource/thumbnail/{urlFriendlyHash}")>
    'Public Function GetThumbnail(urlFriendlyHash As String) As HttpResponseMessage
    '    Dim retval As HttpResponseMessage = Nothing
    '    Try
    '        Dim hash = CryptoHelper.FromUrFriendlyBase64(urlFriendlyHash)
    '        Dim oMediaResource As New DTOMediaResource(hash)
    '        Dim value = BEBL.MediaResource.Thumbnail(oMediaResource)
    '        retval = MyBase.HttpImageResponseMessage(value)
    '    Catch ex As Exception
    '        retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la thumbnail del MediaResource")
    '    End Try
    '    Return retval
    'End Function

    <HttpPost, ValidateInput(False)>
    <Route("api/MediaResource")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOMediaResource)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la MediaResource")
            Else
                value.Thumbnail = oHelper.GetImage("thumbnail")
                'value.Stream = oHelper.GetFileBytes("stream") // the stream has been already saved in the website filesystem

                If BEBL.MediaResource.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el MediaResource a DAL.MediaResourceLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.MediaResourceLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/MediaResource/delete")>
    Public Function Delete(<FromBody> value As DTOMediaResource) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.MediaResource.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la MediaResource")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la MediaResource")
        End Try
        Return retval
    End Function

End Class

Public Class MediaResourcesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/MediaResources/{product}")>
    Public Function All(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.MediaResources.FromProductOrParent(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les MediaResources")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/MediaResources/Sprite")>
    Public Function Sprite(<FromBody> guids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSpriteImage = BEBL.MediaResources.Sprite(guids)
            retval = MyBase.HttpImageResponseMessage(oSpriteImage)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les MediaResources")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/MediaResources/productSpecific/{product}")>
    Public Function productSpecific(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = New DTOProduct(product)
            Dim values = BEBL.MediaResources.ProductSpecific(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les MediaResources")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/MediaResources/delete")>
    Public Function Delete(<FromBody> values As DTOMediaResource.Collection) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.MediaResources.Delete(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar las MediaResource")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar las MediaResource")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/MediaResources/ExistsFromProductOrChildren/{product}")>
    Public Function ExistsFromProductOrChildren(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim value = BEBL.MediaResources.ExistsFromProductOrChildren(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les MediaResources")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/MediaResources/MissingResources")>
    Public Function MissingResources(<FromBody> filenames As List(Of String)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.MediaResources.MissingResources(filenames)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els recursos a la base de dades")
        End Try
        Return retval
    End Function

End Class
