@ModelType DTOProductRank
@Code
    Layout = "~/Views/Shared/_Layout_SideNav.vbhtml"

End Code

<div class="pagewrapper">
    <h1>@ViewBag.Title</h1>

    <div class="parameters">
        <div class="Period">
            <span class="Label">@Model.Lang.Tradueix("periodo", "periode", "period")</span>
            <select>
                <option value="0" selected>@Model.Lang.Tradueix("últimos 12 meses", "darrers 12 mesos", "last 12 months")</option>
                <option value="1">@Model.Lang.Tradueix("últimos 3 meses", "darrers 3 mesos", "last 3 months")</option>
                <option value="2">@Model.Lang.Tradueix("últimos mes", "darrer mes", "last month")</option>
            </select>
        </div>

        <div class="Zona">
            <span class="Label">@Model.Lang.Tradueix("zona", "zona", "zone")</span>
            <select>
                <option selected value="@Guid.Empty.ToString">@Model.Lang.Tradueix("(todas las zonas)", "(totes les zones)", "(all areas)")</option>
                @For Each oZona As DTOGuidNom In Model.Zonas
                    @<option value="@oZona.Guid.ToString">@oZona.nom</option>
                Next
            </select>
        </div>
        <div class="Brand">
            <span class="Label">@Model.Lang.Tradueix("marca", "marca", "brand")</span>
            <select>
                <option selected value="@Guid.Empty.ToString">@Model.Lang.Tradueix("(todas las marcas)", "(totes les marques)", "(all brands)")</option>
                @For Each oBrand In Model.Brands
                    @<option value="@oBrand.Guid.ToString">@oBrand.Nom</option>
                Next
            </select>
        </div>
        <div class="Unit">
            <span class="Label">@Model.Lang.Tradueix("medida", "mesura", "measure")</span>
            <select>
                <option @IIf(Model.Unit = DTOProductRank.Units.Units, "selected", "") value="0">@Model.Lang.Tradueix("unidades", "unitats", "units")</option>
                <option @IIf(Model.Unit = DTOProductRank.Units.Eur, "selected", "") value="1">@Model.Lang.Tradueix("importe", "import", "amount")</option>
            </select>
        </div>
    </div>


    <div class="ProductRanking">
        @Html.Partial("ProductRank_")
    </div>


</div>


@Section Scripts
    <script src="~/Media/js/Tables.js"></script>
    <script>
        $(document).on('change', '.Period select, .Zona select, .Zona select, .Brand select, .Unit select', function (event) {
            reload();
        });

        $(document).on('click', '.ProductRanking .Categories .Row', function (e) {
            $('.loading').show();
            var data = {
                fchfrom: $('.FchFrom input').val(),
                fchto: $('.FchTo input').val(),
                unit: $('.Unit select').val(),
                areaguid: $('.Zona select').val(),
                productparentguid: $(this).data('guid')
            };
            $('.ProductRanking').load(
                "/ProductRanking/onCategorySelected",
                { data: JSON.stringify(data) },
                function (e) {
                    $('.loading').hide();
                    $('.BackToMainRanking').show();
                });
        });


        function reload() {
            $('.loading').show();

            $('.ProductRanking').load(
                "/ProductRank/update",
                {
                    period: $('.Period select').val(),
                    zona: $('.Zona select').val(),
                    brand: $('.Brand select').val(),
                    unit: $('.Unit select').val()
                },
                function (e) {
                    $('.loading').hide();
                });
        };


    </script>
End Section

@Section Styles
    <link href="~/Media/Css/tables.css" rel="stylesheet" />
    <style>
        .parameters .Label {
            display: inline-block;
            width: 70px;
        }

        .parameters input, .parameters select {
            width: 150px;
        }

        .BackToMainRanking {
            text-align: right;
        }

        .pagewrapper {
            max-width: 400px;
        }

        #Csv {
            float: right;
        }
    </style>
End Section