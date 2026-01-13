@ModelType DTOCcd
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"

    Dim DcSaldo As Decimal = 0
End Code



<div>
    @DTOPgcCta.FullNom(Model.Cta, Mvc.ContextHelper.Lang())
</div>

@If Model.Contact IsNot Nothing Then
    @<div>
        @Model.Contact.FullNom
    </div>  End If

<div class="OtherYears">
    <a href="@FEB2.Ccd.Url(Model.Cta, Model.Contact, MatHelperStd.TimeHelper.LastDayOfYear(Model.Fch).AddYears(-1))">
        @Mvc.ContextHelper.Tradueix("Ejercicio anterior", "Exercici anterior", "Previous year")
    </a>
</div>

<div class="Grid">
    <div class="RowHdr">
        <div class="CellNum Cca">
            @Mvc.ContextHelper.Tradueix("Asiento", "Assentament", "Log")
        </div>
        <div class="CellIco Pdf">
            &nbsp;
        </div>
        <div class="CellFch">
            @Mvc.ContextHelper.Tradueix("Fecha", "Data", "Date")
        </div>
        <div class="CellTxt">
            @Mvc.ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
        </div>
        <div class="CellAmt">
            @Mvc.ContextHelper.Tradueix("Debe", "Deure", "Debit")
        </div>
        <div class="CellAmt">
            @Mvc.ContextHelper.Tradueix("Haber", "Haver", "Credit")
        </div>
        <div class="CellAmt">
            @Mvc.ContextHelper.Tradueix("Saldo", "Saldo", "Balance")
        </div>
    </div>



    @For Each item As DTOCcb In Model.Ccbs
        @<div Class="Row SelectableRow" data-url="@FEB2.Cca.Url(item.Cca)">

            <div class="CellNum Cca">
                @item.Cca.Id
            </div>
            <div class='@IIf(item.Cca.DocFile Is Nothing, "CellIco", "CellPdf") Pdf'>
                &nbsp;
            </div>
            <div class="CellFch">
                @Format(item.Cca.Fch, "dd/MM/yy")
            </div>
            <div class="CellTxt">
                @item.Cca.Concept
            </div>
            <div class="CellAmt">
                @Format(IIf(item.Dh = DTOCcb.DhEnum.debe, item.Amt.Eur, 0), "#,##0.00;-#,##0.00;#")
            </div>
            <div class="CellAmt">
                @Format(IIf(item.Dh = DTOCcb.DhEnum.haber, item.Amt.Eur, 0), "#,##0.00;-#,##0.00;#")
            </div>
            <div class="CellAmt">
                @Format(DTOPgcCta.Saldo(DcSaldo, item), "#,##0.00;-#,##0.00;#")
            </div>
        </div>
        @Code
            DcSaldo = DTOPgcCta.Saldo(DcSaldo, item)
        End Code
    Next

    <div Class="Row">

        <div class="CellNum Cca">
            &nbsp;
        </div>
        <div class='CellIco Pdf'>
            &nbsp;
        </div>
        <div class="CellFch">
            @IIf(Model.Fch = Nothing, "&nbsp;", Format(Model.Fch, "dd/MM/yy"))
        </div>
        <div class="CellTxt">
            @Mvc.ContextHelper.Tradueix("totales", "totals", "totals")
        </div>
        <div class="CellAmt">
            @Format(Model.Ccbs.Where(Function(x) x.Dh = DTOCcb.DhEnum.debe).Sum(Function(x) x.Amt.Eur), "#,##0.00;-#,##0.00;#")
        </div>
        <div class="CellAmt">
            @Format(Model.Ccbs.Where(Function(x) x.Dh = DTOCcb.DhEnum.haber).Sum(Function(x) x.Amt.Eur), "#,##0.00;-#,##0.00;#")
        </div>
        <div class="CellAmt">
            @Format(DcSaldo, "#,##0.00;-#,##0.00;#")
        </div>
    </div>


</div>

<div class="OtherYears">
    <a href="@FEB2.Ccd.Url(Model.Cta, Model.Contact, MatHelperStd.TimeHelper.LastDayOfYear(Model.Fch).AddYears(1))">
        @Mvc.ContextHelper.Tradueix("Siguiente Ejercicio", "Següent Exercici", "Next year")
    </a>
</div>

@Section Scripts
    <script src="~/Media/js/Tables.js"></script>
End Section

@Section styles
    <style>

        main {
            max-width: 900px;
        }

        .CellTxt {
            min-width: 150px;
        }

        .OtherYears {
            text-align: right;
        }


        @@media screen and (max-width:900px) {
            main {
                max-width: 100%;
            }
        }

        @@media screen and (max-width:500px) {
            .CellNum.Cca, .Row .Pdf, .RowHdr .Pdf {
                display: none;
            }
        }
    </style>
End Section