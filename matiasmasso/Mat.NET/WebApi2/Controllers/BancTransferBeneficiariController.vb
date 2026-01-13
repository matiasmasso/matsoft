Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BancTransferBeneficiariController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BancTransferBeneficiari/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.BancTransferBeneficiari.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el BancTransferBeneficiari")
        End Try
        Return retval
    End Function

End Class
