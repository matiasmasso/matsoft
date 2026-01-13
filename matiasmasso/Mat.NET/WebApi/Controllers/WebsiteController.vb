Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebsiteController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Website/Home/{user}/{lang}")>
    Public Function Home(user As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.WebsiteHome.Factory(oUser, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al redactar la Home")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Website/Noticias/{user}/{lang}/{pageIdx}")>
    Public Function Noticias(user As Guid, lang As String, pageIdx As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.Website.Noticias(oUser, oLang, pageIdx)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Banc")
        End Try
        Return retval
    End Function

End Class
