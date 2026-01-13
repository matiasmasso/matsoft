Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TaskLogController
    Inherits _BaseController

    <HttpGet>
    <Route("api/tasklog/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.TaskLog.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la tasklog")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/tasklog")>
    Public Function Update(<FromBody> value As DTOTask) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TaskLog.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la tasklog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la tasklog")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/tasklog/delete")>
    Public Function Delete(<FromBody> value As DTOTaskLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TaskLog.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la tasklog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la tasklog")
        End Try
        Return retval
    End Function

End Class

Public Class tasklogsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/tasklogs/{task}")>
    Public Function All(task As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTask As New DTOTask(task)
            Dim values = BEBL.TaskLogs.All(oTask)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les tasklogs")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/TaskLogs/delete")>
    Public Function Delete(<FromBody()> ByVal value As List(Of DTOTaskLog)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TaskLogs.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la tasklog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la tasklog")
        End Try

        Return retval
    End Function
End Class





