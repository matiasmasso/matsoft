Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StatQuotasOnlineController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StatQuotasOnline/{proveidor}")>
    Public Function All(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.StatQuotasOnline.All(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StatQuotaOnlines")
        End Try
        Return retval
    End Function

End Class
