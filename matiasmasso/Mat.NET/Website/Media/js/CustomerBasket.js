var uploadStickers = [];
var maxFilesSize = 10000000;
var categoriesDownloaded = false;


//--------------------------------------------------------------------------------------- Event Handlers



$(document).on('catalogLoaded', function (e, argument) {
    $('a.AdvancedOptions').show("fast", function () {
        $('.ProductSelection').show("fast");
    });
    $('.Spinner.Loading').hide("fast");

});

$(document).on('click', "a.AdvancedOptions", function (event) {
    $('table.AdvancedOptions').toggle();
});

$(document).on('click', '#CheckboxFchMin', function (event) {
    $('#FchMin').toggle();
});

$(document).on('blur', '#FchMin', function (event) {
    testFchMin();
});

$(document).on('change', '#FchMin', function (event) {
    testFchMin();
});

$(document).on('click', '#StockOnly', function (event) {
    loadBrands();
});


$(document).on('click', '.IconRemoveThumbnail', function (event) {
    var thumbnail = $(this).parent();
    var thumbnailCollection = thumbnail.parent();
    var collection = thumbnailCollection.attr("id");
    var index = thumbnail.index();
    removeFile(collection, index);
    thumbnail.remove();
});

$(document).on('change', '#CheckBoxConditions', function (event) {
    var checked = $('#CheckBoxConditions :checked').is(':checked');
   $('#ButtonSubmit').prop("disabled", !checked);
});


$(document).on('click', '#ButtonKeepPurchasing', function (event) {
    $('.DataCollection').show();
});

$(document).on('click', 'input[name=ports][value=1]', function (event) {
    $('.DataCollection').show();
});

$(document).on('click', '#ButtonCancel', function (event) {
    window.location = $('#MmoBlankOrder').val();
});


$(document).on('click', '#ButtonSubmit', function (event) {
    validateSubmitConditions();
});


$(document).on('updateSuccess', function (e, argument) {
    loadThanks(argument)
});

$(document).on('updateFailure', function (e, argument) {
    loadFailure(argument)
});

$(document).on('click', '#RequestForNewOrder input[type=button]', function (event) {
    window.location = $('#MmoBlankOrder').val();
});



//---------------------------------------------------------------------------------------Database

function update() {
    $('#ButtonSubmit').hide();
    $('.Spinner.Updating').show("fast");
    var url = $('#MmoBasketUpdateUrl').val();
    updateRequest(url, formData());
}

function formData() {
    var retval = new FormData();
    retval.append('purchaseOrder', purchaseOrderFormData());

    var files = fileboxFiles();
    retval.append('stickers', files.length);
    for (i = 0; i < files.length; i++) {
        retval.append(files[i].name, files[i]);
    }
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


//---------------------------------------------------------------------------------------------------------------------

function validateSubmitConditions() {
    var exceptions = []

    if (!$('#CheckBoxConditions :checked').is(':checked'))
        exceptions.push('por favor acepte las condiciones');
    if (purchaseOrder.items - length == 0)
        exceptions.push('el pedido está vacío');

    if (exceptions.length == 0) {
        update();
    } else {
        var msg = '';
        $.each(exceptions, function (key,value) {
            if (msg.length > 0)
                msg += '\n';
            msg += value;
        })
        alert(msg);
    }
}

function testFchMin() {
    var fchmin = new Date($('#FchMin').val());
    var today = new Date();
    var days = dateDiffInDays(today, fchmin);
    if (days > 60 || days < 0) {
        var fchminwarn = $('#FchMin').data('fchminwarn');
        alert(fchminwarn);
        $('#FchMin').val('');
    }
}

function dateDiffInDays(a, b) {
    var _MS_PER_DAY = 1000 * 60 * 60 * 24;
    // Discard the time and time-zone information.
    var utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
    var utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());

    return Math.floor((utc2 - utc1) / _MS_PER_DAY);
}


//---------------------------------------------------------------------------------------------------------------------


//------------------ Upload file

$(".UploadLabel").click(function () {
    event.preventDefault();
    $('#fileBoxStickers').trigger('click');
});

$("#fileBoxStickers").change(function () {
    if (exceededSize()) {
        alert('El tamaño de los archivos es de ' + SizeText(filesSize()) + ', lo que supera el admitido de ' + SizeText(maxFilesSize) + '.');
        $('.loading').hide();
    } else {
        multiPreview('fileBoxStickers', '#stickers');
    }
});

function fileboxFiles() {
    var retval = document.getElementById("fileBoxStickers").files;
    return retval;
}


function filesSize() {
    var retval = 0;
    var files = fileboxFiles();
    for (i = 0; i < files.length; i++) {
        retval += files[i].size;
    }
    return retval;
}

function SizeText(filesSize) {
    var size = Math.ceil(filesSize / 1000000);
    var retval = size.toString() + 'Mb';
    return retval;
}

function exceededSize() {
    var retval = filesSize() > maxFilesSize;
    return retval;
}

//------------------ Upload file common functions

function multiPreview(fileBox, destination) {
    var err = '';

    $(destination).html('');
    for (var i = 0; i < document.getElementById(fileBox).files.length; i++) {
        var file = document.getElementById(fileBox).files[i];
        if (file) {
            var extension = file.name.substr((~-file.name.lastIndexOf(".") >>> 0) + 2).toLowerCase();
            if (extension === 'bmp' || extension === 'tiff' || extension === 'jpg' || extension === 'jpeg' || extension === 'gif' || extension === 'png' || extension === 'pdf') {

                if (extension === 'pdf') {
                    previewPdf(file, destination);
                }
                else {
                    var html = $(destination).html();
                    blobURLref = window.URL.createObjectURL(file);
                    html += previewHtml(blobURLref, file.name);
                    $(destination).html(html);
                }

                uploadStickers.push(file);
            }

            else
                err += file.name + ': formato no admitido (solo Jpg, Gif, Png o Pdf)\n';
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

function previewHtml(imgSrc, filename) {
    var html = '<div class = "PostedFileThumbnail" >';
    html += '       <img src="' + imgSrc + '" alt="' + filename + '" class = "PostedFileThumbnail" />';
    html += '       <a href="#" title="clic para eliminar" class="IconRemoveThumbnail" onclick="return false;">';
    html += '           <img src="/Media/Img/ico/removex24.gif" alt="eliminar" />';
    html += '       </a>';
    html += '   </div>';
    return html;
}


function removeFile(collection, index) {
    if (collection === "stickers") {
        uploadStickers.splice(index, 1);
    }
}


function loadThanks(response) {
    $('.Spinner.Updating').hide("fast");
    $('.DataCollection').hide();
    $('.Thanks').show();
    $(".Thanks").html(response);
}

function loadFailure(response) {
    $('.DataCollection').hide();
    $('.Thanks').show();
    $('.Thanks').html(response);
}

