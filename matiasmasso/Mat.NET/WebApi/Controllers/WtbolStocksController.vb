Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class WtbolStocksController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolStocks/{site}")>
    Public Function All(site As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSite As New DTOWtbolSite(site)
            Dim values = BEBL.WtbolStocks.All(oSite)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WtbolStocks")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/WtbolStocks/merged/{site}")>
    Public Function Merged(site As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSite As New DTOWtbolSite(site)
            Dim values = BEBL.WtbolStocks.Merged(oSite)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WtbolStocks")
        End Try
        Return retval
    End Function

End Class
