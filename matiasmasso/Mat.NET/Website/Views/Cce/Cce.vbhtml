@ModelType DTOPgcCta
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"

    Dim exs As New List(Of Exception)
    Dim items As List(Of DTOBalanceSaldo) = FEB.Balance.CceSync(exs, Website.GlobalVariables.Emp, Model, ViewBag.fch)
    Dim DtFch As Date = CDate(ViewBag.fch)
    Dim oClass As DTOPgcClass = Model.PgcClass
    Dim oRoot As DTOPgcClass = DTOPgcClass.Root(oClass)
    Dim oCod As DTOPgcClass.Cods = oRoot.Cod
End Code

<div>
    fecha: @DtFch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
    Cta: @DTOPgcCta.FullNom(Model, ContextHelper.Lang())
</div>

<div class="Grid">
    <div class="RowHdr">
        <div class="CellTxt">
            @ContextHelper.Tradueix("Concepto", "Concepte", "Concept")
        </div>
        <div class="CellNum CurrentYear">
            @Html.Raw(DtFch.Year)
        </div>
        <div class="CellNum PreviousYear">
            @Html.Raw(DtFch.Year - 1)
        </div>
    </div>

    <div class="RowHdr">
        <div class="CellTxt">
            @ContextHelper.Tradueix("Totales", "Totals", "Totals")
        </div>
        <div class="CellNum CurrentYear">
            @Select Case oCod
                Case DTOPgcClass.Cods.aA_Activo
                    @<span>@Format(items.Sum(Function(x) x.CurrentDeb - x.CurrentHab), "#,##0.00;-#,##0.00;#")</span>
                Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                                        @<span>@Format(items.Sum(Function(x) x.CurrentHab - x.CurrentDeb), "#,##0.00;-#,##0.00;#")</span>
            End Select
        </div>
        <div class="CellNum PreviousYear">
            @Select Case oCod
                Case DTOPgcClass.Cods.aA_Activo
                    @<span>@Format(items.Sum(Function(x) x.PreviousDeb - x.PreviousHab), "#,##0.00;-#,##0.00;#")</span>
                Case DTOPgcClass.Cods.aB_Pasivo, DTOPgcClass.Cods.b_Cuenta_Explotacion
                                        @<span>@Format(items.Sum(Function(x) x.PreviousHab - x.PreviousDeb), "#,##0.00;-#,##0.00;#")</span>
            End Select
        </div>
    </div>


    @For Each item As DTOBalanceSaldo In items
        @<div Class="Row SelectableRow" data-url="@FEB.Ccd.Url(Model, item.Contact, ViewBag.fch)">
            <div Class="CellTxt">
                @If item.Contact Is Nothing Then
                    @<span>&nbsp;</span>
                Else
                    @item.Contact.Nom
                End If
            </div>
            <div Class="CellBigNum CurrentYear">
                @Format(FEB.Balance.CurrentYearEur(item, oCod), "#,##0.00;-#,##0.00;#")
            </div>
            <div Class="CellBigNum PreviousYear">
                @Format(FEB.Balance.PreviousYearEur(item, oCod), "#,##0.00;-#,##0.00;#")
            </div>
        </div>
    Next
</div>

@Section scripts
    <script src="~/Media/js/Tables.js"></script>
End Section
@Section styles
    <style>
        main {
            max-width: 700px;
        }

        .CellNum {
            max-width: 100px;
        }

        @@media screen and (max-width:700px) {
            main {
                max-width: 100%;
            }
        }

        @@media screen and (max-width:400px) {
            .PreviousYear {
                display: none;
            }
        }
    </style>
End Section