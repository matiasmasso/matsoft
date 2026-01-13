@ModelType DTODelivery

@Code
    Dim exs As New List(Of Exception)
    ViewBag.Title = "delivery_Confirmation_Cash"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oOrder As New DTOPurchaseOrder
    Dim oSpv As New DTOSpv
    Dim oLang As DTOLang = Model.Customer.Lang
    Dim oAmt = Model.TotalCash()
    
    'Dim oAmt As DTOAmt = FEB.Delivery.TotalSync(exs,Model)
    Dim sCash As String = DTOAmt.CurFormatted(oAmt)
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
                @Html.Raw(oLang.Tradueix("Pasamos relación de mercancía lista para su envío.", _
                                "Passem relació de mercaderia llesta per enviar.", _
                                "You'll find below the goods list ready to be shipped."))
                <br />
                @Html.Raw(oLang.Tradueix("Por favor revise los artículos, las cantidades y la dirección de entrega.", _
                                "Si us plau revisi els productes, quantitats i adreça d'entrega.", _
                                "Please check products, quantities and delivery address"))
            </p>
            <p>
                @Html.Raw(oLang.Tradueix("Quedamos a <b>la espera de recibir el pago por " & sCash & " </b> para darle salida lo antes posible.",
                                "Quedem a l’espera de <b>rebre el pagament per " & sCash & "</b> per donar-li sortida el més aviat possible.",
                                "We will <b>wait for your payment of " & sCash & "</b> to ship it at soonest:"))
            </p>
<p>
    
    @Html.Raw(oLang.Tradueix("Haga ", _
                               "Faci ", _
                               "Please "))  
      
    <a href="@FEB.Delivery.UrlTpv(Model, True)">
        @Html.Raw(oLang.Tradueix("clic aquí ", _
                               "clic aquí ", _
                               "clic here "))

    </a>&nbsp;@Html.Raw(oLang.Tradueix("para pagar mediante tarjeta de crédito.", _
                               "per pagar amb tarja de crèdit.", _
                               "for credit card payment."))

</p>
            <p>
                @Html.Raw(oLang.Tradueix("Si lo prefiere, puede pagar mediante transferencia, haciendo constar en el concepto su nombre fiscal y el número de albarán.", _
                               "Si s'ho estima més pot pagar-ho per transferència, fent constar el seu nom fiscal y el numero d’albarà.", _
                               "Alternatively you may send a bank transfer, mentioning your company name and the number of the delivery note."))
            </p>
            <p>
@Html.Raw(oLang.Tradueix("Cuenta bancaria:","Compte bancari:","Bank account:"))<br/>
@Html.Raw(FEB.Banc.BancTransfersReceptionHtml(Website.GlobalVariables.Emp))
            </p>
            <p>
                </a>&nbsp;@Html.Raw(oLang.Tradueix("Agradeceremos nos remita el justificante bancario por correo electrónico:",
                      "Agrairem ens enviï el justificant bancari per correu electronic:",
                      "Please send us the bank receipt via email:"))
            </p>
            
            <table style="font-family:Arial,helvetica,sans-serif;font-size:1em;line-height:1.2em;">
                <tr>
                    <td>email:</td>
                    <td><a href="mailto:cuentas@matiasmasso.es">cuentas@matiasmasso.es</a></td>
                </tr>
            </table>

            <p>
                @Html.Raw(oLang.Tradueix("Pasadas 48 horas sin recibir el justificante el albarán quedará anulado y la mercancía pasará a estar disponible para su venta.",
                                 "Després de 48 hores sense rebre el justificant l’albarà quedarà anul•lat i es disposarà de la mercaderia per la seva venda.",
                                 "After 48 hours if no bank receipt has been received the delivery will be cancelled and the goods will become available for sale."))
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
                                <td style="text-align:right"><a href="@FEB.Delivery.PdfUrl(Model, True)">(@oLang.Tradueix("descarga de Pdf", "descàrrega de Pdf", "Pdf download"))</a></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    @If Model.PortsCod = DTOCustomer.PortsCodes.Reculliran Then
                        @<td style="padding:5px 0 5px 10px; vertical-align:top;">@oLang.Tradueix("recogida", "recollida", "pick-up address")</td>
                        @<td style="padding:5px 0 5px 10px;">
                            @for Each line In Website.GlobalVariables.Emp.Mgz.NomAndAddressLines
                                @<br />
                                @Html.Raw(line)
                            Next
                        </td>
                    Else
                        @<td style="padding:5px 0 5px 10px; vertical-align:top;">@oLang.Tradueix("destino", "destinació", "delivery address")</td>
                        @<td style="padding:5px 0 5px 10px;">@Html.Raw(FEB.Delivery.FullNameAddress(Model))</td>
                    End If
                </tr>
                <tr>
                    <td style="padding:15px 0 0 10px;">@oLang.Tradueix("cantidad", "quantitat", "quantity")</td>
                    <td style="padding:15px 0 0 10px;">@oLang.Tradueix("producto", "producte", "product")</td>
                </tr>

                @For i As Integer = 0 To Model.Items.Count - 1
                    Dim Item As DTODeliveryItem = Model.Items(i)

                    @If Model.Cod = DTOPurchaseOrder.Codis.Reparacio Then
                        @If Not Item.Spv.Guid.Equals(oSpv.Guid) Then
                            oSpv = Item.Spv
                            @<tr>
                                 <td colspan="2" style="padding:8px 0 5px 30px;">
                                     @for each line In oSpv.Lines(oLang)
                                                 @<i>@line</i>
                                                 @<br/>
                                     Next
                                 </td>
                            </tr>
                        End If
                    Else
                        @If Not Item.PurchaseOrderItem.PurchaseOrder.Guid.Equals(oOrder.Guid) Then
                            oOrder = Item.PurchaseOrderItem.PurchaseOrder
                            @<tr>
                                <td colspan="2" style="padding:8px 0 5px 30px;">
                                    <i><a href="@FEB.PurchaseOrder.Url(oOrder, True)">@oOrder.Caption()</a></i>
                                </td>
                            </tr>
                        End If
                    End If

                    @<tr>
                        <td style="text-align:right;">@Item.Qty</td>
                        <td style="padding-left:10px;"><a href="@Item.Sku.GetUrl(oLang,, True)">@Item.Sku.RefYNomLlarg.Tradueix(oLang)</a></td>
                    </tr>
                Next
            </table>

        </td>
    </tr>

</table>
<br />


