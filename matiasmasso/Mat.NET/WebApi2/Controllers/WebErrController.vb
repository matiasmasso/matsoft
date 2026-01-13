Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebErrController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebErr/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WebErr.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WebErr")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WebErr")>
    Public Function Update(<FromBody> value As DTOWebErr) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebErr.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WebErr")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WebErr")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/WebErr/delete")>
    Public Function Delete(<FromBody> value As DTOWebErr) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebErr.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WebErr")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WebErr")
        End Try
        Return retval
    End Function

End Class

Public Class WebErrsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebErrs")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.WebErrs.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WebErrs")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/WebErrs/reset")>
    Public Function Reset() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebErrs.Reset(exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al resetejar els WebErr")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al resetejar els WebErr")
        End Try
        Return retval
    End Function
End Class
