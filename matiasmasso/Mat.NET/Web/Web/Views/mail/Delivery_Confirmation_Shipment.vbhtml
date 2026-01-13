@ModelType  DTODelivery

@Code
    Dim exs As New List(Of Exception)
    ViewBag.Title = "Delivery_Confirmation_Shipment"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oOrder As New DTOPurchaseOrder
    Dim oLang As DTOLang = Model.Customer.Lang
    FEB2.Contact.Load(Model.Customer, exs)
End Code

<style>
    a {
        text-decoration:none;
    }
    a:hover {
         text-decoration:underline;
    }
</style>

<table style="font-family:Arial,helvetica,sans-serif;font-size:1em;line-height:1.2em;width:100%">
    <tr>
        <td>
            <p>
                @Html.Raw(oLang.Tradueix("Pasamos relación de mercancía lista para su envío.",
                                 "Passem relació de mercaderia llesta per enviar.",
                                 "You'll find below the goods list ready to be shipped."))
                <br />
                @Html.Raw(oLang.Tradueix("Por favor revise los artículos, las cantidades y la dirección de entrega.",
                                 "Si us plau revisi els productes, quantitats i adreça d'entrega.",
                                 "Please check products, quantities and delivery address"))
            </p>

            @If Model.Customer.ContactClass.Equals(DTOContactClass.Wellknown(DTOContactClass.Wellknowns.Farmacia)) Or Model.Customer.ContactClass.Equals(DTOContactClass.Wellknown(DTOContactClass.Wellknowns.MajoristaFarmacies)) Then
                @<p>
                    @Html.Raw(oLang.Tradueix("Esta mercancía saldrá de nuestro almacén mañana y tiene un plazo aproximado de entrega de 48 horas.",
                                                    "Aquesta mercaderia sortirà demà del nostre magatzem i té un termini d'entrega aproximat de 48 hores.",
                                                    "This delivery is scheduled for shipping tomorrow and it takes aprox. 48 hours to reach its destination."))
                     <br />
                     @Html.Raw(oLang.Tradueix("En caso de no recibirlo en el plazo indicado por favor, póngase en contacto con nosotros.",
                                             "En cas de no rebre'l en aquest termini preguem es posin en contacte amb nosaltres.",
                                             "Please contact us if you do not get it on time."))
                     <br />
                     @Html.Raw(oLang.Tradueix("Le recordamos que es imprescindible revisar y contar los bultos y notificar cualquier incidencia en el albarán de entrega.",
                                             "Recordin que és imprescindible revisar i comptar els paquets i fer constar qualsevol incidència a l'albarà d'entrega.",
                                             "Remember you must check and count the packages and write in the shipping document any delivery incidence."))
                </p>
            Else
                @<p>
                    @Html.Raw(oLang.Tradueix("Quedamos a <b>la espera de su confirmación</b> <span style='color:red'>(IMPRESCINDIBLE NOMBRE DE LA PERSONA QUE CONFIRMA)</span> para darle salida lo antes posible.",
                                           "Quedem a <b>l’espera de la seva confirmació</b> <span style='color:red'>(IMPRESCINDIBLE NOM DE LA PERSONA QUE CONFIRMA)</span> per donar-li sortida el més aviat possible.",
                                           "We will <b>wait for your authorisation</b> to ship it at soonest <span style='color:red'>(DON'T FORGET TO ESPECIFY THE NAME OF THE CONFIRMEE)</span>."))
                    <br />
                    @Html.Raw(oLang.Tradueix("De no recibir noticias en un <b>plazo máximo de 24 horas</b>, el albarán quedará anulado y la mercancía pasará a estar disponible para su venta.",
                                     "En cas que no rebem confirmació en un <b>termini màxim de 24 hores</b> l'albarà quedarà anul•lat i es disposarà de la mercaderia per la seva venda.",
                                     "If no news are received on <b>24 hours at latest</b> the delivery will be cancelled and the goods will become available for sale."))
                </p>
            End If

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
                    <td style="padding:5px 0 5px 10px; vertical-align:top;">@oLang.Tradueix("destino", "destinació", "delivery address")</td>
                    <td style="padding:5px 0 5px 10px;">@Html.Raw(FEB2.Delivery.FullNameAddress(Model))</td>
                </tr>
                <tr>
                    <td style="padding:15px 0 0 10px;">@oLang.Tradueix("cantidad", "quantitat", "quantity")</td>
                    <td style="padding:15px 0 0 10px;">@oLang.Tradueix("producto", "producte", "product")</td>
                </tr>

                @For i As Integer = 0 To Model.Items.Count - 1
                    Dim Item As  DTODeliveryItem = Model.Items(i)
                    @If Not Item.PurchaseOrderItem.PurchaseOrder.Guid.Equals(oOrder.Guid) Then
                        oOrder = Item.PurchaseOrderItem.PurchaseOrder
                        @<tr>
                            <td colspan="2" style="padding:8px 0 5px 30px;">
                                <i>
                                    <a href="@FEB2.PurchaseOrder.Url(oOrder, True)">
                                        @oOrder.Caption(oLang)
                                    </a>
                                </i>
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
<br/>



