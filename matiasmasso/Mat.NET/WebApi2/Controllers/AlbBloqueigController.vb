Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AlbBloqueigController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AlbBloqueig/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.AlbBloqueig.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AlbBloqueig")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/AlbBloqueig/search/{contact}/{cod}")>
    Public Function Search(contact As Guid, cod As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim exs As New List(Of Exception)
            Dim value = BEBL.AlbBloqueig.Search(oContact, cod, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir el Bloqueig")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Bloqueig")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AlbBloqueig")>
    Public Function Update(<FromBody> value As DTOAlbBloqueig) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AlbBloqueig.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la AlbBloqueig")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la AlbBloqueig")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AlbBloqueig/delete")>
    Public Function Delete(<FromBody> value As DTOAlbBloqueig) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AlbBloqueig.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la AlbBloqueig")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la AlbBloqueig")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/AlbBloqueig/BloqueigEnd/{user}/{contact}/{cod}")>
    Public Function BloqueigEnd(user As Guid, contact As Guid, cod As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser As New DTOUser(user)
            Dim oContact As DTOContact = Nothing
            If Not contact.Equals(Guid.Empty) Then oContact = New DTOContact(contact)
            Dim oAlbBloqueig = DTOAlbBloqueig.Factory(oUser, oContact, cod)
            If BEBL.AlbBloqueig.BloqueigEnd(oAlbBloqueig, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desbloquejar")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desbloquejar")
        End Try
        Return retval
    End Function

End Class

Public Class AlbBloqueigsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AlbBloqueigs/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.AlbBloqueigs.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les AlbBloqueigs")
        End Try
        Return retval
    End Function

End Class
