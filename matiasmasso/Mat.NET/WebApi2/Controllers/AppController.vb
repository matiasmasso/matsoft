Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AppController
    Inherits _BaseController

    <HttpGet>
    <Route("api/App/{id}")>
    Public Function Find(id As DTOApp.AppTypes) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.App.Find(id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la App")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/App")>
    Public Function Update(<FromBody> value As DTOApp) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.App.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la App")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la App")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/App/delete")>
    Public Function Delete(<FromBody> value As DTOApp) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.App.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la App")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la App")
        End Try
        Return retval
    End Function

End Class

Public Class AppsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Apps")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Apps.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Apps")
        End Try
        Return retval
    End Function

End Class
