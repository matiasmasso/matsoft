Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DefaultImageController
    Inherits _BaseController

    <HttpGet>
    <Route("api/DefaultImage/{id}")>
    Public Function Find(id As Defaults.ImgTypes) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DefaultImage.Find(id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la DefaultImage")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DefaultImage/image/{id}")>
    Public Function GetIcon(id As Defaults.ImgTypes) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DefaultImage.Image(id)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del DefaultImage")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/DefaultImage")>
    Public Function Update(<FromBody> value As DTODefaultImage) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.DefaultImage.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la DefaultImage")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la DefaultImage")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/DefaultImage/upload")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTODefaultImage)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la DefaultImage")
            Else
                value.image = oHelper.GetImage("image")

                If DAL.DefaultImageLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.DefaultImageLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.DefaultImageLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/DefaultImage/delete")>
    Public Function Delete(<FromBody> value As DTODefaultImage) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.DefaultImage.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la DefaultImage")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la DefaultImage")
        End Try
        Return retval
    End Function

End Class

