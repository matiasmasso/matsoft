Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DocFileController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Docfile/{Base64Hash}")>
    Public Function Find(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim oDocfile = BEBL.DocFile.Find(hash)
            retval = Request.CreateResponse(HttpStatusCode.OK, oDocfile)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Docfile/thumbnail/{Base64Hash}")>
    Public Function GetThumbnail(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim value = BEBL.DocFile.Thumbnail(hash)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el thumbnail del docfile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/thumbnail/{Base64Hash}/{width}")>
    Public Function GetThumbnail150(Base64Hash As String, width As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim value = BEBL.DocFile.Thumbnail(hash, width)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el thumbnail del docfile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/stream/{Base64Hash}")>
    Public Function GetStream(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim value = BEBL.DocFile.Stream(hash)
            Dim filename = String.Format("M+O file.{0}", value.Mime.ToString())
            retval = MyBase.HttpFileAttachmentResponseMessage(value.ByteArray, filename)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el fitxer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/stream/inline/{Base64Hash}")>
    Public Function GetStreamInline(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim value = BEBL.DocFile.Find(hash, True)
            Dim filename = String.Format("M+O file.{0}", value.Mime.ToString())
            retval = MyBase.HttpFileStreamResponseMessage(value.Stream, filename)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el stream del docfile")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Docfile/log/{user}/{Base64Hash}")>
    Public Function Log(user As Guid, Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim oUser As New DTOUser(user)
            Dim exs As New List(Of Exception)
            If BEBL.DocFile.Log(hash, oUser, exs) Then
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
    <Route("api/Docfile/Srcs/{Base64Hash}")>
    Public Function Srcs(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim oDocfile As New DTODocFile(hash)
            Dim exs As New List(Of Exception)
            Dim values = BEBL.DocFile.Srcs(oDocfile)
            retval = Request.CreateResponse(Of List(Of DTODocFileSrc))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les fonts del docfile")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Docfile/Logs/{Base64Hash}")>
    Public Function Logs(Base64Hash As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim hash = CryptoHelper.FromUrFriendlyBase64(Base64Hash)
            Dim oDocfile As New DTODocFile(hash)
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

