Imports System.Drawing
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Imports DAL

Public Class PdfController
    Inherits _BaseController

    <Route("api/Pdf/pdf2Jpg")>
    <HttpPost>
    Public Function PdfToJpg() As HttpResponseMessage
        Dim retval As New HttpResponseMessage
        Try
            Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
            Dim fileBytes = oHelper.GetFileBytes("file")
            Dim thumbnail = LegacyHelper.GhostScriptHelper.Pdf2Jpg(fileBytes)
            retval = MyBase.HttpImageResponseMessage(thumbnail.Bytes())

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al convertir la portada del pdf a imatge")
        End Try
        Return retval
    End Function

End Class
