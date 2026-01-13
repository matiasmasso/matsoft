Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DocController
    Inherits _BaseController


    <HttpGet>
    <Route("doc/{asin}")>
    Public Function GetStream(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DocFile.Stream(asin)
            Dim filename = String.Format("M+O file.{0}.{1}", asin, value.Mime.ToString())
            'retval = MyBase.HttpFileAttachmentResponseMessage(value.ByteArray, filename)
            retval = MyBase.HttpFileStreamResponseMessage(value.ByteArray, filename)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el fitxer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("doc/thumbnail/{asin}")>
    Public Function GetThumbnail(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DocFile.Thumbnail(asin)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el fitxer")
        End Try
        Return retval
    End Function

End Class
