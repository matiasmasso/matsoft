@Modeltype List(Of DTOBalanceSaldo)
@Code
    
End Code


<section class="Grid">

    <div class="RowHdr">
        <div class="CellTxt">@Mvc.ContextHelper.Tradueix("Cuenta", "Compte", "Account")</div>
        <div class="CellAmt CellDesktop">@Mvc.ContextHelper.Tradueix("Deudor", "Deutor", "Debitor")</div>
        <div class="CellAmt CellDesktop">@Mvc.ContextHelper.Tradueix("Acreedor", "Creditor", "Creditor")</div>
        <div class="CellAmt CellMobile">@Mvc.ContextHelper.Tradueix("Saldo", "Saldo", "Balance")</div>
    </div>

    @For Each item As DTOBalanceSaldo In Model
        @<div class="Row" data-url="@FEB2.Cce.Url(item, ViewBag.fch)">
            <div class="CellTxt">
                    @DTOPgcCta.FullNom(item, Mvc.ContextHelper.lang())
            </div>
             <div class="CellAmt CellDesktop">
                 @If item.IsDeutor Then
                     @Format(item.CurrentDeb - item.CurrentHab, "#,##0.00;-#,##0.00;#")
                 End If
             </div>
             <div class="CellAmt CellDesktop">
                 @If item.IsCreditor Then
                     @Format(item.CurrentHab - item.CurrentDeb, "#,##0.00;-#,##0.00;#")
                 End If
             </div>
             <div class="CellAmt CellMobile">
                @Format(item.CurrentDeb - item.CurrentHab, "#,##0.00;-#,##0.00;#")
             </div>
        </div>
    Next

</section>

