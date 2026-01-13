var storelocator;
//var countries;
var inStock = $('#inStock').val();

function loadStoreLocator(product, lang) {
    $.ajax({
        url: '/storelocator/fetch/' + product,
        type: "GET",
        success: function (result) {
            storelocator = result;
            loadDropdown($('#dropdownCountry'), storelocator.Offline.Countries, storelocator.Offline.DefaultCountry);
            loadDropdown($('#dropdownZona'), storelocator.Offline.DefaultCountry.Zonas, storelocator.Offline.DefaultZona);
            loadDropdown($('#dropdownLocation'), storelocator.Offline.DefaultZona.Locations, storelocator.Offline.DefaultLocation);
            loadDistributors($('#divDistributors'), storelocator.Offline.DefaultLocation);
            loadLandingPages($('.storelocator .landingPages'), storelocator.Online.LandingPages, lang);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }       
    });
}

function loadPremiumStoreLocator(premiumline, lang) {
    $.ajax({
        url: '/storelocator/fetchPremium/' + premiumline,
        type: "GET",
        success: function (result) {
            storelocator = result;
            loadDropdown($('#dropdownCountry'), storelocator.Offline.Countries, storelocator.Offline.DefaultCountry);
            loadDropdown($('#dropdownZona'), storelocator.Offline.DefaultCountry.Zonas, storelocator.Offline.DefaultZona);
            loadDropdown($('#dropdownLocation'), storelocator.Offline.DefaultZona.Locations, storelocator.Offline.DefaultLocation);
            loadDistributors($('#divDistributors'), storelocator.Offline.DefaultLocation);
            loadLandingPages($('.storelocator .landingPages'), storelocator.Online.LandingPages, lang);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Status: " + textStatus); alert("Error: " + errorThrown);
        }       
    });
}


$(document).on('storeLocatorChanged', function () {
    if (typeof gtag_report_conversion === "function") {
        gtag_report_conversion(window.location.href);
    }
});

$(document).on('change', '#dropdownCountry', function (e) {
    loadDropdown($('#dropdownZona'), selectedCountry().Zonas, selectedCountry().DefaultZona);
    loadDropdown($('#dropdownLocation'), selectedZona().Locations, selectedZona().DefaultLocation);
    loadDistributors($('#divDistributors'), selectedLocation());
    $(document).trigger('storeLocatorChanged');
});

$(document).on('change', '#dropdownZona', function (e) {
    loadDropdown($('#dropdownLocation'), selectedZona().Locations, selectedZona().DefaultLocation);
    loadDistributors($('#divDistributors'), selectedLocation());
    $(document).trigger('storeLocatorChanged');
});

$(document).on('change', '#dropdownLocation', function (e) {
    loadDistributors($('#divDistributors'), selectedLocation());
    $(document).trigger('storeLocatorChanged');
});

function selectedCountry() {
    var retval;
    var guid = $('#dropdownCountry').val();
    if (guid === null) 
        retval = storelocator.Offline.DefaultCountry;
    else
        retval = storelocator.Offline.Countries.filter(function (item) { return item.Guid === guid; })[0];
    return retval;
}

function selectedZona() {
    var guid = $('#dropdownZona option:selected').val();
    var retval = selectedCountry().Zonas.filter(function (item) { return item.Guid === guid; })[0];
    return retval;
}

function selectedLocation() {
    var guid = $('#dropdownLocation option:selected').val();
    var retval = selectedZona().Locations.filter(function (item) { return item.Guid === guid; })[0];
    return retval;
}

function loadDropdown(dropdown, jsonObj, selectedObj) {
    dropdown.empty();
    $.each(jsonObj, function (index, item) {
        var option = $('<option/>', {
            value: item.Guid,
            text: item.Nom,
            selected: (item.Guid === selectedObj.Guid)
        })
        dropdown.append(option);
    });
}

function loadDistributors(div, location) {
    div.empty();
    $.each(location.Distributors, function (index, item) {
        var divItem = jQuery('<div/>', {
            class: "Distributor"
        })
        div.append(divItem);
        divItem.append(
            $('<div/>', {
                text: item.Nom,
                class: "Nom"
            }),
            $('<div/>', {
                text: item.Adr
            }),
            $('<div/>', {
                text: location.Nom
            }),
            $('<div/>', {
                text: 'Tel.: ' + item.Tel
            }),
        );

    });
}

function loadLandingPages(div, landingPages, lang) {
    div.empty();
    if (landingPages.length === 0) {
        $('.online').hide();
    } else {
        $('.Online').show();
        $.each(landingPages, function (index, item) {
            var anchor = $('<a class="OnlineStore" data-landingpage="' + item.Guid + '" href="#" target="_blank"/>');
            anchor.append($('<div class="nom">' + item.Nom + '</div>'));
            anchor.append($('<div class="url">' + item.Url + '</div>'));
            if (item.CustomerStock > 0) {
                var text = '';
                switch (lang) {
                    case 'CAT':
                        text = 'en stock per entrega immediata'
                        break;
                    case 'ENG':
                        text = 'in stock for immediate delivery'
                        break;
                    case 'POR':
                        text = 'em stock para entrega imediata'
                        break;
                    default:
                        text = 'en stock per entrega immediata'
                        break;
                }
                anchor.append($('<div class="InStock">' + text + '</div>'));

            }
            div.append(anchor);
        })
    }
}

$(document).on('click', '.OnlineStore', function (event) {
    event.preventDefault();
    logClickThrough($(this));
    $(document).trigger('storeLocatorChanged');
})

function logClickThrough(thisObj) {
    var landingpage = thisObj.data("landingpage");
    var url = '/wtbol/ClickThroughLog';
    var data = { landingpage: landingpage };

    var jqxhr = $.post(url, data, function (result) {
        var destination = result.url;
        location.href = destination;
    })
        .done(function () {
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            $(event).html('<div class="Error">' + textStatus + '</div>');
        })
        .always(function () {
            $('.Spinner.Loading').hide("fast");
        });
}


