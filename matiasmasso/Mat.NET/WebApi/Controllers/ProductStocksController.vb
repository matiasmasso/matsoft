Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductStocksController
    Inherits _BaseController


    <HttpGet>
    <Route("api/ProductStocks/FromUserOrCustomer/{emp}/{userorcustomer}/{mgz}")>
    Public Function FromUserOrCustomer(emp As DTOEmp.Ids, userorcustomer As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oUserOrCustomer As New DTOBaseGuid(userorcustomer)
            Dim values = BEBL.ProductStocks.FromUserOrCustomer(oEmp, oUserOrCustomer, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductStocks")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductStocks/Skus/{mgz}")>
    Public Function Skus(mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim values = BEBL.ProductStocks.Skus(oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductStocks")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ProductStocks/Custom/{mgz}")>
    Public Function Custom(emp As DTOEmp.Ids, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.ProductStocks.Custom(oEmp, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les ProductStocks")
        End Try
        Return retval
    End Function

End Class
