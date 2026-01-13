@ModelType DTOPgcClass
@Code
    
    Dim oLang As DTOLang = ContextHelper.lang()
    Dim oCod As DTOPgcClass.Cods = DTOPgcClass.Root(Model).Cod
    Dim IsRoot As Boolean = Model.Equals(DTOPgcClass.Root(Model))
    Dim DrillDownAllowed As Boolean = ViewBag.DrillDownAllowed
End Code

    
@If IsRoot Then
    @<div Class="Row">
        <div Class="CellTxt">
            &nbsp;
        </div>
        <div Class="CellBigNum CurrentYear">
                    &nbsp;
        </div>
        <div Class="CellBigNum PreviousYear">
             &nbsp;
        </div>
    </div>

End If

    <div Class="Row">
        <div Class='CellTxt @iif(IsRoot, "RootPgcClass", "")'>
            @Html.Raw(WebHelper.Indent(Model.Level * 2))
            @Model.Nom.Tradueix(oLang)
        </div>
        <div class="CellBigNum CurrentYear">
            @If Model.HideFigures Then
                @<span>&nbsp;</span>
            Else
                @<span>@Format(FEB.Balance.CurrentYearEur(Model, oCod), "#,##0.00;-#,##0.00;#")</span>
            End If
        </div>
        <div Class="CellBigNum PreviousYear">
            @If Model.HideFigures Then
                @<span>&nbsp;</span>
            Else
                @<span>@Format(FEB.Balance.PreviousYearEur(Model, oCod), "#,##0.00;-#,##0.00;#")</span>
            End If
        </div>
    </div>


    @For Each oClass As DTOPgcClass In Model.Children
        @Html.Partial("ClassItem_", oClass)
    Next

    @For Each oCta As DTOBalanceSaldo In Model.Ctas
        @<div class='Row' @IIf(DrillDownAllowed, "data-url=" & FEB.Cce.Url(oCta, ViewBag.Fch) & "", "")>
            <div class="CellTxt">
                @Html.Raw(WebHelper.Indent(8 * 2))
                @DTOPgcCta.FullNom(oCta, oLang)
            </div>
            <div class="CellBigNum CurrentYear">
                @Format(FEB.Balance.CurrentYearEur(oCta, oCod), "#,##0.00;-#,##0.00;#")
            </div>
            <div class="CellBigNum PreviousYear">
                @Format(FEB.Balance.PreviousYearEur(oCta, oCod), "#,##0.00;-#,##0.00;#")
            </div>
        </div>
    Next

