Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebPageAliasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebPageAlias/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WebPageAlias.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WebPageAlias")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WebPageAlias/FromUrl")>
    Public Function FromUrl(<FromBody> oWebPageAlias As DTOWebPageAlias) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WebPageAlias.FromUrl(oWebPageAlias)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WebPageAlias")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WebPageAlias")>
    Public Function Update(<FromBody> value As DTOWebPageAlias) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebPageAlias.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WebPageAlias")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WebPageAlias")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/WebPageAlias/delete")>
    Public Function Delete(<FromBody> value As DTOWebPageAlias) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebPageAlias.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WebPageAlias")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WebPageAlias")
        End Try
        Return retval
    End Function

End Class

Public Class WebPagesAliasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebPagesAlias")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.WebPagesAlias.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WebPagesAlias")
        End Try
        Return retval
    End Function

End Class
