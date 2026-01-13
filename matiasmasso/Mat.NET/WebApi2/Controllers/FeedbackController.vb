Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class FeedbackController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Feedback/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Feedback.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Feedback")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Feedback")>
    Public Function Update(<FromBody> value As DTOFeedback) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Feedback.Update(value, exs) Then
                retval = Request.CreateResponse(Of Guid)(HttpStatusCode.OK, value.Guid)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Feedback")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Feedback")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Feedback/delete")>
    Public Function Delete(<FromBody> value As DTOFeedback) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Feedback.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Feedback")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Feedback")
        End Try
        Return retval
    End Function

End Class

Public Class FeedbacksController
    Inherits _BaseController


End Class
