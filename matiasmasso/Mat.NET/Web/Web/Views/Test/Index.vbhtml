@ModelType DTO.Models.CustomerBasket
@Code
    Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
End Code

<div class="PageWrapper">
    <div class="Title">Customer Basket</div>
    <div class="Grids">
        <div class="Catalog">
            <div class="Grid"></div>
        </div>
        <div class="Basket">
            <div class="Grid"></div>

            <div class="SendRow" hidden="hidden">
                <div>
                    <div class="Conditions">
                        <input type="checkbox" id="AcceptConditions" />
                        <label for="AcceptConditions">
                            Acepto las
                            <a href="https://www.matiasmasso.es/condiciones" target="_blank">
                                condiciones de venta
                            </a>
                        </label>
                    </div>
                    <a href="#" class="Send Button">
                        Enviar
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>

<template id="BrandTemplate">
    <a class="Row Collapsed" href="#">
        <div class="Caption">
            <i class="fa-solid fa-chevron-down ChevronDown"></i>
            <div class="Nom Truncate"></div>
        </div>
        <i class="fa-solid fa-chevron-up ChevronUp"></i>
    </a>
</template>

<template id="CategoryTemplate">
    <a class="Row Collapsed" href="#">
        <div class="Caption">
            <i class="fa-solid fa-chevron-down ChevronDown"></i>
            <div class="Nom Truncate"></div>
        </div>
        <i class="fa-solid fa-chevron-up ChevronUp"></i>
    </a>
</template>

<template id="SkuTemplate">
    <div class="Row">
        <div class="SkuPicture">
            <img width="150" height="171">
        </div>
        <div class="SkuFeature">
            <div>
                <div class="Nom"></div>
                <div class="Price">
                    <span>Preu:</span>
                    <span></span>
                </div>
                <div class="Units">
                    <span>Unitats:</span>
                    <input type="number" value="1" min="0" />
                </div>
            </div>
            <a class="AddToBasket Truncate Button" href="#">Añadir a la cesta</a>
        </div>
    </div>
</template>

<template id="ItemTemplate">
    <div class="Item" data-mod="Item">
        <div class="Dsc"></div>
        <div class="Ico">
            <a href="#" title="Eliminar linea">
                <i class="fa-solid fa-circle-xmark"></i>
            </a>
        </div>
        <div class="Qty">
            <input type="number" value="1" min="0" />
        </div>
        <div class="Eur"></div>
        <div class="Amt"></div>
    </div>
</template>

<template id="TotalTemplate">
    <div class="Total" data-mod="Total">
        <div class="Caption"></div>
        <div class="Amt"></div>
    </div>
</template>

@section Scripts
    <script>
        var model = @Html.Raw(System.Web.Helpers.Json.Encode(Model));
        var skuImgRootUrl = '@Html.Raw(DTO.MmoUrl.ApiUrl("productSku/thumbnail"))';
        var spinner20 = $('<div />', { 'class': 'Spinner20' });

        var selectedItem;
        var brandTemplate = $('#BrandTemplate')[0];
        var categoryTemplate = $('#CategoryTemplate')[0];
        var skuTemplate = $('#SkuTemplate')[0];
        var itemTemplate = $('#ItemTemplate')[0];
        var totalTemplate = $('#TotalTemplate')[0];

        $(document).ready(function () {
            LoadBrands();
            PreloadBasket();
        });

        $(document).on('click', 'a.Collapsed, a.Expanded', function (e) {
            e.preventDefault();
            selectedItem = $(this);
            CollapseOrExpand();
        });

        $(document).on('click', 'a.AddToBasket', function (e) {
            e.preventDefault();
            AddToBasket($(this));
            SetTotal();
        });

        $(document).on('click','.Basket .Item .Delete a', function(e) {
            e.preventDefault();
            $(this).closest('.Row').remove();
        });

        $(document).on('change', '.Item .Qty input', function (e) {
            e.preventDefault();
            var row = $(this).closest('.Item');
            var qty = parseInt($(row).find(".Qty input").val());
            var eur = parseFloat($(row).attr('data-price')).toFixed(2);
            $(row).find('.Amt').text((qty * eur).toLocaleString("es-ES", { minimumFractionDigits: 2 }) + ' €');
            SetTotal();
        })

        $(document).on('click', '.Ico a', function (e) {
            e.preventDefault();
            var item = $(this).closest('.Item');
            $(item).remove();
            SetTotal();
        })

        $(document).on('click', '.Send', function (e) {
            e.preventDefault();
            if ($('#AcceptConditions').is(":checked")) {
                Submit();
            } else {
                alert('por favor, lea y acepte las condiciones de venta');
            }
            ;
        })

        function PreloadBasket() {
            var grid = $('.Basket .Grid');
            var row = TotalRow();
            $(grid).append(row);
        }

        function AddToBasket(button) {
            var row = $(button).closest('.Row');
            var skuGuid = $(row).data('id');
            var qty = $(row).find('.Units input').val();
            var categories = model.Catalog.Brands.flatMap(x => x.Categories);
            var skus = categories.flatMap(y => y.Skus);
            var sku = skus.find(z => z.Guid === skuGuid);
            var grid = $('.Basket .Grid');

            var row = ItemRow(sku, qty);
            $(grid).append(row);
            SetTotal();
        }

        function SetTotal() {
            var grid = $('.Basket .Grid');
            var rows = $('[data-mod="Item"]');
            var rowTotal = $('[data-mod="Total"]');
            if (rows.length == 0) {
                $(rowTotal).find('.Caption').text('(la cesta está vacía)');
                $(rowTotal).find('.Amt').text('');
                $('.Basket .SendRow').hide();
            } else {
                var sum = 0;
                $.each(rows, function (index, row) {
                    var qty = $(row).find('.Qty input').val();
                    var eur = parseFloat($(row).data('price'));
                    var amt = qty * eur;
                    sum += amt;
                });
                $(rowTotal).find('.Caption').text('Total (IVA no incl.)');
                $(rowTotal).find('.Amt').text(sum.toLocaleString("es-ES", { minimumFractionDigits: 2 }) + ' €');
                $('.Basket .SendRow').show();
            }
        }

        function CollapseOrExpand() {
            $(selectedItem).toggleClass('Collapsed Expanded');
            IsCollapsed() ? Collapse() : Expand();
        }

        function IsCollapsed() {
            return $(selectedItem).hasClass('Collapsed');
        }

        function Expand() {
            IsBrand() ? LoadBrand() : LoadCategory();
        }

        function Collapse() {
            if (IsBrand())
                LoadBrands();
            else {
                var categoryGuid = $(selectedItem).data('id');
                var brand = model.Catalog.Brands.filter(x => x.Categories.some(y => y.Guid === categoryGuid))[0];
                LoadBrand(brand);
            }
        }

        function IsBrand() {
            return $(selectedItem).data('mod') == 'Brand';
        }

        function LoadBrands() {
            var grid = $('.Catalog .Grid');
            $(grid).find('.Row').remove();
            $.each(model.Catalog.Brands, function (index, brand) {
                var row = brandTemplate.content.cloneNode(true);
                $(row).find('.Row').attr('data-mod', 'Brand');
                $(row).find('.Row').attr('data-id', brand.Guid);
                $(row).find('.Row').attr('data-level', 1);
                $(row).find('.Nom').text(brand.Nom);
                $(grid).append(row);
            });
        }

        function LoadBrand(brand) {
            var grid = $('.Catalog .Grid');
            $(grid).find('.Row').remove();
            if (brand === undefined) {
                var brandGuid = $(selectedItem).data('id');
                var brand = model.Catalog.Brands.find(x => x.Guid === brandGuid);
            }

            var row = BrandRow(brand);
            $(grid).append(row);
            $(grid).find('.Row').last().toggleClass('Collapsed Expanded');

            var categories = brand.Categories.reverse();
            $.each(categories, function (index, category) {
                row = CategoryRow(category);
                $(grid).append(row);
            });
        }

        function LoadCategory() {
            var grid = $('.Catalog .Grid');
            $(grid).find('.Row').remove();
            var categoryGuid = $(selectedItem).data('id');
            var brand = model.Catalog.Brands.filter(x => x.Categories.some(y => y.Guid === categoryGuid))[0];
            var category = brand.Categories.find(x => x.Guid === categoryGuid);

            var row = BrandRow(brand);
            $(grid).append(row);
            $(grid).find('.Row').last().toggleClass('Collapsed Expanded');

            row = CategoryRow(category, true);
            $(grid).append(row);
            $(grid).find('.Row').last().toggleClass('Collapsed Expanded');

            $.each(category.Skus, function (index, sku) {
                row = SkuRow(sku);
                $(grid).append(row);
            });
        }

        function BrandRow(brand) {
            var retval = brandTemplate.content.cloneNode(true);
            $(retval).find('.Row').attr('data-mod', 'Brand');
            $(retval).find('.Row').attr('data-id', brand.Guid);
            $(retval).find('.Row').attr('data-level', 1);
            $(retval).find('.Nom').text(brand.Nom);
            return retval;
        }

        function CategoryRow(category) {
            var retval = categoryTemplate.content.cloneNode(true);
            $(retval).find('.Row').attr('data-mod', 'Category');
            $(retval).find('.Row').attr('data-id', category.Guid);
            $(retval).find('.Row').attr('data-level', 2);
            $(retval).find('.Nom').text(category.Nom);
            return retval;
        }

        function SkuRow(sku) {
            var retval = skuTemplate.content.cloneNode(true);
            $(retval).find('.Row').attr('data-mod', 'Sku');
            $(retval).find('.Row').attr('data-id', sku.Guid);
            $(retval).find('.Row').attr('data-level', 3);
            $(retval).find('.Nom').text(sku.Nom);
            var units = $(retval).find('.Units input');
            for (var i = sku.Moq; i < 51 * sku.Moq; i++) {
                var option = new Option(i.toString());
                units.append($(option));
            }
            $(retval).find('.Price span').last().text(sku.Price.toLocaleString("es-ES", { minimumFractionDigits: 2 }) + ' €');
            $(retval).find('img').attr('src', skuImgRootUrl + '/' + sku.Guid);
            return retval;
        }

        function ItemRow(sku, qty) {
            var retval = itemTemplate.content.cloneNode(true);
            $(retval).find('.Item').attr('data-sku', sku.Guid);
            $(retval).find('.Item').attr('data-price', (sku.Price.toString()));
            $(retval).find('.Dsc').text(Brand(sku).Nom + ' ' + Category(sku).Nom + ' ' + sku.Nom);
            $(retval).find('.Qty input').val(qty).change();
            $(retval).find('.Eur').text(sku.Price.toLocaleString("es-ES", { minimumFractionDigits: 2 }) + ' €');
            $(retval).find('.Amt').text((qty * sku.Price).toLocaleString("es-ES", { minimumFractionDigits: 2 }) + ' €');
            return retval;
        }

        function TotalRow() {
            var retval = totalTemplate.content.cloneNode(true);
            $(retval).find('.Row').attr('data-mod', 'Total');
            $(retval).find('.Caption').text('(la cesta está vacía)');
            return retval;
        }



        function Brand(sku) {
            return model.Catalog.Brands.filter(x => x.Categories.some(y => y.Skus.some(z => z.Guid === sku.Guid)))[0];
        }

        function Category(sku) {
            return model.Catalog.Brands.flatMap(x => x.Categories).filter(y => y.Skus.some(z => z.Guid === sku.Guid))[0];
        }

        function Submit() {
            $('.Basket .Send').html(spinner20);
        }


    </script>
End Section


@Section Styles
    <link href="~/Styles/Site.css" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/05a6a08892.js" crossorigin="anonymous"></script>
    <style scoped>

        .PageWrapper {
            display: flex;
            flex-direction: column;
            max-width: 900px;
            margin: auto;
            justify-content: flex-start;
        }

        .Title {
            margin: 0 auto;
            color: gray;
            font-size: large;
            font-weight: 600;
        }

        .Grids {
            display: flex;
            justify-content: space-between;
            max-width: 800px;
            margin: 20px auto;
            align-items: start;
        }

        .Catalog {
            margin: 10px 10px;
            width: 320px;
        }

        .Basket {
            margin: 10px 10px;
            width: 320px;
        }


        .Catalog .Grid {
        }

        .Catalog .Row {
            display: flex;
            flex-direction: row;
            justify-content: left;
            border-bottom: 0.5px solid gray;
            padding: 10px 0;
            max-width: 100%;
        }

        .Catalog a.Row {
            padding-left: 5px;
            justify-content: space-between;
        }

            .Catalog a.Row .Caption {
                display: flex;
            }

        .ChevronDown {
            margin-right: 10px;
        }

        .ChevronUp {
            margin-left: 10px;
            margin-right: 10px;
        }

        .Catalog a.Row.Expanded[data-mod="Brand"] {
            background-color: #EEEEEE;
        }

        .Catalog a.Row.Collapsed .ChevronDown {
            visibility: visible;
        }

        .Catalog a.Row.Collapsed .ChevronUp {
            visibility: hidden;
        }

        .Catalog a.Row.Expanded .ChevronDown {
            visibility: hidden;
        }

        .Catalog a.Row.Expanded .ChevronUp {
            visibility: visible;
        }


        .Catalog div.Row {
            display: flex;
        }

        .Catalog .SkuPicture {
            width: 150px;
            height: 171px;
        }

            .Catalog .SkuPicture img {
                width: 100%;
            }


        .Catalog .SkuFeature {
            display: flex;
            flex-direction: column;
            justify-content: space-between;
            margin-left: 10px;
        }

            .Catalog .SkuFeature .Nom {
                margin: 20px 0 5px;
            }

            .Catalog .SkuFeature .Price {
                display: flex;
                justify-content: space-between;
            }

            .Catalog .SkuFeature .Units {
                display: flex;
                justify-content: space-between;
                align-items: center;
                margin-top: 5px;
            }

                .Catalog .SkuFeature .Units input {
                    max-width: 50px;
                    text-align: right;
                    padding: 1px 2px;
                    border: 1px solid gray;
                }



        .Grids .Button {
            display: block;
            border-radius: 4px;
            border: 1px solid black;
            padding: 7px 15px;
            margin-top: 15px !important;
            margin-bottom: 20px;
        }

            .Grids .Button:hover {
                background-color: #fde95e;
                border: 1px solid #fde95e;
                color: black;
            }

        .Basket .Total {
            display: flex;
            justify-content: space-between;
            border-bottom: 0.5px solid gray;
            padding: 0;
            background-color: #EEEEEE;
        }

            .Basket .Total div {
                padding: 10px 10px;
            }

            .Basket .Total .Caption {
                white-space: nowrap;
            }

        .Basket .Item {
            display: grid;
            grid-template-columns: 1fr 80px 74px 16px;
            grid-template-areas: "dsc dsc dsc ico"
                "qty eur amt amt";
            margin: 15px 0;
        }


            .Basket .Item .Dsc {
                grid-area: dsc;
                text-align: left;
                white-space: nowrap;
                overflow: hidden;
                text-overflow: ellipsis;
                margin-bottom: 5px;
            }

            .Basket .Item .Ico {
                grid-area: ico;
                text-align: right;
                margin-left: 5px;
            }

            .Basket .Item .Qty {
                grid-area: qty;
                text-align: right;
            }

                .Basket .Item .Qty input {
                    max-width: 50px;
                    text-align: right;
                    padding: 1px 2px;
                    border: 1px solid gray;
                }

            .Basket .Item .Eur {
                grid-area: eur;
                text-align: right;
            }

            .Basket .Item .Amt {
                grid-area: amt;
                text-align: right;
                padding-right: 10px;
            }

        .Basket .SendRow div {
            display: flex;
            align-items: center;
        }

        .Basket .Conditions {
        }

            .Basket .Conditions input {
                vertical-align: top !important;
                margin: 0 10px;
            }
    </style>
End Section
