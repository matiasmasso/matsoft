Public Class PurchaseOrderItem

    Shared Function Factory(basketline As String) As DTOPurchaseOrderItem
        Dim exs As New List(Of Exception)
        Dim oBasketLine As DTOBasketLine = FEB2.Basket.GetModelBasketLine(exs, basketline)
        Dim retval = DTOPurchaseOrderItem.Factory(oBasketLine)
        Return retval
    End Function

    Shared Async Function DeliveryItems(exs As List(Of Exception), value As DTOPurchaseOrderItem) As Task(Of List(Of DTODeliveryItem))
        Return Await Api.Fetch(Of List(Of DTODeliveryItem))(exs, "PurchaseOrderItem/DeliveryItems", value.Guid.ToString())
    End Function

    Shared Async Function UpdateRepCom(exs As List(Of Exception), value As DTOPurchaseOrderItem) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOPurchaseOrderItem, Boolean)(value, exs, "PurchaseOrderItem/UpdateRepCom")
    End Function

    Shared Async Function ResetPendingQty(exs As List(Of Exception), value As DTOPurchaseOrderItem) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "PurchaseOrderItem/ResetPendingQty", value.Guid.ToString())
    End Function

    Shared Async Function UnitatsSortides(exs As List(Of Exception), value As DTOPurchaseOrderItem) As Task(Of Integer)
        Return Await Api.Fetch(Of Integer)(exs, "PurchaseOrderItem/UnitatsSortides", value.Guid.ToString())
    End Function

    Shared Function Url(value As DTOPurchaseOrderItem, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If value IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "LineaDePedido", value.PurchaseOrder.Guid.ToString, value.Lin)
        End If
        Return retval
    End Function

    Shared Async Function SuggestedRepCom(oEmp As DTOEmp, value As DTOPurchaseOrderItem, oRepProducts As List(Of DTORepProduct), oRepCliComs As List(Of DTORepCliCom), exs As List(Of Exception)) As Task(Of DTORepCom)
        Dim oCustomer As DTOCustomer = value.PurchaseOrder.Contact
        Dim oProduct As DTOProduct = value.Sku
        Dim DtFch As Date = value.PurchaseOrder.Fch
        Dim retval As DTORepCom = Await FEB2.RepCom.GetRepCom(oEmp, oCustomer, oProduct, DtFch, oRepProducts, oRepCliComs, exs)
        Return retval
    End Function

    Shared Async Function SuggestedRepCom(exs As List(Of Exception), oEmp As DTOEmp, value As DTOPurchaseOrderItem, oRepProducts As List(Of DTORepProduct), oRepCliComs As List(Of DTORepCliCom)) As Task(Of DTORepCom)
        Dim retval As DTORepCom = Nothing
        If value.RepCom IsNot Nothing AndAlso value.RepCom.RepCustom Then
            'representant o comisió assignats manualment; saltar-se validació
        Else
            Dim oOriginalRepCom As DTORepCom = value.RepCom
            Dim oSuggestedRepCom = Await FEB2.PurchaseOrderItem.SuggestedRepCom(oEmp, value, oRepProducts, oRepCliComs, exs)
            If Not DTORepCom.Equals(oOriginalRepCom, oSuggestedRepCom) Then
                retval = oSuggestedRepCom
            End If
        End If
        Return retval
    End Function

    Shared Function GetDiscount(oSku As DTOProductSku, oCustomer As DTOCustomer) As Decimal
        Dim exs As New List(Of Exception)
        Dim oCliProductDtos = FEB2.CliProductDtos.AllSync(oCustomer, exs)
        Dim retval As Decimal = GetDiscount(oSku, oCustomer, oCliProductDtos)
        Return retval
    End Function

    Shared Function GetDiscount(oSku As DTOProductSku, oCustomer As DTOCustomer, oDtos As List(Of DTOCliProductDto)) As Decimal
        Dim exs As New List(Of Exception)
        Dim retval As Decimal

        Dim oDto As DTOCliProductDto = oSku.CliProductDto(oDtos)
        If oDto IsNot Nothing Then
            retval = oDto.Dto
        End If

        Return retval
    End Function

    Shared Async Function BundleItems(exs As List(Of Exception), item As DTOPurchaseOrderItem) As Task(Of List(Of DTOPurchaseOrderItem))
        Dim retval = Await ApiClient.Execute(Of DTOPurchaseOrderItem, List(Of DTOPurchaseOrderItem))(item, exs, "purchaseOrderItem/bundleItems", item.purchaseOrder.customer.Guid.ToString())
        Return retval
    End Function

    Shared Async Function BundleItemsFactory(exs As List(Of Exception), item As DTOPurchaseOrderItem, oEmp As DTOEmp, oCustomCosts As List(Of DTOPricelistItemCustomer), oTarifaDtos As List(Of DTOCustomerTarifaDto), oCliProductDtos As List(Of DTOCliProductDto), Optional oRepProducts As List(Of DTORepProduct) = Nothing) As Task(Of List(Of DTOPurchaseOrderItem))
        Dim retval As New List(Of DTOPurchaseOrderItem)
        item.sku.bundleSkus = Await FEB2.ProductSku.Bundles(exs, item.sku)
        If exs.Count = 0 Then
            Dim oOrder = item.purchaseOrder
            Dim oSku = item.sku
            For Each oSkuBundle As DTOSkuBundle In oSku.bundleSkus
                Dim oRepCom As DTORepCom = Nothing
                If oOrder.cod = DTOPurchaseOrder.Codis.client Then
                    oRepCom = Await FEB2.RepCom.GetRepCom(oEmp, oOrder.customer, oSkuBundle.Sku, oOrder.fch, oRepProducts, exs:=exs)
                End If



                'Dim DcDto As Decimal = 0
                'Dim oPrice As DTOAmt = Nothing
                'If oOrder.cod <> DTOPurchaseOrder.Codis.proveidor Then
                ' oPrice = DTOProductSku.GetCustomerCost(oSkuBundle.Sku, oCustomCosts, oTarifaDtos)
                ' End If
                'If oPrice Is Nothing Then
                'oPrice = DTOAmt.factory
                'Else
                'If oOrder.cod = DTOPurchaseOrder.Codis.client Then

                'Dim oDto As DTOCliProductDto = oSku.CliProductDto(oCliProductDtos)
                'If oDto IsNot Nothing Then DcDto = oDto.Dto
                'End If

                'Dim oBundleChild = DTOPurchaseOrderItem.Factory(oOrder, oSkuBundle.Sku, item.qty, oPrice, DcDto)

                Dim oBundleChild = DTOPurchaseOrderItem.bundleItemFactory(oSkuBundle, item, oEmp, oCustomCosts, oTarifaDtos, oCliProductDtos, oRepCom)
                retval.Add(oBundleChild)
            Next
        End If
        Return retval
    End Function


End Class

Public Class PurchaseOrderItems
    Inherits _FeblBase

    Shared Async Function Kpis(exs As List(Of Exception), oEmp As DTOEmp, yearfrom As Integer) As Task(Of List(Of DTOKpi))
        Return Await Api.Fetch(Of List(Of DTOKpi))(exs, "PurchaseOrderItems/kpis", oEmp.Id, yearfrom)
    End Function

    Shared Async Function All(exs As List(Of Exception), oContact As DTOContact) As Task(Of List(Of DTOPurchaseOrderItem))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrderItem))(exs, "PurchaseOrderItems/FromContact", oContact.Guid.ToString())
    End Function


    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct) As Task(Of List(Of DTOPurchaseOrderItem))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrderItem))(exs, "PurchaseOrderItems/FromProduct", oProduct.Guid.ToString())
    End Function

    Shared Async Function Pending(exs As List(Of Exception), oEmp As DTOEmp, oSku As DTOProductSku, oCod As DTOPurchaseOrder.Codis, Optional oMgz As DTOMgz = Nothing) As Task(Of List(Of DTOPurchaseOrderItem))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrderItem))(exs, "purchaseOrderItems/Pending/FromSku", oEmp.Id, oSku.Guid.ToString, oCod, OpcionalGuid(oMgz))
    End Function

    Shared Async Function Pending(exs As List(Of Exception), oContact As DTOBaseGuid, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz, Optional oCustomerGroupLevel As DTOCustomer.GroupLevels = DTOCustomer.GroupLevels.Single) As Task(Of List(Of DTOPurchaseOrderItem))
        Dim retval As List(Of DTOPurchaseOrderItem) = Await Api.Fetch(Of List(Of DTOPurchaseOrderItem))(exs, "purchaseOrderItems/Pending/FromContact", oContact.Guid.ToString, oCod, oMgz.Guid.ToString, oCustomerGroupLevel)
        Return retval
    End Function

    Shared Async Function Pending(exs As List(Of Exception), oUser As DTOUser, oCod As DTOPurchaseOrder.Codis, oMgz As DTOMgz) As Task(Of List(Of DTOPurchaseOrderItem))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrderItem))(exs, "purchaseOrderItems/Pending/FromUser", oUser.Guid.ToString, oCod, OpcionalGuid(oMgz))
    End Function

    Shared Async Function Descuadres(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOPurchaseOrderItem))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrderItem))(exs, "purchaseOrderItems/Descuadres", oUser.Guid.ToString())
    End Function

    Shared Async Function PendentsDeLiquidacioRep(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOPurchaseOrderItem))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrderItem))(exs, "purchaseOrderItems/PendentsDeLiquidacioRep", oEmp.Id)
    End Function

    Shared Async Function RecuperaLiniesDeSortides(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Task(Of Integer)
        Return Await Api.Fetch(Of Integer)(exs, "purchaseOrderItems/RecuperaLiniesDeSortides", oPurchaseOrder.Guid.ToString())
    End Function

    Shared Async Function UpdateEtd(exs As List(Of Exception), items As List(Of DTOPurchaseOrderItem)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOPurchaseOrderItem), Boolean)(items, exs, "purchaseOrderItems/UpdateEtd")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), items As List(Of DTOPurchaseOrderItem)) As Task(Of Boolean)
        Return Await Api.Delete(items, exs, "purchaseOrderItems")
    End Function

    Shared Async Function ReasignaComisions(oEmp As DTOEmp, items As List(Of DTOPurchaseOrderItem)) As Threading.Tasks.Task
        For Each item As DTOPurchaseOrderItem In items
            If item.PurchaseOrder Is Nothing Then
                item.RepCom = Nothing
            Else
                item.RepCom = Await FEB2.RepCom.GetRepCom(oEmp, item.PurchaseOrder.Contact, item.Sku, item.PurchaseOrder.Fch)
            End If
        Next
    End Function

    Shared Function Excel(items As List(Of DTOPurchaseOrderItem), Optional sCaption As String = "M+O Pendiente de entrega", Optional sFilename As String = "") As ExcelHelper.Sheet
        Dim exs As New List(Of Exception)
        Dim oFirstOrder As DTOPurchaseOrder = items.First.PurchaseOrder
        Dim oContact As DTOContact = oFirstOrder.Contact
        FEB2.Contact.Load(oContact, exs)
        Dim oLang As DTOLang = oContact.lang
        Dim oDomain = DTOWebDomain.Factory(oLang, True)

        Dim retval As New ExcelHelper.Sheet(sCaption, sFilename)

        With retval
            .DisplayTotals = True
            .AddColumn(oLang.Tradueix("Pedido", "Comanda", "Order"), ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("Fecha", "Data", "Date"), ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("Concepto", "Concepte", "Subject"), ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Sku", ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("Producto", "Producte", "Product"), ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn(oLang.Tradueix("Cantidad", "Quantitat", "Quantity"), ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn(oLang.Tradueix("Precio", "Preu", "Price"), ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("Descuento", "Descompte", "Discount"), ExcelHelper.Sheet.NumberFormats.Percent)
            .AddColumn(oLang.Tradueix("Importe", "Import", "Amount"), ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("ETD", ExcelHelper.Sheet.NumberFormats.DDMMYY)
        End With


        Dim oRow As ExcelHelper.Row = Nothing
        If items.Count > 0 Then

            Dim oOrder As New DTOPurchaseOrder
            Dim oCell As ExcelHelper.Cell
            For Each item As DTOPurchaseOrderItem In items
                oRow = retval.AddRow()

                If item.PurchaseOrder Is Nothing Then
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                Else
                    oOrder = item.PurchaseOrder
                    Dim sUrlOrder As String = FEB2.PurchaseOrder.Url(oOrder, True)
                    oCell = oRow.addCell(item.PurchaseOrder.Num, sUrlOrder)
                    oCell = oRow.AddCell(oOrder.Fch.ToShortDateString)
                    'oCell.CellStyle = ExcelHelper.Cell.CellStyles.Italic
                    oCell = oRow.AddCell(oOrder.Caption(oLang))
                    'oCell.CellStyle = ExcelHelper.Cell.CellStyles.Italic
                End If

                Dim sUrlSku As String = item.Sku.GetUrl(oLang, , True)
                oRow.addCell(item.Sku.Id) ', sUrlSku)
                If oOrder.Cod = DTOPurchaseOrder.Codis.Proveidor Then
                    oRow.AddCell(DTOProductSku.RefYNomPrv(item.Sku))
                Else
                    oCell = oRow.addCell(item.sku.nomLlarg.Esp)
                    oCell.Indent = 1
                End If
                oRow.AddCell(item.Pending)

                If item.Price Is Nothing Then
                    oRow.AddCell()
                    oRow.AddCell()
                    oRow.AddCell()
                Else
                    oRow.AddCell(item.Price.Eur)
                    oRow.AddCell(item.Dto)
                    oRow.AddFormula("RC[-3]*RC[-2]*(100-RC[-1])/100")
                End If
                If item.ETD = Nothing Then
                    oRow.AddCell()
                Else
                    oRow.AddCell(item.ETD.ToShortDateString)
                End If
            Next
        End If

        Return retval
    End Function

End Class
