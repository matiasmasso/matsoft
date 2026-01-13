@ModelType StocksModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, ContextHelper.Lang, ViewBag.Lang)
    Dim oUser = ContextHelper.GetUser()
    Dim showCustomRefs As Boolean = Model.Brands.SelectMany(Function(x) x.Categories).SelectMany(Function(y) y.Skus).Any(Function(z) Not String.IsNullOrEmpty(z.CustomRef))
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


<div class="grid-container">
    @For Each oBrand In Model.Brands
        @<div class="brandRow collapsed" data-guid="@oBrand.Guid.ToString()">@oBrand.Nom</div>
        @For Each oCategory In oBrand.Categories
            @<div class="categoryRow collapsed" data-guid="@oCategory.Guid.ToString()" data-parent="@oBrand.Guid.ToString()" hidden>
                @oCategory.Nom
            </div>
            @For each oSku In oCategory.Skus
                If oSku.Stock > 0 Then
                    @<div class="skuRow" data-parent="@oCategory.Guid.ToString()" hidden>
                        <div>
                            @If showCustomRefs Then
                                @<span class="skuCustomRef">@oSku.CustomRef</span>
                            Else
                                @<span>@oSku.Id</span>
                            End If
                            &nbsp;
                            @oSku.NomCurt
                        </div>
                        <div>@(Math.Max(0, oSku.Stock - oSku.CustomersPending))</div>
                    </div>
                End If
            Next
        Next
    Next
</div>

@Html.Partial("_AvailableOnAppstore")

@Section Styles
    <style scoped>
        .ContentColumn {
            max-width: 600px;
            margin: 0 auto;
        }

        .Subscribe, .DownloadStocks {
            display: flex;
            align-items: center;
            justify-content: end;
            margin: 15px 0;
            font-size: 0.9em;
        }

           .Subscribe .Label, .DownloadStocks .Label {
                margin: 0 5px 0 0;
            }


        .grid-container {
            display: flex;
            flex-direction: column;
            margin: 20px 0;
        }

        .brandRow {
            /*margin: 20px 0px 20px 0px;*/
            padding: 10px;
            border-bottom: 1px solid LightGrey;
            border-left: 1px solid LightGrey;
            border-right: 1px solid LightGrey;
        }

            .brandRow:hover {
                font-weight: 600;
                background-color: LightGrey;
                cursor: pointer;
            }

            .brandRow:first-child {
                margin-top: 0px;
                border-top: 1px solid LightGrey;
            }

            .brandRow.expanded {
                font-weight: 600;
                background-color: LightGrey;
            }

        .categoryRow {
            display: none;
            border-bottom: 1px solid LightGrey;
            border-left: 1px solid LightGrey;
            border-right: 1px solid LightGrey;
            padding: 10px 10px 10px 50px;
            /*margin: 20px 0px 20px 20px;*/
        }

            .categoryRow:hover {
                font-weight: 600;
                background-color: #EEEEEE;
                cursor: pointer;
            }

            .categoryRow.expanded {
                font-weight: 600;
                background-color: #EEEEEE;
            }

        .skuRow {
            display: flex;
            padding: 10px 10px 10px 100px;
            border-bottom: 1px solid LightGrey;
            border-left: 1px solid LightGrey;
            border-right: 1px solid LightGrey;
            flex-direction: row;
            justify-content: space-between;
            color: gray;
        }

        .skuCustomRef {
            color: crimson;
        }


        @@media screen and (max-width: 400px) {
            .categoryRow {
                padding-left: 20px;
            }

            .skuRow {
                padding-left: 40px;
            }
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


         $(".DownloadStocks").click(function (e) {
            e.preventDefault();

             $(this).find('.Ico').hide();
             $(this).append(spinner20);
            var url = '@MmoUrl.ApiUrl("SkuStocks/Excel", oUser.Guid.ToString())';
            var filename = 'M+O - stocks.xlsx';
            fetch(url)
                .then(resp => resp.blob())
                .then(blob => {
                    DownloadFetchedExcel(blob, filename);
                    spinner20.remove();
                    $(this).find('.Ico').show();
                })
                .catch(error => {
                    spinner20.remove();
                    $(this).find('.Ico').show();
                    $('.Errs').text('Error: ' + error.message);
                });
        });

    </script>
End Section
