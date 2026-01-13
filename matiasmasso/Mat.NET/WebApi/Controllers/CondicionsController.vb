Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CondicioController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Condicio/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Condicio.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Condicio")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Condicio")>
    Public Function Update(<FromBody> value As DTOCondicio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Condicio.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Condicio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Condicio")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Condicio/delete")>
    Public Function Delete(<FromBody> value As DTOCondicio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Condicio.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Condicio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Condicio")
        End Try
        Return retval
    End Function

End Class

Public Class CondicionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Condicions")>
    Public Function Headers() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Condicions.Headers()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Condicions")
        End Try
        Return retval
    End Function

End Class
