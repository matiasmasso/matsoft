Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class CacheController
    Inherits _BaseController

    <HttpPost>
    <Route("api/cache/checkForClientUpdates")>
    Public Function CheckForClientUpdates(<FromBody> cache As Models.ServerCache) As HttpResponseMessage 'To Deprecate
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ServerCache.CheckForClientUpdates(cache)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualitzar la caché de dades")
        End Try
        Return retval
    End Function

End Class
