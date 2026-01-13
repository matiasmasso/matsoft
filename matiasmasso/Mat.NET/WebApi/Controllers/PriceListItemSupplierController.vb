Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PriceListItemSupplierController
    Inherits _BaseController


    <HttpPost>
    <Route("api/PriceListItemSupplier")>
    Public Function Update(<FromBody> value As DTOPriceListItem_Supplier) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListItemSupplier.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la PriceListItemSupplier")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la PriceListItemSupplier")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PriceListItemSupplier/delete")>
    Public Function Delete(<FromBody> value As DTOPriceListItem_Supplier) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListItemSupplier.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PriceListItemSupplier")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PriceListItemSupplier")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/PriceListItemSupplier/FromSku/{proveidor}/{fch}")>
    Public Function FromSku(proveidor As Guid, fch As Date, <FromBody> ref As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.PriceListItemSupplier.FromSku(oProveidor, ref, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListItemSuppliers")
        End Try
        Return retval
    End Function

End Class

Public Class PriceListItemsSupplierController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PriceListItemsSupplier/Vigent/{proveidor}")>
    Public Function Vigent(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.PriceListItemsSupplier.Vigent(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListItemSuppliers")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PriceListItemsSupplier/{sku}")>
    Public Function All(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim values = BEBL.PriceListItemsSupplier.All(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PriceListItemSuppliers")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/PriceListItemsSupplier/delete")>
    Public Function Delete(<FromBody> values As List(Of DTOPriceListItem_Supplier)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListItemsSupplier.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PriceListItemsSupplier")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PriceListItemsSupplier")
        End Try
        Return retval
    End Function

End Class

