Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MailMessageController
    Inherits _BaseController

    <HttpPost>
    <Route("api/MailMessage/Send/{user}")>
    Public Async Function Send(user As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim exs As New List(Of Exception)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                result = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                Dim oEmp = MyBase.GetEmp(oUser.Emp.Id)
                Dim json As String = oHelper.GetValue("serialized")
                Dim value = ApiHelper.Client.DeSerialize(Of DTOMailMessage)(json, exs)
                If value Is Nothing Then
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el missatge")
                Else
                    If Await BEBL.MailMessageHelper.Send(oEmp, value, exs) Then
                        result = Request.CreateResponse(HttpStatusCode.OK, value)
                    Else
                        result = MyBase.HttpErrorResponseMessage(exs, "Error al enviar el missatge")
                    End If
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al enviar el missatge")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/MailMessage/Post/{user}")>
    Public Async Function Post(user As Guid, <FromBody> value As DTOMailMessage) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                result = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                Dim oEmp = MyBase.GetEmp(oUser.Emp.Id)
                If Await BEBL.MailMessageHelper.Send(oEmp, value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK, True)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al enviar el missatge")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al enviar el missatge")
        End Try

        Return result
    End Function

End Class
