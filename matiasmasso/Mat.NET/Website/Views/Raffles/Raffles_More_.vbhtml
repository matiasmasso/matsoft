@ModelType DTORaffle.HeadersModel

@If Model.AnyMoreItems() Then
    @<a href="#"
        class="ReadMore"
        data-totalcount="@Model.TotalCount" 
        data-take="@Model.Take"
        data-takefrom="@Model.TakeFrom">

        @Html.Raw(ContextHelper.Tradueix("Ver", "Veure", "See", "ver mais"))

        <span class='Take'>
            @Html.Raw(Model.Take)
        </span>

        @Html.Raw(ContextHelper.Tradueix("más", "més", "more", " "))
        @Html.Raw(ContextHelper.Tradueix("de los", "dels", "from"))

        <span class='LeftCount'>
            @Html.Raw(Model.ItemsLeft())
        </span>

        @String.Format(ContextHelper.Tradueix("restantes", "restants", "left"))
    </a>
End If

