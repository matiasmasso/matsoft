@ModelType DTOProduct
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

        @Html.Partial("_ProductBreadcrumbs", Model.Breadcrumbs(lang, "Accesorios", "Accessoris", "Accessories", "Acessórios"))

        <div id="Accessories">
            <div class="Spinner64"></div>
        </div>


    <input type="hidden" id="productGuid" value="@Model.Guid.ToString()" />

@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script>
        $(document).ready(function () {
            loadOrNavigate($('#Accessories'), '/product/PartialAccessories');
        });
    </script>
End Section


@Section Styles
    <link href="~/Media/Css/Product.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            width: 100%;
        }

        #Accessories {
            width: 100%;
            max-width: 100%;
        }
    </style>
End Section

