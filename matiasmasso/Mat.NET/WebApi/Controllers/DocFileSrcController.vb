Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DocFileSrcController
    Inherits _BaseController

    <HttpPost>
    <Route("api/DocFileSrc/load")>
    Public Function Load(value As DTODocFileSrc) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            BEBL.DocFileSrc.load(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el DocFileSrc")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/DocFileSrc")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTODocFileSrc)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la DocFileSrc")
            Else
                If value.Docfile IsNot Nothing Then
                    value.Docfile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.Docfile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.DocfileSrcLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.DocFileSrcLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.DocFileSrcLoader")
        End Try

        Return result
    End Function



    <HttpPost>
    <Route("api/DocFileSrc/delete")>
    Public Function Delete(<FromBody> value As DTODocFileSrc) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.DocFileSrc.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la DocFileSrc")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la DocFileSrc")
        End Try
        Return retval
    End Function

End Class

Public Class DocFileSrcsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/DocFileSrcs/{guid}")>
    Public Function All(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBaseGuid As New DTOBaseGuid(guid)
            Dim values = BEBL.DocFileSrcs.All(oBaseGuid)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DocFileSrcs")
        End Try
        Return retval
    End Function

End Class
