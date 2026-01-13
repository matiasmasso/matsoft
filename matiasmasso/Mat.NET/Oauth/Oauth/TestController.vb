Imports System.Web.Http
Imports System.Net.Http

<Authorize>
Public Class TestController
    Inherits ApiController
    <Route("test")>
    Public Function [Get]() As HttpResponseMessage
        Return Request.CreateResponse(System.Net.HttpStatusCode.OK, "hello from a secured resource!")
    End Function
End Class