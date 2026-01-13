Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebMenuItemController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebMenuItem/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WebMenuItem.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WebMenuItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WebMenuItem")>
    Public Function Update(<FromBody> value As DTOWebMenuItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebMenuItem.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WebMenuItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WebMenuItem")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WebMenuItem/delete")>
    Public Function Delete(<FromBody> value As DTOWebMenuItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebMenuItem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WebMenuItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WebMenuItem")
        End Try
        Return retval
    End Function

End Class

