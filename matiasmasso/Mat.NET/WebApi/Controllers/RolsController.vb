Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RolController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Rol/{id}")>
    Public Function Find(id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Rol.Find(id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error el llegir la Rol")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Rol")>
    Public Function Update(<FromBody> value As DTORol) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Rol.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error el desar la Rol")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el Rol")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Rol/delete")>
    Public Function Delete(<FromBody> value As DTORol) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Rol.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Rol")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Rol")
        End Try
        Return retval
    End Function

End Class

Public Class RolsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Rols")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Rols.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Rols")
        End Try
        Return retval
    End Function

End Class
