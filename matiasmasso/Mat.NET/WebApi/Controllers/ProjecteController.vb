Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProjecteController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Projecte/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Projecte.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Projecte")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Projecte")>
    Public Function Update(<FromBody> value As DTOProjecte) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Projecte.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Projecte")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Projecte")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Projecte/delete")>
    Public Function Delete(<FromBody> value As DTOProjecte) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Projecte.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Projecte")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Projecte")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Projecte/items/{projecte}")>
    Public Function Items(projecte As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProjecte As New DTOProjecte(projecte)
            Dim values = BEBL.Projecte.Items(oProjecte)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Projectes")
        End Try
        Return retval
    End Function

End Class

Public Class ProjectesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Projectes")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Projectes.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Projectes")
        End Try
        Return retval
    End Function

End Class
