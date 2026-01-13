$(document).ready(function () {
    var $CS_country;
    var $CS_zona;
    var $CS_location;
    var $CS_customer;
    var CS_model;

    function CS_loadCustomerSelection(div, url, guid) {
        var $CS_country = $(div + ':nth-child(1) select')
        var $CS_zona = $(div + ':nth-child(2) select')
        var $CS_location = $(div + ':nth-child(3) select')
        var $CS_customer = $(div + ':nth-child(4) select')

        load_CS_model(url, guid);
    }

    function load_CS_model(url, guid) {
        $.getJSON(url, { guid: guid }, function (result) {
            CS_model = result;
            load_CS_Dropdowns();
        });
    }

    function load_CS_Dropdowns() {
        load_CS_Dropdown($CS_country, CS_countries());
    }

    function load_CS_Dropdown(select, items) {
        select.find('option').remove();
        $.each(items, function (key, value) {
            $('<option>').val(value.Guid).text(value.Nom).appendTo(select);
        });
    }

    $(document).on('change', '.CustomerSelection .Country select', function (e) {
        var $select = $('.CustomerSelection .Zona select');
        loadDropdown($select, zonas());
    });

    $(document).on('change', '.CustomerSelection .Zona select', function (e) {
        var $select = $('.CustomerSelection .Location select');
        loadDropdown($select, locations());
    });

    $(document).on('change', '.CustomerSelection .Location select', function (e) {
        var $select = $('.CustomerSelection .Customer select');
        loadDropdown($select, customers());
    });



    function CS_countries() {
        return CS_model.Data;
    }

    function zonas() {
        var country = $('.CustomerSelection .Country select').val();
        var retval;
        $.each(model.Data, function (key, value) {
            if (value.Guid == country) {
                retval = value.Items;
                return false;
            }
        });
        return retval;
    }
    function locations() {
        var zona = $('.CustomerSelection .Zona select').val();
        var retval;
        $.each(zonas(), function (key, value) {
            if (value.Guid == zona) {
                retval = value.Items;
                return false;
            }
        });
        return retval;
    }
    function customers() {
        var location = $('.CustomerSelection .Location select').val();
        var retval;
        $.each(locations(), function (key, value) {
            if (value.Guid == location) {
                retval = value.Items;
                return false;
            }
        });
        return retval;
    }




});