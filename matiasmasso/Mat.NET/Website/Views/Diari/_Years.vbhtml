@ModelType DTODiari
@Code
    Dim exs As New List(Of Exception)
    Dim years As List(Of DTODiariItem) = FEB.Diari.Years(Model, exs)
    Dim months As List(Of DTODiariItem) = FEB.Diari.Months(Model, exs)
    Dim days As List(Of DTODiariItem) = FEB.Diari.Days(Model, exs)
End Code

<div class="Grid">

    <div class="RowHdr">
        <div class="CellTxt">
            @Model.Lang.Tradueix("Ejercicios", "Exercicis", "Exercises")
        </div>
        <div class="CellAmt">
            @Model.Lang.Tradueix("Totales", "Totals", "Totals")
        </div>
        @For Each oBrand As DTOProductBrand In Model.Brands
            @<div class="CellAmt">
                @oBrand.Nom
            </div>
        Next
        <div class="CellAmt">
            @Model.Lang.Tradueix("Otras", "Altres", "Others")
        </div>
    </div>

        @For i As Integer = 0 To 2 'years.Count - 1
            @<div class='@IIf(i > 2, "Row RowMore","Row")'>
                <div class="CellTxt">
                    <a href="#" data-year="@years(i).Text">
                        @years(i).Text
                    </a>
                </div>
                <div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOAmt.Factory(years(i).Total))
                </div>
                @For j As Integer = 0 To Model.Brands.Count - 1
                    @<div class="CellAmt">
                        @DTOAmt.CurFormatted(DTOAmt.Factory(years(i).Values(j)))
                    </div>
                Next
                <div class="CellAmt">
                    @DTOAmt.CurFormatted(DTOAmt.Factory(years(i).Resto))
                </div>
            </div>
        Next
            
<div class="RowHdr">
    <div class="CellTxt">&nbsp;</div>
    <div class="CellAmt">&nbsp;</div>
    @For Each oBrand As DTOProductBrand In Model.Brands
        @<div class="CellAmt">&nbsp;</div>
    Next
    <div class="CellAmt">&nbsp;</div>
</div>

<div class="RowHdr">
    <div class="CellTxt">
        @Model.Lang.Tradueix("Meses", "Mesos", "Months")
    </div>
    <div class="CellAmt">
        @Model.Lang.Tradueix("Totales", "Totals", "Totals")
    </div>
    @For Each oBrand As DTOProductBrand In Model.Brands
        @<div class="CellAmt">
            @oBrand.Nom
        </div>
    Next
    <div class="CellAmt">
        @Model.Lang.Tradueix("Otras", "Altres", "Others")
    </div>
</div>

@For i As Integer = 0 To months.Count - 1
    @<div class='Row'>
        <div class="CellTxt">
            <a href="#" data-year="@Model.Year" data-month="@months(i).Source">
                @months(i).Text
            </a>
        </div>
        <div class="CellAmt">
            @DTOAmt.CurFormatted(DTOAmt.Factory(months(i).Total))
        </div>
        @For j As Integer = 0 To Model.Brands.Count - 1
            @<div class="CellAmt">
                @DTOAmt.CurFormatted(DTOAmt.Factory(months(i).Values(j)))
            </div>
        Next
        <div class="CellAmt">
            @DTOAmt.CurFormatted(DTOAmt.Factory(months(i).Resto))
        </div>
    </div>  
Next

<div class="RowHdr">
    <div class="CellTxt">&nbsp;</div>
    <div class="CellAmt">&nbsp;</div>
    @For Each oBrand As DTOProductBrand In Model.Brands
        @<div class="CellAmt">&nbsp;</div>
    Next
    <div class="CellAmt">&nbsp;</div>
</div>

<div class="RowHdr">
    <div class="CellTxt">
        @(Model.Year & " " & Model.Lang.Mes(Model.Month))
    </div>
    <div class="CellAmt">
        @Model.Lang.Tradueix("Totales", "Totals", "Totals")
    </div>
    @For Each oBrand As DTOProductBrand In Model.Brands
        @<div class="CellAmt">
            @oBrand.Nom
        </div>
    Next
    <div class="CellAmt">
        @Model.Lang.Tradueix("Otras", "Altres", "Others")
    </div>
</div>

@For i As Integer = 0 To days.Count - 1
    @<div class='Row'>
        <div class="CellTxt">
            <a href="#" data-year="@Model.Year" data-month="@Model.Month" data-day="@days(i).Source">
                @days(i).Text
            </a>
        </div>
        <div class="CellAmt">
            @DTOAmt.CurFormatted(DTOAmt.Factory(days(i).Total))
        </div>
        @For j As Integer = 0 To Model.Brands.Count - 1
            @<div class="CellAmt">
                @DTOAmt.CurFormatted(DTOAmt.Factory(days(i).Values(j)))
            </div>
        Next
        <div class="CellAmt">
            @DTOAmt.CurFormatted(DTOAmt.Factory(days(i).Resto))
        </div>
    </div>
Next

    </div>

<a href="#" class="moreYears">@Model.Lang.Tradueix("ver todod los ejercicios","tots els exercicis","see more years")</a>


