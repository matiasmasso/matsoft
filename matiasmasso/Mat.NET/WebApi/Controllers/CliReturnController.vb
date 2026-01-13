Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CliReturnController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CliReturn/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CliReturn.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CliReturn")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CliReturn")>
    Public Function Update(<FromBody> value As DTOCliReturn) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CliReturn.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CliReturn")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CliReturn")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CliReturn/delete")>
    Public Function Delete(<FromBody> value As DTOCliReturn) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CliReturn.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CliReturn")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CliReturn")
        End Try
        Return retval
    End Function

End Class

Public Class CliReturnsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CliReturns")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.CliReturns.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CliReturns")
        End Try
        Return retval
    End Function

End Class

