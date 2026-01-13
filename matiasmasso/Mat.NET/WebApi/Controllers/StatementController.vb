Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StatementController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Statement/years/fromContact/{contact}")>
    Public Function Years(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.Statement.Years(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els exercicis de l'extracte")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Statement/fromContact/{contact}/{year}")>
    Public Function Items(contact As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.Statement.Items(oContact, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'extracte")
        End Try
        Return retval
    End Function

End Class
