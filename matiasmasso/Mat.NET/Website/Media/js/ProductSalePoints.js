function onDropdownCountryChange() {
    reloadZonas().then(selectBestZona).then(reloadLocations).then(selectBestLocation).then(reloadDistributors);
}

function onDropdownZonaChange() {
    reloadLocations().then(selectBestLocation).then(reloadDistributors);
}

function onDropdownLocationChange() {
    reloadDistributors();
}


function reloadZonas() {
    var deferred = jQuery.Deferred();
    jQuery.ajax({
        url: '/Product/zonas',
        data: {
            'productGuid': productGuid,
            'countryGuid': jQuery('#dropdownCountry').val()
        },
        dataType: "json",
        success: function (result) {
            var dropdown = jQuery('#dropdownZona');
            loadDropdown(dropdown, result);
            deferred.resolve(result);
        }
    })
    return deferred.promise();
}


function reloadLocations() {

    var deferred = jQuery.Deferred();
    jQuery.ajax({
        url: '/product/locations',
        data: {
            'productGuid': productGuid,
            'zonaGuid': $('#dropdownZona option:selected').val()
        },
        dataType: "json",
        success: function (result) {
            var dropdown = jQuery('#dropdownLocation');
            loadDropdown(dropdown, result);
            deferred.resolve(result);
        }
    })
    return deferred.promise();
}



function reloadDistributors() {
    var deferred = jQuery.Deferred();
    var locationGuid = $('#dropdownLocation option:selected').val();
    jQuery('#divDistributors').load('/Product/Distributors', { 'productGuid': productGuid, 'locationGuid': locationGuid });
    deferred.resolve();
    return deferred.promise();
}

function selectBestZona() {
    var deferred = jQuery.Deferred();
    jQuery.ajax({
        url: '/Product/bestZona',
        data: {
            'productGuid': productGuid,
            'countryGuid': jQuery('#dropdownCountry').val()
        },
        dataType: "json",
        success: function (result) {
            jQuery('#dropdownZona').val(result.Guid);
            deferred.resolve();
        }
    })
    return deferred.promise();
}

function selectBestLocation() {
    var deferred = jQuery.Deferred();
    jQuery.ajax({
        url: '/Product/bestLocation',
        data: {
            'productGuid': productGuid,
            'zonaGuid': jQuery('#dropdownZona').val()
        },
        dataType: "json",
        success: function (result) {
            jQuery('#dropdownLocation').val(result.Guid);
            deferred.resolve();
        }
    })
    return deferred.promise();
}
//---helpers


function loadDropdown(dropdown, jsonObj) {
    dropdown.empty();
    jQuery.each(jsonObj, function (index, item) {
        dropdown.append(
            jQuery('<option/>', {
                value: item.Guid,
                text: item.Nom
            })
        );

    });
}