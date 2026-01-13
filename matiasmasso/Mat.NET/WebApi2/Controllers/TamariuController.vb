Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports Newtonsoft.Json.Linq
Public Class TamariuController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Tamariu")>
    Function Index() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Tamariu.Read()
            retval = Request.CreateResponse(Of DTO.Tamariu)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'estat de la corrent de Tamariu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Tamariu/Ok")>
    Function SetOk() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            BEBL.Tamariu.SetOk()
            retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al simular el restabliment de la llum a Tamariu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Tamariu/Ko")>
    Async Function SetKo() As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            If BEBL.Tamariu.SetKo() Then
                Await BEBL.Tamariu.NotifyKo
            End If
            retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al simular un tall se llum a Tamariu")
        End Try
        Return retval
    End Function

    'NotifyKo

    <HttpGet>
    <Route("api/Tamariu/Warn")>
    Async Function Warn() As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Await BEBL.Tamariu.NotifyKo()
            retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al avisar del tall de llum a Tamariu")
        End Try
        Return retval
    End Function
End Class
