Public Class DeliveryItems
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oPurchaseOrderItem As DTOPurchaseOrderItem) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "DeliveryItems/fromPurchaseOrderItem", oPurchaseOrderItem.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "DeliveryItems/fromPurchaseOrder", oPurchaseOrder.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of Models.SkuInOutModel)
        Return Await Api.Fetch(Of Models.SkuInOutModel)(exs, "DeliveryItems/fromProduct", oProduct.Guid.ToString)
    End Function

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer, oMgz As DTOMgz) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "DeliveryItems/fromCustomer", OpcionalGuid(oCustomer), oMgz.Guid.ToString())
    End Function

    Shared Async Function All(exs As List(Of Exception), oProveidor As DTOProveidor, oMgz As DTOMgz) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "DeliveryItems/fromProveidor", oProveidor.Guid.ToString, oMgz.Guid.ToString())
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oContact As DTOContact, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz) As Task(Of List(Of DTODeliveryItem))
        Dim retval = Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "DeliveryItems/factory", oContact.Guid.ToString, oCod, oMgz.Guid.ToString())

        For Each item In retval
            Select Case oCod
                Case DTOPurchaseOrder.Codis.proveidor
                    item.purchaseOrderItem.purchaseOrder.proveidor = oContact
                Case DTOPurchaseOrder.Codis.client
                    item.PurchaseOrderItem.PurchaseOrder.Customer = oContact
            End Select
        Next
        Return retval
    End Function

    Shared Async Function SetIncentius(exs As List(Of Exception), oCcx As DTOCustomer, values As List(Of DTODeliveryItem), oUser As DTOUser) As Task(Of List(Of DTODeliveryItem))
        Dim retval = Await Api.Execute(Of List(Of DTODeliveryItem), List(Of DTODeliveryItem))(values, exs, "DeliveryItems/SetIncentius", oUser.Guid.ToString, oCcx.Guid.ToString())
        Return retval
    End Function



End Class
