@ModelType List(Of DTORepLiq)
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = Mvc.ContextHelper.GetLang()
End Code


<div class="pagewrapper">

    <section class="title">
        <h1>@Html.Raw(ViewBag.Title)</h1>
    </section>

    <div class="Errors" hidden="hidden"></div>
    <div class="Loading" hidden="hidden"></div>

    <div class="Filters">
        <div class="Reps">
            <select>
                <option value="">@lang.Tradueix("(Seleccionar representante)", "(Sel·leccionar representant)", "(Select a rep)")</option>
            </select>
        </div>

        <div class="Years">
            <select></select>
        </div>
    </div>

    <div class="Grid Repliqs">
        <div class="Row Hdr">
            <div class="Fch">@lang.Tradueix("Fecha", "Data", "Date", "")</div>
            <div class="Bas">@lang.Tradueix("Base", "Base", "Tax base", "")</div>
            <div class="Iva">@lang.Tradueix("Iva", "Iva", "Vat", "")</div>
            <div class="Ipf">@lang.Tradueix("Irpf", "Irpf", "Irpf", "")</div>
            <div class="Tot">@lang.Tradueix("Total", "Total", "Total", "")</div>
        </div>
    </div>
</div>

<template id="RowTemplate">
    <a class="Row" href="#">
        <div class="Fch"></div>
        <div class="Bas"></div>
        <div class="Iva"></div>
        <div class="Ipf"></div>
        <div class="Tot"></div>
    </a>
</template>

<template id="RepliqDetailsTemplate" >
    <div class="RepliqDetails">
        <div class="Grid Repliq">
            <div class="Row Hdr">
                <div class="Caption">
                    @lang.tradueix("Liquidacion")
                    <span class="RepliqNum"></span>
                    @lang.tradueix("de fecha")
                    <span class="RepliqFch"></span>
                </div>
                <div class="Download"><a href="#" target="_blank"><i class="fa fa-download" aria-hidden="true"></i></a></div>
            </div>
            <div class="Row Hdr">
                <div class="Fra">@lang.Tradueix("Factura", "Factura", "Invoice", "")</div>
                <div class="Fch">@lang.Tradueix("Fecha", "Data", "Date", "")</div>
                <div class="Nom">@lang.Tradueix("Cliente", "Client", "Customer", "")</div>
                <div class="Bas">@lang.Tradueix("Base", "Base", "Tax base", "")</div>
                <div class="Com">@lang.Tradueix("Comisión", "Comisió", "Commission", "")</div>
                <div class="Download"></div>
            </div>
        </div>
    </div>
</template>

<template id="InvoiceTemplate">
    <div class="Row">
        <div class="Fra"></div>
        <div class="Fch"></div>
        <div class="Nom Truncate"></div>
        <div class="Bas"></div>
        <div class="Com"></div>
        <div class="Download"><a href="#" target="_blank"><i class="fa fa-download" aria-hidden="true"></i></a></div>
    </div>
</template>

@Section Styles
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style scoped>
        .pagewrapper {
            max-width: 500px;
            margin: auto;
        }

        .Errors {
            margin:15px 0;
            color:red;
            font-weight:600;
        }

        .Filters {
            display: flex;
            flex-direction: row;
            justify-content: space-between;
        }

            .Filters select {
                font-size: 1.1em;
            }

        .Grid.Repliqs {
            display: grid;
            grid-template-columns: auto auto auto auto auto;
            grid-auto-flow: row;
            margin-top: 20px;
            max-width: 500px;
        }

        .Grid.Repliq {
            display: grid;
            grid-template-columns:  auto auto auto auto auto 24px;
            grid-auto-flow: row;
            margin-top: 20px;
            max-width: 500px;
        }

        .RepliqDetails {
            grid-column: 1 / 6;
            margin: 15px 0;
        }
            .RepliqDetails .Caption {
                grid-column: 1 / 6;
            }

        .Row {
            display: contents;
            padding-top: 5px;
        }

        .Row.Hdr div {
            background-color:aliceblue;
        }

            .Row div {
                border-top: 1px solid lightgray;
                border-left: 1px solid lightgray;
                border-bottom: 1px solid lightgray;
                padding: 4px 7px 2px 4px;
            }

                .Row div:last-child, .Row .Nom {
                    border-right: 1px solid lightgray;
                }

            .Row .Fch {
                text-align: center;
            }

            .Row .Bas, .Row .Iva, .Row .Ipf, .Row .Tot, .Row .Fra, .Row .Com {
                text-align: right;
            }

        Repliq {
            margin-top: 20px;
            border: 1px solid red;
        }

        @@media (max-width:700px) {
            .Row:not(.Hdr) div {
                min-height: 42px;
                padding-top: 20px;
            }
            .Grid.Repliq {
                grid-template-columns: auto auto auto auto auto 42px;
            }
            .Download {
                text-align:center;
            }
        }

    </style>
End Section

@Section Scripts
    <script src="~/Media/js/Helper.js"></script>
    <script>
        var model = [];
        var repliq = null;
        var selectedItem;
        var rowTemplate = $('#RowTemplate')[0];
        var repliqDetailsTemplate = $('#RepliqDetailsTemplate')[0];
        var invoiceTemplate = $('#InvoiceTemplate')[0];
    //apiRoot = 'http://localhost:55836/api/';

    $(document).ready(function () {
        $('.Loading').show();
        loadData();
    })

    $(document).on('change', '.Reps select', function (e) {
        loadYears();
        selectOption($('.Years select'), 1);
        loadGrid();
    });

        $(document).on('change', '.Years select', function (e) {
            loadGrid();
        });

        $(document).on('click', '.Grid.Repliqs > .Row', function (e) {
            e.preventDefault();
            selectedItem = $(this);
            if (selectedItem.hasClass('Expanded')) {
                selectedItem.removeClass('Expanded');
                selectedItem.next().remove();
            } else {
                selectedItem.addClass('Expanded');
                loadRepliq();
            }
        });


        function refrescaRepliqs() {
            $('.Loading').hide();
            loadReps();
            selectOption($('.Reps select'), 2);
            loadYears();
            selectOption($('.Years select'), 1);
            loadGrid();
        }

        function refrescaRepliq() {
            var repliqDetails = repliqDetailsTemplate.content.cloneNode(true);
            $(repliqDetails).find('.RepliqNum').html(repliq.Id);
            $(repliqDetails).find('.RepliqFch').html(formattedFchCompact(repliq.Fch));
            if (getYear(repliq.Fch) > 2007)
                $(repliqDetails).find('.Download a').attr('href', apiRoot + 'repliq/pdf/' + repliq.Guid);
            else
                $(repliqDetails).find('.Download i').remove();

            var items = repliq.Items;
            $.each(items, function (index, item) {
                var row = invoiceTemplate.content.cloneNode(true);
                $(row).find('.Fra').text(item.Fra.Num);
                $(row).find('.Fch').text(formattedFchCompact(item.Fra.Fch));
                $(row).find('.Nom').text(item.Fra.Customer.FullNom);
                $(row).find('.Bas').text(formattedEur(item.BaseFras.Eur));
                $(row).find('.Com').text(formattedEur(item.Comisio.Eur));
                $(row).find('.Download a').attr('href', apiRoot + 'repliq/pdf/' + repliq.Guid);

                $(row).find('.Row').attr('data-Guid', item.Fra.Guid);
                $(repliqDetails).find('.Grid').append(row);
            })
            $(repliqDetails).insertAfter(selectedItem);
        }

    function currentYear() {
        var retval = $('.Years select option:selected').text();
        return retval;
    }

    function currentRep() {
        var retval = null;
        const guid = $('.Reps select option:selected').val();
        if (guid != null)
            retval = model.Reps.find(x => x.Guid === guid);
        return retval;
    }


    function loadReps() {
        var select = $('.Reps select');
        var reps = sortedGuidNoms(model.Reps);
        $.each(reps, function (index, rep) {
            $(select).append($('<option>', {
                value: rep.Guid,
                text: rep.Nom
            }));
        })
    }

    function loadYears() {
        const rep = currentRep();
        const years = [...new Set(rep.Items.map(item => getYear(item.Fch)))];
        var select = $('.Years select');
        select.find('option').remove();
        $.each(years, function (index, year) {
            $(select).append($('<option>', {
                text: year
            }));
        })
    }

    function loadGrid() {
        const rep = currentRep();
        const year = currentYear();
        const items = rep.Items.filter(x => getYear(x.Fch) == year);
        $('.Grid.Repliqs .Row').not(':first').remove();
        $.each(items, function (index, item) {
            var row = rowTemplate.content.cloneNode(true);
            $(row).find('.Fch').text(formattedFchCompact(item.Fch));
            $(row).find('.Bas').text(formattedEur(item.Base));
            $(row).find('.Iva').text(item.Iva.toString() + '%');
            $(row).find('.Ipf').text(item.Irpf.toString() + '%');
            $(row).find('.Tot').text(formattedEur(total(item)));
            $(row).find('.Row').attr('data-Guid', item.Guid);
            $('.Grid.Repliqs').append(row);
        })
    }

        function total(item) {
            var retval = item.Base;
            if (item.Iva != 0)
                retval += item.Base * item.Iva / 100;
            if (item.Irpf != 0)
                retval -= item.Base * item.Irpf / 100;
        return retval;
        }


        function loadRepliq() {
            var guid = selectedItem.data('guid');
            var url = apiRoot + 'repliq/' + guid;
            var jqxhr = $.getJSON(url)
                .done(function (data) {
                    repliq = data;
                    refrescaRepliq();
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('.Errors').append('<div class="Error">' + textStatus + '<br/>' + errorThrown + '</div>');
                    $('.Errors').show();
                })
}

function loadData() {
  var url = apiRoot + 'repliqs/@Mvc.ContextHelper.GetUser().Guid.ToString()';
  var jqxhr = $.getJSON(url)
      .done(function(data) {
          model = data;
      })
      .fail(function(jqXHR, textStatus, errorThrown) {
          $('.Errors').append('<div class="Error">' + textStatus + '<br/>' + errorThrown + '</div>');
          $('.Errors').show();
      })
      .always(function() {
          refrescaRepliqs();
      });
}

    </script>
End Section




