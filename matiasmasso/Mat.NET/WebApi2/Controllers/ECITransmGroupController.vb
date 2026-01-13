Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ECITransmGroupController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ECITransmGroup/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ECITransmGroup.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la ECITransmGroup")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ECITransmGroup")>
    Public Function Update(<FromBody> value As DTOECITransmGroup) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ECITransmGroup.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ECITransmGroup")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ECITransmGroup")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ECITransmGroup/delete")>
    Public Function Delete(<FromBody> value As DTOECITransmGroup) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ECITransmGroup.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ECITransmGroup")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ECITransmGroup")
        End Try
        Return retval
    End Function

End Class

Public Class ECITransmGroupsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ECITransmGroups")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ECITransmGroups.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ECITransmGroups")
        End Try
        Return retval
    End Function

End Class

