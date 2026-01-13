@ModelType DTORaffle

@Code
    Layout = "~/Views/Shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


    <div class="banner">
        <img src="@FEB2.Raffle.ImgBanner600Url(Model)" width="600" height="200" />
    </div>
    <h1>
        @Html.Raw(Model.Title)
        <br />
        @Html.Raw(Mvc.ContextHelper.Lang.Tradueix("Bases del Sorteo", "Bases del Sorteig", "Raffle Terms", "Bases do Sorteio"))
    </h1>
    <div class="Contingut">
        @Html.Raw(Model.Bases)
    </div>


@Section Styles
    <style>
        .ContentColumn {
            max-width: 600px;
        }

        .banner img {
            width: 100%;
            height: auto;
        }
    </style>
End Section
