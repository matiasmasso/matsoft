Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class KeywordsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/keywords/{src}")>
    Public Function All(src As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.KeyWords.all(src)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les keywords")
        End Try
        Return retval
    End Function

End Class

