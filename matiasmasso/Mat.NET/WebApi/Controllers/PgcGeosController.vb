Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PgcGeosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PgcGeos/FromExercici/{emp}/{year}")>
    Public Function FromExercici(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oExercici = MyBase.GetExercici(emp, year)
            Dim values = BEBL.PgcGeos.FromExercici(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els PgcGeos")
        End Try
        Return retval
    End Function


End Class

