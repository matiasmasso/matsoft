@ModelType DTOProductRank

<div class="Grid Categories">
    <div class="RowHdr">
        <div class="CellNum">@Model.Lang.Tradueix("Cuota", "Quota", "Share")</div>
        <div class="CellTxt">@Model.Lang.Tradueix("Producto", "Producte", "Product")</div>
    </div>

    @For Each item In Model.Items
        If Model.share(item) < 0.01 Then Exit For
        @<div class="Row SelectableRow" data-guid="@item.Product.Guid.ToString">
            <div class="CellNum">@Format(Model.share(item), "0%")</div>
            <div class="CellTxt">@DTOProductCategory.FullNom(item.Product)</div>
        </div>
    Next

</div>

