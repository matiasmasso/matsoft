Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StockController
    Inherits _BaseController

    <HttpGet>
    <Route("api/stock/available/{sku}/{mgz}")>
    Public Function available(sku As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim oMgz As New DTOMgz(mgz)
            Dim value = BEBL.Stock.Available(oSku, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'stock")
        End Try
        Return retval
    End Function

End Class

Public Class StocksController
    Inherits _BaseController

    <HttpGet>
    <Route("api/stocks/{mgz}/{category}")>
    Public Function All(mgz As Guid, category As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim oCategory = DTOBaseGuid.Opcional(Of DTOProductCategory)(category)
            Dim values = BEBL.Stocks.All(oMgz, oCategory)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els stocks")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/stocks/{user}")> '---------------------------- integracio clients externs
    Public Function GetStocks(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As DTOUser = BEBL.User.Find(user)
            Dim values = BEBL.Stocks.All(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els stocks")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("stocks")> '---------------------------- integracio clients externs
    Public Function GetStocks_ApiKey() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = MyBase.GetUser(exs) 'As DTOUser = BEBL.User.Find(user)
            If exs.Count = 0 Then
                Dim values = BEBL.Stocks.All(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "ApiKey auth exception")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els stocks")
        End Try
        Return retval
    End Function

End Class
