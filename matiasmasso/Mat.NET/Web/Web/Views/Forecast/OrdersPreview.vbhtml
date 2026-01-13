@ModelType List(Of DTOPurchaseOrder)
@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"
End Code

<div class="pagewrapper">
    <table class="OrdersTable">
        <tr>
            <th class="CellFch">date</th>
            <th class="CellAmtx">amount</th>
            <th class="CellVolumex">volume</th>
            <th class="CellPlaceHolder">&nbsp;</th>
        </tr>
        @For Each item As DTOPurchaseOrder In Model
            @<tr>
                 <td class="CellFch">
                     <a href="#" data-order="@item.Guid.ToString">
                         @Html.Raw(String.Format("{0:dd/MM/yy}", item.Fch))
                     </a>
                 </td>
                 <td class="CellAmtx">@Html.Raw(String.Format("{0:#,##0.00} €", DTOPurchaseOrder.Eur(item)))</td>
                 <td class="CellVolumex">@Html.Raw(String.Format("{0:0.000} m3", DTOPurchaseOrder.VolumeM3(item)))</td>
                <td class="CellPlaceHolder">&nbsp;</td>
            </tr>
            @<tr id="@item.Guid.ToString" hidden="hidden">
                 <td colspan="3" align="right">
                     <table class="OrderDetail">
                         <tr>
                             <th class="CellSku">Sku</th>
                             <th class="CellProduct">Product</th>
                             <th class="CellQty">Qty</th>
                             <th class="CellPrice">Price</th>
                             <th class="CellDto">Discount</th>
                             <th class="CellAmt">Amount</th>
                             <th class="CellM3">m3</th>
                             <th class="CellM3x">Volume</th>
                         </tr>

                         @for Each itm As DTOPurchaseOrderItem In item.Items
                            @<tr>
                                 <td class="CellSku">@itm.Sku.RefProveidor</td>
                                 <td class="CellProduct">@itm.Sku.NomProveidor</td>
                                 <td class="CellQty">@itm.Qty</td>
                                 <td class="CellPrice">@DTOAmt.CurFormatted(itm.Price)</td>
                                 <td class="CellDto">@String.Format("{0}%", itm.Dto)</td>
                                 <td class="CellAmt">@(DTOAmt.CurFormatted(DTOAmt.Import(itm.Qty, itm.Price, itm.Dto)))</td>
                                 <td class="CellM3">@String.Format("{0:0.000} m3", DTOProductSku.VolumeM3OrInherited(itm.Sku))</td>
                                 <td class="CellM3x">@String.Format("{0:0.000} m3", itm.Qty * DTOProductSku.VolumeM3OrInherited(itm.Sku))</td>
                            </tr>
                         Next
                     </table>
                 </td>
            </tr>
        Next
    </table>
</div>



@Section Styles
    <style>
        .OrdersTable {
            width:100%;
            margin:10px;
        }
        .OrderDetail {
            font-size: smaller;
        }
        .CellFch {
            text-align: center;
            width: 200px;
            padding-right: 1em;
            padding-left: 1em;
        }
        .CellAmtx {
            text-align: right;
            width: 200px;
            padding-right: 1em;
            padding-left: 1em;
            white-space: nowrap;
        }
        .CellVolumex {
            text-align: right;
            width: 200px;
            padding-right: 1em;
            padding-left: 1em;
            white-space: nowrap;
        }
        .CellPlaceHolder {
            width:100%;
        }
        .CellSku {
            text-align: left;
            width: 100px;
            padding-right: 1em;
            padding-left: 1em;
        }
        .CellProduct {
            text-align: left;
            padding-right: 1em;
            padding-left: 1em;
            white-space:nowrap;
        }
        .CellQty {
            text-align: right;
            width: 80px;
            padding-right: 1em;
            padding-left: 1em;
        }
        .CellPrice {
            text-align: right;
            width: 80px;
            padding-right: 1em;
            padding-left: 1em;
            white-space: nowrap;
        }
        .CellDto {
            text-align: right;
            width: 80px;
            padding-right: 1em;
            padding-left: 1em;
            white-space: nowrap;
        }
        .CellAmt {
            text-align: right;
            width: 80px;
            padding-right: 1em;
            padding-left: 1em;
            white-space: nowrap;
        }
        .CellM3 {
            text-align: right;
            width: 80px;
            padding-right: 1em;
            padding-left: 1em;
            white-space: nowrap;
        }
        .CellM3x {
            text-align: right;
            width: 80px;
            padding-right: 1em;
            padding-left: 1em;
            white-space: nowrap;
        }
    </style>
End Section
@Section Scripts
    <script>
        $(document).on('click', '[data-order]', function (event) {
            event.preventDefault();
            var order = $(this).data("order");
            $('#' + order + '').toggle();
        });

    </script>
End Section
