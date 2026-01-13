Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IncidenciaCodController
    Inherits _BaseController

    <HttpGet>
    <Route("api/IncidenciaCod/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.IncidenciaCod.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la IncidenciaCod")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/IncidenciaCod")>
    Public Function Update(<FromBody> value As DTOIncidenciaCod) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.IncidenciaCod.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la IncidenciaCod")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la IncidenciaCod")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/IncidenciaCod/delete")>
    Public Function Delete(<FromBody> value As DTOIncidenciaCod) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.IncidenciaCod.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la IncidenciaCod")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la IncidenciaCod")
        End Try
        Return retval
    End Function

End Class

Public Class IncidenciaCodsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/IncidenciaCods/{cod}")>
    Public Function All(cod As DTOIncidenciaCod.cods) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.IncidenciaCods.All(cod)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les IncidenciaCods")
        End Try
        Return retval
    End Function

End Class
