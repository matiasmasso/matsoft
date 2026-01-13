@ModelType DTO.ProductModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
    Dim sLaunchment As String = DTOProduct.Launchment(Model.Product, Mvc.ContextHelper.Lang())
End Code



@Html.Partial("_ProductBreadcrumbs", Model.Product.Breadcrumbs(lang))

@If Not String.IsNullOrEmpty(sLaunchment) Then
    @<div class="disponibilitat">
        @sLaunchment
    </div>  
End If

<div class="SkuImg">
    <img src="@Model.ImageUrl" alt="@Model.Title" width="700" height="800" />
</div>

@If Model.Retail IsNot Nothing Then
    @<div Class="Pvp">
        @Mvc.ContextHelper.Tradueix("PVP:", "PVP:", "RRPP:")
        &nbsp;
        <Span>@Model.Retail.Formatted()</Span>
    </div>
End If

<hr />

<div class="Contingut">
    @Html.Raw(Model.Text)
</div>

@Html.Partial("_Persianas")

<input type="hidden" id="productGuid" value="@Model.Product.Guid.ToString()" />



@Section Scripts
    <script src="~/Media/js/Product.js"></script>
    <script src="~/Media/js/StoreLocator.js"></script>
    <script>
        var lang = '@Mvc.ContextHelper.Lang.Tag';

        $(document).ready(function () {
            var product = $('#productGuid').val();
            loadStoreLocator(product, lang);
        });

    </script>

End Section

@Section Styles
    <link href="~/Media/Css/Product.css" rel="stylesheet" />
    <link href="~/Media/Css/StoreLocator.css" rel="stylesheet" />
    <link href="~/Styles/VideoPlugins.css" rel="stylesheet" />
    <style scoped>
        .ContentColumn {
            max-width: 600px;
            min-width: 300px;
        }

        .Contingut {
            width: 100%;
        }

        .disponibilitat {
            clear: both;
            color: red;
        }

        .SkuImg {
            background: transparent url('/Media/Img/preloaders/Spinner150.gif') no-repeat scroll center center;
            background-size: auto;
            min-height: 100px; /*per que hi capiga l'spinner*/
            max-width: 600px;
            width: 100%;
        }

            .SkuImg img {
                width: 100%;
                height: auto;
            }

        .Pvp {
            text-align: center;
        }

        @@media(max-width:700px) {
            .PageWrapper {
                display: flex;
                flex-direction: column-reverse !important;
                align-items: center;
            }

            .SideMenuCaption {
                display: block;
                padding: 20px 0 15px 0;
            }
        }
    </style>
End Section

