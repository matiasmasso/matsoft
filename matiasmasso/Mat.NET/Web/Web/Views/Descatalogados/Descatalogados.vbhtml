@ModelType Mvc.DescatalogadosModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang As DTOLang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code


<div class="fchFrom">
    @lang.Tradueix("Mostrar productos descatalogados desde", "Mostrar productes descatalogats des de", "see outdated products from", "Mostrar produtos descontinuados desde")
    <input type="date"
           value='@Model.FchFrom.ToString("yyyy-MM-dd")'
           min="2018-01-01" max='@Today.ToString("yyyy-MM-dd")'>
    <input id="reloadButton" type="button" disabled="disabled" value='@lang.Tradueix("actualizar", "actualitzar", "update", "atualizar")' />
</div>


<div class="subscribe">
    @lang.Tradueix("Quiero recibir un correo a", "Vull rebre un correo a", "Please send me an email to", "Quero receber um email para")
    @Model.User.EmailAddress
    @lang.Tradueix("en cuanto se descataloguen más productos", "quan es descataloguin mes productes", "when any more `products get outdated", "assim que mais produtos forem descontinuados")

    <input type="checkbox" @IIf(Model.IsSubscribed, "checked", "") />
</div>


<div class="download">
    <a href="#">
        @lang.Tradueix("descargar un Excel con esta información", "descarregar un Excel amb aquesta informació", "Download an Excel sheet with this info", "baixe um Excel com esta informação")
    </a>
</div>
<div class="downloading" hidden>
    <a href="#">
        @lang.Tradueix("descargando Excel...", "descarregant Excel...", "downloading Excel...", "baixando excel...")
    </a>
</div>

<div class="grid-container">
    @Html.Partial("_Grid", Model)
</div>

@Section Styles
    <style>
        .fchFrom, .subscribe, .download, .downloading {
            text-align: right;
            margin-top: 20px;
        }

        .grid-container {
            display: grid;
            grid-template-columns: auto auto auto auto auto auto auto auto auto auto auto;
            font-size: 0.6em;
            margin-top: 20px;
            grid-gap: 0px;
            border-right: 1px solid lightgray;
            border-bottom: 1px solid lightgray;
        }

            .grid-container div, .grid-container a {
                border-top: 1px solid lightgray;
                border-left: 1px solid lightgray;
                padding: 5px;
            }
    </style>
End Section

@Section Scripts
    <script>
        $(document).on('click', '#reloadButton', function () {
            reload(0);
        });

        $(document).on('change', '.fchFrom', function () {
            $('#reloadButton').prop("disabled", false);
        });

        $(".download a").click(function (e) {
            e.preventDefault();
            $('.download').hide();
            $('.downloading').show();
            $('.downloading').append(spinner20);

            var fchFrom = $(".fchFrom input[type = 'date']").val();
            var url = '/descatalogados/excel/' + fchFrom;

            fetch(url)
                .then(resp => resp.blob())
                .then(blob => {
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.style.display = 'none';
                    a.href = url;
                    // the filename you want
                    a.download = 'descatalogados.xlsx';
                    document.body.appendChild(a);
                    a.click();
                    window.URL.revokeObjectURL(url);
                })
                .then(() => {
                    $('.download').show();
                    $('.downloading').hide();
                   spinner20.remove();
                })
                .catch(() => {
                    alert('error al descargar el Excel');
                });


        });

        $(".subscribe").click(function (e) {
            var isChecked = ($(".subscribe input[type = 'checkbox']").is(':checked'))
            $.ajax({
                    url: 'Descatalogados/Subscribe/',
                    type: 'GET',
                    cache: false,
                    data: { value: isChecked },
                    error: function (jqXHR, status, err) {
                        alert(err);
                    }
            });
        });



        function reload() {
            $('.fchFrom').append(spinner20);
            $('#reloadButton').hide();

            var fchFrom = $(".fchFrom input[type = 'date']").val();
            $.ajax({
                url: '@Url.Action("Reload")',
                type: 'GET',
                cache: false,
                data: { fchFrom: fchFrom },
                success: function (result) {
                    spinner20.remove();
                    $('#reloadButton').show();
                    $('#reloadButton').prop("disabled", true);
                    $('.grid-container').html(result);
                },
                error: function (jqXHR, status, err) {
                    alert(err);
                }
            });
        };


    </script>
End Section
