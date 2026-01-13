var catalog = {};
var purchaseOrder = {};
var item = {};


$(document).on('change', '#Brand', function (event) {
    loadCategories();
});


$(document).on('change', '#Category', function (event) {
    loadSkus();
});


$(document).on('change', '#Sku', function (event) {
    loadSku();
});


$(document).on('click', '#ButtonAdd', function (event) {
    validateQty();
});


$(document).on('click', '.CellKo', function (event) {
    removeItem($(this));
});

$(document).on('CustomerSelected', function (e, argument) {
    customer = argument;
    initPurchaseOrder(customer);
    loadCatalog(customer.guid);
});

function initPurchaseOrder(customer) {
    purchaseOrder = {
        customer: customer,
        items: []
    };
}


function loadBrands() {
    $('#Brand').children('option:not(:first)').remove();
    $.each(catalog.Brands, function (key, item) {
        $('#Brand').append('<option value=' + item.Guid + '>' + item.Nom.Esp + '</option>');
    });

    if (catalog.Brands.length == 1) {
        $('#Brand > option:eq(1)').prop('selected', true);
        loadCategories();
        $('#Brand').removeClass("Focused");
        $('#Category').addClass("Focused");
        $('#Sku').removeClass("Focused");
        $('#Brand').prop("disabled", true);
        $('#Category').prop("disabled", false);
        $('#Sku').prop("disabled", true);
    } else {
        $('#Brand').addClass("Focused");
        $('#Brand').prop("disabled", false);
        $('#Category').prop("disabled", true);
        $('#Sku').prop("disabled", true);
    }
}


function loadCategories() {
    var categories = currentCategories();

    $('#Category').children('option:not(:first)').remove();
    $('#Sku').children('option:not(:first)').remove();
    $.each(categories, function (key, item) {
        $('#Category').append('<option value=' + item.Guid + '>' + item.Nom.Esp + '</option>');
    });

    if (categories.length == 1) {
        $('#Category > option:eq(1)').prop('selected', true);
        loadSkus();
    } else {
        $('#Brand').removeClass("Focused");
        $('#Sku').removeClass("Focused");
        $('#Category').addClass("Focused");
        $('#Category').prop("disabled", false);
        $('#Sku').prop("disabled", true);
    }
}

function loadSkus() {
    var skus = currentSkus();

    $('#Sku').children('option:not(:first)').remove();
    $.each(skus, function (key, sku) {
        $('#Sku').append('<option value=' + sku.Guid + '>' + sku.Nom.Esp + '</option>');
    });

        $('#Category').removeClass("Focused");
        $('#Sku').addClass("Focused");
        $('#Sku').prop("disabled", false);
}

function loadSku() {
    $('.SkuThumbnail a').attr("href", '');
    $('.SkuThumbnail').addClass("Loading");
    var sku = currentSku();

    $('.SkuThumbnail').removeClass("Loading");
    $('.SkuThumbnail a').removeClass("disabled");
    $('.SkuThumbnail a').attr("href", $('#MmoSkuUrl').val() + '/' + sku.Guid);
    $('.SkuThumbnail a img').attr("src", $('#MmoSkuThumbnailUrl').val() + '/' + sku.Guid);
    $('#ButtonAdd').prop("disabled", false);
    $('#Qty').data("moq", moq(sku));
    $('#Qty').val(moq(sku));
    $('#Qty').addClass("Focused");
    $('#Brand').removeClass("Focused");
    $('#Category').removeClass("Focused");
    $('#Sku').removeClass("Focused");
}

function validateQty() {
    var qty = parseInt($('#Qty').val());
    if (qty > 0) {
        var moq = parseInt($('#Qty').data('moq'));
        var modResult = qty % moq;
        if (modResult === 0) {
            addItem(currentSku(), qty);
    }
    else {
        var moqwarn = $('#Qty').data('moqwarn') + moq;
        alert(moqwarn);
    }

}

function addItem(sku, qty) {
            item = {
                sku: sku,
                price: {
                    val: (sku.Price === undefined) ? 0 : sku.Price.Val,
                    eur: (sku.Price === undefined) ? 0 : sku.Price.Eur,
                    cur: {
                        tag: 'EUR'
                    }
                },
                dto: (sku.CustomerDto === undefined) ? 0 : sku.CustomerDto,
                qty: qty
            };

    purchaseOrder.items.push(item);
            redrawBasket();

            $('#buttonAdd').prop("disabled", true);
            $('.SkuThumbnail a').addClass("disabled");
            $('.SkuThumbnail a img').attr("src", "");
            $('#Qty').val('');
            $('#Qty').removeClass("Focused");
            $('#Brand').addClass("Focused");
            $('#Category').addClass("Focused");
            $('#Sku > option:eq(0)').prop('selected', true);
            $('#Sku').addClass("Focused");

    }

}

function toggleSubmitButtons() {
    if (purchaseOrder.items.length == 0) {
        $('.SubmitButtons').hide("fast");
    } else {
        $('.SubmitButtons').show("fast");
    }
}

function redrawBasket() {
    $('section.Grid .Row').remove();
    $.each(purchaseOrder.items, function (key, item) {
        $('section.Grid').append(rowHtml(item));
    });

    $('section.Grid').append(rowTotal());
    if (discountExists()) {
        $('.CellDto').show();
    } 
    toggleSubmitButtons();
}

function removeItem(e) {
    var index = e.closest(".Row").index() - 1;
    purchaseOrder.items.splice(index, 1);
    redrawBasket();
}



function rowHtml(item) {
    var nomLlarg = item.sku.NomLlarg.Esp;
    var retval = '<div class="Row">';
    retval += '<div class="CellTxt">';
    retval += nomLlarg;
    retval += '</div>';
    retval += '<div class="CellNum">';
    retval += item.qty;
    retval += '</div>';
    retval += '<div class="CellAmt">';
    retval += currencyFormat(item.price.eur);
    retval += '</div>';
    retval += '<div class="CellDto">';
    if (item.dto !== 0) {
        retval += item.dto + '%';
    }
    retval += '</div>';
    retval += '<div class="CellAmt">';
    retval += currencyFormat(item.price.eur * item.qty * (100 - item.dto) / 100);
    retval += '</div>';
    retval += '<div class="CellKo">';
    retval += '</div>';
    retval += '</div>';
    return (retval);
}
function rowTotal() {
    var retval = '<div class="Row">';
    retval += '<div class="CellTxt">Total</div>';
    retval += '<div class="CellNum">&nbsp;</div>';
    retval += '<div class="CellAmt">&nbsp;</div>';
    retval += '<div class="CellDto">&nbsp;</div>';
    retval += '<div class="CellAmt">';
    retval += currencyFormat(baseImponible());
    retval += '</div>';
    retval += '<div class="CellIco">&nbsp;</div>';
    retval += '</div>';
    return (retval);
}

function baseImponible() {
    var retval = 0;
    $.each(purchaseOrder.items, function (key, item) {
        retval += item.price.eur * item.qty * (100 - item.dto) / 100;
    });
    return retval;
}

function discountExists() {
    var retval;
    $.each(purchaseOrder.items, function (key, item) {
        if (item.dto !== 0) {
            retval = true;
        };
    });
    return retval;
}

function currentCategories() {
    var brand = currentBrand();
    var stockOnly = $('#StockOnly').is(':checked');
    if (stockOnly === true) {
        return categoriesInStock(brand);
    } else {
        return brand.Categories;
    };
}

function currentSkus() {
    var category = currentCategory();
    var stockOnly = $('#StockOnly').is(':checked');
    if (stockOnly === true) {
        return skusInStock(category);
    } else {
        return category.Skus;
    };
}


function currentBrand() {
    var Guid = $('#Brand').val();
    var brands = $.grep(catalog.Brands, function (e) { return e.Guid == Guid; });
    var retval = brands[0];
    return retval;
}

function currentCategory() {
    var Guid = $('#Category').val();
    var brand = currentBrand();
    var categories = $.grep(brand.Categories, function (e) { return e.Guid == Guid; });
    var retval = categories[0];
    return retval;
}

function currentSku() {
    var Guid = $('#Sku').val();
    var category = currentCategory();
    var skus = $.grep(category.Skus, function (e) { return e.Guid == Guid; });
    var retval = skus[0];
    retval.category = {Guid: category.Guid};
    retval.category.brand = { Guid: currentBrand().Guid };
    return retval;
}


function categoriesInStock(brand) {
    var retval = brand.Categories.filter(function (category) {
        return categoryInStock(category) === true;
    });
    return retval;
}

function categoryInStock(category) {
    var retval = skusInStock(category).length > 0;
    return retval;
}

function skusInStock(category) {
    var retval = category.Skus.filter(function (sku) {
        return sku.Stock-sku.Clients > 0;
    });
    return retval;
}


function currencyFormat(value) {
    var to2decimals = parseFloat(value).toFixed(2);
    var toDecimalComma = to2decimals.toString().replace(/\./g, ',');
    var toThousands = toDecimalComma.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1.");
    var retval = toThousands + '€';
    return retval;
}

function moq(sku) {
    var retval = 1;

    if (sku.HeredaDimensions == true) {
        var category = currentCategory();
        if (category.forzarInnerPack == true) {
            retval = Math.max(1, category.innerPack);
        }
    } else {
        if (sku.ForzarInnerPack == true) {
            retval = Math.max(1, sku.InnerPack);
        }
    }

    return retval
}



function loadCatalog(customerGuid) {
    $('.Spinner.Loading').show("fast");

    var url = $('#ApiCatalogUrl').val() + '/' + customerGuid;
    var jqxhr = $.getJSON(url, function (result) {
        catalog = result;
        //initPurchaseOrder();
        $(document).trigger('catalogLoaded', result);
    })
    .done(function () {
        loadBrands();
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        $('.ProductSelection').show("fast");
        $('.ProductSelection').html('<div class="Error">' + textStatus + '<br/>' + errorThrown + '</div>');
    })
    .always(function () {
        $('.Spinner.Loading').hide("fast");
    });

}

function updateRequest(url, formData) {

    var xhttp;
    if (window.XMLHttpRequest) {
        xhttp = new XMLHttpRequest();
    } else {
        xhttp = new ActiveXObject("Microsoft.XMLHTTP"); // code for IE6, IE5
    }

    xhttp.open('POST', url);
    xhttp.send(formData);
    xhttp.onreadystatechange = function () {
        if (xhttp.readyState == 4) {
            if (xhttp.status == 200) {
                //var response = $.parseJSON(xhttp.response);
                $(document).trigger('updateSuccess', xhttp.response);
            } else {
                $(document).trigger('updateFailure', xhttp.response);
            }
        }
    };
}

