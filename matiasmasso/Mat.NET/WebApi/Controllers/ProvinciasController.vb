Imports System.Net
Imports System.Net.Http
Imports System.Web.Http 'permet <fromBody> i rutas diferents del nom del controlador
Public Class ProvinciasController
    Inherits _BaseController


    <HttpGet>
    <Route("api/provincia/{guid}/zonas")>
    Public Function Zonas(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProvincia As New DTOAreaProvincia(guid)
            Dim values = BEBL.AreaProvincia.Zonas(oProvincia)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al carregar les zones")
        End Try
        Return retval
    End Function
End Class
