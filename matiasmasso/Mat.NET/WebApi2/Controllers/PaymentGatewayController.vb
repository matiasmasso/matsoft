Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PaymentGatewayController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PaymentGateway/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PaymentGateway.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PaymentGateway")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PaymentGateway")>
    Public Function Update(<FromBody> value As DTOPaymentGateway) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PaymentGateway.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PaymentGateway")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PaymentGateway")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PaymentGateway/delete")>
    Public Function Delete(<FromBody> value As DTOPaymentGateway) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PaymentGateway.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PaymentGateway")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PaymentGateway")
        End Try
        Return retval
    End Function

End Class

Public Class PaymentGatewaysController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PaymentGateways")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PaymentGateways.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PaymentGateways")
        End Try
        Return retval
    End Function

End Class
