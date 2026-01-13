@ModelType DTOProduct
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


@Html.Partial("_ProductBreadcrumbs", Model.Breadcrumbs(lang, "Vídeos"))

<div id="Videos">
    <div class="Spinner64"></div>
</div>

<input type="hidden" id="productGuid" value="@Model.Guid.ToString()" />

@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script>
        $(document).ready(function () {
            loadOrNavigate($('#Videos'), '/product/PartialVideos');
        });
    </script>
End Section

@Section Styles
    <link href="~/Media/Css/Product.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            width: 100%;
        }
    </style>
End Section

