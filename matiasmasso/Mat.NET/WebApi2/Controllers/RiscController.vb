Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RiscController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Risc/CreditLimit/{customer}")>
    Public Function CreditLimit(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Risc.CreditLimit(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Risc/CreditDisponible/{customer}")>
    Public Function CreditDisponible(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = BEBL.Customer.Find(customer)
            Dim value = BEBL.Risc.CreditDisponible(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Risc/CreditDisposat/{customer}")>
    Public Function CreditDisposat(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = BEBL.Customer.Find(customer)
            Dim value = BEBL.Risc.CreditDisposat(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Risc/DipositIrrevocable/{customer}")>
    Public Function DipositIrrevocable(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Risc.DipositIrrevocable(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Risc/EntregatACompte/{customer}")>
    Public Function EntregatACompte(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Risc.EntregatACompte(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Risc/FrasPendentsDeVencer/{customer}")>
    Public Function FrasPendentsDeVencer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = BEBL.Customer.Find(customer)
            Dim value = BEBL.Risc.FrasPendentsDeVencer(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Risc/SdoAlbsNoCredit/{customer}")>
    Public Function SdoAlbsNoCredit(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Risc.SdoAlbsNoCredit(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Risc/SdoAlbsACredit/{customer}")>
    Public Function SdoAlbsACredit(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Risc.SdoAlbsACredit(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Risc")
        End Try
        Return retval
    End Function


End Class
