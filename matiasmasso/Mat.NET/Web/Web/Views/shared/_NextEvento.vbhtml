@ModelType DTOEvento


<div class="MatBox180">
    <a href="@FEB2.Evento.UrlFriendly(Model, False)">
        <div class="MatBoxHeaderBlue">
            @Html.Raw(Mvc.ContextHelper.Tradueix("próximos eventos", "propers esdeveniments", "next events", "eventos vindouros"))
        </div>
        <img src="@FEB2.Evento.UrlThumbnail(Model, False)" alt="@Model.Title.Tradueix(Mvc.ContextHelper.lang())">
        <div class="MatBoxFooter">
            @Model.Title.Tradueix(Mvc.ContextHelper.lang())
        </div>
    </a>
</div>

