Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class XecsPresentacioController
    Inherits _BaseController


    <HttpPost>
    <Route("api/XecsPresentacio")>
    Public Function Update(<FromBody> value As DTOXecsPresentacio) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.XecsPresentacio.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la XecPresentacio")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la XecPresentacio")
        End Try
        Return retval
    End Function

End Class
