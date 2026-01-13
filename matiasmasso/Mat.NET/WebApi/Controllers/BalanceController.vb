Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class BalanceController
    Inherits _BaseController

    <HttpGet>
    <Route("api/balance/tree/{emp}/{yearFrom}")>
    Public Function Tree(emp As Integer, yearFrom As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim oYearMonthFrom = New DTOYearMonth(yearFrom, 1).AddMonths(-1) 'inclou el cap d'any anterior
            Dim oYearMonthTo = DTOYearMonth.current
            Dim value = BEBL.Balance.Tree(oEmp, oYearMonthFrom, oYearMonthTo)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el balanç")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/balance/sumasysaldos/{emp}/{year}/{fch}")>
    Public Function Find(emp As Integer, year As Integer, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim value = BEBL.Balance.SumasYSaldos(oExercici, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els sumes i saldos")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/balance/cce/{emp}/{cta}/{fch}")>
    Public Function Cce(emp As Integer, cta As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCta As New DTOPgcCta(cta)
            Dim value = BEBL.Balance.Cce(oEmp, oCta, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els sumes i saldos")
        End Try
        Return retval
    End Function

End Class
