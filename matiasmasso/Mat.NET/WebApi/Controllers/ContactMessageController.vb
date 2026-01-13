Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ContactMessageController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactMessage/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.ContactMessage.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ContactMessage")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ContactMessage")>
    Public Function Update(<FromBody> value As DTOContactMessage) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContactMessage.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ContactMessage")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ContactMessage")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/ContactMessage/delete")>
    Public Function Delete(<FromBody> value As DTOContactMessage) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ContactMessage.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la ContactMessage")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la ContactMessage")
        End Try
        Return retval
    End Function

End Class

Public Class ContactMessagesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ContactMessages")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.ContactMessages.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ContactMessages")
        End Try
        Return retval
    End Function

End Class

