@ModelType DTO.DTOPremiumLine
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim oBreadcrumbs As New DTOProduct.BreadcrumbViewModel()
    oBreadcrumbs.BrandNom = "PremiumLine"
    oBreadcrumbs.Subtiltle = lang.tradueix("¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar")
End Code

 
    @Html.Partial("_ProductBreadcrumbs", oBreadcrumbs)

    @Html.Partial("_StoreLocator")


@Section Scripts
    <script src="~/Media/js/StoreLocator.js"></script>
    <script>
        var lang = '@ContextHelper.Lang.Tag';
        var premiumLine = '@Model.Guid.ToString()';

        $(document).ready(function () {
            loadPremiumStoreLocator(premiumLine, lang);
        });
    </script>
End Section

@Section Styles
    <link href="~/Media/Css/StoreLocator.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            max-width: 600px;
            margin: 0 auto;
        }
    </style>
End Section
