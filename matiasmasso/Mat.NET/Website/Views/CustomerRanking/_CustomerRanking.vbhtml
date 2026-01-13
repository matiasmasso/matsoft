@ModelType  DTOCustomerRanking
@Code
    Dim Arrastre As Decimal
End Code


<div class="Grid">
    <div class="Row">
        <span class="CellId">
            @ContextHelper.Tradueix("Posición", "Posició", "Rank")
        </span>
        <span class="CellNom">
            @ContextHelper.Tradueix("Cliente", "Client", "Customer")
        </span>
        <span class="CellAmt HideOnNarrow">
            @ContextHelper.Tradueix("Importe", "Import", "Amount")
        </span>
        <span class="CellPct">
            @ContextHelper.Tradueix("Cuota", "Quota", "Share")
        </span>
        <span class="CellPct HideOnNarrow">
            @ContextHelper.Tradueix("Acum.", "Acum.", "Accum")
        </span>
    </div>

    <div class="Row Total">
        <span class="CellId">
            &nbsp;
        </span>
        <span class="CellNom">
            total
        </span>
        <span class="CellAmt HideOnNarrow">
            @Format(Model.Sum, "#,###.00 €")
        </span>
        <span class="CellPct">
            @FormatPercent(Model.Quota(Model.Sum), 0)
        </span>
        <span class="CellPct HideOnNarrow">
            &nbsp;
        </span>
    </div>


    @For Each item As DTOCustomerRankingItem In Model.items
        @<div class="Row" data-url="@FEB.Contact.Url(item.Customer)">
            <span class="CellId">
                @Model.Rank(item)
            </span>
            <span class="CellNom">
                @item.Customer.FullNom
            </span>
            <span class="CellAmt HideOnNarrow">
                @Format(item.Eur, "#,###.00 €")
            </span>
            <span class="CellPct">
                @FormatPercent(Model.Quota(item.Eur), 0)
            </span>
            <span class="CellPct HideOnNarrow">
                @FormatPercent(Model.Accumulated(Arrastre, item.Eur), 0)
            </span>
        </div>
    Next
</div>






