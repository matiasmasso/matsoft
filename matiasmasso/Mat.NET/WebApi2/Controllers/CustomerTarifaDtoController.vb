Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CustomerTarifaDtoController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerTarifaDto/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CustomerTarifaDto.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CustomerTarifaDto")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CustomerTarifaDto")>
    Public Function Update(<FromBody> value As DTOCustomerTarifaDto) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerTarifaDto.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CustomerTarifaDto")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CustomerTarifaDto")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/CustomerTarifaDto/delete")>
    Public Function Delete(<FromBody> value As DTOCustomerTarifaDto) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerTarifaDto.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CustomerTarifaDto")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CustomerTarifaDto")
        End Try
        Return retval
    End Function

End Class

Public Class CustomerTarifaDtosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerTarifaDtos/FromCustomer/{customer}")>
    Public Function FromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.CustomerTarifaDtos.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CustomerTarifaDtos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerTarifaDtos/FromChannel/{channel}")>
    Public Function FromChannel(channel As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oChannel As New DTODistributionChannel(channel)
            Dim values = BEBL.CustomerTarifaDtos.All(oChannel)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CustomerTarifaDtos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerTarifaDtos/ActiveFromCustomer/{customer}/{fch}")>
    Public Function ActiveFromCustomer(customer As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.CustomerTarifaDtos.Active(oCustomer, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CustomerTarifaDtos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerTarifaDtos/ActiveFromChannel/{channel}/{fch}")>
    Public Function ActiveFromChannel(channel As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oChannel As New DTODistributionChannel(channel)
            Dim values = BEBL.CustomerTarifaDtos.Active(oChannel, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CustomerTarifaDtos")
        End Try
        Return retval
    End Function

End Class

