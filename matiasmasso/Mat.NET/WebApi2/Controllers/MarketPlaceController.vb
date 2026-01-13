Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MarketPlaceController
    Inherits _BaseController

    <HttpGet>
    <Route("api/MarketPlace/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.MarketPlace.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la MarketPlace")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/MarketPlace/Sku/{marketplace}/{sku}")>
    Public Function Find(marketPlace As Guid, sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.MarketPlace.FindSku(marketPlace, sku)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'article del MarketPlace")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/MarketPlace/catalog/{guid}")>
    Public Function Catalog(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMarketplace As New DTOMarketPlace(guid)
            Dim value = BEBL.MarketPlace.Catalog(oMarketplace)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el cataleg del MarketPlace")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/MarketPlace")>
    Public Function Update(<FromBody> value As DTOMarketPlace) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.MarketPlace.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la MarketPlace")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la MarketPlace")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/MarketPlace/UpdateSku")>
    Public Function UpdateSku(<FromBody> value As DTOMarketplaceSku) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.MarketPlace.UpdateSku(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'article al MarketPlace")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'article al MarketPlace")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/MarketPlace/delete")>
    Public Function Delete(<FromBody> value As DTOMarketPlace) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.MarketPlace.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la MarketPlace")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la MarketPlace")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/MarketPlace/skus/enable/{enabled}")>
    Public Function Enable(enabled As Integer, <FromBody> values As List(Of DTOMarketplaceSku)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.MarketPlace.EnableSkus(values, enabled = 1, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al habilitar producte")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al habilitar producte")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/MarketPlace/offers/{marketplace}")>
    Public Function Offers(marketplace As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oMarketplace As New DTOMarketPlace(marketplace)
            Dim values = BEBL.MarketPlace.Offers(oMarketplace)
            retval = Request.CreateResponse(Of List(Of DTOOffer))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al habilitar producte")
        End Try
        Return retval
    End Function

End Class

Public Class MarketPlacesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/MarketPlaces/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.MarketPlaces.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les MarketPlaces")
        End Try
        Return retval
    End Function

End Class

