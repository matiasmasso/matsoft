Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PriceListCustomerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PriceListCustomer/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PriceListCustomer.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PriceListCustomer")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PriceListCustomer")>
    Public Function Update(<FromBody> value As DTOPricelistCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListCustomer.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PriceListCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PriceListCustomer")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PriceListCustomer/delete")>
    Public Function Delete(<FromBody> value As DTOPricelistCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListCustomer.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PriceListCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PriceListCustomer")
        End Try
        Return retval
    End Function

End Class

Public Class PriceListsCustomerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PriceListsCustomer")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PriceListsCustomer.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListsCustomer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PriceListsCustomer/{customer}")>
    Public Function All(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.PriceListsCustomer.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListsCustomer")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PriceListsCustomer/Delete")>
    Public Function Delete(<FromBody> values As List(Of DTOPricelistCustomer)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListsCustomer.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les PriceListCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les PriceListCustomer")
        End Try
        Return retval
    End Function

End Class
