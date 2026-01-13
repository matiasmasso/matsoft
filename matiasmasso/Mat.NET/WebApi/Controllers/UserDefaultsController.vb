Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class UserDefaultsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/UserDefaults/{user}/{cod}")>
    Public Function GetValue(user As Guid, cod As DTOUserDefault.Cods) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim value = BEBL.UserDefaults.GetValue(oUser, cod)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la UserDefaults")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/UserDefaults")>
    Public Function Update(<FromBody> value As DTOUserDefault) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)

            If BEBL.UserDefaults.SetValue(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la UserDefaults")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la UserDefaults")
        End Try
        Return retval
    End Function

End Class
