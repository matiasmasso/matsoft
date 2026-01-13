@ModelType DTOEvento


<div class="MatBox180">
    <a href="@FEB.Evento.UrlFriendly(Model, False)">
        <div class="MatBoxHeaderBlue">
            @Html.Raw(ContextHelper.Tradueix("próximos eventos", "propers esdeveniments", "next events", "eventos vindouros"))
        </div>
        <img src="@FEB.Evento.UrlThumbnail(Model, False)" alt="@Model.Title.Tradueix(ContextHelper.lang())">
        <div class="MatBoxFooter">
            @Model.Title.Tradueix(ContextHelper.lang())
        </div>
    </a>
</div>

