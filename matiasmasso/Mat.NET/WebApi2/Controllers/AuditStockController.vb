Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AuditStockController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AuditStock/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.AuditStock.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la AuditStock")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AuditStock")>
    Public Function Update(<FromBody> value As DTOAuditStock) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AuditStock.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la AuditStock")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la AuditStock")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/AuditStock/delete")>
    Public Function Delete(<FromBody> value As DTOAuditStock) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AuditStock.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la AuditStock")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la AuditStock")
        End Try
        Return retval
    End Function

End Class

Public Class AuditStocksController
    Inherits _BaseController

    <HttpGet>
    <Route("api/AuditStocks/{emp}/{year}")>
    Public Function All(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.AuditStocks.All(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les AuditStocks")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/AuditStocks")>
    Public Function Update(<FromBody> values As List(Of DTOAuditStock)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.AuditStocks.Update(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la AuditStock")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la AuditStock")
        End Try
        Return retval
    End Function

End Class
