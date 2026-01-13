Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RemittanceAdviceController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RemittanceAdvice/FromCca/{cca}")>
    Public Function Find(cca As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCca As New DTOCca(cca)
            Dim value = BEBL.RemittanceAdvice.FromCca(oCca)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RemittanceAdvice")
        End Try
        Return retval
    End Function

End Class

