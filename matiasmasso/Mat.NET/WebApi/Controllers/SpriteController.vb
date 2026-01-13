Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SpriteController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Sprite/{Base64Hash}")>
    Public Function Find(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim value = BEBL.Sprite.Find(hash)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sprite")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Sprite/image/{Base64Hash}")>
    Public Function GetIcon(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim value = BEBL.Sprite.Find(hash)
            retval = MyBase.HttpImageResponseMessage(value.Image)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del Sprite")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Sprite/upload")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOSprite)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Sprite")
            Else
                value.Image = oHelper.GetImage("image")

                If DAL.SpriteLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el sprite a DAL.SpriteLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.SpriteLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Sprite/delete")>
    Public Function Delete(<FromBody> value As DTOSprite) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Sprite.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Sprite")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Sprite")
        End Try
        Return retval
    End Function

End Class

