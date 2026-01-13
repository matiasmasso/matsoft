Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CondicioCapitolController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CondicioCapitol/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CondicioCapitol.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CondicioCapitol")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CondicioCapitol")>
    Public Function Update(<FromBody> value As DTOCondicio.Capitol) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CondicioCapitol.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CondicioCapitol")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CondicioCapitol")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CondicioCapitol/delete")>
    Public Function Delete(<FromBody> value As DTOCondicio.Capitol) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CondicioCapitol.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CondicioCapitol")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CondicioCapitol")
        End Try
        Return retval
    End Function

End Class

Public Class CondicioCapitolsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CondicioCapitols/{Condicio}")>
    Public Function All(Condicio As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCondicio As New DTOCondicio(Condicio)
            Dim values = BEBL.CondicioCapitols.Headers(oCondicio)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CondicioCapitols")
        End Try
        Return retval
    End Function

End Class
