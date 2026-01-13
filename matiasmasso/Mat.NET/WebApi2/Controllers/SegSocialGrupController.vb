Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class SegSocialGrupController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SegSocialGrup/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.SegSocialGrup.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la SegSocialGrup")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/SegSocialGrup")>
    Public Function Update(<FromBody> value As DTOSegSocialGrup) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SegSocialGrup.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la SegSocialGrup")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la SegSocialGrup")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/SegSocialGrup/delete")>
    Public Function Delete(<FromBody> value As DTOSegSocialGrup) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SegSocialGrup.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la SegSocialGrup")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la SegSocialGrup")
        End Try
        Return retval
    End Function

End Class

Public Class SegSocialGrupsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SegSocialGrups")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SegSocialGrups.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les SegSocialGrups")
        End Try
        Return retval
    End Function

End Class

