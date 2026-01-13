Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class WebMenuGroupController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebMenuGroup/{guid}/{rol}")>
    Public Function Find(guid As Guid, rol As DTORol.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRol As DTORol = Nothing
            If rol <> DTORol.Ids.NotSet Then oRol = New DTORol(rol)
            Dim value = BEBL.WebMenuGroup.Find(guid, oRol)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la WebMenuGroup")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WebMenuGroup")>
    Public Function Update(<FromBody> value As DTOWebMenuGroup) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebMenuGroup.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la WebMenuGroup")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la WebMenuGroup")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/WebMenuGroup/delete")>
    Public Function Delete(<FromBody> value As DTOWebMenuGroup) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.WebMenuGroup.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la WebMenuGroup")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la WebMenuGroup")
        End Try
        Return retval
    End Function

End Class

Public Class WebMenuGroupsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/WebMenuGroups/{user}/{JustActiveItems}")>
    Public Function All(user As Guid, JustActiveItems As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = Nothing
            If user <> Nothing Then oUser = BEBL.User.Find(user)
            Dim values = BEBL.WebMenuGroups.All(oUser, (JustActiveItems = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les WebMenuGroups")
        End Try
        Return retval
    End Function

End Class
