@ModelType DTODelivery
@Code
    Dim exs As New List(Of Exception)
    
    Dim oCustomer As DTOCustomer = Model.Contact
End Code

<div>
    @ContextHelper.Tradueix("Por favor revise su pedido antes de enviarlo", "Si us plau revisi la seva comanda abans de enviar-la", "Please check your order before submitting")
    <br />
    <section class="Grid">
        <div class="RowHdr">
            <div class="CellTxt">@ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
            <div class="CellNum">@ContextHelper.Tradueix("Cant", "Quant", "Quant")</div>
            <div class="CellAmt">@ContextHelper.Tradueix("Precio", "Preu", "Price", "Preço")</div>
            @If DTODelivery.DiscountExists(Model) Then
                @<div Class="CellDto">@ContextHelper.Tradueix("Dto", "Dte", "Dct")</div>
            End If
            <div Class="CellAmt">@ContextHelper.Tradueix("Importe", "Import", "Amount")</div>
        </div>

        @For Each item As DTODeliveryItem In Model.Items

            @<div Class="Row">
                <div Class="CellTxt">
                    @item.PurchaseOrderItem.Sku.RefYNomLlarg.Tradueix(ContextHelper.Lang)
                </div>
                <div Class="CellNum" hidden="hidden">@item.Qty</div>
                <div Class="CellAmt">@DTOAmt.CurFormatted(item.Price)</div>
                @If DTODelivery.DiscountExists(Model) Then
                    @<div Class="CellDto" hidden="hidden">@item.Dto</div>
                End If
                <div Class="CellAmt">@DTOAmt.CurFormatted(DTOAmt.Import(item.Qty, item.Price, item.Dto))</div>
            </div>

        Next


        @If DTODelivery.IvaTipus(Model) <> 0 Then
            @<div Class="Row">
                <div Class="CellTxt">@ContextHelper.Tradueix("Base imponible", "Base imponible", "Taxable amount")</div>
                <div Class="CellNum">&nbsp;</div>
                <div Class="CellAmt">&nbsp;</div>
                @If DTODelivery.DiscountExists(Model) Then
                    @<div Class="CellDto">&nbsp;</div>
                End If
                <div Class="CellAmt">@DTOAmt.CurFormatted(DTODelivery.SumaDeImports(Model))</div>
            </div>

            @<div Class="Row">
                <div Class="CellTxt">@String.Format("{0} {1}%", ContextHelper.Tradueix("IVA", "IVA", "VAT"), DTODelivery.IvaTipus(Model))</div>
                <div Class="CellNum">&nbsp;</div>
                <div Class="CellAmt">&nbsp;</div>
                @If DTODelivery.DiscountExists(Model) Then
                    @<div Class="CellDto">&nbsp;</div>
                End If
                <div Class="CellAmt">@DTOAmt.CurFormatted(DTODelivery.IvaAmt(Model))</div>
            </div>

            @If DTODelivery.ReqTipus(Model) <> 0 Then
                @<div Class="Row">
                    <div Class="CellTxt">@String.Format("{0} {1}%", ContextHelper.Tradueix("Recargo de equivalencia", "Recarrec d'equivalencia", "VAT add-on"), DTODelivery.ReqTipus(Model))</div>
                    <div Class="CellNum">&nbsp;</div>
                    <div Class="CellAmt">&nbsp;</div>
                    @If DTODelivery.DiscountExists(Model) Then
                        @<div Class="CellDto">&nbsp;</div>
                    End If
                    <div Class="CellAmt">@DTOAmt.CurFormatted(DTODelivery.ReqAmt(Model))</div>
                </div>
            End If

        End If

        <div Class="Row">
            <div Class="CellTxt">Total</div>
            <div Class="CellNum">&nbsp;</div>
            <div Class="CellAmt">&nbsp;</div>
            @If DTODelivery.DiscountExists(Model) Then
                @<div Class="CellDto">&nbsp;</div>
            End If
            <div Class="CellAmt">@DTOAmt.CurFormatted(FEB.Delivery.TotalSync(exs, Model))</div>
        </div>


    </section>




    <div class="Condition">
        <div class="ConditionCaption">@ContextHelper.Tradueix("Forma de envío", "Forma de enviament", "Shipment method")</div>
        @Select Case DTODelivery.GetPortsQualification(Model)
            Case DTODelivery.PortsQualification.Qualified
                @<div>
                    @ContextHelper.Tradueix("Portes pagados a la siguiente dirección:", "Ports pagats a la següent destinació:", "Paid ports to destination:")
                    <div>@Model.Nom</div>
                    <div>@Model.Address.Text</div>
                    <div>@DTOZip.FullNom(Model.Address.Zip)</div>
                    <div>
                        @ContextHelper.Tradueix("Teléfono de contacto:", "Telefon de contacte:", "Contact phone:")
                        @Model.Tel
                    </div>
                </div>

            Case DTODelivery.PortsQualification.LowVolume
                @<div>
                    <div>
                        @ContextHelper.Tradueix("El importe del pedido no llega para portes.", "L'import de la comanda no arriba a ports.", "Order volume does not qualify for paid ports.")
                    </div>
                    <div>
                        @ContextHelper.Tradueix("Por favor seleccione una de las siguientes opciones", "Si us plau seleccioni una de les següents opcions:", "Please select your following choice:")
                    </div>
                    <div>
                        <input type="radio" name="ports" value="1" />
                        @ContextHelper.Tradueix("Seguir comprando", "Continuar comprant", "Keep purchasing")
                    </div>
                    <div>
                        <input type="radio" name="ports" value="2" />
                        @ContextHelper.Tradueix("Cargar 12,00€ de gastos de envío", "Carregar 12,00€ de despeses d'enviament", "Add 12,00€ shipment fee")
                    </div>
                     <div>
                         <input type="radio" name="ports" value="3" />
                         @ContextHelper.Tradueix("Dejar el pedido pendiente hasta el próximo envío", "Deixar la comanda pendent fins el proper enviament", "Keep order until next shipment")
                     </div>
                     <div>
                         <input type="radio" name="ports" value="3" />
                         @ContextHelper.Tradueix("Mi transporte lo recogerá por el almacén", "El meu transport el passará a recullir", "I'll send a courier to pick it up")
                         <a href="#">
                            @ContextHelper.Tradueix("(ver condiciones)", "(veure condicions)", "(see conditions)")
                         </a>
                     </div>

                </div>

            Case Else

        End Select

    </div>



    <div Class="Condition">
        <div Class="ConditionCaption">@ContextHelper.Tradueix("Forma de pago", "Forma de pagament", "Payment method")</div>
        @For Each line As String In FEB.Customer.PaymentTermsText(oCustomer, ContextHelper.lang())
            @<div>@line</div>
        Next
    </div>

    <div Class="Condition">
        <input type = "checkbox" />
        @ContextHelper.Tradueix("He leido y acepto las condiciones de venta disponibles en", "He legit i accepto les condicions de venda disponibles a", "I've read and agree sales conditions available at")
        <a href = "https://www.matiasmasso.es/condiciones" target="_blank">www.matiasmasso.es</a>
    </div>

    <div Class="Condition">
        <section Class="SubmitButtons">
            <input type = "button" id="ButtonKeepPurchasing" value='@ContextHelper.Tradueix("Seguir comprando", "Seguir comprant", "Keep purchasing")' />
            <input type = "button" id="ButtonSubmit" value='@ContextHelper.Tradueix("Enviar", "Enviar", "Submit")' />
        </section>
    </div>

</div>
