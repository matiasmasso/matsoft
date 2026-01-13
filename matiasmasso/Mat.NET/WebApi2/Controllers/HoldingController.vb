Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class HoldingController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Holding/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Holding.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Holding")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Holding")>
    Public Function Update(<FromBody> value As DTOHolding) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Holding.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el Holding")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el Holding")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Holding/delete")>
    Public Function Delete(<FromBody> value As DTOHolding) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Holding.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Holding")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Holding")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Holding/pending/excel/{holding}")>
    Public Function PendingExcel(holding As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oHolding As New DTOHolding(holding)
            Dim oSheet = BEBL.Holding.PendingExcel(oHolding)
            retval = MyBase.HttpExcelResponseMessage(oSheet)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Holdings")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Holding/PendingPurchaseOrders/{holding}")>
    Public Function PendingPurchaseOrders(holding As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oHolding As New DTOHolding(holding)
            Dim values = BEBL.Holding.PendingPurchaseOrders(oHolding)
            retval = Request.CreateResponse(Of List(Of DTOPurchaseOrder))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Holdings")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Holding/ComandesECIDuplicades")>
    Public Function ComandesECIDuplicades() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Holding.ComandesECIDuplicades()
            retval = Request.CreateResponse(Of String)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Holdings")
        End Try
        Return retval
    End Function

End Class

Public Class HoldingsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Holdings/{emp}")>
    Public Function All(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Holdings.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Holdings")
        End Try
        Return retval
    End Function

End Class
