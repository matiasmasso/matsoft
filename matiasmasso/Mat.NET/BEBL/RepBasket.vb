Public Class RepBasket
    Shared Function update(exs As List(Of Exception), oOrder As DTOPurchaseOrder) As Boolean
        Dim oUser = oOrder.UsrLog.usrCreated
        Dim orep = BEBL.User.GetRep(oUser)
        Dim oCcx = oOrder.Customer.CcxOrMe
        Dim oRepProducts = BEBL.RepProducts.All(orep)
        Dim oRepCliComs = BEBL.RepCliComs.All(oOrder.emp)
        Dim oCustomCosts = BEBL.PriceListItemsCustomer.Active(oCcx, oOrder.fch)
        Dim oTarifaDtos = BEBL.CustomerTarifaDtos.Active(oCcx)
        Dim oCliProductDtos = BEBL.CliProductDtos.All(oCcx)


        For Each item In oOrder.items
            With item
                .purchaseOrder = oOrder
                .pending = .Qty
                .Sku.IsNew = False
                BEBL.ProductSku.Load(.Sku)
                .Price = DTOProductSku.getCustomerCost(item.Sku, oCustomCosts, oTarifaDtos)
                If .Price Is Nothing Then
                    .Price = DTOAmt.Factory
                Else
                    Dim DcDto As Decimal = 0
                    Dim oDto As DTOCliProductDto = .Sku.cliProductDto(oCliProductDtos)
                    If oDto IsNot Nothing Then DcDto = oDto.Dto
                End If

                If .Sku.IsBundle Then
                    item.Bundle = BEBL.PurchaseOrderItem.BundleItemsFactory(item, oOrder.Emp, oCustomCosts, oTarifaDtos, oCliProductDtos, oRepProducts)
                End If
                .repCom = BEBL.PurchaseOrderItem.SuggestedRepCom(oUser, item, oRepProducts, oRepCliComs, exs)
            End With
        Next

        Dim retval = BEBL.PurchaseOrder.Update(oOrder, exs)
        Return retval
    End Function

End Class
