Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ImmobleController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Immoble/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Immoble.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'Immoble")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Immoble")>
    Public Function Update(<FromBody> value As DTOImmoble) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Immoble.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'Immoble")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'Immoble")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Immoble/delete")>
    Public Function Delete(<FromBody> value As DTOImmoble) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Immoble.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar l'Immoble")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar l'Immoble")
        End Try
        Return retval
    End Function

End Class

Public Class ImmoblesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Immobles")>
    Public Function Bundle() As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = MyBase.GetUser(exs)
            If exs.Count = 0 Then
                Dim values = BEBL.Immobles.Bundle(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir els Immobles")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Immobles")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Immobles/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New Models.Base.IdNom(emp)
            Dim values = BEBL.Immobles.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Immobles")
        End Try
        Return retval
    End Function

End Class
