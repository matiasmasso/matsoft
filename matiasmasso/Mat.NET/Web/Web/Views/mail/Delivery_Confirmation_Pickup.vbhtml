@ModelType DTODelivery

@Code
    ViewBag.Title = "Delivery_Confirmation_Shipment"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oOrder As New DTOPurchaseOrder
    Dim oLang As DTOLang = Model.Customer.Lang

    Dim Horaris As String = oLang.Tradueix("de 09:00 a 17:00 horas", "de 09:00 a 17:00 hores", "from 09:00am to 05:00pm")
    Dim DtPickUpFrom As Date = FEB2.Delivery.PickUpFchFrom(Model).ToLocalTime.Date
    Dim DtPickUpDeadline As Date = DTODelivery.PickUpDeadline(DtPickUpFrom.ToLocalTime.Date)
End Code

<style>
    a {
        text-decoration: none;
    }

        a:hover {
            text-decoration: underline;
        }
</style>

<table style="font-family:Arial,helvetica,sans-serif;font-size:1em;line-height:1.2em;width:100%">
    <tr>
        <td>
            <p>
                @Html.Raw(oLang.Tradueix("Pasamos relación de mercancía lista para su recogida.",
                                 "Passem relació de mercaderia llesta per recollir.",
                                 "You'll find below the goods list ready to be picked-up."))
                <br />
                @Html.Raw(oLang.Tradueix("Por favor revise los artículos, cantidades y horarios.", _
                                "Si us plau revisi els productes, quantitats i horaris.", _
                                "Please check products, quantities and delivery time frames."))
            </p>
            <p>
                @Html.Raw(oLang.Tradueix("La mercancía se puede recoger en nuestros almacenes los siguientes dias:", _
                                         "La mercaderia es pot recollir als nostres magatzems els següents dies:", _
                               "Goods will be available for pickup on our warehouse on following dates:"))
            </p>
            <p>
                <table style="font-family:Arial,helvetica,sans-serif;font-size:1em;line-height:1.2em;font-weight:700;">
                    <tr>
                        <td>@DTOLang.WeekDay(oLang, DtPickUpFrom)</td>
                        <td>@(DtPickUpFrom.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")) & " " & Horaris)</td>
                    </tr>
                    <tr>
                        <td>@DTOLang.WeekDay(oLang, DtPickUpDeadline)</td>
                        <td>@(DtPickUpDeadline.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")) & " " & Horaris)</td>
                    </tr>
                </table>
            </p>
            <p>
                @Html.Raw(oLang.Tradueix("Pasado este plazo sin que la mercancía haya sido recogida se dispondrá de ella para su venta, haciéndose cargo de los gastos de gestión ocasionados.", _
                                "Passat aquest termini sense que s'hagi recollit la mercaderia, es disposarà d’aquesta per a la seva venda, fent-se càrrec de les despeses de gestió ocasionades.", _
                                "In case the goods are not picked up after deadline they will be back on sale and the customer will be charged for expenses."))
            </p>
            <p>
                @Html.Raw(oLang.Tradueix("Dirección de recogida:",
                                            "Adreça de recollida:",
                                               "Pick-up address:"))
                @for Each line In Mvc.GlobalVariables.Emp.Mgz.NomAndAddressLines
                @<br />
                @Html.Raw(line)
                Next
            </p>
            <p>
                @Html.Raw(oLang.Tradueix("Para la recogida es imprescindible dar el número de albarán <b>" & Model.Id & "</b> y el nombre de la tienda <b>" & Model.Nom & "</b>",
                                           "Per la recollida es imprescindible donar el número d'albarà <b>" & Model.Id & "</b> y el nom de l'establiment <b>" & Model.Nom & "</b>",
                                              "Delivery note number <b>" & Model.Id & "</b> and customer name <b>" & Model.Nom & "</b> are required to deliver the goods"))
            </p>
        </td>
    </tr>
    <tr>
        <td>
            <table style="font-family:Arial,helvetica,sans-serif;font-size:1em;line-height:1.2em;width:100%">
                <tr>
                    <td style="padding-left:10px;width:60px;">@oLang.Tradueix("albarán", "albarà", "delivery note")</td>
                    <td style="padding-left:10px;width:90%">
                        <table style="font-family:Arial,helvetica,sans-serif;font-size:1em;line-height:1.2em;width:100%">
                            <tr>
                                <td style="text-align:left;">@Model.Id</td>
                                <td style="text-align:right"><a href="@FEB2.Delivery.PdfUrl(Model, True)">(@oLang.Tradueix("descarga de Pdf", "descàrrega de Pdf", "Pdf download"))</a></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding:15px 0 0 10px;">@oLang.Tradueix("cantidad", "quantitat", "quantity")</td>
                    <td style="padding:15px 0 0 10px;">@oLang.Tradueix("producto", "producte", "product")</td>
                </tr>

                @For i As Integer = 0 To Model.Items.Count - 1
                    Dim Item As DTODeliveryItem = Model.Items(i)
                    @If Not Item.PurchaseOrderItem.PurchaseOrder.Guid.Equals(oOrder.Guid) Then
                        oOrder = Item.PurchaseOrderItem.PurchaseOrder
                        @<tr>
                            <td colspan="2" style="padding:8px 0 5px 30px;">
                                <i><a href="@FEB2.PurchaseOrder.Url(oOrder, True)">@oOrder.Caption()</a></i>
                            </td>
                        </tr>
                    End If
                    @<tr>
                        <td style="text-align:right;">@Item.Qty</td>
                        <td style="padding-left:10px;"><a href="@Item.PurchaseOrderItem.Sku.GetUrl(Mvc.ContextHelper.Lang,, True)">@Item.PurchaseOrderItem.Sku.NomLlarg.Tradueix(Mvc.ContextHelper.Lang)</a></td>
                    </tr>
                Next
            </table>

        </td>
    </tr>

</table>
<br />



