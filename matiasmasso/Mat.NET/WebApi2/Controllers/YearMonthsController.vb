Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class YearMonthsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/YearMonths/{fchfrom}/{fchTo}")>
    Public Function All(FchFrom As Date, FchTo As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.YearMonths.All(FchFrom, FchTo)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les YearMonths")
        End Try
        Return retval
    End Function

End Class
