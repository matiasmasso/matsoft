var productGuid;
var isStoreLocator = true;
var geolocationState;

function loadStoreLocator(product) {
    productGuid = product;
    var url = '/product/StoreLocator';
    var location = '00000000-0000-0000-0000-000000000000';
    var data = { product: product, location: location };
    $('#StoreLocator').load(url, data, function (result) {
    });
}

//Geolocation ====================================================================== */

if (isStoreLocator) {
    if (geolocationState === 'granted') {
        navigator.geolocation.getCurrentPosition(loadNearestNeighbours, errorHandler, { timeout: 10000 });
    } else {
        loadGeoSelector();
    }
}

function geolocationState() {
    if (navigator.permissions) {
        navigator.permissions.query({ name: 'geolocation' })
            .then(function (permissionStatus) {
                return (permissionStatus.state);
            });
    }
}

function loadNearestNeighbours(position) {
    $('.loading').show();
    var url = '/Product/FromGeoLocation';
    $('#StoreLocatorOfflineNearestNeighbours').load(
        url,
        {
            latitud: position.coords.latitude,
            longitud: position.coords.longitude,
            product: productGuid
        },
        function () {
            $('#StoreLocatorOffline').hide();
            $('#StoreLocatorOfflineNearestNeighbours').show();
            $('.loading').hide();
        }
    );
}

function loadGeoSelector() {
    $('#StoreLocatorOfflineNearestNeighbours').hide();
    $('#StoreLocatorOffline').show();
}

function errorHandler(positionError) {
    loadGeoSelector();
    if (window.console) {
        console.log(positionError);
    }
    if (positionError.code == 1) {
        alert('debe autorizar el acceso a su ubicación para poder mostrar los puntos de venta más cercanos');
    }
}

$(document).on('click', ".BackToGeoLocator a", function (event) {
    event.preventDefault();
    loadGeoSelector();
});

$(document).on('click', ".BackToNearestNeighbours a", function (event) {
    event.preventDefault();
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(loadNearestNeighbours, errorHandler, { timeout: 10000 });
    }
});

$(document).on('click', "a.OnlineStore", function (event) {
    event.preventDefault();
    logClickThrough($(this));
});




function logClickThrough(thisObj) {
    //var landingpage = $(this).data("landingpage");

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
            //$('.ProductSelection').html('<div class="Error">' + textStatus + '</div>');
            $(event).html('<div class="Error">' + textStatus + '</div>');
        })
        .always(function () {
            $('.Spinner.Loading').hide("fast");
        });
}


function reportConversion() {
    var fullUrl = window.location.href;
    gtag_report_conversion(fullUrl);
}


// Event snippet for Clic en dónde comprar conversion page In your html page, add the snippet and call gtag_report_conversion when someone clicks on the chosen link or button. -->
function gtag_report_conversion(url) {
    /*var callback = function () { if (typeof (url) != 'undefined') { window.location = url; } };*/
    /*gtag('event', 'conversion', { 'send_to': 'AW-965897101/TpTICN7kn8kBEI3XycwD', 'value': 520.0, 'currency': 'EUR', 'event_callback': callback }); return false;*/
    gtag('event', 'conversion', { 'send_to': 'AW-965897101/TpTICN7kn8kBEI3XycwD', 'value': 100.0, 'currency': 'EUR' });
    return false;
}
