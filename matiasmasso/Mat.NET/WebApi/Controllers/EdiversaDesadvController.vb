Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaDesadvController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaDesadv/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaDesadv.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la EdiversaDesadv")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/EdiversaDesadv")>
    Public Function Update(<FromBody> value As DTOEdiversaDesadv) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaDesadv.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaDesadv")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaDesadv")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/EdiversaDesadv/delete")>
    Public Function Delete(<FromBody> value As DTOEdiversaDesadv) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaDesadv.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la EdiversaDesadv")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la EdiversaDesadv")
        End Try
        Return retval
    End Function

End Class

Public Class EdiversaDesadvsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaDesadvs")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EdiversaDesadvs.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiversaDesadvs")
        End Try
        Return retval
    End Function

End Class
