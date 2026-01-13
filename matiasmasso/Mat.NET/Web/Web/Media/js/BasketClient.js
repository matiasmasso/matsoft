$(document).on('CustomerSelected', function (e, argument) {
    $('#Details').data("customer", argument.guid);

    if (argument.guid === '')
        $('#Details').hide();
    else
        $('#Details').show();
        $('#Details').load('CustomerOrderDetails', { id: argument.guid });
});

$(document).on('click', 'a.AdvancedOptions', function (e) {
    $('div.AdvancedOptions').toggle();
});


$(document).ready(function () {




    $('#ProductBrand select').change(function () {
        var guid = $(this).val();
        var customer = $('#Details').data("customer");
        if (guid === '') {
            $('#ProductCategory').hide();
            $('#ProductSku').hide();
        } else {
            $.getJSON('/BasketClient/ProductCategories', { brand: guid, customer: customer}, function (result) {
                $('#ProductCategory select').find('option:gt(0)').remove();
                var $select = $('#ProductCategory select')
                $.each(result, function (key, item) {
                    $select.append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
                });
                $('#ProductCategory select').val('');
                $('#ProductCategory').show();
                $('#ProductSku select').val('');
            });
        }
    });


    $('#ProductCategory select').change(function () {
        var guid = $(this).val();
        var customer = $('#Details').data("customer");
        if (guid === '') {
            $('#ProductSku').hide();
        } else {
            $.getJSON('/BasketClient/ProductSkus', { category: guid, customer: customer }, function (result) {
                var html = '';

                $.each(result, function (key, item) {
                    html += newSkuBox(item);
                });

                $('#ProductSku').html(html);
                $('#ProductSku').show();
            });
        }
    });

    $(document).on('click', 'a.SkuBox', function (e) {
        event.preventDefault();
        incrementQty($(this));
        recalcTotals();
        $('#CloseOrder').show();
    });

    $(document).on('click', 'a.CellRemove', function (e) {
        event.preventDefault();
        var $BasketRow = $(this).closest('.BasketRow');
        var sku = $BasketRow.data('sku');
        var $SkuBox = $('.SkuBox[data-sku=' + sku + ']');
        $SkuBox.find('.Qty').text('');
        $BasketRow.remove();

        if($('#Basket').html()==='')
            $('#CloseOrder').hide();
        else
            recalcTotals();
    });


    $('#KeepPurchasing a').click(function () {
        event.preventDefault();
        $('#Details').show();
        $('#ProductSku').show();
        $('#Basket').show();
        $('#CloseOrder').show();
    })

    $(document).on('afterUpdate', function (e) {
        $('#emailedAdr').val($('#emailAdr').val());
        $('#Customer').hide();
        $('#Details').hide();
        $('#Thanks').show();
    });



    function newSkuBox(item) {
        var retval = '<a href="#" class="SkuBox truncate" '
        retval += 'data-sku="' + item.Guid + '" '
        retval += 'data-nom="' + item.NomCurt + '" '
        retval += 'data-price="' + item.Precio + '" '
        retval += '>';
        retval += item.NomCurt;
        retval += '<span class="Precio">' + format(item.Precio) + '</span>';
        retval += '<span class="Qty">' + getBasketQty(item.Guid) + '</span>';
        retval += '</a>';
        return retval;
    }

    function incrementQty($SkuBox) {
        $('#Basket').show();

        var $Qty = $SkuBox.find('.Qty');
        var qty = $Qty.text();
        if (qty === '') {
            $Qty.text('1');
            addBasketRow($SkuBox);
        }
        else {
            ++qty;
            $Qty.text(qty);
            var price = $SkuBox.data('price');
            var $BasketRow = $('.BasketRow[data-sku=' + $SkuBox.data('sku') + ']');
            var $CellQty = $BasketRow.find('.CellQty')
            var $CellPreu = $BasketRow.find('.CellPrice')
            var $CellAmt = $BasketRow.find('.CellAmt')
            $CellQty.html(qty);
            $CellAmt.html(format(Number(qty) * Number(price)));
        }
    }



    function addBasketRow($SkuBox) {
        var html = '<div class="BasketRow" data-sku="' + $SkuBox.data('sku') + '">';
        html += '<a href="#" class="CellRemove"><img src="/Media/Img/Ico/aspa.png" /></a>';
        html += '<div class="CellNom truncate">';
        html += $('#ProductBrand select option:selected').text() + ' ';
        html += $('#ProductCategory select option:selected').text() + ' ';
        html += $SkuBox.data('nom');
        html += '</div>';
        html += '<div class="CellQty">1</div>';
        html += '<div class="CellPrice">' + format($SkuBox.data('price')) + '</div>';
        html += '<div class="CellAmt">' + format($SkuBox.data('price')) + '</div>';
        html += '</div>';

        $('#BasketTotal').before(html);
        return html;
    }

    function recalcTotals() {
        var tot = suma($('.CellAmt'));

        $('#BasketTotal').find('.CellTot').html(format(tot));
    }

    function getBasketQty(guid) {
        var $BasketRow = $('.BasketRow[data-sku=' + guid + ']');
        if ($BasketRow) {
            return $BasketRow.find('.CellQty').text();
        }
    }


    function update() {
        $('.loading').show();

        var formdata = new FormData();
        formdata.append('customer', $('#Details').data("customer"));
        formdata.append('contactPerson', $('#contactPerson').val());
        formdata.append('emailAdr', $('#emailAdr').val());
        formdata.append('tel', $('#tel').val());
        formdata.append('customerRef', $('#customerRef').val());
        formdata.append('procedencia', $('#procedencia').val());
        formdata.append('product', product());
        formdata.append('serialnumber', $('#serialNumber').val());
        formdata.append('description', $('#description').val());

        var files = document.getElementById('fileBoxImgs').files
        formdata.append('imgCount', files.length);
        for (i = 0; i < files.length; i++) {
            formdata.append(files[i].name, files[i]);
        }

        files = document.getElementById('fileBoxTicket').files
        formdata.append('ticketCount', files.length);
        for (i = 0; i < files.length; i++) {
            formdata.append(files[i].name, files[i]);
        }

        var xhr = new XMLHttpRequest();
        var url = '/BasketClient/save';
        xhr.open('POST', url);
        xhr.send(formdata);
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                $('.loading').hide();
                var response = $.parseJSON(xhr.response);
                if (response.result === 1)
                    $(document).trigger('afterUpdate', response);
                else
                    alert(response.message);
            }
        }
    }


    function product() {
        var retval = $('#ProductSku select').val();
        if (retval === '') {
            retval = $('#ProductCategory select').val();
            if (retval == '') {
                retval = $('#ProductBrand select').val();
            }
        }
        return retval;
    }


    function format(n) {
        var retval = n.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + '€';
        return retval
    }

    suma = function (selector) {
        var retval = 0;
        $(selector).each(function () {
            var itm = $(this).text().replace('€', '');
            if (!isNaN(Number(itm)))
                retval += Number(itm);
        });
        return retval;
    }




})

