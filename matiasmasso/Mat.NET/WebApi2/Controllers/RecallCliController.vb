Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RecallCliController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RecallCli/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.RecallCli.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RecallCli")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RecallCli")>
    Public Function Update(<FromBody> value As DTORecallCli) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RecallCli.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RecallCli")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RecallCli")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/RecallCli/delete")>
    Public Function Delete(<FromBody> value As DTORecallCli) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.RecallCli.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la RecallCli")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la RecallCli")
        End Try
        Return retval
    End Function

End Class

Public Class RecallClisController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RecallClis/{recall}")>
    Public Function All(recall As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRecall As New DTORecall(recall)
            Dim values = BEBL.RecallClis.All(oRecall)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RecallClis")
        End Try
        Return retval
    End Function

End Class
