Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class FourMomsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/FourMoms/SalePoints/{fch}")>
    Public Function SalePoints(fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.FourMoms.SalePoints(fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Salepoints")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/FourMoms/Sales/{fch}")>
    Public Function Sales(fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.FourMoms.Sales(fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Sales")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/FourMoms/Stocks/{fch}")>
    Public Function Stocks(fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
            Dim oMgz = oEmp.Mgz
            Dim values = BEBL.FourMoms.Stocks(oMgz, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Stocks")
        End Try
        Return retval
    End Function

End Class

