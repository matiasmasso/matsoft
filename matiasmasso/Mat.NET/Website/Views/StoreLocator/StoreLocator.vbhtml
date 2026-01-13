@ModelType DTO.ProductModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
End Code

 @If Model.Items.Count > 0 Then
            @<div>
                @ContextHelper.Tradueix("Selecciona una marca comercial", "Selecciona una marca comercial", "Pick a brand name")
            </div>

            @<div>
                <select id="BrandSelector">
                    @for each item In Model.Items
                        If item.Tag = Model.Tag Then
                            @<option selected value="@item.Tag">@item.Title</option>
                        Else
                            @<option value="@item.Tag">@item.Title</option>
                        End If
                    Next
                </select>
            </div>
        Else
            @Html.Partial("_ProductBreadcrumbs", Model.Product.Breadcrumbs(lang, "¿Dónde comprar?", "On comprar?", "Where to buy?", "Onde comprar"))
        End If

        @Html.Partial("_StoreLocator")




@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script src="~/Media/js/StoreLocator.js"></script>
    <script>
        var lang = '@ContextHelper.Lang.Tag';
        var product = '@Model.Tag';

        $(document).ready(function () {
            if (product)
                loadStoreLocator(product, lang);
        });

        $('#BrandSelector').change(function () {
            product = $(this).val();
            loadStoreLocator(product, lang);
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
