Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CatalogController
    Inherits _BaseController

    <HttpGet>
    <Route("api/catalog")>
    Public Function Fetch() As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = MyBase.GetUser(exs)
            If exs.Count = 0 Then
                Dim value = BEBL.Catalog.Fetch(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs)
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el cataleg")
        End Try
        Return retval
    End Function


End Class
