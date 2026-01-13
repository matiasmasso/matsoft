Imports System.Net
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Web.Http

Public Class CertController
    Inherits _BaseController

    <HttpGet>
    <Route("api/cert/{contact}")>
    Public Function GetValue(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim value As DTOCert = BEBL.Cert.FromContact(oContact)
            If value Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage(New Exception("Contacte sense certificat"))
            Else
                retval = Request.CreateResponse(Of DTOCert)(HttpStatusCode.OK, value)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error de servidor al llegir el certificat")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/cert/image/{contact}")>
    Public Function Image(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCert As New DTOCert(contact)
            Dim oImage = BEBL.Cert.Image(oCert)
            retval = MyBase.HttpImageResponseMessage(oImage)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descarregar la imatge de la signatura")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/cert/stream/{contact}")>
    Public Function GetStream(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim value As DTOCert = BEBL.Cert.FromContact(oContact)
            If value Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Contacte sense certificat")
            Else
                retval = Request.CreateResponse(HttpStatusCode.OK)
                retval.Content = New ByteArrayContent(value.Stream)
                retval.Content.Headers.ContentType = New MediaTypeHeaderValue("application/octet-stream")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error de servidor al llegir el certificat")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Cert")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOCert)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el Certificat")
            Else
                value.Stream = oHelper.GetFileBytes("stream")
                value.Image = oHelper.GetImage("image")
                If DAL.CertLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.CertLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.CertLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Cert/delete")>
    Public Function Delete(<FromBody> value As DTOCert) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cert.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Certificat")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Certificat")
        End Try
        Return retval
    End Function

End Class
