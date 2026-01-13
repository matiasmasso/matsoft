Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiOrderController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiOrder/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiOrder.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la EdiOrder")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/EdiOrder")>
    Public Function Update(<FromBody> value As DTOEdiOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiOrder.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiOrder")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiOrder")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/EdiOrder/delete")>
    Public Function Delete(<FromBody> value As DTOEdiOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiOrder.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la EdiOrder")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la EdiOrder")
        End Try
        Return retval
    End Function

End Class

Public Class EdiOrdersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiOrders")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EdiOrders.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiOrders")
        End Try
        Return retval
    End Function

End Class
