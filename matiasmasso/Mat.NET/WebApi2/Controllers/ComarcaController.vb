Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ComarcaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Comarca/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Comarca.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Comarca")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Comarca")>
    Public Function Update(<FromBody> value As DTOComarca) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Comarca.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Comarca")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Comarca")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Comarca/delete")>
    Public Function Delete(<FromBody> value As DTOComarca) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Comarca.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Comarca")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Comarca")
        End Try
        Return retval
    End Function

End Class

Public Class ComarcasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Comarcas/{zona}")>
    Public Function All(zona As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oZona As New DTOZona(zona)
            Dim values = BEBL.Comarcas.All(oZona)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Comarcas")
        End Try
        Return retval
    End Function

End Class

