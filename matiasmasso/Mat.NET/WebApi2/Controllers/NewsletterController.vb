Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class NewsletterController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Newsletter/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Newsletter.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Newsletter")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Newsletter")>
    Public Function Update(<FromBody> value As DTONewsletter) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Newsletter.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Newsletter")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Newsletter")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Newsletter/delete")>
    Public Function Delete(<FromBody> value As DTONewsletter) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Newsletter.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Newsletter")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Newsletter")
        End Try
        Return retval
    End Function

End Class

Public Class NewslettersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Newsletters")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Newsletters.Headers()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Newsletters")
        End Try
        Return retval
    End Function

End Class

