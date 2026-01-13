Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CobramentController
    Inherits _BaseController

    <HttpPost>
    <Route("api/Cobrament")>
    Public Function Update(<FromBody> value As DTOCobrament) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cobrament.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el Cobrament")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el Cobrament")
        End Try
        Return retval
    End Function


End Class
