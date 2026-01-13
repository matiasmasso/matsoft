Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MultaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Multa/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Multa.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Multa")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Multa")>
    Public Function Update(<FromBody> value As DTOMulta) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Multa.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Multa")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Multa")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Multa/delete")>
    Public Function Delete(<FromBody> value As DTOMulta) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Multa.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Multa")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Multa")
        End Try
        Return retval
    End Function

End Class

Public Class MultasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Multas/{subjecte}")>
    Public Function All(subjecte As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSubjecte As New DTOBaseGuid(subjecte)
            Dim values = BEBL.Multas.All(oSubjecte)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Multes")
        End Try
        Return retval
    End Function
End Class
