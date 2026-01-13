@ModelType DTOPurchaseOrder
@Code
    ViewBag.Title = "Pedido de Cliente"
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    Dim DtoExists As Boolean = Model.items.SelectMany(Function(x) x.deliveries).Any(Function(y) y.dto <> 0)
    Dim total = DTOAmt.Factory(Model.items.SelectMany(Function(x) x.deliveries).Sum(Function(y) y.Import.Eur))
End Code

    <div class="pagewrapper">
        <div class="PageTitle">
            @ContextHelper.Tradueix("Historial de pedido", "Historial de comanda", "Order history")
        </div>
        <div>
            @Model.customer.FullNom
        </div>
        <div>
            @DTOPurchaseOrder.FullConcepte(Model)
        </div>
        <div class="Grid">
            <div class="RowHdr">
                <div class="CellTxt">
                    @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
                </div>
                <div class="CellNum">
                    @ContextHelper.Tradueix("Servido", "Servit", "Shipped")
                </div>
                <div class="CellNum">
                    @ContextHelper.Tradueix("Precio", "Preu", "Price")
                </div>

                @If DtoExists Then
                    @<div Class="CellNum">
                        @ContextHelper.Tradueix("Descuento", "Descompte", "Discount")
                    </div>
                End If

                <div class="CellNum">
                    @ContextHelper.Tradueix("Importe", "Import", "Amount")
                </div>
                <div class="CellFch">
                    @ContextHelper.Tradueix("Fecha", "Data", "Date")
                </div>
                <div class="CellNum">
                    @ContextHelper.Tradueix("Albaran", "Albarà", "Delivery note")
                </div>
                <div class="CellNum">
                    @ContextHelper.Tradueix("Factura", "Factura", "Invoice")
                </div>
                <div class="CellNum">
                    @ContextHelper.Tradueix("Liquidacion", "Liquidació", "Balance")
                </div>
            </div>


            @For Each item As DTOPurchaseOrderItem In Model.items
                For Each item2 As DTODeliveryItem In item.deliveries
                    @<div class="Row">
                        <div class="CellTxt">
                            @item.Sku.RefYNomLlarg.Tradueix(ContextHelper.Lang)
                        </div>
                        <div class="CellNum">
                            @item2.qty
                        </div>
                        <div class="CellNum">
                            @DTOAmt.CurFormatted(item2.price)
                        </div>

                        @If DtoExists Then
                            @<div Class="CellNum">
                                @Format(item2.dto, "0\%")
                            </div>
                        End If

                        <div class="CellNum">
                            @DTOAmt.CurFormatted(item2.Import)
                        </div>
                        <div class="CellFch">
                            @Format(item2.delivery.fch, "dd/MM/yy")
                        </div>
                        <div class="CellNum">
                            <a href="@FEB.Delivery.Url(item2.delivery)">@item2.delivery.id</a>
                        </div>
                        <div class="CellNum">
                            @If item2.delivery.invoice IsNot Nothing Then
                                @<a href="@FEB.Invoice.Url(item2.delivery.invoice)">@item2.delivery.invoice.num</a>
                            Else
                                @Html.Raw("&nbsp;")
                            End If
                        </div>
                        <div class="CellNum">
                            @If item2.repComLiquidable IsNot Nothing AndAlso item2.repComLiquidable.repLiq IsNot Nothing Then
                                @<a href="@FEB.Repliq.DownloadUrl(item2.repComLiquidable.repLiq)">@item2.repComLiquidable.repLiq.id</a>
                            Else
                                @Html.Raw("&nbsp;")
                            End If
                        </div>
                    </div>
                Next
            Next


            <div class="RowHdr">
                <div class="CellTxt">
                   Total
                </div>
                <div class="CellNum">
                    &nbsp;
                </div>
                <div class="CellNum">
                    &nbsp;
                </div>

                @If DtoExists Then
                    @<div Class="CellNum">
    &nbsp;
</div>
                End If

            <div class="CellNum">
                @DTOAmt.CurFormatted(total)
            </div>
                <div class="CellFch">
                    &nbsp;
                </div>
                <div class="CellNum">
                    &nbsp;
                </div>
                <div class="CellNum">
                    &nbsp;
                </div>
                <div class="CellNum">
                    &nbsp;
                </div>
            </div>

        </div>
    </div>

 