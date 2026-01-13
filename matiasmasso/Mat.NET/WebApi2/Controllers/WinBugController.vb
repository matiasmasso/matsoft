Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WinBugController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WinBug/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WinBug.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WinBug")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WinBug")>
    Public Function Update(<FromBody> value As DTOWinBug) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WinBug.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WinBug")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WinBug")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/WinBug/delete")>
    Public Function Delete(<FromBody> value As DTOWinBug) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WinBug.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WinBug")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WinBug")
        End Try
        Return retval
    End Function

End Class

Public Class WinBugsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WinBugs")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.WinBugs.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WinBugs")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WinBug/delete")>
    Public Function Delete(<FromBody> values As List(Of DTOWinBug)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WinBugs.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WinBug")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WinBug")
        End Try
        Return retval
    End Function
End Class
