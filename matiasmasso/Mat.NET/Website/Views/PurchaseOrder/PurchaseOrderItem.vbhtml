@ModelType DTOPurchaseOrderItem
@Code
    ViewBag.Title = "Pedido de Cliente"
    Layout = "~/Views/shared/_Layout.vbhtml"
    
    Dim iPending As Integer = Model.Qty
End Code

<div class="pagewrapper">
    <div class="PageTitle">
        @ContextHelper.Tradueix("Historial de linea de pedido","Historial de linia de comanda","Order line history")
    </div>
    <div>
        @DTOProductSku.FullNom(Model.Sku)
    </div>
   <div class="Grid">
        <div class="RowHdr">
            <div class="CellFch">
                @ContextHelper.Tradueix("Fecha", "Data", "Date")
            </div>
            <div class="CellTxt">
                @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
            </div>
            <div class="CellNum">
                @ContextHelper.Tradueix("Servido", "Servit", "Shipped")
            </div>
            <div class="CellNum">
                @ContextHelper.Tradueix("Pendiente", "Pendent", "Pending")
            </div>
        </div>


        <div class="Row">
            <div class="CellFch">@Format(Model.PurchaseOrder.Fch, "dd/MM/yy")</div>
            <div class="CellTxt">
                <a href="@FEB.PurchaseOrder.Url(Model.PurchaseOrder)" target="_blank">
                    @Model.PurchaseOrder.Caption())
                </a>
            </div>
            <div class="CellNum"></div>
            <div class="CellNum">@Format(iPending, "#,###")</div>
        </div>

        @For Each item As DTODeliveryItem In Model.Deliveries
            iPending -= item.Qty
            @<div class="Row">
                <div class="CellFch">@format(item.Delivery.Fch,"dd/MM/yy")</div>
                <div class="CellTxt">
                    <a href="@FEB.Delivery.Url(item.Delivery)">
                        @String.Format("{0} {1}", ContextHelper.Tradueix("albarán ", "albarà ", "delivery note "), item.Delivery.Id)
                    </a>
                    @If item.Delivery.Invoice IsNot Nothing Then
                    @<a href="@FEB.Invoice.Url(item.Delivery.Invoice)">
                        @String.Format("{0} {1}", ContextHelper.Tradueix("factura ", "factura ", "invoice "), item.Delivery.Invoice.Num)
                    </a>
                    End If
                </div>
                <div class="CellNum">@Format(item.Qty, "#,###")</div>
                <div class="CellNum">@Format(iPending, "#,###")</div>
            </div>
        Next
            
    </div>
</div>

@Section Styles
    <!--
    <style>
        .pagewrapper {
            max-width: 550px;
            margin: auto;
        }

        .Grid {
            display: table;
            table-layout: auto;
            width: 100%;
            margin: 20px auto;
        }

            .Grid a:hover {
                color: red;
            }

        .Row {
            display: table-row;
        }

            .Row:nth-child(odd) {
                background: #dddddd;
            }

            .Row:nth-child(even) {
                background: #eeeeee;
            }

        .RowHdr {
            display: table-row;
            color: gray;
        }

        .CellFch {
            display: table-cell;
            padding: 4px 7px 2px 4px;
            width: 100px;
            overflow: hidden;
            vertical-align: top;
            text-align: center;
            border: 1px solid #bbbbbb;
        }

        .CellTxt {
            display: table-cell;
            padding: 4px 7px 2px 4px;
            width: 100%;
            max-width: 0;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            vertical-align: top;
            text-align: left;
            border: 1px solid #bbbbbb;
        }

        .CellNum {
            display: table-cell;
            text-align: right;
            padding: 4px 7px 2px 4px;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
            min-width: 70px;
            max-width: 100px;
            vertical-align: top;
            border: 1px solid #bbbbbb;
        }
    </style>
        -->
End Section
