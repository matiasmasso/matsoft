Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class DeliveryItemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/DeliveryItems/fromPurchaseOrderItem/{purchaseOrderItem}")>
    Public Function fromPurchaseOrderItem(purchaseOrderItem As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem(purchaseOrderItem)
            Dim values = BEBL.DeliveryItems.All(oPurchaseOrderItem)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DeliveryItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DeliveryItems/fromPurchaseOrder/{purchaseOrder}")>
    Public Function fromPurchaseOrder(purchaseOrder As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPurchaseOrder As New DTOPurchaseOrder(purchaseOrder)
            Dim values = BEBL.DeliveryItems.All(oPurchaseOrder)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DeliveryItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DeliveryItems/fromProduct/{product}")>
    Public Function fromProduct(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.DeliveryItems.All(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DeliveryItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DeliveryItems/fromCustomer/{customer}/{mgz}")>
    Public Function fromCustomer(customer As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = DTOBaseGuid.Opcional(Of DTOCustomer)(customer)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.DeliveryItems.All(oCustomer, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DeliveryItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DeliveryItems/fromProveidor/{proveidor}/{mgz}")>
    Public Function fromProveidor(proveidor As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.DeliveryItems.All(oProveidor, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les DeliveryItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/DeliveryItems/factory/{contact}/{cod}/{mgz}")>
    Public Function Factory(contact As Guid, cod As DTOPurchaseOrder.Codis, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.DeliveryItems.Factory(oContact, cod, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al generar les DeliveryItems")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/DeliveryItems/SetIncentius/{user}/{ccx}")>
    Public Function SetIncentius(user As Guid, ccx As Guid, <FromBody> values As List(Of DTODeliveryItem)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUser = BEBL.User.Find(user)
            Dim oCcx = BEBL.Customer.Find(ccx)
            If BEBL.DeliveryItems.SetIncentius(exs, oCcx, values, oUser) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al asignar els incentius")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al asignar els incentius")
        End Try
        Return retval
    End Function

End Class
