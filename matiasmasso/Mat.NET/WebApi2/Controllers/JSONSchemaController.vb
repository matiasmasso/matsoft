Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class JsonSchemaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/JsonSchema/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.JsonSchema.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la JsonSchema")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/JsonSchema")>
    Public Function Update(<FromBody> value As DTOJsonSchema) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.JsonSchema.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la JsonSchema")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la JsonSchema")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/JsonSchema/delete")>
    Public Function Delete(<FromBody> value As DTOJsonSchema) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.JsonSchema.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la JsonSchema")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la JsonSchema")
        End Try
        Return retval
    End Function

End Class

Public Class JsonSchemasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/JsonSchemas")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.JsonSchemas.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les JsonSchemas")
        End Try
        Return retval
    End Function

End Class
