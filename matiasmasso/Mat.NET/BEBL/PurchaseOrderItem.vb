
Public Class PurchaseOrderItem

    Shared Function BundleItems(oParent As DTOPurchaseOrderItem, oCustomer As DTOCustomer) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        Dim oBundleSkus = ProductSkuBundlesLoader.All(oParent.sku, oCustomer)
        For Each oBundleSku In oBundleSkus
            Dim item As New DTOPurchaseOrderItem
            With item
                .sku = oBundleSku.Sku
                .price = oBundleSku.Sku.price
                .dto = oBundleSku.Sku.customerDto
                .qty = oBundleSku.Qty
                '.repCom = BEBL.RepCom.GetRepCom(oEmp, oOrder.customer, oSkuBundle.Sku, oOrder.fch, oRepProducts, exs:=exs)
            End With
            retval.Add(item)
        Next
        Return retval
    End Function


    Shared Function DeliveryItems(value As DTOPurchaseOrderItem) As List(Of DTODeliveryItem)
        Return PurchaseOrderItemLoader.DeliveryItems(value)
    End Function

    Shared Function UpdateRepCom(exs As List(Of Exception), value As DTOPurchaseOrderItem) As Boolean
        Return PurchaseOrderItemLoader.UpdateRepCom(value, exs)
    End Function

    Shared Function ResetPendingQty(exs As List(Of Exception), value As DTOPurchaseOrderItem) As Boolean
        Return PurchaseOrderItemLoader.ResetPendingQty(value, exs)
    End Function

    Shared Function UnitatsSortides(value As DTOPurchaseOrderItem) As Integer
        Return PurchaseOrderItemLoader.UnitatsSortides(value)
    End Function

    Shared Function GetDiscount(oSku As DTOProductSku, oCustomer As DTOCustomer) As Decimal
        Dim oCliProductDtos As List(Of DTOCliProductDto) = BEBL.CliProductDtos.All(oCustomer)
        Dim retval As Decimal = GetDiscount(oSku, oCustomer, oCliProductDtos)
        Return retval
    End Function

    Shared Function GetDiscount(oSku As DTOProductSku, oCustomer As DTOCustomer, oDtos As List(Of DTOCliProductDto)) As Decimal
        Dim retval As Decimal

        Dim oDto As DTOCliProductDto = oSku.CliProductDto(oDtos)
        If oDto IsNot Nothing Then
            retval = oDto.Dto
        End If
        Return retval
    End Function

    Shared Function SuggestedRepCom(oUser As DTOUser, value As DTOPurchaseOrderItem, oRepProducts As List(Of DTORepProduct), oRepCliComs As List(Of DTORepCliCom), exs As List(Of Exception)) As DTORepCom
        Dim oCustomer As DTOCustomer = value.PurchaseOrder.Contact
        Dim oProduct As DTOProduct = value.Sku
        Dim DtFch As Date = value.PurchaseOrder.Fch
        Dim retval As DTORepCom = RepCom.GetRepCom(oUser.Emp, oCustomer, oProduct, DtFch, oRepProducts, oRepCliComs)
        Return retval
    End Function


    Shared Function BundleItemsFactory(item As DTOPurchaseOrderItem, oEmp As DTOEmp, oCustomCosts As List(Of DTOPricelistItemCustomer), oTarifaDtos As List(Of DTOCustomerTarifaDto), oCliProductDtos As List(Of DTOCliProductDto), Optional oRepProducts As List(Of DTORepProduct) = Nothing) As List(Of DTOPurchaseOrderItem)
        Dim retval As New List(Of DTOPurchaseOrderItem)
        item.sku.bundleSkus = BEBL.ProductSku.BundleSkus(item.sku)
        Dim oOrder = item.purchaseOrder
        Dim oSku = item.sku
        For Each oSkuBundle As DTOSkuBundle In oSku.bundleSkus
            Dim oRepCom As DTORepCom = Nothing
            If oOrder.cod = DTOPurchaseOrder.Codis.client Then
                oRepCom = BEBL.RepCom.GetRepCom(oEmp, oOrder.customer, oSkuBundle.Sku, oOrder.fch, oRepProducts)
            End If
            Dim oBundleChild = DTOPurchaseOrderItem.BundleItemFactory(oSkuBundle, item, oEmp, oCustomCosts, oTarifaDtos, oCliProductDtos, oRepCom)
            retval.Add(oBundleChild)
        Next
        Return retval
    End Function

End Class


Public Class PurchaseOrderItems
    Shared Function All(oContact As DTOContact) As List(Of DTOPurchaseOrderItem)
        Return PurchaseOrderItemsLoader.All(oContact)
    End Function

    Shared Function All(oProduct As DTOProduct) As List(Of DTOPurchaseOrderItem)
        Return PurchaseOrderItemsLoader.All(oProduct)
    End Function

    Shared Function Pending(oEmp As DTOEmp, oSku As DTOProductSku, oCod As DTOPurchaseOrder.Codis, Optional oMgz As DTOMgz = Nothing) As List(Of DTOPurchaseOrderItem)
        If oMgz Is Nothing Then oMgz = oEmp.Mgz
        Return PurchaseOrderItemsLoader.Pending(oSku, oCod, oMgz)
    End Function

    Shared Function Pending(oContact As DTOBaseGuid, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz, oCustomerGroupLevel As DTOCustomer.GroupLevels) As List(Of DTOPurchaseOrderItem)
        Return PurchaseOrderItemsLoader.Pending(oContact, oCod, oMgz, oCustomerGroupLevel)
    End Function

    Shared Function Pending(oUser As DTOUser, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz) As List(Of DTOPurchaseOrderItem)
        Return PurchaseOrderItemsLoader.Pending(oUser, oCod, oMgz)
    End Function

    Shared Function Descuadres(oUser As DTOUser) As List(Of DTOPurchaseOrderItem)
        Return PurchaseOrderItemsLoader.Descuadres(oUser)
    End Function

    Shared Function PendentsDeLiquidacioRep(oEmp As DTOEmp) As List(Of DTOPurchaseOrderItem)
        Dim retval As List(Of DTOPurchaseOrderItem) = PurchaseOrderItemsLoader.PendentsDeLiquidacioRep(oEmp)
        Return retval
    End Function

    Shared Function RecuperaLiniesDeSortides(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Integer
        Return PurchaseOrderItemsLoader.RecuperaLiniesDeSortides(exs, oPurchaseOrder)
    End Function

    Shared Function UpdateEtd(exs As List(Of Exception), items As List(Of DTOPurchaseOrderItem)) As Boolean
        Return PurchaseOrderItemsLoader.UpdateEtd(exs, items)
    End Function

    Shared Function Delete(exs As List(Of Exception), items As List(Of DTOPurchaseOrderItem)) As Boolean
        Return PurchaseOrderItemsLoader.Delete(items, exs)
    End Function

    Shared Function Kpis(oEmp As DTOEmp, yearFrom As Integer) As List(Of DTOKpi)
        Return PurchaseOrderItemsLoader.Kpis(oEmp, yearFrom)
    End Function

    Shared Function PncCustomSkuEans(oPncGuids As List(Of Guid)) As List(Of DTOEdiSku)
        Return PurchaseOrderItemsLoader.PncCustomSkuEans(oPncGuids)
    End Function
End Class
