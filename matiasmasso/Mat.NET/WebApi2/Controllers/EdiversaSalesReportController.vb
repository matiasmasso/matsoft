Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaSalesReportController
    Inherits _BaseController



    <HttpPost>
    <Route("api/EdiversaSalesReport")>
    Public Function SalesReport(<FromBody> value As DTOSalesReport) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            If BEBL.EdiversaSalesReport.SalesReport(exs, value) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir el SalesReport")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el SalesReport")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/EdiversaSalesReport/Items")>
    Public Function Items(oStat As DTOStat) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EdiversaSalesReport.Items(oStat)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els items del EdiversaSalesReport")
        End Try
        Return retval
    End Function

End Class

Public Class EdiversaSalesReportsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaSalesReports/years/{emp}")>
    Public Function Years(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.EdiversaSalesReports.Years(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiversaSalesReports")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/EdiversaSalesReports/{emp}/{year}")>
    Public Function Years(emp As Integer, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim values = BEBL.EdiversaSalesReports.All(oEmp, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiversaSalesReports")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/EdiversaSalesReports/rebuildAll")>
    Public Function rebuildAll() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaSalesReports.RebuildAll(exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir les EdiversaSalesReports")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiversaSalesReports")
        End Try
        Return retval
    End Function

End Class
