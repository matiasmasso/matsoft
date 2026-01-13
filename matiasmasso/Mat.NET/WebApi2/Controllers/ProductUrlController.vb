Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductUrlController
    Inherits _BaseController

    <HttpPost>
    <Route("api/productUrl/{emp}")>
    Public Function Search(emp As DTOEmp.Ids, <FromBody> url As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim value = BEBL.ProductUrl.Search2(url)
            'Dim value2 = BEBL.ProductUrl.Search(url)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Url")
        End Try
        Return retval
    End Function
End Class
