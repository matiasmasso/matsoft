Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CurController
    Inherits _BaseController



    <HttpPost>
    <Route("api/Cur")>
    Public Function Update(<FromBody> value As DTOCur) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Cur.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la divisa")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la divisa")
        End Try
        Return retval
    End Function
End Class


Public Class CursController
    Inherits _BaseController

    <HttpGet>
    <Route("api/curs")>
    Public Function GetValues() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Curs.All
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex)
        End Try
        Return retval
    End Function



End Class
