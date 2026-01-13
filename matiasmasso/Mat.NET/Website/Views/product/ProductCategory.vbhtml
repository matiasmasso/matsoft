@ModelType DTO.ProductModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code


@Html.Partial("_ProductBreadcrumbs", Model.Product.Breadcrumbs(lang))


<div class="Contingut">
    @Html.Raw(Model.Text)
</div>

@Html.Partial("_Persianas")


<input type="hidden" id="productGuid" value="@Model.Product.Guid.ToString()" />


@Section AdditionalMetaTags
    @If CType(Model.Product, DTOProductCategory).obsoleto Then
        @<meta name="robots" content="noindex" />
    Else
        @<meta Property="og:type" content="website" />
        @<meta Property="og:title" content="@ViewBag.Title" />
        If Model.TextHasImages Then
            @<meta property="og:image:url" content="@Model.FacebookImgUrl()" />
        End If
        @<meta Property="og:url" content="@Model.Product.DomainUrl(ContextHelper.Domain)" />
        If Model.Excerpt > "" Then
            @<meta Property="og:description" content="@Html.Raw(Model.Excerpt)" />
        End If
    End If

End Section

@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script src="~/Media/js/StoreLocator.js"></script>
    <script>
        var lang = '@ContextHelper.Lang.Tag';

        $(document).ready(function () {
            var product = $('#productGuid').val();
            loadStoreLocator(product, lang);
        });
    </script>

End Section

@Section Styles
    <script src="https://kit.fontawesome.com/05a6a08892.js" crossorigin="anonymous"></script>
    <link href="~/Media/Css/Plugin.css" rel="stylesheet" />
    <link href="~/Media/Css/Product.css" rel="stylesheet" />
    <link href="~/Media/Css/StoreLocator.css" rel="stylesheet" />
    <link href="~/Styles/VideoPlugins.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            max-width: 600px;
        }
    </style>
End Section


