@ModelType DTOComputer
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<h1>@Model.Nom</h1>

@Html.Raw(Model.Html())


@Section Styles
    <style scoped>

    </style>
End Section