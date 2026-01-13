Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TaskScheduleController
    Inherits _BaseController

    <HttpGet>
    <Route("api/TaskSchedule/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.TaskSchedule.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la TaskSchedule")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/TaskSchedule")>
    Public Function Update(<FromBody> value As DTOTaskSchedule) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TaskSchedule.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la TaskSchedule")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la TaskSchedule")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/TaskSchedule/delete")>
    Public Function Delete(<FromBody> value As DTOTaskSchedule) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.TaskSchedule.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la TaskSchedule")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la TaskSchedule")
        End Try
        Return retval
    End Function

End Class

Public Class TaskSchedulesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/TaskSchedules/{task}")>
    Public Function All(task As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTask As New DTOTask(task)
            Dim values = BEBL.TaskSchedules.All(oTask)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les TaskSchedules")
        End Try
        Return retval
    End Function

End Class

