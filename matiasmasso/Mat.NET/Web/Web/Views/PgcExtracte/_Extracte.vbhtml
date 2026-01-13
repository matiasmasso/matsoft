@ModelType DTOPgcExtracte
@Code
    
    Dim oSaldo = DTOAmt.Empty
End Code

<section class="Compte">
    @Mvc.ContextHelper.Tradueix("Cuenta", "Compte", "Account"):
    <a href="@FEB2.SumasYSaldos.UrlSubComptes(Model.Exercici, Model.Cta)">
        @DTOPgcCta.FullNom(Model.Cta, Mvc.ContextHelper.lang())
    </a>
</section>

@If Model.Contact IsNot Nothing Then
    @<section class="SubCompte">
        @Mvc.ContextHelper.Tradueix("Subcuenta", "Subcompte", "Subaccount"):
        <a href="@FEB2.Contact.Url(Model.Contact)">
            @Model.Contact.FullNom
        </a>
    </section>
End If

<section class="Grid">
    <div class="RowHdr">
        <div class="CellNum CellDesktop">@Mvc.ContextHelper.Tradueix("Num", "Num", "Num")</div>
        <div class="CellIco"></div>
        <div class="CellFch CellDesktop">@Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")</div>
        <div class="CellFch CellMobile">@Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")</div>
        <div class="CellTxt">@Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
        <div class="CellAmt CellDesktop">@Mvc.ContextHelper.Tradueix("Debe", "Debit", "Debit")</div>
        <div class="CellAmt CellDesktop">@Mvc.ContextHelper.Tradueix("Haber", "Credit", "Credit")</div>
        <div class="CellAmt CellMobile">@Mvc.ContextHelper.Tradueix("Importe", "Import", "Amount")</div>
        <div class="CellAmt CellDesktop">@Mvc.ContextHelper.Tradueix("Saldo", "Saldo", "Balance")</div>
    </div>

    @For Each item As DTOCcb In Model.items
        DTOPgcSaldo.UpdateSaldo(oSaldo, item)

        @<div class="Row">
            <div class="CellNum CellDesktop">@item.Cca.Id</div>
            <div class="CellIco">
                @If item.Cca.DocFile IsNot Nothing Then
                    @<a href="@FEB2.DocFile.DownloadUrl(item.Cca.DocFile, False)">
                        <img src="~/Media/Img/Ico/pdf.gif" />
                    </a>
                End If
            </div>
            <div class="CellFch CellDesktop">@Format(item.Cca.Fch, "dd/MM/yy")</div>
            <div class="CellFch CellMobile">@Format(item.Cca.Fch, "dd/MM")</div>
            <div class="CellTxt">
                <a href="@FEB2.Cca.Url(item.Cca)">
                    @item.Cca.Concept
                </a>
            </div>
            <div class="CellAmt CellDesktop">
                @If item.Dh = DTOCcb.DhEnum.Debe Then
                    @DTOAmt.CurFormatted(item.Amt)
                End If
            </div>
            <div class="CellAmt CellDesktop">
                @If item.Dh = DTOCcb.DhEnum.Haber Then
                    @DTOAmt.CurFormatted(item.Amt)
                End If
            </div>
            <div class='CellAmt CellMobile @IIf(item.Dh = item.Cta.Act, "Green", "Red")'>
                @If item.Dh = item.Cta.Act Then
                    @DTOAmt.CurFormatted(item.Amt)
                Else
                    @DTOAmt.CurFormatted(item.Amt.Inverse)
                End If
            </div>
            <div class="CellAmt CellDesktop">
                @DTOAmt.CurFormatted(oSaldo)
            </div>
        </div>

    Next
</section>


