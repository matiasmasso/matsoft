Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ImportUrlController
    Inherits _BaseController

    <HttpPost>
    <Route("api/ImportUrl/{user}")>
    Public Function Find(user As Guid, <FromBody> input As DTOImportUrl) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("user not allowed")
            Else
                Dim value As DTOImportUrl = BEBL.ImportUrl.Import(oUser, input.Url, exs)
                If exs.Count = 0 Then
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al importar la url")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al importar la url")
        End Try
        Return retval
    End Function
End Class
