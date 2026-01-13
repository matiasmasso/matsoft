Public Class CustomerBasket

    Shared Function Model(oCustomer As DTOCustomer) As Models.CustomerBasket
        Dim retval As New Models.CustomerBasket
        retval.Customer = oCustomer
        retval.TarifaDtos = CustomerTarifaDtosLoader.ForCustomerBasket(oCustomer)
        Return retval
    End Function

    Shared Function Catalog(oUser As DTOUser, oCustomer As DTOCustomer, Optional oMgz As DTOMgz = Nothing, Optional oLang As DTOLang = Nothing) As DTOCustomerTarifa.Compact
        'Dim retval As New DTOCustomerTarifa.Compact

        Dim retval = BEBL.CustomerTarifa.Load(oCustomer, , oMgz, oLang)
        If oUser Is Nothing Then
            'grant full range for consumers
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager, DTORol.Ids.comercial, DTORol.Ids.operadora, DTORol.Ids.marketing, DTORol.Ids.salesManager, DTORol.Ids.comercial, DTORol.Ids.logisticManager
                'grant the full range of the customer's catalogue
                'retval = oCustomerTarifa
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                'grant the full range of the customer's catalogue
                'retval = oCustomerTarifa
                Case DTORol.Ids.rep
                    'remove the brands out of the rep portfolio
                    Dim oRep = BEBL.User.GetRep(oUser)
                    Dim oRepBrands = BEBL.RepProducts.CatalogueTree(oRep)
                    retval.Brands = retval.Brands.Where(Function(x) oRepBrands.Any(Function(y) y.Guid.Equals(x.Guid))).ToList
                Case Else
                    'grant full range for consumers
            End Select
        End If
        Return retval
    End Function

    Shared Function update(exs As List(Of Exception), oOrder As DTOPurchaseOrder) As Boolean
        Dim oUser = oOrder.UsrLog.usrCreated

        Dim oCcx = oOrder.Customer.CcxOrMe
        Dim oRepProducts = BEBL.RepProducts.All(oOrder.Emp)
        Dim oRepCliComs = BEBL.RepCliComs.All(oOrder.emp)
        Dim oCustomCosts = BEBL.PriceListItemsCustomer.Active(oCcx, oOrder.fch)
        Dim oTarifaDtos = BEBL.CustomerTarifaDtos.Active(oCcx)
        Dim oCliProductDtos = BEBL.CliProductDtos.All(oCcx)


        For Each item In oOrder.items
            With item
                .purchaseOrder = oOrder
                .pending = .Qty
                .Sku.IsNew = False
                BEBL.ProductSku.Load(.sku)
                .price = DTOProductSku.GetCustomerCost(item.sku, oCustomCosts, oTarifaDtos)
                If .price Is Nothing Then
                    .price = DTOAmt.factory
                Else
                    Dim DcDto As Decimal = 0
                    Dim oDto As DTOCliProductDto = .sku.CliProductDto(oCliProductDtos)
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
