Public Class CustomerBasketController_Old
    Inherits _MatController

    Function Index() As ActionResult
        Dim oUser As DTOUser = GetSession.User
        Dim oCustomers As List(Of DTOCustomer) = BLL.BLLUser.CustomersForBasket(oUser)
        If oCustomers.Count = 0 Then
            Return MyBase.UnauthorisedView
        Else
            Return View("CustomerBasket", oCustomers)
        End If
    End Function

    Function OnCustomerSelected(basket As String) As PartialViewResult
        Dim oBasket As DTOBasket = GetModelBasket(basket)

        Dim oCustomer As DTOCustomer = BLL.BLLCustomer.Find(oBasket.customer)
        Dim retval As PartialViewResult = PartialView("OnCustomerSelected_", oCustomer)
        Return retval
    End Function

    Function ProductCategories(brand As Guid, customer As Guid) As JsonResult
        Dim oBrand As New DTOProductBrand(brand)
        Dim oCustomer As New DTOCustomer(customer)
        Dim items As List(Of DTOGuidNom) = BLL.BLLProductCategories.GuidNoms(oBrand, oCustomer, stockOnly:=False)
        Dim retval As JsonResult = Json(items, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function ProductSkus(category As Guid, customer As Guid) As JsonResult
        Dim oCategory As New DTOProductCategory(category)
        Dim oCustomer As New DTOCustomer(customer)
        Dim items As List(Of DTOGuidNom) = BLL.BLLProductSkus.GuidNoms(oCategory, oCustomer, BLLApp.Mgz, stockOnly:=False)
        Dim retval As JsonResult = Json(items, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function ProductSku(sku As Guid, customer As Guid) As JsonResult
        Dim oSku As DTOProductSku = BLL.BLLProductSku.Find(sku)
        Dim oCustomer As New DTOCustomer(customer)
        Dim iMoq As Integer = BLL.BLLProductSku.Moq(oSku)
        Dim thumbnail As String = BLL.BLLProductSku.ThumbnailUrl(oSku)
        Dim myData As Object = New With {.moq = iMoq, .thumbnail = thumbnail}

        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Function RequestToAddRow(basket As String, basketline As String) As JsonResult
        Dim oPurchaseOrder As DTOPurchaseOrder = GetPurchaseOrder(basket)
        Dim oAddedItem As DTOPurchaseOrderItem = GetPurchaseOrderItem(basketline)

        BLL.BLLCustomer.Load(oPurchaseOrder.Customer)
        Dim oCcx As DTOCustomer = BLL.BLLCustomer.CcxOrMe(oPurchaseOrder.Customer)
        oCcx.ProductDtos = BLL.BLLCliProductDtos.All(oCcx)

        BLL.BLLProductSku.Load(oAddedItem.Sku)
        oPurchaseOrder.Items.Add(oAddedItem)

        Dim oSkuWithsToAppend As List(Of DTOSkuWith) = BLL.BLLSkuWiths.Find(oAddedItem.Sku)
        If oSkuWithsToAppend.Count > 0 Then

            BLL.BLLProductSku.Load(oAddedItem.Sku)
            For Each oSkuWith As DTOSkuWith In oSkuWithsToAppend
                Dim oItemWith As New DTOPurchaseOrderItem
                With oItemWith
                    .Sku = oSkuWith.Child
                    .Qty = oAddedItem.Qty * oSkuWith.Qty
                    .Price = BLL.BLLCustomer.SkuPrice(oCcx, oSkuWith.Child)
                    .Dto = BLL.BLLPurchaseOrderItem.GetDiscount(oAddedItem.Sku, oCcx)
                End With
                oPurchaseOrder.Items.Add(oItemWith)
            Next
        End If

        BLL.BLLPurchaseOrder.SetIncentius(oPurchaseOrder)

        Dim oBasket As DTOBasket = GetRepBasket(oPurchaseOrder)

        Dim myData As Object = New With {.basket = oBasket, .errors = 0}
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function


#Region "Conversion functions"
    Function GetModelBasket(basket As String) As DTOBasket
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim retval As DTOBasket = jss.Deserialize(Of DTOBasket)(basket)
        Return retval
    End Function

    Function GetModelBasketLine(value As String) As DTOBasketLine
        Dim jss As New System.Web.Script.Serialization.JavaScriptSerializer()
        Dim retval As DTOBasketLine = jss.Deserialize(Of DTOBasketLine)(value)
        Return retval
    End Function

    Function GetPurchaseOrder(basket As String) As DTOPurchaseOrder
        Dim oBasket As DTOBasket = GetModelBasket(basket)

        Dim oCustomer As DTOCustomer = BLL.BLLCustomer.Find(oBasket.customer)
        Dim oUser As DTOUser = GetSession.User

        Dim retval As DTOPurchaseOrder = BLL.BLLPurchaseOrder.NewCustomerOrder(oCustomer, oUser, Today, DTOPurchaseOrder.Sources.representante_por_Web)
        With retval
            .Concept = oBasket.concept
            .TotJunt = oBasket.totjunt
            .Obs = oBasket.obs
            .UsrCreated = MyBase.GetSession.User

            Dim DtFchMin As Date
            If Date.TryParseExact(oBasket.fchmin, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DtFchMin) Then
                If DtFchMin > Today Then
                    .FchMin = DtFchMin
                End If
            End If

        End With

        For Each oBasketLine As DTOBasketLine In oBasket.lines
            Dim item As New DTOPurchaseOrderItem
            With item
                .Qty = oBasketLine.qty
                .Pending = .Qty
                .Sku = New DTOProductSku(oBasketLine.sku)
                .Sku.NomLlarg = oBasketLine.nom
                .Price = BLL.BLLAmt.Factory(oBasketLine.price)
                .Dto = oBasketLine.dto
            End With
            retval.Items.Add(item)
        Next
        Return retval
    End Function

    Function GetPurchaseOrderItem(basketline As String) As DTOPurchaseOrderItem
        Dim oBasketLine As DTOBasketLine = GetModelBasketLine(basketline)
        Dim retval As DTOPurchaseOrderItem = GetPurchaseOrderItem(oBasketLine)
        Return retval
    End Function

    Function GetPurchaseOrderItem(value As DTOBasketLine) As DTOPurchaseOrderItem
        Dim retval As New DTOPurchaseOrderItem
        With retval
            .Qty = value.qty
            .Pending = .Qty
            .Sku = New DTOProductSku(value.sku)
            .Sku.NomLlarg = value.nom
            .Price = BLL.BLLAmt.Factory(value.price)
            .Dto = value.dto
        End With
        Return retval
    End Function

    Function GetRepBasket(oOrder As DTOPurchaseOrder) As DTOBasket
        Dim retval As New DTOBasket
        With retval
            .customer = oOrder.Customer.Guid
            .customerUrl = BLL.BLLContact.Url(oOrder.Customer)
            .lines = New List(Of DTOBasketLine)
            For Each item As DTOPurchaseOrderItem In oOrder.Items
                .lines.Add(GetRepBasketLine(item))
            Next
        End With
        Return retval
    End Function

    Function GetRepBasketLine(oItem As DTOPurchaseOrderItem) As DTOBasketLine
        Dim retval As New DTOBasketLine
        With retval
            .sku = oItem.Sku.Guid
            .qty = oItem.Qty
            .nom = oItem.Sku.NomLlarg
            .price = oItem.Price.Eur
            .priceFormatted = oItem.Price.CurFormatted
            .dto = oItem.Dto
        End With
        Return retval
    End Function

#End Region


End Class