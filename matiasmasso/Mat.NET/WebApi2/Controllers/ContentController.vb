Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContentController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Content/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Content.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el content")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Content/SearchByUrl")>
    Public Function SearchByUrl(<FromBody> urlFriendlySegment As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Content.SearchByUrl(urlFriendlySegment)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el contingut")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Content")>
    Public Function Update(<FromBody> value As DTOContent) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Content.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Content")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Content")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Content/delete")>
    Public Function Delete(<FromBody> value As DTOContent) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Content.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Content")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Content")
        End Try
        Return retval
    End Function

End Class

Public Class ContentsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Contents")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Contents.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Contents")
        End Try
        Return retval
    End Function

End Class
