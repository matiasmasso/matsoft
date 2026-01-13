Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PurchaseOrderItemController
    Inherits _BaseController

    <HttpGet>
    <Route("api/purchaseOrderItem/DeliveryItems/{purchaseOrderItem}")>
    Public Function Deliveries(purchaseOrderItem As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem(purchaseOrderItem)
            Dim values = BEBL.PurchaseOrderItem.DeliveryItems(oPurchaseOrderItem)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les sortides de una linia de comanda")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/purchaseOrderItem/BundleItems/{customer}")>
    Public Function BundleItems(customer As Guid, <FromBody> oPurchaseOrderItem As DTOPurchaseOrderItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCustomer As New DTOCustomer(customer)
            Dim values = BEBL.PurchaseOrderItem.BundleItems(oPurchaseOrderItem, oCustomer)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of List(Of DTOPurchaseOrderItem))(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir els bundles")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els bundles")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/purchaseOrderItem/UpdateRepCom")>
    Public Function UpdateRepCom(<FromBody> oPurchaseOrderItem As DTOPurchaseOrderItem) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.PurchaseOrderItem.UpdateRepCom(exs, oPurchaseOrderItem)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al actualitzar la comisió de representant")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualitzar la comisió de representant")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseOrderItem/ResetPendingQty/{purchaseOrderItem}")>
    Public Function ResetPendingQty(purchaseOrderItem As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem(purchaseOrderItem)
            If BEBL.PurchaseOrderItem.ResetPendingQty(exs, oPurchaseOrderItem) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al actualitzar les partides pendents de enviar")
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualitzar les partides pendents de enviar")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseOrderItem/UnitatsSortides/{purchaseOrderItem}")>
    Public Function UnitatsSortides(purchaseOrderItem As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oPurchaseOrderItem As New DTOPurchaseOrderItem(purchaseOrderItem)

            Dim value = BEBL.PurchaseOrderItem.UnitatsSortides(oPurchaseOrderItem)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les unitats sortides de la comanda")
        End Try
        Return retval
    End Function

End Class

Public Class PurchaseOrderItemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/purchaseOrderItems/fromContact/{contact}")>
    Public Function fromContact(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim values = BEBL.PurchaseOrderItems.All(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseOrderItems/fromProduct/{product}")>
    Public Function fromProduct(product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim values = BEBL.PurchaseOrderItems.All(oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseOrderItems/Pending/FromSku/{emp}/{sku}/{cod}/{mgz}")>
    Public Function PendingFromSku(emp As DTOEmp.Ids, sku As Guid, cod As DTOPurchaseOrder.Codis, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oSku As New DTOProductSku(sku)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.PurchaseOrderItems.Pending(oEmp, oSku, cod, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes pendents")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseOrderItems/Pending/FromContact/{contact}/{cod}/{mgz}/{CustomerGroupLevel}")>
    Public Function PendingFromContact(contact As Guid, cod As DTOPurchaseOrder.Codis, mgz As Guid, CustomerGroupLevel As DTOCustomer.GroupLevels) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.PurchaseOrderItems.Pending(oContact, cod, oMgz, CustomerGroupLevel)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes pendents")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseOrderItems/Pending/FromUser/{user}/{cod}/{mgz}")>
    Public Function PendingFromUser(user As Guid, cod As DTOPurchaseOrder.Codis, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oMgz = DTOBaseGuid.Opcional(Of DTOMgz)(mgz)
            Dim values = BEBL.PurchaseOrderItems.Pending(oUser, cod, oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les comandes pendents")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseOrderItems/Descuadres/{user}")>
    Public Function Descuadres(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.PurchaseOrderItems.Descuadres(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els descuadres")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/purchaseOrderItems/PendentsDeLiquidacioRep/{emp}")>
    Public Function PendentsDeLiquidacioRep(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.PurchaseOrderItems.PendentsDeLiquidacioRep(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els pendents de liquidar a rep")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseOrderItems/RecuperaLiniesDeSortides/{purchaseOrder}")>
    Public Function RecuperaLiniesDeSortides(purchaseOrder As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oPurchaseOrder = New DTOPurchaseOrder(purchaseOrder)
            Dim value = BEBL.PurchaseOrderItems.RecuperaLiniesDeSortides(exs, oPurchaseOrder)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al llegir els pendents de liquidar a rep")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els pendents de liquidar a rep")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/purchaseOrderItems/UpdateEtd")>
    Public Function UpdateEtd(<FromBody> oPurchaseOrderItems As List(Of DTOPurchaseOrderItem)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PurchaseOrderItems.UpdateEtd(exs, oPurchaseOrderItems) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al actualitzar les dates previstes de sortida")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al actualitzar les dates previstes de sortida")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/purchaseOrderItems/Delete")>
    Public Function Delete(<FromBody> oPurchaseOrderItems As List(Of DTOPurchaseOrderItem)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PurchaseOrderItems.Delete(exs, oPurchaseOrderItems) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les linies de comanda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les linies de comanda")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/purchaseOrderItems/kpis/{emp}/{yearfrom}")>
    Public Function kpis(emp As DTOEmp.Ids, yearfrom As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.PurchaseOrderItems.Kpis(oEmp, yearfrom)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els pendents de liquidar a rep")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/purchaseOrderItems/PncCustomSkuEans")>
    Public Function PncCustomSkuEans(<FromBody> oGuids As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.PurchaseOrderItems.PncCustomSkuEans(oGuids)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir els Eans")
        End Try
        Return retval
    End Function

    '

End Class
