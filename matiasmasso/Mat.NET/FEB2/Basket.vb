Public Class Basket
    Inherits _FeblBase

    Shared Function Factory(oOrder As DTOPurchaseOrder) As DTOBasket
        Dim retval As New DTOBasket
        With retval
            .customer = oOrder.Contact.Guid
            .customerUrl = FEB2.Contact.Url(oOrder.Contact)
            .lines = New List(Of DTOBasketLine)
            For Each item As DTOPurchaseOrderItem In oOrder.Items
                .lines.Add(DTOBasketLine.Factory(item))
            Next
            .suma = .lines.Sum(Function(x) x.Amount)
            .sumaFormatted = DTOAmt.CurFormatted(DTOAmt.Factory(.suma))
        End With
        Return retval
    End Function
    Shared Async Function Brands(exs As List(Of Exception), basket As DTOBasket) As Task(Of List(Of DTOGuidNom))
        Dim oCustomer = Await FEB2.Customer.Find(exs, basket.customer)
        Dim oCcx As DTOCustomer = FEB2.Customer.CcxOrMe(exs, oCustomer)
        Dim oBrands = Await FEB2.ProductBrands.All(exs, oCcx)
        Dim retval As New List(Of DTOGuidNom)
        For Each item As DTOProductBrand In oBrands
            retval.Add(New DTOGuidNom(item.Guid, item.nom.Esp))
        Next
        Return retval
    End Function


    Shared Async Function Categories(exs As List(Of Exception), basket As String, brand As Guid, stockOnly As Boolean, Optional includeHidden As Boolean = False) As Task(Of List(Of DTOGuidNom))
        Dim oBasket As DTOBasket = GetModelBasket(exs, basket)
        Dim oCustomer = Await FEB2.Customer.Find(exs, oBasket.customer)
        Dim oCcx = FEB2.Customer.CcxOrMe(exs, oCustomer)
        Dim oCategories = Await FEB2.ProductCategories.All(exs, oCcx)
        oCategories = oCategories.Where(Function(x) x.Brand.Guid.Equals(brand)).ToList
        Dim retval As New List(Of DTOGuidNom)
        For Each item As DTOProductCategory In oCategories
            If includeHidden Or item.hideUntil < DateTime.Today Then
                retval.Add(New DTOGuidNom(item.Guid, item.Nom.Esp))
            End If
        Next
        Return retval
    End Function

    Shared Async Function Skus(exs As List(Of Exception), oEmp As DTOEmp, basket As String, category As Guid, stockOnly As Boolean, Optional includeHidden As Boolean = False) As Task(Of List(Of DTOGuidNom))
        Dim oBasket As DTOBasket = GetModelBasket(exs, basket)
        Dim oCustomer = Await FEB2.Customer.Find(exs, oBasket.customer)
        Dim oCcx As DTOCustomer = FEB2.Customer.CcxOrMe(exs, oCustomer)
        Dim oCategory As New DTOProductCategory(category)
        Dim retval As List(Of DTOGuidNom) = Await FEB2.ProductSkus.GuidNoms(exs, oCategory, oCcx, oEmp.Mgz, stockOnly, includeHidden)
        Return retval
    End Function

    Shared Function GetModelBasket(exs As List(Of Exception), basket As String) As DTOBasket
        Dim retval As DTOBasket = Nothing
        If basket > "" Then
            retval = JsonHelper.DeSerialize(Of DTOBasket)(basket, exs)
        End If
        Return retval
    End Function

    Shared Function GetModelBasketLine(exs As List(Of Exception), value As String) As DTOBasketLine
        Dim retval As DTOBasketLine = Nothing
        If value > "" Then
            retval = JsonHelper.DeSerialize(Of DTOBasketLine)(value, exs)
        End If
        Return retval
    End Function

    Shared Function Serialize(exs As List(Of Exception), oBasket As DTOBasket) As String
        Dim retval As String = JsonHelper.Stringify(oBasket)
        Return retval
    End Function

    Shared Async Function GetPurchaseOrder(oBasket As DTOBasket, oUser As DTOUser, oSrc As DTOPurchaseOrder.Sources) As Task(Of DTOPurchaseOrder)
        Dim exs As New List(Of Exception)
        Dim oRep As DTORep = Nothing
        Dim oRepProducts As New List(Of DTORepProduct)
        Dim oRepCliComs As New List(Of DTORepCliCom)
        Dim oCustomer = Await FEB2.Customer.Find(exs, oBasket.customer)
        Dim oCcx = oCustomer.CcxOrMe()
        Dim oCustomCosts = Await FEB2.PriceListItemsCustomer.Active(exs, oCcx, DateTime.Today)
        Dim oTarifaDtos = Await FEB2.CustomerTarifaDtos.Active(exs, oCcx)
        Dim oCliProductDtos = Await FEB2.CliProductDtos.All(oCcx, exs)

        FEB2.User.Load(exs, oUser)
        Select Case oUser.Rol.id
            Case DTORol.Ids.Comercial, DTORol.Ids.Rep
                oRep = Await FEB2.User.GetRep(oUser, exs)
                oRepProducts = Await FEB2.RepProducts.All(exs, oUser.Emp, oRep, False)
                oRepCliComs = Await FEB2.RepCliComs.All(exs, oUser.Emp)
        End Select

        Dim retval = DTOPurchaseOrder.Factory(oCustomer, oUser, DateTime.Today, oSrc)
        With retval
            .Concept = oBasket.concept
            .TotJunt = oBasket.totjunt
            .Obs = oBasket.obs
            .UsrLog = DTOUsrLog.Factory(oUser)
            Dim DtFchMin As Date
            If Date.TryParseExact(oBasket.fchmin, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DtFchMin) Then
                If DtFchMin > DateTime.Today Then
                    .fchDeliveryMin = DtFchMin
                End If
            End If

        End With

        For Each oBasketLine As DTOBasketLine In oBasket.lines
            Dim item As New DTOPurchaseOrderItem()
            With item
                .PurchaseOrder = retval
                .Qty = oBasketLine.qty
                .Pending = .qty
                .sku = Await FEB2.ProductSku.Find(exs, oBasketLine.sku)
                .price = DTOAmt.Factory(oBasketLine.price)
                .Dto = oBasketLine.dto
                .RepCom = Await FEB2.PurchaseOrderItem.SuggestedRepCom(oUser.Emp, item, oRepProducts, oRepCliComs, exs)
            End With
            retval.items.Add(item)

            If item.sku.isBundle Then
                item.bundle = Await FEB2.PurchaseOrderItem.BundleItemsFactory(exs, item, oUser.Emp, oCustomCosts, oTarifaDtos, oCliProductDtos, oRepProducts)
            End If

        Next

        Return retval
    End Function

    Shared Async Function Deliver(oOrders As List(Of DTOPurchaseOrder), oUser As DTOUser, oMgz As DTOMgz) As Task(Of DTODelivery)
        Dim exs As New List(Of Exception)
        Dim oCustomer As DTOCustomer = oOrders.First.Contact
        Dim retval = FEB2.Delivery.Factory(exs, oCustomer, oUser, oMgz)
        With retval
            .Nom = oCustomer.NomComercialOrDefault()
            .Address = FEB2.Customer.ShippingAddressOrDefault(oCustomer)
            .Tel = Await FEB2.Contact.Tel(exs, oCustomer)

            For Each oOrder As DTOPurchaseOrder In oOrders
                For Each item As DTOPurchaseOrderItem In oOrder.Items
                    Dim line As New DTODeliveryItem
                    With line
                        .Delivery = retval
                        .PurchaseOrderItem = item
                        .Qty = item.Pending
                        item.Pending -= .Qty
                        .Price = item.Price
                        .Dto = item.Dto
                    End With
                    .Items.Add(line)
                Next
            Next
        End With
        Return retval
    End Function

End Class
