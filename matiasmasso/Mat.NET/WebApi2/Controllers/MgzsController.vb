Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class MgzController
    Inherits _BaseController

    <HttpGet>
    <Route("api/mgz/inventory/{mgz}/{fch}")>
    Public Function Inventory(mgz As Guid, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim values = BEBL.Mgz.Inventory(oMgz, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/mgz/inventari/fromProveidor/{mgz}/{proveidor}")>
    Public Function fromProveidor(mgz As Guid, proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.Mgz.Inventari(oProveidor, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/mgz/stocks/{user}/{mgz}")>
    Public Function GetStocks(user As Guid, mgz As Guid) As List(Of DTOProductSku)
        Dim oUser As DTOUser = BEBL.User.Find(user)
        Dim oMgz As New DTOMgz(mgz)
        Dim retval As List(Of DTOProductSku) = BEBL.Mgz.Stocks(oUser.Emp, oUser, oMgz)
        Return retval
    End Function

    <HttpGet>
    <Route("api/mgz/stocks/custom/{customer}/{mgz}")>
    Public Function GetCustomStocks(customer As Guid, mgz As Guid) As List(Of DTOCustomerProduct)
        Dim oCustomer As DTOCustomer = BEBL.Customer.Find(customer)
        Dim oMgz As New DTOMgz(mgz)
        Dim retval As List(Of DTOCustomerProduct) = BEBL.Mgz.StocksCustom(oCustomer, oMgz)
        Return retval
    End Function

    <HttpGet>
    <Route("api/mgz/deliveryItems/{mgz}/{year}")>
    Public Function deliveryItems(mgz As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim values = BEBL.Mgz.DeliveryItems(oMgz, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/mgz/SetPrecioMedioCoste/{mgz?}")>
    Public Function SetPrecioMedioCoste(Optional mgz As Nullable(Of Guid) = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oMgz As DTOMgz = Nothing
            If mgz Is Nothing Then
                Dim oEmp = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso)
                oMgz = oEmp.Mgz
            Else
                oMgz = New DTOMgz(mgz)
            End If

            If BEBL.Mgz.SetPrecioMedioCoste(oMgz, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RepCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RepCustomer")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/mgz/SetPrecioMedioCoste/{mgz}/{sku}")>
    Public Function SetPrecioMedioCosteSku(mgz As Guid, sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oMgz As New DTOMgz(mgz)
            Dim oSku As New DTOProductSku(sku)
            If BEBL.Mgz.SetPrecioMedioCoste(oSku, oMgz, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la RepCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la RepCustomer")
        End Try
        Return retval
    End Function


End Class

Public Class MgzsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/mgzs/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Mgzs.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els magatzems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/mgzs/FromSku/{sku}")>
    Public Function FromSku(sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oSku As New DTOProductSku(sku)
            Dim values = BEBL.Mgzs.All(oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els magatzems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/mgzs/actius/{emp}/{fch}")>
    Public Function All(emp As Integer, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Mgzs.Actius(oEmp, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els magatzems")
        End Try
        Return retval
    End Function

End Class