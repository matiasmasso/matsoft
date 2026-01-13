Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class TxtController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Txt/{id}")>
    Public Function Find(id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Txt.Find(id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Txt")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Txt")>
    Public Function Update(<FromBody> value As DTOTxt) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Txt.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Txt")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Txt")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Txt/delete")>
    Public Function Delete(<FromBody> value As DTOTxt) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Txt.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Txt")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Txt")
        End Try
        Return retval
    End Function

End Class

