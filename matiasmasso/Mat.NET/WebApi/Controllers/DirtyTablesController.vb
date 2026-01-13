Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class DirtyTablesController
    Inherits _BaseController


    <HttpGet>
    <Route("api/dirtytables")>
    Public Function GetValues() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DirtyTables.GetValues()
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les darreres actualitzacions de cada tabla")
        End Try
        Return retval
    End Function




End Class
