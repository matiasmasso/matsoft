Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SkuController
    Inherits _BaseController


    <HttpGet>
    <Route("api/skus/search/{mgz}/{searchkey}/{fch}")>
    Public Function Search(mgz As Guid, searchkey As String, fch As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim dtFch = DateTime.Parse(fch)
            Dim oMgz = DTOMgz.FromContact(BEBL.Contact.Find(mgz))
            If oMgz IsNot Nothing Then
                Dim values = BEBL.ProductSkus.Search(oMgz.Emp, searchkey, oMgz, fch)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar el producte")
        End Try
        Return retval
    End Function




End Class
