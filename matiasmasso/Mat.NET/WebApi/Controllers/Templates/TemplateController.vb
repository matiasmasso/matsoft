Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TemplateController
    Inherits _BaseController

    <HttpGet>
    <Route("api/template/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Template.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Template")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Template/image/{guid}")>
    Public Function GetIcon(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Template.Find(guid)
            'retval = MyBase.HttpImageResponseMessage(value.Image)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del Template")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/template")>
    Public Function Update(<FromBody> value As DTOTemplate) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Template.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Template")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Template")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Template/upload")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOTemplate)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Template")
            Else
                If value.Docfile IsNot Nothing Then
                    value.Docfile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.Docfile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.TemplateLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.TemplateLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.TemplateLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Template/delete")>
    Public Function Delete(<FromBody> value As DTOTemplate) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Template.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Template")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Template")
        End Try
        Return retval
    End Function

End Class

Public Class TemplatesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Templates")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Templates.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Templates")
        End Try
        Return retval
    End Function

End Class
