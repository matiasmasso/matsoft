Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RecallController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Recall/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Recall.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Recall")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Recall")>
    Public Function Update(<FromBody> value As DTORecall) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Recall.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Recall")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Recall")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Recall/delete")>
    Public Function Delete(<FromBody> value As DTORecall) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Recall.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Recall")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Recall")
        End Try
        Return retval
    End Function

End Class

Public Class RecallsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Recalls")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Recalls.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Recalls")
        End Try
        Return retval
    End Function

End Class
