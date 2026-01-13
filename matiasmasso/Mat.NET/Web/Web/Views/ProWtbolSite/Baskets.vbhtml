@ModelType DTOWtbolSite
@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang As DTOLang = Mvc.ContextHelper.Lang
End Code

<div>
    <input type="text" class="SearchBox" />
</div>

<div class="Grid" data-contextmenu="Baskets">
    <div class="Row">
        <div>@lang.Tradueix("Fecha", "Data", "Date")</div>
        <div>@lang.Tradueix("Importe", "Import", "Amount")</div>
    </div>
    @For Each item In Model.Baskets
        @<div class="Row" data-guid="@item.Guid.ToString()" >
            <div>@item.Fch.ToString("dd/MM/yy HH:mm")</div>
            <div>@item.Amt.Formatted()</div>
        </div>
    Next
</div>



@Section Scripts
End Section

@Section Styles
    <style scoped>

        .Grid {
            grid-template-columns: auto 200px;
        }
    </style>
End Section
