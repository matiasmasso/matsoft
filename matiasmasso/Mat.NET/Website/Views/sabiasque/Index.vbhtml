@ModelType DTOSabiasQuePost
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

<H1>@ViewBag.Title</H1>

<p class="Contingut">
    @Html.Raw(Model.Html(ContextHelper.Lang()))
</p>

@Section Styles
    <link href="~/Media/Css/VideoGallery.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            max-width: 600px !important;
            margin: 0 auto;
        }
    </style>
End Section