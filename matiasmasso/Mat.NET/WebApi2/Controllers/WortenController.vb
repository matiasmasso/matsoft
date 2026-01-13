Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class WortenController
    Inherits _BaseController

    <HttpGet>
    <Route("api/worten/updateStocks/{emp}")>
    Public Async Function UpdateStocks(emp As DTOEmp.Ids) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = GetEmp(emp)
            Dim value As DTOTaskLog = Await BEBL.Integracions.Worten.Offers.UpdateStocks(exs, oEmp)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al actualitzar els stocks a Worten")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualitzar els stocks a Worten")
        End Try
        Return retval
    End Function

End Class
