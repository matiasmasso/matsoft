Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CurExchangeRatesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/curExchangeRates/update")>
    Public Function Update() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CurExchangeRates.UpdateRates(exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els canvis de divisa")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/curExchangeRates/closest/{cur}/{year}/{month}/{day}")>
    Public Function Closest(cur As String, year As Integer, month As Integer, day As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCur = DTOCur.Factory(cur)
            Dim fch As New Date(year, month, day)
            Dim value = BEBL.CurExchangeRate.Closest(oCur, fch)
            retval = Request.CreateResponse(Of DTOCurExchangeRate)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els canvis de divisa")
        End Try

        Return retval
    End Function

End Class
