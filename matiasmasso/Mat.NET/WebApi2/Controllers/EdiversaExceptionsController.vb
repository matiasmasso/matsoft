Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaExceptionsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ediversaExceptions/{parent}")>
    Public Function All(parent As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oParent As New DTOBaseGuid(parent)
            Dim values = BEBL.EdiversaExceptions.All(oParent)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ediversaExceptions")
        End Try
        Return retval
    End Function

End Class
