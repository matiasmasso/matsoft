
@Code
    ViewBag.Title = "Sell-out"
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
    Dim lang As DTOLang = If(ViewBag.Lang Is Nothing, Mvc.ContextHelper.Lang, ViewBag.Lang)
End Code

<section class="PageTitle">
    @Mvc.ContextHelper.Tradueix("Pedidos de clientes", "Comandes de clients", "Sell-out data")
</section>

<div id="Loading">
    @Html.Raw(lang.Tradueix("Un momento por favor, estamos descargando los datos", "Un moment siusplau, estem descarregant les dades", "One moment please, we are busy downloading the data"))
    ...
</div>

<div id="Filters" hidden>
    <select id="Year">
        <option selected>@Date.Today.Year</option>
        @For year As Integer = Date.Today.Year - 1 To Date.Today.Year - 10 Step -1
            @<option>@year</option>
        Next
    </select>

    <a href="#" id="DownloadExcel" title='@lang.Tradueix("descargar todos los datos del año en Excel", "descarregar totes les dades de l'any en Excel", "download Excel full year raw data")'>
        <div class="DownloadExcel" width="20" height="20"></div>
    </a>

    <select id="Format">
        <option selected value="@CInt(DTOSellOut.Formats.amounts)">@lang.Tradueix("Importes", "Imports", "Amounts")</option>
        <option value="@CInt(DTOSellOut.Formats.units)">@lang.Tradueix("Unidades", "Unitats", "Units")</option>
    </select>

    <select id="Concept">
        <option selected value="@DTOSellOut.ConceptTypes.product">@lang.Tradueix("productos", "productes", "products")</option>
        <option value="@DTOSellOut.ConceptTypes.channels">@lang.Tradueix("canales", "canals", "channels")</option>
        <option value="@DTOSellOut.ConceptTypes.geo">@lang.Tradueix("areas", "areas", "areas")</option>
    </select>

    <select id="Country">
        <option value="">@lang.Tradueix("(todos los paises)", "(tots els paisos)", "(all countries)")</option>
    </select>

    <select id="Zona" hidden>
        <option value="">@lang.Tradueix("(todas las zonas)", "(totes les zones)", "(all country areas)")</option>
    </select>

    <select id="Location" hidden>
        <option value="">@lang.Tradueix("(todas las poblaciones)", "(totes les poblacions)", "(all area locations)")</option>
    </select>

    <select id="Customer" hidden>
        <option value="">@lang.Tradueix("(todos los clientes)", "(tots els clients)", "(all customers)")</option>
    </select>

</div>



<div id="Errors" hidden></div>

<div id="Grid"></div>

<input type="hidden" id="RawDataUrl" value="@MmoUrl.ApiUrl("sellout/data", Mvc.ContextHelper.GetUser().Guid.ToString())" />

@Section Scripts
    <script src="~/Scripts/jquery-3.6.0.min.js"></script>
    <script type="text/javascript" src="//cdn.jsdelivr.net/npm/xlsx/dist/xlsx.full.min.js"></script>
    <script src="~/Media/js/Site.js"></script>
    <script>
        var sellout = {};
        var sortColumn = 0;
        var spinner16 = $('<div />', { 'class': 'Spinner16' });


        let imageExpand = document.createElement('img');
        imageExpand.src = "/Media/Img/ico/expand9.png";
        let imageExpanded = document.createElement('img');
        imageExpanded.src = "/Media/Img/ico/expanded-12.png";

        $(document).ready(function () {
            $('#Zona').hide();
            $('#Location').hide();
            $('#Customer').hide();
            $('#Filters').hide();

            LoadData();
        });


        $(document).on('click', '#DownloadExcel', function (e) {
            $('#DownloadExcel div').toggleClass('DownloadExcel DownloadingExcel');
            AsyncDownloadExcel();
        });




        $(document).on('change', '#Year', function (e) {
            $('#Zona').hide();
            $('#Location').hide();
            $('#Customer').hide();
            let rows = $('#Grid .Row[data-epg]');
            rows.remove();

            LoadData();
        });

        $(document).on('change', '#Format', function (e) {
            LoadItems();
        });

        $(document).on('click', 'div.Header div a', function (e) {
            e.preventDefault();
            let headerCell = $(this).closest('div')[0];
            let $headerRow = $(headerCell).parent();
            let $headerCells = Array.from($headerRow.children());
            sortColumn = $headerCells.indexOf(headerCell);
            LoadItems();
        });

        $(document).on('click', 'div.Row a', function (e) {
            e.preventDefault();
            $(this).children('img').remove();
            let row = $(this).closest('div.Row')[0];
            if (row.hasAttribute('expanded')) {
                $(this).prepend(imageExpand);
                $(row).removeAttr('expanded');
                RemoveChildren(row);

            } else {
                $(this).prepend(imageExpanded);
                $(row).attr('expanded', '');
                AddChildren(row);
            }

        });



        $(document).on('change', '#Concept', function (e) {
            LoadItems();
        });

        $(document).on('change', '#Country', function (e) {
            LoadItems();
            LoadZonas();
            $('#Location').hide();
            $('#Customer').hide();
        });

        $(document).on('change', '#Zona', function (e) {
            LoadItems();
            LoadLocations();
            $('#Customer').hide();
        });

        $(document).on('change', '#Location', function (e) {
            LoadItems();
            LoadCustomers();
        });

        $(document).on('change', '#Customer', function (e) {
            LoadItems();
        });

        function LoadFilters() {
            LoadCountries();
        }


        function LoadItems() {
            ClearItems();
            let headerRow = LoadHeader();
            switch (CurrentConcept()) {
                case "@DTOSellOut.ConceptTypes.product":
                    AddBrands(headerRow);
                    break;
                case "@DTOSellOut.ConceptTypes.channels":
                    AddChannels(headerRow);
                    break;
                case "@DTOSellOut.ConceptTypes.geo":
                    AddCountries(headerRow);
                    break;
            }
        }

        function AddChildren(row) {
            if (row.hasAttribute("brand")) {
                AddCategories(row);
            } else if (row.hasAttribute("category")) {
                AddSkus(row);
            } else if (row.hasAttribute("country")) {
                AddZonas(row);
            } else if (row.hasAttribute("zona")) {
                AddLocations(row);
            } else if (row.hasAttribute("location")) {
                AddCustomers(row);
            }
        }

        function RemoveChildren(parentRow) {
            let epg = Epg(parentRow);
            let rows = $('#Grid .Row[data-epg^="' + epg + '."]');
            rows.remove();
        }

        function LoadCountries() {
            $.each(sellout.Countries, function (key, item) {
                $('#Country').append('<option value="' + item.Guid + '">' + item.Nom + '</option>');
            });
        }

        function LoadZonas() {
            $('#Zona').find('option').not(':first').remove();
            if (SelectedCountry()) {
                $.each(SelectedCountry().Zonas, function (key, item) {
                    $('#Zona').append('<option value="' + item.Guid + '">' + item.Nom + '</option>');
                });
                $('#Zona').show();
            } else {
                $('#Zona').hide();
            }
        }

        function LoadLocations() {
            $('#Location').find('option').not(':first').remove();
            if (SelectedZona()) {
                $.each(SelectedZona().Locations, function (key, item) {
                    $('#Location').append('<option value="' + item.Guid + '">' + item.Nom + '</option>');
                });
                $('#Location').show();
            } else {
                $('#Location').hide();
            }
        }

        function LoadCustomers() {
            $('#Customer').find('option').not(':first').remove();
            if (SelectedLocation()) {
                $.each(SelectedLocation().Customers, function (key, item) {
                    $('#Customer').append('<option value="' + item.Guid + '">' + item.Nom + '</option>');
                });
                $('#Customer').show();
            } else {
                $('#Customer').hide();
            }
        }

        function CurrentFormat() {
            return $('#Format').val();
        }

        function CurrentConcept() {
            return $('#Concept').val();
        }

        function SelectedYear() {
            return $('#Year').val();
        }

        function SelectedBrand() {
            var retval = null;
            let guid = $('#Brand').val();
            if (guid) {
                let brands = sellout.Brands.filter(function (brand) {
                    return brand.Guid === guid;
                });
                retval = countries.first();
            }
            return retval;
        }

        function SelectedCountry() {
            var retval = null;
            let guid = $('#Country').val();
            if (guid) {
                let countries = sellout.Countries.filter(function (country) {
                    return country.Guid === guid;
                });
                retval = countries[0];
            }
            return retval;
        }

        function SelectedZona() {
            var retval = null;
            let guid = $('#Zona').val();
            if (guid) {
                let zonas = SelectedCountry().Zonas.filter(function (zona) {
                    return zona.Guid === guid;
                });
                retval = zonas[0];
            }
            return retval;
        }

        function SelectedLocation() {
            var retval = null;
            let guid = $('#Location').val();
            if (guid) {
                let locations = SelectedZona().Locations.filter(function (location) {
                    return location.Guid === guid;
                });
                retval = locations[0];
            }
            return retval;
        }

        function SelectedCustomer() {
            var retval = null;
            let guid = $('#Customer').val();
            if (guid) {
                let customers = SelectedLocation().Customers.filter(function (customer) {
                    return customer.Guid === guid;
                });
                retval = customers[0];
            }
            return retval;
        }


        function EpgFactory(idx, parentEpg) {
            //format X.0.0
            var retval = (parentEpg ? parentEpg : "X") + '.' + idx;
            return retval;
        }

        function Epg(row) {
            return $(row).data('epg');
        }

        function Indent(epg) {
            var retval = "";
            let level = epg.split(".").length - 2;
            for (i = 0; i < level; i++) {
                retval += "&nbsp;&nbsp;&nbsp;&nbsp;";
            }
            return retval;
        }

        function AddChannels(parentRow) {
            var idx = 0;
            var parentEpg;
            let channels = FilteredChannels();
            var rows = [];
            $.each(channels, function (key, channel) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = ChannelItem(channel);
                let row = Row(item, 'channel', epg, false, false);
                rows.push(row);
            });

            AddRows(parentRow, rows);
            if (channels.length == 1) {
                let row = rows[0]; // is a jQuery object
                //AddCategories(row[0]);
            }
        }



        function AddCountries(parentRow) {
            var idx = 0;
            var parentEpg;
            let countries = FilteredCountries();
            var rows = [];
            $.each(countries, function (key, country) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = CountryItem(country);
                    let row = Row(item, 'country', epg);
                    rows.push(row);
            });

            AddRows(parentRow, rows);
            if (countries.length == 1) {
                let row = rows[0]; // is a jQuery object
                AddZonas(row[0]);
            }
        }


        function AddZonas(parentRow) {
            let parentGuid = parentRow.getAttribute("country");
            let parentEpg = Epg(parentRow);
            let country = sellout.Countries.find(country => country.Guid === parentGuid);
            var zonas = FilteredZonas(country);
            var idx = 0;
            var rows = [];
            $.each(zonas, function (key, zona) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = ZonaItem(zona);
                    let row = Row(item, 'zona', epg);
                    rows.push(row);
            });

            AddRows(parentRow, rows);
        }

        function AddLocations(parentRow) {
            let parentGuid = parentRow.getAttribute("zona");
            let parentEpg = Epg(parentRow);

            var zonas = sellout.Countries.flatMap((country) => country.Zonas);
            let zona = zonas.find(zona => zona.Guid === parentGuid);
            let locations = FilteredLocations(zona);
            var idx = 0;
            var rows = [];
            $.each(locations, function (key, location) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = LocationItem(location);
                    let row = Row(item, 'location', epg);
                    rows.push(row);
            });

            AddRows(parentRow, rows);
        }

        function AddCustomers(parentRow) {
            let parentGuid = parentRow.getAttribute("location");
            let parentEpg = Epg(parentRow);

            var locations = sellout.Countries.flatMap((country) => country.Zonas).flatMap((zona) => zona.Locations);
            let location = locations.find(location => location.Guid === parentGuid);
            let customers = FilteredCustomers(location);
            var idx = 0;
            var rows = [];
            $.each(customers, function (key, customer) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = CustomerItem(customer);
                    let row = Row(item, 'customer', epg, false, false);
                    rows.push(row);
            });

            AddRows(parentRow, rows);
        }

        function AddBrands(parentRow) {
            var idx = 0;
            var parentEpg;
            let brands = FilteredBrands();
            var rows = [];
            $.each(brands, function (key, brand) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = BrandItem(brand);
                let row = Row(item, 'brand', epg, (brands.length == 1));
                rows.push(row);
            });

            AddRows(parentRow, rows);
            if (brands.length == 1) {
                let row = rows[0]; // is a jQuery object
                AddCategories(row[0]);
            }
        }

        function AddCategories(parentRow) {
            let parentGuid = parentRow.getAttribute("brand");
            let parentEpg = Epg(parentRow);
            let brand = sellout.Brands.find(brand => brand.Guid === parentGuid);
            var categories = FilteredCategories(brand);
            var idx = 0;
            var rows = [];
            $.each(categories, function (key, category) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = CategoryItem(category);
                let row = Row(item, 'category', epg);
                rows.push(row);
            });

            AddRows(parentRow, rows);
        }


        function AddSkus(parentRow) {
            let parentGuid = parentRow.getAttribute("category");
            let parentEpg = Epg(parentRow);
            var categories = sellout.Brands.flatMap((brand) => brand.Categories);
            let category = categories.find(category => category.Guid === parentGuid);
            let skus = FilteredSkus(category);
            var idx = 0;
            var rows = [];
            $.each(skus, function (key, sku) {
                idx += 1;
                let epg = EpgFactory(idx, parentEpg);
                let item = SkuItem(sku);
                let row = Row(item, 'sku', epg, false, false);
                rows.push(row);
            });

            AddRows(parentRow, rows);
        }


        function AddRows(parentRow, rows) {
            rows.sort(SortByColumn);

            var lastRow = parentRow;
            $.each(rows, function (key, row) {
                    let $next = $(lastRow).next();
                    if ($next.length == 0) {
                        $(row).insertAfter($(lastRow))
                    } else {
                        $(row).insertBefore($next);
                    }
                    lastRow = row;
            });
        }


        function Row(item, mod, epg, expanded = false, hasChildren = true) {
           var retval = $('<div class="Row" data-epg="' + epg + '" ' + mod + ' = "' + item.Guid + '" ' + (expanded ? 'expanded':'') + ' ></div>');
            retval.append(CellNom(item.Nom, epg, expanded, hasChildren));
            retval.append(CellNum(item.Total.Amt, item.Total.Qty));
            for (i = 0; i < 12; i++) {
                retval.append(CellNum(item.Months[i].Amt, item.Months[i].Qty));
            }
            return retval;
        }

        function CellNom(nom, epg, expanded, hasChildren) {
            let retval = $('<div class="CellNom" data-sortvalue = "' + nom + '">' + Indent(epg) + '</div>');
            if (hasChildren) {
                let anchor = $('<a href="#">' + nom + '</a>');
                let iconUrl = expanded ? "/Media/Img/ico/expanded-12.png" : "/Media/Img/ico/expand9.png";
                let img = $('<img src="' + iconUrl + '" width="9" height="9"/>');
                anchor.prepend(img);
                retval.append(anchor);
            } else {
                retval.append(nom);
            }
            return retval;
        }

        function CellNum(amt, qty) {
            let isAmt = CurrentFormat() == "0";
            let rawValue = isAmt ? RoundToTwo(amt) : qty;
            let formattedValue = isAmt ? rawValue.toLocaleString(undefined, { minimumFractionDigits: 2 }) + ' €' : rawValue.toLocaleString();
            let retval=$('<div class="CellNum" data-sortvalue = "' + rawValue + '">' + formattedValue + '</div>');
            return retval;
        }

        function ChannelItem(channel) {
            var customers = sellout.Customers.filter(function (customer) { return channel.Guid === customer.Channel; })
            var items = FilteredItems().filter(function (item) { return customers.some(customer => customer.Guid === item.Customer); })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(channel.Guid, channel.Nom, months);
            return retval;
        }

        function CountryItem(country) {
            var customers = country.Zonas.flatMap((zona) => zona.Locations).flatMap((location) => location.Customers);
            var items = FilteredItems().filter(function (item) { return customers.some(customer => customer.Guid === item.Customer); })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(country.Guid, country.Nom, months);
            return retval;
        }

        function ZonaItem(zona) {
            var customers = zona.Locations.flatMap((location) => location.Customers);
            var items = FilteredItems().filter(function (item) { return customers.some(customer => customer.Guid === item.Customer); })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(zona.Guid, zona.Nom, months);
            return retval;
        }

        function LocationItem(location) {
            var customers = location.Customers;
            var items = FilteredItems().filter(function (item) { return customers.some(customer => customer.Guid === item.Customer); })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(location.Guid, location.Nom, months);
            return retval;
        }

        function CustomerItem(customer) {
            var items = FilteredItems().filter(function (item) { return customer.Guid === item.Customer; })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(customer.Guid, customer.Nom, months);
            return retval;
        }

        function BrandItem(brand) {
            var skus = brand.Categories.flatMap((category) => category.Skus);
            var items = FilteredItems().filter(function (item) { return skus.some(sku => sku.Guid === item.Sku); })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(brand.Guid, brand.Nom, months);
            return retval;
        }

        function CategoryItem(category) {
            var skus = category.Skus;
            var items = FilteredItems().filter(function (item) { return skus.some(x => x.Guid === item.Sku); })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(category.Guid, category.Nom, months);
            return retval;
        }

        function SkuItem(sku) {
            var items = FilteredItems().filter(function (item) { return sku.Guid === item.Sku; })
            var months = items.flatMap((item) => item.Months);
            var retval = SummaryItem(sku.Guid, sku.Nom, months);
            return retval;
        }


        function SummaryItem(guid, nom, months) {
            var retval = {
                "Guid": guid, "Nom": nom, "Months": [], "Total": {}
            };

            for (i = 1; i <= 12; i++) {
                var filteredMonths = months.filter(function (x) { return x.Id == i; });
                var month = {
                    Id: i,
                    Qty: SummaryQty(filteredMonths),
                    Amt: SummaryAmt(filteredMonths)
                };
                retval.Months.push(month);
            }

            retval.Total = {
                Qty: SummaryQty(retval.Months),
                Amt: SummaryAmt(retval.Months)
            };

            return retval;
        }

        function SummaryQty(months) {
            return months.map(x => x.Qty).reduce(SumQtyReducer, parseInt(0))
        }

        function SummaryAmt(months) {
            return months.map(x => x.Amt).reduce(SumAmtReducer, parseFloat(0))
        }


        function ClearItems() {
            $("#Grid div").remove();
        }

        function LoadHeader() {
            var $grid = $('#Grid');
            var $row = $('<div class="Header"/>').appendTo($grid);
            $row.append('<div><a href="#">' + '@lang.Tradueix("Concepto", "Concepte", "Concept")' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.Tradueix("Totales", "Totals", "Totals")' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(1)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(2)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(3)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(4)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(5)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(6)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(7)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(8)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(9)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(10)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(11)' + '</a></div>');
            $row.append('<div class="CellNum"><a href="#">' + '@lang.MesAbr(12)' + '</a></div>');

            let $headerCells = $row.children();
            let sortCell = $headerCells[sortColumn];
            $headerCells.removeClass('SortColumn');
            $(sortCell).addClass('SortColumn');
            return $row;
        }

        function SumQtyReducer(sum, val) {
            if (val)
                return parseInt(sum) + parseInt(val);
            else
                return parseInt(sum);
        }

        function SumAmtReducer(sum, val) {
            if (val) {
                let retval = parseFloat(sum) + parseFloat(val);
                return retval;
            }
            else
                return parseFloat(sum);
        }

        function FilteredItems(filters) {
            var retval = sellout.Items;
            let country = SelectedCountry();
            let zona = SelectedZona();
            let location=SelectedLocation();
            let customer = SelectedCustomer();

            if (customer) {
                retval = retval.filter(function (item) {
                    return customer.Guid === item.Customer;
                });

            } else if (location) {
                let customers = location.Customers;

                retval = retval.filter(function (item) {
                    return customers.some(customer => customer.Guid === item.Customer);
                });

            } else if (zona) {
                let locations = zona.Locations;
                let customers = locations.flatMap((location) => location.Customers);

                retval = retval.filter(function (item) {
                    return customers.some(customer => customer.Guid === item.Customer);
                });
            } else if (country) {
                let zonas = country.Zonas;
                let locations = zonas.flatMap((zona) => zona.Locations);
                let customers = locations.flatMap((location) => location.Customers);

                retval = retval.filter(function (item) {
                    return customers.some(customer => customer.Guid === item.Customer);
                });
            }

            if (CurrentFormat() == "0") {
                retval = retval.filter(function (item) {
                    return item.Months.some(month => month.Amt != 0);
                });
            } else {
                retval = retval.filter(function (item) {
                    return item.Months.some(month => month.Qty != 0);
                });
            }

            return retval;
        }

        function FilteredChannels() {
            var items = FilteredItems();
            let customers = sellout.Customers.filter(function (customer) { return items.some(item => item.Customer === customer.Guid); })
            let channelGuids = customers.flatMap((customer) => customer.Channel);
            let channels = sellout.Channels.filter(function (channel) { return channelGuids.some(channelGuid => channel.Guid === channelGuid); })
            let retval = [...new Set(channels)];
            return retval;
        }

        function FilteredCountries() {
            var items = FilteredItems();
            let customerGuids = items.flatMap((item) => item.Customer);
            let customers = customerGuids.flatMap((customerGuid) => sellout.Customers.filter(function (customer) { return customer.Guid === customerGuid; }))
            let countries = customers.flatMap((customer) => customer.Location.Zona.Country);
            let retval = [...new Set(countries)];
            return retval;
        }

        function FilteredZonas(country) {
            var items = FilteredItems();
            let customerGuids = items.flatMap((item) => item.Customer);
            let customers = customerGuids.flatMap((customerGuid) => sellout.Customers.filter(function (customer) { return customer.Guid === customerGuid && customer.Location.Zona.Country.Guid === country.Guid; }))
            let zonas = customers.flatMap((customer) => customer.Location.Zona);
            let retval = [...new Set(zonas)];
            return retval;
        }

        function FilteredLocations(zona) {
            var items = FilteredItems();
            let customerGuids = items.flatMap((item) => item.Customer);
            let customers = customerGuids.flatMap((customerGuid) => sellout.Customers.filter(function (customer) { return customer.Guid === customerGuid && customer.Location.Zona.Guid === zona.Guid; }))
            let locations = customers.flatMap((customer) => customer.Location);
            let retval = [...new Set(locations)];
            return retval;
        }

        function FilteredCustomers(location) {
            var items = FilteredItems();
            let customerGuids = items.flatMap((item) => item.Customer);
            let customers = customerGuids.flatMap((customerGuid) => sellout.Customers.filter(function (customer) { return customer.Guid === customerGuid && customer.Location.Guid === location.Guid; }))
            let retval = [...new Set(customers)];
            return retval;
        }

        function FilteredBrands() {
            var items = FilteredItems();
            let skuGuids = items.flatMap((item) => item.Sku);
            let skus = skuGuids.flatMap((skuGuid) => sellout.Skus.filter(function (sku) { return sku.Guid === skuGuid; }))
            let brands = skus.flatMap((sku) => sku.Category.Brand);
            let retval = [...new Set(brands)];
            return retval;
        }

        function FilteredCategories(brand) {
            var items = FilteredItems();
            let skuGuids = items.flatMap((item) => item.Sku);
            let skus = skuGuids.flatMap((skuGuid) => sellout.Skus.filter(function (sku) { return sku.Guid === skuGuid && sku.Category.Brand.Guid === brand.Guid; }))
            let categories = skus.flatMap((sku) => sku.Category);
            let retval = [...new Set(categories)];
            return retval;
        }


        function FilteredSkus(category) {
            var items = FilteredItems();
            let skuGuids = items.flatMap((item) => item.Sku);
            let skus = skuGuids.flatMap((skuGuid) => sellout.Skus.filter(function (sku) { return sku.Guid === skuGuid && sku.Category.Guid === category.Guid; }))
            let retval = [...new Set(skus)];
            return retval;
        }

        function SortByColumn(aRow, bRow) {
            let aCell = $(aRow).children()[sortColumn];
            let bCell = $(bRow).children()[sortColumn];
            let a = $(aCell).data('sortvalue');
            let b = $(bCell).data('sortvalue');
            if (sortColumn == 0) {
                return ((a < b) ? -1 : ((a > b) ? 1 : 0));
            } else {
                return ((a < b) ? 1 : ((a > b) ? -1 : 0));
            }
        }

        function AllSkus() {
            var retval = [];
            $.each(sellout.Brands, function (key, brand) {
                $.each(brand.Categories, function (key, category) {
                    category.Brand = brand;
                    $.each(category.Skus, function (key, sku) {
                        sku.Category = category;
                        retval.push(sku);
                    });
                });
            });
            return retval;
        }

        function AllCustomers() {
            var retval = [];
            $.each(sellout.Countries, function (key, country) {
                $.each(country.Zonas, function (key, zona) {
                    zona.Country = country;
                    $.each(zona.Locations, function (key, location) {
                        location.Zona = zona;
                        $.each(location.Customers, function (key, customer) {
                            customer.Location = location;
                            customer.DistributionChannel = sellout.Channels.filter(function (channel) { return channel.Guid === customer.Channel; })[0];
                            retval.push(customer);
                        });
                    });
                });
            });
            return retval;
        }

        async function AsyncDownloadExcel() {
            const result = await ResolveAfter1Seconds();
            DownloadExcel()
            $('#DownloadExcel div').toggleClass('DownloadExcel DownloadingExcel');
        }

        function ResolveAfter1Seconds() {
            return new Promise(resolve => {
                setTimeout(() => { resolve('resolved asyncCall'); resolve('resolved asyncCall'); }, 1000);
            });
        }

        function DownloadExcel() {

            var filename = 'M+O SellOut ' + SelectedYear() + ' raw data ' + NowToString() + '.xlsx';
            var sheetname = "Sellout " + SelectedYear();
            var rows = [];
            rows.push(["Year", "Month", "Brand", "Category", "Sku", "Ean", "Ref", "Channel", "Country", "Area", "Location", "Customer", "Units", "Amount"]);

            $.each(sellout.Items, function (key, item) {
                let sku = sellout.Skus.filter(function (x) { return x.Guid === item.Sku; })[0];
                let customer = sellout.Customers.filter(function (x) { return x.Guid === item.Customer; })[0];
                $.each(item.Months, function (key, month) {
                    let row = [];
                    row.push(SelectedYear());
                    row.push(month.Id);
                    row.push(sku.Category.Brand.Nom);
                    row.push(sku.Category.Nom);
                    row.push(sku.Nom);
                    row.push(sku.Ean);
                    row.push(sku.Ref);
                    row.push(customer.DistributionChannel.Nom);
                    row.push(customer.Location.Zona.Country.Nom);
                    row.push(customer.Location.Zona.Nom);
                    row.push(customer.Location.Nom);
                    row.push(customer.Nom);
                    row.push(month.Qty);
                    row.push(month.Amt);
                    rows.push(row);
                });
            });

            var workbook = XLSX.utils.book_new()
            var worksheet = XLSX.utils.aoa_to_sheet(rows);
            XLSX.utils.book_append_sheet(workbook, worksheet, sheetname);
            XLSX.writeFile(workbook, filename);

        }


        function NowToString() {
            var m = new Date();
            var retval =
                m.getUTCFullYear() + "-" +
                ("0" + (m.getUTCMonth() + 1)).slice(-2) + "-" +
                ("0" + m.getUTCDate()).slice(-2) + " " +
                ("0" + m.getUTCHours()).slice(-2) + " " +
                ("0" + m.getUTCMinutes()).slice(-2) + " " +
                ("0" + m.getUTCSeconds()).slice(-2);
            return retval;
        }


        function RoundToTwo(num) {
            let retval = +(Math.round(num + "e+2") + "e-2");
            return retval;
        }

        function ShowError(msg) {
            let div = $('#Error');
            div.html(msg);
            div.show();
        }


        function LoadData() {
            $('#Loading').append(spinner16);
            $('#Loading').show();
            var year = $('#Year').val();
            var url = $('#RawDataUrl').val() + '/' + year;
            var jqxhr = $.getJSON(url, function (result) {
                sellout = result;
                sellout.Skus = AllSkus();
                sellout.Customers = AllCustomers();
                $(document).trigger('selloutLoaded', result);
            })
                .done(function () {
                    LoadFilters();
                    $('#Filters').show();

                    LoadItems();

                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    //$('.ProductSelection').show("fast");
                    $('#Errors').html('<div class="Error">' + textStatus + '<br/>' + errorThrown + '</div>');
                })
                .always(function () {
                    spinner.remove();
                    $('#Loading').hide();
                });
        }
    </script>
End Section

@section Styles
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <style scoped>
        .ContentColumn {
            display: block;
            white-space: nowrap;
            text-align: left;
        }

        .PageTitle {
            font-size: 1em;
            margin-top: 0 0 15px 0;
            padding-top: 0;
            color: darkgray;
        }

        #Loading {
            margin-top: 20px;
            vertical-align: middle;
        }

        #Errors {
            color: red;
            font-weight: 700;
        }

            #Errors:before {
                display: inline-block;
                content: '';
                background: url('https://www.matiasmasso.es/Media/Img/Ico/warn.gif') no-repeat;
                width: 16px;
                height: 16px;
                margin-right: 10px;
            }

        #Filters {
            margin-top: 20px;
            vertical-align: middle;
        }

            #Filters select {
                display: inline-block;
            }

        .Row .CellNom {
            white-space: nowrap;
        }

        .Row .CellNom a {
            display: inline-block;
        }

        .CellNom a img {
            width: 9px;
            height: 9px;
            margin-right: 5px;
        }

        .Row .CellNum {
            white-space: nowrap;
        }



        .DownloadExcel {
            display: inline-block;
            background-image: url('/Media/Img/Ico/download-20.png');
            background-repeat: no-repeat;
            background-position: bottom;
            vertical-align: bottom;
            width: 18px;
            height: 18px;
        }

        .DownloadingExcel {
            display: inline-block;
            background-image: url('/Media/Img/preloaders/Spinner16.png');
            background-repeat: no-repeat;
            background-position: bottom;
            vertical-align: bottom;
            width: 18px;
            height: 18px;
        }


        #Grid {
            display: grid;
            grid-template-columns: 1fr auto auto auto auto auto auto auto auto auto auto auto auto auto;
            grid-gap: 10px;
            margin-top: 20px;
            font-size: 0.8em;
        }

        .Header {
            display: contents;
        }

        .Row {
            display: contents;
        }

        .CellNum {
            justify-self: end;
        }

        .SortColumn:after {
            display: inline-block;
            content: '';
            background: url('https://www.matiasmasso.es/Media/img/Ico/expand-arrow-12.png') no-repeat;
            width: 14px;
            height: 12px;
            margin-left: 10px;
        }
    </style>
End Section
