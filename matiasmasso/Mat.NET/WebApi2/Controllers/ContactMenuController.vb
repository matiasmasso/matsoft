Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContactMenuController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactMenu/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ContactMenu.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el contacte")
        End Try
        Return retval
    End Function
End Class