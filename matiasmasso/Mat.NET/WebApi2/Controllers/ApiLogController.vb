Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class ApiLogController
    Inherits _BaseController

    <HttpPost>
    <Route("api/apilog")>
    Public Function Update(<FromBody> value As DTOApiLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ApiLog.Log(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la ApiLog")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la ApiLog")
        End Try
        Return retval
    End Function
End Class
