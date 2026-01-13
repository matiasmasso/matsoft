Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaOrderItemController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaOrderItem/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaOrderItem.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la EdiversaOrderItem")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/EdiversaOrderItem")>
    Public Function Update(<FromBody> value As DTOEdiversaOrderItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaOrderItem.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaOrderItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaOrderItem")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/EdiversaOrderItem/delete")>
    Public Function Delete(<FromBody> value As DTOEdiversaOrderItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaOrderItem.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la EdiversaOrderItem")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la EdiversaOrderItem")
        End Try
        Return retval
    End Function
End Class
