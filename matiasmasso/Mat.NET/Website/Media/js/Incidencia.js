$(document).ready(function () {

    var uploadImages = [];
    var uploadTickets = [];
    var catalog = [];
    var maxSize = 10000000;

    $('.Spinner64').hide();
    var customer = $('#Details').data("customer");
    if (customer !== null) {
       loadCatalog();
    }


    $(document).on('CustomerSelected', function (e, argument) {
        $('#Details').data("customer", argument.guid);

        if (argument.guid === '')
            $('#Details').hide();
        else {
            loadCatalog();
            $('#Details').show();
        }
    });


    $('#procedencia').change(function () {
        loadCatalog();
    });

     $('#productBrand select').change(function () {
        loadCategories();
    });

        $('#productCategory select').change(function () { 
        loadSkus();
    });

        $(".UploadImgs").click(function () {
        event.preventDefault();
        $('#fileBoxImgs').trigger('click');
    });

    $("#fileBoxImgs").change(function () {
        multiPreview('fileBoxImgs', '#images');
    });

    $(".UploadTicket").click(function () {
        event.preventDefault();
        $('#fileBoxTicket').trigger('click');
    });

    $("#fileBoxTicket").change(function () {
        multiPreview('fileBoxTicket', '#ticket');
    });


    $(document).on('click', '.IconRemoveThumbnail', function (event) {
        var thumbnail = $(this).parent();
        var thumbnailCollection = thumbnail.parent();
        var collection = thumbnailCollection.attr("id");
        var index = thumbnail.index();
        removeFile(collection, index);
        thumbnail.remove();
    });

       
    $('#submit input[type=button]').click(function () {
        if ($('#acceptTerms').is(':checked')) {
            update();
        }
        else
            alert($('#acceptTerms').data('warning'));
    });

    $(document).on('afterUpdate', function (e) {
        $('#emailedAdr').val($('#emailAdr').val());
        $('#Customer').hide();
        $('#Details').hide();
        $('#Thanks').show();
    });

    //----------------------------------------------------------- utilities

    function selectedBrand() {
        var guid = $('#productBrand select').val();
        if (guid === '') {
            return null;
        } else {
            var filteredBrands = catalog.filter(function (item) { return item.Guid === guid; });
            return filteredBrands[0];
        }
    }

    function selectedCategory() {
        var guid = $('#productCategory select').val();
        if (guid === '') {
            return null;
        } else {
            var brand = selectedBrand();
            var filteredCategories = brand.Categories.filter(function (item) { return item.Guid === guid; });
            return filteredCategories[0];
        }
    }

    function selecteSku() {
        var guid = $('#productSku select').val();
        if (guid === '') {
            return null;
        } else {
            var category = selectedCategory();
            var filteredSkus = category.Skus.filter(function (item) { return item.Guid === guid; });
            return filteredSkus[0];
        }
    }

    function loadCatalog() {
        var customer = $('#Details').data("customer");
        var procedencia = $('#procedencia').val();
        $.getJSON('/incidencia/Catalog', { customer: customer, procedencia: procedencia })
            .done(function (result) {
                if (result.success) {
                    console.log("second success");
                    console.log(result.data);
                    catalog = result.data;
                    loadBrands();
                } else {
                    console.log(result.message);
                }
            })
            .fail(function (xhr) {
                console.log(xhr.responseText);
            });
    }

    function loadBrands() {
        $('#productBrand select').find('option:gt(0)').remove();
        var $select = $('#productBrand select');
        $.each(catalog, function (key, item) {
            $select.append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
        });
        $('#productBrand select').val('');
        $('#productCategory').hide();
        $('#productSku').hide();
    }

    function loadCategories() {
        var brand = selectedBrand();
        if (brand === null) {
            $('#productCategory').hide();
            $('#productSku').hide();
        } else {
            $('#productCategory select').find('option:gt(0)').remove();
            var $select = $('#productCategory select');
            $.each(brand.Categories, function (key, item) {
                $select.append('<option value=' + item.Guid + '>' + item.Nom + '</option>');
            });
            $('#productCategory select').val('');
            $('#productCategory').show();
            $('#productSku select').val('');
            $('#productSku').hide();
        }
    }

    function loadSkus() {
        var category = selectedCategory();
        if (category === null) {
            $('#productSku').hide();
        } else {
           $('#productSku select').find('option:gt(0)').remove();
            var $select = $('#productSku select');
            $.each(category.Skus, function (key, item) {
                $select.append('<option value=' + item.Guid + '>' + item.NomCurt + '</option>');
            });
            $('#productSku select').val('');
            $('#productSku').show();
        }
    }


    function multiPreview(fileBox, destination) {
        var err = '';

        $(destination).html('');
        for (var i = 0; i < document.getElementById(fileBox).files.length; i++) {
            var file = document.getElementById(fileBox).files[i];
            if (file) {
                var extension = file.name.substr((~-file.name.lastIndexOf(".") >>> 0) + 2).toLowerCase();
                if (extension === 'bmp' || extension === 'tiff' || extension === 'jpg' || extension === 'jpeg' || extension === 'gif' || extension === 'png' || extension === 'mov' || extension === 'mp4' || extension === 'pdf') {

                    if (extension === 'pdf') {
                        previewPdf(file, destination);
                        /*blobURLref = '/Media/Img/Ico/pdfx96.gif';*/
                    }
                    else if (extension === 'mov' || extension === 'mp4') {
                        /*blobURLref = window.URL.createObjectURL(file);*/
                        var html = $(destination).html();
                        blobURLref = window.URL.createObjectURL(file);
                        html += previewVideo(blobURLref, file.name);
                        $(destination).html(html);
                    }
                    else {
                        /*blobURLref = window.URL.createObjectURL(file);*/
                        var html = $(destination).html();
                        blobURLref = window.URL.createObjectURL(file);
                        html += previewHtml(blobURLref, file.name);
                        $(destination).html(html);
                    }

                    pushUpload(destination, file);
                    /*
                    html += '<div class = "Thumbnail" >'
                    html += '<img src="' + blobURLref + '" alt="' + file.name + '" class = "Thumbnail" />'
                    html += '<a href="#" title="clic para eliminar" class="IconRemoveThumbnail" onclick="return false;">'
                    html += '<img src="/Media/Img/Ico/removex24.gif" alt="eliminar" />'
                    html += '</a>'
                    html += '</div>'
                    */
                }

                else
                    err += file.name + ': formato no admitido (solo Jpg, Gif, Png, Pdf, Mp4 o Mov)\n';
            }
        }

        if (err > '')
            alert(err);

        return html;
    }

    function previewPdf(file, destination) {

        var formdata = new FormData();
        formdata.append(file.name, file);

        var xhttp;
        if (window.XMLHttpRequest) {
            xhttp = new XMLHttpRequest();
        } else {
            // code for IE6, IE5
            xhttp = new ActiveXObject("Microsoft.XMLHTTP");
        }

        var url = '/Img/renderPdf/150/150';
        xhttp.open('POST', url);
        xhttp.send(formdata);
        xhttp.onreadystatechange = function () {
            if (xhttp.readyState === 4 && xhttp.status === 200) {
                var response = $.parseJSON(xhttp.response);
                var html = $(destination).html();
                html += previewHtml(response.url, file.name);
                $(destination).html(html);
            }
        };
    }

function pushUpload(destination, file) {
    if (destination === "#images") {
        uploadImages.push(file);
    }
    else if (destination === "#ticket") {
        uploadTickets.push(file);
        }
    }


    function previewVideo(videoSrc, filename) {
        var html = '<div class = "Thumbnail" >';
        html += '<video height="100" width="auto" src="' + videoSrc + '" alt="' + filename + '" class = "Thumbnail" controls>';
        html += '</video>';
        html += '<a href="#" title="clic para eliminar" class="IconRemoveThumbnail" onclick="return false;">';
        html += '<img src="/Media/Img/ico/removex24.gif" alt="eliminar" />';
        html += '</a>';
        html += '</div>';
        return html;
    }

    function previewHtml(imgSrc, filename) {
        var html = '<div class = "Thumbnail" >';
        html += '<img height="100" width="auto" src="' + imgSrc + '" alt="' + filename + '" class = "Thumbnail" />';
        html += '<a href="#" title="clic para eliminar" class="IconRemoveThumbnail" onclick="return false;">';
        html += '<img src="/Media/Img/ico/removex24.gif" alt="eliminar" />';
        html += '</a>';
        html += '</div>';
        return html;
    }

    function removeFile(collection, index) {
        if (collection === "images") {
            uploadImages.splice(index, 1);
        }
        else if (collection === "ticket") {
            uploadTickets.splice(index, 1);
        }
    }

    function appendFilesToFormdata(formdata, files) {
        for (i = 0; i < files.length; i++) {
            formdata.append(files[i].name, files[i]);
        }
    }

    function filesSize(files) {
        var retval = 0;
        for (i = 0; i < files.length; i++) {
            retval += files[i].size;
        }
        return retval;
    }

    function overallSize() {
        var retval = filesSize(uploadImages) +  filesSize(uploadTickets);
        return retval;
    }

    function overallSizeText() {
        var size = Math.ceil(overallSize() / 1000000);
        var retval = size.toString() + 'Mb';
        return retval;
    }

    function exceededSize() {
        var retval = overallSize() > maxSize;
        return retval;
    }

    function update() {
        var size = filesSize(uploadImages) + filesSize(uploadTickets);
        if (exceededSize()) {
            alert('El tamaño de las imágenes es de ' + overallSizeText() + ', lo que excede de los 10Mb admitidos.\nPor favor reduzca su tamaño a un máximo de 800 pixels de lado o suba menos imágenes');
            $('.loading').hide();
        } else {
            $('.Submit').hide();
            $('.Spinner64').show();

            var formdata = new FormData();
            formdata.append('customer', $('#Details').data("customer"));
            formdata.append('contactPerson', $('#contactPerson').val());
            formdata.append('emailAdr', $('#emailAdr').val());
            formdata.append('tel', $('#tel').val());
            formdata.append('customerRef', $('#customerRef').val());
            formdata.append('procedencia', $('#procedencia').val());
            formdata.append('product', product());
            formdata.append('serialnumber', $('#serialNumber').val());
            formdata.append('ManufactureDate', $('#ManufactureDate').val());
            formdata.append('description', $('#description').val());

            //var files = document.getElementById('fileBoxImgs').files

            formdata.append('imgCount', uploadImages.length);
            appendFilesToFormdata(formdata, uploadImages);

            formdata.append('ticketCount', uploadTickets.length);
            appendFilesToFormdata(formdata, uploadTickets);

            var xhttp;
            if (window.XMLHttpRequest) {
                xhttp = new XMLHttpRequest();
            } else {
                // code for IE6, IE5
                xhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }

            //var xhttp = new XMLHttpRequest();
            var url = '/incidencia/save';
            xhttp.open('POST', url);
            xhttp.send(formdata);
            xhttp.onreadystatechange = function () {
                if (xhttp.readyState === 4 && xhttp.status === 200) {
                    $('.loading').hide();
                    var response = $.parseJSON(xhttp.response);
                    if (response.result === 1)
                        $(document).trigger('afterUpdate', response);
                    else {
                        $('.Submit').show();
                        $('.Spinner64').hide();
                        alert(response.message);
                    }
                }
            };
        }
    }


    function product() {
        var retval = $('#productSku select').val();
        if (retval === '') {
            retval = $('#productCategory select').val();
            if (retval === '') {
                retval = $('#productBrand select').val();
            }
        }
        return retval;
    }

});