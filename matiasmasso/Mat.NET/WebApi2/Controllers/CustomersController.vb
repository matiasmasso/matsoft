Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class CustomerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Customer/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Customer.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Customer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Customer/Exists/{customer}")>
    Public Function Exists(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Customer.Exists(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Customer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Customer/IsGroup/{customer}")>
    Public Function IsGroup(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Customer.IsGroup(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Customer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Customer/EFrasEnabled/{customer}")>
    Public Function EFrasEnabled(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Customer.EFrasEnabled(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Customer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Customer/Children/{customer}")>
    Public Function Children(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.Customer.Children(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Customer")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Customer")>
    Public Function Update(<FromBody> value As DTOCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Customer.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Customer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Customer")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Customer/delete")>
    Public Function Delete(<FromBody> value As DTOCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Customer.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Customer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Customer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Customer/IsImpagat/{customer}")>
    Public Function IsImpagat(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim value = BEBL.Customer.IsImpagat(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value) 'IIf(value, 1, 0))
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Customer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/customer/{customer}/deliveries")>
    Public Function CustomerDeliveries(customer As Guid) As List(Of DTODelivery)
        Dim oCustomer As New DTOCustomer(customer)
        Dim retval As List(Of DTODelivery) = BEBL.Deliveries.All(oCustomer)
        Return retval
    End Function
End Class

Public Class CustomersController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Customers/FromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.Customers.FromUser(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Customer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Customers/RaonsSocialsWithInvoices/{user}")>
    Public Function RaonsSocialsWithInvoices(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.Customers.RaonsSocialsWithInvoices(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Customer")
        End Try
        Return retval
    End Function

End Class