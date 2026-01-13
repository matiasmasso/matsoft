Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WtbolSerpController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolSerp/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WtbolSerp.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WtbolSerp")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WtbolSerp")>
    Public Function Update(<FromBody> value As DTOWtbolSerp) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            If BEBL.WtbolSerp.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WtbolSerp")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WtbolSerp")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WtbolSerp/delete")>
    Public Function Delete(<FromBody> value As DTOWtbolSerp) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WtbolSerp.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WtbolSerp")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WtbolSerp")
        End Try
        Return retval
    End Function

End Class

Public Class WtbolSerpsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolSerps/{site}")>
    Public Function All(site As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSite As DTOWtbolSite = Nothing
            If site <> Nothing Then oSite = New DTOWtbolSite(site)
            Dim values = BEBL.WtbolSerps.All(oSite)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WtbolSerps")
        End Try
        Return retval
    End Function

End Class
