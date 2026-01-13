Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SessionController
    Inherits _BaseController

    <HttpGet>
    <Route("api/session/{guid}")> 'MAT.NET
    Public Function GetSession(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Session.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sessió")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/session/next/{lastToken}")> 'MAT.NET, SpvCli
    Public Function NextSession(lastToken As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.Session.NextSession(lastToken, exs)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al crear nova sessió")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/session/next")> 'MAT.NET 09/03/2021
    Public Function NextSession(<FromBody> oLastSession As DTOSession) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.Session.NextSession(oLastSession, exs)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al crear nova sessió")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/session")>
    Public Function Update(<FromBody> value As DTOSession) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            If BEBL.Session.Update(value, exs) Then
                retval = Request.CreateResponse(Of DTOSession)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Session")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Session")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/session/close/{session}")>
    Public Function Close(session As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oSession = BEBL.Session.Find(session)
            If oSession Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("no s'ha trobat cap sessió oberta per tancar")
            Else
                If BEBL.Session.Close(oSession, exs) Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al tancar la sessió")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al tancar la sessió")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/session/log/{src}/{user}")> 'MAT.NET
    Public Function Log(src As Guid, user As Guid) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = DTOBaseGuid.Opcional(Of DTOUser)(user)
            If BEBL.Session.Log(exs, src, oUser) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir la Sessió")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Sessió")
        End Try
        Return retval
    End Function

End Class
