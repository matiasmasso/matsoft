@ModelType DTOPurchaseOrder
@Code
    ViewBag.Title = "RepPurchaseOrder"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oLang As DTOLang = Model.Customer.Lang
End Code

<table width="550" style="font-family:Arial;font-size:10pt;">
    <tr>
        <td>@oLang.Tradueix("Pedido", "Comanda", "Order")</td>
        <td>
            <a href="@FEB2.PurchaseOrder.Url(Model)">
                @Model.Num
            </a>
        </td>
    </tr>
    <tr>
        <td>@oLang.Tradueix("Cliente", "Client", "Customer")</td>
        <td>
            <a href="@FEB2.Contact.Url(Model.Customer)">
                @Model.Customer.FullNom
            </a>
        </td>
    </tr>
    <tr>
        <td>@oLang.Tradueix("Descripción", "Descripció", "Description")</td>
        <td>@Model.Concept</td>
    </tr>

    @If Model.fchDeliveryMin > Today Then
        @<tr>
            <td>@oLang.Tradueix("Fecha Servicio", "Data Servei", "Shipping date")</td>
            <td>
                <span style="color:red;">@String.Format(oLang.Tradueix("A partir del {0}", "A partir del {0}", "From {0} on"), Format(Model.fchDeliveryMin, "dd/MM/yy"))</span>
            </td>
        </tr>
    End If

    @If Model.TotJunt Then
        @<tr>
            <td>@oLang.Tradueix("Fraccionamiento", "Fraccionament", "Fractioned shippings")</td>
            <td>
                <span style="color:red;">@oLang.Tradueix("NO permitido", "NO permés", "NOT allowed")</span>
            </td>
        </tr>
    End If

    @If Model.Obs > "" Then
        @<tr>
            <td>@oLang.Tradueix("Observaciones", "Observacions", "Comments")</td>
            <td><span style="color:red;">@Model.Obs</span></td>
        </tr>
    End If
</table>

<br />

<table width="550" style="font-family:Arial;font-size:10pt;">
    <tr>
        <td>@oLang.Tradueix("Concepto", "Concepte", "Concept")</td>
        <td align="right">@oLang.Tradueix("Cantidad", "Quantitat", "Quantity")</td>
        <td align="right">@oLang.Tradueix("Precio", "Preu", "Price", "Preço")</td>
        <td align="right">
            @oLang.Tradueix("Dto", "Dte", "Dct")
        </td>
        <td align="right">@oLang.Tradueix("Importe", "Import", "Amount")</td>
    </tr>

    @For Each item As DTOPurchaseOrderItem In Model.Items
        @<tr>
            <td style="white-space: nowrap;overflow: hidden;text-overflow: ellipsis;">
                <a href="@item.Sku.GetUrl(Mvc.ContextHelper.Lang,, True)">
                    @item.Sku.NomLlarg.Tradueix(Mvc.ContextHelper.Lang)
                </a>
            </td>
            <td align="right">@item.Qty</td>
            <td align="right" nowrap="nowrap">@DTOAmt.CurFormatted(item.Price)</td>
            <td align="right">
                @If item.Dto <> 0 Then
                    Html.Raw(item.Dto)
                End If
            </td>
            <td align="right" nowrap="nowrap">@DTOAmt.CurFormatted(item.Amount)</td>
        </tr>
    Next

    <tr>
        <td>@oLang.Tradueix("Total", "Total", "Total")</td>
        <td align="right"></td>
        <td align="right"></td>
        <td align="right"></td>
        <td align="right" nowrap="nowrap">@DTOAmt.CurFormatted(Model.SumaDeImportes())</td>
    </tr>
</table>
<br />

