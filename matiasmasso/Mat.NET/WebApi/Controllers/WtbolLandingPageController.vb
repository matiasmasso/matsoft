Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WtbolLandingPageController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolLandingPage/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.WtbolLandingPage.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WtbolLandingPage")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WtbolLandingPage")>
    Public Function Update(<FromBody> value As DTOWtbolLandingPage) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            value.RestoreObjects()
            If BEBL.WtbolLandingPage.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WtbolLandingPage")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WtbolLandingPage")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WtbolLandingPage/delete")>
    Public Function Delete(<FromBody> value As DTOWtbolLandingPage) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WtbolLandingPage.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WtbolLandingPage")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WtbolLandingPage")
        End Try
        Return retval
    End Function

End Class

Public Class WtbolLandingPagesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WtbolLandingPages/{product}/{includeStocksFromMgz}")>
    Public Function All(product As Guid, includeStocksFromMgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(includeStocksFromMgz)
            Dim values = BEBL.WtbolLandingPages.All(oProduct, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WtbolLandingPages")
        End Try
        Return retval
    End Function

End Class
