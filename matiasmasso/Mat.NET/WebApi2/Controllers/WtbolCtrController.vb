Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WtbolCtrController
    Inherits _BaseController

    <HttpPost>
    <Route("api/WtbolCtr/Log/{landingpage}")>
    Public Function Log(landingpage As Guid, <FromBody> Ip As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oLandingPage = BEBL.WtbolLandingPage.Find(landingpage)
            Dim value = BEBL.WtbolCtr.Log(oLandingPage, Ip, exs)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al loguejar la WtbolCtr")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WtbolCtr/delete")>
    Public Function Delete(<FromBody> value As DTOWtbolCtr) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WtbolCtr.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WtbolCtr")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WtbolCtr")
        End Try
        Return retval
    End Function

End Class

Public Class WtbolCtrsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolCtrs/{site}/{fchfrom}/{fchto}")>
    Public Function All(site As Guid, fchfrom As Date, fchto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSite = DTOBaseGuid.Opcional(Of DTOWtbolSite)(site)
            Dim values = BEBL.WtbolCtrs.All(oSite, fchfrom, fchto)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WtbolCtrs")
        End Try
        Return retval
    End Function

End Class
