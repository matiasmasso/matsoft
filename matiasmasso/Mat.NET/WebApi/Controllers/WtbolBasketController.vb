Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WtbolBasketController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolBasket/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WtbolBasket.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WtbolBasket")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WtbolBasket")>
    Public Function Update(<FromBody> value As DTOWtbolBasket) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WtbolBasket.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WtbolBasket")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WtbolBasket")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WtbolBasket/delete")>
    Public Function Delete(<FromBody> value As DTOWtbolBasket) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WtbolBasket.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WtbolBasket")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WtbolBasket")
        End Try
        Return retval
    End Function

End Class

Public Class WtbolBasketsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolBaskets/{site}")>
    Public Function All(site As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSite As New DTOWtbolSite(site)
            Dim values = BEBL.WtbolBaskets.All(oSite)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WtbolBaskets")
        End Try
        Return retval
    End Function

End Class
