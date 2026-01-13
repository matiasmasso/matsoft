@ModelType DTOPurchaseOrder

@Code
    ViewBag.Title = "Venciment"
    Layout = "~/Views/mail/_Layout.vbhtml"
    Dim oLang As DTOLang = Model.Customer.Lang
End Code

<table border="0" style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; line-height: 1.3em; padding: 0 10px 0 10px;">
    <tr>
        <td>
            <p>
                @oLang.Tradueix(
                  "Confirmamos el siguiente pedido registrado online:",
                  "Confirmem la següent comanda que ha estat registrada online:",
                  "We are glad to confirm next order which has been logged online:")
            </p>

            <table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; line-height: 1.3em; width:100%; border-style:none;">
                <tr>
                    <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">
                        @oLang.Tradueix("Pedido", "Comanda", "Order #")
                    </td>
                    <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">@Model.Num</td>
                </tr>

                <tr>
                    <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">
                        @oLang.Tradueix("Cliente", "Client", "Customer")
                    </td>
                    <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">@Model.Customer.FullNom</td>
                </tr>

                <tr>
                    <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">
                        @oLang.Tradueix("Concepto", "Concepte", "Concept")
                    </td>
                    <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">@Model.Concept</td>
                </tr>

                @If Model.fchDeliveryMin <> Nothing Then
                    @<tr>
                        <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">
                            @oLang.Tradueix("Fecha servicio", "Data servei", "Shipping date")
                        </td>
                        <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">@Model.concept</td>
                    </tr>
                End If

                @If Model.TotJunt Then
                    @<tr>
                        <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">
                            @oLang.Tradueix("Servir todo junto", "Servir tot junt", "Ship all together")
                        </td>
                        <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">@oLang.Tradueix("Si", "Si", "Yes")</td>
                    </tr>
                End If

                @If Model.Obs > "" Then
                    @<tr>
                        <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none; vertical-align:top;">
                            @oLang.Tradueix("Observaciones", "Observacions", "Comments")
                        </td>
                        <td style="padding: 0 10px 0 10px; border:1px solid gray;border-style:none;">@Model.Obs</td>
                    </tr>
                End If

                <tr><td colspan="2" style="padding:   0 10px 0 10px; border-style:none;">&nbsp;</td></tr>

                <tr>
                    <td colspan="2" style="padding:0 10px 0 10px; border:1px solid gray;border-style:none;">


                        <table style="font-family: Helvetica Neue, Helvetica, Arial, sans-serif; font-size: 16px; line-height: 1.3em; width:100%">
                            <tr>
                                <td style="padding:0 10px 0 10px; border:1px solid gray;">
                                    @oLang.Tradueix("Producto", "Producte", "Product")
                                </td>
                                <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">
                                    @oLang.Tradueix("Cantidad", "Quantitat", "Quantity")
                                </td>
                                <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">
                                    @oLang.Tradueix("Precio", "Preu", "Price", "Preço")
                                </td>

                                @If DTOPurchaseOrder.DiscountsExist(Model) Then
                                    @<td align="right" style="padding:0 10px 0 10px; border:1px solid gray; ">
                                        @oLang.Tradueix("Dto", "Dte", "Dct")
                                    </td>
                                End If

                                <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">
                                    @oLang.Tradueix("Importe", "Import", "Amount")
                                </td>
                            </tr>

                            @for Each item As DTOPurchaseOrderItem In Model.Items

                                @<tr>
                                    <td style="padding:0 10px 0 10px; border:1px solid gray;">
                                        <a href="@item.Sku.GetUrl(Mvc.ContextHelper.Lang,, True)">
                                            @item.sku.NomLlarg.Tradueix(Mvc.ContextHelper.Lang)
                                        </a>
                                    </td>
                                    <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">
                                        @item.Qty
                                    </td>
                                    <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;text-wrap:none;">
                                        @item.Price.Formatted
                                    </td>

                                    @If DTOPurchaseOrder.DiscountsExist(Model) Then
                                        @<td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">
                                            @if item.Dto = 0 Then
                                                @<span>&nbsp;</span>
                                            Else
                                                @<span>@Format(item.Dto, "0\%")</span>
                                            End If
                                        </td>
                                    End If


                                    <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;text-wrap:none;">
                                        @item.Amount.Formatted
                                    </td>
                                </tr>

                            Next


                            @If DTOPurchaseOrder.DevengaIva(Model) Then

                                @<tr>
                                    <td style="padding: 0 10px 0 10px; border:1px solid gray;">
                                        @oLang.Tradueix("Base imponible", "Base imponible", "Taxable amount")
                                    </td>
                                    <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                    <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                    @If DTOPurchaseOrder.DiscountsExist(Model) Then
                                        @<td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                    End If
                                    <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;text-wrap:none;">
                                        @Model.SumaDeImportes().Formatted
                                    </td>
                                </tr>

                                @<tr>
                                    <td style="padding: 0 10px 0 10px; border:1px solid gray;">
                                        @oLang.Tradueix("IVA", "IVA", "VAT")
                                        @Format(DTOPurchaseOrder.IvaPct(Model), "0.#\%")
                                    </td>
                                    <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                    <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                    @If DTOPurchaseOrder.DiscountsExist(Model) Then
                                        @<td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">
                                            &nbsp;
                                        </td>
                                    End If
                                    <td align="right" style="padding:0 10px 0 10px; border:1px solid gray;">
                                        @DTOPurchaseOrder.IvaAmt(Model).Formatted
                                    </td>
                                </tr>

                                @If DTOPurchaseOrder.DevengaReq(Model) Then

                                    @<tr>
                                        <td style="padding: 0 10px 0 10px; border:1px solid gray;">
                                            @oLang.Tradueix("Recargo de equivalencia", "Recarrec d'equivalencia", "VAT")
                                            @Format(DTOPurchaseOrder.ReqPct(Model), "0.#\%")
                                        </td>
                                        <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                        <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                        @If DTOPurchaseOrder.DiscountsExist(Model) Then
                                            @<td style="padding: 0 10px 0 10px; border:1px solid gray;">
                                                &nbsp;
                                            </td>
                                        End If
                                        <td align="right" style="padding: 0 10px 0 10px; border:1px solid gray;">
                                            @DTOPurchaseOrder.ReqAmt(Model).Formatted
                                        </td>
                                    </tr>

                                End If

                            End If

                            <tr>
                                <td style="padding: 0 10px 0 10px; border:1px solid gray;">
                                    @oLang.Tradueix("Total Eur", "Total Eur", "Rotal Eur")
                                </td>
                                <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                <td style="padding: 0 10px 0 10px; border:1px solid gray;">&nbsp;</td>
                                @If DTOPurchaseOrder.DiscountsExist(Model) Then
                                    @<td style="padding: 0 10px 0 10px; border:1px solid gray;">
                                        &nbsp;
                                    </td>
                                End If
                                <td align="right" style="padding: 0 10px 0 10px; border:1px solid gray;text-wrap:none;">
                                    @DTOPurchaseOrder.totalIvaInclos(Model).formatted
                                </td>
                            </tr>

                        </table>

                    </td>
                </tr>
            </table>

            <p>
                @oLang.Tradueix("En breve nos pondremos en contacto para concertar el envío.", "En breu contactarem per concertar l'enviament.", "You'll be contacted for shipment details.")
            </p>
            <p>
                @oLang.Tradueix("Puede consultar sus pedidos en cualquier momento en", "Pot consultar les seves comandes en qualsevol moment en", "Browse your orders at any time on")
                <a href="@FEB2.PurchaseOrders.Url(Model.Customer, True)">www.matiasmasso.es/pedidos</a>
            </p>


            <p>
                @oLang.Tradueix("Para cualquier aclaración puede dirigirse a:", "Per qualsevol aclaració pot adreçar-se a:", "On any doubt please contact:")
            </p>

            <p>
                <a href="mailto:info@matiasmasso.es"> info@matiasmasso.es</a><br />
                tel.:   932541522
            </p>


            <p>
                @oLang.Tradueix(
                  "Reciba un cordial saludo,",
                  "Cordialment,",
                  "Regards,")

            </p>

            <p>
                MATIAS MASSO, S.A.<br />
                @oLang.Tradueix(
                  "Depto.Comercial",
                  "Dept.Comercial,",
                  "Commercial dept.,")
            </p>
        </td>
    </tr>
</table>