Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MailingLogController
    Inherits _BaseController

    <HttpPost>
    <Route("api/MailingLog")>
    Public Function MailingLog(<FromBody()> ByVal guid As Guid, users As IEnumerable(Of DTOUser)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Mailings.Log(guid, users, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al loguejar el mailing")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al loguejar el mailing")
        End Try
        Return retval
    End Function

End Class


Public Class MailingLogsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/MailingLogs/{guid}")>
    Public Function Logs(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oSource As New DTOBaseGuid(guid)
            Dim values = BEBL.MailingLogs.All(oSource)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els logs")
        End Try
        Return retval
    End Function
End Class
