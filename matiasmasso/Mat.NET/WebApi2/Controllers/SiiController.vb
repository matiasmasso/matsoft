Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class SiiController
    Inherits _BaseController

    <HttpGet>
    <Route("api/sii/sendEmeses/{emp}")>
    Public Function sendEmeses(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp As DTOEmp = MyBase.GetEmp(emp)
            If BEBL.Sii.SendEmeses(oEmp, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RepCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RepCustomer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/sii/sendRebudes/{emp}")>
    Public Function SendRebudes(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp As DTOEmp = MyBase.GetEmp(emp)
            If BEBL.Sii.SendRebudes(oEmp, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RepCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RepCustomer")
        End Try
        Return retval
    End Function

End Class
