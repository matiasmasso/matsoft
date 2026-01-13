Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BoxItemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BoxItems/BrandVideos/{lang}")>
    Public Function BrandVideos(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.BoxItems.BrandVideos(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BoxItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/BoxItems/FromEBooks/{emp}")>
    Public Function FromEBooks(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.BoxItems.FromEBooks(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BoxItems")
        End Try
        Return retval
    End Function

End Class
