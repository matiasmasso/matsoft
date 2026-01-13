Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class InemCompresProductRankingController
    Inherits _BaseController

    <HttpGet>
    <Route("api/InemCompresProductRanking/{emp}/{year}/{month}")>
    Public Function All(emp As DTOEmp.Ids, year As Integer, month As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oYearMonth As New DTOYearMonth(year, month)
            Dim values = BEBL.InemCompresProductRanking.All(oEmp, oYearMonth)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les InemCompresProductRanking")
        End Try
        Return retval
    End Function

End Class

