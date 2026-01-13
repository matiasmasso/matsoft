Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BritaxController
    Inherits _BaseController

    <HttpGet>
    <Route("api/britax/storelocator")>
    Public Function JSONStorelocator() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Britax.XMLStoreLocator()
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error on reading sale points")
        End Try
        Return retval
    End Function

    Public Function XMLStorelocator() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Britax.XMLStoreLocator()
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error on reading sale points")
        End Try
        Return retval
    End Function



End Class

