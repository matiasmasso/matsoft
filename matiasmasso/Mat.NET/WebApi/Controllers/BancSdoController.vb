Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BancSdoController
    Inherits _BaseController


    <HttpPost>
    <Route("api/BancSdo")>
    Public Function Update(<FromBody> value As DTOBancSdo) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BancSdo.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el saldo del banc")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el saldo del banc")
        End Try
        Return retval
    End Function


End Class

Public Class BancSdosController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BancSdos/Last/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.BancSdos.Last(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els darrers saldos dels bancs")
        End Try
        Return retval
    End Function

End Class
