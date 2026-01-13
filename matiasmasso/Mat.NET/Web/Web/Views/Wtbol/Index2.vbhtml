@Code
    Layout = "~/Views/Shared/_Layout_2.vbhtml"
    'Layout = "~/Views/shared/_Layout_FullWidth.vbhtml"
End Code


<div class="PageWrapper">
    <div class="Title">Market Place</div>

    <a class="Epigraf" href="#" Expanded="true">Configuración<i class="fa-solid fa-chevron-up"></i></a>
    <div class="SettingsWrapper">
        <div id="SettingsGrid">
            <div class="Label">Titular</div>
            <div><input type="Text" disabled value="@Model.Site.Customer.Nom"></div>
            <div class="Label">Merchant Id</div>
            <div><input type="Text" disabled value="@Model.Site.MerchantId"></div>
            <div class="Label">Clave</div>
            <div><input type="Text" disabled value="@Model.Site.Guid.ToString()"></div>
            <div class="Label">Landing pages registradas</div>
            <div><input type="Text" disabled value="@Model.Site.LandingPages.Count"></div>
            <div class="Label">Última declaración de stocks</div>
            <div><input type="Text" disabled value="@Model.Site.LastStkFch.ToString("dd/MM/yy HH:mm")"></div>
            <div class="Label">Página web</div>
            <div><input type="Text" id="Website" value="@Model.Site.Website"></div>
            <div class="Label">Persona de contacto</div>
            <div><input type="Text" id="ContactNom" value="@Model.Site.ContactNom"></div>
            <div class="Label">Email de contacto</div>
            <div><input type="Text" id="ContactEmail" value="@Model.Site.ContactEmail"></div>
            <div class="Label">Teléfono de contacto</div>
            <div><input type="Text" id="ContactTel" value="@Model.Site.ContactTel"></div>
            <div></div>
            <div class="Buttons">
                <div class="Spacer"></div>
                <button class="CancelButton">Cancelar</button>
                <button class="SubmitButton" disabled>Aceptar</button>
            </div>

        </div>

    </div>


    <a class="Epigraf" href="#" Expanded="true">Sugerencias<i class="fa-solid fa-chevron-up"></i></a>
    <ol id="TasksWrapper">
    </ol>


    <a class="Epigraf" href="#">Landing pages<i class="fa-solid fa-chevron-down"></i></a>
    <div class="LandingPagesWrapper" hidden="hidden">
        <a href="#" class="AddLandingPage"><i class="fa-regular fa-square-plus"></i>&nbsp;Añadir nueva landing page</a>

        <div class="Grid" id="LandingPages">
            <div class="Row Header">
                <div class="Stock">Stock</div>
                <div class="Brand Truncate">Marca comercial</div>
                <div class="Category Truncate">Categoría</div>
                <div class="Sku Truncate">Producto</div>
            </div>
        </div>
    </div>

</div>

<template id="RowTemplate">
    <a class="Row" href="#">
        <div class="Stock"><input type="number" min="0"></div>
        <div class="Brand Truncate"></div>
        <div class="Category Truncate"></div>
        <div class="Sku Truncate"></div>
        <div class="Spacer">&nbsp;</div>
        <div class="Url Truncate"></div>
    </a>
</template>

<template id="LandingPageFormTemplate">
    <div id="LandingPageForm">
        <div id="Filters">
            <select id="LandingPageType" class="Active">
                <option value="" selected>(tipo de página)</option>
                <option value="Brand">página de marca</option>
                <option value="Category">página de categoría</option>
                <option value="Sku">página de producto</option>
            </select>
            <select id="Brand" hidden="hidden">
                <option value="" selected>(marca comercial)</option>
            </select>
            <select id="Category" hidden="hidden">
                <option value="" selected>(seleccionar categoríal)</option>
            </select>
            <select id="Sku" hidden="hidden">
                <option value="" selected>(seleccionar producto)</option>
            </select>
        </div>
        <div id="Url">
            <input type="text" placeholder="landing page http://..." disabled />
            <button class="CancelButton">Cancelar</button>
            <button class="DeleteButton" disabled>Eliminar</button>
            <button class="SubmitButton" disabled>Aceptar</button>
        </div>
    </div>
</template>

<template id="SingleUrlTemplate">
    <div id="SingleUrl">
        <input type="text" placeholder="landing page http://..." />
        <button class="SubmitButton" disabled>Aceptar</button>
    </div>
</template>

@section Scripts
    <script src="https://kit.fontawesome.com/05a6a08892.js" crossorigin="anonymous"></script>
    <script>
            var model = {};
            var rowTemplate = $('#RowTemplate')[0];
            var landingPageFormTemplate = $('#LandingPageFormTemplate')[0];
            var singleUrlTemplate = $('#SingleUrlTemplate')[0];
            var spinner = $('<i class="fa-solid fa-spinner fa-spin"></i>');

            $(document).ready(function (e) {
                LoadData()
            })

            $(document).on('DataLoaded', function (e, argument) {
                LoadLandingPages();
                SuggestMissingPages();
            })

            $(document).on('click', 'a.Epigraf', function (e) {
                e.preventDefault();
                if ($(this).prop('Expanded') == true) {
                    $(this).prop('Expanded', false)
                    $(this).next().hide();
                    $(this).find('i').removeClass('fa-chevron-up');
                    $(this).find('i').addClass('fa-chevron-down');
                } else {
                    $(this).prop('Expanded', true)
                    $(this).next().show();
                    $(this).find('i').removeClass('fa-chevron-down');
                    $(this).find('i').addClass('fa-chevron-up');
                }
            })

            $(document).on('keypress paste change', '#SettingsGrid input', function (e) {
                $('#SettingsGrid .SubmitButton').prop('disabled', false);
            })

            $(document).on('click', '#SettingsGrid .CancelButton', function (e) {
                $('a.Epigraf')[0].click();
            })

            $(document).on('click', '#SettingsGrid .SubmitButton', function (e) {
                UpdateSettings();
            })

            $(document).on('click', '#Url, #SingleUrl', function (e) {
            e.stopPropagation();
        })

        $(document).on('click', 'a.Row', function (e) {
            e.preventDefault();
            if ($(this).prop('editing') == true) {
                LoadLandingPages();
                return false;
            } else {
                $(this).prop('editing', true);
                var template = landingPageFormTemplate.content.cloneNode(true);
                var productGuid = $(this).data('product');
                var landingPage = LandingPage(productGuid);
                $(this).find('.Url').empty();
                $(this).find('.Url').append($(template));
                $('#Filters').hide();
                $('#Url input[type="text"]').val(landingPage.Url);
                $('#Url input[type="text"]').prop('disabled', false);
                $('#LandingPageForm .DeleteButton').prop('disabled', false);
            }
        })

            $(document).on('click', '#LandingPageForm .CancelButton', function (e) {
            if ($('#LandingPageForm').prop('adding') == true)
                $('#LandingPageForm').remove();
            else
                LoadLandingPages();
            return false;
        })

            $(document).on('click', '#LandingPageForm .DeleteButton', function (e) {
                var template = $(this).closest('#LandingPageForm');
                var row = $(this).closest('a.Row');
                var productGuid = $(row).data('product');
                DeleteLandingPage(productGuid, template)
            return false;
        })

            $(document).on('click', '#LandingPageForm .SubmitButton', function (e) {
                var template = $(this).closest('#LandingPageForm');
                $(this).prop('disabled', true);
                var productGuid;

                if ($(template).prop('adding') == true) {
                    productGuid = LandingPageFormProductGuid();
                } else {
                    var row = $(this).closest('a.Row');
                    productGuid = $(row).data('product');
                }

                var url = $(template).find('input[type="text"]').val();
                UpdateLandingPage(productGuid, url, template);
                return false;

            })



            $(document).on('click', '#Url input, #SingleUrl input', function (e) {
                //prevents reloading the page on click
                return false;
            })

        $(document).on('click', '.Row .Stock input', function (e) {
            //prevents reloading the page on click
            return false;
        })

        $(document).on('change', '.Row .Stock input', function (e) {
            var stock = $(this).val();
            $(this).css("background-color", StockColor(stock));
            var productGuid = $(this).closest('a.Row').data('product');
            UpdateStock($(this), model.Site.Guid, productGuid, stock);
        })


        $(document).on('click', 'a.AddLandingPage', function (e) {
            e.preventDefault();
            if ($('#LandingPageForm').prop('adding') == true) {
                $('#LandingPageForm').remove();
            } else {
                var template = landingPageFormTemplate.content.cloneNode(true);
                $(template).insertAfter(this);
                $('#LandingPageForm').prop('adding', true);
                LoadBrands();
            }
        })

        $(document).on('change', '#LandingPageType', function (e) {
            if ($('#LandingPageType').val() == '') {
                $('#LandingPageType').addClass('Active');
                $('#Brand').removeClass('Active');
                $('#Brand').hide();
            } else {
                $('#LandingPageType').removeClass('Active');
                $('#Brand').addClass('Active');
                $('#Brand').show();
            }
            $('#Category').removeClass('Active');
            $('#Category').hide();
            $('#Sku').removeClass('Active');
            $('#Sku').hide();
        })

        $(document).on('change', '#Brand', function (e) {
            var brandGuid = $('#Brand').val();
            $('#Category option').not(':first').remove();
            $('#Sku option').not(':first').remove();
            $('#Sku').removeClass('Active');
            $('#Sku').hide();
            if (brandGuid === '')
                $('#Category').hide();
            else {
                $('#Brand').removeClass('Active');
                if ($('#LandingPageType').val() == 'Brand') {
                    $('#Url input[type="text"]').prop('disabled', false)
                    $('#Url input[type="text"]').addClass('Active');
                    DisplayUrlIfExists(brandGuid);
                } else {
                    $('#Category').addClass('Active');
                    $('#Category').show();
                    var brand = model.Catalog.Brands.find(x => x.Guid === brandGuid);
                    $.each(brand.Categories, function (index, category) {
                        $('#Category').append($('<option>', {
                            value: category.Guid,
                            text: category.Nom
                        }));
                    });
                }
            }
        })

        $(document).on('change', '#Category', function (e) {
            $('#Category').removeClass('Active');
            $('#Sku option').not(':first').remove();
            var categoryGuid = $('#Category').val();
            if (categoryGuid === '')
                $('#Sku').hide();
            else {
                if ($('#LandingPageType').val() == 'Category') {
                    $('#Url input[type="text"]').prop("disabled", false)
                    $('#Url input[type="text"]').addClass('Active');
                    DisplayUrlIfExists(categoryGuid);
                } else {
                    $('#Sku').addClass('Active');
                    $('#Sku').show();
                    var category = model.Catalog.Brands.flatMap(x => x.Categories).find(x => x.Guid === categoryGuid);
                    $.each(category.Skus, function (index, sku) {
                        $('#Sku').append($('<option>', {
                            value: sku.Guid,
                            text: sku.Nom
                        }));
                    });
                }
            }
        })

        $(document).on('change', '#Sku', function (e) {
            var skuGuid = $('#Sku').val();
            if (skuGuid === '') {
                $('#Url input[type="text"]').prop('disabled', true);
                $('#Url input[type="text"]').removeClass('Active');
            } else {
                $('#Sku').removeClass('Active');
                $('#Url input[type="text"]').prop('disabled', false);
                $('#Url input[type="text"]').addClass('Active');
                DisplayUrlIfExists(skuGuid);
            }
        })


            $(document).on('change keypress paste', '#Url input[type="text"], #SingleUrl input[type="text"]', function (e) {
            var url = $(this).find('input[type="text"]').val();
                $(this).siblings('.SubmitButton').prop('disabled', url === '');
        })




        function LoadBrands() {
            $.each(model.Catalog.Brands, function (index, brand) {
                $('#Brand').append($('<option>', {
                    value: brand.Guid,
                    text: brand.Nom
                }));
            })
        }

        function LoadLandingPages() {
            var landingPage = null;
            $('.Grid .Row').not(':first').remove();

            if (model.Site.LandingPages.length == 0)
                $('.Grid').hide();
            else {
                $('.Grid').show();
                $.each(model.Catalog.Brands, function (index, brand) {
                    landingPage = model.Site.LandingPages.find(x => x.ProductGuid === brand.Guid);
                    if (landingPage != null)
                        AppendRow(brand.Nom, null, null, landingPage)

                    $.each(brand.Categories, function (index, category) {
                        landingPage = model.Site.LandingPages.find(x => x.ProductGuid === category.Guid);
                        if (landingPage != null)
                            AppendRow(brand.Nom, category.Nom, null, landingPage)
                        $.each(category.Skus, function (index, sku) {
                            landingPage = model.Site.LandingPages.find(x => x.ProductGuid === sku.Guid);
                            if (landingPage != null)
                                AppendRow(brand.Nom, category.Nom, sku.Nom, landingPage)
                        });

                    });

                });
            }
        }

        function AppendRow(brandNom, categoryNom, skuNom, landingPage) {
            var row = rowTemplate.content.cloneNode(true);
            var stock = Stock(landingPage.ProductGuid);
            $(row).find('.Row').attr('data-product', landingPage.ProductGuid);
            if (skuNom == null)
                $(row).find('.Stock input').hide();
            else {
                $(row).find('.Stock input').val(stock);
                $(row).find('.Stock input').css("background-color", StockColor(stock));
            }
            $(row).find('.Brand').text(brandNom);
            $(row).find('.Category').text(categoryNom);
            $(row).find('.Sku').text(skuNom);
            $(row).find('.Url').text(landingPage.Url);
            $('.Grid').append(row);
        }

        function StockColor(stock) {
            var retval = stock <= 0 ? "#FDD" : "#DFD";
            return retval;
        }

        function Stock(productGuid) {
            var retval = 0;
            var stock = null;
            var brands = model.Catalog.Brands;
            var brand = brands.find(x => x.Guid === productGuid);
            if (brand == undefined) {
                var categories = brands.flatMap(x => x.Categories);
                var category = categories.find(x => x.Guid === productGuid);
                if (category == undefined) {
                    stock = model.Site.Stocks.find(x => x.ProductGuid === productGuid);
                    retval = (stock == null) ? 0 : stock.Qty;
                } else {
                    var skus = category.Skus;
                    stocks = model.Site.Stocks.filter(x => skus.some( y => x.Guid === y.Guid));
                    retval = (stocks.length == 0) ? 0 : stocks.reduce((s, a) => s + a.Qty, 0);
                }
            } else {
                var skus = brand.Categories.flatMap(x => x.Skus);
                stocks = model.Site.Stocks.filter(x => skus.some(y => x.Guid === y.Guid));
                retval = (stocks.length == 0) ? 0 : stocks.reduce((s, a) => s + a.Qty, 0);
            }
            return retval;
        }

        function BrandNom(landingPage) {
            var retval = "";
            var brands = model.Catalog.Brands.filter(x => x.Categories.some(y => y.Skus.some(z => z.Guid === landingPage.ProductGuid)));
            if (brands.length > 0)
                retval = brands[0].Nom;
            return retval;
        }

        function CategoryNom(landingPage) {
            var retval = "";
            var categories = model.Catalog.Brands.flatMap(x => x.Categories).filter(y => y.Skus.some(z => z.Guid === landingPage.ProductGuid));
            if (categories.length > 0)
                retval = categories[0].Nom;
            return retval;
        }

        function SkuNom(landingPage) {
            var retval = "";
            var skus = model.Catalog.Brands.flatMap(x => x.Categories).flatMap(y => y.Skus).filter(z => z.Guid === landingPage.ProductGuid);
            if (skus.length > 0)
                retval = skus[0].Nom;
            return retval;
        }

        function LandingPage(productGuid) {
            return model.Site.LandingPages.find(x => x.ProductGuid === productGuid);
        }

        function DisplayUrlIfExists(productGuid) {
            var landingPage = LandingPage(productGuid);
            if (landingPage != null)
                $('#Url input[type="text"]').val(landingPage.Url);
        }

        function LandingPageFormProductGuid() {
            var retval = '';
            if ($('#LandingPageType').val() == 'Brand') {
                retval = $('#Brand').val();
            } else if ($('#LandingPageType').val() == 'Category') {
                retval = $('#Category').val();
            } else if ($('#LandingPageType').val() == 'Sku') {
                retval = $('#Sku').val();
            }
            return retval;
            }

            //------------- epifgraf Tasks

            function SuggestMissingPages() {
                $('#TasksWrapper').empty();
                $.each(model.Catalog.Brands, function (index, brand) {
                    if (BrandHasLandingPages(brand) == true) {
                        var brandPage = model.Site.LandingPages.find(x => x.ProductGuid === brand.Guid);
                        if (brandPage == undefined) SuggestMissingPage(brand)

                        $.each(brand.Categories, function (index, category) {
                            if (CategoryHasLandingPages(category) == true) {
                                var categoryPage = model.Site.LandingPages.find(x => x.ProductGuid === category.Guid);
                                if (categoryPage == undefined) SuggestMissingPage(brand, category)
                            }
                        })
                    }
                })
                $('#TasksWrapper a').first().click();

                if ($('#TasksWrapper').children().length == 0) {
                    if (model.Site.LandingPages.length == 0) {
                        $('#TasksWrapper').append('<a class="AddLandingPage">Registra tu primera landing page</a>')
                        $('#TasksWrapper a').click();
                    } else {
                        $('#TasksWrapper').append('<div>Añade landing pages adicionales haciendo clic sobre el enlace "Añadir nueva landing page" que encontrarás dentro del apartado "Landing pages"</div>')
                    }
                }
            }

            function BrandHasLandingPages(brand) {
                var skus = brand.Categories.flatMap(x => x.Skus);
                var retval = model.Site.LandingPages.some(x => skus.some(y => y.Guid === x.ProductGuid));
                return retval;
            }

            function CategoryHasLandingPages(category) {
                var skus = category.Skus;
                var retval = model.Site.LandingPages.some(x => skus.some(y => y.Guid === x.ProductGuid));
                return retval;
            }

            function SuggestMissingPage(brand, category) {
                if (category == undefined) {
                    var anchor = $('<li><a class="Task" data-pagetype="Brand" data-product="' + brand.Guid + '"  data-brand="' + brand.Guid + '" href="#">Proporciona una landing page para la marca ' + brand.Nom + '</a></li>')
                } else {
                    var anchor = $('<li><a class="Task" data-pagetype="Category" data-product="' + category.Guid + '"  data-brand="' + brand.Guid + '"  data-category="' + category.Guid + '" href="#">Proporciona una landing page para la categoria ' + category.Nom + ' de ' + brand.Nom + '</a></li>')
                }
                $('#TasksWrapper').append(anchor);
            }

            $(document).on('click', '.Task', function (e) {
                e.preventDefault();
                if ($('#SingleUrl').prop('adding') == true) {
                    $('#SingleUrl').remove();
                } else {
                    var pageType = $(this).data('pagetype');
                    var brand = $(this).data('brand');
                    var category = $(this).data('category');
                    var template = singleUrlTemplate.content.cloneNode(true);
                    $(template).insertAfter(this);
                    $('#SingleUrl').prop('adding', true);
                }
            })

            $(document).on('click', '#SingleUrl .SubmitButton', function (e) {
                var template = $(this).closest('#SingleUrl');
                $(template).find('.SubmitButton').prop('disabled', true);
                var anchor = $(template).closest('li').find('a.Task');
                var productGuid = anchor.data('product');
                var url = $(template).find('input[type="text"]').val();
                UpdateLandingPage(productGuid, url, template);
                return false;

            })
            //-----------------------------

            function UpdateSettings() {
                var button = $('#SettingsGrid .SubmitButton');
                button.val(spinner);
            var url = '@MmoUrl.ApiUrl("WtbolSite")';
                var data = jQuery.extend(true, {}, model.Site);
                data.Website = $('#Website').val();
                data.ContactNom = $('#ContactNom').val();
                data.ContactEmail = $('#ContactEmail').val();
                data.ContactTel = $('#ContactTel').val();

            var jqxhr = $.post(url, data, function (result) {
            })
                .done(function () {
                    model.Site = data;
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    alert("error: " + errorThrown);
                    $('#Website').val(model.Site.Website);
                    $('#ContactNom').val(model.Site.ContactNom);
                    $('#ContactEmail').val(model.Site.ContactEmail);
                    $('#ContactTel').val(model.Site.ContactTel);
                })
                .always(function () {
                    button.val("Aceptar");
                    button.prop('disabled', true);
                });
        }

        function UpdateStock(input, site, productGuid, stock) {
            $(input).hide();
            $(input).parent().append(spinner);
            var url = '@MmoUrl.ApiUrl("WtbolSite/StockUpdate")';
            var data = { 'Site': { 'Guid': site }, 'Sku': { 'Guid': productGuid }, 'Stock': stock };

            var jqxhr = $.post(url, data, function (result) {
                //alert("success");
            })
                .done(function () {
                    //alert("second success");
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    alert("error: " + errorThrown);
                    var stockModel = model.Site.Stocks.find(x => x.ProductGuid === productGuid);
                    var previousStock = stockModel == null ? 0 : stockModel.Qty;
                    $(input).val(previousStock);
                    $(input).css("background-color", StockColor(previousStock));
                })
                .always(function () {
                    spinner.remove();
                    $(input).show();
                });
        }


            function UpdateLandingPage(productGuid, landingUrl, template) {
                var button = $(template).find('.SubmitButton');
                $(spinner).insertAfter(button);
                button.hide();
                var url = '@MmoUrl.ApiUrl("WtbolSite/LandingPageUpdate")';
                var data = { 'Site': { 'Guid': model.Site.Guid }, 'Product': { 'Guid': productGuid }, 'Uri': { 'OriginalString': landingUrl } };

                var jqxhr = $.post(url, data, function (result) {
                })
                    .done(function () {
                        var landingPage = LandingPage(productGuid);
                        if (landingPage == null) {
                            landingPage = { ProductGuid: productGuid, Url: landingUrl };
                            model.Site.LandingPages.push(landingPage);
                        }
                        else
                            landingPage.Url = landingUrl;

                        LoadLandingPages();
                        $(template).remove();
                        SuggestMissingPages();

                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        alert("error: " + errorThrown);
                    })
                    .always(function () {
                        spinner.remove();
                        $(button).show();
                    });
            }

            function DeleteLandingPage(productGuid, template) {
                var button = $(template).find('.DeleteButton');
                $(spinner).insertAfter(button);
                button.hide();
                var url = '@MmoUrl.ApiUrl("WtbolSite/LandingPageDelete")';
                var data = { 'Site': { 'Guid': model.Site.Guid }, 'Product': { 'Guid': productGuid }};

                var jqxhr = $.post(url, data, function (result) {
                })
                    .done(function () {
                        var landingPages = model.Site.LandingPages;
                        landingPages.splice(landingPages.findIndex(e => e.ProductGuid === productGuid), 1);
                        LoadLandingPages();
                        $(template).remove();
                        SuggestMissingPages();

                    })
                    .fail(function (jqXHR, textStatus, errorThrown) {
                        alert("error: " + errorThrown);
                    })
                    .always(function () {
                        spinner.remove();
                        $(button).show();
                    });
            }

        function LoadData() {
            var url = '@MmoUrl.ApiUrl("wtbol/model", Mvc.ContextHelper.GetUser().Guid.ToString())';
            var jqxhr = $.getJSON(url, function (result) {
            })
                .done(function (data, textStatus, jqXHR) {
                    $(document).trigger('DataLoaded', result);
                })
                .fail(function (jqXHR, textStatus, errorThrown) {
                    $('#Errors').html('<div class="Error">' + textStatus + '<br/>' + errorThrown + '</div>');
                })
                .always(function () {
                });
        }


    </script>
End Section


@Section Styles

    <style scoped>
        .PageWrapper {
            display: flex;
            flex-direction: column;
            justify-content: flex-start;
            max-width: 900px;
            margin: auto;
        }

        .Title {
            margin: 0 auto;
            color: gray;
            font-size: large;
            font-weight: 600;
        }

        .Epigraf {
            margin-top: 20px;
            color: cornflowerblue;
            font-weight: 600;
        }

            .Epigraf i {
                padding-left: 10px;
            }

        .SettingsWrapper, .LandingPagesWrapper, #TasksWrapper {
            padding: 15px 0 0 30px;
        }

        #SettingsGrid {
            display: grid;
            grid-template-columns: auto 1fr;
            column-gap: 10px;
            width: 100%;
        }

            #SettingsGrid input {
                width: 100%;
            }

            #SettingsGrid .Label {
                padding: 5px 7px 2px 4px;
            }


            #SettingsGrid .Buttons {
                display: grid;
                grid-template-columns: 1fr 90px 90px;
                column-gap: 5px;
                margin-top: 5px;
                height: 29px;
            }


        #Filters {
            display: grid;
            grid-template-columns: auto auto auto 1fr;
            column-gap: 5px;
            margin-top: 15px;
        }

        .Active {
            background-color: lemonchiffon;
        }

        a.AddLandingPage {
            display: block;
            margin-top: 15px;
        }

        #LandingPageForm {
            grid-column: 1 / -1; /*full width*/
        }

        #TasksWrapper #LandingPageForm {
            padding-bottom: 10px;
        }

        #Url {
            display: grid;
            grid-template-columns: 1fr 90px 90px 90px;
            column-gap: 5px;
            margin-top: 5px;
        }

        #SingleUrl {
            display: grid;
            grid-template-columns: 1fr 90px;
            column-gap: 5px;
            margin: 5px 0;
        }

            #Url input[type="text"], #SingleUrl input[type="text"] {
                width: 100%;
                text-align: left;
            }

            #Url > *:not(first), #SingleUrl > *:not(first) {
                text-align: center;
            }

        .Grid {
            display: grid;
            grid-template-columns: 60px minmax(50px,150px) minmax(50px,150px) minmax(50px,1fr);
            grid-auto-flow: row;
            margin-top: 15px;
        }

            .Grid .Row {
                display: contents;
            }

        .Row.Header {
            font-weight: 700;
        }

        .Row .Stock input::-webkit-outer-spin-button,
        .Row .Stock input::-webkit-inner-spin-button {
            margin-left: 3px;
        }

        .Row .Stock input {
            text-align: right;
            width: 100%;
            padding: 4px 0 2px 4px;
        }

        .Row .Stock {
            text-align: center;
            width: 100%;
            padding: 0 10px 0 0;
        }

        .Row .Brand {
            text-align: left;
            padding: 4px 7px 2px 4px;
        }

        .Row .Category {
            text-align: left;
            padding: 4px 7px 2px 4px;
        }

        .Row .Sku {
            text-align: left;
            padding: 4px 7px 2px 4px;
        }

        .Row .Url {
            text-align: left;
            grid-column: 2 / -1; /*full width*/
            padding: 2px 7px 10px 4px;
        }

        .Row.Header div {
            padding: 2px 7px 10px 4px;
        }
    </style>
End Section
