Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AreaRegioController

    Inherits _BaseController

    <HttpGet>
    <Route("api/AreaRegio/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.AreaRegio.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AreaRegio")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AreaRegio")>
    Public Function Update(<FromBody> value As DTOAreaRegio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AreaRegio.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la AreaRegio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la AreaRegio")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AreaRegio/delete")>
    Public Function Delete(<FromBody> value As DTOAreaRegio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AreaRegio.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la AreaRegio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la AreaRegio")
        End Try
        Return retval
    End Function

End Class

Public Class AreaRegiosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AreaRegions/{country}")>
    Public Function All(country As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim values = BEBL.AreaRegions.All(oCountry)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les AreaRegios")
        End Try
        Return retval
    End Function

End Class
