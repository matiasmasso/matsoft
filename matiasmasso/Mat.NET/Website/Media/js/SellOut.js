$(document).ready(function () {


    $("#year").on('change', function (e) {
        refresca();
    });

    $("#conceptType").on('change', function (e) {
        if ($(this).val() === 0) //product
            $('#format').val('1'); //units
        else
            $('#format').val('0'); //amounts
        refresca();
    });

    $("#format").on('change', function (e) {
        refresca();
    });

    $("#brand").on('change', function (e) {
        loadCategories();
        refresca();
    });

    $("#category").on('change', function (e) {
        refresca();
    });

    $("#channel").on('change', function (e) {
        refresca();
    });

    $("#country").on('change', function (e) {
        loadZonas();
        $("#location").find('option').not(':first').remove();
        $("#contact").find('option').not(':first').remove();
        refresca();
    });

    $("#zona").on('change', function (e) {
        loadLocations();
        $("#contact").find('option').not(':first').remove();
        refresca();
    });

    $("#location").on('change', function (e) {
        loadContacts();
        refresca();
    });

    $("#contact").on('change', function (e) {
        refresca();
    });

    $(document).on('click', '.drilldown', function (e) {
        event.preventDefault();
        if ($(this).data("expanded") === "1") {
            collapse($(this));
        } else {
            expand($(this));
        }
        
    });

    function collapse($parent) {
        var guid = $parent.data("guid");
        $('div').filter('[data-parent=' + guid + ']').hide();
        $parent.data("expanded", "0");
        $parent.children('img').attr('src', '/Media/Img/Ico/expand9.png');
    }

    function expand($parent) {
        var guid = $parent.data("guid");
        $('div').filter('[data-parent=' + guid + ']').show();
        $parent.data("expanded","1");
        $parent.children('img').attr('src', '/Media/Img/Ico/collapse9.png');
    }

    function data() {
        var retval = {
            'year': $("#year option:selected").val(),
            'conceptType': $("#conceptType option:selected").val(),
            'format': $("#format option:selected").val(),
            'brand': $("#brand option:selected").val(),
            'category': $("#category option:selected").val(),
            'channel': $("#channel option:selected").val(),
            'country': $("#country option:selected").val(),
            'zona': $("#zona option:selected").val(),
            'location': $("#location option:selected").val(),
            'contact': $("#HiddenCustomerGuid").val(),
            'groupbyholding': $("#HiddenGroupbyholding").val()
        };
        return retval;
    }

    function refresca() {
 
        $(".loading").show();
        $('#divItems').load('/sellout', data(), function () {
            $(".loading").hide();
        });
    }

    $(document).on('click', '#ExcelFullDownload', function (event) {
        event.preventDefault();
        var url = '/sellout/ExcelFull/' + $("#year option:selected").val();
        window.location.href = url;
    });



    //---helpers

    function loadCategories() {
        $.post(
            '/sellout/Categories',
            {
                brand: $("#brand option:selected").val()
            },
            function (result) {
                var $dropdown = $('#category');
                loadDropdown($dropdown, result);
            }
        )

    }


    function loadZonas() {
        $.post(
            '/sellout/Zonas',
            {
                id: $("#country option:selected").val()
            },
            function (result) {
                var $dropdown = $('#zona');
                loadDropdown($dropdown, result);
            }
        )

    }

    function loadLocations() {
        $.post(
            '/sellout/Locations',
            {
                id: $("#zona option:selected").val()
            },
            function (result) {
                var $dropdown = $('#location');
                loadDropdown($dropdown, result);
            }
        )

    }


    function loadContacts() {
        $.post(
            '/sellout/Contacts',
             {
                 location: $("#location option:selected").val()
             },
            function (result) {
                var $dropdown = $('#contact');
                loadDropdown($dropdown, result);
            }
        )

    }

    function loadDropdown($dropdown, jsonObj) {
        $dropdown.empty();
        jQuery.each(jsonObj, function (index, item) {
            $dropdown.append(
                $('<option/>', {
                    value: item.value,
                    text: item.text
                })
            );

        });
    }


})