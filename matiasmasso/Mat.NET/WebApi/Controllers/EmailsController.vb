Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class EmailsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Emails/fromContact/{contact}/{includeobsolets}")>
    Public Function All(contact As Guid, includeobsolets As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.Emails.All(oContact, includeobsolets <> 0)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Emails del contacte")
        End Try
        Return retval
    End Function

End Class
