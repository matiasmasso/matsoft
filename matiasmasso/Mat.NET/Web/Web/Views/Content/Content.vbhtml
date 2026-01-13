@ModelType DTOContent
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<h1 class="Title">@ViewBag.Title</h1>
<div class="Contingut">
    @Html.Raw(Model.Html(Mvc.ContextHelper.Lang))
</div>

@Section Styles
    <script src="https://kit.fontawesome.com/05a6a08892.js" crossorigin="anonymous"></script>
    <link href="~/Media/Css/Plugin.css" rel="stylesheet" />
    <style scoped>
        .ContentColumn {
            max-width: 600px;
        }
    </style>
End Section
