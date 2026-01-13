Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class FeedbackSourceController
    Inherits _BaseController

    <HttpGet>
    <Route("api/FeedbackSource/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.FeedbackSource.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la FeedbackSource")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/FeedbackSource")>
    Public Function Update(<FromBody> value As DTOFeedback.SourceClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.FeedbackSource.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la FeedbackSource")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la FeedbackSource")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/FeedbackSource/delete")>
    Public Function Delete(<FromBody> value As DTOFeedback.SourceClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.FeedbackSource.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la FeedbackSource")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la FeedbackSource")
        End Try
        Return retval
    End Function

End Class

Public Class FeedbackSourcesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/FeedbackSources/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.FeedbackSources.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les FeedbackSources")
        End Try
        Return retval
    End Function

End Class
