Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class TrackingController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Tracking/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Tracking.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el tracking")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Tracking")>
    Public Function Update(<FromBody> value As DTOTracking) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Tracking.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el tracking")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el tracking")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Tracking/delete")>
    Public Function Delete(<FromBody> value As DTOTracking) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Tracking.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el tracking")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el tracking")
        End Try
        Return retval
    End Function

End Class

Public Class TrackingsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Trackings/{target}")>
    Public Function All(target As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTarget As New DTOBaseGuid(target)
            Dim values = BEBL.Trackings.All(oTarget)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els trackings")
        End Try
        Return retval
    End Function

End Class
