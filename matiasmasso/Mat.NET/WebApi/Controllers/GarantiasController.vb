Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class GarantiasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Garantias/{emp}/{fchfrom}/{fchto}")>
    Public Function All(emp As DTOEmp.Ids, fchfrom As Date, fchto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Garantias.all(oEmp, fchfrom, fchto)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Garantias")
        End Try
        Return retval
    End Function

End Class

