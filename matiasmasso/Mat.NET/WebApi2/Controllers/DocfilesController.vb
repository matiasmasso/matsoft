Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DocFileController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Docfile/{asin}")>
    Public Function Find(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDocfile = BEBL.DocFile.Find(asin)
            retval = Request.CreateResponse(HttpStatusCode.OK, oDocfile)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/bySha256/{Base64Hash}")>
    Public Function FindBySha256(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim sha256 = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim oDocfile = BEBL.DocFile.FindBySha256(sha256)
            retval = Request.CreateResponse(HttpStatusCode.OK, oDocfile)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Docfile/thumbnail/{asin}")>
    Public Function GetThumbnail(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DocFile.Thumbnail(asin)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el thumbnail del docfile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/thumbnail/{asin}/{width}")>
    Public Function GetThumbnail150(asin As String, width As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DocFile.Thumbnail(asin, width)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el thumbnail del docfile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/stream/{asin}")>
    Public Function GetStream(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DocFile.Stream(asin)
            Dim filename = String.Format("M+O file.{0}", value.Mime.ToString())
            retval = MyBase.HttpFileAttachmentResponseMessage(value.ByteArray, filename)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el fitxer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/stream/inline/{asin}")>
    Public Function GetStreamInline(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.DocFile.Find(asin, True)
            Dim filename = String.Format("M+O file.{0}", value.Mime.ToString())
            retval = MyBase.HttpFileStreamResponseMessage(value.Stream, filename)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el stream del docfile")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Docfile/log/{user}/{asin}")>
    Public Function Log(user As Guid, asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim exs As New List(Of Exception)
            If BEBL.DocFile.Log(asin, oUser, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al loggejar el docfile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/Srcs/{asin}")>
    Public Function Srcs(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDocfile As New DTODocFile(asin)
            Dim exs As New List(Of Exception)
            Dim values = BEBL.DocFile.Srcs(oDocfile)
            retval = Request.CreateResponse(Of List(Of DTODocFileSrc))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les fonts del docfile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/Logs/{asin}")>
    Public Function Logs(asin As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDocfile As New DTODocFile(asin)
            Dim exs As New List(Of Exception)
            Dim values = BEBL.DocFile.Logs(oDocfile)
            retval = Request.CreateResponse(Of List(Of DTODocFileLog))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els logs del docfile")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Docfile")>
    Public Function SinglePost() As HttpResponseMessage
        Dim result As HttpResponseMessage = Nothing
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTODocFile)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el document")
            Else
                value.Thumbnail = oHelper.GetImage("thumbnail")
                value.Stream = oHelper.GetFileBytes("stream")
                If BEBL.DocFile.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar la imatge del vehicle")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.DocFileLoader.Update")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/docfile/delete")>
    Public Function DeleteValue(<FromBody()> ByVal value As DTODocFile) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        If DAL.DocFileLoader.Delete(value, exs) Then
            retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
        Else
            retval = MyBase.HttpErrorResponseMessage(exs)
        End If
        Return retval
    End Function

End Class

Public Class DocFilesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Docfiles/{year}")>
    Public Function GetValues(year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.DocFiles.All(year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al descarregar els fitxers")
        End Try
        Return retval
    End Function
End Class

