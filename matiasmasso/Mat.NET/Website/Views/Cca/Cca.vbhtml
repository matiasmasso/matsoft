@ModelType DTOCca
@Code
    Layout = "~/Views/Shared/_Layout_FullWidth.vbhtml"

End Code

<section>
    @Model.Concept
</section>
<section>
    @Model.Id
</section>
<section>
    @Model.Fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
</section>

<section class="Grid">
    <div class="RowHdr">
        <div class="CellShortTxt CellDesktop">@ContextHelper.Tradueix("Cuenta", "Compte", "Account")</div>
        <div class="CellShortTxt CellMobile">@ContextHelper.Tradueix("Cuenta", "Compte", "Account")</div>
        <div class="CellTxt">@ContextHelper.Tradueix("Contacto", "Contacte", "Contact")</div>
        <div class="CellAmt CellDesktop">@ContextHelper.Tradueix("Debe", "Debit", "Debit")</div>
        <div class="CellAmt CellDesktop">@ContextHelper.Tradueix("Haber", "Credit", "Credit")</div>
        <div class="CellAmt CellMobile">@ContextHelper.Tradueix("Haber", "Credit", "Credit")</div>
    </div>

    @For Each item As DTOCcb In Model.Items

        @<div class="Row SelectableRow" data-url="@FEB.Ccd.Url(item.Cta, item.Contact, MatHelperStd.TimeHelper.LastDayOfYear(item.Cca.Fch))">
            <div class="CellShortTxt CellDesktop">
                @DTOPgcCta.FullNom(item.Cta, ContextHelper.Lang())
            </div>
            <div class="CellShortTxt CellMobile">
                <a href="@FEB.PgcExtracte.Url(Model.Fch.Year, item.Cta, item.Contact)">
                    @item.Cta.Id
                </a>
            </div>
            <div class="CellTxt Contact">
                @If item.Contact IsNot Nothing Then
                    @item.Contact.FullNom
                End If
            </div>
            <div class="CellAmt CellDesktop">
                @If item.Dh = DTOCcb.DhEnum.debe Then
                    @DTOAmt.CurFormatted(item.Amt)
                End If
            </div>
            <div class="CellAmt CellDesktop">
                @If item.Dh = DTOCcb.DhEnum.haber Then
                    @DTOAmt.CurFormatted(item.Amt)
                End If
            </div>
            <div class="CellAmt CellMobile @IIf(item.Dh = DTOCcb.DhEnum.debe, "Red", "Green")">
                @DTOAmt.CurFormatted(item.Amt)
            </div>
        </div>

    Next
</section>

@If Model.DocFile IsNot Nothing Then
    @<section class="DocFile">
        <a href="@FEB.DocFile.DownloadUrl(Model.DocFile, False)">
            <img src="@FEB.DocFile.ThumbnailUrl(Model.DocFile, True)" />
        </a>
    </section>
End If

@Section Scripts
    <script src="~/Media/js/Tables.js"></script>
End Section

@Section Styles
    <style>
        main {
            max-width: 900px;
        }

        .Contact {
            min-width: 100px;
            width: 40%;
        }

        .DocFile img {
            width: 100%;
            max-width: 350px;
        }
    </style>

End Section
