@ModelType DTOProduct
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

@Html.Partial("_ProductBreadcrumbs", Model.Breadcrumbs(lang, "Galería de imágenes", "Galeria d'imatges", "Image gallery", "Galeria de Imagens"))

<div class="Spinner64"></div>
<div id="ProductGallery"></div>

<input type="hidden" id="productGuid" value="@Model.Guid.ToString()" />



@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script>

        var product = '@Model.Guid.ToString()';

        $(document).ready(function () {
            var url = '/product/PartialImgGallery';
            var data = { guid: product };
            $('#ProductGallery').hide();
            $('#ProductGallery').load(url, data, function (result) {
                $('.Spinner64').hide();
                $(this).slideDown(2000);
            });
        });


    </script>
End Section

@Section Styles
    <link href="~/Media/Css/Product.css" rel="stylesheet" />
End Section