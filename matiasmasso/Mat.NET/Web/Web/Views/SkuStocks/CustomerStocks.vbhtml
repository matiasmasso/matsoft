@ModelType Mvc.StocksModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<h1>Stocks</h1>

<div class="subscribe">
    @lang.Tradueix("Quiero recibir un correo diario a", "Vull rebre un email diari a ", "Please send a daily email to ")
    <a href="mailto:@Model.User.EmailAddress">@Model.User.EmailAddress</a>
    @lang.Tradueix("con los stocks disponibles", "amb els stocks disponibles", "with stock availability")
    <input type="checkbox" @IIf(Model.IsSubscribed, "checked", "") />
</div>


<div class="download">
    <a href="#">descargar Excel con esta información</a>
</div>


<div class="grid-container">
    @For Each oBrand In Model.Brands
        @<div class="brandRow collapsed" data-guid="@oBrand.Guid.ToString()">@oBrand.nom</div>
        @For Each oCategory In oBrand.Categories
            @<div class="categoryRow collapsed" data-guid="@oCategory.Guid.ToString()" data-parent="@oBrand.Guid.ToString()" hidden>
                @oCategory.Nom
            </div>
            @For each oSku In oCategory.Skus
                If oSku.Stock > 0 Then
                    @<div class="skuRow" data-parent="@oCategory.Guid.ToString()" hidden>
                        <div>@oSku.Id &nbsp; @oSku.NomCurt</div>
                        <div>@oSku.Stock</div>
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

        .subscribe, .download {
            text-align: right;
            margin: 20px 0;
            font-size: 0.8em;
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
