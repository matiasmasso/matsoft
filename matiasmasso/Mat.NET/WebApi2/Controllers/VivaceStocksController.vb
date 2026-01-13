Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class VivaceStocksController
    Inherits _BaseController

    <HttpGet>
    <Route("api/VivaceStocks/{emp}/{fch}")>
    Public Function All(emp As DTOEmp.Ids, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.VivaceStocks.All(oEmp, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Stocks")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/VivaceStocks/Fchs")>
    Public Function Fchs() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.VivaceStocks.Fchs()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les dates dels Stocks")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/VivaceStocks/{emp}/{fch}")>
    Public Function Update(emp As DTOEmp.Ids, fch As Date, <FromBody> values As List(Of DTOVivaceStock)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            If BEBL.VivaceStocks.Update(exs, oEmp, fch, values) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al desar els Stocks")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al desar els Stocks")
        End Try
        Return retval
    End Function


End Class
