@ModelType StocksModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim oUser = ContextHelper.GetUser()
End Code


<h1>Stocks</h1>

<div class="Subscribe">
    <span class="Label">
        @lang.Tradueix("Quiero recibir un correo diario a", "Vull rebre un email diari a ", "Please send a daily email to ")
        &nbsp;<a href="mailto:@Model.User.EmailAddress">@Model.User.EmailAddress</a>
        @lang.Tradueix(" con los stocks disponibles", " amb els stocks disponibles", " with stock availability")
    </span>
    <input type="checkbox" @IIf(Model.IsSubscribed, "checked", "") />
</div>


<a href="#" class="DownloadStocks">
    <span class="Label">
        @lang.Tradueix("descargar stocks en Excel", "Descarregar stocks en Excel", "Download Excel stocks")
    </span>
    <img class="Ico" src="~/Media/Img/Ico/download-16.png" width="16" height="16" alt='@lang.tradueix("descargar Excel", "descarregar Excel", "download Excel")' />
</a>


<a href="#" class="DownloadStockMovements">
    <span class="Label">
        @lang.Tradueix("descargar movimientos de stock en Excel", "Descarregar moviment de stocks en Excel", "Download Excel stocks movement")
    </span>
    <img class="Ico" src="~/Media/Img/Ico/download-16.png" width="16" height="16" alt='@lang.tradueix("descargar Excel", "descarregar Excel", "download Excel")' />
</a>



<div class="Errs"></div>


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
        .Errs {
            color: red;
            font-weight: 500;
        }

        .Subscribe, .DownloadStocks, .DownloadStockMovements {
            display: flex;
            align-items: center;
            justify-content: end;
            margin: 15px 0;
            font-size: 0.9em;
        }

            .Subscribe .Label, .DownloadStocks .Label, .DownloadStockMovements .Label {
                margin: 0 5px 0 0;
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
    <script src="https://kit.fontawesome.com/05a6a08892.js" crossorigin="anonymous"></script>
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

        $(".DownloadStocks").click(function (e) {
            e.preventDefault();

            $(this).append(spinner20);
            var url = '@MmoUrl.ApiUrl("SkuStocks/Excel", oUser.Guid.ToString())';
            var filename = 'M+O - stocks.xlsx';
            fetch(url)
                .then(resp => resp.blob())
                .then(blob => {
                    DownloadFetchedExcel(blob, filename);
                })
                .then(() => {
                    spinner20.remove();
                })
                .catch(error => {
                    spinner20.remove();
                    $('.Errs').text('Error: ' + error.message);
                });
        });

        $(".DownloadStockMovements").click(function (e) {
            e.preventDefault();

            $(this).append(spinner20);
            var url = '@MmoUrl.ApiUrl("SkuStocks/StockMovements", oUser.Guid.ToString(), DTO.GlobalVariables.Today().Year.ToString())';
            var filename = 'M+O - stock movements.xlsx';
            fetch(url)
                .then(resp => resp.blob())
                .then(blob => {
                    DownloadFetchedExcel(blob, filename);
                })
                .then(() => {
                    spinner20.remove();
                })
                .catch(error => {
                    spinner20.remove();
                    $('.Errs').text('Error: '+ error.message);
                });
        });

            function DownloadFetchedExcel(blob, filename) {
                const url = window.URL.createObjectURL(blob);
                const a = document.createElement('a');
                a.style.display = 'none';
                a.href = url;
                // the filename you want
                a.download = filename;
                document.body.appendChild(a);
                a.click();
                window.URL.revokeObjectURL(url);
            }

        $(".Subscribe").click(function (e) {
            var isChecked = ($(".Subscribe input[type = 'checkbox']").is(':checked'))
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
