@ModelType DTORaffle.HeadersModel

@If Model.AnyMoreItems() Then
    @<a href="#"
        class="ReadMore"
        data-totalcount="@Model.TotalCount" 
        data-take="@Model.Take"
        data-takefrom="@Model.TakeFrom">

        @Html.Raw(Mvc.ContextHelper.Tradueix("Ver", "Veure", "See", "ver mais"))

        <span class='Take'>
            @Html.Raw(Model.Take)
        </span>

        @Html.Raw(Mvc.ContextHelper.Tradueix("más", "més", "more", " "))
        @Html.Raw(Mvc.ContextHelper.Tradueix("de los", "dels", "from"))

        <span class='LeftCount'>
            @Html.Raw(Model.ItemsLeft())
        </span>

        @String.Format(Mvc.ContextHelper.Tradueix("restantes", "restants", "left"))
    </a>
End If

