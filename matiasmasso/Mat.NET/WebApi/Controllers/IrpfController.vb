Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IrpfController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Irpf/{emp}/{year}/{month}")>
    Public Function Irpf(emp As DTOEmp.Ids, year As Integer, month As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim value = BEBL.Irpf.Factory(oEmp, year, month)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les partides d'Irpf")
        End Try
        Return retval
    End Function

End Class
