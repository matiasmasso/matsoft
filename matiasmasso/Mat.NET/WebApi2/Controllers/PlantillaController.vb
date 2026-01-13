Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PlantillaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Plantilla/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Plantilla.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Plantilla")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Plantilla/doc/{guid}")>
    Public Function Doc(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Plantilla.Find(guid)
            Dim oStream = BEBL.DocFile.Load(value.DocFile, LoadStream:=True)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Plantilla")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Plantilla/upload/{emp}")>
    Public Function Upload(emp As Integer) As HttpResponseMessage
        Dim oEmp As New DTOEmp(emp)
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOPlantilla)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la Plantilla")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.PlantillaLoader.Update(value, oEmp, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.PlantillaLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.PlantillaLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Plantilla/delete")>
    Public Function Delete(<FromBody> value As DTOPlantilla) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Plantilla.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Plantilla")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la plantilla")
        End Try
        Return retval
    End Function

End Class

Public Class PlantillasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Plantillas/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            Dim values = BEBL.Plantillas.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Plantilles")
        End Try
        Return retval
    End Function

End Class

