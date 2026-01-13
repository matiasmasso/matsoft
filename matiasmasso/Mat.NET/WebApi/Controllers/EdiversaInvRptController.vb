Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaInvRptController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaInvRpt/send/{emp}/{customer}")>
    Public Function Send(emp As DTOEmp.Ids, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As New DTOCustomer(customer)
            If BEBL.EdiversaInvRpt.Send(oEmp, oCustomer, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al redactar el Inventory Report")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar el Inventory Report")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/EdiversaInvRpt/src/{emp}/{customer}")>
    Public Function Src(emp As DTOEmp.Ids, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.EdiversaInvRpt.EdiSrc(oEmp, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar el Inventory Report")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/EdiversaInvRpt/Excel/{emp}/{customer}")>
    Public Function Excel(emp As DTOEmp.Ids, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.EdiversaInvRpt.Excel(oEmp, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar el Inventory Report")
        End Try
        Return retval
    End Function

End Class

