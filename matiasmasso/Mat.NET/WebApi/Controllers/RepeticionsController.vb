Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepeticionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Repeticions/{product}")>
    Public Function All(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.Repeticions.All(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Repeticions")
        End Try
        Return retval
    End Function

End Class
