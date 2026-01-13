Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SepaTextController
    Inherits _BaseController


    <HttpPost>
    <Route("api/SepaText")>
    Public Function Update(<FromBody> value As DTOSepaText) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.SepaText.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la SepaText")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la SepaText")
        End Try
        Return retval
    End Function


End Class

Public Class SepaTextsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/SepaTexts")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.SepaTexts.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les SepaTexts")
        End Try
        Return retval
    End Function

End Class
