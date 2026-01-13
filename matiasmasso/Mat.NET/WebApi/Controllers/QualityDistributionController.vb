Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class QualityDistributionController
    Inherits _BaseController

    <HttpGet>
    <Route("api/QualityDistribution/{proveidor}/{fchfrom}")>
    Public Function All(proveidor As Guid, fchfrom As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.QualityDistribution.All(oProveidor, fchfrom)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les QualityDistributions")
        End Try
        Return retval
    End Function

End Class
