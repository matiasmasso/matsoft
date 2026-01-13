@ModelType DTO.ProductModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code


@Html.Partial("_ProductBreadcrumbs", Model.Product.Breadcrumbs(lang, "¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar"))


        @If Model.Items IsNot Nothing AndAlso Model.Items.Count > 0 Then
            @<div>
                <select id="BrandSelector" onchange="brandChanged()">
                    <option value=""> (seleccione una marca)</option>
                    @For Each item In Model.Items
                        @<option value="@item.Tag" @IIf(item.Tag = Model.Tag, "selected", "")>
                            @item.Title
                        </option>
                    Next
                </select>
            </div>
        End If

        @Html.Partial("_StoreLocator")


@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script src="~/Media/js/StoreLocator.js"></script>
    <script>
        var lang = '@ContextHelper.Lang.Tag';

        $(document).ready(function () {
            var product = '@Model.Tag';
            loadStoreLocator(product, lang);
        });

            function brandChanged() {
                var product = $('#BrandSelector').val();
                loadStoreLocator(product, lang);
            }

    </script>
End Section

@Section Styles
    <link href="~/Media/Css/StoreLocator.css" rel="stylesheet" />
    <link href="~/Media/Css/Product.css" rel="stylesheet" />
    <style>
        .ContentColumn {
            max-width: 600px;
        }
    </style>
End Section


