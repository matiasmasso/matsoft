Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EventoController
    Inherits _BaseController

    <HttpPost>
    <Route("api/Evento")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOEvento)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el evento")
            Else
                value.Image265x150 = oHelper.GetImage("Image265x150")

                If DAL.NoticiaLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el Evento")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.NoticiaLoader")
        End Try

        Return result
    End Function

End Class


Public Class EventosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Eventos/{user}")>
    Public Function Headers(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As DTOUser = Nothing
            If user <> Nothing Then oUser = BEBL.User.Find(user)
            Dim values = BEBL.Eventos.Headers(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Eventos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Eventos/nextEvento/{user}")>
    Public Function nextEvento(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As DTOUser = Nothing
            If user <> Nothing Then oUser = BEBL.User.Find(user)
            Dim value = BEBL.Eventos.NextEvento(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Eventos")
        End Try
        Return retval
    End Function

End Class

