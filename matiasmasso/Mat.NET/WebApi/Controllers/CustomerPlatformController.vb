Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CustomerPlatformController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerPlatform/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CustomerPlatform.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CustomerPlatform")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CustomerPlatform")>
    Public Function Update(<FromBody> value As DTOCustomerPlatform) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerPlatform.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CustomerPlatform")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CustomerPlatform")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CustomerPlatform/delete")>
    Public Function Delete(<FromBody> value As DTOCustomerPlatform) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerPlatform.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CustomerPlatform")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CustomerPlatform")
        End Try
        Return retval
    End Function

End Class

Public Class CustomerPlatformsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerPlatforms/{parent}")>
    Public Function All(parent As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oParent As New DTOContact(parent)
            Dim values = BEBL.CustomerPlatforms.All(oParent)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les CustomerPlatforms")
        End Try
        Return retval
    End Function

End Class
