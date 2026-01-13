Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class Mod145Controller
    Inherits _BaseController

End Class
Public Class Mods145Controller
    Inherits _BaseController

    <HttpGet>
    <Route("api/Mods145/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Mods145.GetValues(emp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els models 145")
        End Try
        Return retval
    End Function
End Class
