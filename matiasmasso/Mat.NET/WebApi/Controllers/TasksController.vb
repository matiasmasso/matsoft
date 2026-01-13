Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class TasksController
    Inherits _BaseController

    <HttpGet>
    <Route("api/tasks")>
    Public Function GetTasks() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Tasks.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les tasques")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/tasks/due")>
    Public Function DueTasks() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Tasks.DueTasks()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les tasques")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/task/{guid}")>
    Public Function GetTask(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Task.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir la tasca")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/taskByCod/{cod}")>
    Public Function GetTaskByCod(cod As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Task.Find(cod)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir la tasca")
        End Try
        Return retval
    End Function

    <Route("api/tasks/execute/{user}")>
    <HttpGet>
    Public Async Function ExecuteTasks(user As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)

            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage(exs, "usuari desconegut")
            Else
                oUser.Emp = MyBase.GetEmp(oUser.Emp.Id)
                BEBL.Contact.Load(oUser.Emp.Org)
                Dim values = Await BEBL.Tasks.Execute(oUser, exs)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(Of List(Of DTOTask))(HttpStatusCode.OK, values)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al executar la tasca")
                End If
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Task")
        End Try
        Return retval
    End Function


    <Route("api/task/execute/{task}/{user}")>
    <HttpGet>
    Public Async Function ExecuteTask(task As Guid, user As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)

            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage(exs, "usuari desconegut")
            Else

                Dim oTask = BEBL.Task.Find(task)
                If oTask Is Nothing Then
                    retval = MyBase.HttpErrorResponseMessage(exs, "tasca desconeguda")
                Else
                    oUser.Emp = MyBase.GetEmp(oUser.Emp.Id)
                    BEBL.Contact.Load(oUser.Emp.Org)
                    oTask = Await BEBL.Task.Execute(exs, oTask, oUser)
                    If exs.Count = 0 Then
                        retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                    Else
                        retval = MyBase.HttpErrorResponseMessage(exs, "error al executar la tasca")
                    End If
                End If
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al executar la tasca")
        End Try
        Return retval
    End Function


    <Route("api/task/log")>
    <HttpPost>
    Public Function Log(<FromBody()> ByVal value As DTOTask) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Task.Log(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Task")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Task")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/task")>
    Public Function Update(<FromBody> value As DTOTask) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Task.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Task")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Task")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/task/delete")>
    Public Function Delete(<FromBody()> ByVal value As DTOTask) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Task.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Task")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Task")
        End Try
        Return retval
    End Function


End Class
