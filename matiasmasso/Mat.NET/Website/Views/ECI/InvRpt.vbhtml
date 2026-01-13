@ModelType DTO.Models.InvrptModel
@Code
    Layout = "~/Views/shared/_Layout_SideNav.vbhtml"
    Dim lang = DTOLang.ENG
End Code

<h1 class="Title">Stocks El Corte Ingles</h1>


<div class="Filters">
    <select class="Mode">
        <option value="Prod">Products</option>
        <option value="Cust">Centers</option>
    </select>
    <a href="#" id="DownloadExcel" title=@lang.tradueix("Descargar los datos en Excel", "Descarregar les dades en Excel", "Download Excel file")>
        <i class="fa fa-download fa-lg"></i>
    </a>
</div>

<div class="Grid">
    <div class="Row Hdr">
        <div class="Nom">@lang.tradueix("Concepto", "Concepte", "Concept")</div>
        <div class="Qty">@lang.tradueix("Unidades", "Unitats", "Units")</div>
        <div class="Eur">@lang.tradueix("Importe", "Import", "Amount")</div>
    </div>
</div>

<template id="RowTemplate">
    <a class="Row Collapsed" href="#">
        <div class="Nom Truncate"><span></span></div>
        <div class="Qty"></div>
        <div class="Eur"></div>
    </a>
</template>

@Section Styles
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style scoped>
        .ContentColumn {
            max-width: 600px;
        }

        .Filters {
            display: flex;
            justify-content: space-between;
        }

            .Filters select {
                font-size: 1.0em;
                padding: 4px 7px 2px 4px;
            }

        .Grid {
            display: grid;
            grid-template-columns: 1fr 70px 120px;
            grid-auto-flow: row;
            margin-top: 15px;
        }

            .Grid .Row {
                display: contents;
            }

        .Row .Nom {
            text-align: left;
        }

        .Row .Qty {
            text-align: right;
        }

        .Row .Eur {
            text-align: right;
        }

        .Row.Expanded .Nom span:before {
            content: "";
            display: block;
            background: url("/Media/Img/Ico/collapse9.png") no-repeat;
            width: 9px;
            height: 9px;
            float: left;
            margin: 2px 2px 0 0;
        }

        .Row.Collapsed .Nom span:before {
            content: "";
            display: block;
            background: url("/Media/Img/Ico/expand9.png") no-repeat;
            width: 9px;
            height: 9px;
            float: left;
            margin: 2px 2px 0 0;
        }

        .Row.NoDrill .Nom span:before {
            content: "";
            display: block;
            width: 9px;
            height: 9px;
            float: left;
            margin: 2px 2px 0 0;
            pointer-events: none;
            color: black;
        }
    </style>
End Section
@Section Scripts
    <script type="text/javascript" src="//cdn.jsdelivr.net/npm/xlsx/dist/xlsx.full.min.js"></script>
    <script src="~/Media/js/Helper.js"></script>
    <script>
        var model = @Html.Raw(System.Web.Helpers.Json.Encode(Model));
                var rowTemplate = $('#RowTemplate')[0];
                var selectedItem;

                $(document).ready(function (e) {
                    loadTotals();
                    loadBrands();
                });

                $(document).on('click', '.Row:not(.Hdr)', function (e) {
                    e.preventDefault();
                    selectedItem = $(this);
                    if (selectedItem.hasClass('Expanded'))
                        collapse()
                    else if (selectedItem.hasClass('Collapsed'))
                        expand();
                });

        $(document).on('change', '.Mode', function (e) {
            $('.Grid .Row:not(.Hdr)').remove();
            loadTotals();
            const mod = $(this).val();
            if (mod == 'Prod')
                loadBrands();
            else if (mod == 'Cust')
                loadCustomers();
        });


        $(document).on('click', '#DownloadExcel', function (e) {
            e.preventDefault();
            $('#DownloadExcel i').removeClass('fa-download');
            $('#DownloadExcel i').addClass('fa-spinner fa-spin');
            AsyncDownloadExcel();
        });

        function currentMode() {
            return $('.Mode').val();
        }



                function collapse() {
                    selectedItem.removeClass('Expanded');
                    selectedItem.addClass('Collapsed');
                    const idx = selectedItem.index();
                    const selectedLevel = parseInt(selectedItem.data('level'));
                    while (true) {
                        const rowCount = $('.Grid .Row').length;
                        if (rowCount > (idx + 1)) {
                            const row = selectedItem.next();
                            const level = parseInt($(row).data('level'));
                            if (level > selectedLevel) {
                                $(row).remove();
                            } else
                                break;
                        } else
                            break;
                    }
                }

                function expand() {
                    selectedItem.removeClass('Collapsed');
                    selectedItem.addClass('Expanded');
                    const mod = selectedItem.data('mod');
                    if (mod == 'Tot') {
                        loadBrands();
                    } else if (mod == 'Brand') {
                        loadCategories();
                    } else if (mod == 'Category') {
                        loadSkus();
                    } else if (mod == 'Sku') {
                        loadCustomers();
                    } else if (mod == 'Customer') {
                        loadBrands();
                    }
                }


        function loadCustomers() {
            const level = parseInt(selectedItem.data('level')) + 1;
            $.each(customers(), function (index, customer) {
                var row = rowTemplate.content.cloneNode(true);
                $(row).find('.Row').attr('data-mod', 'Customer');
                $(row).find('.Row').attr('data-id', customer.Guid);
                $(row).find('.Row').attr('data-level', level);
                $(row).find('.Nom span').text(customer.Nom);
                $(row).find('.Nom').css("padding-left", (level * 10).toString() + '.px');
                $(row).find('.Qty').text(formattedInt(qty('Customer', customer.Guid)));
                $(row).find('.Eur').text(formattedEur(volume('Customer', customer.Guid)));
                $(row).insertAfter(selectedItem);
                if (currentMode() == 'Prod') {
                    const appendedRow = $('.Grid .Row[data-id="' + customer.Guid + '"]');
                    $(appendedRow).removeAttr('href');
                    $(appendedRow).toggleClass('Collapsed NoDrill');
                }

            })
        }

                function loadSkus() {
                    const category = currentCategory();
                    const level = parseInt(selectedItem.data('level')) + 1;
                    const skus = sortedGuidNoms(category.Skus).reverse();
                    $.each(skus, function (index, sku) {
                        var row = rowTemplate.content.cloneNode(true);
                        $(row).find('.Row').attr('data-mod', 'Sku');
                        $(row).find('.Row').attr('data-id', sku.Guid);
                        $(row).find('.Row').attr('data-level', level);
                        $(row).find('.Nom span').text(sku.Nom);
                        $(row).find('.Nom').css("padding-left", (level * 10).toString() + '.px');
                        $(row).find('.Qty').text(formattedInt(qty('Sku', sku.Guid)));
                        $(row).find('.Eur').text(formattedEur(volume('Sku', sku.Guid)));
                        $(row).insertAfter(selectedItem);
                        if (currentMode() == 'Cust') {
                            const appendedRow = $('.Grid .Row[data-id="' + sku.Guid + '"]');
                            $(appendedRow).removeAttr('href');
                            $(appendedRow).toggleClass('Collapsed NoDrill');
                        }
                    })
                }

                function loadCategories() {
                    const brand = currentBrand();
                    const level = parseInt(selectedItem.data('level')) + 1;
                    const categories = sortedGuidNoms(brand.Categories).reverse();
                    $.each(categories, function (index, category) {
                        var row = rowTemplate.content.cloneNode(true);
                        $(row).find('.Row').attr('data-mod', 'Category');
                        $(row).find('.Row').attr('data-id', category.Guid);
                        $(row).find('.Row').attr('data-level', level);
                        $(row).find('.Nom span').text(category.Nom);
                        $(row).find('.Nom').css("padding-left", (level * 10).toString() + '.px');
                        $(row).find('.Qty').text(formattedInt(qty('Category', category.Guid)));
                        $(row).find('.Eur').text(formattedEur(volume('Category', category.Guid)));
                        $(row).insertAfter(selectedItem);
                    })
                }

                function loadBrands() {
                    const level = parseInt(selectedItem.data('level')) + 1;
                    var filteredBrands = brands();
                    $.each(filteredBrands, function (index, brand) {
                        var row = rowTemplate.content.cloneNode(true);
                        $(row).find('.Row').attr('data-mod', 'Brand');
                        $(row).find('.Row').attr('data-id', brand.Guid);
                        $(row).find('.Row').attr('data-level', level);
                        $(row).find('.Nom span').text(brand.Nom);
                        $(row).find('.Eur').text(formattedEur(volume('Brand',brand.Guid)));
                        $(row).find('.Nom').css("padding-left", (level * 10).toString() + '.px');
                        $(row).insertAfter(selectedItem);
                    })
                    if (filteredBrands.length == 1) {
                        selectedItem = $('.Grid .Row[data-mod="Brand"][data-id="' + filteredBrands[0].Guid +'"]').first();
                        $(selectedItem).toggleClass('Collapsed Expanded');
                        loadCategories();
                    }
                }


                function loadTotals() {
                    var row = rowTemplate.content.cloneNode(true);
                    $(row).find('.Row').attr('data-mod', 'Tot');
                    $(row).find('.Row').attr('data-level', '0');
                    $(row).find('.Nom').text('total');
                    $(row).find('.Eur').text(formattedEur(volume('Tot')));
                    $('.Grid').append(row);
                    selectedItem = $('.Grid .Row').last();
                    $(selectedItem).toggleClass('Collapsed Expanded');
                }


                function currentCategory() {
                    const parent = selectedItem.parent();
                    const idx = selectedItem.index();
                    for (i = idx; i > 0; i--) {
                        const child = parent.children()[i];
                        const mod = $(child).data('mod');
                        if (mod == 'Category') {
                            const sibling = parent.children()[i];
                            const guid = $(sibling).data('id');
                            const retval = model.Catalog.Brands.flatMap(x => x.Categories).find(y => y.Guid === guid);
                            return retval;
                        }
                    }
                }


        function customers() {
            var items = model.Items;
            if (currentMode() == "Prod") {
                var currentSku = currentParent('Sku');
                items = model.Items.filter(x => x.ProductGuid === currentSku);
            }
            var retval = items.map(x => x.CustomerGuid);
            retval = [...new Set(retval)];
            retval = retval.map(x => model.Customers.find(y => y.Guid === x));
            retval = sortedGuidNoms(retval).reverse();
            return retval;
        }

        function brands() {
            var retval = [];
            if (currentMode() == 'Prod') {
                retval = model.Catalog.Brands;
            } else if (currentMode() == "Cust") {
                var customerGuid = currentParent('Customer');
                var items = model.Items.filter(x => x.CustomerGuid === customerGuid);
                $.each(model.Catalog.Brands, function (index, brand) {
                    var found = false;
                    $.each(brand.Categories, function (index, category) {
                        $.each(category.Skus, function (index, sku) {
                            if (items.some(x => x.ProductGuid === sku.Guid)) {
                                found = true;
                                return false;
                            }
                        });
                        if (found)
                            return false;
                    });
                    if (found)
                        retval.push(brand);
                });
            }
            return retval;
        }

        function currentParent(mod) {
            const idx = selectedItem.index();
            for (i = idx; i > 0; i--) {
                var row = $('.Grid .Row')[i];
                if ($(row).data('mod') == mod)
                    return $(row).data('id');
            }
        }

                function currentBrand() {
                    const parent = selectedItem.parent();
                    const idx = selectedItem.index();
                    for (i = idx; i > 0; i--) {
                        const child = parent.children()[i];
                        const mod = $(child).data('mod');
                        if (mod == 'Brand') {
                            const sibling = parent.children()[i];
                            const guid = $(sibling).data('id');
                            const retval = model.Catalog.Brands.find(x => x.Guid === guid);
                            return retval;
                        }
                    }
                }




                function children(mod, id) {
                    var retval = model.Items;
                    if (mod == 'Brand') {
                        var brand = model.Catalog.Brands.find(x => x.Guid === id);
                        var skus = brand.Categories.flatMap(y => y.Skus);
                        retval = retval.filter(x => skus.find(y => y.Guid === x.ProductGuid) != null);
                        if (currentMode() == 'Cust')
                            retval = retval.filter(x => x.CustomerGuid === currentParent('Customer'));

                    } else if (mod == 'Category') {
                        var category = model.Catalog.Brands.flatMap(x => x.Categories).find(y => y.Guid === id);
                        retval = retval.filter(x => category.Skus.find(y => y.Guid === x.ProductGuid) != null);
                    } else if (mod == 'Sku') {
                        var sku = model.Catalog.Brands.flatMap(x => x.Categories).flatMap(y => y.Skus).find(z => z.Guid === id);
                        retval = retval.filter(x => x.ProductGuid === id);
                    } else if (mod == 'Customer') {
                        if (currentMode() == "Prod") {
                            var skuGuid = currentParent('Sku');
                            retval = retval.filter(x => x.CustomerGuid === id && x.ProductGuid === skuGuid);
                        } else {
                            retval = retval.filter(x => x.CustomerGuid === id);
                        }
                    }

                    //if (currentMode() == 'Cust') {
                    //    retval = retval.filter(x => x.CustomerGuid === currentParent('Customer'));
                    //}
                    return retval;
                }

                function qty(mod, id) {
                    var retval;
                    retval = children(mod, id).reduce((a, b) => a + b.Qty, 0);
                    return retval;
                }
                function volume(mod, id) {
                    var retval;
                    retval = children(mod, id).reduce((a, b) => a + b.Qty * b.Retail, 0);
                    return retval;
        }

        async function AsyncDownloadExcel() {
            const result = await ResolveAfter1Seconds();
            DownloadExcel()
            $('#DownloadExcel i').removeClass('fa-spinner fa-spin');
            $('#DownloadExcel i').addClass('fa-download');
        }

        function ResolveAfter1Seconds() {
            return new Promise(resolve => {
                setTimeout(() => { resolve('resolved asyncCall'); resolve('resolved asyncCall'); }, 1000);
            });
        }

        function DownloadExcel() {

            var filename = 'M+O El Corte Ingles inventory report ' + NowToString() + '.xlsx';
            var sheetname = NowToString();
            var rows = [];
            rows.push(["Date", "Sale point", "Brand", "Category", "Sku", "Units", "RRPP"]);

            $.each(model.Items, function (key, item) {
                let brand = model.Catalog.Brands.filter(x => x.Categories.some(y => y.Skus.some(z => z.Guid === item.ProductGuid)))[0];
                let category = brand.Categories.filter(x => x.Skus.some(y => y.Guid === item.ProductGuid))[0];
                let sku = category.Skus.filter(x => x.Guid === item.ProductGuid)[0];
                let customer = model.Customers.find(x => x.Guid === item.CustomerGuid);
                var fch = fromMicrosoftDate(item.Fch);
                let row = [];
                row.push(formattedFchCompact(fch) + ' ' + formattedTime(fch));
                row.push(customer.Nom);
                row.push(brand.Nom);
                row.push(category.Nom);
                row.push(sku.Nom);
                row.push(item.Qty);
                row.push(item.Retail);
                rows.push(row);
            });

            var workbook = XLSX.utils.book_new()
            var worksheet = XLSX.utils.aoa_to_sheet(rows);
            XLSX.utils.book_append_sheet(workbook, worksheet, sheetname);
            XLSX.writeFile(workbook, filename);
        }

    </script>

End Section
