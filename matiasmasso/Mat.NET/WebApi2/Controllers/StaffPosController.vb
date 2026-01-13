Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StaffPosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StaffPos/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.StaffPos.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la StaffPos")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/StaffPos")>
    Public Function Update(<FromBody> value As DTOStaffPos) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffPos.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la StaffPos")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StaffPos")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/StaffPos/delete")>
    Public Function Delete(<FromBody> value As DTOStaffPos) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffPos.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la StaffPos")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la StaffPos")
        End Try
        Return retval
    End Function

End Class

Public Class StaffPossController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StaffPoss")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.StaffPoss.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StaffPoss")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/StaffPoss/delete")>
    Public Function Delete(<FromBody> values As List(Of DTOStaffPos)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffPoss.Delete(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la StaffPos")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la StaffPos")
        End Try
        Return retval
    End Function

End Class
