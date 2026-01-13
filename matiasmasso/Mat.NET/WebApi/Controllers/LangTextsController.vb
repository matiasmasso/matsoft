Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class LangTextController
    Inherits _BaseController


    <HttpPost>
    <Route("api/LangText/Search")>
    Public Function Search(oRequest As DTOSearchRequest) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.LangText.Search(oRequest)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cercar per paraules clau")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/LangText/{guid}/{src}")>
    Public Function Find(guid As Guid, src As DTOLangText.Srcs) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.LangText.Find(guid, src)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Template")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/LangText")>
    Public Function Update(<FromBody> oLangText As DTOLangText) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.LangText.Update(exs, oLangText) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, oLangText)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al desar la traduccio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al desar la traduccio")
        End Try
        Return retval
    End Function
End Class

Public Class LangTextsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/LangTexts")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.LangTexts.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les traduccions")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/LangTexts/MissingTranslations")>
    Public Function Missing() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.LangTexts.MissingTranslations()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les traduccions")
        End Try
        Return retval
    End Function


End Class
