
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IvaLiquidacioController
    Inherits _BaseController

    <HttpGet>
    <Route("api/IvaLiquidacio/factory/{emp}/{year}/{month}")>
    Public Function Factory(emp As DTOEmp.Ids, year As Integer, month As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim value = BEBL.IVALiquidacio.Factory(oExercici, month)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al generar la Liquidacio de Iva")
        End Try
        Return retval
    End Function


End Class

