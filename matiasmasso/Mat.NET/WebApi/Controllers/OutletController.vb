Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class OutletController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Outlet/{user}")>
    Public Function All(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Outlet.All(oUser.Emp, oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function

End Class
