Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SabiasQueController
    Inherits _BaseController

    <HttpPost>
    <Route("api/sabiasQue/search")>
    Public Function search(<FromBody> sFriendlyUrlSegment As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.SabiasQue.Search(sFriendlyUrlSegment)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la sabiasQue")
        End Try
        Return retval
    End Function


End Class
