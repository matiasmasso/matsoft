Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class LocalizedStringController
    Inherits _BaseController

    <HttpGet>
    <Route("api/LocalizedString/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.LocalizedString.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la LocalizedString")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/LocalizedString")>
    Public Function Update(<FromBody> value As DTOLocalizedString) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.LocalizedString.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la LocalizedString")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la LocalizedString")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/LocalizedString/delete")>
    Public Function Delete(<FromBody> value As DTOLocalizedString) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.LocalizedString.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la LocalizedString")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la LocalizedString")
        End Try
        Return retval
    End Function

End Class

Public Class LocalizedStringsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/LocalizedStrings")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.LocalizedStrings.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les LocalizedStrings")
        End Try
        Return retval
    End Function

End Class

