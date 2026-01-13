Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace Controllers
    Public Class AppUsrLogController
        Inherits _BaseController


        <HttpPost>
        <Route("api/AppUsrLog")>
        Public Function Log(<FromBody> oRequest As DTOAppUsrLog.Request) As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oResponse = BEBL.AppUsrLog.Log(exs, oRequest)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, oResponse)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el log")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el log")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/AppUsrLog/exit/{guid}")>
        Public Function Log(guid As Guid) As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim value = BEBL.AppUsrLog.LogExit(exs, guid)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al fitxar la sortida")
                End If
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al fitxar la sortida")
            End Try
            Return retval
        End Function
    End Class

    Public Class AppUsrLogsController
        Inherits _BaseController


        <HttpGet>
        <Route("api/AppUsrLogs")>
        Public Function All() As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim values = BEBL.AppUsrLogs.All()
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els logs")
            End Try
            Return retval
        End Function

        <HttpGet>
        <Route("api/AppUsrLogs/MatNet/lastlogs")>
        Public Function LastLogs() As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim values = BEBL.AppUsrLogs.lastLogs(DTOApp.AppTypes.matNet)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els logs")
            End Try
            Return retval
        End Function


        <HttpGet>
        <Route("api/AppUsrLogs/MatNet/lastlogs/{usr}")>
        Public Function LastLogs(usr As Guid) As HttpResponseMessage
            Dim exs As New List(Of Exception)
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oUser As New DTOUser(usr)
                Dim values = BEBL.AppUsrLogs.lastLogs(DTOApp.AppTypes.matNet, oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els logs")
            End Try
            Return retval
        End Function
    End Class

End Namespace
