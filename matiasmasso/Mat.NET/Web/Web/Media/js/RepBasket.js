var atlas;

//---------------------------------------------------------------------------------------Event handlers


$(document).ready(function () {
    if (tarifa) { //if exists tarifa preloaded from view controller
        catalog = tarifa;
        initPurchaseOrder();
        loadCustomer();
        loadBrands()
        $('.Spinner.Loading').hide("fast");
    } else {
        loadAtlas();
    }
});


$(document).on('change', '#Country', function (event) {
    loadZonas();
});


$(document).on('change', '#Zona', function (event) {
    loadLocations();
});


$(document).on('change', '#Location', function (event) {
    loadCustomers()
});


$(document).on('change', '#Customer', function (event) {
    loadCatalog(currentCustomer().Guid);
});

$(document).on('click', '.CustomerSelected a', function (event) {
    event.preventDefault();
    clearCustomer();
});

$(document).on('catalogLoaded', function (e, argument) {
    loadCustomer();
});

$(document).on('click', "a.AdvancedOptions", function (event) {
    $('table.AdvancedOptions').toggle("fast");
});


$(document).on('click', '#CheckboxFchMin', function (event) {
    $('#FchMin').toggle();
});


$(document).on('click', '#ButtonAdd', function (event) {
    var emptyLines = (purchaseOrder.items.length == 0);
    $('#SubmitButton').prop('disabled', emptyLines);
});


$(document).on('click', '#ButtonSubmit', function (event) {
    $('.Spinner').show("fast");
    update()
});

$(document).on('updateSuccess', function (e, argument) {
    $('.DataCollection').hide();
    $('.Thanks').replaceWith(argument);
    $('.Spinner').hide("fast", function () {
        $('.Thanks').show("fast");
    });
});

$(document).on('updateFailure', function (e, argument) {
    $('.DataCollection').hide();
    $('.Thanks').replaceWith(argument);
    $('.Spinner').hide("fast", function () {
        $('.Thanks').show("fast");
    });
});


$(document).on('click', '#ButtonCancel', function (event) {
    window.location = $('#MmoBlankOrder').val();
});


$(document).on('click', '#RequestForNewOrder input[type=button]', function (event) {
    window.location = $('#MmoBlankOrder').val();
})


//---------------------------------------------------------------------------------------Database


function loadAtlas() {
    $('.Spinner.Loading').show();
    $('.CustomerSelection').hide();
    var url = $('#ApiAtlasUrl').val();

    var jqxhr = $.getJSON(url, function (result) {
        atlas = result;
    })
        .done(function () {
            $('.CustomerSelection').show("fast")
            loadCountries();
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $('.CustomerSelection').show("fast");
            $('.CustomerSelection').html('<div class="Error">' + textStatus + '</div>');
        })
        .always(function () {
            $('.Spinner.Loading').hide();
        });

}


function update() {
    var url = $('#MmoBasketUpdateUrl').val();
    updateRequest(url, formData());
}

function formData() {
    var retval = new FormData();
    retval.append('purchaseOrder', purchaseOrderFormData());
    retval.append('mailConfirmation', mailConfirmationFormData());
    return retval
}

function purchaseOrderFormData() {
    purchaseOrder.totjunt = $('#TotJunt').is(':checked');
    purchaseOrder.concept = $('#Concept').val();
    purchaseOrder.obs = $('#Obs').val();
    if ($('#CheckboxFchMin').is(':checked'))
        purchaseOrder.fchDeliveryMin = $('#FchMin').val();

    var retval = JSON.stringify(purchaseOrder);
    return retval;
}

function mailConfirmationFormData() {
    var value = $('#CheckBoxMail input[type=checkbox]').is(':checked');
    var retval = JSON.stringify(value);
    return retval;
}

//---------------------------------------------------------------------------------------Atlas

function loadCountries() {
    $('#Country').children('option:not(:first)').remove();
    $.each(atlas, function (key, item) {
        $('#Country').append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
    });

    if (atlas.length == 1) {
        $('#Country > option:eq(1)').prop('selected', true);
        $('#Country').hide();
        loadZonas();
    } else {
        $('#Country').addClass("Focused");
        $('#Country').prop("disabled", false);
        $("#Country").focus();
    }
}

function loadZonas() {
    var country = currentCountry();

    $('#Zona').children('option:not(:first)').remove();
    $.each(country.Zonas, function (key, item) {
        $('#Zona').append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
    });

    if (country.Zonas.length == 1) {
        $('#Zona > option:eq(1)').prop('selected', true);
        loadLocations()
    } else {
        $('#Country').removeClass("Focused");
        $('#Zona').addClass("Focused");

        $('#Zona').prop("disabled", false);
        $('#Location').prop("disabled", true);
        $('#Customer').prop("disabled", true);
        $('#Brand').prop("disabled", true);
        $('#Category').prop("disabled", true);
        $('#Sku').prop("disabled", true);
        $("#Zona").focus();
    }
}

function loadLocations() {
    var zona = currentZona();

    $('#Location').children('option:not(:first)').remove();
    $.each(zona.Locations, function (key, item) {
        $('#Location').append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
    });

    if (zona.Locations.length == 1) {
        $('#Location > option:eq(1)').prop('selected', true);
        loadCustomers()
    } else {
        $('#Zona').removeClass("Focused");
        $('#Customer').removeClass("Focused");
        $('#Location').addClass("Focused");

        $('#Location').prop("disabled", false);
        $('#Customer').prop("disabled", true);
        $('#Brand').prop("disabled", true);
        $('#Category').prop("disabled", true);
        $('#Sku').prop("disabled", true);
        $("#Location").focus();
    }
}


function loadCustomers() {
    var location = currentLocation();

    $('#Customer').children('option:not(:first)').remove();
    $.each(location.Contacts, function (key, item) {
        $('#Customer').append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
    });

    $('#Zona').removeClass("Focused");
    $('#Location').removeClass("Focused");
    $('#Customer').addClass("Focused");

    $('#Customer').prop("disabled", false);
    $('#Brand').prop("disabled", true);
    $('#Category').prop("disabled", true);
    $('#Sku').prop("disabled", true);
    $("#Customer").focus();
}


function loadCustomer() {
    var customer = purchaseOrder.customer;
    var address = customer.Address;
    var zip = address.Zip;
    var location = zip.Location;

    $('.CustomerSelected a').html(customer.Nom + '<br/>');
    $('.CustomerSelected a').append(address.Text + '<br/>');
    $('.CustomerSelected a').append(zip.ZipCod + ' ' + location.Nom + '<br/>');

    $('.CustomerSelection').hide("fast", function () {
        $('.CustomerSelected').show("fast", function () {
            $('a.AdvancedOptions').show("fast", function () {
                $('.ProductSelection').show("fast");
            })
        })
    });
}

function clearCustomer() {
    $('#Customer > option:eq(0)').prop('selected', true);
    $('a.AdvancedOptions').hide();
    $('.ProductSelection').hide();
    $('.CustomerSelected').hide("fast", function() {
        $('.CustomerSelection').show("fast");
    });
    purchaseOrder.items = [];
    redrawBasket();
}


function currentCountry() {
    var guid = $('#Country').val();
    var countries = $.grep(atlas, function (e) { return e.Guid == guid; });
    var retval = countries[0];
    return retval
}

function currentZona() {
    var guid = $('#Zona').val();
    var country = currentCountry();
    var zonas = $.grep(country.Zonas, function (e) { return e.Guid == guid; });
    var retval = zonas[0];
    return retval
}

function currentLocation() {
    var guid = $('#Location').val();
    var zona = currentZona();
    var locations = $.grep(zona.Locations, function (e) { return e.Guid == guid; });
    var retval = locations[0];
    return retval
}

function currentCustomer() {
    var guid = $('#Customer').val();
    var location = currentLocation();
    var customers = $.grep(location.Contacts, function (e) { return e.Guid == guid; });
    var retval = customers[0];
    return retval
}

