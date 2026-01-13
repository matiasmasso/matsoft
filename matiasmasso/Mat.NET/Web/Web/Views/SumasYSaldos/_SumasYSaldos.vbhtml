@Modeltype List(Of DTOPgcSaldo)
@Code
    
End Code


<section class="Grid">

    <div class="RowHdr">
        <div class="CellTxt">@Mvc.ContextHelper.Tradueix("Cuenta","Compte","Account")</div>
        <div class="CellAmt">@Mvc.ContextHelper.Tradueix("Saldo", "Saldo", "Balance")</div>
    </div>

    @For Each item As DTOPgcSaldo In Model
        @<div class="Row SelectableRow" data-url="@FEB2.PgcExtracte.Url(item)">
            <div class="CellTxt">
                    @DTOPgcCta.FullNom(item.Epg, Mvc.ContextHelper.lang())
            </div>
            <div class="CellAmt">
                @DTOAmt.CurFormatted(DTOPgcSaldo.Saldo(item))
            </div>
        </div>
    Next
        
</section>


<script src="~/Media/js/Tables.js"></script>
