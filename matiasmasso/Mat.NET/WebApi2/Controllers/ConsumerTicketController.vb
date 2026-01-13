Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ConsumerTicketController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ConsumerTicket/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ConsumerTicket.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ticket de consumidor")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ConsumerTicket/{marketplace}/{orderid}")>
    Public Function Find(marketplace As Guid, orderid As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMarketPlace As New DTOMarketPlace(marketplace)
            Dim value = BEBL.ConsumerTicket.Find(oMarketPlace, orderid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ticket de consumidor")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ConsumerTicket/fromDelivery/{delivery}")>
    Public Function FromTarget(delivery As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDelivery As New DTODelivery(delivery)
            Dim value = BEBL.ConsumerTicket.FromDelivery(oDelivery)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ticket de consumidor")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ConsumerTicket")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOConsumerTicket)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el ticket de consumidor")
            Else
                If value.PurchaseOrder IsNot Nothing AndAlso value.PurchaseOrder.DocFile IsNot Nothing Then
                    value.PurchaseOrder.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.PurchaseOrder.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If BEBL.ConsumerTicket.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al desar el ticket de consumidor")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al desar el ticket de consumidor")
        End Try

        Return result

    End Function

    <HttpPost>
    <Route("api/ConsumerTicket/delete")>
    Public Function Delete(<FromBody> value As DTOConsumerTicket) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ConsumerTicket.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el ticket de consumidor")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el ticket de consumidor")
        End Try
        Return retval
    End Function

End Class

Public Class ConsumerTicketsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ConsumerTickets/{emp}/{year}")>
    Public Function All(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.ConsumerTickets.All(oEmp, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els tickets de consumidor")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ConsumerTickets/FromMarketPlace/{marketPlace}/{year}")>
    Public Function FromMarketPlace(marketPlace As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMarketPlace As New DTOMarketPlace(marketPlace)
            Dim values = BEBL.ConsumerTickets.All(oMarketPlace, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els tickets de consumidor")
        End Try
        Return retval
    End Function

End Class

