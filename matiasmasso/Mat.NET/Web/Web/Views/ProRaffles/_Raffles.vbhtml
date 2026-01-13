
@ModelType List(Of DTORaffle)

<div class="Row">
    <div class="CenterAlign">@Mvc.ContextHelper.Tradueix("Inicio", "Inici", "Start", "")</div>
    <div class="CenterAlign">@Mvc.ContextHelper.Tradueix("Final", "Final", "End", "")</div>
    <div class="CenterAlign">@Mvc.ContextHelper.Tradueix("Idioma", "Idioma", "", "")</div>
    <div>@Mvc.ContextHelper.Tradueix("Título", "Titol", "", "")</div>
    <div class="RightAlign">@Mvc.ContextHelper.Tradueix("Leads", "Leads", "", "")</div>
    <div class="RightAlign">@Mvc.ContextHelper.Tradueix("Nuevos", "Nous", "", "")</div>
    <div class="RightAlign">@Mvc.ContextHelper.Tradueix("Nuevos", "Nous", "", "")</div>
    <div class="RightAlign">@Mvc.ContextHelper.Tradueix("Premio", "Premi", "", "")</div>
    <div class="RightAlign">@Mvc.ContextHelper.Tradueix("Publi", "Publi", "", "")</div>
    <div class="RightAlign">@Mvc.ContextHelper.Tradueix("Cpl", "Cpl", "", "")</div>
    <div>&nbsp;</div>
</div>

@For Each item In Model
    @<div class="Row" data-guid="@item.Guid.ToString()">
    <div class="CenterAlign">@item.FchFrom.ToString("dd/MM/yy")</div>
    <div class="CenterAlign">@item.FchTo.ToString("dd/MM/yy")</div>
    <div class="CenterAlign">@item.Lang.Tag</div>
    <div>@item.Title</div>
    <div class="RightAlign">@item.ParticipantsCount.ToString("N0")</div>
    <div class="RightAlign">@item.NewParticipantsCount.ToString("N0")</div>
    <div class="RightAlign">@item.RateNewLeads().ToString("N0") %</div>
    <div class="RightAlign">@DTOAmt.CurFormatted(item.CostPrize)</div>
    <div Class="RightAlign">@DTOAmt.CurFormatted(item.CostPubli)</div>
    <div class="RightAlign">@DTOAmt.CurFormatted(item.Cpl())</div>
    <div class='Status @Choose(item.Status, "WinnerReacted", "DistributorReacted", "Delivered", "WinnerPictureSubmitted")'>&nbsp;</div>
</div>
Next