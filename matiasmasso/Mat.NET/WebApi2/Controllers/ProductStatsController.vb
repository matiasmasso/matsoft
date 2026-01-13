Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductStatsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ProductStats/{category}")>
    Public Function All(category As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCategory As New DTOProductCategory(category)
            Dim values = BEBL.ProductStats.All(oCategory, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir les estadistiques")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les estadistiques")
        End Try
        Return retval
    End Function
End Class
