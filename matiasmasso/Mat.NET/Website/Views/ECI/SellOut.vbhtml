@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
End Code

<div id="ControlPanel">
    <div id="Filters">
        <select id="CurrentYear">
            <option value="@DTO.GlobalVariables.Today().Year" selected>@DTO.GlobalVariables.Today().Year</option>
            @For year As Integer = DTO.GlobalVariables.Today().Year - 1 To 2017 Step -1
                @<option value="@year">@year</option>
            Next
        </select>
        <select id="CurrentUnits">
            <option value="0" selected>@ContextHelper.Tradueix("Importes", "Imports", "Amounts")</option>
            <option value="1">@ContextHelper.Tradueix("Unidades", "Unitats", "Units")</option>
        </select>
        <select id="CurrentBrand" hidden="hidden">
            <option value="" selected>@ContextHelper.Tradueix("Todas las marcas", "Totes les marques", "All brands")</option>
        </select>
        <select id="CurrentCategory" hidden="hidden" ">
            <option value="" selected>@ContextHelper.Tradueix("Todas las categorías", "Totes les categories", "All categories")</option>
        </select>
        <select id="CurrentSku" hidden="hidden">
            <option value="" selected>@ContextHelper.Tradueix("Todos los productos", "Tots els productes", "All products")</option>
        </select>
    </div>
    <div>
        <a href='#' id="ExcelRequest" title='@(ContextHelper.Tradueix("Descargar fichero Excel", "Descarregar fitxer Excel", "Download Excel file"))'>
            <img src="/Media/Img/48x48/Excel48.png" width="21" height="21" />
        </a>
    </div>
</div>

<div class="Error"></div>

<div id="Grid-Container" hidden="hidden">
    <div class="Row Header">
        <div>@ContextHelper.Tradueix("Concepto", "Concepte", "Concept")</div>
        <div class="Total">@ContextHelper.Tradueix("Totales", "Totals", "Totals")</div>
        @For i = 1 To 12
            @<div class="Month">@ContextHelper.Lang.MesAbr(i)</div>
        Next
    </div>
</div>



@Section Scripts
    <script src="~/Scripts/jquery-3.6.0.min.js"></script>
    <script src="~/Media/js/Site.js"></script>
    <script>
        var sellout = {};

        $(document).ready(function () {
            FetchData(CurrentYear());
        })

        $(document).on('change', '#CurrentYear', function () {
            FetchData(CurrentYear());
            LoadGrid();
        })

        $(document).on('change', '#CurrentUnits', function () {
            LoadGrid();
        })

        $(document).on('change', '#CurrentBrand', function () {
            LoadCategories();
            LoadGrid();
        })

        $(document).on('change', '#CurrentCategory', function () {
            LoadSkus();
            LoadGrid();
        })

        $(document).on('change', '#CurrentSku', function () {
            LoadGrid();
        })

        $(document).on('click', '.Customer', function () {
            event.preventDefault();
            var row = $(this).closest('.Row')
            DrilldownCustomer(row);
        })

        $(document).on('click', '.Brand', function () {
            event.preventDefault();
            var row = $(this).closest('.Row')
            DrilldownBrand(row);
        })

        $(document).on('click', '.Category', function () {
            event.preventDefault();
            var row = $(this).closest('.Row')
            DrilldownCategory(row);
        })

        $(document).on('click', '.Sku', function () {
            event.preventDefault();
            var row = $(this).closest('.Row')
        })

        $(document).on('click', '#ExcelRequest', function () {
            event.preventDefault();
            DownloadExcel();
        })

        function CurrentYear() {
            return $('#CurrentYear').val();
        }

        function CurrentUnits() {
            return $('#CurrentUnits').val();
        }

        function CurrentBrand() {
            var retval = null;
            var guid = $('#CurrentBrand').val();
            if (guid) {
                retval = sellout.Catalog.filter(function (item) { return item.Guid === guid; })[0];
            }
            return retval;
        }

        function CurrentCategory() {
            var retval = null;
            var brand = CurrentBrand();
            var guid = $('#CurrentCategory').val();
            if (guid) {
                retval = brand.Categories.filter(function (item) { return item.Guid === guid; })[0];
            }
            return retval;
        }

        function CurrentSku() {
            var retval = null;
            var category = CurrentCategory();
            var guid = $('#CurrentSku').val();
            if (guid) {
                retval = category.Skus.filter(function (item) { return item.Guid === guid; })[0];
            }
            return retval;
        }

        function CurrentItems() {
            var src = sellout.Items;
            var skus = [];
            if (CurrentSku() != null) {
                skus = [CurrentSku()]
            } else if (CurrentCategory() != null) {
                skus = CurrentCategory().Skus;
            } else if (CurrentBrand() != null) {
                var categories = CurrentBrand().Categories;
                $.each(categories, function (i, category) {
                    skus = skus.concat(category.Skus)
                });
            } else {
                return src
            }

            skuGuids = $.map(skus, function (n, i) {
                return (n.Guid);
            });

            var retval = $.grep(src, function (item, index) {
                return skuGuids.includes(item.Sku);
            });
            return retval;
        }

        function CurrentCustomers() {
            var src = CurrentItems();
            var uniqueCustomerGuids = [];
            var customerGuids = src.reduce((r, item) => r.concat(item.Customer), []);
            $.each(customerGuids, function (i, customerGuid) {
                if ($.inArray(customerGuid, uniqueCustomerGuids) === -1) uniqueCustomerGuids.push(customerGuid);
            });
            var retval = [];
            $.each(uniqueCustomerGuids, function (i, customerGuid) {
                var customer = sellout.Customers.filter(function (customer) { return customer.Guid == customerGuid })[0];
                retval.push(customer);
            });

            retval.sort((a, b) => (a.Nom > b.Nom) ? 1 : -1)
            return retval;
        }

        function CustomerItems(customer) {
            var currentItems = CurrentItems();

            var retval = $.grep(currentItems, function (item, index) {
                return item.Customer == customer.Guid;
            });

            //var retval = currentItems.filter(function (item) { return item.Customer == customer.Guid; });
            return retval;
        }

        function CustomerBrands(customer) {
            var items = CustomerItems(customer);
            var retval = ItemBrands(items);
            retval.sort((a, b) => (a.Nom > b.Nom) ? 1 : -1)
            return retval;
        }

        function CustomerCategories(customer, brand) {
            var items = CustomerItems(customer);
            var retval = ItemCategories(items, brand);
            retval.sort((a, b) => (a.Nom > b.Nom) ? 1 : -1)
            return retval;
        }

        function CustomerSkus(customer, category) {
            var items = CustomerItems(customer);
            var retval = ItemSkus(items, category);
            retval.sort((a, b) => (a.Nom > b.Nom) ? 1 : -1)
            return retval;
        }

        function LoadBrands() {
            var select = $('#CurrentBrand');
            select.find('option').not(':first').remove();
            for (i = 0; i < sellout.Catalog.length; i++) {
                $(select).append("<option value='" + sellout.Catalog[i].Guid + "'>" + sellout.Catalog[i].Nom + "</option>");
            }

            if (sellout.Catalog.length == 1) {
                select.find('option:eq(1)').prop('selected', true);
                LoadCategories();
            } else {
                select.show();
            }
        }

        function LoadCategories() {
            var select = $('#CurrentCategory');
            select.find('option').not(':first').remove();
            var brand = CurrentBrand();

            if (brand == null) {
                select.hide();
            } else {
                for (i = 0; i < brand.Categories.length; i++) {
                    $(select).append("<option value='" + brand.Categories[i].Guid + "'>" + brand.Categories[i].Nom + "</option>");
                }

                select.show();

                if (brand.Categories.length == 1) {
                    select.find('option:eq(1)').prop('selected', true);
                    LoadSkus();
                }
            }
        }

        function LoadSkus() {
            var select = $('#CurrentSku');
            select.find('option').not(':first').remove();
            var category = CurrentCategory();

            if (category == null) {
                select.hide();
            } else {
                for (i = 0; i < category.Skus.length; i++) {
                    $(select).append("<option value='" + category.Skus[i].Guid + "'>" + category.Skus[i].NomCurt + "</option>");
                }

                select.show();

                if (category.Skus.length == 1) {
                    select.find('option:eq(1)').prop('selected', true);
                }
            }
        }


        function LoadGrid() {
            var customers = CurrentCustomers();
            var grid = $('#Grid-Container');
            $('#Grid-Container > div').not(':first').remove();
            $(grid).append(SummaryRow);
            for (var i = 0; i < customers.length; i++) {
                var customer = customers[i];
                $(grid).append(CustomerRow(customer));
            }
        }

        function SummaryRow() {
            var items = CurrentItems();
            var retval = $('<div class="Row"></div>');
            AddConcept(retval, 'Total')
            AddMonthValues(retval, items)
            return retval;
        }

        function CustomerRow(customer) {
            var items = CurrentItems().filter(function (item) { return item.Customer === customer.Guid; });
            var retval = $('<div class="Row" IsExpanded="false" data-tag="' + customer.Guid + '"></div>');
            AddCustomer(retval, customer)
            AddMonthValues(retval, items)
            return retval;
        }

        function BrandRow(customer, brand) {
            var items = CurrentItems().filter(function (item) {
                var itemBrand = ItemBrand(item);
                return item.Customer == customer.Guid && itemBrand.Guid == brand.Guid;
            });
            var retval = $('<div class="Row" IsExpanded="false" data-customer="' + customer.Guid + '" data-brand="' + brand.Guid + '"></div>');
            AddBrand(retval, brand)
            AddMonthValues(retval, items)
            return retval;
        }

        function CategoryRow(customer, brand, category) {
            var items = CurrentItems().filter(function (item) {
                var itemCategory = ItemCategory(item);
                return item.Customer == customer.Guid && itemCategory.Guid == category.Guid;
            });
            var retval = $('<div class="Row" IsExpanded="false" data-customer="' + customer.Guid + '" data-brand="' + brand.Guid + '" data-category="' + category.Guid + '"></div>');
            AddCategory(retval, category)
            AddMonthValues(retval, items)
            return retval;
        }

        function SkuRow(customer, brand, category, sku) {
            var items = CurrentItems().filter(function (item) {
                return item.Customer === customer.Guid && item.Sku === sku.Guid;
            });
            var retval = $('<div class="Row" IsExpanded="false" data-customer="' + customer.Guid + '" data-brand="' + brand.Guid + '" data-category="' + category.Guid + '" data-sku="' + sku.Guid + '"></div>');
            AddSku(retval, sku)
            AddMonthValues(retval, items)
            return retval;
        }

        function AddConcept(row, concept) {
            $(row).append('<div class="Concept">' + concept + '</div>');
        }

        function AddCustomer(row, customer) {
            $(row).append('<div class="Concept"><a href="#" class="Customer" data-tag="' + customer.Guid + '">' + customer.Nom + '</a></div>');
        }

        function AddBrand(row, brand) {
            $(row).append('<div class="Concept"><a href="#" class="Brand" data-tag="' + brand.Guid + '">' + ('&nbsp;').repeat(4) + brand.Nom + '</a></div>');
        }

        function AddCategory(row, category) {
            $(row).append('<div class="Concept"><a href="#" class="Category" data-tag="' + category.Guid + '">' + ('&nbsp;').repeat(8) + category.Nom + '</a></div>');
        }

        function AddSku(row, sku) {
            $(row).append('<div class="Concept"><a href="#" class="Sku" data-tag="' + sku.Guid + '">' + ('&nbsp;').repeat(12) + sku.Ref + ' ' + sku.NomCurt + '</a></div>');
        }

        function AddMonthValues(row, items) {
            $(row).append('<div class="Total">' + Sum(items) + '</div>');
            for (month = 1; month < 13; month++) {
                var monthItems = items.filter(function (item) { return item.Month == month; });
                $(row).append('<div class="Month">' + Sum(monthItems) + '</div>');
            }
        }



        function Sum(items) {
            var retval = "&nbsp;";
            if (items.length > 0) {
                if (CurrentUnits() == 0) {
                    sum = FormatEur(items.reduce((accum, item) => accum + item.Eur, 0));
                } else {
                    sum = FormatInt(items.reduce((accum, item) => accum + item.Qty, 0));
                }
                retval = sum.toString();
            }
            return retval;
        }

        function GetCustomer(guid) {
            return sellout.Customers.filter(function (customer) { return customer.Guid === guid })[0];
        }

        function GetBrand(guid) {
            return sellout.Catalog.filter(function (brand) { return brand.Guid === guid })[0];
        }

        function GetCategory(guid) {
            var retval = null;
            $.each(sellout.Catalog, function (i, brand) {
                $.each(brand.Categories, function (i, category) {
                    if (category.Guid == guid) {
                        retval = category;
                        return false;
                    }
                });
                if (retval) return false;
            });
            return retval;
        }

        function GetSku(guid) {
            $.each(sellout.Catalog, function (i, brand) {
                $.each(brand.Categories, function (i, category) {
                    $.each(category.Skus, function (i, sku) {
                        if (sku.Guid == guid)
                            return sku;
                    });
                });
            });
        }

        function ItemBrands(items) {
            var retval = [];
            var found = false;
            $.each(sellout.Catalog, function (i, brand) {
                found = false;
                $.each(brand.Categories, function (i, category) {
                    $.each(category.Skus, function (i, sku) {
                        if (items.filter(function (item) { return item.Sku == sku.Guid }).length > 0) {
                            retval.push(brand);
                            found = true;
                            if (found) return false;
                        }
                    });
                    if (found) return false;
                });
            });
            return retval;
        }

        function ItemCategories(items, brand) {
            var retval = [];
            $.each(brand.Categories, function (i, category) {
                $.each(category.Skus, function (i, sku) {
                    if (items.filter(function (item) { return item.Sku == sku.Guid }).length > 0) {
                        retval.push(category);
                        return false;
                    }
                });
            });
            return retval;
        }

        function ItemSkus(items, category) {
            var retval = [];
            $.each(category.Skus, function (i, sku) {
                if (items.filter(function (item) { return item.Sku == sku.Guid }).length > 0) {
                    retval.push(sku);
                }
            });
            return retval;
        }


        function ItemBrand(item) {
            var retval = null;
            $.each(sellout.Catalog, function (i, brand) {
                $.each(brand.Categories, function (i, category) {
                    $.each(category.Skus, function (i, sku) {
                        if (item.Sku === sku.Guid) {
                            retval = brand;
                            return false;
                        }
                    });
                    if (retval)
                        return false;
                });
                if (retval)
                    return false;
            });
            return retval;
        }

        function ItemCategory(item) {
            var retval = null;
            $.each(sellout.Catalog, function (i, brand) {
                $.each(brand.Categories, function (i, category) {
                    $.each(category.Skus, function (i, sku) {
                        if (item.Sku === sku.Guid) {
                            retval = category;
                            return false;
                        }
                    });
                    if (retval)
                        return false;
                });
                if (retval)
                    return false;
            });
            return retval;
        }

        function DrilldownCustomer(row) {
            var grid = $('#Grid-Container');
            var customerGuid = $(row).data('tag');

            if ($(row).prop('IsExpanded')) {
                $(row).prop('IsExpanded', false)
                $('.Row[data-customer=' + customerGuid + ']').remove();
            } else {
                var customer = GetCustomer(customerGuid);
                $(row).prop('IsExpanded', true);

                if (CurrentSku()) {
                    //prevent default
                } else if (CurrentCategory()) {
                    //skip brand and category and display skus directly
                    var brand = CurrentBrand();
                    var category = CurrentCategory();
                    var skus = CustomerSkus(customer, category)
                    $.each(skus, function (i, sku) {
                        $(row).after(SkuRow(customer, brand, category, sku));
                    });
                } else if (CurrentBrand()) {
                    //skip brand and display categories directly
                    var brand = CurrentBrand();
                    var categories = CustomerCategories(customer, brand)
                    $.each(categories, function (i, category) {
                        $(row).after(CategoryRow(customer, brand, category));
                    });
                } else {
                    var brands = CustomerBrands(customer)
                    $.each(brands, function (i, brand) {
                        var brandRow = BrandRow(customer, brand);
                        $(row).after(brandRow);
                        if (brands.length == 1) {
                            $(brandRow).prop('IsExpanded', true);
                            var categories = CustomerCategories(customer, brand)
                            $.each(categories, function (i, category) {
                                $(brandRow).after(CategoryRow(customer, brand, category));
                            });
                        }
                    });
                }
            }
        }

        function DrilldownBrand(row) {
            var grid = $('#Grid-Container');
            var customerGuid = $(row).data('customer');
            var brandGuid = $(row).data('brand');
            if ($(row).prop('IsExpanded')) {
                $(row).prop('IsExpanded', false)
                $('.Row[data-customer=' + customerGuid + '][data-brand=' + brandGuid + '][data-category]').remove();
            } else {
                var customer = GetCustomer(customerGuid);
                var brand = GetBrand(brandGuid);
                $(row).prop('IsExpanded', true);
                var categories = CustomerCategories(customer, brand)
                $.each(categories, function (i, category) {
                    $(row).after(CategoryRow(customer, brand, category));
                });
            }
        }

        function DrilldownCategory(row) {
            var grid = $('#Grid-Container');
            var customerGuid = $(row).data('customer');
            var brandGuid = $(row).data('brand');
            var categoryGuid = $(row).data('category');
            if ($(row).prop('IsExpanded')) {
                $(row).prop('IsExpanded', false)
                $('.Row[data-customer=' + customerGuid + '][data-brand=' + brandGuid + '][data-category=' + categoryGuid + '][data-sku]').remove();
            } else {
                var customer = GetCustomer(customerGuid);
                var brand = GetBrand(brandGuid);
                var category = GetCategory(categoryGuid);
                $(row).prop('IsExpanded', true);
                var skus = CustomerSkus(customer, category)
                $.each(skus, function (i, sku) {
                    $(row).after(SkuRow(customer, brand, category, sku));
                });
            }
        }




        function FetchData(year) {
            var url = '/ECI/SellOutData/' + CurrentYear();
            var grid = $("#Grid-Container");
            $(grid).hide();
            $('#ExcelRequest').hide();
            $('.Error').html('');
            $('#Filters').append(spinner20);

            var jqxhr = $.get(url, function (data) {
                if (data.success) {
                    OnDataFetched(data.result);
                } else {
                    OnError(data.reason);
                }
            })
                .done(function () {
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    OnError(textStatus + '<br/>' + errorThrown);
                })
                .always(function () {
                    spinner20.remove();
                });
        }

        function OnDataFetched(result) {
            sellout = result;
            LoadBrands();
            LoadGrid();
            $("#Grid-Container").show();
            $('#ExcelRequest').show();
            $('.Error').html('');
        }

        function DownloadExcel() {
            $('#ControlPanel').append(spinner20);
            $('#ExcelRequest').hide();

            var currentBrand = '00000000-0000-0000-0000-000000000000';
            var currentCategory = '00000000-0000-0000-0000-000000000000';
            var currentSku = '00000000-0000-0000-0000-000000000000';
            if (CurrentBrand())
                currentBrand = CurrentBrand().Guid.toString();
            if (CurrentCategory())
                currentCategory = CurrentCategory().Guid.toString();
            if (CurrentSku())
                currentSku = CurrentSku().Guid.toString();

            var url = '/ECI/Excel/' + CurrentYear() + '/' + CurrentUnits() + '/' + currentBrand + '/' + currentCategory + '/' + currentSku;
            fetch(url)
                .then(resp => resp.blob())
                .then(blob => {
                    const url = window.URL.createObjectURL(blob);
                    const a = document.createElement('a');
                    a.style.display = 'none';
                    a.href = url;
                    // the filename you want
                    a.download = 'M+O - ECI SellOut.xlsx';
                    document.body.appendChild(a);
                    a.click();
                    window.URL.revokeObjectURL(url);
                })
                .then(() => {
                    $('#ExcelRequest').show();
                    spinner20.remove();
                    OnError('');
                })
                .catch(() => {
                    OnError('error al descargar el Excel');
                    $('#ExcelRequest').show();
                    spinner20.remove();
                });
        }


        function OnError(reason) {
            $('.Error').html(reason);
        }

        function FormatEur(num) {
            return (
                num
                    .toFixed(2) // always two decimal digits
                    .replace('.', ',') // replace decimal point character with ,
                    .replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.') + ' €' // use . as a separator
            )
        }

        function FormatInt(num) {
            return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.')
        }
    </script>
End section

@Section Styles
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <style scoped>
        #ControlPanel {
            display: flex;
            justify-content: space-between;
        }

        #Grid-Container {
            margin-top: 20px;
            display: grid;
            grid-template-columns: minmax(200px,1fr) repeat(13,1fr);
            font-size: 0.7em;
        }

            #Grid-Container .Row {
                display: contents;
            }

                #Grid-Container .Row > div {
                    padding: 1px 5px;
                }

                #Grid-Container .Row:hover > div {
                    border-bottom: 1px solid gray;
                    border-top: 1px solid gray;
                }

            #Grid-Container .Concept {
                text-overflow: ellipsis;
                white-space: nowrap;
                overflow: hidden;
            }

            #Grid-Container .Total {
                text-align: right;
                white-space: nowrap;
            }

            #Grid-Container .Month {
                text-align: right;
                white-space: nowrap;
            }

        .Error {
            color: red;
        }
    </style>
End Section
