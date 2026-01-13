@ModelType DTO.DTOBasket
@Code
    Layout = "~/Views/Shared/_Layout_eCommerce.vbhtml"
    ViewData("eComPage") = DTO.Defaults.eComPages.Basket
End Code

<div class="pagewrapper">
    <div class="Grid">
        <div class="RowHdr">
            <div class="CellThumbnail">&nbsp;</div>
            <div class="CellTxt">Concepto</div>
            <div class="CellQty">Cantidad</div>
            <div class="CellPrice">Precio&nbsp;&nbsp;&nbsp;</div>
            <div class="CellAmt">Importe&nbsp;&nbsp;&nbsp;</div>
        </div>

        @For Each item As DTO.DTOConsumerBasketItem In Model.Items
            @<div class="Row">
                <div class="CellThumbnail">
                    <img src="@BLL.BLLProductSku.ThumbnailUrl(item.Sku)" />
                </div>
                <div class="CellMultiLineTxt">@Html.Raw(BLL.BLLProduct.FullNomMultiLineHtml(item.Sku))</div>
                <div class="CellQty">@item.Qty</div>
                <div class="CellPrice">@item.Price.CurFormatted</div>
                <div class="CellAmt">@BLL.BLLBasketItem.Amt(item).CurFormatted</div>
            </div>
        Next

        <div class="Row">
            <div class="CellThumbnail">&nbsp;</div>
            <div class="CellTxt">Total (IVA 21% incluido)</div>
            <div class="CellQty">&nbsp;</div>
            <div class="CellPrice">&nbsp;</div>
            <div class="CellTot">@Model. BLL.BLLBasket.Total(Model).curformatted()</div>
        </div>

    </div>

</div>

@Section Styles
    <style scoped>
        .pagewrapper {
            max-width:600px;
        }
    </style>
End Section>
