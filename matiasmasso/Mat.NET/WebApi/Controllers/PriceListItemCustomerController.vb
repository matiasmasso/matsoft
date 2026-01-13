Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PriceListItemCustomerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PriceListItemCustomer/{pricelist}/{sku}")>
    Public Function Find(pricelist As Guid, sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPriceList As New DTOPricelistCustomer(pricelist)
            Dim oSku As New DTOProductSku(sku)
            Dim value = BEBL.PriceListItemCustomer.Find(oPriceList, oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PriceListItemCustomer")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/PriceListItemCustomer")>
    Public Function Update(<FromBody> value As DTOPricelistItemCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListItemCustomer.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PriceListItemCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PriceListItemCustomer")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PriceListItemCustomer/delete")>
    Public Function Delete(<FromBody> value As DTOPricelistItemCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListItemCustomer.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PriceListItemCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PriceListItemCustomer")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/PriceListItemCustomer/search/{sku}/{fch}")>
    Public Function Search(sku As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim value = BEBL.PriceListItemCustomer.Search(oSku, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PriceListItemCustomer")
        End Try
        Return retval
    End Function

End Class

Public Class PriceListItemsCustomerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PriceListItemsCustomer/{sku}")>
    Public Function All(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim values = BEBL.PriceListItemsCustomer.All(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListItemsCustomer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PriceListItemsCustomer/Active/{customer}/{fch}")>
    Public Function Active(customer As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.PriceListItemsCustomer.Active(oCustomer, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListItemsCustomer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PriceListItemsCustomer/Vigent/{fch}")>
    Public Function Vigent(fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PriceListItemsCustomer.Vigent(fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListItemsCustomer")
        End Try
        Return retval
    End Function

End Class


