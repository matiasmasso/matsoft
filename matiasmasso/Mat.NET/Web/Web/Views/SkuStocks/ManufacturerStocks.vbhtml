@ModelType Mvc.StocksModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


    <h1>Stocks</h1>

    <div class="subscribe">
        @Model.Lang.Tradueix("Quiero recibir un correo diario a", "Vull rebre un email diari a ", "Please send a daily email to ")
        <a href="mailto:@Model.User.EmailAddress">@Model.User.EmailAddress</a>
        @Model.Lang.Tradueix("con los stocks disponibles", "amb els stocks disponibles", "with stock availability")
        <input type="checkbox" @IIf(Model.IsSubscribed, "checked", "") />
    </div>

    <div class="download">
        <a href="#">descargar Excel con esta información</a>
    </div>


    <div class="grid-container">

        <div class="skuRef">code</div>
        <div class="skuNom">product</div>
        <div class="skuInt">stock</div>
        <div class="skuInt">customers pending</div>
        <div class="skuInt">supplier pending</div>

        @For Each oBrand In Model.Brands
            @<div class="brandRow">
                <a href="#" target="_blank">
                    @oBrand.Nom
                </a>
            </div>

            @For Each oCategory In oBrand.Categories
                @<div class="categoryRow">
                    <a href="#" target="_blank">
                        @oCategory.Nom
                    </a>
                </div>

                @For each oSku In oCategory.Skus
                    @<div class="skuRef">
                        <a href="#" target="_blank">
                            @oSku.Ref
                        </a>
                    </div>
                    @<div class="skuNom">
                        <a href="#" target="_blank">
                            @oSku.NomPrv
                        </a>
                    </div>
                    @<div class="skuInt">@oSku.Stock</div>
                    @<div class="skuInt">@oSku.CustomersPending</div>
                    @<!--<div class='skuInt @@IIf(oSku.WarnSupplier(), "warnSupplier", "")'>@@oSku.proveidors</div>-->
                    @<div class='skuInt'>@oSku.SuppliersPending</div>
                Next
            Next
        Next
    </div>


@Section Styles
    <style>
        .subscribe, .download {
            text-align: right;
            margin: 20px 0;
            font-size: 0.8em;
        }

        .grid-container {
            display: grid;
            grid-template-columns: auto auto auto auto auto;
            margin-top: 20px;
            grid-gap: 5px;
            border-right: 1px solid lightgray;
            border-bottom: 1px solid lightgray;
        }

            .grid-container div {
                border-top: 1px solid lightgray;
                border-left: 1px solid lightgray;
                width: 100%;
            }

        .brandRow {
            grid-column: 1 / 6;
        }

        .categoryRow {
            grid-column: 1 / 6;
            background-color: #DDDDDD;
        }

        .skuRef {
            justify-self: start;
        }

        .skuNom {
            justify-self: start;
        }

        .skuInt {
            text-align: right;
        }

        .warnSupplier {
            color: red;
        }
    </style>
End Section

@Section Scripts
    <script>
        $('.skuRow').hide();

        $(document).on('click', '.brandRow', function (event) {
            var guid = $(this).data("guid");
            var categoryRows = $('*[data-parent="' + guid + '"]');
            $(this).toggleClass("expanded collapsed")

            if ($(this).hasClass("expanded")) {
                $(categoryRows).show();
            } else {
                $(categoryRows).hide();
            }

            $(categoryRows).each(function (index, categoryRow) {
                if ($(categoryRow).hasClass("expanded")) {
                    $(categoryRow).toggleClass("expanded collapsed")
                    var categoryGuid = $(categoryRow).data("guid");
                    var skus = $('*[data-parent="' + categoryGuid + '"]');
                    $(skus).hide();
                }
            });
        });

        $(document).on('click', '.categoryRow', function (event) {
            var guid = $(this).data("guid");
            var skuRows = $('*[data-parent="' + guid + '"]');
            $(this).toggleClass("expanded collapsed")

            if ($(this).hasClass("expanded")) {
                $(skuRows).attr('display', 'flex');
                $(skuRows).show();
            } else {
                $(skuRows).hide();
            }

        });

        $(".download a").click(function (e) {
            e.preventDefault();
            var url = '@Url.Action("Download")';
            window.location = url;
         });

        $(".subscribe").click(function (e) {
            var isChecked = ($(".subscribe input[type = 'checkbox']").is(':checked'))
            $.ajax({
                url: '@Url.Action("Subscribe")',
                type: 'GET',
                cache: false,
                data: { value: isChecked },
                error: function (jqXHR, status, err) {
                    alert(err);
                    }
                });
            });
    </script>
End Section
