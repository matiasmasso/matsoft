Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepCustomersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/RepCustomers/{user}/{area?}")>
    Public Function All(user As Guid, Optional area As Nullable(Of Guid) = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oArea As DTOArea = Nothing
            If area IsNot Nothing Then
                oArea = New DTOArea(area)
            End If
            Dim values = BEBL.RepCustomers.All(oUser, oArea)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/RepCustomers/Atlas/{repUser}")>
    Public Function Atlas(repUser As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(repUser)
            Dim values = BEBL.RepCustomers.Atlas(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els clients")
        End Try
        Return retval
    End Function


End Class
