@ModelType List(Of DTOPurchaseOrder)


<p>
    @ContextHelper.Tradueix("Gracias por tu pedido.", "Gracies per la teva comanda.", "Thanks for your order.")
</p>
<p>
    @ContextHelper.Tradueix("A continuación puedes consultar una lista de los pedidos que has registrado durante las últimas 24 horas:",
                                                   "A continuació pots consultar una llista de les comandes que has registrat les darreres 24 hores:",
                                                   "Next list displays the orders you entered on the last 24 hours:")
</p>

<div class="Grid">
    <div class="RowHdr">
        <div class="CellNum">@ContextHelper.Tradueix("Hora", "Hora", "Time")</div>
        <div class="CellNum">@ContextHelper.Tradueix("Pedido", "Comanda", "Order")</div>
        <div class="CellTxt">@ContextHelper.Tradueix("Cliente", "Client", "Customer")</div>
        <div class="CellAmt">@ContextHelper.Tradueix("Importe", "Import", "Amount")</div>
    </div>

    @For Each oOrder In Model
        @<div class="Row">
            <div class="CellNum">@Format(oOrder.UsrLog.fchCreated, "HH:mm")</div>
            <div class="CellNum">
                <a href="@oOrder.Url()" target="_blank">
                    @oOrder.num
                </a>
            </div>
            <div class="CellTxt">
                <a href="@oOrder.Url()" target="_blank">
                    @oOrder.customer.FullNom
                </a>
            </div>
            <div class="CellAmt">@DTOAmt.CurFormatted(oOrder.sumaDeImports)</div>
        </div>
    Next
</div>

<div id="RequestForNewOrder">
    <input type="button" value='@ContextHelper.Tradueix("Cursar un nuevo pedido", "Cursar un altre comanda", "Place another order")' />
</div>