Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ComputerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Computer/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Computer.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Computer")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Computer")>
    Public Function Update(<FromBody> value As DTOComputer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Computer.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Computer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el Computer")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Computer/delete")>
    Public Function Delete(<FromBody> value As DTOComputer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Computer.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Computer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Computer")
        End Try
        Return retval
    End Function

End Class

Public Class ComputersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Computers")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Computers.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Computers")
        End Try
        Return retval
    End Function

End Class
