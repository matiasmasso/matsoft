Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContactTelsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactTels/{contact}/{cod}")>
    Public Function AllFromCod(contact As Guid, cod As DTOContactTel.Cods) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.ContactTels.All(oContact, cod)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ContactTels")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ContactTels/{contact}")>
    Public Function All(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.ContactTels.All(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ContactTels")
        End Try
        Return retval
    End Function

End Class
