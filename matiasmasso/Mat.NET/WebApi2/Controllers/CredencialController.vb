Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CredencialController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Credencial/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Credencial.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Credencial")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Credencial2")> 'Credencial1 es per iMat i DUI
    Public Function Update(<FromBody> value As DTOCredencial) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Credencial.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Credencial")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Credencial")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Credencial/delete")>
    Public Function Delete(<FromBody> value As DTOCredencial) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Credencial.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Credencial")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Credencial")
        End Try
        Return retval
    End Function

End Class


Public Class CredencialsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Credencials/{user}")>
    Public Function All(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Credencials.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Credencials")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Credencials/owners/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Credencials.Owners(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Credencials")
        End Try
        Return retval
    End Function

End Class