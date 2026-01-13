@Code
    Layout = "~/Views/shared/_Layout_Pro.vbhtml"
    Dim lang = ContextHelper.Lang
End Code

<form method="post" action="/pro/proCatalog/Search" class="SkuSearch">
    <input type="text" name="SearchKey" />
</form>

<div class="Catalog">

    <div class="Grid Brands" data-contextmenu="Brands">
        <div class="HeaderRow">
            <div>@ContextHelper.Tradueix("Marca", "Marca", "Brand")</div>
        </div>
    </div>

    <div class="Grid Categories" data-contextmenu="Categories">
        <div class="HeaderRow">
            <div>@ContextHelper.Tradueix("Categoría", "Categoría", "Category")</div>
        </div>
    </div>

    <div class="Grid Skus" data-contextmenu="Skus">
        <div class="HeaderRow">
            <div>@ContextHelper.Tradueix("Producto", "Producte", "Product")</div>
            <div class="Stk">@ContextHelper.Tradueix("Stock")</div>
            <div class="Pvp">@ContextHelper.Tradueix("PVP", "PVP", "RRPP")</div>
        </div>
    </div>
</div>

<div class="ContextMenu" data-grid="Brands">
    <a href="#" data-url="/pro/proCatalog/Brand/{guid}/" target="_blank">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
</div>
<div class="ContextMenu" data-grid="Categories">
    <a href="#" data-url="/pro/proCatalog/Category/{guid}/" target="_blank">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
</div>
<div class="ContextMenu" data-grid="Skus">
    <a href="#" data-url="/pro/proCatalog/Sku/{guid}">@lang.Tradueix("Ficha", "Fitxa", "Properties")</a>
    <a href="#" data-url="/pro/proCatalog/resourceUpload/{guid}">@lang.Tradueix("Subir recurso", "Pujar recurs", "Resource upload")</a>
</div>

@Section Styles
    <style>
        .Aside {
            width: 200px;
        }

        .SkuSearch {
            text-align: right;
            padding-bottom: 15px;
        }

        .Catalog {
            display: flex;
            flex-direction: row;
            align-items: start;
            column-gap: 10px;
        }

        .Grid.Brands, .Grid.Categories {
            width: 200px;
        }

        .Grid .Row div {
            padding: 4px 7px 2px 0;
        }

        .Grid .Row div:first-child {
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
        }

        .Grid.Skus {
            flex: 0 1 auto;
            grid-template-columns: 1fr 80px 80px;
            width: 100%;
        }

            .Grid.Skus .Stk, .Grid.Skus .Pvp {
                text-align: right;
            }

        /*
        .Grid.Brands, .Grid.Categories {
            position: relative;
            flex: 0 1 auto;
            display: grid;
            grid-template-columns: 1fr;
            min-width: 150px;
        }



            .Grid.Skus .Row {
                display: contents;
            }


            .Grid.Brands div, .Grid.Categories div, .Grid.Skus div {
                padding: 4px 7px 2px 4px;
                border: 1px solid #E0E0E0;
            }

                .Grid.Brands div:first-child, .Grid.Categories div:first-child, .Grid.Skus div:first-child > div {
                    background-color: #F0F0F0;
                }


            .Grid.Brands > div:not(:first-child):hover, .Grid.Categories > div:not(:first-child):hover, .Grid.Skus > div:not(:first-child) div:hover {
                background-color: #167ac6;
                color: white;
                cursor: pointer;
            }
            */
    </style>
End Section

@Section Scripts
    <script src="~/Media/js/ContextMenu.js"></script>
    <script>
        var model = [];
        var contextmenu = $(".ContextMenu");
        var selectedItem;

        $(document).ready(function () {
            loadCatalog();
        });

        $(document).on('click', '.Grid.Brands > div:not(:first)', function (e) {
            loadCategories();
        });

        $(document).on('click', '.Grid.Categories > div:not(:first)', function (e) {
            loadSkus();
        });


        function loadBrands() {
            $.each(model, function (key, item) {
                $('.Grid.Brands').append('<div class="Row" data-guid="' + item.Guid + '"><div>' + item.Nom.Esp + '</div></div>');
            });
        }

        function loadCategories() {
            $('.Grid.Categories .Row').remove();
            $('.Grid.Skus .Row').remove();
            $.each(SelectedBrand().Categories, function (key, item) {
                $('.Grid.Categories').append('<div class="Row" data-guid="' + item.Guid + '"><div>' + item.Nom.Esp + '</div></div>');
            });
        }

        function loadSkus() {
            $('.Grid.Skus .Row').remove();
            $('.Grid.Skus').append(spinner);

            var url = "/pro/proCatalog/skus/" + SelectedCategory().Guid;
            $.getJSON(url, function (result) {
            })
                .done(function (result) {
                    if (result.success) {
                        var skus = result.data;
                        $.each(skus, function (key, item) {
                            loadSku(item);
                        });

                    } else {
                        alert('failed: ' + result.message);
                        $('#ErrorWarning').innerText(result.message);
                        $('#ErrorWarning').show();
                        console.log(result.message);
                    }
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    alert('failed: ' + xhr.responseText);
                    $('#ErrorWarning').html(xhr.responseText);
                    $('#ErrorWarning').show();
                    console.log(xhr.responseText);
                })
                .always(function () {
                    spinner.remove();
                });

        }

        function loadSku(item) {
            var row = $('<div class="Row" data-guid="' + item.Guid + '"></div>');
            $('.Grid.Skus').append(row);
            row.append('<div>' + item.Nom.Esp + '</div>');
            row.append('<div class="Stk">' + item.Stock + '</div>');
            row.append('<div class="Pvp">' + item.Rrpp.Eur + '</div>');
        }

        function SelectedBrand() {
            var guid = $('.Grid.Brands .Active').data('guid');
            var matchingItems = model.filter(function (item) { return item.Guid === guid; });
            var retval = matchingItems[0];
            return retval;
        }

        function SelectedCategory() {
            var guid = $('.Grid.Categories .Active').data('guid');
            var matchingItems = SelectedBrand().Categories.filter(function (item) { return item.Guid === guid; });
            var retval = matchingItems[0];
            return retval;
        }

        function loadCatalog() {
            $('.Catalog').append(spinner);
            var url = "/Pro/proCatalog/tree";
            $.getJSON(url, function (result) {
            })
                .done(function (result) {
                    if (result.success) {
                        model = result.data;
                        loadBrands();
                    } else {
                        $('#ErrorWarning').innerText(result.message);
                        $('#ErrorWarning').show();
                        console.log(result.message);
                        alert('failed: ' + result.message);
                    }
                })
                .fail(function (xhr, textStatus, errorThrown) {
                    $('#ErrorWarning').html(xhr.responseText);
                    $('#ErrorWarning').show();
                    console.log(xhr.responseText);
                    alert('failed: ' + xhr.responseText);
                })
                .always(function () {
                    spinner.remove();
                });

        }
    </script>
End Section
