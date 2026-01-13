Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WtbolsiteController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Wtbolsite/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Wtbolsite.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Wtbolsite")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Wtbolsite/FromMerchantId/{merchantId}")>
    Public Function FromMerchantId(merchantId As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Wtbolsite.FromMerchantId(merchantId)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Wtbolsite")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Wtbolsite/logo/{guid}")>
    Public Function Logo(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Wtbolsite.Logo(guid)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el logo del Wtbolsite")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Wtbolsite")>
    Public Function Update(<FromBody> value As DTOWtbolSite) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            value.RestoreObjects()
            If BEBL.Wtbolsite.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el Site")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el site")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Wtbolsite/model")>
    Public Function UpdateModel(<FromBody> value As Models.Wtbol.Site) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Wtbolsite.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el Site")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el site")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Wtbolsite/delete")>
    Public Function Delete(<FromBody> value As DTOWtbolSite) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Wtbolsite.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Wtbolsite")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Wtbolsite")
        End Try
        Return retval
    End Function


    'Logs each time Hatch gets the feed of customers stocks and landing pages from https://www.matiasmasso.es/britax/stocks/98781 or any other merchant id
    <HttpPost>
    <Route("api/WtbolSite/Log/{site}")>
    Public Function Log(site As Guid, <FromBody> Ip As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oSite As New DTOWtbolSite(site)
            Dim value = BEBL.Wtbolsite.Log(oSite, Ip, exs)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al loguejar la WtbolSite")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WtbolSite/LandingPageUpdate")>
    Public Function LandingPageUpdate(<FromBody> oLandingPage As Newtonsoft.Json.Linq.JObject) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim o = oLandingPage("Site")
            If BEBL.Wtbolsite.UpdateLandingPage(exs, oLandingPage) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al actualizar la landing page")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualizar la landing page")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WtbolSite/LandingPageDelete")>
    Public Function LandingPageDelete(<FromBody> oLandingPage As Newtonsoft.Json.Linq.JObject) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim o = oLandingPage("Site")
            If BEBL.Wtbolsite.DeleteLandingPage(exs, oLandingPage) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la landing page")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la landing page")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/WtbolSite/StockUpdate")>
    Public Function StockUpdate(<FromBody> oStock As DTOWtbolStock) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Wtbolsite.UpdateStock(exs, oStock) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al actualizar el stock")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualizar el stock")
        End Try
        Return retval
    End Function

End Class

Public Class WtbolsitesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Wtbolsites")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Wtbolsites.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Wtbolsites")
        End Try
        Return retval
    End Function

End Class
