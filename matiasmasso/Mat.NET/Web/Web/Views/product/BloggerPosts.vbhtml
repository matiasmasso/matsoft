@ModelType DTOProduct
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

@Html.Partial("_ProductBreadcrumbs", Model.Breadcrumbs(lang, "Publicaciones", "Publicacions", "Posts"))

<div class="Spinner64"></div>
<div id="BloggerPosts"></div>


<input type="hidden" id="productGuid" value="@Model.Guid.ToString()" />

@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script>

        var product = '@Model.Guid.ToString()';

        $(document).ready(function () {
            var url = '/product/PartialBloggerPosts';
            var data = { guid: product };
            $('#BloggerPosts').hide();
            $('#BloggerPosts').load(url, data, function (result) {
                $('.Spinner64').hide();
                $(this).slideDown(2000);
            });
        });


    </script>
End Section

@Section Styles
    <link href="~/Media/Css/Product.css" rel="stylesheet" />
End Section