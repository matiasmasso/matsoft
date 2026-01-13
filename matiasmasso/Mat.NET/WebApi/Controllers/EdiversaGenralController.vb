Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class EdiversaGenralsController
    Inherits _BaseController

    <HttpPost>
    <Route("api/EdiversaGenrals/search")>
    Public Function All(<FromBody> searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EdiversaGenrals.Search(searchkey)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al cercar les EdiversaGenrals")
        End Try
        Return retval
    End Function

End Class
