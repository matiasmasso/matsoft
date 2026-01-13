Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class CnapController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Cnap/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Cnap.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Cnap")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Cnap")>
    Public Function Update(<FromBody> value As DTOCnap) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cnap.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Cnap")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Cnap")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Cnap/delete")>
    Public Function Delete(<FromBody> value As DTOCnap) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cnap.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Cnap")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Cnap")
        End Try
        Return retval
    End Function

End Class

Public Class CnapsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Cnaps")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Cnaps.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Cnaps")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Cnaps/{searchkey}")>
    Public Function Search(searchkey As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Cnaps.All(searchkey)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Cnaps")
        End Try
        Return retval
    End Function

End Class

