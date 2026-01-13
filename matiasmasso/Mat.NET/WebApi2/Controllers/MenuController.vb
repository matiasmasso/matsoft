Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class MenuController
    Inherits _BaseController

    <HttpGet>
    <Route("api/menu/{user}")>
    Public Function Fetch(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = DTOBaseGuid.opcional(Of DTOUser)(user)
            If oUser IsNot Nothing Then BEBL.User.Load(oUser)
            Dim values = BEBL.Menu.Fetch(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el menu")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/menu/products")>
    Public Function Products() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Depts.BrandDeptsMenuItems()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el menu de marques i categories")
        End Try
        Return retval
    End Function



End Class
