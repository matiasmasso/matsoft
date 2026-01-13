Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TpvLogController
    Inherits _BaseController

    <HttpGet>
    <Route("api/TpvLog/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.TpvLog.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la TpvLog")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/TpvLog/FromOrder/{order}")>
    Public Function FromOrder(order As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.TpvLog.FromOrder(order)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la TpvLog")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/TpvLog")>
    Public Function Update(<FromBody> value As DTOTpvLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TpvLog.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la TpvLog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la TpvLog")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/TpvLog/delete")>
    Public Function Delete(<FromBody> value As DTOTpvLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TpvLog.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la TpvLog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la TpvLog")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/TpvLog/bookRequest")>
    Public Function bookRequest(<FromBody> value As DTOTpvLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TpvLog.BookRequest(value, exs) Then
                retval = Request.CreateResponse(Of DTOTpvLog)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la TpvLog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la TpvLog")
        End Try
        Return retval
    End Function

End Class

Public Class TpvLogsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/TpvLogs")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.TpvLogs.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les TpvLogs")
        End Try
        Return retval
    End Function

End Class
