Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports Newtonsoft.Json.Linq

Public Class JsonLogController
    Inherits _BaseController

    <HttpGet>
    <Route("api/JsonLog/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.JsonLog.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el missatge")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/JsonLog")>
    Public Function Update(<FromBody> value As DTOJsonLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.JsonLog.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el missatge")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el missatge")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/JsonLog/mailbox")>
    Public Async Function Mailbox(<FromBody> value As JObject) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            If Await BEBL.JsonLog.Procesa(exs, value, oEmp) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "se han producido errores al subir el mensaje")
                Try
                    'Dim body = TextHelper.Html(value.ToString())
                    'Await BEBL.MailMessageHelper.MailAdmin("error al processar api/jsonlog/mailbox", body)
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "se han producido errores al subir el mensaje")
            Try
                Dim body = value.ToString()
                Dim unused = BEBL.MailMessageHelper.MailAdmin("error al processar api/jsonlog/mailbox", body)
            Catch ex2 As Exception

            End Try
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/JsonLog/delete")>
    Public Function Delete(<FromBody> value As DTOJsonLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.JsonLog.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el missatge")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el missatge")
        End Try
        Return retval
    End Function

End Class


Public Class JsonLogsController
    Inherits _BaseController

    <HttpPost>
    <Route("api/JsonLogs")>
    Public Function All(<FromBody> searchKey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.JsonLogs.All(searchKey)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els logs json")
        End Try
        Return retval
    End Function
End Class