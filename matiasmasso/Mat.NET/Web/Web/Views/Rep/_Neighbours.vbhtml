@ModelType List(Of DTONeighbour)

@For Each item In Model

    @<div class="Store">
        <div class="Distance">@DTONeighbour.FormattedDistance(item.Distance)</div>
        <span class="StoreNom">@item.NomComercialOrDefault()</span><br />
        @item.Address.Text<br />
        @item.Address.Zip.Location.Nom<br />
        <a href="tel:@item.Telefon">@item.Telefon</a><br />
        <div class="Turnover">@DTOAmt.CurFormatted(item.Amt)</div>
</div>

Next