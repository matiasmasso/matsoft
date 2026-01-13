Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MaybornController
    Inherits _BaseController


    <HttpGet>
    <Route("api/mayborn/sales/{year}")>
    Public Function Sales(year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.Mayborn.sales(year)
            retval = Request.CreateResponse(Of List(Of DTOProductMonthQtySalepoint))(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les vendes")
        End Try
        Return retval
    End Function

End Class


