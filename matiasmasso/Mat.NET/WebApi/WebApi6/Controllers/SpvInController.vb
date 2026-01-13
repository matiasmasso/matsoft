Imports System.Net
Imports System.Web.Http
Imports System.Net.Http

Namespace Controllers
    Public Class SpvInController
        Inherits ApiController

        <HttpPost>
        <Route("api/spvin/update")>
        Public Function Spvin_Update(oSpvIn As DTOSpvIn) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Dim exs As New List(Of Exception)
            If BLLSpvIn.Update(oSpvIn, exs) Then
                retval = Request.CreateResponse(Of DTOSpvIn)(HttpStatusCode.OK, oSpvIn)
            Else
                retval = New HttpResponseMessage(HttpStatusCode.InternalServerError)
                retval.Content = New StringContent(exs.First.Message)
                retval.ReasonPhrase = "error al grabar la entrada de mercancia per reparar"
            End If
            Return retval
        End Function


    End Class
End Namespace


